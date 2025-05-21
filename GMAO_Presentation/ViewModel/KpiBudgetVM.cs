using GMAO_Business.KPI;
using GMAO_Business.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Presentation.ViewModel
{
    public class KpiBudgetVM : INotifyPropertyChanged
    {
        private readonly KPIService kpiService;
        public event PropertyChangedEventHandler PropertyChanged;

        public KpiBudgetVM()
        {
            kpiService = new KPIService();
        }

        public List<KpiBudgetMoisResult> CoutPrevuVsReel { get; set; }
        public List<KpiBudgetResult> EcartBudgets { get; set; }
        public List<KpiEcartParTypeMaintenanceResult> Depassement { get; set; }
        public List<KpiBudgetMensuelResult> DepensesMensuelles { get; set; }

        private int _anneeSelectionnee = DateTime.Now.Year;
        public int AnneeSelectionnee
        {
            get => _anneeSelectionnee;
            set
            {
                _anneeSelectionnee = value;
                OnPropertyChanged(nameof(AnneeSelectionnee));
                LoadMensuel();
                LoadPourcentage();
            }
        }

        public decimal PourcentageUtilisationBudget { get; set; }

        public void LoadData()
        {
            CoutPrevuVsReel = kpiService.GetCoutPrevuVsReelParMois();
            EcartBudgets = kpiService.GetEcartBudgetParAnnee();
            Depassement = kpiService.GetEcartParTypeMaintenanceParEquipement();
            LoadMensuel();
            LoadPourcentage();
            OnAllPropertiesChanged();
        }

        private void LoadMensuel()
        {
            DepensesMensuelles = kpiService.GetDepenseMensuelleCumulative(AnneeSelectionnee);
            OnPropertyChanged(nameof(DepensesMensuelles));
        }

        private void LoadPourcentage()
        {
            PourcentageUtilisationBudget = kpiService.GetPourcentageBudgetUtilise(AnneeSelectionnee);
            OnPropertyChanged(nameof(PourcentageUtilisationBudget));
        }

        private void OnAllPropertiesChanged()
        {
            OnPropertyChanged(nameof(CoutPrevuVsReel));
            OnPropertyChanged(nameof(EcartBudgets));
            OnPropertyChanged(nameof(Depassement));
        }

        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}

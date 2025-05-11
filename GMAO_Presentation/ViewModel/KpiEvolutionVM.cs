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
    public class KpiEvolutionVM : INotifyPropertyChanged
    {
        private readonly KPIService kpiService;
        public event PropertyChangedEventHandler PropertyChanged;

        public KpiEvolutionVM()
        {
            kpiService = new KPIService();
        }

        private int _anneeSelectionnee = DateTime.Now.Year;
        public int AnneeSelectionnee
        {
            get => _anneeSelectionnee;
            set
            {
                _anneeSelectionnee = value;
                OnPropertyChanged(nameof(AnneeSelectionnee));
                LoadEvolution();
            }
        }

        public List<KpiMonthlyEvolutionResult> EvolutionMensuelle { get; set; }

        public void LoadData()
        {
            LoadEvolution();
        }

        private void LoadEvolution()
        {
            EvolutionMensuelle = kpiService.GetEvolutionMensuelleDesCouts(AnneeSelectionnee);
            OnPropertyChanged(nameof(EvolutionMensuelle));
        }

        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

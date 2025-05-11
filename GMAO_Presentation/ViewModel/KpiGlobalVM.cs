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
    public class KpiGlobalVM : INotifyPropertyChanged
    {
        private readonly KPIService kpiService;
        public event PropertyChangedEventHandler PropertyChanged;

        public KpiGlobalVM()
        {
            kpiService = new KPIService();
        }

        public decimal CoutTotalCorrective { get; set; }
        public decimal CoutTotalPlanifiee { get; set; }
        public decimal CoutTotalGlobal { get; set; }
        public decimal CoutMoyenCorrective { get; set; }
        public decimal CoutMoyenPlanifiee { get; set; }

        public List<KpiResult> CoutParEquipement { get; set; }
        public List<KpiResult> RepartitionMaintenance { get; set; }

        public void LoadData()
        {
            CoutTotalCorrective = kpiService.GetCoutTotalMaintenanceCorrective();
            CoutTotalPlanifiee = kpiService.GetCoutTotalMaintenancePlanifiee();
            CoutTotalGlobal = kpiService.GetCoutTotalMaintenance();
            CoutMoyenCorrective = kpiService.GetCoutMoyenMaintenanceCorrective();
            CoutMoyenPlanifiee = kpiService.GetCoutMoyenInterventionPlanifiee();
            CoutParEquipement = kpiService.GetCoutParEquipement();
            RepartitionMaintenance = kpiService.GetRepartitionTypeMaintenance();

            OnAllPropertiesChanged();
        }

        private void OnAllPropertiesChanged()
        {
            OnPropertyChanged(nameof(CoutTotalCorrective));
            OnPropertyChanged(nameof(CoutTotalPlanifiee));
            OnPropertyChanged(nameof(CoutTotalGlobal));
            OnPropertyChanged(nameof(CoutMoyenCorrective));
            OnPropertyChanged(nameof(CoutMoyenPlanifiee));
            OnPropertyChanged(nameof(CoutParEquipement));
            OnPropertyChanged(nameof(RepartitionMaintenance));
        }
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}

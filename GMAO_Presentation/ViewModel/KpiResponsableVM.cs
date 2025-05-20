using GMAO_Business.KPI;
using GMAO_Business.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Presentation.ViewModel
{
    public class KpiResponsableVM : INotifyPropertyChanged
    {
        private readonly KpiService2 _kpiService;

        public ObservableCollection<KpiPlanificationResult> TauxPlanification { get; set; }
        public ObservableCollection<KpiClotureWoResult> TauxClotureWo { get; set; }

        public KpiResponsableVM()
        {
            _kpiService = new KpiService2();

            // Initialiser les collections
            TauxPlanification = new ObservableCollection<KpiPlanificationResult>();
            TauxClotureWo = new ObservableCollection<KpiClotureWoResult>();
        }

        public void LoadData(DateTime dateDebut, DateTime dateFin)
        {
            var planifs = _kpiService.GetTauxPlanificationParResponsable();
            var clotures = _kpiService.GetTauxClotureWoParResponsable(dateDebut, dateFin);

            TauxPlanification.Clear();
            foreach (var p in planifs)
                TauxPlanification.Add(p);

            TauxClotureWo.Clear();
            foreach (var c in clotures)
                TauxClotureWo.Add(c);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

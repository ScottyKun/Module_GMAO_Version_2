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
    public class KpiEquipeVM : INotifyPropertyChanged
    {
        private readonly KpiService2 _kpiService;

        public ObservableCollection<KpiEquipeTempsMoyen> TempsIntervention { get; set; }
        public ObservableCollection<KpiEquipeReussite> TauxReussite { get; set; }

        public KpiEquipeVM()
        {
            _kpiService = new KpiService2();
            TempsIntervention = new ObservableCollection<KpiEquipeTempsMoyen>();
            TauxReussite = new ObservableCollection<KpiEquipeReussite>();
        }

        public void LoadData()
        {
            var temps = _kpiService.GetTempsMoyenInterventionParEquipe();
            var reussites = _kpiService.GetTauxReussiteMaintenanceParEquipe();

            TempsIntervention.Clear();
            foreach (var t in temps)
                TempsIntervention.Add(t);

            TauxReussite.Clear();
            foreach (var r in reussites)
                TauxReussite.Add(r);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

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
    public class KpiEquipementVM : INotifyPropertyChanged
    {
        private readonly KpiService2 _kpiService;

        public ObservableCollection<KpiABCResult> ClassificationABC { get; set; }
        public ObservableCollection<KpiTauxPanneResult> TauxPanne { get; set; }
        public ObservableCollection<KpiMTTRResult> Mttr { get; set; }
        public ObservableCollection<KpiMTBFResult> Mtbf { get; set; }

        public KpiEquipementVM()
        {
            _kpiService = new KpiService2();

            ClassificationABC = new ObservableCollection<KpiABCResult>();
            TauxPanne = new ObservableCollection<KpiTauxPanneResult>();
            Mttr = new ObservableCollection<KpiMTTRResult>();
            Mtbf = new ObservableCollection<KpiMTBFResult>();
        }

        public void LoadData(DateTime dateDebut, DateTime dateFin)
        {
            var abc = _kpiService.GetClassificationABC();
            var pannes = _kpiService.GetTauxPanneParEquipement(dateDebut, dateFin);
            var mttrs = _kpiService.GetMTTRParEquipement();
            var mtbfs = _kpiService.GetMTBFParEquipement();

            ClassificationABC.Clear();
            foreach (var item in abc)
                ClassificationABC.Add(item);

            TauxPanne.Clear();
            foreach (var item in pannes)
                TauxPanne.Add(item);

            Mttr.Clear();
            foreach (var item in mttrs)
                Mttr.Add(item);

            Mtbf.Clear();
            foreach (var item in mtbfs)
                Mtbf.Add(item);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

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
    public class KpiTopVM : INotifyPropertyChanged
    {
        private readonly KPIService kpiService;
        public event PropertyChangedEventHandler PropertyChanged;

        private List<KpiResult> _topEquipements;
        public List<KpiResult> TopEquipements
        {
            get => _topEquipements;
            set
            {
                _topEquipements = value;
                OnPropertyChanged(nameof(TopEquipements));
            }
        }

        private List<KpiResult> _topPieces;
        public List<KpiResult> TopPieces
        {
            get => _topPieces;
            set
            {
                _topPieces = value;
                OnPropertyChanged(nameof(TopPieces));
            }
        }

        public KpiTopVM()
        {
            kpiService = new KPIService();
        }

        public void LoadData()
        {
            TopEquipements = kpiService.GetTopEquipementsCout();
            TopPieces = kpiService.GetTopPiecesUtilisation();
        }

        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

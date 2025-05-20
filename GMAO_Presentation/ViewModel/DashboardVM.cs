using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Presentation.ViewModel
{
    public class DashboardVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public KpiEquipementVM equipementVM { get; set; }
        public KpiEquipeVM equipeVM { get; set; }
        public KpiResponsableVM responsableVM { get; set; }

        public DashboardVM()
        {
            equipeVM = new KpiEquipeVM();
            equipementVM = new KpiEquipementVM();
            responsableVM = new KpiResponsableVM();

        }

        public void LoadAll(DateTime dateDebut, DateTime dateFin)
        {
            equipementVM.LoadData(dateDebut, dateFin);
            equipeVM.LoadData();
            responsableVM.LoadData(dateDebut, dateFin);
        }
    }
}

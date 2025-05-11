using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Presentation.ViewModel
{
    public class KpiDashboardVM : INotifyPropertyChanged
    {
        public KpiTopVM TopVM { get; set; }
        public KpiBudgetVM BudgetVM { get; set; }
        public KpiEvolutionVM EvolutionVM { get; set; }
        public KpiGlobalVM GlobalVM { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


        public KpiDashboardVM()
        {
            TopVM = new KpiTopVM();
            GlobalVM = new KpiGlobalVM();
            BudgetVM = new KpiBudgetVM();
            EvolutionVM = new KpiEvolutionVM();
        }

        public void LoadAll()
        {
            TopVM.LoadData();
            GlobalVM.LoadData();
            BudgetVM.LoadData();
            EvolutionVM.LoadData();
        }
    }
}

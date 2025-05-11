using GMAO_Business.DTOs;
using GMAO_Presentation.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMAO_Presentation.Views
{
    public partial class MaintenanceCoFormAccueil : Form
    {
        private readonly MaintenanceCoAccueilViewModel viewModel;

        public MaintenanceCoFormAccueil(int idResponsable)
        {
            InitializeComponent();
            viewModel = new MaintenanceCoAccueilViewModel(idResponsable);

            gridControl1.DataSource = viewModel.Maintenances;
            gridView1.OptionsBehavior.Editable = false;

            gridView1.BestFitColumns();
            gridView1.DoubleClick += GridView1_DoubleClick;

            btnAjouter.Click += (s, e) => {
                var addForm = new MaintenanceCoAddForm(idResponsable);
                if (addForm.ShowDialog() == DialogResult.OK)
                    viewModel.ChargerMaintenances();
            };

        }




        private void GridView1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle >= 0)
            {
                var selected = gridView1.GetFocusedRow() as MaintenanceCorrectiveDTO;
                if (selected != null)
                {
                    var form = new MaintenanceCoUpdateForm(selected.MaintenanceId);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        if (form.MaintenanceSupprimee)
                        {
                            viewModel.Maintenances.Remove(selected); // Suppression manuelle
                            gridView1.RefreshData(); // Mise à jour visuelle
                        }
                        else if (form.MaintenanceModifiee != null)
                        {
                            var index = viewModel.Maintenances.IndexOf(selected);
                            if (index >= 0)
                            {
                                viewModel.Maintenances[index] = form.MaintenanceModifiee;
                                gridView1.RefreshData();
                            }
                        }
                    }
                }
            }
        }

    }
}

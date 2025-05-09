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
    public partial class MaintenancePlanAccueilForm : Form
    {
        private readonly MaintenancePlanAccVM viewModel;

        public MaintenancePlanAccueilForm(int idResponsable)
        {
            InitializeComponent();

            viewModel = new MaintenancePlanAccVM(idResponsable);

            //dgvMaintenances.AutoGenerateColumns = false;
            dgvMaintenances.DataSource = viewModel.Maintenances;

            btnAjouter.Click += (s, e) =>
            {
                var form = new MaintenancePlanAddForm(idResponsable);
                if (form.ShowDialog() == DialogResult.OK)
                    viewModel.ChargerMaintenances();
            };


            dgvMaintenances.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    var selected = (MaintenancePlanifieeDTO)dgvMaintenances.Rows[e.RowIndex].DataBoundItem;
                    var form = new MaintenancePlanUpdateForm(selected.MaintenanceId);
                    if (form.ShowDialog() == DialogResult.OK)
                        viewModel.ChargerMaintenances();
                }
            };

        }
    }
}

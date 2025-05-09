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

            dataGridView1.DataSource = viewModel.Maintenances;
            btnAjouter.Click += (s, e) => {
                var addForm = new MaintenanceCoAddForm(idResponsable);
                if (addForm.ShowDialog() == DialogResult.OK)
                    viewModel.ChargerMaintenances();
            };

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                var selected = dataGridView1.Rows[e.RowIndex].DataBoundItem as MaintenanceCorrectiveDTO;
                if (selected != null)
                {
                    var form = new MaintenanceCoUpdateForm(selected.MaintenanceId);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        viewModel.ChargerMaintenances();
                        dataGridView1.DataSource = viewModel.Maintenances;
                    }

                }
            }
        }

    }
}

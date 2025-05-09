using GMAO_Business.DTOs;
using GMAO_Business.Entities;
using GMAO_Presentation.Helpers;
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
    public partial class WorkOrderCoAccueilForm : Form
    {
        private readonly WOCoAccueilVM viewModel;

        public int id;
        public WorkOrderCoAccueilForm()
        {
            InitializeComponent();
            viewModel = new WOCoAccueilVM();
            id = UserContext.IdUser;

            dgvWorkOrders.DataSource = viewModel.WorkOrders;
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            var form = new WorkOrderCoAddForm(id);
            if (form.ShowDialog() == DialogResult.OK)
                viewModel.ChargerWorkOrders();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvWorkOrders.Rows[e.RowIndex].DataBoundItem is WorkOrderDTO selected)
            {
                var form = new WorkOrderCoUpdateForm(selected.Id);
                if (form.ShowDialog() == DialogResult.OK)
                    viewModel.ChargerWorkOrders();
            }
        }
    }
}

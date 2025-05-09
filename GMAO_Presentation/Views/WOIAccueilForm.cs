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
    public partial class WOIAccueilForm : Form
    {
        private readonly WOIAccVM viewModel;
        public WOIAccueilForm()
        {
            InitializeComponent();
            viewModel = new WOIAccVM();

            //dgvWorkOrders.AutoGenerateColumns = true;
            dgvWorkOrders.DataSource = viewModel.WorkOrders;

            btnAjouter.Click += (s, e) =>
            {
                var form = new WOIAddForm(UserContext.IdUser);
                if (form.ShowDialog() == DialogResult.OK)
                    viewModel.ChargerWorkOrders();
            };

            dgvWorkOrders.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    var selected = dgvWorkOrders.Rows[e.RowIndex].DataBoundItem as WorkOrderDTO2;
                    if (selected != null)
                    {
                        var form = new WOIUpdateForm(selected.Id);
                        if (form.ShowDialog() == DialogResult.OK)
                            viewModel.ChargerWorkOrders();
                    }
                }
            };

        }
    }
}

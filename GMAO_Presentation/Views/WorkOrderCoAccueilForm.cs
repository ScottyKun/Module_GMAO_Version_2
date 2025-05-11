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

            gridControl1.DataSource = viewModel.WorkOrders;
            gridView1.OptionsBehavior.Editable = false;

            // Double-clic sur une ligne
            gridView1.DoubleClick += GridView1_DoubleClick;
        }
        private void btnAjouter_Click_1(object sender, EventArgs e)
        {
            var form = new WorkOrderCoAddForm(id);
            if (form.ShowDialog() == DialogResult.OK)
                viewModel.ChargerWorkOrders();
                gridView1.RefreshData();
        }

        private void GridView1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle >= 0)
            {
                var selected = gridView1.GetFocusedRow() as WorkOrderDTO;
                if (selected != null)
                {
                    var form = new WorkOrderCoUpdateForm(selected.Id);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        if (form.EstSupprime)
                        {
                            viewModel.WorkOrders.Remove(selected);
                        }
                        else if (form.WorkOrderModifie != null)
                        {
                            var index = viewModel.WorkOrders.IndexOf(selected);
                            if (index >= 0)
                                viewModel.WorkOrders[index] = form.WorkOrderModifie;
                        }

                        gridView1.RefreshData(); // Mise à jour visuelle
                    }
                }
            }
        }
    }
}

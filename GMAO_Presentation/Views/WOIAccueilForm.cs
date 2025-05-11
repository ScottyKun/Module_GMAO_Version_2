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

            gridControl1.DataSource = viewModel.WorkOrders;
            gridView1.OptionsBehavior.Editable = false;

            btnAjouter.Click += (s, e) =>
            {
                var form = new WOIAddForm(UserContext.IdUser);
                if (form.ShowDialog() == DialogResult.OK)
                    viewModel.ChargerWorkOrders();
            };

            gridView1.DoubleClick += GridView1_DoubleClick;

        }

        private void GridView1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle >= 0)
            {
                var selected = gridView1.GetFocusedRow() as WorkOrderDTO2;
                if (selected != null)
                {
                    var form = new WOIUpdateForm(selected.Id);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        if (form.Supprime)
                        {
                            viewModel.WorkOrders.Remove(selected);
                            gridView1.RefreshData(); // Mise à jour visuelle après suppression
                        }
                        else if (form.WorkOrderModifie != null)
                        {
                            var index = viewModel.WorkOrders.IndexOf(selected);
                            if (index >= 0)
                            {
                                viewModel.WorkOrders[index] = form.WorkOrderModifie;
                                gridView1.RefreshData();
                            }
                        }
                    }

                }
            }
        }
    }
}

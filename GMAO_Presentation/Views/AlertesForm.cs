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
    public partial class AlertesForm : Form
    {
        private readonly AlerteAccVM viewModel;
        public AlertesForm()
        {
            InitializeComponent();

            viewModel = new AlerteAccVM();

            dgvAlertes.AutoGenerateColumns = false;
            dgvAlertes.DataSource = viewModel.Alertes;

            dgvAlertes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Libelle",
                HeaderText = "Libellé"
            });

            dgvAlertes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Priorite",
                HeaderText = "Priorité"
            });
            dgvAlertes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DateCreation",
                HeaderText = "Date"
            });

            dgvAlertes.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "Terminee",
                HeaderText = "Lue"
            });

            dgvAlertes.CellDoubleClick += (s, e) =>
            {
                if (dgvAlertes.CurrentRow?.DataBoundItem is AlerteDTO selected)
                {
                    var form = new AlerteDetailForm(selected.Id);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        viewModel.ChargerAlertes();
                    }
                }
            };
        }
    }
}

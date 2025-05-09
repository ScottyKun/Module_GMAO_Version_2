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
    public partial class BudgetAccForm : Form
    {
        private readonly BudgetAccVM viewModel;
        public BudgetAccForm()
        {
            InitializeComponent();

            viewModel = new BudgetAccVM();
            dgvBudgets.DataSource = viewModel.Budgets;
            ConfigurerDataGridView();

            btnAjouter.Click += (s, e) =>
            {
                var form = new BudgetAddForm();
                if (form.ShowDialog() == DialogResult.OK)
                    viewModel.ChargerBudgets();
            };

            dgvBudgets.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0 && dgvBudgets.Rows[e.RowIndex].DataBoundItem is GMAO_Business.DTOs.BudgetDTO selected)
                {
                    var form = new BudgetUpForm(selected);
                    if (form.ShowDialog() == DialogResult.OK)
                        viewModel.ChargerBudgets();
                }
            };

        }
        private void ConfigurerDataGridView()
        {
            dgvBudgets.AutoGenerateColumns = false;
            dgvBudgets.Columns.Clear();

            // Colonne Nom
            dgvBudgets.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Nom",
                HeaderText = "Nom",
                Width = 150
            });

            // Colonne Année
            dgvBudgets.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Annee",
                HeaderText = "Année",
                Width = 80
            });

            // Colonne Montant
            dgvBudgets.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Montant",
                HeaderText = "Montant",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } // format 0.00
            });

            // Colonne Date de création
            dgvBudgets.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DateCreation",
                HeaderText = "Date de création",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            // Lier les données
            dgvBudgets.DataSource = viewModel.Budgets;
        }


    }
}

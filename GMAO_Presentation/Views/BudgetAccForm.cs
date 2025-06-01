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
    public partial class BudgetAccForm : Form
    {
        private readonly BudgetAccVM viewModel;
        public BudgetAccForm()
        {
            InitializeComponent();

            viewModel = new BudgetAccVM();
            gridControlBudgets.DataSource = viewModel.Budgets;
            ConfigurerDataGridView();

            btnAjouter.Click += (s, e) =>
            {
                var form = new BudgetAddForm();
                if (form.ShowDialog() == DialogResult.OK)
                    ActualiserDonnees();
            };

            gridViewBudgets.DoubleClick += (s, e) =>
            {
                var selected = gridViewBudgets.GetFocusedRow() as BudgetDTO;
                if (selected != null)
                {
                    var form = new BudgetUpForm(selected);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        // Récupérer le nouveau budget modifié
                        var updatedBudget = form.BudgetModifie;
                        if (updatedBudget != null)
                        {
                            // Remplacer manuellement dans la liste bindée
                            int index = viewModel.Budgets.IndexOf(selected);
                            if (index >= 0)
                            {
                                viewModel.Budgets[index] = updatedBudget;
                            }
                        }
                    }
                }
            };

        }
        private void ConfigurerDataGridView()
        {
            gridViewBudgets.OptionsBehavior.Editable = false;
            gridViewBudgets.OptionsView.ShowGroupPanel = false;
            gridViewBudgets.OptionsBehavior.ReadOnly = true;

            gridViewBudgets.Columns.Clear();

            gridViewBudgets.Columns.AddVisible("Nom", "Nom").Width = 150;
            gridViewBudgets.Columns.AddVisible("Annee", "Année").Width = 80;
            gridViewBudgets.Columns.AddVisible("Montant", "Montant").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridViewBudgets.Columns["Montant"].DisplayFormat.FormatString = "n2";
            gridViewBudgets.Columns["Montant"].Width = 120;

            gridViewBudgets.Columns.AddVisible("DateCreation", "Date de création").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridViewBudgets.Columns["DateCreation"].DisplayFormat.FormatString = "dd/MM/yyyy";
            gridViewBudgets.Columns["DateCreation"].Width = 130;
        }

        private void ActualiserDonnees()
        {
            viewModel.ChargerBudgets();
            gridControlBudgets.RefreshDataSource(); // Important avec BindingList
        }

    }
}

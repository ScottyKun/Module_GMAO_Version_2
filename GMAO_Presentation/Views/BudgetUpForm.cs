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
    public partial class BudgetUpForm : Form
    {
        private readonly BudgetUpVM viewModel;
        public BudgetDTO BudgetModifie { get; private set; }
        public BudgetUpForm(BudgetDTO budget)
        {
            InitializeComponent();

            viewModel = new BudgetUpVM(budget);

            txtNom.DataBindings.Add("Text", viewModel, "Nom");
            numAnnee.DataBindings.Add("Value", viewModel, "Annee");
            numMontant.DataBindings.Add("Value", viewModel, "Montant");
            dtCreation.DataBindings.Add("Value", viewModel, "DateAjout");

            dtCreation.Enabled = false;

            btnUpdate.Click += (s, e) =>
            {
                if (viewModel.ModifierCommand.CanExecute(null))
                    viewModel.ModifierCommand.Execute(null);
            };

            viewModel.OnClose += () =>
            {
                BudgetModifie = new BudgetDTO
                {
                    BudgetId = budget.BudgetId, // important si tu l’utilises dans l’update
                    Nom = txtNom.Text,
                    Annee = int.Parse(numAnnee.Text),
                    Montant = decimal.Parse(numMontant.Text),
                    DateCreation = dtCreation.Value
                };

                MessageBox.Show("Budget modifié avec succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.OK;
                Close();
            };

            viewModel.OnError += msg =>
            {
                MessageBox.Show(msg, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
        }
    }
}

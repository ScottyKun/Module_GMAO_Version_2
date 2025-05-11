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
    public partial class BudgetAddForm : Form
    {
        private readonly BudgetAddVM viewModel;

        public BudgetAddForm()
        {
            InitializeComponent();

            viewModel = new BudgetAddVM();

            txtNom.DataBindings.Add("Text", viewModel, "Nom");
            numAnnee.DataBindings.Add("Value", viewModel, "Annee");
            numMontant.DataBindings.Add("Value", viewModel, "Montant");
            dtCreation.DataBindings.Add("Value", viewModel, "DateAjout");

            btnAjouter.Click += (s, e) =>
            {
                if (viewModel.AjouterCommand.CanExecute(null))
                    viewModel.AjouterCommand.Execute(null);
            };

            viewModel.OnClose += () =>
            {
                MessageBox.Show("Budget ajouté avec succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

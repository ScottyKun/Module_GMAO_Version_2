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
    public partial class WOIAddForm : Form
    {
        private readonly WOIAddVM viewModel;
        public WOIAddForm(int idResponsable, string description = null, int? interventionId = null)
        {
            InitializeComponent();
            viewModel = new WOIAddVM(idResponsable, interventionId, description);

            txtNom.DataBindings.Add("Text", viewModel, "Nom");
            txtRapport.DataBindings.Add("Text", viewModel, "Rapport");
            dtExecution.DataBindings.Add("Value", viewModel, "DateExecution");
            txtDescription.DataBindings.Add("Text", viewModel, "DescriptionIntervention");

            comboInterventions.DataSource = viewModel.InterventionsDispo;
            comboInterventions.DisplayMember = "Id";
            comboInterventions.ValueMember = "Id";
            comboInterventions.DataBindings.Add("SelectedValue", viewModel, "SelectedInterventionId", false, DataSourceUpdateMode.OnPropertyChanged);

            comboInterventions.SelectedValueChanged += (s, e) =>
            {
                viewModel.MiseAJourDescription();
            };

            btnAjouter.Click += (s, e) =>
            {
                var confirm = MessageBox.Show("Confirmer l'ajout  ?", "Ajouter", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.Yes)
                {
                    if (viewModel.AjouterCommand.CanExecute(null))
                        viewModel.AjouterCommand.Execute(null);
                }

            };

            viewModel.AjouterCommand.CanExecuteChanged += (s, e) =>
            {
                btnAjouter.Enabled = viewModel.AjouterCommand.CanExecute(null);
            };

            viewModel.OnClose += () =>
            {

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

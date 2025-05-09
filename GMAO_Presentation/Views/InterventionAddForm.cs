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
    public partial class InterventionAddForm : Form
    {
        private readonly InterventionAddVM viewModel;
        public InterventionAddForm(int? maintenanceId = null, string description = null)
        {
            InitializeComponent();

            viewModel = new InterventionAddVM(maintenanceId, description);

            comboMaintenance.DataSource = viewModel.MaintenancesDispo;
            comboMaintenance.DisplayMember = "MaintenanceId";
            comboMaintenance.ValueMember = "MaintenanceId";
            comboMaintenance.DataBindings.Add("SelectedValue", viewModel, "SelectedMaintenanceId", false, DataSourceUpdateMode.OnPropertyChanged);

            // comboMaintenance.Enabled = !viewModel.EstConversion; // désactiver combo si conversion
            // txtDescription.Enabled= !viewModel.EstConversion;

            /* comboMaintenance.SelectedIndexChanged += (s, e) =>
             {
                 viewModel.MiseAJourDescription();
                 //viewModel.EnregistrerCommand.RaiseCanExecuteChanged();
             };*/

            txtNom.DataBindings.Add("Text", viewModel, "Name", false, DataSourceUpdateMode.OnPropertyChanged);
            txtStatut.DataBindings.Add("Text", viewModel, "Statut");
            txtStatut.Enabled = false;

            dtPrevue.DataBindings.Add("Value", viewModel, "Date", false, DataSourceUpdateMode.OnPropertyChanged);
            txtDescription.DataBindings.Add("Text", viewModel, "DescriptionMaintenance", true, DataSourceUpdateMode.OnPropertyChanged);

            btnAjouter.Click += (s, e) =>
            {
                viewModel.EnregistrerCommand.Execute(null);
            };

            viewModel.OnClose += () =>
            {
                MessageBox.Show("Intervention ajoutée avec succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            };

            viewModel.OnError += (msg) =>
            {
                MessageBox.Show(msg, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            viewModel.EnregistrerCommand.CanExecuteChanged += (s, e) =>
            {
                btnAjouter.Enabled = viewModel.EnregistrerCommand.CanExecute(null);
            };
        }

        private void comboMaintenance_SelectedValueChanged(object sender, EventArgs e)
        {
            viewModel.MiseAJourDescription();
        }

    }
}

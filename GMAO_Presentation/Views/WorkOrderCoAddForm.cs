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
    public partial class WorkOrderCoAddForm : Form
    {
        private readonly WOCoAddVM viewModel;
        public WorkOrderCoAddForm(int idResponsable, int? maintenanceId = null, string description = null)
        {
            InitializeComponent();
            viewModel = new WOCoAddVM(idResponsable, maintenanceId, description);

            comboMaintenance.DataSource = viewModel.Maintenances;
            comboMaintenance.DisplayMember = "MaintenanceId";
            comboMaintenance.ValueMember = "MaintenanceId";
            comboMaintenance.DataBindings.Add("SelectedValue", viewModel, "SelectedMaintenanceId", false, DataSourceUpdateMode.OnPropertyChanged);

            comboMaintenance.SelectedValueChanged += (s, e) =>
            {
                viewModel.MiseAJourDescription();
            };

            txtNom.DataBindings.Add("Text", viewModel, "Nom", false, DataSourceUpdateMode.OnPropertyChanged);
            txtDescription.DataBindings.Add("Text", viewModel, "Description", false, DataSourceUpdateMode.Never);
            txtRapport.DataBindings.Add("Text", viewModel, "Rapport", false, DataSourceUpdateMode.OnPropertyChanged);
            dtExecution.DataBindings.Add("Value", viewModel, "DateExecution", false, DataSourceUpdateMode.OnPropertyChanged);

            //btnAjouter.DataBindings.Add("Enabled", viewModel.EnregistrerCommand, "CanExecute", false, DataSourceUpdateMode.OnPropertyChanged);
            btnAjouter.Click += (s, e) => {
                viewModel.EnregistrerCommand.Execute(null);
                DialogResult = DialogResult.OK;
                Close();
            };

            viewModel.OnClose += () =>
            {
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

    }
}

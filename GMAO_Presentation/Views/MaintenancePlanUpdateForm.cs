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
    public partial class MaintenancePlanUpdateForm : Form
    {
        private readonly MaintenancePlanUpVM viewModel;

        public bool EstSupprimee { get; private set; } = false;

        public MaintenancePlanifieeDTO MaintenanceModifiee { get; private set; }

        public MaintenancePlanUpdateForm(int maintenanceId)
        {
            InitializeComponent();

            viewModel = new MaintenancePlanUpVM(maintenanceId);

            cbEquipements.DisplayMember = "Nom";
            cbEquipements.ValueMember = "Id";
            cbEquipements.DataSource = viewModel.Equipements;
            cbEquipements.DataBindings.Add("SelectedValue", viewModel, "EquipementId");

            txtDescription.DataBindings.Add("Text", viewModel, "Description", false, DataSourceUpdateMode.OnPropertyChanged);
            txtStatut.DataBindings.Add("Text", viewModel, "Statut");
            txtResponsable.DataBindings.Add("Text", viewModel, "NomResponsable");
            txtEquipe.DataBindings.Add("Text", viewModel, "NomEquipe");
            dtDebut.DataBindings.Add("Value", viewModel, "DateDebut", false, DataSourceUpdateMode.OnPropertyChanged);
            dtFin.DataBindings.Add("Value", viewModel, "DateFin", false, DataSourceUpdateMode.OnPropertyChanged);
            numericRecurrence.DataBindings.Add("Value", viewModel, "Recurrence", false, DataSourceUpdateMode.OnPropertyChanged);

            btnModifier.Click += (s, e) =>
            {
                viewModel.ModifierCommand.Execute(null);
            };

            btnSupprimer.Click += (s, e) =>
            {
                var confirm = MessageBox.Show("Supprimer cette maintenance ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.Yes)
                {
                    viewModel.SupprimerCommand.Execute(null);
                    EstSupprimee = true;
                    DialogResult = DialogResult.OK;
                    Close();
                }
            };


            btnConvertir.Click += (s, e) =>
            {
                viewModel.ConvertirCommand.Execute(null);
            };
            viewModel.OnClose += () =>
            {
                MaintenanceModifiee = viewModel.GetById(maintenanceId);
                DialogResult = DialogResult.OK;
                Close();
            };


            viewModel.OnError += (msg) =>
            {
                MessageBox.Show(msg, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            viewModel.OnConvertToIntervention += (id, desc) =>
            {
                var form = new InterventionAddForm(id, desc);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            };

            viewModel.ModifierCommand.CanExecuteChanged += (s, e) =>
            {
                btnModifier.Enabled = viewModel.ModifierCommand.CanExecute(null);
            };
        }
    }
}

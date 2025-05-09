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
    public partial class MaintenancePlanAddForm : Form
    {
        private readonly MaintenancePlanAddVM viewModel;
        public MaintenancePlanAddForm(int idResponsable)
        {
            InitializeComponent();

            viewModel = new MaintenancePlanAddVM(idResponsable);

            cbEquipements.DataSource = viewModel.Equipements;
            cbEquipements.DisplayMember = "Nom";
            cbEquipements.ValueMember = "Id";
            // cbEquipements.DataBindings.Add("SelectedValue", viewModel, "EquipementId", false, DataSourceUpdateMode.OnPropertyChanged);

            cbEquipements.SelectedValueChanged += (s, e) =>
            {
                if (cbEquipements.SelectedValue is int id)
                {
                    viewModel.EquipementId = id;
                    viewModel.ChargerInfos(id);
                }

            };

            txtResponsable.DataBindings.Add("Text", viewModel, "NomResponsable");
            txtEquipe.DataBindings.Add("Text", viewModel, "NomEquipe");
            txtStatut.DataBindings.Add("Text", viewModel, "Statut");

            txtEquipe.Enabled = false;
            txtResponsable.Enabled = false;
            txtStatut.Enabled = false;

            txtDescription.DataBindings.Add("Text", viewModel, "Description");
            dtDebut.DataBindings.Add("Value", viewModel, "DateDebut");
            dtFin.DataBindings.Add("Value", viewModel, "DateFin");
            numRecurrence.DataBindings.Add("Value", viewModel, "Recurrence", false, DataSourceUpdateMode.OnPropertyChanged);


            viewModel.EnregistrerCommand.CanExecuteChanged += (s, e) =>
            {
                btnAjouter.Enabled = viewModel.EnregistrerCommand.CanExecute(null);
            };

            btnAjouter.Click += (s, e) =>
            {
                viewModel.EnregistrerCommand.Execute(null);
            };

            viewModel.OnClose += () =>
            {
                MessageBox.Show("Maintenance ajoutée avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            };

            viewModel.OnError += (msg) =>
            {
                MessageBox.Show(msg, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };


        }
    }
}

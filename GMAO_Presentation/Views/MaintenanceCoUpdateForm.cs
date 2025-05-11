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
    public partial class MaintenanceCoUpdateForm : Form
    {
        private readonly MaintenanceCoUpdateViewModel viewModel;
        public bool MaintenanceSupprimee { get; private set; }


        public MaintenanceCorrectiveDTO MaintenanceModifiee { get; private set; }
        public MaintenanceCoUpdateForm(int maintenanceId)
        {
            InitializeComponent();
            viewModel = new MaintenanceCoUpdateViewModel(maintenanceId);


            txtDescription.DataBindings.Add("Text", viewModel, "Description", false, DataSourceUpdateMode.OnPropertyChanged);
            txtStatut.DataBindings.Add("Text", viewModel, "Statut", false, DataSourceUpdateMode.OnPropertyChanged);
            txtStatut.Enabled = false;
            txtResponsable.DataBindings.Add("Text", viewModel, "ResponsableNom", false, DataSourceUpdateMode.OnPropertyChanged);
            txtEquipe.DataBindings.Add("Text", viewModel, "EquipeMaintenanceNom", false, DataSourceUpdateMode.OnPropertyChanged);

            txtResponsable.Enabled = false;
            txtEquipe.Enabled = false;

            cbEquipement.DataSource = viewModel.Equipements;
            cbEquipement.DisplayMember = "Nom";
            cbEquipement.ValueMember = "Id";
            cbEquipement.DataBindings.Add("SelectedValue", viewModel, "EquipementId", false, DataSourceUpdateMode.OnPropertyChanged);

            cbEquipement.Enabled = false;

            dgvPieces.AutoGenerateColumns = false;

            // ID
            var colId = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PieceId",
                HeaderText = "ID",
                ReadOnly = true
            };

            // Nom
            var colNom = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Nom",
                HeaderText = "Nom",
                ReadOnly = true
            };

            // Quantité en stock
            var colStock = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "QuantiteStock",
                HeaderText = "Qté stock",
                ReadOnly = true
            };

            // Quantité à réserver (editable)
            var colResa = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "QuantiteAReserver",
                HeaderText = "Qté à réserver",
                ReadOnly = false
            };

            // Ajout dans le DataGridView
            dgvPieces.Columns.Clear();
            dgvPieces.Columns.AddRange(new DataGridViewColumn[] { colId, colNom, colStock, colResa });

            dgvPieces.DataSource = viewModel.Pieces;

            btnModofier.Click += (s, e) =>
            {

                var confirm = MessageBox.Show("Confirmer la modificaton ?", "Modification", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.Yes)
                    viewModel.ModifierCommand.Execute(null);

            };

            btnSupprimer.Click += (s, e) =>
            {
                var confirm = MessageBox.Show("Confirmer la suppression ?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.Yes)
                {
                    viewModel.SupprimerCommand.Execute(null);
                    MaintenanceSupprimee = true;
                    DialogResult = DialogResult.OK;
                    Close();
                }
            };

            btnConvertir.Click += (s, e) => viewModel.ConvertirCommand.Execute(null);

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

            viewModel.OnConvertToWorkOrder += (idResponsable, mId, description) =>
            {
                var form = new WorkOrderCoAddForm(idResponsable, mId, description);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            };

            viewModel.ModifierCommand.CanExecuteChanged += (s, e) =>
            {
                btnModofier.Enabled = viewModel.ModifierCommand.CanExecute(null);
            };
        }

    }
}

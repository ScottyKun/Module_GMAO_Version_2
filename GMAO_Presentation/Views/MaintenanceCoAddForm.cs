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
    public partial class MaintenanceCoAddForm : Form
    {
        private readonly MaintenanceCoAddViewModel viewModel;
        public MaintenanceCoAddForm(int idResponsable)
        {
            InitializeComponent();
            viewModel = new MaintenanceCoAddViewModel(idResponsable);

            cbEquipement.DataSource = viewModel.Equipements;
            cbEquipement.DisplayMember = "Nom";
            cbEquipement.ValueMember = "Id";

            cbEquipement.SelectedIndexChanged += (s, e) =>
            {
                if (cbEquipement.SelectedValue is int equipId)
                {
                    viewModel.EquipementId = equipId;
                    viewModel.ChargerPiecesEtInfos(equipId);
                }
            };

            txtDescription.DataBindings.Add("Text", viewModel, "Description", false, DataSourceUpdateMode.OnPropertyChanged);
            txtStatut.Text = viewModel.Statut;
            txtStatut.Enabled = false;
            txtResponsable.DataBindings.Add("Text", viewModel, "NomResponsable", false, DataSourceUpdateMode.OnPropertyChanged);
            txtEquipe.DataBindings.Add("Text", viewModel, "NomEquipe", false, DataSourceUpdateMode.OnPropertyChanged);

            txtResponsable.Enabled = false;
            txtEquipe.Enabled = false;

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


            dgvPieces.DataSource = viewModel.PiecesDisponibles;



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

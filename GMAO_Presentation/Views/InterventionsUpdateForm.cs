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
    public partial class InterventionsUpdateForm : Form
    {
        private readonly InterventionUpVM viewModel;

        public InterventionsUpdateForm(int interventionId)
        {
            InitializeComponent();

            viewModel = new InterventionUpVM(interventionId);

            txtNom.DataBindings.Add("Text", viewModel, "Nom");
            txtStatut.DataBindings.Add("Text", viewModel, "Statut");
            datPrevue.DataBindings.Add("Value", viewModel, "DatePrevue");

            txtDescription.DataBindings.Add("Text", viewModel, "DescriptionMaintenance");
            txtEquipement.DataBindings.Add("Text", viewModel, "EquipementNom");

            txtMaintenanceId.Text = viewModel.InterventionId.ToString();

            txtStatut.Enabled = false;
            txtDescription.Enabled = false;
            txtMaintenanceId.Enabled = false;
            txtEquipement.Enabled = false;


            ConfigurerDataGridPieces();

            btnModifier.Click += (s, e) =>
            {
                if (viewModel.ModifierCommand.CanExecute(null))
                    viewModel.ModifierCommand.Execute(null);
            };

            btnSupprimer.Click += (s, e) =>
            {
                var confirm = MessageBox.Show("Confirmer la suppression ?", "Confirmation", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                    viewModel.SupprimerCommand.Execute(null);
            };

            btnConvertir.Click += (s, e) =>
            {
                viewModel.ConvertirCommand.Execute(null);
            };

            viewModel.OnClose += () =>
            {
                var confirm = MessageBox.Show("Confirmer la modification ?", "Confirmation", MessageBoxButtons.YesNo);
                DialogResult = DialogResult.OK;
                Close();
            };

            viewModel.OnError += msg =>
            {
                MessageBox.Show(msg, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            viewModel.OnConvertToWorkOrder += (id, desc, id2) =>
            {

                var form = new WOIAddForm(id, desc, id2);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            };
        }

        private void ConfigurerDataGridPieces()
        {
            dgvPieces.Columns.Clear();
            dgvPieces.AutoGenerateColumns = false;
            dgvPieces.DataSource = viewModel.PiecesDisponibles;

            // === Colonne Nom (lecture seule) ===
            var colNom = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Nom",
                HeaderText = "Nom",
                ReadOnly = true,
                Width = 150
            };

            // === Colonne Stock (lecture seule) ===
            var colStock = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "QuantiteStock",
                HeaderText = "Stock",
                ReadOnly = true,
                Width = 80
            };

            // === Colonne Quantité à réserver (editable avec numeric up down) ===
            var colQuantite = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "QuantiteAReserver",
                HeaderText = "À réserver",
                ReadOnly = false,
                Width = 100
            };

            dgvPieces.Columns.AddRange(colNom, colStock, colQuantite);
        }
    }
}

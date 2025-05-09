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
    public partial class WorkOrderCoUpdateForm : Form
    {
        private readonly WOCoUpdateVM viewModel;

        public WorkOrderCoUpdateForm(int workOrderId)
        {
            InitializeComponent();
            viewModel = new WOCoUpdateVM(workOrderId);

            txtNom.DataBindings.Add("Text", viewModel, "Nom", false, DataSourceUpdateMode.OnPropertyChanged);
            txtDescription.DataBindings.Add("Text", viewModel, "MaintenanceDescription", true, DataSourceUpdateMode.OnPropertyChanged);
            txtDescription.Enabled = false;

            txtRapport.DataBindings.Add("Text", viewModel, "Rapport", false, DataSourceUpdateMode.OnPropertyChanged);
            dtExecution.DataBindings.Add("Value", viewModel, "DateExecution", false, DataSourceUpdateMode.OnPropertyChanged);

            txtMaintenanceId.DataBindings.Add("Text", viewModel, "MaintenanceId", true, DataSourceUpdateMode.OnPropertyChanged);
            txtMaintenanceId.Enabled = false;


            gridPiecesReservees.AutoGenerateColumns = false;
            gridPiecesReservees.Columns.Clear();

            gridPiecesReservees.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "ID",
                DataPropertyName = "PieceId",
                Name = "PieceId"
            });

            gridPiecesReservees.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Nom",
                DataPropertyName = "Nom"
            });

            gridPiecesReservees.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Quantité réservée",
                DataPropertyName = "QuantiteAReserver"
            });





            gridPiecesReservees.DataSource = viewModel.PiecesReservees;
            gridPiecesUtilisees.DataSource = viewModel.PiecesUtilisees;

            viewModel.OnClose += () =>
            {
                DialogResult = DialogResult.OK;
                Close();
            };

            viewModel.OnError += msg =>
            {
                MessageBox.Show(msg, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            btnModifier.Click += (s, e) =>
            {
                var confirm = MessageBox.Show("Confirmer la modification ?", "Modifier", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.Yes)
                {
                    viewModel.ModifierCommand.Execute(null);
                }
            };

            btnSupprimer.Click += (s, e) =>
            {
                var confirm = MessageBox.Show("Confirmer la suppression ?", "Supprimer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.Yes)
                {
                    viewModel.SupprimerCommand.Execute(null);
                }
            };

            btnTerminer.Click += (s, e) =>
            {
                var confirm = MessageBox.Show("Confirmer la terminaison ?", "Terminer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    viewModel.TerminerCommand.Execute(null);
                }
            };

            btnImpossible.Click += (s, e) =>
            {
                var confirm = MessageBox.Show("Marquer cette maintenance comme impossible ?", "Échec", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (confirm == DialogResult.Yes)
                {
                    viewModel.ImpossibleCommand.Execute(null);
                }
            };

        }

        private void btnUtiliser_Click(object sender, EventArgs e)
        {
            if (gridPiecesReservees.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une pièce réservée à utiliser.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = gridPiecesReservees.SelectedRows[0];
            var pieceId = (int)row.Cells["PieceId"].Value;

            viewModel.AjouterPieceUtiliseeDepuisReservation(pieceId);
        }

    }
}

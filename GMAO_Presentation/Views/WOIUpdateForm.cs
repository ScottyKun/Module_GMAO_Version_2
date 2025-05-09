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
    public partial class WOIUpdateForm : Form
    {
        private readonly WOIUpVM viewModel;
        public WOIUpdateForm(int workOrderId)
        {
            InitializeComponent();
            viewModel = new WOIUpVM(workOrderId);

            txtNom.DataBindings.Add("Text", viewModel, "Nom");
            txtRapport.DataBindings.Add("Text", viewModel, "Rapport");
            dtExecution.DataBindings.Add("Value", viewModel, "DateExecution");

            txtDescription.DataBindings.Add("Text", viewModel, "DescriptionIntervention");
            txtEquipement.DataBindings.Add("Text", viewModel, "EquipementNom");

            txtInterventionId.Enabled = false;
            txtInterventionId.Text = viewModel.WorkOrderId.ToString();

            dgvPiecesReservees.AutoGenerateColumns = false;
            dgvPiecesReservees.DataSource = viewModel.PiecesReservees;
            dgvPiecesReservees.Columns.Clear();

            dgvPiecesReservees.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Nom",
                HeaderText = "Nom",
                ReadOnly = true
            });
            dgvPiecesReservees.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "QuantiteAReserver",
                HeaderText = "Quantité réservée",
                ReadOnly = true
            });

            dgvPiecesUtilisees.AutoGenerateColumns = false;
            dgvPiecesUtilisees.DataSource = viewModel.PiecesUtilisees;
            dgvPiecesUtilisees.Columns.Clear();
            dgvPiecesUtilisees.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Nom",
                HeaderText = "Nom",
                ReadOnly = true
            });

            dgvPiecesUtilisees.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantite",
                HeaderText = "Quantité utilisée",
                ReadOnly = false
            });

            //
            btnUtiliser.Click += (s, e) =>
            {
                if (dgvPiecesReservees.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Veuillez sélectionner une pièce réservée à utiliser.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var row = dgvPiecesReservees.SelectedRows[0];
                if (row.DataBoundItem is PieceReservationView selected)
                {
                    viewModel.AjouterPieceUtiliseeDepuisReservation(selected.PieceId);
                }
            };


            //
            btnModifier.Click += (s, e) =>
            {
                var confirm = MessageBox.Show("Confirmer la modification ?", "Modifier", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.Yes)
                {
                    if (viewModel.ModifierCommand.CanExecute(null))
                        viewModel.ModifierCommand.Execute(null);
                }

            };

            btnSupprimer.Click += (s, e) =>
            {
                var confirm = MessageBox.Show("Confirmer la suppression ?", "Suppression", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                    viewModel.SupprimerCommand.Execute(null);
            };

            //
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

            //
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

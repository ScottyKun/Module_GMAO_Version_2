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
    public partial class GstPieceAcceuil : Form
    {
        private readonly GstPiecesAccueilViewModel viewModel;
        public GstPieceAcceuil()
        {
            InitializeComponent();

            viewModel = new GstPiecesAccueilViewModel();

            dataGridView1.AutoGenerateColumns = false;
            dataGridView2.AutoGenerateColumns = false;
            SetupColumns();

            dataGridView1.DataSource = viewModel.ListePieces;
            dataGridView2.DataSource = viewModel.ListePiecesCritiques;

            var equipements = viewModel.GetEquipementsNoms();
            equipements.Insert(0, "-- Sélectionnez un équipement --"); // ligne par défaut
            cbRecherche.DataSource = equipements;

            cbRecherche.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbRecherche.AutoCompleteSource = AutoCompleteSource.ListItems;



            btnAjouter.Click += (s, e) =>
            {
                var form = new Views.GstPiecesAdd();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    viewModel.Rafraichir();
                    RefreshGrids();
                }
            };

            btnActualiser.Click += (s, e) =>
            {
                viewModel.Rafraichir();
                RefreshGrids();
            };

            btnCommander.Click += (s, e) =>
            {
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    var piece = (PieceDTO)dataGridView2.SelectedRows[0].DataBoundItem;
                    viewModel.Commander(piece);

                    MessageBox.Show("Demande d'achat créée avec succès.", "Commande", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    viewModel.Rafraichir();
                    RefreshGrids();
                }
            };

            dataGridView1.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    var piece = (PieceDTO)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                    var form = new Views.GstPiecesUpdate(piece.PieceId);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        viewModel.Rafraichir();
                        RefreshGrids();
                    }
                }
            };

        }

        private void SetupColumns()
        {
            dataGridView1.Columns.Clear();
            dataGridView2.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nom", DataPropertyName = "Nom" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Référence", DataPropertyName = "Reference" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Prix", DataPropertyName = "Prix" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Date Ajout", DataPropertyName = "DateAjout" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Quantité", DataPropertyName = "Quantite" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Stock", DataPropertyName = "StockNom" });

            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nom", DataPropertyName = "Nom" });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Référence", DataPropertyName = "Reference" });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Quantité", DataPropertyName = "Quantite" });
        }

        private void RefreshGrids()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = viewModel.ListePieces;

            dataGridView2.DataSource = null;
            dataGridView2.DataSource = viewModel.ListePiecesCritiques;
        }

        private void picBtnRechercher_Click(object sender, EventArgs e)
        {
            string nomRecherche = cbRecherche.SelectedItem?.ToString()?.Trim();

            if (!string.IsNullOrWhiteSpace(nomRecherche) && nomRecherche != "-- Sélectionnez un équipement --")
            {
                viewModel.FiltrerParNomEquipement(nomRecherche);
                RefreshGrids();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un équipement valide.", "Recherche", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

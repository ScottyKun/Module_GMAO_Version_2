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

            gridControlPieces.DataSource = viewModel.ListePieces;
            gridViewPieces.OptionsBehavior.Editable=false;

            gridControlDemandeAchat.DataSource = viewModel.ListePiecesCritiques;
            gridViewDemandeAchat.PopulateColumns();

            gridViewDemandeAchat.OptionsBehavior.Editable = false;
            gridViewPieces.OptionsBehavior.Editable = false;

           



            btnAjouter.Click += (s, e) =>
            {
                var form = new Views.GstPiecesAdd();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    viewModel.Rafraichir();
                    RefreshGrids();
                }
            };

         

            btnCommander.Click += (s, e) =>
            {
                if (gridViewDemandeAchat.SelectedRowsCount > 0) // Utilisation du GridControl et de la méthode de sélection de ligne
                {
                    var selectedRow = gridViewDemandeAchat.GetRow(gridViewDemandeAchat.FocusedRowHandle) as PieceDTO;
                    if (selectedRow != null)
                    {
                        viewModel.Commander(selectedRow);

                        MessageBox.Show("Demande d'achat créée avec succès.", "Commande", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Rafraîchir les données après la commande
                        viewModel.Rafraichir();
                        RefreshGrids();
                        // Actualiser le GridControl pour refléter les changements
                        gridViewDemandeAchat.RefreshData();
                    }
                }
            };


            gridViewPieces.DoubleClick += (s, e) =>
            {
                if (gridViewPieces.GetFocusedRow() is PieceDTO selected)
                {
                    var modifForm = new Views.GstPiecesUpdate(selected.PieceId);

                    var result = modifForm.ShowDialog(); // Important : Affiche la vue

                    if (result == DialogResult.OK)
                    {
                        if (modifForm.EstSupprimee)
                        {
                            viewModel.ListePieces.Remove(selected);
                            gridViewPieces.RefreshData();
                        }
                        else if (modifForm.PieceModifiee != null)
                        {
                            int index = viewModel.ListePieces.IndexOf(selected);
                            if (index >= 0)
                            {
                                viewModel.ListePieces[index] = modifForm.PieceModifiee;
                                gridViewPieces.RefreshData();
                            }
                        }
                    }
                }
            };


        }


        private void RefreshGrids()
        {
            gridControlPieces.DataSource = null;
            gridControlPieces.DataSource = viewModel.ListePieces;
            gridViewPieces.RefreshData();

            gridControlDemandeAchat.DataSource = null;
            gridControlDemandeAchat.DataSource = viewModel.ListePiecesCritiques;
            gridViewDemandeAchat.RefreshData();

        }

    }
}

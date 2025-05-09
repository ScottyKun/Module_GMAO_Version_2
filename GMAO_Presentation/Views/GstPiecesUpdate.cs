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
    public partial class GstPiecesUpdate : Form
    {
        private readonly GstPieceUpdateViewModel viewModel;
        public GstPiecesUpdate(int pieceId)
        {
            InitializeComponent();

            viewModel = new GstPieceUpdateViewModel(pieceId);


            txtNom.DataBindings.Add("Text", viewModel, "Nom", false, DataSourceUpdateMode.OnPropertyChanged);
            txtReference.DataBindings.Add("Text", viewModel, "Reference", false, DataSourceUpdateMode.OnPropertyChanged);
            numPrix.DataBindings.Add("Value", viewModel, "Prix", false, DataSourceUpdateMode.OnPropertyChanged);
            numQuantite.DataBindings.Add("Value", viewModel, "Quantite", false, DataSourceUpdateMode.OnPropertyChanged);
            dtAjout.DataBindings.Add("Value", viewModel, "DateAjout", false, DataSourceUpdateMode.OnPropertyChanged);


            cbStock.DataSource = viewModel.Stocks;
            cbStock.DisplayMember = "Nom";
            cbStock.ValueMember = "Id";

            cbStock.SelectedValue = viewModel.StockId;

            cbStock.SelectedIndexChanged += (s, e) =>
            {
                if (cbStock.SelectedItem is StockDTO selected)
                    viewModel.StockId = selected.Id;
            };


            clbEquipements.DataSource = viewModel.Equipements;
            clbEquipements.DisplayMember = "Nom";

            for (int i = 0; i < clbEquipements.Items.Count; i++)
            {
                var eq = (EquipementLightDTO)clbEquipements.Items[i];
                if (viewModel.EquipementIds.Contains(eq.Id))
                    clbEquipements.SetItemChecked(i, true);
            }

            clbEquipements.ItemCheck += (s, e) =>
            {
                var id = ((EquipementLightDTO)clbEquipements.Items[e.Index]).Id;
                if (e.NewValue == CheckState.Checked)
                {
                    if (!viewModel.EquipementIds.Contains(id))
                        viewModel.EquipementIds.Add(id);
                }
                else
                {
                    if (viewModel.EquipementIds.Contains(id))
                        viewModel.EquipementIds.Remove(id);
                }
                viewModel.ModifierCommand.RaiseCanExecuteChanged();
            };


            btnModifier.Enabled = viewModel.ModifierCommand.CanExecute(null);
            viewModel.ModifierCommand.CanExecuteChanged += (s, e) =>
            {
                btnModifier.Enabled = viewModel.ModifierCommand.CanExecute(null);
            };

            btnModifier.Click += (s, e) =>
            {
                viewModel.ModifierCommand.Execute(null);
                MessageBox.Show("Pièce modifiée avec succès !", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            };

            btnSupprimer.Click += (s, e) =>
            {
                var result = MessageBox.Show("Confirmer la suppression ?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    viewModel.SupprimerCommand.Execute(null);
                    MessageBox.Show("Pièce supprimée.", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
            };
        }
    }
}

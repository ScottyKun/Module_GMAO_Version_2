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
    public partial class GstPiecesAdd : Form
    {
        private readonly GstPieceAddViewModel viewModel;
        public GstPiecesAdd()
        {
            InitializeComponent();
            viewModel = new GstPieceAddViewModel();

            txtNom.DataBindings.Add("Text", viewModel, "Nom", false, DataSourceUpdateMode.OnPropertyChanged);
            txtReference.DataBindings.Add("Text", viewModel, "Reference", false, DataSourceUpdateMode.OnPropertyChanged);
            numPrix.DataBindings.Add("Value", viewModel, "Prix", false, DataSourceUpdateMode.OnPropertyChanged);
            numQuantite.DataBindings.Add("Value", viewModel, "Quantite", false, DataSourceUpdateMode.OnPropertyChanged);
            dtAjout.DataBindings.Add("Value", viewModel, "DateAjout", false, DataSourceUpdateMode.OnPropertyChanged);

            cbStock.DataSource = viewModel.Stocks;
            cbStock.DisplayMember = "Nom";
            cbStock.ValueMember = "Id";

            cbStock.SelectedIndexChanged += (s, e) =>
            {
                if (cbStock.SelectedItem is StockDTO selected)
                    viewModel.StockId = selected.Id;
            };


            clbEquipements.DataSource = viewModel.Equipements;
            clbEquipements.DisplayMember = "Nom";

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
            };


            btnAjouter.Click += (s, e) =>
            {
                if (viewModel.AjouterCommand.CanExecute(null))
                {
                    viewModel.AjouterCommand.Execute(null);
                    MessageBox.Show("Pièce de rechange ajoutée avec succès !", "Ajout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Veuillez remplir tous les champs requis.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
        }


    }
}

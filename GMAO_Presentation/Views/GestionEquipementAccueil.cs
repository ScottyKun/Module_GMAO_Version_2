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
    public partial class GestionEquipementAccueil : Form
    {
        private EquipementAccueilViewModel _viewModel;
        private BindingSource _binding;

        public GestionEquipementAccueil()
        {
            InitializeComponent();
            _viewModel = new EquipementAccueilViewModel();
            _binding = new BindingSource();

            dataGridView1.AutoGenerateColumns = false;
            SetupColumns();
            dataGridView1.DataSource = _binding;

            LoadData();
        }

        private void SetupColumns()
        {

            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nom", DataPropertyName = "Nom" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Catégorie", DataPropertyName = "Categorie" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Date d'achat", DataPropertyName = "DateAchat" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Responsable", DataPropertyName = "Responsable" });
            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "Statut", DataPropertyName = "Statut" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Fin garantie", DataPropertyName = "DateFinGarantie" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Équipe maintenance", DataPropertyName = "MaintenanceTeam" });
        }

        private void LoadData()
        {
            _viewModel.ChargerEquipements();
            _binding.DataSource = _viewModel.Equipements;
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            var ajoutForm = new Views.GestionEquipementAjout();
            if (ajoutForm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var equipement = (EquipementDTO)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                var modifForm = new Views.GestionEquipementUpdate(equipement.Id);
                if (modifForm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }
    }
}

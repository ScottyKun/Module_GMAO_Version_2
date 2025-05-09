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
    public partial class GestionDesEquipes : Form
    {
        private readonly GestionDesEquipes1ViewModel viewModel;

        public GestionDesEquipes()
        {
            InitializeComponent();
            viewModel = new GestionDesEquipes1ViewModel();

            dataGridView1.DataSource = viewModel.Equipes;


            // btnAjouter.Click += (s, e) => viewModel.AjouterEquipeCommand.Execute(null);
            // viewModel.OnNavigate += Navigate;
        }



        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Récupérez l'ID de l'équipe à partir de la cellule de la première colonne
                int equipeId = (int)dataGridView1.Rows[e.RowIndex].Cells["Id"].Value;

                var _viewModel = new GestionDesEquipes2ViewModel(equipeId);

                var form = new GestionDesEquipes2(equipeId) { ViewModel = _viewModel };

                if (form.ShowDialog() == DialogResult.OK)
                {
                    loadData();

                }
                // new Views.GestionDesEquipes2(equipeId).ShowDialog();
            }

        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            var form = new Views.GestionDesEquipes3();
            if (form.ShowDialog() == DialogResult.OK)
            {
                loadData();

            }

        }

        private void loadData()
        {
            viewModel.RafraichirEquipes();
            var equipes = viewModel.Equipes;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = new List<TeamInfo>(equipes);
        }
    }
}

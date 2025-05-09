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
    public partial class InterventionsAccueilForm : Form
    {
        private readonly InterventionAccVM viewModel;

        public InterventionsAccueilForm()
        {
            InitializeComponent();

            viewModel = new InterventionAccVM();

            //dgvInterventions.AutoGenerateColumns = false;
            dgvInterventions.DataSource = viewModel.Interventions;

            btnAjouter.Click += (s, e) =>
            {
                var form = new InterventionAddForm();
                if (form.ShowDialog() == DialogResult.OK)
                    viewModel.ChargerInterventions();
            };

            viewModel.OnDemandeAjout += () =>
            {
                var form = new InterventionAddForm();
                if (form.ShowDialog() == DialogResult.OK)
                    viewModel.ChargerInterventions();
            };

            dgvInterventions.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0 && dgvInterventions.Rows[e.RowIndex].DataBoundItem is InterventionDTO selected)
                {
                    var form = new InterventionsUpdateForm(selected.Id);
                    if (form.ShowDialog() == DialogResult.OK)
                        viewModel.ChargerInterventions();
                }
            };
        }
    }
}

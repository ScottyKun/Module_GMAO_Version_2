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

            gridControlInterventions.DataSource = viewModel.Interventions;

            gridViewInterventions.OptionsBehavior.Editable = false;

            btnAjouter.Click += (s, e) =>
            {
                var form = new InterventionAddForm();
                if (form.ShowDialog() == DialogResult.OK)
                    viewModel.ChargerInterventions();
                     gridViewInterventions.RefreshData();
            };

            viewModel.OnDemandeAjout += () =>
            {
                var form = new InterventionAddForm();
                if (form.ShowDialog() == DialogResult.OK)
                    viewModel.ChargerInterventions();
            };

            gridViewInterventions.DoubleClick += (s, e) =>
            {
                if (gridViewInterventions.GetFocusedRow() is InterventionDTO selected)
                {
                    var form = new InterventionsUpdateForm(selected.Id);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        var modifiee = form.InterventionModifiee;

                        if (modifiee != null)
                        {
                            // Cas modification
                            int index = viewModel.Interventions.IndexOf(selected);
                            if (index >= 0)
                            {
                                viewModel.Interventions[index] = modifiee;
                                gridViewInterventions.RefreshData();
                            }
                        }
                        else
                        {
                            // Cas suppression (modifiee == null)
                            viewModel.Interventions.Remove(selected);
                            gridViewInterventions.RefreshData();
                        }
                    }
                }
            };

        }
    }
}

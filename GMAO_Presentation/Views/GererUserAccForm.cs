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
    public partial class GererUserAccForm : Form
    {
        private readonly GstUserAccVM viewModel;
        public GererUserAccForm()
        {
            InitializeComponent();

            viewModel = new GstUserAccVM();

            dgvUsers.DataSource = viewModel.Users;

            btnModPWD.Enabled = false;

            txtRecherche.DataBindings.Add("Text", viewModel, "Recherche", false, DataSourceUpdateMode.OnPropertyChanged);

            imgREchercher.Click += (s, e) => viewModel.RechercherCommand.Execute(null);
            imgActualiser.Click += (s, e) => viewModel.ActualiserCommand.Execute(null);

            btnModPWD.Click += (s, e) =>
            {
                if (dgvUsers.CurrentRow?.DataBoundItem is UserDTO3 selectedUser)
                {
                    var form = new ModifPwdForm(selectedUser.IdUser);
                    if (form.ShowDialog() == DialogResult.OK)
                        viewModel.Actualiser();
                }
            };

            dgvUsers.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    var selected = dgvUsers.Rows[e.RowIndex].DataBoundItem as UserDTO3;
                    if (selected != null)
                    {
                        var form = new GererUserUpForm(selected);
                        if (form.ShowDialog() == DialogResult.OK)
                            viewModel.Actualiser();
                    }
                }
            };

            dgvUsers.SelectionChanged += (s, e) =>
            {
                btnModPWD.Enabled = dgvUsers.SelectedRows.Count > 0;
            };
        }
    }
}

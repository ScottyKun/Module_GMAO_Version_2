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
        private BindingList<UserDTO3> _bindedUsers;
        public GererUserAccForm()
        {
            InitializeComponent();

            viewModel = new GstUserAccVM();

            _bindedUsers = new BindingList<UserDTO3>(viewModel.Users);

            gridControlUsers.DataSource = _bindedUsers;

            btnModPWD.Enabled = false;

            btnModPWD.Click += (s, e) =>
            {
                if (gridViewUsers.GetFocusedRow() is UserDTO3 selectedUser)
                {
                    var form = new ModifPwdForm(selectedUser.IdUser);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        viewModel.Actualiser();
                        gridViewUsers.RefreshData();

                        gridViewUsers.BestFitColumns();
                    }
                }
            };

            gridViewUsers.RowCellClick += (s, e) =>
            {
                btnModPWD.Enabled = gridViewUsers.FocusedRowHandle >= 0;
            };

            gridViewUsers.DoubleClick += (s, e) =>
            {
                if (gridViewUsers.GetFocusedRow() is UserDTO3 selected)
                {
                    var form = new GererUserUpForm(selected);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        var updated = form.UtilisateurModifie;

                        if (updated == null)
                        {
                            viewModel.Actualiser(); // recharge depuis la BDD
                            _bindedUsers = new BindingList<UserDTO3>(viewModel.Users);
                            gridControlUsers.DataSource = _bindedUsers;
                            gridViewUsers.RefreshData();
                            gridViewUsers.BestFitColumns();
                        }
                        else
                        {
                            // ✅ C’était une modification
                            int index = _bindedUsers.IndexOf(selected);
                            if (index >= 0)
                            {
                                _bindedUsers[index] = updated;
                                gridViewUsers.RefreshData();
                                gridViewUsers.BestFitColumns();
                            }
                        }
                    }
                }
            };



        }
    }
}

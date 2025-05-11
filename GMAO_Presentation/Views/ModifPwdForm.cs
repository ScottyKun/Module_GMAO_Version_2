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
    public partial class ModifPwdForm : Form
    {
        private readonly GstUserModifVM viewModel;
        public ModifPwdForm(int idUser)
        {
            InitializeComponent();

            viewModel = new GstUserModifVM(idUser);

            txtPassword.DataBindings.Add("Text", viewModel, "NouveauMotDePasse", false, DataSourceUpdateMode.OnPropertyChanged);

            btnModifier.Click += (s, e) =>
            {
                if (viewModel.ModifierCommand.CanExecute(null))
                    viewModel.ModifierCommand.Execute(null);
            };

            viewModel.OnClose += () =>
            {
                MessageBox.Show("Mot de passe modifié avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            };

            viewModel.OnError += msg =>
            {
                MessageBox.Show(msg, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
        }

    }
}

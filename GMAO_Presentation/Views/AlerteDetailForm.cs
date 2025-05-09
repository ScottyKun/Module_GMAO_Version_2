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
    public partial class AlerteDetailForm : Form
    {
        private readonly AlerteDetVM viewModel;
        public AlerteDetailForm(int alerteId)
        {
            InitializeComponent();

            viewModel = new AlerteDetVM(alerteId);

            txtLibelle.Text = viewModel.Libelle;
            txtMessage.Text = viewModel.Message;
            txtPriorite.Text = viewModel.Priorite;
            txtDateCreation.Text = viewModel.DateCreation.ToString("g");

            txtLibelle.Enabled = txtMessage.Enabled = txtPriorite.Enabled = txtDateCreation.Enabled = false;

            chkTerminee.DataBindings.Add("Checked", viewModel, "Terminee");

            btnEnregistrer.Click += (s, e) => viewModel.EnregistrerCommand.Execute(null);
            btnSupprimer.Click += (s, e) => viewModel.SupprimerCommand.Execute(null);

            viewModel.OnClose += () =>
            {
                MessageBox.Show("Opération effectuée", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            };

            viewModel.OnError += (msg) =>
            {
                MessageBox.Show(msg, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

        }
    }
}

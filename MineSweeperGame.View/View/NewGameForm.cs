using MineSweeperGame.Model;
using MineSweeperGame.Persistence;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MineSweeperGame.View
{
    public partial class NewGameForm : Form
    {
        #region Private fields

        private MineSweeperModel _model;

        #endregion

        #region Constructors

        public NewGameForm(MineSweeperModel model)
        {
            InitializeComponent();
            _model = model;
        }

        #endregion

        #region Button event handlers

        private void ButtonOk_MouseClick(object? sender, EventArgs e)
        {
            foreach (RadioButton radioButton in _panelSizes.Controls.OfType<RadioButton>())
            {
                if (radioButton.Checked)
                {
                    int gridSize = Convert.ToInt32(radioButton.Text.Split('x')[0]);
                    _model.ChangeSize(gridSize);
                    break;
                }
            }
            DialogResult = DialogResult.OK;
            Close();
        }
        private void ButtonCancel_MouseClick(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


        #endregion
    }
}

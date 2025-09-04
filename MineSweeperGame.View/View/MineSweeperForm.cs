using Microsoft.VisualBasic.Logging;
using MineSweeperGame.Model;
using MineSweeperGame.Persistence;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MineSweeperGame.View
{
    public partial class MineSweeperForm : Form
    {
        #region Private fields

        private MineSweeperModel _model;
        private Button[,] _buttonGrid;

        #endregion

        #region Constructors

        public MineSweeperForm()
        {
            InitializeComponent();
            _model = new MineSweeperModel(new TextFilePersistence());
            _model.FieldChanged += new EventHandler<FieldChangedEventArgs>(Model_FieldChanged);
            _model.GameOver += new EventHandler(Model_GameOver);
            _model.GameWon += new EventHandler<GameWonEventArgs>(Model_GameWon);
            _model.LoadSizeChanged += new EventHandler(Model_LoadSizeChanged);
            _buttonGrid = null!;
        }

        #endregion

        #region Private methods

        private void GenerateTable()
        {
            _tableLayoutPanel.RowCount = _tableLayoutPanel.ColumnCount = _model.TableSize;

            _buttonGrid = new Button[_model.TableSize, _model.TableSize];
            for (Int32 i = 0; i < _model.TableSize; i++)
                for (Int32 j = 0; j < _model.TableSize; j++)
                {
                    _buttonGrid[i, j] = new GridButton(i, j);
                    _buttonGrid[i, j].Location = new Point(1 + 5 * i, 10 + 5 * j);
                    _buttonGrid[i, j].Size = new Size(10, 10);
                    _buttonGrid[i, j].Margin = new Padding(0);
                    _buttonGrid[i, j].Font = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold);
                    _buttonGrid[i, j].Dock = DockStyle.Fill;
                    _buttonGrid[i, j].BackColor = Color.Gray;
                    _buttonGrid[i, j].MouseClick += new MouseEventHandler(ButtonGrid_MouseClick);

                    _tableLayoutPanel.Controls.Add(_buttonGrid[i, j], j, i);
                }

            _tableLayoutPanel.RowStyles.Clear();
            _tableLayoutPanel.ColumnStyles.Clear();

            for (Int32 i = 0; i < _model.TableSize; i++)
            {
                _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 1 / (float)_model.TableSize));
            }
            for (Int32 j = 0; j < _model.TableSize; j++)
            {
                _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1 / (float)_model.TableSize));
            }
        }

        private void SetTable()
        {
            for (Int32 i = 0; i < _model.TableSize; i++)
                for (Int32 j = 0; j < _model.TableSize; j++)
                {
                    (Boolean covered, Int32 value) = _model[i, j];
                    if (covered)
                    {
                        _buttonGrid[i, j].BackColor = Color.Gray;
                        _buttonGrid[i, j].Text = String.Empty;
                    }
                    else
                    {
                        _buttonGrid[i, j].BackColor = Color.White;
                        _buttonGrid[i, j].Text = value == 0 ? String.Empty : (value == -1 ? "\U0001F4A3" : value.ToString());
                    }
                }
        }

        #endregion

        #region Model event handlers

        private void Model_GameWon(object? sender, GameWonEventArgs e)
        {
            switch (e.Player)
            {
                case Player.PlayerOne:
                    MessageBox.Show("Az elsõ játékos gyõzött!", "Játék vége!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
                case Player.PlayerTwo:
                    MessageBox.Show("A második játékos gyõzött!", "Játék vége!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
            }
            _model.NewGame();
            _currentPlayerLabel.Text = "Az 1. játékos következik.";
            SetTable();
        }

        private void Model_GameOver(object? sender, EventArgs e)
        {
            SetTable();
            MessageBox.Show("Döntetlen játék!", "Játék vége!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            _model.NewGame();
            _currentPlayerLabel.Text = "Az 1. játékos következik.";
            SetTable();
        }

        private void Model_FieldChanged(object? sender, FieldChangedEventArgs e)
        {
            _currentPlayerLabel.Text = (e.Player == Player.PlayerOne ? "A 2. játékos következik." : "Az 1. játékos következik.");
            SetTable();
        }

        private void Model_LoadSizeChanged(object? sender, EventArgs e)
        {
            _tableLayoutPanel.Controls.Clear();
            GenerateTable();
        }

        #endregion

        #region Form event handlers

        private void MineSweeperForm_Load(object? sender, EventArgs e)
        {
            _model.NewGame();

            GenerateTable();
            _currentPlayerLabel.Text = "Az 1. játékos következik.";
        }

        #endregion

        #region Grid event handlers

        private void ButtonGrid_MouseClick(object? sender, MouseEventArgs e)
        {
            if (sender is GridButton button)
            {
                Int32 x = button.GridX;
                Int32 y = button.GridY;

                try
                {
                    _model.StepGame(x, y);
                }
                catch { }
            }
        }

        #endregion

        #region Button event handlers
        private void NewGameButton_MouseClick(object? sender, EventArgs e)
        {
            NewGameForm f = new NewGameForm(_model);
            if (f.ShowDialog() == DialogResult.OK)
            {
                _tableLayoutPanel.Controls.Clear();
                GenerateTable();
                _currentPlayerLabel.Text = "Az 1. játékos következik.";
            }
        }
        private async void SaveGameButton_MouseClick(object? sender, EventArgs e)
        {
            if (_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await _model.SaveGameAsync(_saveFileDialog.FileName);
                }
                catch (DataException)
                {
                    MessageBox.Show("Hiba keletkezett a mentés során.", "Aknakeresõ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private async void LoadGameButton_MouseClick(object sender, EventArgs e)
        {
            if (_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await _model.LoadGameAsync(_openFileDialog.FileName);
                }
                catch (DataException)
                {
                    MessageBox.Show("Hiba keletkezett a betöltés során.", "Aknakeresõ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

    }
}
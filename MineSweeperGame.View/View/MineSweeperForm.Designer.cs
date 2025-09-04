using System.Windows.Forms;

namespace MineSweeperGame.View
{
    partial class MineSweeperForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._currentPlayerLabel = new System.Windows.Forms.Label();
            this._newGameButton = new System.Windows.Forms.Button();
            this._saveGameButton = new System.Windows.Forms.Button();
            this._loadGameButton = new System.Windows.Forms.Button();
            this._saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this._openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // _tableLayoutPanel
            // 
            this._tableLayoutPanel.ColumnCount = 1;
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tableLayoutPanel.Location = new System.Drawing.Point(253, 9);
            this._tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this._tableLayoutPanel.Name = "_tableLayoutPanel";
            this._tableLayoutPanel.RowCount = 1;
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tableLayoutPanel.Size = new System.Drawing.Size(720, 720);
            this._tableLayoutPanel.TabIndex = 0;
            // 
            // _currentPlayerLabel
            // 
            this._currentPlayerLabel.AutoSize = true;
            this._currentPlayerLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._currentPlayerLabel.Location = new System.Drawing.Point(12, 9);
            this._currentPlayerLabel.Name = "_currentPlayerLabel";
            this._currentPlayerLabel.Size = new System.Drawing.Size(76, 28);
            this._currentPlayerLabel.TabIndex = 1;
            this._currentPlayerLabel.Text = "Játékos";
            // 
            // _newGameButton
            // 
            this._newGameButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._newGameButton.Location = new System.Drawing.Point(8, 560);
            this._newGameButton.Name = "_newGameButton";
            this._newGameButton.Size = new System.Drawing.Size(235, 50);
            this._newGameButton.TabIndex = 2;
            this._newGameButton.Text = "Új játék (méretválasztás)";
            this._newGameButton.UseVisualStyleBackColor = true;
            this._newGameButton.Click += new System.EventHandler(this.NewGameButton_MouseClick);
            // 
            // _saveGameButton
            // 
            this._saveGameButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._saveGameButton.Location = new System.Drawing.Point(8, 620);
            this._saveGameButton.Name = "_saveGameButton";
            this._saveGameButton.Size = new System.Drawing.Size(235, 50);
            this._saveGameButton.TabIndex = 3;
            this._saveGameButton.Text = "Játék mentése";
            this._saveGameButton.UseVisualStyleBackColor = true;
            this._saveGameButton.Click += new System.EventHandler(this.SaveGameButton_MouseClick);
            // 
            // _loadGameButton
            // 
            this._loadGameButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._loadGameButton.Location = new System.Drawing.Point(8, 680);
            this._loadGameButton.Name = "_loadGameButton";
            this._loadGameButton.Size = new System.Drawing.Size(235, 50);
            this._loadGameButton.TabIndex = 0;
            this._loadGameButton.Text = "Játék betöltése";
            this._loadGameButton.UseVisualStyleBackColor = true;
            this._loadGameButton.Click += new System.EventHandler(this.LoadGameButton_MouseClick);
            // 
            // _saveFileDialog
            // 
            this._saveFileDialog.Filter = "Játékmentések|*.sav";
            this._saveFileDialog.Title = "Aknakereső - Játék mentése";
            // 
            // _openFileDialog
            // 
            this._openFileDialog.Filter = "Játékmentések|*.sav";
            this._openFileDialog.Title = "Aknakereső - Játék betöltése";
            // 
            // MineSweeperForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(982, 738);
            this.Controls.Add(this._loadGameButton);
            this.Controls.Add(this._saveGameButton);
            this.Controls.Add(this._newGameButton);
            this.Controls.Add(this._currentPlayerLabel);
            this.Controls.Add(this._tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MineSweeperForm";
            this.Text = "Aknakereső";
            this.Load += new System.EventHandler(this.MineSweeperForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TableLayoutPanel _tableLayoutPanel;
        private Label _currentPlayerLabel;
        private Button _newGameButton;
        private Button _saveGameButton;
        private Button _loadGameButton;
        private SaveFileDialog _saveFileDialog;
        private OpenFileDialog _openFileDialog;
    }
}
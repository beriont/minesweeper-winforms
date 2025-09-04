using System.Windows.Forms;

namespace MineSweeperGame.View
{
    partial class NewGameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this._buttonOk = new System.Windows.Forms.Button();
            this._buttonCancel = new System.Windows.Forms.Button();
            this._radioButton6 = new System.Windows.Forms.RadioButton();
            this._radioButton10 = new System.Windows.Forms.RadioButton();
            this._radioButton16 = new System.Windows.Forms.RadioButton();
            this._panelSizes = new System.Windows.Forms.Panel();
            this._panelSizes.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Válasszon pályaméretet:";
            // 
            // _buttonOk
            // 
            this._buttonOk.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._buttonOk.Location = new System.Drawing.Point(220, 104);
            this._buttonOk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._buttonOk.Name = "_buttonOk";
            this._buttonOk.Size = new System.Drawing.Size(100, 40);
            this._buttonOk.TabIndex = 1;
            this._buttonOk.Text = "OK";
            this._buttonOk.UseVisualStyleBackColor = true;
            this._buttonOk.Click += new System.EventHandler(this.ButtonOk_MouseClick);
            // 
            // _buttonCancel
            // 
            this._buttonCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._buttonCancel.Location = new System.Drawing.Point(12, 104);
            this._buttonCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(100, 40);
            this._buttonCancel.TabIndex = 2;
            this._buttonCancel.Text = "Mégsem";
            this._buttonCancel.UseVisualStyleBackColor = true;
            this._buttonCancel.Click += new System.EventHandler(this.ButtonCancel_MouseClick);
            // 
            // _radioButton6
            // 
            this._radioButton6.AutoSize = true;
            this._radioButton6.Checked = true;
            this._radioButton6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._radioButton6.Location = new System.Drawing.Point(0, 2);
            this._radioButton6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._radioButton6.Name = "_radioButton6";
            this._radioButton6.Size = new System.Drawing.Size(64, 32);
            this._radioButton6.TabIndex = 3;
            this._radioButton6.TabStop = true;
            this._radioButton6.Text = "6x6";
            this._radioButton6.UseVisualStyleBackColor = true;
            // 
            // _radioButton10
            // 
            this._radioButton10.AutoSize = true;
            this._radioButton10.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._radioButton10.Location = new System.Drawing.Point(101, 2);
            this._radioButton10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._radioButton10.Name = "_radioButton10";
            this._radioButton10.Size = new System.Drawing.Size(86, 32);
            this._radioButton10.TabIndex = 4;
            this._radioButton10.TabStop = true;
            this._radioButton10.Text = "10x10";
            this._radioButton10.UseVisualStyleBackColor = true;
            // 
            // _radioButton16
            // 
            this._radioButton16.AutoSize = true;
            this._radioButton16.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._radioButton16.Location = new System.Drawing.Point(222, 2);
            this._radioButton16.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._radioButton16.Name = "_radioButton16";
            this._radioButton16.Size = new System.Drawing.Size(86, 32);
            this._radioButton16.TabIndex = 5;
            this._radioButton16.TabStop = true;
            this._radioButton16.Text = "16x16";
            this._radioButton16.UseVisualStyleBackColor = true;
            // 
            // _panelSizes
            // 
            this._panelSizes.Controls.Add(this._radioButton6);
            this._panelSizes.Controls.Add(this._radioButton16);
            this._panelSizes.Controls.Add(this._radioButton10);
            this._panelSizes.Location = new System.Drawing.Point(12, 48);
            this._panelSizes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._panelSizes.Name = "_panelSizes";
            this._panelSizes.Size = new System.Drawing.Size(308, 39);
            this._panelSizes.TabIndex = 6;
            // 
            // NewGameForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(332, 153);
            this.ControlBox = false;
            this.Controls.Add(this._panelSizes);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._buttonOk);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "NewGameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Új játék";
            this._panelSizes.ResumeLayout(false);
            this._panelSizes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Button _buttonOk;
        private Button _buttonCancel;
        private RadioButton _radioButton6;
        private RadioButton _radioButton10;
        private RadioButton _radioButton16;
        private Panel _panelSizes;
    }
}
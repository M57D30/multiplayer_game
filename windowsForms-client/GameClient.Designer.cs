using System.Windows.Forms;

namespace windowsForms_client
{
    partial class GameClient
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
            this.TimeLabel = new System.Windows.Forms.Label();
            this.gameState = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Location = new System.Drawing.Point(375, 9);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(0, 13);
            this.TimeLabel.TabIndex = 0;
            // 
            // gameState
            // 
            this.gameState.AutoSize = true;
            this.gameState.Location = new System.Drawing.Point(12, 9);
            this.gameState.Name = "gameState";
            this.gameState.Size = new System.Drawing.Size(0, 13);
            this.gameState.TabIndex = 1;
            // 
            // GameClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gameState);
            this.Controls.Add(this.TimeLabel);
            this.Name = "GameClient";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.GameClient_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label TimeLabel;
        private Label gameState;
    }
}
﻿using System.Windows.Forms;

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
            TimeLabel = new Label();
            gameState = new Label();
            SuspendLayout();
            // 
            // TimeLabel
            // 
            TimeLabel.AutoSize = true;
            TimeLabel.Location = new Point(438, 10);
            TimeLabel.Margin = new Padding(4, 0, 4, 0);
            TimeLabel.Name = "TimeLabel";
            TimeLabel.Size = new Size(0, 15);
            TimeLabel.TabIndex = 0;
            // 
            // gameState
            // 
            gameState.AutoSize = true;
            gameState.Location = new Point(14, 10);
            gameState.Margin = new Padding(4, 0, 4, 0);
            gameState.Name = "gameState";
            gameState.Size = new Size(0, 15);
            gameState.TabIndex = 1;
            // 
            // GameClient
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 519);
            Controls.Add(gameState);
            Controls.Add(TimeLabel);
            Margin = new Padding(4, 3, 4, 3);
            Name = "GameClient";
            Text = "MainForm";
            Load += GameClient_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label TimeLabel;
        private Label gameState;
    }
}
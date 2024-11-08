using System;
using System.Windows.Forms;

namespace windowsForms_client
{
    partial class TankProperties
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
            this.TankType = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Upgrade = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TankType
            // 
            this.TankType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TankType.FormattingEnabled = true;
            this.TankType.Items.AddRange(new object[] {
            "Pistol",
            "TommyGun"});
            this.TankType.Location = new System.Drawing.Point(309, 204);
            this.TankType.Name = "TankType";
            this.TankType.Size = new System.Drawing.Size(165, 21);
            this.TankType.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(355, 250);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Confirm";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Upgrade
            // 
            this.Upgrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Upgrade.FormattingEnabled = true;
            this.Upgrade.Items.AddRange(new object[] {
            "-",
            "Movement speed boost",
            "Shooting speed boost",
            "Health boost",
            "Shield boost"});
            this.Upgrade.Location = new System.Drawing.Point(390, 138);
            this.Upgrade.Name = "Upgrade";
            this.Upgrade.Size = new System.Drawing.Size(146, 21);
            this.Upgrade.TabIndex = 3;
            this.Upgrade.SelectedIndexChanged += new System.EventHandler(this.Upgrade_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(144, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select one upgrade to receive every 10 seconds:";
            // 
            // TankProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Upgrade);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.TankType);
            this.Name = "TankProperties";
            this.Text = "TankProperties";
            this.Load += new System.EventHandler(this.TankProperties_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.ComboBox TankType;
        private Button button1;
        private ComboBox Upgrade;
        private Label label1;
    }
}
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
            TankType = new ComboBox();
            button1 = new Button();
            Upgrade = new ComboBox();
            label1 = new Label();
            ControlType = new ComboBox();  // Declare here to make it accessible within InitializeComponent
            SuspendLayout();

            // 
            // TankType
            // 
            TankType.DropDownStyle = ComboBoxStyle.DropDownList;
            TankType.FormattingEnabled = true;
            TankType.Items.AddRange(new object[] { "Pistol", "TommyGun", "Shotgun" });
            TankType.Location = new Point(360, 235);
            TankType.Margin = new Padding(4, 3, 4, 3);
            TankType.Name = "TankType";
            TankType.Size = new Size(192, 23);
            TankType.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(414, 288);
            button1.Margin = new Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new Size(88, 27);
            button1.TabIndex = 1;
            button1.Text = "Confirm";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Upgrade
            // 
            Upgrade.DropDownStyle = ComboBoxStyle.DropDownList;
            Upgrade.FormattingEnabled = true;
            Upgrade.Items.AddRange(new object[] { "-", "Movement speed boost", "Shooting speed boost", "Health boost", "Shield boost" });
            Upgrade.Location = new Point(455, 159);
            Upgrade.Margin = new Padding(4, 3, 4, 3);
            Upgrade.Name = "Upgrade";
            Upgrade.Size = new Size(170, 23);
            Upgrade.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(168, 163);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(257, 15);
            label1.TabIndex = 4;
            label1.Text = "Select one upgrade to receive every 10 seconds:";
            // 
            // ControlType
            // 
            ControlType.DropDownStyle = ComboBoxStyle.DropDownList;
            ControlType.FormattingEnabled = true;
            ControlType.Items.AddRange(new object[] { "Keyboard", "Mouse" });
            ControlType.Location = new Point(455, 200);  // Position it below the TankType combo box
            ControlType.Margin = new Padding(4, 3, 4, 3);
            ControlType.Name = "ControlType";
            ControlType.Size = new Size(170, 23);
            ControlType.TabIndex = 5;
            ControlType.SelectedIndex = 0; // Set default to Keyboard

            // Add ControlType to the form's controls
            Controls.Add(ControlType);  // This was missing in your original code

            // 
            // TankProperties
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 519);
            Controls.Add(label1);
            Controls.Add(Upgrade);
            Controls.Add(button1);
            Controls.Add(TankType);
            Margin = new Padding(4, 3, 4, 3);
            Name = "TankProperties";
            Text = "TankProperties";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox TankType;
        private Button button1;
        private ComboBox Upgrade;
        private Label label1;
        private ComboBox ControlType;  // Declare the ComboBox here
    }
}

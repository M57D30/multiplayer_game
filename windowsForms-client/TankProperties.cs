﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace windowsForms_client
{
    public partial class TankProperties : Form
    {
        public TankProperties()
        {
            InitializeComponent();
            this.TankType.SelectedIndex = 0;
            this.Upgrade.SelectedIndex = 0;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string tankType = TankType.SelectedItem.ToString();
            string selectedUpgrade = Upgrade.SelectedItem.ToString();
            MessageBox.Show($"Tank selected: {tankType}\nTank upgrade selected: {selectedUpgrade}", "Game Setup");
            GameClient gameClient = new GameClient(tankType, selectedUpgrade);
            gameClient.Show();
            this.Hide();
        }

        private void Upgrade_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TankProperties_Load(object sender, EventArgs e)
        {

        }
    }
}

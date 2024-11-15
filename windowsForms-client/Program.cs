using System;
using System.Windows.Forms;

namespace windowsForms_client
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TankProperties());
            Application.Run(new Form()); // Ensure this points to your MainForm
        }
    }
}


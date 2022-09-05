using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab7_14
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            PreSplash preSplash = new PreSplash();
            DateTime end = DateTime.Now + TimeSpan.FromSeconds(5);
            preSplash.Show();
            while (end > DateTime.Now)
            {
                Application.DoEvents();
            }
            preSplash.Close();
            preSplash.Dispose();

            Application.Run(new Form1());
        }
    }
}

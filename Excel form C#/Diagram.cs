using System.Windows.Forms;
using System.Collections.Generic;

namespace Lab7_14
{
    public partial class Diagram : Form
    {
        public List<int[]> cells = new List<int[]>();
        public Diagram(string values)
        {
            InitializeComponent();
            chart1.Series[0].Points.Clear();

            string[] elements = values.Split('\n');
            for (int i = elements.Length - 1; i >= 0; i--)
            {
                string[] vals = elements[i].Split(' ');
                int[] cr = { int.Parse(vals[0]), int.Parse(vals[1]) };
                cells.Add(cr);

                chart1.Series[0].Points.AddXY(vals[0] + ", " + vals[1], double.Parse(vals[2]));
            }
        }

        public void Draw(string values)
        {
            chart1.Series[0].Points.Clear();

            string[] elements = values.Split('\n');
            try
            {
                for (int i = 0; i < elements.Length - 1; i++)
                {
                    string[] vals = elements[i].Split(' ');
                    chart1.Series[0].Points.AddXY(vals[0] + ", " + vals[1], double.Parse(vals[2]));
                }
            } catch { this.Close(); }
        }
    }
}

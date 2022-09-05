using System.Windows.Forms;
using System.Collections.Generic;

namespace Lab7_14
{
    public partial class Form2 : Form
    {
        private List<Diagram> children = new List<Diagram>();
        public string path = "";
        public bool dataChanged = false;

        public Form2()
        {
            InitializeComponent();
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            dataGridView1.RowCount = 20;
        }

        public string GetString()
        {
            string str = "";
            try
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    string strRow = dataGridView1.Rows[i].Cells[0].Value != null ? dataGridView1.Rows[i].Cells[0].Value.ToString() : "";
                    for (int j = 1; j < dataGridView1.ColumnCount; j++)
                    {
                        strRow += dataGridView1.Rows[i].Cells[j].Value != null ? "`" + dataGridView1.Rows[i].Cells[j].Value.ToString() : "`";
                    }
                    str += strRow + "\n";
                }
            }
            catch { str = ""; }
            return str;
        }

        public void SetValues(string str)
        {
            string[] rows = str.Split('\n');
            dataGridView1.RowCount = rows.Length - 1;
            for (int i = 0; i < rows.Length - 1; i++)
            {
                string[] cells = rows[i].Split('`');
                for (int j = 0; j < cells.Length; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = cells[j];
                }
            }
        }

        private void копироватьToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 1)
            {
                if (dataGridView1.SelectedCells[0].Value != null)
                {
                    Clipboard.SetText(dataGridView1.SelectedCells[0].Value.ToString());
                }
            }
            else if (dataGridView1.SelectedCells.Count > 1)
            {
                string res = "";
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                    {
                        if (cell.Value != null && cell.RowIndex == row.Index)
                        {
                            if (res == "")
                            {
                                res = cell.Value.ToString();
                            }
                            else
                            {
                                res = cell.Value.ToString() + ", " + res;
                            }
                        }
                    }
                }
                Clipboard.SetText(res);
            }
        }

        private void вставитьToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 1 && Clipboard.GetText() != "")
            {
                dataGridView1.SelectedCells[0].Value = Clipboard.GetText();
            }
            else if (dataGridView1.SelectedCells.Count == Clipboard.GetText().Split(',').Length)
            {
                int i = dataGridView1.SelectedCells.Count - 1;
                string[] toCells = Clipboard.GetText().Split(',');
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                    {
                        if (cell.RowIndex == row.Index)
                        {
                            cell.Value = toCells[i--].Trim();
                        }
                    }
                }
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                {
                    cell.Value = null;
                }
            }
        }

        private void определитьToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 1)
            {
                DataGridViewCell cell = dataGridView1.SelectedCells[0];
                double res;

                if (cell.Value == null)
                {
                    MessageBox.Show("Ячейка пуста");
                }
                else if (double.TryParse(cell.Value.ToString(), out res))
                {
                    MessageBox.Show("Ячейка содержит число");
                }
                else
                {
                    MessageBox.Show("Ячейка содержит текст");
                }
            }
            else if (dataGridView1.SelectedCells.Count > 1)
            {
                bool empty = false, num = false, str = false;

                foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                {
                    double res;

                    if (cell.Value == null)
                    {
                        empty = true;
                    }
                    else if (double.TryParse(cell.Value.ToString(), out res))
                    {
                        num = true;
                    }
                    else
                    {
                        str = true;
                    }
                }

                string toShow = "Выделенные ячейки содержат:";
                if (empty) toShow += "\nПустые значения";
                if (num) toShow += "\nЧисленные значения";
                if (str) toShow += "\nТекстовые значения";

                MessageBox.Show(toShow);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            dataChanged = true;

            string values, res;
            foreach (Diagram child in children)
            {
                if (child.Text == "")
                {
                    continue;
                }
                try
                {
                    values = "";
                    res = "";
                    int count = 0;
                    foreach (int[] cell in child.cells)
                    {
                        values += cell[0] + " " + cell[1] + " " + dataGridView1.Rows[cell[1]].Cells[cell[0]].Value.ToString() + "\n";
                        count++;
                    }

                    string[] added = new string[count];
                    for (int i = 0; i < count; i++)
                    {
                        double min = double.PositiveInfinity;
                        string toAdd = "";
                        foreach (string s in values.Split('\n'))
                        {
                            if (s == "")
                            {
                                break;
                            }
                            string coords = s.Split(' ')[0] + " " + s.Split(' ')[1];
                            double val = double.Parse(s.Split(' ')[2]);
                            if (val <= min)
                            {
                                bool adding = true;
                                for (int j = 0; j < i; j++)
                                {
                                    if (added[j] == s)
                                    {
                                        adding = false;
                                    }
                                }
                                if (adding)
                                {
                                    min = val;
                                    toAdd = s;
                                }
                            }
                        }
                        added[i] = toAdd;
                        if (res == "")
                        {
                            res = toAdd;
                        }
                        else
                        {
                            res += toAdd + "\n";
                        }
                    }

                    child.Draw(res);
                } catch { }
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 parent = (Form1)MdiParent;
            e = parent.CloseForm(this, e);

            if (!e.Cancel)
            {
                foreach (Diagram child in children)
                {
                    try
                    {
                        child.Close();
                    } catch { }
                }
            }
        }

        private void диаграммаToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            int count = 0;
            string values = "", res = "";
            double tester;

            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                if (cell.Value.ToString() == "") { continue; }
                else if (!double.TryParse(cell.Value.ToString(), out tester))
                {
                    MessageBox.Show("Ошибка! Для построения диаграммы необходимы только численные значения!");
                    return;
                }
                else
                {
                    values += cell.ColumnIndex.ToString() + " " + cell.RowIndex.ToString() + " " + cell.Value.ToString() + "\n";
                    count++;
                }
            }

            if (count < 2)
            {
                MessageBox.Show("Для построения диаграммы необходимы по крайней мере 2 выбранные ячейки с числами!");
                return;
            }

            string[] added = new string[count];
            for (int i = 0; i < count; i++)
            {
                double min = double.PositiveInfinity;
                string toAdd = "";
                foreach (string s in values.Split('\n'))
                {
                    if (s == "")
                    {
                        break;
                    }
                    string coords = s.Split(' ')[0] + " " + s.Split(' ')[1];
                    double val = double.Parse(s.Split(' ')[2]);
                    if (val <= min)
                    {
                        bool adding = true;
                        for (int j = 0; j < i; j++)
                        {
                            if (added[j] == s)
                            {
                                adding = false;
                            }
                        }
                        if (adding)
                        {
                            min = val;
                            toAdd = s;
                        }
                    }
                }
                added[i] = toAdd;
                if (res == "")
                {
                    res = toAdd;
                }
                else
                {
                    res = toAdd + "\n" + res;
                }
            }

            Diagram diagram = new Diagram(res);
            children.Add(diagram);
            diagram.Show();
        }

        public void CountEven()
        {
            if (dataGridView1.SelectedCells.Count == 0)
            {
                MessageBox.Show("В выбранной таблице не выделена ни одна ячейка!");
            }
            else
            {
                int count = 0;
                foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                {
                    if (cell.Value == null)
                    {
                        continue;
                    }
                    double tester;
                    if (double.TryParse(cell.Value.ToString(), out tester))
                    {
                        if (tester % 2 == 0)
                        {
                            count++;
                        }
                    }
                }
                if (count == 0)
                {
                    MessageBox.Show("В выбранных ячейках нет чётных чисел!");
                }
                else
                {
                    MessageBox.Show($"Количество чётных чисел в выбранных ячейках: {count}");
                }
            }
        }

        public void FindDel()
        {
            if (dataGridView1.SelectedCells.Count == 0)
            {
                MessageBox.Show("В выбранной таблице не выделена ни одна ячейка!");
            }
            else
            {
                string res = "";
                foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                {
                    if (cell.Value == null)
                    {
                        continue;
                    }
                    if (cell.Value.ToString().Length != cell.Value.ToString().Replace("Del", "").Length)
                    {
                        if (res == "")
                        {
                            res = cell.Value.ToString();
                        }
                        else
                        {
                            res += ", " + cell.Value.ToString();
                        }
                    }
                }
                if (res == "")
                {
                    MessageBox.Show("В выбранных ячейках нет слов с 'Del'!");
                }
                else
                {
                    dataGridView1.Rows[0].Cells[0].Value = res;
                }
            }
        }
    }
}

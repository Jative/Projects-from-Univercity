using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace Lab7_14
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void создатьТаблицуToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.MdiParent = this;
            form2.dataChanged = false;
            form2.Show();
        }

        private void открытьТаблицуToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Table files(*.table)|*.table";
            ofd.Title = "Выбор таблицы";

            if (ofd.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            string fileName = ofd.FileName;
            string fileText = System.IO.File.ReadAllText(fileName);

            Form2 form2 = new Form2();
            form2.MdiParent = this;
            form2.SetValues(fileText);
            form2.path = ofd.FileName;
            form2.dataChanged = false;
            form2.Show();
        }

        private void закрытьТаблицуToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form2 activeChild = (Form2)this.ActiveMdiChild;

            if (activeChild != null)
            {
                activeChild.Close();
            }
            else
            {
                MessageBox.Show("Ни одна таблица не выбрана!");
            }
        }

        private void сохранитьТаблицуToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            string fileName;

            Form2 activeChild = (Form2)this.ActiveMdiChild;
            if (activeChild == null)
            {
                MessageBox.Show("Ни одна таблица не выбрана!");
                return;
            }

            if (activeChild.path == "")
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Table files(*.table)|*.table";
                sfd.Title = "Выбор таблицы";

                if (sfd.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }

                fileName = sfd.FileName;
            }
            else
            {
                fileName = activeChild.path;
            }
            string toSave = activeChild.GetString();

            System.IO.File.WriteAllText(fileName, toSave);
            activeChild.dataChanged = false;
        }

        private void сохранитьТаблицуВExcelToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form2 activeForm = (Form2)this.ActiveMdiChild;
            if (activeForm == null)
            {
                MessageBox.Show("Ни одна таблица не выбрана!");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Файл Excel(*.xlsx)|*.xlsx";
            sfd.Title = "Выбор таблицы excel";

            if (sfd.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            string fileName = sfd.FileName;

            if (activeForm != null)
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Add();
                Excel.Worksheet worksheet = workbook.ActiveSheet;

                string[] rows = activeForm.GetString().Split('\n');
                for (int i = 0; i < rows.Length; i++)
                {
                    string[] cells = rows[i].Split('`');
                    for (int j = 0; j < cells.Length; j++)
                    {
                        worksheet.Rows[i + 1].Columns[j + 1] = cells[j];
                    }
                }
                excelApp.AlertBeforeOverwriting = false;
                workbook.SaveAs(fileName);
                excelApp.Quit();
            }
            else
            {
                MessageBox.Show("Ни одна таблица не выбрана!");
            }
        }

        private void сохранитьТаблицуВWordToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form2 activeForm = (Form2)this.ActiveMdiChild;
            if (activeForm == null)
            {
                MessageBox.Show("Ни одна таблица не выбрана!");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Файл Word(*.docx)|*.docx";
            sfd.Title = "Выбор файла word";

            if (sfd.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            string fileName = sfd.FileName;

            if (activeForm != null)
            {
                string[] rows = activeForm.GetString().Split('\n');

                Word.Document doc = new Word.Document();
                Word.Range r = doc.Range();

                Word.Table t = doc.Tables.Add(r, rows.Length, rows[0].Split('`').Length);
                t.Borders.Enable = 1;
                for (int i = 0; i < rows.Length; i++)
                {
                    string[] cells = rows[i].Split('`');
                    for (int j = 0; j < cells.Length; j++)
                    {
                        t.Rows[i + 1].Cells[j + 1].Range.Text = cells[j];
                    }
                }
                doc.SaveAs(fileName);
                doc.Close();
            }
            else
            {
                MessageBox.Show("Ни одна таблица не выбрана!");
            }
        }

        private void окнаКаскадомToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void окнаВертикальноToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void окнаГоризонтальноToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        public FormClosingEventArgs CloseForm(Form2 form, FormClosingEventArgs e)
        {
            Form2 activeChild = form;
            if (activeChild.dataChanged)
            {
                DialogResult dr = MessageBox.Show("Были внесены изменения\nЖелаете их сохранить?", "Внимание!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    string fileName, toSave;

                    if (activeChild.path != "")
                    {
                        fileName = activeChild.path;
                        toSave = activeChild.GetString();
                    }
                    else
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "Table files(*.table)|*.table";
                        sfd.Title = "Выбор таблицы";
                        if (sfd.ShowDialog() == DialogResult.Cancel)
                        {
                            e.Cancel = true;
                            return e;
                        }
                        fileName = sfd.FileName;
                    }
                    toSave = activeChild.GetString();
                    System.IO.File.WriteAllText(fileName, toSave);
                    e.Cancel = false;
                }
                else if (dr == DialogResult.No)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = false;
            }

            return e;
        }

        private void количествоЧётныхToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form2 activeForm = (Form2)this.ActiveMdiChild;
            if (activeForm != null)
            {
                activeForm.CountEven();
            }
            else
            {
                MessageBox.Show("Ни одна таблица не выбрана!");
            }
        }

        private void delВЯчейкахToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form2 activeForm = (Form2)this.ActiveMdiChild;
            if (activeForm != null)
            {
                activeForm.FindDel();
            }
            else
            {
                MessageBox.Show("Ни одна таблица не выбрана!");
            }
        }
    }
}

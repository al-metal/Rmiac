using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rmiac
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void перейтиКРедактированиюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Contacts form = new Contacts();
            form.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.noteBookTableAdapter.Fill(this.dBDataSet.NoteBook);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            this.Hide();
            NoteBookEdits forms = new NoteBookEdits();
            forms.Show();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            string id = dataGridView1.Rows[row].Cells[0].Value.ToString();
            this.Hide();
            NoteBookUpdate forms = new NoteBookUpdate(id);
            forms.Show();
        }
    }
}
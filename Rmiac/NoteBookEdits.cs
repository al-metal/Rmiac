using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rmiac
{
    public partial class NoteBookEdits : Form
    {
        public NoteBookEdits()
        {
            InitializeComponent();
        }

        private void NoteBookEdits_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string date = calendar.SelectionEnd.Date.ToString();
            date = date.Substring(date.IndexOf(' '));
            if(tbTheme.Text != "" && rtbText.Text != "")
            {
                string connStr = "Data Source=localhost;Initial Catalog=DB;Integrated Security=True";

                SqlConnection conn = new SqlConnection(connStr);
                try
                {
                    conn.Open();
                }
                catch (SqlException se)
                {
                    MessageBox.Show("Ошибка подключения:{0}", se.Message);
                    return;
                }

                SqlCommand cmd = new SqlCommand(@"INSERT INTO
                                                NoteBook ([theme], [text], [date], [phone])
                                             VALUES
                                            (@theme, @text, @date, @phone)", conn);

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@theme";
                param.Value = "Новая провер";
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@text";
                param.Value = "Иванов И";
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@date";
                param.Value = "06.03.2017";
                param.SqlDbType = SqlDbType.Date;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@phone";
                param.Value = "123456";
                param.SqlDbType = SqlDbType.NChar;
                cmd.Parameters.Add(param);

                Console.WriteLine("Вставляем запись");
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Ошибка, при выполнении запроса на добавление записи");
                    return;
                }

                cmd = new SqlCommand("Select * From Students", conn);

                conn.Close();
                conn.Dispose();
            }
        }
    }
}

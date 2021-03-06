﻿using System;
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
    public partial class NoteBookUpdate : Form
    {
        string id;
        public NoteBookUpdate(string id)
        {
            this.id = id;
            InitializeComponent();
        }

        private void btnUpdateDB_Click(object sender, EventArgs e)
        {
            string date = calendar.SelectionEnd.Date.ToString();
            date = date.Remove(date.IndexOf(' '));
            if (tbTheme.Text != "" && rtbText.Text != "")
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

                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[NoteBook] SET [theme] = '" + tbTheme.Text + "',[text] = '" + rtbText.Text + "',[date] = '" +date + "',[phone] = '" +tbPhone.Text + "' WHERE id='" + id +"'", conn);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Ошибка, при выполнении запроса на добавление записи");
                    return;
                }

                conn.Close();
                conn.Dispose();
            }
            else
            {
                MessageBox.Show("Введены не все данные");
            }
        }

        private void NoteBookUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
        }

        private void NoteBookUpdate_Load(object sender, EventArgs e)
        {
            string connStr = "Data Source=localhost;Initial Catalog=DB;Integrated Security=True";
            string zapros = @"SELECT * FROM NoteBook WHERE id='" + id + "'";

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

            SqlCommand cmd = new SqlCommand(zapros, conn);
            SqlDataReader data = cmd.ExecuteReader();

            List<string> avtoNames = new List<string>();
            while (data.Read())
            {
                avtoNames.Add(data.GetValue(0).ToString());
                avtoNames.Add(data.GetValue(1).ToString());
                avtoNames.Add(data.GetValue(2).ToString());
                avtoNames.Add(data.GetValue(3).ToString());
                avtoNames.Add(data.GetValue(4).ToString());
            }

            tbTheme.Text = avtoNames[1].ToString();
            rtbText.Text = avtoNames[2].ToString();
            tbPhone.Text = avtoNames[4].ToString().Trim();

            conn.Close();
            conn.Dispose();
        }
    }
}

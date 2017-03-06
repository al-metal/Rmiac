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
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet.NoteBook". При необходимости она может быть перемещена или удалена.
            this.noteBookTableAdapter.Fill(this.dBDataSet.NoteBook);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "noteBookDataSet1.NoteBook". При необходимости она может быть перемещена или удалена.
            // TODO: данная строка кода позволяет загрузить данные в таблицу "noteBookDataSet.NoteBook". При необходимости она может быть перемещена или удалена.
            //this.noteBookTableAdapter.Fill(this.noteBookDataSet.NoteBook);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "testDBDataSet.TestDb". При необходимости она может быть перемещена или удалена.


        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {



            /*Объявляем строковую переменную и записываем в нее
             строку подключения 
             Data Source - имя сервера, по стандарту (local)\SQLEXPRESS
             Initial Catalog - имя БД 
             Integrated Security=-параметры безопасности
             Мое подключение имеет вид
             */
            string connStr = "Data Source=localhost;Initial Catalog=DB;Integrated Security=True";

            /*Объявляем строковую переменную и записываем в нее
             строку подключения 
             Data Source - имя сервера, по стандарту (local)\SQLEXPRESS
             Initial Catalog - имя БД 
             Integrated Security=-параметры безопасности
             Мое подключение имеет вид
             */

            /*Здесь указал имя БД(хотя для создания БД его указывать не нужно)
              для того, чтобы проверить, может данная БД уже создана
            Создаем экземпляр класса  SqlConnection по имени conn
            и передаем конструктору этого класса, строку подключения
             */
            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                //пробуем подключится
                conn.Open();
            }
            catch (SqlException se)
            {
                Console.WriteLine("Ошибка подключения:{0}", se.Message);
                return;
            }

            Console.WriteLine("Соедение успешно произведено");
            /*Создаем экземпляр класса  SqlCommand по имени cmdCreateTable
             и передаем конструктору этого класса, запрос на 
             добавление строки в  таблицу Students
             и объект типа SqlConnection
            */

            SqlCommand cmd = new SqlCommand(@"INSERT INTO
    NoteBook ([theme], [text], [date], [phone])
VALUES
    (@theme, @text, @date, @phone)", conn);

            /*Работаем с параметрами(SqlParameter), эта техника позволяет уменьшить
            кол-во ошибок и достичь большего быстродействия
             но требует и больших усилий в написании кода*/
            //объявляем объект класса SqlParameter
            SqlParameter param = new SqlParameter();
            //задаем имя параметра
            param.ParameterName = "@theme";
            //задаем значение параметра
            param.Value ="Новая провер";
            //задаем тип параметра
            param.SqlDbType = SqlDbType.NVarChar;
            //передаем параметр объекту класса SqlCommand
            cmd.Parameters.Add(param);
            //переопределяем объект класса SqlParameter
            param = new SqlParameter();
            //задаем имя параметра
            param.ParameterName = "@text";
            //задаем значение параметра
            param.Value = "Иванов И";
            //задаем тип параметра
            param.SqlDbType = SqlDbType.NVarChar;
            //передаем параметр объекту класса SqlCommand
            cmd.Parameters.Add(param);
            //переопределяем объект класса SqlParameter
            param = new SqlParameter();
            //задаем имя параметра
            param.ParameterName = "@date";
            //задаем значение параметра
            param.Value = "06.03.2017";
            //задаем тип параметра
            param.SqlDbType = SqlDbType.Date;
            //передаем параметр объекту класса SqlCommand
            cmd.Parameters.Add(param);
            param = new SqlParameter();
            //задаем имя параметра
            param.ParameterName = "@phone";
            //задаем значение параметра
            param.Value = "123456";
            //задаем тип параметра
            param.SqlDbType = SqlDbType.NChar;
            //передаем параметр объекту класса SqlCommand
            cmd.Parameters.Add(param);
            Console.WriteLine("Вставляем запись");
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Ошибка, при выполнении запроса на добавление записи");
                return;
            }
            //Выводим значение на экран
            cmd = new SqlCommand("Select * From Students", conn);
            /*Метод ExecuteReader() класса SqlCommand возврашает
             объект типа SqlDataReader, с помошью которого мы можем
             прочитать все строки, возврашенные в результате выполнения запроса
             CommandBehavior.CloseConnection - закрываем соединение после запроса
             */
           /* using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                //цикл по всем столбцам полученной в результате запроса таблицы
                for (int i = 0; i < dr.FieldCount; i++)
                    /*метод GetName() класса SqlDataReader позволяет получить имя столбца
                     по номеру, который передается в качестве параметра, данному методу
                     и озночает номер столбца в таблице(начинается с 0)
                     */
                    //Console.Write("{0}\t", dr.GetName(i).ToString().Trim());
                /*читаем данные из таблицы
                 чтение происходит только в прямом направлении
                 все прочитаные строки отбрасываюся */
                /*Console.WriteLine();
                while (dr.Read())
                {
                    /*метод GetValue() класса SqlDataReader позволяет получить значение столбца
                                            по номеру, который передается в качестве параметра, данному методу
                                            и озночает номер столбца в таблице(начинается с 0)
                                            */
                    /*Console.WriteLine("{0}\t{1}\t{2}", dr.GetValue(0).ToString().Trim(),
                     dr.GetValue(1).ToString().Trim(),
                     dr.GetValue(2).ToString().Trim());
                }
            }*/
            //закрвываем соединение
            conn.Close();
            conn.Dispose();
            Console.WriteLine();

        }
    }
}











    /*SqlConnection conn1 = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\NoteBook.mdf;Integrated Security=True");
    SqlCommand cmd1 = new SqlCommand();
    cmd1.Connection = conn1;
    conn1.Open();
    cmd1.CommandType = CommandType.Text;
    cmd1.CommandText = @"INSERT INTO
NoteBook([theme], [text], [date], [phone])
VALUES
(N'test', N'123123213 21 32 13 21 3 2 321 3 21', N'05.03.2017', N'78945612')";
    cmd1.ExecuteNonQuery();
    conn1.Close();
    MessageBox.Show("Запрос выполнен");*/


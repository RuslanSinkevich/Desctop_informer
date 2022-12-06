using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Data;

namespace l2mega_informer
{
    class ClassConnect
    {
        string text = @"Data Source=" + Application.StartupPath + @"\dase\full3.db3;Pooling=true;FailIfMissing=false";
        //string text = @"Data Source=.\SQLEXPRESS;AttachDbFilename=" + Application.StartupPath + @"\dase\dase_full.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";        

        public override string ToString()
        {
            return text;
        }

        public static void GridView_list_mobs(DataTable t2, string CommandText) // метод конета получает строку запроса и отправляет таблицу с данными
        {
            string Connect = @"Data Source=" + Application.StartupPath + @"\dase\full3.db3;Pooling=true;FailIfMissing=false";
            //string Connect = @"Data Source=.\SQLEXPRESS;AttachDbFilename=" + Application.StartupPath + @"\dase\dase_full.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";
            SQLiteConnection myConnection = new SQLiteConnection(Connect);
            try
            {
                myConnection.Open();
            }
            catch (Exception l)
            {
                MessageBox.Show("Ошибка!\r\n (" + l.Message + ")", "Подключение к базе данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SQLiteDataAdapter da2 = new SQLiteDataAdapter(CommandText, myConnection);
            try
            {
                da2.Fill(t2);
            }
            catch (Exception l)
            {
                MessageBox.Show("Ошибка!\r\n (" + l.Message + ")", "Подключение к базе данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                myConnection.Close();
            }
        }
    }
}

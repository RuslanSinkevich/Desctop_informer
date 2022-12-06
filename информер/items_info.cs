using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace l2mega_informer
{
    public partial class items_info : Form
    {
        public items_info()
        {
            InitializeComponent();
            string CommandText = "SELECT * FROM item_desc WHERE id = " + ClassMob.Items_Id + "";
            DataTable t2 = new DataTable();
            ClassConnect.GridView_list_mobs(t2, CommandText); // Создан отдельный файл с класом коннект (обозреватель решений)
            DataTableReader dtr = t2.CreateDataReader();
            dtr.Read();
            if (dtr["add_name_ru"].ToString() != "")
            {
                textBox_name_ru.Text = "[ " + dtr["add_name_ru"].ToString() + " ] " + dtr["name_ru"].ToString();
            }
            else
            {
                textBox_name_ru.Text = dtr["name_ru"].ToString();
            }
            if (ClassMob.ping == 0)
            {
                textBox_txt_ru.Text = dtr["desc_ru"].ToString();
            }
            else
            {
                textBox_txt_ru.Text = ClassMob.ping_text;
            }

            if (dtr["add_name_en"].ToString() != "")
            {
                textBox_name_en.Text = "[ " + dtr["add_name_en"].ToString() + " ] " + dtr["name_en"].ToString();
            }
            else
            {
                textBox_name_en.Text = dtr["name_en"].ToString();
            }
            if (ClassMob.ping == 0)
            {
                textBox_txt_en.Text = dtr["desc_en"].ToString();
            }
            else
            {
                textBox_txt_en.Text = ClassMob.ping_text;
            }
            textBox_name_ru.SelectionStart = textBox_name_ru.TextLength;
            dtr.Close();
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

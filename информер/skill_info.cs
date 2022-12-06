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
    public partial class skill_info : Form
    {
        public skill_info()
        {
            InitializeComponent();
            string CommandText = "SELECT sk_ru.skill_id, sk_en.name, sk_ru.name_ru, sk_ru.desc_ru, sk_ru.enchant_name_ru, sk_ru.enchant_desc_ru FROM skill_ru as sk_ru"
                    + " LEFT JOIN skill_en as sk_en ON sk_en.skill_id = sk_ru.skill_id"
                    + " WHERE sk_ru.skill_id = " + ClassMob.Info_SkillId + " AND sk_ru.level = " + ClassMob.Info_SkillLevl + "";
            DataTable t2 = new DataTable();
            ClassConnect.GridView_list_mobs(t2, CommandText); // Создан отдельный файл с классом коннект (обозреватель решений)
            DataTableReader dtr = t2.CreateDataReader();
            dtr.Read();
            textBox_name_ru.Text = dtr["name_ru"].ToString();
            textBox_name_en.Text = dtr["name"].ToString();

            if (dtr["enchant_name_ru"].ToString() != "")
            {
                textBox_txt_desc.Text = @"ID: " + dtr["skill_id"].ToString() + "\r\n[Тип имя]: " + dtr["add_name_ru"].ToString() + "\r\n[Тип описание]: " + dtr["name_ru"].ToString() + " \r\n\r\n[Описание скила]: " + dtr["desc_ru"].ToString();
            }
            else
            {
                if (dtr["desc_ru"].ToString() != "")
                {
                    textBox_txt_desc.Text = "ID: " + dtr["skill_id"].ToString() + "\r\n[Описание скила]: " + dtr["desc_ru"].ToString();
                }
                else
                {
                    textBox_txt_desc.Text = "ID: " + dtr["skill_id"].ToString();
                }
            }
        }

        private void button_cloz_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

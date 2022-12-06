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
    public partial class skill_enchant : Form
    {
        public skill_enchant()
        {
            InitializeComponent();

            pictureBox_skill.Image = Image.FromFile(Application.StartupPath + @"\images\skills\" + ClassMob.skil_id + ".png");
            label_name_skill.Text = ClassMob.skil_name;

            string CommandText = "SELECT *  FROM skill_ru as sk_ru "
                   //+ " LEFT JOIN skill_en as sk_tr ON sk_ru.skill_id = sk_tr.skill_id"
                   + " WHERE sk_ru.skill_id = " + ClassMob.skil_id + " AND sk_ru.level > 100 ";
            DataTable t_skills = new DataTable();
            ClassConnect.GridView_list_mobs(t_skills, CommandText); // Создан отдельный файл с класом коннект (обозреватель решений)
            DataTableReader dtr_skill = t_skills.CreateDataReader();
            int z = t_skills.Rows.Count;

            dataGridView_skils_enchant.Columns.Clear();
            dataGridView_skils_enchant.Refresh();
            dataGridView_skils_enchant.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_skils_enchant.Columns[0].HeaderText = "Тип";
            dataGridView_skils_enchant.Columns[0].Width = 200;
            dataGridView_skils_enchant.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_skils_enchant.Columns[1].HeaderText = "Описание";
            dataGridView_skils_enchant.Columns[1].ReadOnly = true;
            dataGridView_skils_enchant.Columns[1].Width = 280;
            dataGridView_skils_enchant.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_skils_enchant.Columns[2].HeaderText = "Описание";
            dataGridView_skils_enchant.Columns[2].ReadOnly = true;
            dataGridView_skils_enchant.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView_skils_enchant.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView_skils_enchant.Columns[2].DefaultCellStyle.Padding = new Padding(2);
            dataGridView_skils_enchant.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView_skils_enchant.RowCount = z;

            for (int i = 0; i < z; i++) // Циклзаполнения вклади клан шмот
            {
                dtr_skill.Read();
                dataGridView_skils_enchant.Rows[i].Cells[0].Value = dtr_skill["enchant_name_ru"].ToString();
                dataGridView_skils_enchant.Rows[i].Cells[1].Value = dtr_skill["enchant_desc_ru"].ToString();
                dataGridView_skils_enchant.Rows[i].Cells[2].Value = dtr_skill["desc_ru"].ToString(); // описание
            }
            dtr_skill.Close();
        }
    }
}

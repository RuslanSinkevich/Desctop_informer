using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using System.IO;
using System.Net;

namespace l2mega_informer
{
    public partial class mob_info : Form
    {
        public mob_info()
        {
            InitializeComponent();
            string CommandText = "";
            if (ClassMob.Seting_lang == 0)
            {
                CommandText = "SELECT npc.level, npc.name, min.amount_min, min.amount_max FROM minions as min"
                                   + " LEFT JOIN npc as npc ON npc.id = min.minion_id"
                                   + " WHERE min.boss_id = " + ClassMob.Info_Id + "";
            }
            else
            {
                CommandText = "SELECT npc.level, npc.name_ru, min.amount_min, min.amount_max FROM minions as min"
                                   + " LEFT JOIN npc as npc ON npc.id = min.minion_id"
                                   + " WHERE min.boss_id = " + ClassMob.Info_Id + "";
            }
            DataTable t1 = new DataTable();
            ClassConnect.GridView_list_mobs(t1, CommandText); // Создан отдельный файл с класом коннект (обозреватель решений)
            dataGridView_list_minions.DataSource = t1;

            if (dataGridView_list_minions.Rows.Count == 0) { dataGridView_list_minions.Visible = false; } // Если у npc Нет миньёнов скрываем грид

            for (int i = 3; i < 7; i++) { dataGridView_list_minions.Columns[i].Visible = false; } // Убирает лишнии столбцы таблицы
            for (int i = 0; i < 3; i++) { dataGridView_list_minions.Columns[i].Visible = true; }// отображает столбцы столбцы таблицы
            
            string CommandText2 = "";
            if (ClassMob.Seting_lang == 1)
            {
                CommandText2 = "SELECT npc.skillid, sk_ru.name_ru, npc.level FROM npcskills as npc"
                                + " LEFT JOIN skill_ru as sk_ru ON sk_ru.skill_id = npc.skillid AND sk_ru.level = npc.level"
                                + " WHERE npc.npcid = " + ClassMob.Info_Id + " ORDER BY npc.skillid DESC";
            }
            else
            {
                CommandText2 = "SELECT npc.skillid, sk_en.name, npc.level FROM npcskills as npc"
                                + " LEFT JOIN skill_en as sk_en ON sk_en.skill_id = npc.skillid"
                                + " WHERE npc.npcid = " + ClassMob.Info_Id + " ORDER BY npc.skillid DESC";
            }
            DataTable t3 = new DataTable();
            ClassConnect.GridView_list_mobs(t3, CommandText2); // Создан отдельный файл с класом коннект (обозреватель решений)
            dataGridView_info_skilss.DataSource = t3;

            for (int i = 2; i < 5; i++) { dataGridView_info_skilss.Columns[i].Visible = false; } // Убирает лишнии столбцы таблицы
            for (int i = 0; i < 2; i++) { dataGridView_info_skilss.Columns[i].Visible = true; }// отображает столбцы столбцы таблицы

        }   

        private void button1_Click(object sender, EventArgs e) // закрытие окна
        {
            this.Close();
        }

        private void click(object sender, DataGridViewCellEventArgs e) // событие на двойной клик по гриду скилов
        {
            if (dataGridView_info_skilss.CurrentRow == null) { return; }
            ClassMob.Info_SkillId = Convert.ToInt32(dataGridView_info_skilss.CurrentRow.Cells[2].Value);
            ClassMob.Info_SkillLevl = Convert.ToInt32(dataGridView_info_skilss.CurrentRow.Cells[4].Value);
            skill_info form2 = new skill_info();
            form2.Text = "Подробное описание Скила " + dataGridView_info_skilss.CurrentRow.Cells[3].Value.ToString(); form2.ShowDialog(); 
        }

        int global_img = 0;
        private void load_form(object sender, EventArgs e) // Событие формы запуск после открытия для коректности заполнения данными , картинками
        {
            // миньёны

            for (int i = 0; i < dataGridView_list_minions.Rows.Count; i++)
            {
                dataGridView_list_minions.Rows[i].Cells[0].Value = dataGridView_list_minions.Rows[i].Cells[3].Value; // Лэвл моба
                dataGridView_list_minions.Rows[i].Cells[1].Value = dataGridView_list_minions.Rows[i].Cells[4].Value; // Имя моба
                dataGridView_list_minions.Rows[i].Cells[2].Value =
                "" + dataGridView_list_minions.Rows[i].Cells[5].Value + " - " + dataGridView_list_minions.Rows[i].Cells[6].Value + ""; // количество миньёнов минимум - максимум
            }
            dataGridView_list_minions.ClearSelection();

            // скилы

            double xp_reit = 1;
            dataGridView_info_skilss.ClearSelection();
            for (int i = 0; i < dataGridView_info_skilss.Rows.Count; i++)
            {
                int skillId = Convert.ToInt32(dataGridView_info_skilss.Rows[i].Cells[2].Value); // Скил id
                int level = Convert.ToInt32(dataGridView_info_skilss.Rows[i].Cells[4].Value); // Скил лвл
                try
                {
                    if (skillId == 4415) { dataGridView_info_skilss.Rows[i].Visible = false; continue; }
                    else if (skillId == 4408 & level <= 7) { dataGridView_info_skilss.Rows[i].Visible = false; }
                    else if (skillId == 4414) { dataGridView_info_skilss.Rows[i].Visible = false; continue; }
                    else if (skillId == 4413) { dataGridView_info_skilss.Rows[i].Visible = false; continue; }
                    else if (skillId == 4412) { dataGridView_info_skilss.Rows[i].Visible = false; continue; }
                    else if (skillId == 4411) { dataGridView_info_skilss.Rows[i].Visible = false; continue; }
                    else if (skillId == 4410) { dataGridView_info_skilss.Rows[i].Visible = false; continue; }
                    else if (skillId == 4409) { dataGridView_info_skilss.Rows[i].Visible = false; continue; }
                } catch { }
                if (skillId == 4408) 
                {
                    dataGridView_info_skilss.Rows[i].Visible = false; 
                    if (level == 2) { xp_reit = 1.1; continue; }
                    else if (level == 3) { xp_reit = 1.21; continue; }
                    else if (level == 4) { xp_reit = 1.33; continue; }
                    else if (level == 5) { xp_reit = 1.46; continue; }
                    else if (level == 6) { xp_reit = 1.61; continue; }
                    else if (level == 7) { xp_reit = 1.77; continue; }
                    else if (level == 8) { pictureBox2.Image = Properties.Resources.hp_8; xp_reit = 0.25; continue; }
                    else if (level == 9) { pictureBox2.Image = Properties.Resources.hp_9; xp_reit = 0.5; continue; }
                    else if (level == 10) { pictureBox2.Image = Properties.Resources.hp_10; xp_reit = 2; continue; }
                    else if (level == 11) { pictureBox2.Image = Properties.Resources.hp_11; xp_reit = 3; continue; }
                    else if (level == 12) { pictureBox2.Image = Properties.Resources.hp_12; xp_reit = 4; continue; }
                    else if (level == 13) { pictureBox2.Image = Properties.Resources.hp_13; xp_reit = 5; continue; }
                    else if (level == 14) { pictureBox2.Image = Properties.Resources.hp_14; xp_reit = 6; continue; }
                    else if (level == 15) { pictureBox2.Image = Properties.Resources.hp_15; xp_reit = 7; continue; }
                    else if (level == 16) { pictureBox2.Image = Properties.Resources.hp_16; xp_reit = 8; continue; }
                    else if (level == 17) { pictureBox2.Image = Properties.Resources.hp_17; xp_reit = 9; continue; }
                    else if (level == 18) { pictureBox2.Image = Properties.Resources.hp_18; xp_reit = 10; continue; }
                    else if (level == 19) { pictureBox2.Image = Properties.Resources.hp_19; xp_reit = 11; continue; }
                    else if (level == 20) { pictureBox2.Image = Properties.Resources.hp_20; xp_reit = 12; continue; }
                }                           
                if (skillId == 4416)
                {
                    try // Если нет картинки вставяет пустую стандартную
                    { dataGridView_info_skilss.Rows[i].Cells[0].Value = Image.FromFile(Application.StartupPath + @"\images\skills_mob\" + skillId + "_" + level + ".png"); }
                    catch { }
                }
                 dataGridView_info_skilss.Rows[i].Cells[1].Value = Convert.ToString(dataGridView_info_skilss.Rows[i].Cells[3].Value);
            }

            string CommandText = "SELECT * FROM npc WHERE id = " + ClassMob.Info_Id + "";
            DataTable t2 = new DataTable();
            ClassConnect.GridView_list_mobs(t2, CommandText); // Создан отдельный файл с класом коннект (обозреватель решений)
            DataTableReader dtr = t2.CreateDataReader();
            dtr.Read();

            label_info_hp.Text = Convert.ToInt32(Convert.ToInt32(dtr["hp"]) * xp_reit).ToString("### ### ### ### ###");
            label_info_mp.Text = Convert.ToInt32(Convert.ToInt32(dtr["mp"]) * xp_reit).ToString("### ### ### ### ###");

            //ClassMob.Seting_xp = 1; // поже тут поставить из настроек рейды
            //ClassMob.Seting_sp = 1; // поже тут поставить из настроек рейды
            label_info_xp.Text = Convert.ToInt64(Convert.ToInt32(dtr["exp"]) * ClassMob.Seting_xp).ToString("### ### ### ### ###");
            label_info_sp.Text = Convert.ToInt64(Convert.ToInt32(dtr["sp"]) * ClassMob.Seting_sp).ToString("### ### ### ### ###");

            label__info_pAtk.Text = Convert.ToInt32(dtr["patk"]).ToString("### ### ### ### ###");
            label_info_mAtk.Text = Convert.ToInt32(dtr["matk"]).ToString("### ### ### ### ###");
            label_info_pDef.Text = Convert.ToInt32(dtr["pdef"]).ToString("### ### ### ### ###");
            label_info_mDef.Text = Convert.ToInt32(dtr["mdef"]).ToString("### ### ### ### ###");
            label_info_atkSpd.Text = Convert.ToInt32(dtr["atkspd"]).ToString("### ### ### ### ###");
            label_info_runSpd.Text = Convert.ToInt32(dtr["runspd"]).ToString("### ### ### ### ###");
            label_info_id.Text = dtr["id"].ToString();
            label_info_str.Text = dtr["str"].ToString();
            label_info_dex.Text = dtr["dex"].ToString();
            label_info_con.Text = dtr["con"].ToString();
            label_info_int.Text = dtr["int"].ToString();
            label_info_wit.Text = dtr["wit"].ToString();
            label_info_men.Text = dtr["men"].ToString();
            label_info_sex.Text = dtr["sex"].ToString();
            label_info_title.Text = dtr["title"].ToString();
            label_info_class.Text = dtr["class"].ToString();
            string agro = "";
            if (Convert.ToInt32(dtr["aggro"]) > 0) { agro = "Агр"; }
            label_info_agr.Text = agro;

            dtr.Close();

            Thread t1 = new Thread(start_proc);
            t1.Start();      
        }

        private void start_proc()
        {
            try
            {
                global_img = 0;
                WebClient client = new WebClient();
                Stream stream = client.OpenRead("http://l2.kz/img/base/monster/lv" + ClassMob.Info_level + "_" + ClassMob.Info_Id + ".jpg");
                Bitmap image1 = new Bitmap(stream);
                pictureBox1.Image = image1;
            }
            catch { global_img = 1; }
        }

        private void Paint_info(object sender, PaintEventArgs e)
        {
            try
            {
                if (global_img == 1) { return;  }
                Bitmap bmp = new Bitmap(Properties.Resources.lineage21);
                //bmp.MakeTransparent(Color.White); // Делает прозрачными белые места картинки
                e.Graphics.DrawImage((Image)bmp, new Point(9, 131));
            }
            catch { }
        }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net;
using System.Globalization;
using System.Reflection;
using System.Drawing.Imaging;

namespace l2mega_informer
{
    public partial class FormHome : Form
    {
        WebClient MyRequest;
        WebClient MyRequest2;
        public FormHome()
        {
            InitializeComponent();
            this.Text = "l2mega-informer (High_Five) rev 1.5";
            comboBox_ClassAddOn();
            button_seting_loading();
            ClassMob.Seting_xp = Convert.ToInt32(numericUpDown_seting_xp.Value);
            ClassMob.Seting_sp = Convert.ToInt32(numericUpDown_seting_sp.Value);
            ClassMob.Seting_z = Convert.ToInt32(numericUpDown_seting_z.Value);
            ClassMob.info_webBrowser = "";
            ClassMob.ping_text = "У вас отсутствует интернет соединение, либо вы заблокировали фаерволом доступ к сайту (l2mega.net) проверяется каждые 30секунд.";
            ClassMob.ping = 0;
        }

        private void comboBox_ClassAddOn()
        {
            comboBox_lvl_rec.Items.Add(new ClassAddOn("WHERE rec.craftLevel > 0", "Все"));
            comboBox_lvl_rec.Items.Add(new ClassAddOn("WHERE rec.craftLevel = 1 AND rec.type = 'dwarven'", "lv.1  (у Dwarf c lv.5)"));
            comboBox_lvl_rec.Items.Add(new ClassAddOn("WHERE rec.craftLevel = 2 AND rec.type = 'dwarven'", "lv.2  (у Artisan c lv.20)"));
            comboBox_lvl_rec.Items.Add(new ClassAddOn("WHERE rec.craftLevel = 3 AND rec.type = 'dwarven'", "lv.3  (у Artisan c lv.28)"));
            comboBox_lvl_rec.Items.Add(new ClassAddOn("WHERE rec.craftLevel = 4 AND rec.type = 'dwarven'", "lv.4  (у Artisan c lv.36)"));
            comboBox_lvl_rec.Items.Add(new ClassAddOn("WHERE rec.craftLevel = 5 AND rec.type = 'dwarven'", "lv.5  (у Warsmith c lv.43)"));
            comboBox_lvl_rec.Items.Add(new ClassAddOn("WHERE rec.craftLevel = 6 AND rec.type = 'dwarven'", "lv.6  (у Warsmith c lv.49)"));
            comboBox_lvl_rec.Items.Add(new ClassAddOn("WHERE rec.craftLevel = 7 AND rec.type = 'dwarven'", "lv.7  (у Warsmith c lv.55)"));
            comboBox_lvl_rec.Items.Add(new ClassAddOn("WHERE rec.craftLevel = 8 AND rec.type = 'dwarven'", "lv.8  (у Warsmith c lv.62)"));
            comboBox_lvl_rec.Items.Add(new ClassAddOn("WHERE rec.craftLevel = 9 AND rec.type = 'dwarven'", "lv.9  (у Warsmith c lv.70)"));
            comboBox_lvl_rec.Items.Add(new ClassAddOn("WHERE rec.craftLevel = 10 AND rec.type = 'dwarven'", "lv.10 (у Maestro c lv.82)"));
            comboBox_lvl_rec.Items.Add(new ClassAddOn("WHERE rec.craftLevel > 0 AND rec.type = 'common'", "lv.1  - lv.10 (все классы)"));
            comboBox_lvl_rec.Text = "Все";
            comboBox_tip_rec.Items.Add(new ClassAddOn("ORDER BY item.price DESC", "Все"));
            comboBox_tip_rec.Items.Add(new ClassAddOn("AND items.type = 2 ORDER BY items.price DESC", "Оружие"));
            comboBox_tip_rec.Items.Add(new ClassAddOn("AND items.type = 3 ORDER BY items.price DESC", "Броня"));
            comboBox_tip_rec.Items.Add(new ClassAddOn("AND items.type = 1 ORDER BY items.price DESC", "Материалы"));
            comboBox_tip_rec.Text = "Все";

            if (radioButton_lang_ru.Checked)
            {
                comboBox_shop_seeds.Items.Add(new ClassAddOn("1", "Глудио"));
                comboBox_shop_seeds.Items.Add(new ClassAddOn("2", "Дион"));
                comboBox_shop_seeds.Items.Add(new ClassAddOn("3", "Гиран"));
                comboBox_shop_seeds.Items.Add(new ClassAddOn("4", "Орэн"));
                comboBox_shop_seeds.Items.Add(new ClassAddOn("5", "Адэн"));
                comboBox_shop_seeds.Items.Add(new ClassAddOn("6", "Иннадрил"));
                comboBox_shop_seeds.Items.Add(new ClassAddOn("7", "Годард"));
                comboBox_shop_seeds.Items.Add(new ClassAddOn("8", "Руна"));
                comboBox_shop_seeds.Items.Add(new ClassAddOn("9", "Шутгард"));
                comboBox_shop_seeds.Text = "Глудио";

                comboBox_shop_yang.Items.Add(new ClassAddOn(" WHERE mul.id = 32326001", "Элементаль зелья"));
                comboBox_shop_yang.Items.Add(new ClassAddOn(" WHERE mul.id = 32326002", "Элементаль камни"));
                comboBox_shop_yang.Text = "Элементаль зелья";

                comboBox_shop_reputation.Items.Add(new ClassAddOn(" WHERE mul.id = 364790003", "Расходные материалы"));
                comboBox_shop_reputation.Items.Add(new ClassAddOn(" WHERE mul.id = 364790002", "Удалит PvP улучшение"));
                comboBox_shop_reputation.Items.Add(new ClassAddOn(" WHERE mul.id = 364790001", "PvP оружие, доспехи улучшение"));
                comboBox_shop_reputation.Text = "Расходные материалы";

                comboBox_shop_kanore.Items.Add(new ClassAddOn(" WHERE mul.id = 326100002", "S80 & S84 Плаши"));
                comboBox_shop_kanore.Items.Add(new ClassAddOn(" WHERE mul.id = 326100001", "Ремни"));
                comboBox_shop_kanore.Text = "S80 & S84 Плаши";

                comboBox_shop_ishuma.Items.Add(new ClassAddOn(" WHERE mul.id = 326150001", "Крафт двойные мечи"));
                comboBox_shop_ishuma.Items.Add(new ClassAddOn(" WHERE mul.id = 326150002", "Вставка Династии, Икара, Венеры  оружие SA"));
                comboBox_shop_ishuma.Items.Add(new ClassAddOn(" WHERE mul.id = 326150004", "Удаление Династии, Икара, Венеры  оружие SA"));
                comboBox_shop_ishuma.Items.Add(new ClassAddOn(" WHERE mul.id = 326150005", "Распечатать Династии, Венеры доспехи"));
                comboBox_shop_ishuma.Items.Add(new ClassAddOn(" WHERE mul.id = 326150006", "Распечатать Династии, Венеры Аксессуары"));
                comboBox_shop_ishuma.Items.Add(new ClassAddOn(" WHERE mul.id = 326150007", "Обновление Венеры доспехи и Венеры благородные доспехи"));
                comboBox_shop_ishuma.Items.Add(new ClassAddOn(" WHERE mul.id = 326150008", "Изменение класса Династии высокий грейд брони"));
                comboBox_shop_ishuma.Text = "Крафт двойные мечи";

                comboBox_shop_shadai.Items.Add(new ClassAddOn(" WHERE mul.id = 323470001", "Проклятые Кристаллы души"));
                comboBox_shop_shadai.Items.Add(new ClassAddOn(" WHERE mul.id = 323470002", "Украшение и забытые свитки"));
                comboBox_shop_shadai.Items.Add(new ClassAddOn(" WHERE mul.id = 323470003", "Повышение брони"));
                comboBox_shop_shadai.Items.Add(new ClassAddOn(" WHERE mul.id = 323470004", "Удалить повышение"));
                comboBox_shop_shadai.Text = "Проклятые Кристаллы души";

                comboBox_shop_castl.Items.Add(new ClassAddOn(" WHERE mul.id = 90002001", "Магия сопутствующих товаров"));
                comboBox_shop_castl.Items.Add(new ClassAddOn(" WHERE mul.id = 90002002", "браслеты"));
                comboBox_shop_castl.Items.Add(new ClassAddOn(" WHERE mul.id = 90002003", "футболки"));
                comboBox_shop_castl.Items.Add(new ClassAddOn(" WHERE mul.id = 90002004", "заколдованные футболки"));
                comboBox_shop_castl.Text = "Магия сопутствующих товаров";

                comboBox_shop_hiro.Items.Add(new ClassAddOn(" WHERE mul.id = 102", "Оборудование награды"));
                comboBox_shop_hiro.Items.Add(new ClassAddOn(" WHERE mul.id = 103", "Разные Награды"));
                comboBox_shop_hiro.Text = "Оборудование награды";
            }

            else
            {
                comboBox_shop_seeds.Items.Add(new ClassAddOn("1", "Gludio"));
                comboBox_shop_seeds.Items.Add(new ClassAddOn("2", "Dion"));
                comboBox_shop_seeds.Items.Add(new ClassAddOn("3", "Giran"));
                comboBox_shop_seeds.Items.Add(new ClassAddOn("4", "Oren"));
                comboBox_shop_seeds.Items.Add(new ClassAddOn("5", "Aden"));
                comboBox_shop_seeds.Items.Add(new ClassAddOn("6", "Innadril"));
                comboBox_shop_seeds.Items.Add(new ClassAddOn("7", "Goddard"));
                comboBox_shop_seeds.Items.Add(new ClassAddOn("8", "Rune"));
                comboBox_shop_seeds.Items.Add(new ClassAddOn("9", "Schuttgart"));
                comboBox_shop_seeds.Text = "Gludio";

                comboBox_shop_yang.Items.Add(new ClassAddOn(" WHERE mul.id = 32326001", "Elemental potions"));
                comboBox_shop_yang.Items.Add(new ClassAddOn(" WHERE mul.id = 32326002", "Elemental stones"));
                comboBox_shop_yang.Text = "Elemental potions";

                comboBox_shop_reputation.Items.Add(new ClassAddOn(" WHERE mul.id = 364790003", "Consumables"));
                comboBox_shop_reputation.Items.Add(new ClassAddOn(" WHERE mul.id = 364790002", "Removal of PvP enhancement"));
                comboBox_shop_reputation.Items.Add(new ClassAddOn(" WHERE mul.id = 364790001", "PvP weapons, armors enhancement"));
                comboBox_shop_reputation.Text = "Consumables";

                comboBox_shop_kanore.Items.Add(new ClassAddOn(" WHERE mul.id = 326100002", "S80 & S84 Clock"));
                comboBox_shop_kanore.Items.Add(new ClassAddOn(" WHERE mul.id = 326100001", "Belts"));
                comboBox_shop_kanore.Text = "S80 & S84 Clock";

                comboBox_shop_ishuma.Items.Add(new ClassAddOn(" WHERE mul.id = 326150001", "Craft a Dualsword"));
                comboBox_shop_ishuma.Items.Add(new ClassAddOn(" WHERE mul.id = 326150002", "Apply Dynasty, Icarus, Vesper Weapon SA"));
                comboBox_shop_ishuma.Items.Add(new ClassAddOn(" WHERE mul.id = 326150004", "Remove Dynasty, Icarus, Vesper Weapon SA"));
                comboBox_shop_ishuma.Items.Add(new ClassAddOn(" WHERE mul.id = 326150005", "Unseal Dynasty, Vesper Armor"));
                comboBox_shop_ishuma.Items.Add(new ClassAddOn(" WHERE mul.id = 326150006", "Unseal Dynasty, Vesper Accesories"));
                comboBox_shop_ishuma.Items.Add(new ClassAddOn(" WHERE mul.id = 326150007", "Upgrade Vesper Armor to Vesper Noble Armor"));
                comboBox_shop_ishuma.Items.Add(new ClassAddOn(" WHERE mul.id = 326150008", "Change the class of Dynasty High Grade Armor"));
                comboBox_shop_ishuma.Text = "Craft a Dualsword";

                comboBox_shop_shadai.Items.Add(new ClassAddOn(" WHERE mul.id = 323470001", "Cursed Soul Crystals"));
                comboBox_shop_shadai.Items.Add(new ClassAddOn(" WHERE mul.id = 323470002", "Shoulder Ornament and Forgotten Scrolls"));
                comboBox_shop_shadai.Items.Add(new ClassAddOn(" WHERE mul.id = 323470003", "Enhance Armor"));
                comboBox_shop_shadai.Items.Add(new ClassAddOn(" WHERE mul.id = 323470004", "Remove Enhancement"));
                comboBox_shop_shadai.Text = "Cursed Soul Crystals";

                comboBox_shop_castl.Items.Add(new ClassAddOn(" WHERE mul.id = 90002001", "magic related products"));
                comboBox_shop_castl.Items.Add(new ClassAddOn(" WHERE mul.id = 90002002", "bracelets"));
                comboBox_shop_castl.Items.Add(new ClassAddOn(" WHERE mul.id = 90002003", "t-shirts"));
                comboBox_shop_castl.Items.Add(new ClassAddOn(" WHERE mul.id = 90002004", "enchanted t-shirts"));
                comboBox_shop_castl.Text = "magic related products";

                comboBox_shop_hiro.Items.Add(new ClassAddOn(" WHERE mul.id = 102", "Equipment Rewards"));
                comboBox_shop_hiro.Items.Add(new ClassAddOn(" WHERE mul.id = 103", "Misc. Rewards"));
                comboBox_shop_hiro.Text = "Equipment Rewards";
            }
        }

        #region вкладка Мобы

        //------------------------------ вкладка мобы - groupBox (Тип мобов) -----------------------//
        //------------------------------ вкладка мобы - groupBox (Подбор по уязвимостям) -----------------------//

        private void OnAll3(object sender, MouseEventArgs e) { radioButton_mob_all3.Image = Properties.Resources.all3; }
        private void OnFire(object sender, MouseEventArgs e) { radioButton_mob_fire.Image = Properties.Resources.fire; }
        private void OnWind(object sender, MouseEventArgs e) { radioButton_mob_wind.Image = Properties.Resources.wind; }
        private void OnWater(object sender, MouseEventArgs e) { radioButton__mob_water.Image = Properties.Resources.water; }
        private void OnEarh(object sender, MouseEventArgs e) { radioButton_mob_earch.Image = Properties.Resources.earch; }
        private void OnHoly(object sender, MouseEventArgs e) { radioButton_mob_holy.Image = Properties.Resources.holy; }
        private void OnDark(object sender, MouseEventArgs e) { radioButton_mob_darc.Image = Properties.Resources.dark; }
        private void OnBow(object sender, MouseEventArgs e) { radioButton_mob_bow.Image = Properties.Resources.bov; }
        private void OnBlunt(object sender, MouseEventArgs e) { radioButton_mob_blunt.Image = Properties.Resources.blunt; }
        private void OnDager(object sender, MouseEventArgs e) { radioButton_mob_dager.Image = Properties.Resources.dager; }
        private void OnSword(object sender, MouseEventArgs e) { radioButton_mob_sword.Image = Properties.Resources.sword; }
        private void OnDual(object sender, MouseEventArgs e) { radioButton_mob_dual.Image = Properties.Resources.dual; }
        private void OnKnuckle(object sender, MouseEventArgs e) { radioButton_mob_knuckle.Image = Properties.Resources.knuckle; }
        private void OnSpear(object sender, MouseEventArgs e) { radioButton_mob_spear.Image = Properties.Resources.spear; }

        private void OnMob(object sender, MouseEventArgs e) { radioButton_mob_all.Image = Properties.Resources.mob;  }
        private void OnMobs(object sender, MouseEventArgs e) { radioButton_mob_mobs.Image = Properties.Resources.mobs; }
        private void OnNpc(object sender, MouseEventArgs e) { radioButton_mob_npc.Image = Properties.Resources.npc; }
        private void OnRb(object sender, MouseEventArgs e) { radioButton_mob_rb.Image = Properties.Resources.rb; }
        private void OnGrandRb(object sender, MouseEventArgs e) { radioButton_mob_grann_rb.Image = Properties.Resources.rb_grand; }

        private void OffGroupBoxSelectionMob(object sender, EventArgs e)
        {
            if (radioButton_mob_all3.Focused == false & radioButton_mob_all3.Checked == false) radioButton_mob_all3.Image = Properties.Resources.all350;
            if (radioButton_mob_fire.Focused == false & radioButton_mob_fire.Checked == false) radioButton_mob_fire.Image = Properties.Resources.fire50;
            if (radioButton_mob_wind.Focused == false & radioButton_mob_wind.Checked == false) radioButton_mob_wind.Image = Properties.Resources.wind50;
            if (radioButton__mob_water.Focused == false & radioButton__mob_water.Checked == false) radioButton__mob_water.Image = Properties.Resources.water50;
            if (radioButton_mob_earch.Focused == false & radioButton_mob_earch.Checked == false) radioButton_mob_earch.Image = Properties.Resources.earch50;
            if (radioButton_mob_holy.Focused == false & radioButton_mob_holy.Checked == false) radioButton_mob_holy.Image = Properties.Resources.holy50;
            if (radioButton_mob_darc.Focused == false & radioButton_mob_darc.Checked == false) radioButton_mob_darc.Image = Properties.Resources.dark50;
            if (radioButton_mob_bow.Focused == false & radioButton_mob_bow.Checked == false) radioButton_mob_bow.Image = Properties.Resources.bov50;
            if (radioButton_mob_blunt.Focused == false & radioButton_mob_blunt.Checked == false) radioButton_mob_blunt.Image = Properties.Resources.blunt50;
            if (radioButton_mob_dager.Focused == false & radioButton_mob_dager.Checked == false) radioButton_mob_dager.Image = Properties.Resources.dager50;
            if (radioButton_mob_sword.Focused == false & radioButton_mob_sword.Checked == false) radioButton_mob_sword.Image = Properties.Resources.sword50;
            if (radioButton_mob_dual.Focused == false & radioButton_mob_dual.Checked == false) radioButton_mob_dual.Image = Properties.Resources.dual50;
            if (radioButton_mob_knuckle.Focused == false & radioButton_mob_knuckle.Checked == false) radioButton_mob_knuckle.Image = Properties.Resources.knuckle50;
            if (radioButton_mob_spear.Focused == false & radioButton_mob_spear.Checked == false) radioButton_mob_spear.Image = Properties.Resources.spear50;

            if (radioButton_mob_all.Focused == false & radioButton_mob_all.Checked == false) radioButton_mob_all.Image = Properties.Resources.mob50;
            if (radioButton_mob_mobs.Focused == false & radioButton_mob_mobs.Checked == false) radioButton_mob_mobs.Image = Properties.Resources.mobs50;            
            if (radioButton_mob_npc.Focused == false & radioButton_mob_npc.Checked == false) radioButton_mob_npc.Image = Properties.Resources.npc50;
            if (radioButton_mob_rb.Focused == false & radioButton_mob_rb.Checked == false) radioButton_mob_rb.Image = Properties.Resources.rb50;
            if (radioButton_mob_grann_rb.Focused == false & radioButton_mob_grann_rb.Checked == false) radioButton_mob_grann_rb.Image = Properties.Resources.rb_grand50;
        }

        private void Func_Conect(object sender, EventArgs e)
        {
            dataGridView_spoil_mob.Columns.Clear();
            dataGridView_drop_mob.Columns.Clear();

            string groupBoxTipMobs = " ";
            string groupBoxMobIazvim = " ";
            string panelLvlMob = " ";
            string ReitsMob = " ";

            if (radioButton_mob_all.Checked)
            { groupBoxTipMobs = " tipMob >= 0 ";
            if (radioButton_mob_all.Focused)
            {radioButton_mob_all3.Checked = true; radioButton_mob_reti.Checked = false; radioButton_mob_all2.Checked = true;
              ClassMob.S_level = 1; ClassMob.PO_level = 87; ClassMob.S_Reit = 1; ClassMob.PO_Reit = 14;}
              groupBox_mob_iazvim.Enabled = true; panelMobLvl.Enabled = true; radioButton_mob_reti.Enabled = true; }
            else if (radioButton_mob_mobs.Checked)
            { groupBoxTipMobs = " tipMob = 1 ";
            if (radioButton_mob_mobs.Focused)
            { radioButton_mob_all3.Checked = true; radioButton_mob_reti.Checked = false; radioButton_mob_reti.Enabled = true; }
                groupBox_mob_iazvim.Enabled = true; panelMobLvl.Enabled = true; }
            else if (radioButton_mob_npc.Checked)
            {groupBoxTipMobs = " tipMob = 2 "; groupBox_mob_iazvim.Enabled = false; panelMobLvl.Enabled = false; groupBoxMobIazvim = " "; radioButton_mob_reti.Enabled = false;
            radioButton_mob_all2.Checked = true; radioButton_mob_all3.Checked = true; radioButton_mob_reti.Checked = false;}
            else if (radioButton_mob_rb.Checked)
            { groupBoxTipMobs = " tipMob = 3 ";
            if (radioButton_mob_rb.Focused) { radioButton_mob_all3.Checked = true; radioButton_mob_reti.Checked = false; }
                groupBox_mob_iazvim.Enabled = true; panelMobLvl.Enabled = true; radioButton_mob_reti.Enabled = false;}
            else if (radioButton_mob_grann_rb.Checked)
            {groupBoxTipMobs = " tipMob = 4 "; groupBox_mob_iazvim.Enabled = false; panelMobLvl.Enabled = false; groupBoxMobIazvim = " "; radioButton_mob_reti.Enabled = true;
            radioButton_mob_all2.Checked = true; radioButton_mob_all3.Checked = true; radioButton_mob_reti.Checked = false;}

            if (radioButton_mob_all3.Checked) { groupBoxMobIazvim = "  "; }
            else if (radioButton_mob_fire.Checked) { groupBoxMobIazvim = " AND fire = 1 "; }
            else if (radioButton_mob_wind.Checked) { groupBoxMobIazvim = " AND wind = 1"; }
            else if (radioButton__mob_water.Checked) { groupBoxMobIazvim = " AND water = 1 "; }
            else if (radioButton_mob_earch.Checked) { groupBoxMobIazvim = " AND earth = 1 "; }
            else if (radioButton_mob_holy.Checked) { groupBoxMobIazvim = " AND holy = 1 "; }
            else if (radioButton_mob_darc.Checked) { groupBoxMobIazvim = " AND dark = 1 "; }
            else if (radioButton_mob_bow.Checked) { groupBoxMobIazvim = " AND bow = 1 "; }
            else if (radioButton_mob_blunt.Checked) { groupBoxMobIazvim = " AND blunt = 1 "; }
            else if (radioButton_mob_dager.Checked) { groupBoxMobIazvim = " AND dager = 1 "; }
            else if (radioButton_mob_sword.Checked) { groupBoxMobIazvim = " AND sword = 1 "; }
            else if (radioButton_mob_dual.Checked) { groupBoxMobIazvim = " AND dual = 1 "; }
            else if (radioButton_mob_knuckle.Checked) { groupBoxMobIazvim = " AND knuckle = 1 "; }
            else if (radioButton_mob_spear.Checked) { groupBoxMobIazvim = " AND spear = 1 "; }
           
            if (radioButton_mob_all2.Checked) { panelLvlMob = " AND level >= 0 "; }
            else if (radioButton_mob_80.Checked) { panelLvlMob = " AND level >= 80 "; }
            else if (radioButton_mob_70.Checked) { panelLvlMob = " AND level >= 70 AND level <= 79 "; }
            else if (radioButton_mob_60.Checked) { panelLvlMob = " AND level >= 60 AND level <= 69 "; }
            else if (radioButton_mob_50.Checked) { panelLvlMob = " AND level >= 50 AND level <= 59 "; }
            else if (radioButton_mob_40.Checked) { panelLvlMob = " AND level >= 40 AND level <= 49 "; }
            else if (radioButton_mob_30.Checked) { panelLvlMob = " AND level >= 30 AND level <= 39 "; }
            else if (radioButton_mob_20.Checked) { panelLvlMob = " AND level >= 20 AND level <= 29 "; }
            else if (radioButton_mob_10.Checked) { panelLvlMob = " AND level >= 10 AND level <= 19 "; }
            else if (radioButton_mob_11.Checked) { panelLvlMob = " AND level >= 1 AND level <= 9 "; }
            else if (radioButton_mob_xx.Focused) { selection_level_mob form2 = new selection_level_mob(); form2.Owner = this; form2.ShowDialog(); }
            if (radioButton_mob_xx.Checked & ClassMob.S_level > 0)
            { panelLvlMob = " AND level >= " + ClassMob.S_level + " AND level <= " + ClassMob.PO_level + " "; }
            
            if (radioButton_mob_reti.Focused) { selection_reit_mob form2 = new selection_reit_mob(); form2.Owner = this; form2.ShowDialog(); }
            if (radioButton_mob_reti.Checked & ClassMob.S_Reit > 0)
            { ReitsMob = " AND ratesMob >= " + ClassMob.S_Reit + " AND ratesMob <= " + ClassMob.PO_Reit + " "; }

            list_mobs_output(-1001, "" + groupBoxTipMobs + "" + groupBoxMobIazvim + "" + panelLvlMob + "" + ReitsMob + ""); // Функция вывода мобов в dataGridView_list_mobs вкладка мобы

        }

        private void list_mobs_output(int idTemplate, string name)
        {
            dataGridView_list_mobs.Columns.Clear();
            ClassConnect tt = new ClassConnect();
            SQLiteConnection myConnection = new SQLiteConnection(tt.ToString());
            try
            {
                myConnection.Open();
            }
            catch (Exception l)
            {
                MessageBox.Show("Ошибка!\r\n (" + l.Message + ")", "Подключение к базе данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SQLiteCommand myCommand = myConnection.CreateCommand();
            DataTable list_mobs = new DataTable();
            string CommandText = "";

            if (idTemplate == -1001) // относиться к вкладке мобы
            {
                CommandText = "SELECT idTemplate, name, title, level, name_ru, title_ru FROM npc WHERE " + name + "ORDER BY level";
            }
            else if (idTemplate == -1000) // относиться к поиску вкладка мобы
            {
                int number;
                bool result = Int32.TryParse(name.Trim(), out number);
                if (result)
                {
                    CommandText = "SELECT idTemplate, name, title, level, name_ru, title_ru FROM npc WHERE id = " + name;
                }
                else
                {
                    if (radioButton_lang_ru.Checked)
                    {
                        CommandText = "SELECT idTemplate, name, title, level, name_ru, title_ru FROM npc WHERE lower(name_ru) LIKE '%" + name.ToLower(new CultureInfo("ru", false)) + "%' ORDER BY level";
                    }
                    else
                    {
                        CommandText = "SELECT idTemplate, name, title, level, name_ru, title_ru FROM npc WHERE name LIKE '%" + name.ToLower() + "%' ORDER BY level";
                    }
                }
            }
            else  // относиться к вкладке инвентарь и предметы
            {
                CommandText = "SELECT idTemplate, name, title, level, name_ru, title_ru FROM npc WHERE idTemplate = " + idTemplate + "";
            }
            ClassConnect.GridView_list_mobs(list_mobs, CommandText); // Создан отдельный файл с класом коннект (обозреватель решений)
            DataTableReader dtr_skill = list_mobs.CreateDataReader();
            int z = list_mobs.Rows.Count;

            dataGridView_list_mobs.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_list_mobs.Columns[0].Visible = false;
            dataGridView_list_mobs.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_list_mobs.Columns[1].HeaderText = "lvl";
            dataGridView_list_mobs.Columns[1].ReadOnly = true;
            dataGridView_list_mobs.Columns[1].Width = 28;
            dataGridView_list_mobs.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_list_mobs.Columns[2].HeaderText = "Имя";
            dataGridView_list_mobs.Columns[2].ReadOnly = true;
            dataGridView_list_mobs.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView_list_mobs.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView_list_mobs.Columns[2].DefaultCellStyle.Padding = new Padding(2);
            dataGridView_list_mobs.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView_list_mobs.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_list_mobs.Columns[3].Visible = false;
            dataGridView_list_mobs.RowCount = z;

            if (radioButton_lang_ru.Checked)
            {
                for (int i = 0; i < z; i++) // Циклзаполнения вклади клан шмот
                {
                    dtr_skill.Read();
                    dataGridView_list_mobs.Rows[i].Cells[0].Value = dtr_skill["idTemplate"].ToString(); // лвл моба
                    dataGridView_list_mobs.Rows[i].Cells[1].Value = dtr_skill["level"].ToString(); // лвл моба
                    dataGridView_list_mobs.Rows[i].Cells[3].Value = dtr_skill["title_ru"].ToString(); // имя моба

                    if (checkBox_mob_titul.Checked & dtr_skill["title_ru"].ToString() != "") // если выбран то включает титулы
                    {
                        dataGridView_list_mobs.Rows[i].Cells[2].Value = "[ " + dtr_skill["title_ru"].ToString()
                        + " ]  " + dtr_skill["name_ru"].ToString();
                    }
                    else
                    { dataGridView_list_mobs.Rows[i].Cells[2].Value = dtr_skill["name_ru"].ToString(); }
                }
            }
            else
            {
                for (int i = 0; i < z; i++) // Циклзаполнения вклади клан шмот
                {
                    dtr_skill.Read();
                    dataGridView_list_mobs.Rows[i].Cells[0].Value = dtr_skill["idTemplate"].ToString(); // лвл моба
                    dataGridView_list_mobs.Rows[i].Cells[1].Value = dtr_skill["level"].ToString(); // лвл моба
                    dataGridView_list_mobs.Rows[i].Cells[3].Value = dtr_skill["title"].ToString(); // имя моба

                    if (checkBox_mob_titul.Checked & dtr_skill["title"].ToString() != "") // если выбран то включает титулы
                    {
                        dataGridView_list_mobs.Rows[i].Cells[2].Value = "[ " + dtr_skill["title"].ToString()
                        + " ]  " + dtr_skill["name"].ToString();
                    }
                    else
                    { dataGridView_list_mobs.Rows[i].Cells[2].Value = dtr_skill["name"].ToString(); }
                }
            }
            if (idTemplate > -1000)
            {
                //dataGridView_list_mobs.CurrentCell = dataGridView_list_mobs[0, 0];
            }
            else
            {
                dataGridView_list_mobs.ClearSelection();
            }
            dtr_skill.Close();
            myConnection.Close();
        }

        private int Global_mob_search;
        private void button_mob_search_Click(object sender, EventArgs e) // Авто + поиск по имени моба
        {
            for (; Global_mob_search < dataGridView_list_mobs.RowCount; Global_mob_search++)
                if (dataGridView_list_mobs[2, Global_mob_search].FormattedValue.ToString().ToLower().Contains(textBox_mob_search.Text.ToLower()))
                { dataGridView_list_mobs.CurrentCell = dataGridView_list_mobs[2, Global_mob_search]; Global_mob_search++; return; }
            Global_mob_search = 0; 
        }

        private void button_mob_search_all_Click(object sender, EventArgs e)
        {
            if (textBox_mob_search.Text.Length < 1)
            {
                MessageBox.Show("Поле поиск пустое", "Вкладка мобы (поиск)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            list_mobs_output(-1000, textBox_mob_search.Text); // Функция вывода мобов в dataGridView_list_mobs вкладка мобы
        }

        private void button_mob_info_Click(object sender, EventArgs e) // Подробная информация о мобе
        {
            if (dataGridView_list_mobs.CurrentRow == null)
            { MessageBox.Show("Вы не выбрали моба", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            ClassMob.Info_Id = Convert.ToInt32(dataGridView_list_mobs.CurrentRow.Cells[0].Value);
            ClassMob.Info_level = Convert.ToInt32(dataGridView_list_mobs.CurrentRow.Cells[1].Value);
            ClassMob.Info_Name = Convert.ToString(dataGridView_list_mobs.CurrentRow.Cells[2].Value);
            mob_info form2 = new mob_info();
            form2.Text = dataGridView_list_mobs.CurrentRow.Cells[2].Value.ToString() + " " + dataGridView_list_mobs.CurrentRow.Cells[1].Value.ToString() + "lvl"; 
            form2.ShowDialog(); 
        }

        private void button_mob_map_Click(object sender, EventArgs e) // Карта большая
        {
            if (dataGridView_list_mobs.CurrentRow == null)
            { MessageBox.Show("Вы не выбрали моба", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            ClassMob.Info_Id = Convert.ToInt32(dataGridView_list_mobs.CurrentRow.Cells[0].Value);
            ClassMob.Info_level = Convert.ToInt32(dataGridView_list_mobs.CurrentRow.Cells[1].Value);
            ClassMob.Info_Name = Convert.ToString(dataGridView_list_mobs.CurrentRow.Cells[2].Value);
            int count = 0;
            string CommandText1 = "SELECT COUNT(*) FROM spawnlist WHERE npc_templateid = " + Convert.ToInt32(dataGridView_list_mobs.CurrentRow.Cells[0].Value) + "";
            DataTable t3 = new DataTable();
            ClassConnect.GridView_list_mobs(t3, CommandText1);
            DataTableReader dtr = t3.CreateDataReader(); dtr.Read();
            count = Convert.ToInt32(dtr[0]);
            ClassMob.Map_count = count;

            if (count == 0)
            {
                MessageBox.Show("Моба нет на карте", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                mob_map form2 = new mob_map();
                form2.Text = "Имя моба - (" + ClassMob.Info_Name + ")   лвл моба - (" + ClassMob.Info_level + ")   количество  точек респа - (" + count + ")";
                form2.ShowDialog();
            }
        }

        private void dataGridView_list_mobs_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            button_mob_map.PerformClick();
        }

        private void dataGridView_list_mobs_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var h = dataGridView_list_mobs.HitTest(e.X, e.Y);
                if (h.Type == DataGridViewHitTestType.Cell)
                {
                    dataGridView_list_mobs.Rows[h.RowIndex].Selected = true;
                    dataGridView_list_mobs.CurrentCell = dataGridView_list_mobs.Rows[h.RowIndex].Cells[0];
                    Menu_mob_info.Show(MousePosition.X, MousePosition.Y);
                }
                return;
            }
        }

        private int z = 0; // определяет уменьшать или увелич размер груп бокс дропа если нет спойла
        private void dataGridView_list_mobs_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e) // дроп с мобов
        {
            if (e.Button == MouseButtons.Right) { return; }
            int id;
            try { id = Convert.ToInt32(dataGridView_list_mobs.CurrentRow.Cells[0].Value); }
            catch { id = Convert.ToInt32(dataGridView_list_mobs.Rows[0].Cells[0].Value); }
            string CommandText = "SELECT etc.item_id, etc.icon, etc.crystal_type, min, max, chance, itd.name_ru, itd.name_en FROM droplist"
                               + " LEFT JOIN etcitem as etc ON etc.item_id = itemId"
                               + " LEFT JOIN item_desc as itd ON itd.id = itemId"
                               + " WHERE mobId = " + id + ""
                               + " AND category >= 0 ORDER BY chance ASC";
            DataTable t1 = new DataTable();
            ClassConnect.GridView_list_mobs(t1, CommandText); // Создан отдельный файл с класом коннект (обозреватель решений)
            DataTableReader dtr_set = t1.CreateDataReader();
            int zx = t1.Rows.Count;
            dataGridView_drop_mob.Columns.Clear();
            dataGridView_drop_mob.Columns.Add(new DataGridViewImageColumn());
            dataGridView_drop_mob.Columns[0].HeaderText = "Вид";
            dataGridView_drop_mob.Columns[0].Width = 40;
            dataGridView_drop_mob.Columns[0].DefaultCellStyle.Padding = new Padding(3);
            dataGridView_drop_mob.Columns.Add(new DataGridViewImageColumn());
            dataGridView_drop_mob.Columns[1].HeaderText = "Тип";
            dataGridView_drop_mob.Columns[1].Width = 40;
            dataGridView_drop_mob.Columns[1].DefaultCellStyle.Padding = new Padding(3);
            dataGridView_drop_mob.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_drop_mob.Columns[2].HeaderText = "Название";
            dataGridView_drop_mob.Columns[2].ReadOnly = true;
            dataGridView_drop_mob.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView_drop_mob.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView_drop_mob.Columns[2].DefaultCellStyle.Padding = new Padding(2);
            dataGridView_drop_mob.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_drop_mob.Columns[3].HeaderText = "min-max";
            dataGridView_drop_mob.Columns[3].ReadOnly = true;
            dataGridView_drop_mob.Columns[3].Width = 90;
            dataGridView_drop_mob.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_drop_mob.Columns[4].HeaderText = "Шанс";
            dataGridView_drop_mob.Columns[4].ReadOnly = true;
            dataGridView_drop_mob.Columns[4].Width = 90;
            dataGridView_drop_mob.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_drop_mob.Columns[5].Visible = false;
            dataGridView_drop_mob.RowCount = zx;

            for (int i = 0; i < zx; i++) // Цикл заполнения вклади клан шмот
            {
                dtr_set.Read();
                try // Если нет картинки вставяет пустую стандартную
                {
                    dataGridView_drop_mob.Rows[i].Cells[0].Value = Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dtr_set["icon"].ToString() + ".png");
                    dataGridView_drop_mob.Rows[i].Cells[0].ToolTipText = dtr_set["item_id"].ToString();
                }
                catch { dataGridView_drop_mob.Rows[i].Cells[0].Value = Properties.Resources.none_imge; dataGridView_drop_mob.Rows[i].Cells[0].ToolTipText = dtr_set["item_id"].ToString(); }
                try // Если нет картинки вставяет пустую стандартную
                {
                    dataGridView_drop_mob.Rows[i].Cells[1].Value = Image.FromFile(Application.StartupPath + @"\images\crystal\" + dtr_set["crystal_type"].ToString() + ".gif");
                }
                catch { dataGridView_drop_mob.Rows[i].Cells[1].Value = Properties.Resources.none_imge; }
                try
                {
                    if (radioButton_lang_ru.Checked)
                    {
                        dataGridView_drop_mob.Rows[i].Cells[2].Value = dtr_set["name_ru"].ToString();
                    }
                    else
                    {
                        dataGridView_drop_mob.Rows[i].Cells[2].Value = dtr_set["name_en"].ToString();
                    }
                    if (Convert.ToInt32(dtr_set["item_id"]) == 57)
                    {dataGridView_drop_mob.Rows[i].Cells[3].Value = Convert.ToInt32(dtr_set["min"]) * numericUpDown_seting_adena.Value + " - " + Convert.ToInt32(dtr_set["max"]) * numericUpDown_seting_adena.Value;}
                    else
                    { dataGridView_drop_mob.Rows[i].Cells[3].Value = dtr_set["min"].ToString() + " - " + dtr_set["max"].ToString(); }

                    double procent;
                    if (radioButton_mob_rb.Checked | radioButton_mob_grann_rb.Checked)
                    { procent = ((Convert.ToDouble(Regex.Replace(Convert.ToString(dtr_set["chance"]), @"\.", ",")) / 1000000 * Convert.ToDouble(numericUpDown_seting_dropRb.Value)) * 100); }
                    else
                    { procent = ((Convert.ToDouble(Regex.Replace(Convert.ToString(dtr_set["chance"]), @"\.", ",")) / 1000000 * Convert.ToDouble(numericUpDown_seting_drop.Value)) * 100); }  
                    if (procent > 100) { procent = 100; }
                    dataGridView_drop_mob.Rows[i].Cells[4].Value = "" + Math.Round(procent, 5) + " %";
                    dataGridView_drop_mob.Rows[i].Cells[5].Value = dtr_set["item_id"].ToString();
                }
                catch (Exception l)
                {
                    MessageBox.Show("Ошибка! №5\r\n (" + l.Message + ")", "Мобы дроп", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            dataGridView_drop_mob.ClearSelection();
            dtr_set.Close();

            string CommandText2 = "SELECT etc.item_id, etc.icon, etc.crystal_type, min, max, chance, itd.name_ru, itd.name_en FROM droplist"
                               + " LEFT JOIN etcitem as etc ON etc.item_id = itemId"
                               + " LEFT JOIN item_desc as itd ON itd.id = itemId"
                               + " WHERE mobId = " + id + ""
                               + " AND category < 0 ORDER BY chance ASC";
            DataTable t2 = new DataTable();
            ClassConnect.GridView_list_mobs(t2, CommandText2); // Создан отдельный файл с класом коннект (обозреватель решений)
            DataTableReader dtr_set2 = t2.CreateDataReader();

            if (t2.Rows.Count == 0)
            {
                groupBox2.Visible = false;
                if (z == 0) { groupBox1.Size = new System.Drawing.Size(groupBox1.Size.Width, groupBox1.Size.Height + 160); }
                groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                            | System.Windows.Forms.AnchorStyles.Left)
                            | System.Windows.Forms.AnchorStyles.Right)));
                z = 1;
                return;
            } // Если пустое поле непоказывает споил
            else
            {
                groupBox2.Visible = true;
                if (z == 1) { groupBox1.Size = new System.Drawing.Size(groupBox1.Size.Width, groupBox1.Size.Height - 160); }
                z = 0;
            }

            int zx2 = t2.Rows.Count;
            dataGridView_spoil_mob.Columns.Clear();
            dataGridView_spoil_mob.Columns.Add(new DataGridViewImageColumn());
            dataGridView_spoil_mob.Columns[0].HeaderText = "Вид";
            dataGridView_spoil_mob.Columns[0].Width = 40;
            dataGridView_spoil_mob.Columns[0].DefaultCellStyle.Padding = new Padding(3);
            dataGridView_spoil_mob.Columns.Add(new DataGridViewImageColumn());
            dataGridView_spoil_mob.Columns[1].HeaderText = "Тип";
            dataGridView_spoil_mob.Columns[1].Width = 40;
            dataGridView_spoil_mob.Columns[1].DefaultCellStyle.Padding = new Padding(3);
            dataGridView_spoil_mob.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_spoil_mob.Columns[2].HeaderText = "Название";
            dataGridView_spoil_mob.Columns[2].ReadOnly = true;
            dataGridView_spoil_mob.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView_spoil_mob.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView_spoil_mob.Columns[2].DefaultCellStyle.Padding = new Padding(2);
            dataGridView_spoil_mob.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_spoil_mob.Columns[3].HeaderText = "min-max";
            dataGridView_spoil_mob.Columns[3].ReadOnly = true;
            dataGridView_spoil_mob.Columns[3].Width = 90;
            dataGridView_spoil_mob.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_spoil_mob.Columns[4].HeaderText = "Шанс";
            dataGridView_spoil_mob.Columns[4].ReadOnly = true;
            dataGridView_spoil_mob.Columns[4].Width = 90;
            dataGridView_spoil_mob.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_spoil_mob.Columns[5].Visible = false;

            dataGridView_spoil_mob.RowCount = zx2;

            for (int i = 0; i < zx2; i++) // Цикл заполнения вклади клан шмот
            {
                dtr_set2.Read();
                try // Если нет картинки вставяет пустую стандартную
                {
                    dataGridView_spoil_mob.Rows[i].Cells[0].Value = Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dtr_set2["icon"].ToString() + ".png");
                    dataGridView_spoil_mob.Rows[i].Cells[0].ToolTipText = dtr_set2["item_id"].ToString();
                }
                catch { dataGridView_spoil_mob.Rows[i].Cells[0].Value = Properties.Resources.none_imge; dataGridView_spoil_mob.Rows[i].Cells[0].ToolTipText = dtr_set2["item_id"].ToString(); }
                try // Если нет картинки вставяет пустую стандартную
                {
                    dataGridView_spoil_mob.Rows[i].Cells[1].Value = Image.FromFile(Application.StartupPath + @"\images\crystal\" + dtr_set2["crystal_type"].ToString() + ".gif");
                }
                catch { dataGridView_spoil_mob.Rows[i].Cells[1].Value = Properties.Resources.none_imge; }
                try // Если нет картинки вставяет пустую стандартную
                {
                    if (radioButton_lang_ru.Checked)
                    {
                        dataGridView_spoil_mob.Rows[i].Cells[2].Value = dtr_set2["name_ru"].ToString();
                    }
                    else
                    {
                        dataGridView_spoil_mob.Rows[i].Cells[2].Value = dtr_set2["name_en"].ToString();
                    }
                  dataGridView_spoil_mob.Rows[i].Cells[3].Value = dtr_set2["min"].ToString() + " - " + dtr_set2["max"].ToString();
                  double procent;            
                    if (radioButton_mob_rb.Checked | radioButton_mob_grann_rb.Checked)
                    { procent = ((Convert.ToDouble(Regex.Replace(Convert.ToString(dtr_set2["chance"]), @"\.", ",")) / 1000000 * Convert.ToDouble(numericUpDown_seting_dropRb.Value)) * 100); }
                    else
                    { procent = ((Convert.ToDouble(Regex.Replace(Convert.ToString(dtr_set2["chance"]), @"\.", ",")) / 1000000 * Convert.ToDouble(numericUpDown_seting_spoil.Value)) * 100); } 
                    if (procent > 100) { procent = 100; }
                  dataGridView_spoil_mob.Rows[i].Cells[4].Value = "" + Math.Round(procent, 5) + " %";
                  dataGridView_spoil_mob.Rows[i].Cells[5].Value = dtr_set2["item_id"].ToString();
                }
                catch (Exception l)
                {
                    MessageBox.Show("Ошибка! №5\r\n (" + l.Message + ")", "Мобы дроп", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            dtr_set2.Close();
            dataGridView_spoil_mob.ClearSelection();
        }

        private void dataGridView_drop_mob_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            panel_shop_item.Visible = false;
            panel_rec_igridient.Visible = false;
            panel_item_drop.Visible = true;
            radioButton_item_1.Checked = true;
            expander_function(1, 0);
            tabControl_home.SelectedIndex = 1;
            string CommandText;
            if (radioButton_lang_ru.Checked)
            {
                CommandText = "SELECT item_id, itd.name_ru, icon, null, item_id FROM etcitem"
                                   + " LEFT JOIN item_desc as itd ON itd.id = item_id"
                                   + " WHERE item_id = " + dataGridView_drop_mob.CurrentRow.Cells[5].Value + "";
            }
            else
            {
                CommandText = "SELECT item_id, name, icon, null, item_id FROM etcitem WHERE item_id = " + dataGridView_drop_mob.CurrentRow.Cells[5].Value + "";
            }
            DataTable t1 = new DataTable();
            ClassConnect.GridView_list_mobs(t1, CommandText); // Создан отдельный файл с класом коннект (обозреватель решений)
            dataGridView_items.DataSource = t1;
            for (int i = 0; i < 7; i++) { dataGridView_items.Columns[i].Visible = false; } // Убирает лишнии столбцы таблицы
            dataGridView_items.Refresh();
            for (int i = 0; i < 2; i++) { dataGridView_items.Columns[i].Visible = true; }// отображает столбцы столбцы таблицы
            progressBar_items.Maximum = dataGridView_items.Rows.Count;
            for (int i = 0; i < dataGridView_items.Rows.Count; i++) // Цикл заполнения вклади клан шмот
            {
                progressBar_items.PerformStep();
                try // Если нет картинки вставяет пустую стандартную
                {
                    dataGridView_items.Rows[i].Cells[0].Value = Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dataGridView_items.Rows[i].Cells[4].Value + ".png");
                    dataGridView_items.Rows[i].Cells[0].ToolTipText = dataGridView_items.Rows[i].Cells[2].Value.ToString();
                }
                catch { dataGridView_items.Rows[i].Cells[0].Value = Properties.Resources.no_img; }
                try // Если нет картинки вставяет пустую стандартную
                {
                    dataGridView_items.Rows[i].Cells[1].Value = dataGridView_items.Rows[i].Cells[3].Value;
                }
                catch (Exception l)
                {
                    progressBar_items.Minimum = 1;
                    MessageBox.Show("Ошибка! №6\r\n (" + l.Message + ")", "Вкладка предметы (список предметов)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            dataGridView_items.CurrentCell = dataGridView_items[0, 0];
            tabPage_items.Refresh();
            dataGridView_items_drop();
        }

        private void dataGridView_spoil_mob_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            panel_shop_item.Visible = false;
            panel_rec_igridient.Visible = false;
            panel_item_drop.Visible = true;
            radioButton_item_1.Checked = true;
            expander_function(1, 0);
            tabControl_home.SelectedIndex = 1;
            string CommandText;
            if (radioButton_lang_ru.Checked)
            {
                CommandText = "SELECT item_id, itd.name_ru, icon, null, item_id FROM etcitem"
                                   + " LEFT JOIN item_desc as itd ON itd.id = item_id"
                                   + " WHERE item_id = " + dataGridView_spoil_mob.CurrentRow.Cells[5].Value + "";
            }
            else
            {
                CommandText = "SELECT item_id, name, icon, null, item_id FROM etcitem WHERE item_id = " + dataGridView_spoil_mob.CurrentRow.Cells[5].Value + "";
            }
            DataTable t1 = new DataTable();
            ClassConnect.GridView_list_mobs(t1, CommandText); // Создан отдельный файл с класом коннект (обозреватель решений)
            dataGridView_items.DataSource = t1;
            for (int i = 0; i < 7; i++) { dataGridView_items.Columns[i].Visible = false; } // Убирает лишнии столбцы таблицы
            dataGridView_items.Refresh();
            for (int i = 0; i < 2; i++) { dataGridView_items.Columns[i].Visible = true; }// отображает столбцы столбцы таблицы
            progressBar_items.Maximum = dataGridView_items.Rows.Count;
            for (int i = 0; i < dataGridView_items.Rows.Count; i++) // Цикл заполнения вклади клан шмот
            {
                progressBar_items.PerformStep();
                try // Если нет картинки вставяет пустую стандартную
                {
                    dataGridView_items.Rows[i].Cells[0].Value = Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dataGridView_items.Rows[i].Cells[4].Value + ".png");
                    dataGridView_items.Rows[i].Cells[0].ToolTipText = dataGridView_items.Rows[i].Cells[2].Value.ToString();
                }
                catch { dataGridView_items.Rows[i].Cells[0].Value = Properties.Resources.no_img; }
                try // Если нет картинки вставяет пустую стандартную
                {
                    dataGridView_items.Rows[i].Cells[1].Value = dataGridView_items.Rows[i].Cells[3].Value;
                }
                catch (Exception l)
                {
                    progressBar_items.Minimum = 1;
                    MessageBox.Show("Ошибка! №6\r\n (" + l.Message + ")", "Вкладка предметы (список предметов)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            dataGridView_items.CurrentCell = dataGridView_items[0, 0];
            tabPage_items.Refresh();
            dataGridView_items_drop();
        }

        #endregion
        
        #region вкладка Предметы

        private void expander_function(int this_object, int this_height) // функция раздвижного меню
        {
            int all_buton = 10; //  Всего кнопок
            int this_indent = 27; // Отступ от кнопки в низ
            int first_position = 20; // Первая кнопка её Y положение
            int this_widht = 140; // Ширина кнопок и панелей
            for (int i = 1; i <= all_buton; i++)
            {
                (groupBox_items.Controls["panel_item_" + i.ToString()] as Panel).Size = new System.Drawing.Size(this_widht, 0);
                if (i == 1 & this_object == 1)
                {
                    (groupBox_items.Controls["radioButton_item_" + i.ToString()] as RadioButton).Location = new System.Drawing.Point(7, first_position);
                    (groupBox_items.Controls["panel_item_" + i.ToString()] as Panel).Location = new System.Drawing.Point(7, +
                    (groupBox_items.Controls["radioButton_item_" + i.ToString()] as RadioButton).Location.Y + this_indent);
                    (groupBox_items.Controls["panel_item_" + i.ToString()] as Panel).Size = new System.Drawing.Size(this_widht, this_height);
                    (groupBox_items.Controls["panel_item_" + i.ToString()] as Panel).Visible = true;
                    continue;
                }
                if (i == 1)
                {
                    (groupBox_items.Controls["radioButton_item_" + i.ToString()] as RadioButton).Location = new System.Drawing.Point(7, first_position);
                    continue;
                }
                if (this_object == i)
                {
                    (groupBox_items.Controls["panel_item_" + i.ToString()] as Panel).Location = new System.Drawing.Point(7, +
                    (groupBox_items.Controls["radioButton_item_" + (this_object).ToString()] as RadioButton).Location.Y + this_indent);

                    (groupBox_items.Controls["panel_item_" + i.ToString()] as Panel).Size = new System.Drawing.Size(this_widht, this_height);

                    (groupBox_items.Controls["radioButton_item_" + i.ToString()] as RadioButton).Location = new System.Drawing.Point(7, +
                    (groupBox_items.Controls["radioButton_item_" + (this_object - 1).ToString()] as RadioButton).Location.Y + this_indent);

                    (groupBox_items.Controls["panel_item_" + i.ToString()] as Panel).Visible = true;
                    //MessageBox.Show("" + ((groupBox_items.Controls["radioButton_item_" + (this_object).ToString()] as RadioButton).Location.Y + this_indent) + "");
                    continue;
                }
                if (i == (this_object + 1))
                {
                    (groupBox_items.Controls["radioButton_item_" + i.ToString()] as RadioButton).Location = new System.Drawing.Point(7, +
                    (groupBox_items.Controls["radioButton_item_" + (this_object).ToString()] as RadioButton).Location.Y + this_height + this_indent);
                    (groupBox_items.Controls["panel_item_" + i.ToString()] as Panel).Visible = false;
                    continue;
                }
                (groupBox_items.Controls["panel_item_" + i.ToString()] as Panel).Visible = false;
                (groupBox_items.Controls["radioButton_item_" + i.ToString()] as RadioButton).Location = new System.Drawing.Point(7, +
                (groupBox_items.Controls["radioButton_item_" + (i - 1).ToString()] as RadioButton).Location.Y + this_indent);
            }
        }

        private void expander_button(object sender, EventArgs e)
        {
            radioButton_item_drop.Checked = true;
            string CommandText = "";
            string radioButton_item = "";
            treeView_item_rec.Nodes.Clear();
            pictureBox_product_id.Visible = false;
            label_name_recip.Visible = false;
            textBox_craft_lvl.Text = "";
            progressBar_items.Value = 0;
            if (radioButton_item_1.Checked) 
            { 
                expander_function(1, 134); expander_function(1, 134);
                if (radioButton_lang_ru.Checked)
                {
                    CommandText = "SELECT item_id, name_ru, icon, rec.successRate, item_id FROM etcitem"
                                + " LEFT JOIN recipe as rec ON rec.product_id = item_id"
                                + " LEFT JOIN item_desc as itd ON itd.id = item_id"
                                + " WHERE etcitem_type = 'material'  ORDER BY price DESC";
                }
                else
                {
                    CommandText = "SELECT item_id, name, icon, rec.successRate, item_id FROM etcitem"
                                + " LEFT JOIN recipe as rec ON rec.product_id = item_id"
                                + " WHERE etcitem_type = 'material'  ORDER BY price DESC";
                }
            }
            else if (radioButton_item_2.Checked) 
            { 
                expander_function(2, 78); expander_function(2, 78); radioButton_item = "WHERE etcitem_type_int = 1 "; 
            }
            else if (radioButton_item_3.Checked)
            {
                expander_function(3, 174); expander_function(3, 174);
                string icon = "";
                string item_id = "";
                pictureBox3.Focus();
                comboBox_lvl_rec.Refresh();
                comboBox_tip_rec.Refresh();
                if (checkBox_img_rec.Checked) { icon = "items.icon"; item_id = "items.item_id"; } else { icon = "item.icon"; item_id = "item.item_id"; }
                if (radioButton_lang_ru.Checked)
                {
                    CommandText = "SELECT " + item_id + ", itd.name_ru , " + icon + ", rec.successRate, rec.recipeId FROM recipe as rec"
                                     + " LEFT JOIN etcitem as item ON item.item_id = rec.recipeId"
                                     + " LEFT JOIN item_desc as itd ON itd.id = rec.recipeId"
                                     + " LEFT JOIN etcitem as items ON items.item_id = rec.product_id"
                                     + " " + ((ClassAddOn)comboBox_lvl_rec.SelectedItem).INDEX + ""
                                     + " " + ((ClassAddOn)comboBox_tip_rec.SelectedItem).INDEX + "";
                }
                else
                {
                    CommandText = "SELECT " + item_id + ", item.name , " + icon + ", rec.successRate, rec.recipeId FROM recipe as rec"
                                     + " LEFT JOIN etcitem as item ON item.item_id = rec.recipeId"
                                     + " LEFT JOIN etcitem as items ON items.item_id = rec.product_id"
                                     + " " + ((ClassAddOn)comboBox_lvl_rec.SelectedItem).INDEX + ""
                                     + " " + ((ClassAddOn)comboBox_tip_rec.SelectedItem).INDEX + "";
                }
            }
            else if (radioButton_item_4.Checked) { expander_function(4, 82); expander_function(4, 82); radioButton_item = "WHERE etcitem_type = 'scroll'"+
                                                 " AND material = 'paper' AND crystal_type = 'none' AND weight > 0 AND tradeable = 1 AND questitem = 0 ORDER BY item_id "; }
            else if (radioButton_item_5.Checked)
            { expander_function(5, 104); expander_function(5, 104); radioButton_item = "WHERE name LIKE '%Scroll: Enchant%'  ORDER BY name"; }
            else if (radioButton_item_6.Checked)
            { expander_function(6, 89); expander_function(6, 89); radioButton_item = "WHERE etcitem_type = 'dye'  ORDER BY price DESC"; }
            else if (radioButton_item_7.Checked)
            { expander_function(7, 89); expander_function(7, 89); radioButton_item = "WHERE etcitem_type = 'potion'  ORDER BY price DESC"; }
            else if (radioButton_item_8.Checked)
            { expander_function(8, 89); expander_function(8, 89); radioButton_item = "WHERE icon LIKE '%etc_raid%'  ORDER BY name DESC"; }
            else if (radioButton_item_9.Checked)
            { expander_function(9, 107); expander_function(9, 107); radioButton_item = "WHERE name LIKE '%Life Stone%'  ORDER BY name DESC"; }
            if (radioButton_item_10.Checked)
            {
                expander_function(10, 107); expander_function(10, 107);
                if (radioButton_lang_ru.Checked)
                {
                    CommandText = "SELECT it_s.item_id, itd.name_ru, etc.icon, etc.type, etc.item_id AS item_id2 FROM item_save AS it_s"
                                + " LEFT JOIN item_desc as itd ON itd.id = it_s.item_id"
                                + " LEFT JOIN etcitem as etc ON etc.item_id = it_s.item_id";
                }
                else
                {
                    CommandText = "SELECT it_s.item_id, etc.name, etc.icon, etc.type, etc.item_id AS item_id2 FROM item_save AS it_s"
                                + " LEFT JOIN etcitem as etc ON etc.item_id = it_s.item_id";
                }
            }

            if (radioButton_item_3.Checked == false & radioButton_item_1.Checked == false & radioButton_item_10.Checked == false)
            {
                if (radioButton_lang_ru.Checked)
                {
                    CommandText = "SELECT item_id, itd.name_ru, icon, type, item_id AS item_id2 FROM etcitem "
                                + " LEFT JOIN item_desc as itd ON itd.id = item_id " + radioButton_item;
                }
                else
                {
                    CommandText = "SELECT item_id, name, icon, type, item_id AS item_id2 FROM etcitem " + radioButton_item;
                }
            }
            if (pictureBox3.Focused == false)
            {
                radioButton_rec_drop.Checked = true;
                panel_item_drop.Visible = true;
                panel_rec_igridient.Visible = false;
                checkBox_img_rec.Checked = false;
                comboBox_lvl_rec.Text = "Все";
                comboBox_tip_rec.Text = "Все";
            }
            groupBox_items.Refresh();
            DataTable t1 = new DataTable();
            ClassConnect.GridView_list_mobs(t1, CommandText); // Создан отдельный файл с класом коннект (обозреватель решений)
            dataGridView_items.DataSource = t1;
            for (int i = 0; i < 7; i++) { dataGridView_items.Columns[i].Visible = false; } // Убирает лишнии столбцы таблицы
            dataGridView_items.Refresh();            
            for (int i = 0; i < 2; i++) { dataGridView_items.Columns[i].Visible = true; }// отображает столбцы столбцы таблицы
            progressBar_items.Maximum = dataGridView_items.Rows.Count;
            for (int i = 0; i < dataGridView_items.Rows.Count; i++) // Цикл заполнения вклади клан шмот
            {
                progressBar_items.PerformStep();
                try // Если нет картинки вставяет пустую стандартную
                {
                    dataGridView_items.Rows[i].Cells[0].Value = Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dataGridView_items.Rows[i].Cells[4].Value + ".png");
                    dataGridView_items.Rows[i].Cells[0].ToolTipText = dataGridView_items.Rows[i].Cells[2].Value.ToString();
                }
                catch { dataGridView_items.Rows[i].Cells[0].Value = Properties.Resources.no_img; }
                try // Если нет картинки вставяет пустую стандартную
                {
                    dataGridView_items.Rows[i].Cells[1].Value = dataGridView_items.Rows[i].Cells[3].Value;
                }
                catch (Exception l)
                {
                    progressBar_items.Minimum = 1;
                    MessageBox.Show("Ошибка! №6\r\n (" + l.Message + ")", "Вкладка предметы (список предметов)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            dataGridView_items.ClearSelection();
        }

        private int Global_item_search;
        private void button_item_search_Click(object sender, EventArgs e)
        {
            for (; Global_item_search < dataGridView_items.RowCount; Global_item_search++)
                if (dataGridView_items[1, Global_item_search].FormattedValue.ToString().ToLower().Contains(textBox_item_search.Text.ToLower()))
                { dataGridView_items.CurrentCell = dataGridView_items[1, Global_item_search]; Global_item_search++; return; }
            Global_item_search = 0; 
        }

        private void button_item_search_all_Click(object sender, EventArgs e)
        {
            if (textBox_item_search.Text.Length < 1)
            {
                MessageBox.Show("Поле поиск пустое", "Вкладка предметы (поиск)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string CommandText = "";
            int number;
            bool result = Int32.TryParse(textBox_item_search.Text.Trim(), out number);
            if (result)
            {
                if (radioButton_lang_ru.Checked)
                {
                    CommandText = "SELECT item_id, itd.name_ru, icon, type, item_id AS item_id2 FROM etcitem "
                                   + " LEFT JOIN item_desc as itd ON itd.id = item_id "
                                   + " WHERE item_id = " + textBox_item_search.Text;
                }
                else
                {
                    CommandText = "SELECT item_id, name, icon, type, item_id AS item_id2 FROM etcitem WHERE item_id = " + textBox_item_search.Text;
                }
            }
            else
            {
                if (radioButton_lang_ru.Checked)
                {
                    CommandText = "SELECT item_id, itd.name_ru, icon, type, item_id AS item_id2 FROM etcitem "
                                   + " LEFT JOIN item_desc as itd ON itd.id = item_id "
                                   + " WHERE lower(itd.name_ru) LIKE '%" + textBox_item_search.Text.ToLower() + "%'";
                }
                else
                {
                    CommandText = "SELECT item_id, name, icon, type, item_id AS item_id2 FROM etcitem WHERE name LIKE '%" + textBox_item_search.Text.ToLower() + "%'";
                }
            }
            DataTable t1 = new DataTable();
            ClassConnect.GridView_list_mobs(t1, CommandText); // Создан отдельный файл с класом коннект (обозреватель решений)
            dataGridView_items.DataSource = t1;
            for (int i = 0; i < 7; i++) { dataGridView_items.Columns[i].Visible = false; } // Убирает лишнии столбцы таблицы
            dataGridView_items.Refresh();
            for (int i = 0; i < 2; i++) { dataGridView_items.Columns[i].Visible = true; }// отображает столбцы столбцы таблицы
            progressBar_items.Maximum = dataGridView_items.Rows.Count;
            for (int i = 0; i < dataGridView_items.Rows.Count; i++) // Цикл заполнения вклади клан шмот
            {
                progressBar_items.PerformStep();
                try // Если нет картинки вставяет пустую стандартную
                {
                    dataGridView_items.Rows[i].Cells[0].Value = Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dataGridView_items.Rows[i].Cells[4].Value + ".png");
                    dataGridView_items.Rows[i].Cells[0].ToolTipText = dataGridView_items.Rows[i].Cells[2].Value.ToString();
                }
                catch { dataGridView_items.Rows[i].Cells[0].Value = Properties.Resources.no_img; }
                try // Если нет картинки вставяет пустую стандартную
                {
                    dataGridView_items.Rows[i].Cells[1].Value = dataGridView_items.Rows[i].Cells[3].Value;
                }
                catch (Exception l)
                {
                    progressBar_items.Minimum = 1;
                    MessageBox.Show("Ошибка! №6\r\n (" + l.Message + ")", "Вкладка предметы (список предметов)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            dataGridView_items.ClearSelection();
        }

        private void dataGridView_items_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView_items.CurrentRow == null) { return; }
            ClassMob.Items_Id = Convert.ToInt32(dataGridView_items.CurrentRow.Cells[2].Value);
            ClassMob.Items_Name = Convert.ToString(dataGridView_items.CurrentRow.Cells[3].Value);
            items_info form2 = new items_info();
            form2.Text = "Подробное описание " + dataGridView_items.CurrentRow.Cells[3].Value.ToString(); form2.ShowDialog(); 
        }

        private void dataGridView_items_MouseClick(object sender, MouseEventArgs e) 
        {
            if (e.Button == MouseButtons.Right)
            {
                var h = dataGridView_items.HitTest(e.X, e.Y);
                if (h.Type == DataGridViewHitTestType.Cell)
                {
                    dataGridView_items.Rows[h.RowIndex].Selected = true;
                    dataGridView_items.CurrentCell = dataGridView_items.Rows[h.RowIndex].Cells[0];
                    if(radioButton_item_10.Checked == false)
                    Menu_items_add.Show(MousePosition.X, MousePosition.Y);
                    else
                    Menu_item_del.Show(MousePosition.X, MousePosition.Y);
                }
                return;
            }
            if (panel_rec_igridient.Visible == true) { treeView_item(); } // забивает в treeView_item_rec ингридиенты рецепта
            if (panel_item_drop.Visible == true) { dataGridView_items_drop(); } // забивает nps с кого дропаеться шмотка
            shop_item(); // Отображает npc у кого есть в продаже шмотка
        }

        private void treeView_item()
        {
            treeView_item_rec.Nodes.Clear();
            pictureBox_product_id.Visible = false;
            label_name_recip.Visible = false;
            textBox_craft_lvl.Text = "";
            if (dataGridView_items.CurrentRow.Cells[5].Value.ToString() == "") { return; }
            if (radioButton_item_3.Checked & radioButton_rec_drop.Checked) { return; }
            if (radioButton_item_3.Checked || radioButton_item_1.Checked)
            {
                ClassConnect tt = new ClassConnect();
                SQLiteConnection myConnection = new SQLiteConnection(tt.ToString());
                try
                {
                    myConnection.Open();
                }
                catch (Exception l)
                {
                    MessageBox.Show("Ошибка!\r\n (" + l.Message + ")", "Подключение к базе данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                SQLiteCommand myCommand = myConnection.CreateCommand();
                string item_id = "";
                if (checkBox_img_rec.Checked) { item_id = "rec.product_id"; } else { item_id = "rec.recipeId"; }
                if (radioButton_item_craft.Checked) { item_id = "rec.product_id"; }
                if (radioButton_lang_ru.Checked)
                {
                    myCommand.CommandText = "SELECT rec.product_id, rec.ingredient_id, rec.ingredient_count, etc.icon, itd.name_ru, rec.craftLevel, rec.type FROM etcitem as etc"
                              + " LEFT JOIN item_desc as itd ON itd.id = etc.item_id "
                              + " LEFT JOIN recipe as rec ON rec.product_id = etc.item_id"
                              + " WHERE " + item_id + " = " + dataGridView_items.CurrentRow.Cells[2].Value + " AND rec.successRate = " + dataGridView_items.CurrentRow.Cells[5].Value + "";
                }
                else
                {
                    myCommand.CommandText = "SELECT rec.product_id, rec.ingredient_id, rec.ingredient_count, etc.icon, etc.name, rec.craftLevel, rec.type FROM etcitem as etc"
                              + " LEFT JOIN recipe as rec ON rec.product_id = etc.item_id"
                              + " WHERE " + item_id + " = " + dataGridView_items.CurrentRow.Cells[2].Value + " AND rec.successRate = " + dataGridView_items.CurrentRow.Cells[5].Value + "";
                }
                SQLiteDataReader dataRead0 = myCommand.ExecuteReader();
                dataRead0.Read();
                if (dataRead0.HasRows == false) { dataRead0.Close(); myConnection.Close(); return; }
                pictureBox_product_id.Image = Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dataRead0["icon"].ToString() + ".png");
                pictureBox_product_id.Visible = true;
                if (radioButton_lang_ru.Checked)
                {
                    label_name_recip.Text = dataRead0["name_ru"].ToString();
                }
                else
                {
                    label_name_recip.Text = dataRead0["name"].ToString();

                }
                label_name_recip.Visible = true;
                textBox_craft_lvl.Text = dataRead0["craftLevel"].ToString() + "-й уровень крафта " + dataRead0["type"].ToString();
                string[] ingredient_id = Regex.Split(dataRead0["ingredient_id"].ToString(), @"	");
                string[] ingredient_count = Regex.Split(dataRead0["ingredient_count"].ToString(), @"	");
                dataRead0.Close();
                treeView_item_rec.BeginUpdate();
                ImageList myImageList = new ImageList();
                myImageList.ImageSize = new Size(32, 32);
                myImageList.ColorDepth = ColorDepth.Depth24Bit;
                int z = 0;

                for (int i = 0; i < ingredient_id.Length; i++)
                {
                    if (radioButton_lang_ru.Checked)
                    {
                        myCommand.CommandText = "SELECT rec.product_id, rec.ingredient_id, rec.ingredient_count, etc.icon, itd.name_ru FROM etcitem as etc"
                                              + " LEFT JOIN item_desc as itd ON itd.id = etc.item_id "
                                              + " LEFT JOIN recipe as rec ON rec.product_id = etc.item_id"
                                              + " WHERE etc.item_id = " + ingredient_id[i].ToString() + " ";
                    }
                    else
                    {
                        myCommand.CommandText = "SELECT rec.product_id, rec.ingredient_id, rec.ingredient_count, etc.icon, etc.name FROM etcitem as etc"
                                              + " LEFT JOIN recipe as rec ON rec.product_id = etc.item_id"
                                              + " WHERE etc.item_id = " + ingredient_id[i].ToString() + " ";
                    }
                    SQLiteDataReader dataRead = myCommand.ExecuteReader();
                    dataRead.Read();
                    myImageList.Images.Add(Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dataRead["icon"].ToString() + ".png"));
                    treeView_item_rec.ImageList = myImageList;
                    TreeNode rootNode;
                    if (radioButton_lang_ru.Checked)
                    {
                        rootNode = new TreeNode(ingredient_count[i].ToString() + " - " + dataRead["name_ru"].ToString());
                    }
                    else
                    {
                        rootNode = new TreeNode(ingredient_count[i].ToString() + " - " + dataRead["name"].ToString());
                    }
                    rootNode.ImageIndex = z;
                    rootNode.SelectedImageIndex = z;
                    rootNode.Name = ingredient_id[i].ToString();
                    z++;
                    treeView_item_rec.Nodes.Add(rootNode);

                    if (dataRead["ingredient_id"].ToString() != "")
                    {
                        string[] ingredient_id2 = Regex.Split(dataRead["ingredient_id"].ToString(), @"	");
                        string[] ingredient_count2 = Regex.Split(dataRead["ingredient_count"].ToString(), @"	");
                        dataRead.Close();
                        for (int i2 = 0; i2 < ingredient_id2.Length; i2++)
                        {
                            if (radioButton_lang_ru.Checked)
                            {
                                myCommand.CommandText = "SELECT rec.product_id, rec.ingredient_id, rec.ingredient_count, etc.icon, itd.name_ru FROM etcitem as etc"
                                                      + " LEFT JOIN item_desc as itd ON itd.id = etc.item_id "
                                                      + " LEFT JOIN recipe as rec ON rec.product_id = etc.item_id"
                                                      + " WHERE etc.item_id = " + ingredient_id2[i2].ToString() + " ";
                            }
                            else
                            {
                                myCommand.CommandText = "SELECT rec.product_id, rec.ingredient_id, rec.ingredient_count, etc.icon, etc.name FROM etcitem as etc"
                                                      + " LEFT JOIN recipe as rec ON rec.product_id = etc.item_id"
                                                      + " WHERE etc.item_id = " + ingredient_id2[i2].ToString() + " ";
                            }
                            SQLiteDataReader dataRead2 = myCommand.ExecuteReader();
                            dataRead2.Read();
                            myImageList.Images.Add(Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dataRead2["icon"].ToString() + ".png"));
                            TreeNode rootNode2;
                            if (radioButton_lang_ru.Checked)
                            {
                                rootNode2 = new TreeNode(ingredient_count2[i2].ToString() + " - " + dataRead2["name_ru"].ToString());
                            }
                            else
                            {
                                rootNode2 = new TreeNode(ingredient_count2[i2].ToString() + " - " + dataRead2["name"].ToString());
                            }
                            rootNode2.ImageIndex = z;
                            rootNode2.SelectedImageIndex = z;
                            z++;
                            treeView_item_rec.Nodes[i].Nodes.Add(rootNode2);
                            if (dataRead2["ingredient_id"].ToString() != "")
                            {
                                string[] ingredient_id3 = Regex.Split(dataRead2["ingredient_id"].ToString(), @"	");
                                string[] ingredient_count3 = Regex.Split(dataRead2["ingredient_count"].ToString(), @"	");
                                dataRead2.Close();
                                for (int i3 = 0; i3 < ingredient_id3.Length; i3++)
                                {
                                    if (radioButton_lang_ru.Checked)
                                    {
                                        myCommand.CommandText = "SELECT rec.product_id, rec.ingredient_id, etc.icon, rec.ingredient_count, itd.name_ru FROM etcitem as etc"
                                                              + " LEFT JOIN item_desc as itd ON itd.id = etc.item_id "
                                                              + " LEFT JOIN recipe as rec ON rec.product_id = etc.item_id"
                                                              + " WHERE etc.item_id = " + ingredient_id3[i3].ToString() + " ";
                                    }
                                    else
                                    {
                                        myCommand.CommandText = "SELECT rec.product_id, rec.ingredient_id, etc.icon, rec.ingredient_count, etc.name FROM etcitem as etc"
                                                              + " LEFT JOIN recipe as rec ON rec.product_id = etc.item_id"
                                                              + " WHERE etc.item_id = " + ingredient_id3[i3].ToString() + " ";
                                    }
                                    SQLiteDataReader dataRead3 = myCommand.ExecuteReader();
                                    dataRead3.Read();
                                    myImageList.Images.Add(Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dataRead3["icon"].ToString() + ".png"));
                                    TreeNode rootNode3;
                                    if (radioButton_lang_ru.Checked)
                                    {
                                        rootNode3 = new TreeNode(ingredient_count3[i3].ToString() + " - " + dataRead3["name_ru"].ToString());
                                    }
                                    else
                                    {
                                        rootNode3 = new TreeNode(ingredient_count3[i3].ToString() + " - " + dataRead3["name"].ToString());
                                    }
                                    rootNode3.ImageIndex = z;
                                    rootNode3.SelectedImageIndex = z;
                                    z++;
                                    treeView_item_rec.Nodes[i].Nodes[i2].Nodes.Add(rootNode3);
                                    if (dataRead3["ingredient_id"].ToString() != "")
                                    {
                                        string[] ingredient_id4 = Regex.Split(dataRead3["ingredient_id"].ToString(), @"	");
                                        string[] ingredient_count4 = Regex.Split(dataRead3["ingredient_count"].ToString(), @"	");
                                        dataRead3.Close();
                                        for (int i4 = 0; i4 < ingredient_id4.Length; i4++)
                                        {
                                            if (radioButton_lang_ru.Checked)
                                            {
                                                myCommand.CommandText = "SELECT rec.product_id, rec.ingredient_id, etc.icon, rec.ingredient_count, itd.name_ru FROM etcitem as etc"
                                                                      + " LEFT JOIN item_desc as itd ON itd.id = etc.item_id "
                                                                      + " LEFT JOIN recipe as rec ON rec.product_id = etc.item_id"
                                                                      + " WHERE etc.item_id = " + ingredient_id4[i4].ToString() + " ";
                                            }
                                            else
                                            {
                                                myCommand.CommandText = "SELECT rec.product_id, rec.ingredient_id, etc.icon, rec.ingredient_count, etc.name FROM etcitem as etc"
                                                                      + " LEFT JOIN recipe as rec ON rec.product_id = etc.item_id"
                                                                      + " WHERE etc.item_id = " + ingredient_id4[i4].ToString() + " ";
                                            }
                                            SQLiteDataReader dataRead4 = myCommand.ExecuteReader();
                                            dataRead4.Read();
                                            myImageList.Images.Add(Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dataRead4["icon"].ToString() + ".png"));
                                            TreeNode rootNode4;
                                            if (radioButton_lang_ru.Checked)
                                            {
                                                rootNode4 = new TreeNode(ingredient_count4[i4].ToString() + " - " + dataRead4["name_ru"].ToString());
                                            }
                                            else
                                            {
                                                rootNode4 = new TreeNode(ingredient_count4[i4].ToString() + " - " + dataRead4["name"].ToString());
                                            }
                                            rootNode4.ImageIndex = z;
                                            rootNode4.SelectedImageIndex = z;
                                            z++;
                                            treeView_item_rec.Nodes[i].Nodes[i2].Nodes[i3].Nodes.Add(rootNode4);
                                            if (dataRead4["ingredient_id"].ToString() != "")
                                            {
                                                string[] ingredient_id5 = Regex.Split(dataRead4["ingredient_id"].ToString(), @"	");
                                                string[] ingredient_count5 = Regex.Split(dataRead4["ingredient_count"].ToString(), @"	");
                                                dataRead4.Close();
                                                for (int i5 = 0; i5 < ingredient_id5.Length; i5++)
                                                {
                                                    if (radioButton_lang_ru.Checked)
                                                    {
                                                        myCommand.CommandText = "SELECT rec.product_id, rec.ingredient_id, etc.icon, rec.ingredient_count, itd.name_ru FROM etcitem as etc"
                                                                              + " LEFT JOIN item_desc as itd ON itd.id = etc.item_id "
                                                                              + " LEFT JOIN recipe as rec ON rec.product_id = etc.item_id"
                                                                              + " WHERE etc.item_id = " + ingredient_id5[i5].ToString() + " ";
                                                    }
                                                    else
                                                    {
                                                        myCommand.CommandText = "SELECT rec.product_id, rec.ingredient_id, etc.icon, rec.ingredient_count, etc.name FROM etcitem as etc"
                                                                              + " LEFT JOIN recipe as rec ON rec.product_id = etc.item_id"
                                                                              + " WHERE etc.item_id = " + ingredient_id5[i5].ToString() + " ";
                                                    }
                                                        SQLiteDataReader dataRead5 = myCommand.ExecuteReader();
                                                    dataRead5.Read();
                                                    myImageList.Images.Add(Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dataRead5["icon"].ToString() + ".png"));
                                                    TreeNode rootNode5;
                                                    if (radioButton_lang_ru.Checked)
                                                    {
                                                        rootNode5 = new TreeNode(ingredient_count5[i5].ToString() + " - " + dataRead5["name_ru"].ToString());
                                                    }
                                                    else
                                                    {
                                                        rootNode5 = new TreeNode(ingredient_count5[i5].ToString() + " - " + dataRead5["name"].ToString());
                                                    }
                                                    rootNode5.ImageIndex = z;
                                                    rootNode5.SelectedImageIndex = z;
                                                    z++;
                                                    treeView_item_rec.Nodes[i].Nodes[i2].Nodes[i3].Nodes[i4].Nodes.Add(rootNode5);
                                                    dataRead5.Close();
                                                }
                                            }
                                            dataRead4.Close();
                                        }
                                    }
                                    dataRead3.Close();
                                }
                            }
                            dataRead2.Close();
                        }
                    }
                    dataRead.Close();
                }
                treeView_item_rec.EndUpdate();
                myConnection.Close();
            }
        }

        private void dataGridView_items_drop()
        {
            if (dataGridView_items.Rows.Count == 0) { return; }
            int id;
            try { id = Convert.ToInt32(dataGridView_items.CurrentRow.Cells[6].Value); }
            catch { id = Convert.ToInt32(dataGridView_items.Rows[0].Cells[6].Value); }
            string CommandText = "";
            if (radioButton_lang_ru.Checked)
            {
                CommandText = "SELECT npc.name_ru, npc.idTemplate, min, max, chance, npc.level, npc.title_ru FROM droplist"
                       + " LEFT JOIN npc as npc ON npc.idTemplate = mobId"
                       + " WHERE itemId = " + id + ""
                       + " AND category >= 0 ORDER BY chance DESC";
            }
            else
            {
                CommandText = "SELECT npc.name, npc.idTemplate, min, max, chance, npc.level, npc.title FROM droplist"
                       + " LEFT JOIN npc as npc ON npc.idTemplate = mobId"
                       + " WHERE itemId = " + id + ""
                       + " AND category >= 0 ORDER BY chance DESC";
            }
            DataTable t1 = new DataTable();
            ClassConnect.GridView_list_mobs(t1, CommandText); // Создан отдельный файл с класом коннект (обозреватель решений)
            dataGridView_drop_item.DataSource = t1;
            for (int i = 0; i < 11; i++) { dataGridView_drop_item.Columns[i].Visible = false; }// Убирает лишнии столбцы таблицы
            for (int i = 0; i < 4; i++) { dataGridView_drop_item.Columns[i].Visible = true; }// отображает столбцы столбцы таблицы     
            for (int i = 0; i < dataGridView_drop_item.Rows.Count; i++) // Цикл заполнения вклади клан шмот
            {
                try 
                {
                    dataGridView_drop_item.Rows[i].Cells[0].Value = dataGridView_drop_item.Rows[i].Cells[9].Value.ToString();
                    if (dataGridView_drop_item.Rows[i].Cells[10].Value.ToString() == "")
                    { dataGridView_drop_item.Rows[i].Cells[1].Value = dataGridView_drop_item.Rows[i].Cells[4].Value.ToString(); }
                    else
                    { dataGridView_drop_item.Rows[i].Cells[1].Value = "[" + dataGridView_drop_item.Rows[i].Cells[10].Value.ToString() + "] " + dataGridView_drop_item.Rows[i].Cells[4].Value.ToString(); }
                    dataGridView_drop_item.Rows[i].Cells[2].Value = "" + dataGridView_drop_item.Rows[i].Cells[6].Value.ToString() + "-" + dataGridView_drop_item.Rows[i].Cells[7].Value.ToString() + "";
                    double procent1 = (Convert.ToDouble(Regex.Replace(Convert.ToString(dataGridView_drop_item.Rows[i].Cells[8].Value), @"\.", ",")) / 1000000 * Convert.ToDouble(numericUpDown_seting_drop.Value)) * 100;
                    if (procent1 > 100) { procent1 = 100; }
                    dataGridView_drop_item.Rows[i].Cells[3].Value = Math.Round(procent1, 5) + " %";
                }
                catch (Exception l)
                {
                    MessageBox.Show("Ошибка! №8\r\n (" + l.Message + ")", "Предметы дроп", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                dataGridView_drop_item.ClearSelection();
            }
            string CommandText2 = "";
            if (radioButton_lang_ru.Checked)
            {
                CommandText2 = "SELECT npc.name_ru, npc.idTemplate, min, max, chance, npc.level, npc.title_ru FROM droplist"
                                    + " LEFT JOIN npc as npc ON npc.idTemplate = mobId"
                                    + " WHERE itemId = " + id + ""
                                    + " AND category < 0 ORDER BY chance DESC";
            }
            else
            {
                CommandText2 = "SELECT npc.name, npc.idTemplate, min, max, chance, npc.level, npc.title FROM droplist"
                                    + " LEFT JOIN npc as npc ON npc.idTemplate = mobId"
                                    + " WHERE itemId = " + id + ""
                                    + " AND category < 0 ORDER BY chance DESC";
            }
            DataTable t2 = new DataTable();
            ClassConnect.GridView_list_mobs(t2, CommandText2); // Создан отдельный файл с класом коннект (обозреватель решений)
            dataGridView_spoil_item.DataSource = t2;
            for (int i = 0; i < 11; i++) { dataGridView_spoil_item.Columns[i].Visible = false; }// Убирает лишнии столбцы таблицы
            for (int i = 0; i < 4; i++) { dataGridView_spoil_item.Columns[i].Visible = true; }// отображает столбцы столбцы таблицы     
            for (int i = 0; i < dataGridView_spoil_item.Rows.Count; i++) // Цикл заполнения вклади клан шмот
            {
                try
                {
                    dataGridView_spoil_item.Rows[i].Cells[0].Value = dataGridView_spoil_item.Rows[i].Cells[9].Value.ToString();
                    if (dataGridView_spoil_item.Rows[i].Cells[10].Value.ToString() == "")
                    { dataGridView_spoil_item.Rows[i].Cells[1].Value = dataGridView_spoil_item.Rows[i].Cells[4].Value.ToString(); }
                    else
                    { dataGridView_spoil_item.Rows[i].Cells[1].Value = "[" + dataGridView_spoil_item.Rows[i].Cells[10].Value.ToString() + "] " + dataGridView_spoil_item.Rows[i].Cells[4].Value.ToString(); }
                    dataGridView_spoil_item.Rows[i].Cells[2].Value = "" + dataGridView_spoil_item.Rows[i].Cells[6].Value.ToString() + "-" + dataGridView_spoil_item.Rows[i].Cells[7].Value.ToString() + "";
                    double procent = (Convert.ToDouble(Regex.Replace(Convert.ToString(dataGridView_spoil_item.Rows[i].Cells[8].Value), @"\.", ",")) / 1000000 * Convert.ToDouble(numericUpDown_seting_spoil.Value)) * 100;
                    if (procent > 100) { procent = 100; }
                    dataGridView_spoil_item.Rows[i].Cells[3].Value = Math.Round(procent, 5) + " %";
                }
                catch (Exception l)
                {
                    MessageBox.Show("Ошибка! №8\r\n (" + l.Message + ")", "Предметы дроп", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                dataGridView_spoil_item.ClearSelection();
            }
        }

        private void shop_item()
        {
            int id;
            try { id = Convert.ToInt32(dataGridView_items.CurrentRow.Cells[6].Value); }
            catch { id = Convert.ToInt32(dataGridView_items.Rows[0].Cells[6].Value); }
            string CommandText = "";
            if (radioButton_lang_ru.Checked)
            {
                CommandText = "SELECT npc.idTemplate, npc.name_ru, npc.title_ru, m_bu.price FROM merchant_buylists as m_bu"
                                   + " LEFT JOIN merchant_shopids as m_hop ON m_hop.shop_id = m_bu.shop_id"
                                   + " LEFT JOIN npc as npc ON npc.idTemplate = m_hop.npc_id"
                                   + " WHERE m_bu.item_id = " + id + " AND npc.name != '' ";
            }
            else
            {
                CommandText = "SELECT npc.idTemplate, npc.name, npc.title, m_bu.price FROM merchant_buylists as m_bu"
                                   + " LEFT JOIN merchant_shopids as m_hop ON m_hop.shop_id = m_bu.shop_id"
                                   + " LEFT JOIN npc as npc ON npc.idTemplate = m_hop.npc_id"
                                   + " WHERE m_bu.item_id = " + id + " AND npc.name != '' ";
            }
            DataTable t1 = new DataTable();
            ClassConnect.GridView_list_mobs(t1, CommandText); // Создан отдельный файл с класом коннект (обозреватель решений)
            dataGridView_item_shop.DataSource = t1;
            for (int i = 0; i < 6; i++) { dataGridView_item_shop.Columns[i].Visible = false; }// Убирает лишнии столбцы таблицы
            for (int i = 0; i < 2; i++) { dataGridView_item_shop.Columns[i].Visible = true; }// отображает столбцы столбцы таблицы     
            for (int i = 0; i < dataGridView_item_shop.Rows.Count; i++) // Цикл заполнения вклади клан шмот
            {
                try
                {
                    if (dataGridView_item_shop.Rows[i].Cells[4].Value.ToString() == "")
                    { dataGridView_item_shop.Rows[i].Cells[0].Value = "" + dataGridView_item_shop.Rows[i].Cells[3].Value.ToString() + ""; }
                    else
                    { dataGridView_item_shop.Rows[i].Cells[0].Value = "[" + dataGridView_item_shop.Rows[i].Cells[4].Value.ToString() + "] " + dataGridView_item_shop.Rows[i].Cells[3].Value.ToString() + ""; }
                    dataGridView_item_shop.Rows[i].Cells[1].Value = dataGridView_item_shop.Rows[i].Cells[5].Value.ToString();
                }
                catch (Exception l)
                {
                    MessageBox.Show("Ошибка! №8\r\n (" + l.Message + ")", "Предметы дроп", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                dataGridView_item_shop.ClearSelection();
            }
            if (dataGridView_item_shop.Rows.Count > 0)
            {
                button_shop_item.Visible = true;
                button_item_shop.Visible = true;
            }
            else
            {
                button_shop_item.Visible = false;
                button_item_shop.Visible = false;
            }
        }

        private void radioButton_drop_craft(object sender, EventArgs e)
        {
            if (radioButton_rec_drop.Checked) { panel_item_drop.Visible = true; panel_rec_igridient.Visible = false; panel_shop_item.Visible = false; }
            else if (radioButton_rec_craft.Checked) { panel_item_drop.Visible = false; panel_shop_item.Visible = false; panel_rec_igridient.Visible = true; }
        }

        private void radioButton_item_drop_Click(object sender, EventArgs e)
        {
            if (radioButton_item_drop.Checked) { panel_item_drop.Visible = true; panel_rec_igridient.Visible = false; panel_shop_item.Visible = false; }
            else if (radioButton_item_craft.Checked) { panel_item_drop.Visible = false; panel_shop_item.Visible = false; panel_rec_igridient.Visible = true; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            radioButton_rec_drop.Checked = true;
            radioButton_item_drop.Checked = true;
            panel_item_drop.Visible = true; panel_rec_igridient.Visible = false; panel_shop_item.Visible = false;
            dataGridView_items_drop();
        }

        private void button_item_shop_Click(object sender, EventArgs e)
        {
            radioButton_rec_drop.Checked = true;
            radioButton_item_drop.Checked = true;
            panel_item_drop.Visible = false; panel_rec_igridient.Visible = false; panel_shop_item.Visible = true;
            shop_item();// Отображает npc у кого есть в продаже шмотка
        }

        private void dataGridView_item_shop_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView_item_shop.CurrentRow == null)
            { MessageBox.Show("Вы не выбрали моба", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            ClassMob.Info_Id = Convert.ToInt32(dataGridView_item_shop.CurrentRow.Cells[2].Value);
            ClassMob.Info_Name = Convert.ToString(dataGridView_item_shop.CurrentRow.Cells[0].Value);
            int count = 0;
            string CommandText1 = "SELECT COUNT(*) FROM spawnlist WHERE npc_templateid = " + Convert.ToInt32(dataGridView_item_shop.CurrentRow.Cells[2].Value) + "";
            DataTable t3 = new DataTable();
            ClassConnect.GridView_list_mobs(t3, CommandText1);
            DataTableReader dtr = t3.CreateDataReader(); dtr.Read();
            count = Convert.ToInt32(dtr[0]);
            ClassMob.Map_count = count;

            if (count == 0)
            {
                MessageBox.Show("Моба нет на карте", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                mob_map form2 = new mob_map();
                form2.Text = "Имя - (" + ClassMob.Info_Name + ")  количество  точек респа - (" + count + ")";
                form2.ShowDialog();
            }
        }

        private void dataGridView_drop_item_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            tabControl_home.SelectedIndex = 0;
            list_mobs_output(Convert.ToInt32(dataGridView_drop_item.CurrentRow.Cells[5].Value), ""); // Функция вывода мобов в dataGridView_list_mobs вкладка мобы
            tabPage_mobs.Refresh();
            dataGridView_list_mobs_CellMouseClick(sender, e); // дроп и споил с мобов
        }

        private void dataGridView_spoil_item_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            tabControl_home.SelectedIndex = 0;
            list_mobs_output(Convert.ToInt32(dataGridView_spoil_item.CurrentRow.Cells[5].Value), ""); // Функция вывода мобов в dataGridView_list_mobs вкладка мобы
            tabPage_mobs.Refresh();
            dataGridView_list_mobs_CellMouseClick(sender, e); // дроп и споил с мобов
        }

        private void toolStrip_del_item_Click(object sender, EventArgs e)
        {
            DataTable t1 = new DataTable();
            string CommandText = "DELETE FROM item_save WHERE item_id = (" + dataGridView_items.CurrentRow.Cells[6].Value + ")";
            ClassConnect.GridView_list_mobs(t1, CommandText); // Создан отдельный файл с класом коннект (обозреватель решений)
            radioButton_item_10.PerformClick();
        }

        private void toolStrip_del_all_Click(object sender, EventArgs e)
        {
            if (DialogResult.Cancel == MessageBox.Show("Вы действительно хотите удалить все итемы ?", "Удаление итемов",MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {return;}
            DataTable t1 = new DataTable();
            string CommandText = "DELETE FROM item_save";
            ClassConnect.GridView_list_mobs(t1, CommandText); // Создан отдельный файл с класом коннект (обозреватель решений)
            radioButton_item_10.PerformClick();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataTable t1 = new DataTable();
            string CommandText = "INSERT INTO item_save VALUES (" + dataGridView_items.CurrentRow.Cells[6].Value + ")";
            ClassConnect.GridView_list_mobs(t1, CommandText); // Создан отдельный файл с класом коннект (обозреватель решений)
        }

        #endregion

        #region вкладка Инвентарь

        private void inv_head(object sender, MouseEventArgs e) { radioButton_inv_head.Image = Properties.Resources.head; }
        private void inv_chest(object sender, MouseEventArgs e) { radioButton_inv_chest.Image = Properties.Resources.chest; }
        private void inv_legs(object sender, MouseEventArgs e) { radioButton_inv_legs.Image = Properties.Resources.legs; }
        private void inv_gloves(object sender, MouseEventArgs e) { radioButton_inv_gloves.Image = Properties.Resources.gloves; }
        private void inv_feet(object sender, MouseEventArgs e) { radioButton_inv_feet.Image = Properties.Resources.feet; }
        private void inv_back(object sender, MouseEventArgs e) { radioButton_inv_back.Image = Properties.Resources.back; }
        private void inv_underwear(object sender, MouseEventArgs e) { radioButton_inv_underwear.Image = Properties.Resources.underwear; }
        private void inv_waist(object sender, MouseEventArgs e) { radioButton_inv_waist.Image = Properties.Resources.waist; }
        private void inv_deco(object sender, MouseEventArgs e) { radioButton_inv_deco.Image = Properties.Resources.deco1; }
        private void inv_rbracelet(object sender, MouseEventArgs e) { radioButton_inv_rbracelet.Image = Properties.Resources.rbracelet; }
        private void inv_onepiece(object sender, MouseEventArgs e) { radioButton_inv_onepiece.Image = Properties.Resources.onepiece; }
        private void inv_hair(object sender, MouseEventArgs e) { radioButton_inv_hair.Image = Properties.Resources.hair; }
        private void inv_rear(object sender, MouseEventArgs e) { radioButton_inv_rear.Image = Properties.Resources.rear_lear; }
        private void inv_lear(object sender, MouseEventArgs e) { radioButton_inv_lear.Image = Properties.Resources.rear_lear; }
        private void inv_neck(object sender, MouseEventArgs e) { radioButton_inv_neck.Image = Properties.Resources.neck; }
        private void inv_lfinger(object sender, MouseEventArgs e) { radioButton_inv_lfinger.Image = Properties.Resources.rfinger_lf; }
        private void inv_rfinger(object sender, MouseEventArgs e) { radioButton_inv_rfinger.Image = Properties.Resources.rfinger_lf; }
        private void inv_lhand(object sender, MouseEventArgs e) { radioButton_inv_lhand.Image = Properties.Resources.lhand; }
        private void inv_grand_rb(object sender, MouseEventArgs e) { radioButton_nec_grand_rb.Image = Properties.Resources.nec_grand_rb; }
       
        private void inv_ng(object sender, MouseEventArgs e) { radioButton_inv_ng.Image = Properties.Resources.ng; }
        private void inv_d(object sender, MouseEventArgs e) { radioButton_inv_d.Image = Properties.Resources.d; }
        private void inv_c(object sender, MouseEventArgs e) { radioButton_inv_c.Image = Properties.Resources.c; }
        private void inv_b(object sender, MouseEventArgs e) { radioButton_inv_b.Image = Properties.Resources.b; }
        private void inv_a(object sender, MouseEventArgs e) { radioButton_inv_a.Image = Properties.Resources.a; }
        private void inv_s(object sender, MouseEventArgs e) { radioButton_inv_s.Image = Properties.Resources.s; }
        private void inv_s80(object sender, MouseEventArgs e) { radioButton_inv_80.Image = Properties.Resources.s80; }
        private void inv_s84(object sender, MouseEventArgs e) { radioButton_inv_84.Image = Properties.Resources.s84; }

        private void inv_sword(object sender, MouseEventArgs e) { radioButton_inv_sword.Image = Properties.Resources.inv_sword;}
        private void inv_blunt(object sender, MouseEventArgs e) { radioButton_inv_blunt.Image = Properties.Resources.inv_blunt;  }
        private void inv_dualfist(object sender, MouseEventArgs e) { radioButton_inv_dualfist.Image = Properties.Resources.inv_dualfist; }
        private void inv_dual(object sender, MouseEventArgs e) { radioButton_inv_dual.Image = Properties.Resources.inv_dual; }
        private void inv_bigblunt(object sender, MouseEventArgs e) { radioButton_inv_bigblunt.Image = Properties.Resources.inv_bigblunt; }
        private void inv_pole(object sender, MouseEventArgs e) { radioButton_inv_pole.Image = Properties.Resources.inv_pole;}
        private void inv_dagger(object sender, MouseEventArgs e) { radioButton_inv_dagger.Image = Properties.Resources.inv_dagger;}
        private void inv_bigsword(object sender, MouseEventArgs e) { radioButton_inv_bigsword.Image = Properties.Resources.inv_bigsword; }
        private void inv_bow(object sender, MouseEventArgs e) { radioButton_inv_bow.Image = Properties.Resources.inv_bow;  }
        private void inv_crossbow(object sender, MouseEventArgs e) { radioButton_inv_crossbow.Image = Properties.Resources.inv_crossbow; }
        private void inv_rapier(object sender, MouseEventArgs e) { radioButton_inv_rapier.Image = Properties.Resources.inv_rapier; }
        private void inv_ancientswo(object sender, MouseEventArgs e) { radioButton_inv_ancientswo.Image = Properties.Resources.inv_ancientswo; }
        private void inf_spilbook(object sender, MouseEventArgs e) { radioButton_inf_spilbook.Image = Properties.Resources.spellbook; }
        private void inv_fish(object sender, MouseEventArgs e) { radioButton_inv_fish.Image = Properties.Resources.fishing; }

        private void OffGroupBoxSelectionInv(object sender, EventArgs e) // выбор картинки
        {
            if (radioButton_inv_head.Focused == false & radioButton_inv_head.Checked == false) radioButton_inv_head.Image = Properties.Resources.head50;
            if (radioButton_inv_chest.Focused == false & radioButton_inv_chest.Checked == false) radioButton_inv_chest.Image = Properties.Resources.chest50;
            if (radioButton_inv_legs.Focused == false & radioButton_inv_legs.Checked == false) radioButton_inv_legs.Image = Properties.Resources.legs50;
            if (radioButton_inv_gloves.Focused == false & radioButton_inv_gloves.Checked == false) radioButton_inv_gloves.Image = Properties.Resources.gloves50;
            if (radioButton_inv_feet.Focused == false & radioButton_inv_feet.Checked == false) radioButton_inv_feet.Image = Properties.Resources.feet50;
            if (radioButton_inv_back.Focused == false & radioButton_inv_back.Checked == false) radioButton_inv_back.Image = Properties.Resources.back50;
            if (radioButton_inv_underwear.Focused == false & radioButton_inv_underwear.Checked == false) radioButton_inv_underwear.Image = Properties.Resources.underwear50;
            if (radioButton_inv_waist.Focused == false & radioButton_inv_waist.Checked == false) radioButton_inv_waist.Image = Properties.Resources.waist50;
            if (radioButton_inv_deco.Focused == false & radioButton_inv_deco.Checked == false) radioButton_inv_deco.Image = Properties.Resources.deco150;
            if (radioButton_inv_rbracelet.Focused == false & radioButton_inv_rbracelet.Checked == false) radioButton_inv_rbracelet.Image = Properties.Resources.rbracelet50;
            if (radioButton_inv_onepiece.Focused == false & radioButton_inv_onepiece.Checked == false) radioButton_inv_onepiece.Image = Properties.Resources.onepiece50;
            if (radioButton_inv_hair.Focused == false & radioButton_inv_hair.Checked == false) radioButton_inv_hair.Image = Properties.Resources.hair50;
            if (radioButton_inv_rear.Focused == false & radioButton_inv_rear.Checked == false) radioButton_inv_rear.Image = Properties.Resources.rear_lear50;
            if (radioButton_inv_lear.Focused == false & radioButton_inv_lear.Checked == false) radioButton_inv_lear.Image = Properties.Resources.rear_lear50;
            if (radioButton_inv_neck.Focused == false & radioButton_inv_neck.Checked == false) radioButton_inv_neck.Image = Properties.Resources.neck50;
            if (radioButton_inv_lfinger.Focused == false & radioButton_inv_lfinger.Checked == false) radioButton_inv_lfinger.Image = Properties.Resources.rfinger_lf50;
            if (radioButton_inv_rfinger.Focused == false & radioButton_inv_rfinger.Checked == false) radioButton_inv_rfinger.Image = Properties.Resources.rfinger_lf50;
            if (radioButton_inv_lhand.Focused == false & radioButton_inv_lhand.Checked == false) radioButton_inv_lhand.Image = Properties.Resources.lhand50;
            if (radioButton_nec_grand_rb.Focused == false & radioButton_nec_grand_rb.Checked == false) radioButton_nec_grand_rb.Image = Properties.Resources.nec_grand_rb50;

            if (radioButton_inv_ng.Focused == false & radioButton_inv_ng.Checked == false) radioButton_inv_ng.Image = Properties.Resources.ng50;
            if (radioButton_inv_d.Focused == false & radioButton_inv_d.Checked == false) radioButton_inv_d.Image = Properties.Resources.d50;
            if (radioButton_inv_c.Focused == false & radioButton_inv_c.Checked == false) radioButton_inv_c.Image = Properties.Resources.c50;
            if (radioButton_inv_b.Focused == false & radioButton_inv_b.Checked == false) radioButton_inv_b.Image = Properties.Resources.b50;
            if (radioButton_inv_a.Focused == false & radioButton_inv_a.Checked == false) radioButton_inv_a.Image = Properties.Resources.a50;
            if (radioButton_inv_s.Focused == false & radioButton_inv_s.Checked == false) radioButton_inv_s.Image = Properties.Resources.s50;
            if (radioButton_inv_80.Focused == false & radioButton_inv_80.Checked == false) radioButton_inv_80.Image = Properties.Resources.s8050;
            if (radioButton_inv_84.Focused == false & radioButton_inv_84.Checked == false) radioButton_inv_84.Image = Properties.Resources.s8450;

            if (radioButton_inv_sword.Focused == false & radioButton_inv_sword.Checked == false) radioButton_inv_sword.Image = Properties.Resources.inv_sword50;
            if (radioButton_inv_blunt.Focused == false & radioButton_inv_blunt.Checked == false) radioButton_inv_blunt.Image = Properties.Resources.inv_blunt50;
            if (radioButton_inv_dualfist.Focused == false & radioButton_inv_dualfist.Checked == false) radioButton_inv_dualfist.Image = Properties.Resources.inv_dualfist50;
            if (radioButton_inv_dual.Focused == false & radioButton_inv_dual.Checked == false) radioButton_inv_dual.Image = Properties.Resources.inv_dual50;
            if (radioButton_inv_bigblunt.Focused == false & radioButton_inv_bigblunt.Checked == false) radioButton_inv_bigblunt.Image = Properties.Resources.inv_bigblunt50;
            if (radioButton_inv_pole.Focused == false & radioButton_inv_pole.Checked == false) radioButton_inv_pole.Image = Properties.Resources.inv_pole50;
            if (radioButton_inv_dagger.Focused == false & radioButton_inv_dagger.Checked == false) radioButton_inv_dagger.Image = Properties.Resources.inv_dagger50;
            if (radioButton_inv_bigsword.Focused == false & radioButton_inv_bigsword.Checked == false) radioButton_inv_bigsword.Image = Properties.Resources.inv_bigsword50;
            if (radioButton_inv_bow.Focused == false & radioButton_inv_bow.Checked == false) radioButton_inv_bow.Image = Properties.Resources.inv_bow50;
            if (radioButton_inv_crossbow.Focused == false & radioButton_inv_crossbow.Checked == false) radioButton_inv_crossbow.Image = Properties.Resources.inv_crossbow50;
            if (radioButton_inv_rapier.Focused == false & radioButton_inv_rapier.Checked == false) radioButton_inv_rapier.Image = Properties.Resources.inv_rapier50;
            if (radioButton_inv_ancientswo.Focused == false & radioButton_inv_ancientswo.Checked == false) radioButton_inv_ancientswo.Image = Properties.Resources.inv_ancientswo50;
            if (radioButton_inf_spilbook.Focused == false & radioButton_inf_spilbook.Checked == false) radioButton_inf_spilbook.Image = Properties.Resources.spellbook50;
            if (radioButton_inv_fish.Focused == false & radioButton_inv_fish.Checked == false) radioButton_inv_fish.Image = Properties.Resources.fishing50;

        }

        private void treeView_inventar(object sender, EventArgs e)
        {
            treeView_inv.Nodes.Clear();
            if (Convert.ToInt32(dataGridView_inv.CurrentRow.Cells[0].Value) == 0) { return; }
            ClassConnect tt = new ClassConnect();
            SQLiteConnection myConnection = new SQLiteConnection(tt.ToString());
            try
            {
                myConnection.Open();
            }
            catch (Exception l)
            {
                MessageBox.Show("Ошибка!\r\n (" + l.Message + ")", "Подключение к базе данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SQLiteCommand myCommand = myConnection.CreateCommand();
            string successRate = "";
            if (radioButton_inv_100.Checked) { successRate = "AND rec.successRate = 100";  } else { successRate = "AND rec.successRate < 100"; }
            if (radioButton_lang_ru.Checked)
            {
                myCommand.CommandText = "SELECT rec.product_id, rec.ingredient_id, rec.ingredient_count, etc.icon, itd.name_ru, rec.craftLevel, rec.type FROM etcitem as etc"
                      + " LEFT JOIN recipe as rec ON rec.product_id = etc.item_id"
                      + " LEFT JOIN item_desc as itd ON itd.id = etc.item_id"
                      + " WHERE rec.product_id = " + dataGridView_inv.CurrentRow.Cells[0].Value + "  " + successRate + "";
            }
            else
            {
                myCommand.CommandText = "SELECT rec.product_id, rec.ingredient_id, rec.ingredient_count, etc.icon, etc.name, rec.craftLevel, rec.type FROM etcitem as etc"
                      + " LEFT JOIN recipe as rec ON rec.product_id = etc.item_id"
                      + " WHERE rec.product_id = " + dataGridView_inv.CurrentRow.Cells[0].Value + "  " + successRate + "";
            }
            SQLiteDataReader dataRead0 = myCommand.ExecuteReader();
            dataRead0.Read();
            if (dataRead0.HasRows == false) { dataRead0.Close(); myConnection.Close(); return; }
            string[] ingredient_id = Regex.Split(dataRead0["ingredient_id"].ToString(), @"	");
            string[] ingredient_count = Regex.Split(dataRead0["ingredient_count"].ToString(), @"	");
            dataRead0.Close();
            treeView_inv.BeginUpdate();
            ImageList myImageList = new ImageList();
            myImageList.ImageSize = new Size(32, 32);
            myImageList.ColorDepth = ColorDepth.Depth24Bit;
            int z = 0;

            for (int i = 0; i < ingredient_id.Length; i++)
            {
                if (radioButton_lang_ru.Checked)
                {
                    myCommand.CommandText = "SELECT rec.product_id, rec.ingredient_id, rec.ingredient_count, etc.icon, itd.name_ru FROM etcitem as etc"
                                          + " LEFT JOIN item_desc as itd ON itd.id = etc.item_id "
                                          + " LEFT JOIN recipe as rec ON rec.product_id = etc.item_id"
                                          + " WHERE etc.item_id = " + ingredient_id[i].ToString() + " ";
                }
                else
                {
                    myCommand.CommandText = "SELECT rec.product_id, rec.ingredient_id, rec.ingredient_count, etc.icon, etc.name FROM etcitem as etc"
                                          + " LEFT JOIN recipe as rec ON rec.product_id = etc.item_id"
                                          + " WHERE etc.item_id = " + ingredient_id[i].ToString() + " ";
                }
                SQLiteDataReader dataRead = myCommand.ExecuteReader();
                dataRead.Read();
                myImageList.Images.Add(Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dataRead["icon"].ToString() + ".png"));
                treeView_inv.ImageList = myImageList;
                TreeNode rootNode;
                if (radioButton_lang_ru.Checked)
                {
                    rootNode = new TreeNode(ingredient_count[i].ToString() + " - " + dataRead["name_ru"].ToString());
                }
                else
                {
                    rootNode = new TreeNode(ingredient_count[i].ToString() + " - " + dataRead["name"].ToString());
                }
                rootNode.ImageIndex = z;
                rootNode.SelectedImageIndex = z;
                rootNode.Name = ingredient_id[i].ToString();
                z++;
                treeView_inv.Nodes.Add(rootNode);

                if (dataRead["ingredient_id"].ToString() != "")
                {
                    string[] ingredient_id2 = Regex.Split(dataRead["ingredient_id"].ToString(), @"	");
                    string[] ingredient_count2 = Regex.Split(dataRead["ingredient_count"].ToString(), @"	");
                    dataRead.Close();
                    for (int i2 = 0; i2 < ingredient_id2.Length; i2++)
                    {
                        if (radioButton_lang_ru.Checked)
                        {
                            myCommand.CommandText = "SELECT rec.product_id, rec.ingredient_id, rec.ingredient_count, etc.icon, itd.name_ru FROM etcitem as etc"
                                                  + " LEFT JOIN item_desc as itd ON itd.id = etc.item_id "
                                                  + " LEFT JOIN recipe as rec ON rec.product_id = etc.item_id"
                                                  + " WHERE etc.item_id = " + ingredient_id2[i2].ToString() + " ";
                        }
                        else
                        {
                            myCommand.CommandText = "SELECT rec.product_id, rec.ingredient_id, rec.ingredient_count, etc.icon, etc.name FROM etcitem as etc"
                                                  + " LEFT JOIN recipe as rec ON rec.product_id = etc.item_id"
                                                  + " WHERE etc.item_id = " + ingredient_id2[i2].ToString() + " ";
                        }
                        SQLiteDataReader dataRead2 = myCommand.ExecuteReader();
                        dataRead2.Read();
                        myImageList.Images.Add(Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dataRead2["icon"].ToString() + ".png"));
                        TreeNode rootNode2;
                        if (radioButton_lang_ru.Checked)
                        {
                            rootNode2 = new TreeNode(ingredient_count2[i2].ToString() + " - " + dataRead2["name_ru"].ToString());
                        }
                        else
                        {
                            rootNode2 = new TreeNode(ingredient_count2[i2].ToString() + " - " + dataRead2["name"].ToString());
                        }
                        rootNode2.ImageIndex = z;
                        rootNode2.SelectedImageIndex = z;
                        z++;
                        treeView_inv.Nodes[i].Nodes.Add(rootNode2);
                        if (dataRead2["ingredient_id"].ToString() != "")
                        {
                            string[] ingredient_id3 = Regex.Split(dataRead2["ingredient_id"].ToString(), @"	");
                            string[] ingredient_count3 = Regex.Split(dataRead2["ingredient_count"].ToString(), @"	");
                            dataRead2.Close();
                            for (int i3 = 0; i3 < ingredient_id3.Length; i3++)
                            {
                                if (radioButton_lang_ru.Checked)
                                {
                                    myCommand.CommandText = "SELECT rec.product_id, rec.ingredient_id, etc.icon, rec.ingredient_count, itd.name_ru FROM etcitem as etc"
                                                          + " LEFT JOIN item_desc as itd ON itd.id = etc.item_id "
                                                          + " LEFT JOIN recipe as rec ON rec.product_id = etc.item_id"
                                                          + " WHERE etc.item_id = " + ingredient_id3[i3].ToString() + " ";
                                }
                                else
                                {
                                    myCommand.CommandText = "SELECT rec.product_id, rec.ingredient_id, etc.icon, rec.ingredient_count, etc.name FROM etcitem as etc"
                                                          + " LEFT JOIN recipe as rec ON rec.product_id = etc.item_id"
                                                          + " WHERE etc.item_id = " + ingredient_id3[i3].ToString() + " ";
                                }
                                SQLiteDataReader dataRead3 = myCommand.ExecuteReader();
                                dataRead3.Read();
                                myImageList.Images.Add(Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dataRead3["icon"].ToString() + ".png"));
                                TreeNode rootNode3;
                                if (radioButton_lang_ru.Checked)
                                {
                                    rootNode3 = new TreeNode(ingredient_count3[i3].ToString() + " - " + dataRead3["name_ru"].ToString());
                                }
                                else
                                {
                                    rootNode3 = new TreeNode(ingredient_count3[i3].ToString() + " - " + dataRead3["name"].ToString());
                                }
                                rootNode3.ImageIndex = z;
                                rootNode3.SelectedImageIndex = z;
                                z++;
                                treeView_inv.Nodes[i].Nodes[i2].Nodes.Add(rootNode3);
                                if (dataRead3["ingredient_id"].ToString() != "")
                                {
                                    string[] ingredient_id4 = Regex.Split(dataRead3["ingredient_id"].ToString(), @"	");
                                    string[] ingredient_count4 = Regex.Split(dataRead3["ingredient_count"].ToString(), @"	");
                                    dataRead3.Close();
                                    for (int i4 = 0; i4 < ingredient_id4.Length; i4++)
                                    {
                                        if (radioButton_lang_ru.Checked)
                                        {
                                            myCommand.CommandText = "SELECT rec.product_id, rec.ingredient_id, etc.icon, rec.ingredient_count, itd.name_ru FROM etcitem as etc"
                                                                  + " LEFT JOIN item_desc as itd ON itd.id = etc.item_id "
                                                                  + " LEFT JOIN recipe as rec ON rec.product_id = etc.item_id"
                                                                  + " WHERE etc.item_id = " + ingredient_id4[i4].ToString() + " ";
                                        }
                                        else
                                        {
                                            myCommand.CommandText = "SELECT rec.product_id, rec.ingredient_id, etc.icon, rec.ingredient_count, etc.name FROM etcitem as etc"
                                                                  + " LEFT JOIN recipe as rec ON rec.product_id = etc.item_id"
                                                                  + " WHERE etc.item_id = " + ingredient_id4[i4].ToString() + " ";
                                        }
                                        SQLiteDataReader dataRead4 = myCommand.ExecuteReader();
                                        dataRead4.Read();
                                        myImageList.Images.Add(Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dataRead4["icon"].ToString() + ".png"));
                                        TreeNode rootNode4;
                                        if (radioButton_lang_ru.Checked)
                                        {
                                            rootNode4 = new TreeNode(ingredient_count4[i4].ToString() + " - " + dataRead4["name_ru"].ToString());
                                        }
                                        else
                                        {
                                            rootNode4 = new TreeNode(ingredient_count4[i4].ToString() + " - " + dataRead4["name"].ToString());
                                        }
                                        rootNode4.ImageIndex = z;
                                        rootNode4.SelectedImageIndex = z;
                                        z++;
                                        treeView_inv.Nodes[i].Nodes[i2].Nodes[i3].Nodes.Add(rootNode4);
                                        if (dataRead4["ingredient_id"].ToString() != "")
                                        {
                                            string[] ingredient_id5 = Regex.Split(dataRead4["ingredient_id"].ToString(), @"	");
                                            string[] ingredient_count5 = Regex.Split(dataRead4["ingredient_count"].ToString(), @"	");
                                            dataRead4.Close();
                                            for (int i5 = 0; i5 < ingredient_id5.Length; i5++)
                                            {
                                                if (radioButton_lang_ru.Checked)
                                                {
                                                    myCommand.CommandText = "SELECT rec.product_id, rec.ingredient_id, etc.icon, rec.ingredient_count, itd.name_ru FROM etcitem as etc"
                                                                          + " LEFT JOIN item_desc as itd ON itd.id = etc.item_id "
                                                                          + " LEFT JOIN recipe as rec ON rec.product_id = etc.item_id"
                                                                          + " WHERE etc.item_id = " + ingredient_id5[i5].ToString() + " ";
                                                }
                                                else
                                                {
                                                    myCommand.CommandText = "SELECT rec.product_id, rec.ingredient_id, etc.icon, rec.ingredient_count, etc.name FROM etcitem as etc"
                                                                          + " LEFT JOIN recipe as rec ON rec.product_id = etc.item_id"
                                                                          + " WHERE etc.item_id = " + ingredient_id5[i5].ToString() + " ";
                                                }
                                                SQLiteDataReader dataRead5 = myCommand.ExecuteReader();
                                                dataRead5.Read();
                                                myImageList.Images.Add(Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dataRead5["icon"].ToString() + ".png"));
                                                TreeNode rootNode5;
                                                if (radioButton_lang_ru.Checked)
                                                {
                                                    rootNode5 = new TreeNode(ingredient_count5[i5].ToString() + " - " + dataRead5["name_ru"].ToString());
                                                }
                                                else
                                                {
                                                    rootNode5 = new TreeNode(ingredient_count5[i5].ToString() + " - " + dataRead5["name"].ToString());
                                                }
                                                rootNode5.ImageIndex = z;
                                                rootNode5.SelectedImageIndex = z;
                                                z++;
                                                treeView_inv.Nodes[i].Nodes[i2].Nodes[i3].Nodes[i4].Nodes.Add(rootNode5);
                                                dataRead5.Close();
                                            }
                                        }
                                        dataRead4.Close();
                                    }
                                }
                                dataRead3.Close();
                            }
                        }
                        dataRead2.Close();
                    }
                }
                dataRead.Close();
            }
            treeView_inv.EndUpdate();
            myConnection.Close();
        }

        private void dataGridView_inv_npc(object sender, EventArgs e)
        {
            string CommandText = "";
            if (radioButton_lang_ru.Checked)
            {
                CommandText = "SELECT npc.name_ru, npc.idTemplate, min, max, chance, npc.level, npc.title_ru FROM droplist"
                       + " LEFT JOIN npc as npc ON npc.idTemplate = mobId"
                       + " WHERE itemId = " + dataGridView_inv.CurrentRow.Cells[0].Value + " ORDER BY chance DESC";
            }
            else
            {
                CommandText = "SELECT npc.name, npc.idTemplate, min, max, chance, npc.level, npc.title FROM droplist"
                       + " LEFT JOIN npc as npc ON npc.idTemplate = mobId"
                       + " WHERE itemId = " + dataGridView_inv.CurrentRow.Cells[0].Value + " ORDER BY chance DESC";
            }

            DataTable t1 = new DataTable();
            ClassConnect.GridView_list_mobs(t1, CommandText); // Создан отдельный файл с класом коннект (обозреватель решений)
            dataGridView_inventar_npc.DataSource = t1;
            for (int i = 0; i < 11; i++) { dataGridView_inventar_npc.Columns[i].Visible = false; }// Убирает лишнии столбцы таблицы
            for (int i = 0; i < 4; i++) { dataGridView_inventar_npc.Columns[i].Visible = true; }// отображает столбцы столбцы таблицы     
            for (int i = 0; i < dataGridView_inventar_npc.Rows.Count; i++) // Цикл заполнения вклади клан шмот
            {
                try
                {
                    dataGridView_inventar_npc.Rows[i].Cells[0].Value = dataGridView_inventar_npc.Rows[i].Cells[9].Value.ToString();
                    if (dataGridView_inventar_npc.Rows[i].Cells[10].Value.ToString() == "")
                        dataGridView_inventar_npc.Rows[i].Cells[1].Value = dataGridView_inventar_npc.Rows[i].Cells[4].Value.ToString();
                    else
                        dataGridView_inventar_npc.Rows[i].Cells[1].Value = "[" + dataGridView_inventar_npc.Rows[i].Cells[10].Value.ToString() + "] " + dataGridView_inventar_npc.Rows[i].Cells[4].Value.ToString();
                    dataGridView_inventar_npc.Rows[i].Cells[2].Value = "" + dataGridView_inventar_npc.Rows[i].Cells[6].Value.ToString() + "-" + dataGridView_inventar_npc.Rows[i].Cells[7].Value.ToString() + "";
                    double procent = (Convert.ToDouble(Regex.Replace(Convert.ToString(dataGridView_inventar_npc.Rows[i].Cells[8].Value), @"\.", ",")) / 1000000 * Convert.ToDouble(numericUpDown_seting_drop.Value)) * 100;
                    if (procent > 100) { procent = 100; }
                    dataGridView_inventar_npc.Rows[i].Cells[3].Value = Math.Round(procent, 5) + " %";
                }
                catch (Exception l)
                {
                    MessageBox.Show("Ошибка! №31\r\n (" + l.Message + ")", "Инвентарь дроп-споил", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
                dataGridView_inventar_npc.ClearSelection();
        }

        private void dataGridView_inv_shop(object sender, EventArgs e)
        {
            string CommandText = "";
            if (radioButton_lang_ru.Checked)
            {
                CommandText = "SELECT npc.idTemplate, npc.name_ru, npc.title_ru, m_bu.price FROM merchant_buylists as m_bu"
                                   + " LEFT JOIN merchant_shopids as m_hop ON m_hop.shop_id = m_bu.shop_id"
                                   + " LEFT JOIN npc as npc ON npc.idTemplate = m_hop.npc_id"
                                   + " WHERE m_bu.item_id = " + dataGridView_inv.CurrentRow.Cells[0].Value + " AND npc.name != '' ";
            }
            else
            {
                CommandText = "SELECT npc.idTemplate, npc.name, npc.title, m_bu.price FROM merchant_buylists as m_bu"
                                   + " LEFT JOIN merchant_shopids as m_hop ON m_hop.shop_id = m_bu.shop_id"
                                   + " LEFT JOIN npc as npc ON npc.idTemplate = m_hop.npc_id"
                                   + " WHERE m_bu.item_id = " + dataGridView_inv.CurrentRow.Cells[0].Value + " AND npc.name != '' ";
            }
            DataTable t1 = new DataTable();
            ClassConnect.GridView_list_mobs(t1, CommandText); // Создан отдельный файл с класом коннект (обозреватель решений)
            dataGridView_invetar_shop.DataSource = t1;
            for (int i = 0; i < 6; i++) { dataGridView_invetar_shop.Columns[i].Visible = false; }// Убирает лишнии столбцы таблицы
            for (int i = 0; i < 2; i++) { dataGridView_invetar_shop.Columns[i].Visible = true; }// отображает столбцы столбцы таблицы     
            for (int i = 0; i < dataGridView_invetar_shop.Rows.Count; i++) // Цикл заполнения вклади клан шмот
            {
                try
                {
                    if (dataGridView_invetar_shop.Rows[i].Cells[4].Value.ToString() == "")
                    { dataGridView_invetar_shop.Rows[i].Cells[0].Value = "" + dataGridView_invetar_shop.Rows[i].Cells[3].Value.ToString() + ""; }
                    else
                    { dataGridView_invetar_shop.Rows[i].Cells[0].Value = "[" + dataGridView_invetar_shop.Rows[i].Cells[4].Value.ToString() + "] " + dataGridView_invetar_shop.Rows[i].Cells[3].Value.ToString() + ""; }
                    dataGridView_invetar_shop.Rows[i].Cells[1].Value = dataGridView_invetar_shop.Rows[i].Cells[5].Value.ToString();
                }
                catch (Exception l)
                {
                    MessageBox.Show("Ошибка! №8\r\n (" + l.Message + ")", "Предметы дроп", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void radioButton_inv_Click(object sender, EventArgs e)
        {
            dataGridView_invetar_shop.DataSource = null;
            dataGridView_inventar_npc.DataSource = null;
            treeView_inv.Nodes.Clear();
            string inv_arm_wep = "";
            string cristal_tip = "";
            panel_inv_cri.Enabled = true;
            if (radioButton_inv_head.Checked) { inv_arm_wep = " etc.type = 3 AND etc.bodypart = 'head' "; }
            else if (radioButton_inv_chest.Checked) { inv_arm_wep = " etc.type = 3 AND etc.bodypart = 'chest' "; }
            else if (radioButton_inv_legs.Checked) { inv_arm_wep = " etc.type = 3 AND etc.bodypart = 'legs' "; }
            else if (radioButton_inv_gloves.Checked) { inv_arm_wep = " etc.type = 3 AND etc.bodypart = 'gloves' "; }
            else if (radioButton_inv_feet.Checked) { inv_arm_wep = " etc.type = 3 AND etc.bodypart = 'feet' "; }
            else if (radioButton_inv_back.Checked) { inv_arm_wep = " etc.type = 3 AND etc.bodypart = 'back' "; }
            else if (radioButton_inv_underwear.Checked) { inv_arm_wep = " etc.type = 3 AND etc.bodypart = 'underwear' "; }
            else if (radioButton_inv_onepiece.Checked) { inv_arm_wep = " etc.type = 3 AND etc.bodypart = 'onepiece' "; }
            else if (radioButton_inv_hair.Checked) { inv_arm_wep = " etc.bodypart LIKE '%hair%' ";  }
            else if (radioButton_inv_rear.Checked) { inv_arm_wep = " etc.type = 3 AND etc.bodypart = 'rear;lear' "; }
            else if (radioButton_inv_lear.Checked) { inv_arm_wep = " etc.type = 3 AND etc.bodypart = 'rear;lear' "; }
            else if (radioButton_inv_neck.Checked) { inv_arm_wep = " etc.type = 3 AND etc.bodypart = 'neck' "; }
            else if (radioButton_inv_lfinger.Checked) { inv_arm_wep = " etc.type = 3 AND etc.bodypart = 'rfinger;lf' "; }
            else if (radioButton_inv_rfinger.Checked) { inv_arm_wep = " etc.type = 3 AND etc.bodypart = 'rfinger;lf' "; }
            else if (radioButton_inv_lhand.Checked) { inv_arm_wep = " etc.type = 3 AND etc.bodypart = 'lhand' "; }
            else if (radioButton_inv_rbracelet.Checked) { inv_arm_wep = " etc.type = 3 AND etc.bodypart = 'rbracelet' "; }
            else if (radioButton_inv_deco.Checked) { inv_arm_wep = "etc.bodypart = 'deco1' ";  }
            else if (radioButton_inv_waist.Checked) { inv_arm_wep = " etc.type = 3 AND etc.bodypart = 'waist' "; }
            else if (radioButton_nec_grand_rb.Checked) { inv_arm_wep = " etc.item_id IN ( '6656', '6657', '6658', '22173', '6659', '21712', '22175', '6660', '6660', '22174', '6662', '8191', '16025', '16026',  '10314' )"; }

            pictureBox_inv_weapon.Image = Properties.Resources.inv_weapon;
            if (radioButton_inv_sword.Checked) { inv_arm_wep = " etc.weapon_type = 'sword' "; pictureBox_inv_weapon.Image = Properties.Resources.inv_sword; }
            else if (radioButton_inv_blunt.Checked) { inv_arm_wep = " etc.weapon_type = 'blunt' "; pictureBox_inv_weapon.Image = Properties.Resources.inv_blunt; }
            else if (radioButton_inv_dualfist.Checked) { inv_arm_wep = " etc.weapon_type = 'dualfist' "; pictureBox_inv_weapon.Image = Properties.Resources.inv_dualfist; }
            else if (radioButton_inv_dual.Checked) { inv_arm_wep = " etc.weapon_type = 'dual' "; pictureBox_inv_weapon.Image = Properties.Resources.inv_dual; }
            else if (radioButton_inv_bigblunt.Checked) { inv_arm_wep = " etc.weapon_type = 'bigblunt' "; pictureBox_inv_weapon.Image = Properties.Resources.inv_bigblunt; }
            else if (radioButton_inv_pole.Checked) { inv_arm_wep = " etc.weapon_type = 'pole' "; pictureBox_inv_weapon.Image = Properties.Resources.inv_pole; }
            else if (radioButton_inv_dagger.Checked) { inv_arm_wep = " etc.weapon_type IN ( 'dagger', 'dualdagger') "; pictureBox_inv_weapon.Image = Properties.Resources.inv_dagger; }
            else if (radioButton_inv_bigsword.Checked) { inv_arm_wep = " etc.weapon_type = 'bigsword' "; pictureBox_inv_weapon.Image = Properties.Resources.inv_bigsword; }
            else if (radioButton_inv_bow.Checked) { inv_arm_wep = " etc.weapon_type = 'bow' "; pictureBox_inv_weapon.Image = Properties.Resources.inv_bow; }
            else if (radioButton_inv_crossbow.Checked) { inv_arm_wep = " etc.weapon_type = 'crossbow' "; pictureBox_inv_weapon.Image = Properties.Resources.inv_crossbow; }
            else if (radioButton_inv_rapier.Checked) { inv_arm_wep = " etc.weapon_type = 'rapier' "; pictureBox_inv_weapon.Image = Properties.Resources.inv_rapier; }
            else if (radioButton_inv_ancientswo.Checked) { inv_arm_wep = " etc.weapon_type = 'ancientswo' "; pictureBox_inv_weapon.Image = Properties.Resources.inv_ancientswo; }
            else if (radioButton_inf_spilbook.Checked) { inv_arm_wep = " etc.weapon_type = 'etc' "; pictureBox_inv_weapon.Image = Properties.Resources.spellbook; }
            else if (radioButton_inv_fish.Checked) { inv_arm_wep = " etc.weapon_type IN ( 'flag', 'ownthing', 'fist', 'fishingrod') "; pictureBox_inv_weapon.Image = Properties.Resources.fishing; }

            if (radioButton_inv_deco.Checked == false & radioButton_inv_hair.Checked == false & radioButton_nec_grand_rb.Checked == false & radioButton_inv_fish.Checked == false)
            {
                if (radioButton_inv_ng.Checked) { cristal_tip = " AND etc.crystal_type =  'none' "; }
                else if (radioButton_inv_d.Checked) { cristal_tip = " AND etc.crystal_type =  'd' "; }
                else if (radioButton_inv_c.Checked) { cristal_tip = " AND etc.crystal_type =  'c' "; }
                else if (radioButton_inv_b.Checked) { cristal_tip = " AND etc.crystal_type =  'b' "; }
                else if (radioButton_inv_a.Checked) { cristal_tip = " AND etc.crystal_type =  'a' "; }
                else if (radioButton_inv_s.Checked) { cristal_tip = " AND etc.crystal_type =  's' "; }
                else if (radioButton_inv_80.Checked) { cristal_tip = " AND etc.crystal_type =  's80' "; }
                else if (radioButton_inv_84.Checked) { cristal_tip = " AND etc.crystal_type =  's84' "; }
                panel_inv_cri.Enabled = true;
            }
            else
            {
                panel_inv_cri.Enabled = false;
            }

            //string CommandText = "SELECT * FROM etcitem as etc"
            //                   + " LEFT JOIN item_desc as it_ru ON it_ru.id = etc.item_id"
            //                   + " LEFT JOIN recipe as rec ON rec.product_id = etc.item_id"
            //                   + " WHERE " + inv_arm_wep + "" + cristal_tip + "";

            string CommandText = "SELECT * FROM item_desc as item"
                                + " LEFT JOIN etcitem as etc ON etc.item_id = item.id "
                                + " LEFT JOIN recipe as rec ON rec.product_id = item.id"
                                + " WHERE " + inv_arm_wep + "" + cristal_tip + " GROUP BY etc.item_id ORDER BY etc.price DESC";
            
            DataTable t1 = new DataTable();
            ClassConnect.GridView_list_mobs(t1, CommandText); // Создан отдельный файл с класом коннект (обозреватель решений)
           
            dataGridView_inv.Columns.Clear();
            dataGridView_inv.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_inv.Columns[0].Visible = false;
            dataGridView_inv.Columns.Add(new DataGridViewImageColumn());
            dataGridView_inv.Columns[1].HeaderText = "Вид";
            dataGridView_inv.Columns[1].Width = 40;
            dataGridView_inv.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_inv.Columns[2].HeaderText = "Имя";
            dataGridView_inv.Columns[2].ReadOnly = true;
            dataGridView_inv.Columns[2].Width = 200;
            if (radioButton_inv_deco.Checked == false & radioButton_inv_hair.Checked == false )
            {
                dataGridView_inv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (radioButton_inv_deco.Checked)
            {
                dataGridView_inv.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridView_inv.Columns[3].HeaderText = "Описание";
                dataGridView_inv.Columns[3].ReadOnly = true;
                dataGridView_inv.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            else if (radioButton_inv_hair.Checked)
            {
                dataGridView_inv.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridView_inv.Columns[3].HeaderText = "Эффект";
                dataGridView_inv.Columns[3].ReadOnly = true;
                dataGridView_inv.Columns[3].Width = 120;
                dataGridView_inv.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridView_inv.Columns[4].HeaderText = "Описание";
                dataGridView_inv.Columns[4].ReadOnly = true;
                dataGridView_inv.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            else if (radioButton_inv_sword.Checked | radioButton_inv_blunt.Checked | radioButton_inv_dualfist.Checked | radioButton_inv_dual.Checked
                   | radioButton_inv_bigblunt.Checked | radioButton_inv_pole.Checked | radioButton_inv_dagger.Checked | radioButton_inv_bigsword.Checked
                   | radioButton_inv_bow.Checked | radioButton_inv_crossbow.Checked | radioButton_inv_rapier.Checked | radioButton_inv_ancientswo.Checked
                   | radioButton_inf_spilbook.Checked)
            {
                dataGridView_inv.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridView_inv.Columns[3].HeaderText = "pAtk";
                dataGridView_inv.Columns[3].ReadOnly = true;
                dataGridView_inv.Columns[3].Width = 50;
                dataGridView_inv.Columns[3].ToolTipText = "физ атака";
                dataGridView_inv.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridView_inv.Columns[4].HeaderText = "mAtk";
                dataGridView_inv.Columns[4].ReadOnly = true;
                dataGridView_inv.Columns[4].Width = 50;
                dataGridView_inv.Columns[4].ToolTipText = "маг атака";
                dataGridView_inv.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridView_inv.Columns[5].HeaderText = "speed";
                dataGridView_inv.Columns[5].ReadOnly = true;
                dataGridView_inv.Columns[5].Width = 50;
                dataGridView_inv.Columns[5].ToolTipText = "Скорость атаки";
                dataGridView_inv.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridView_inv.Columns[6].HeaderText = "кри";
                dataGridView_inv.Columns[6].ReadOnly = true;
                dataGridView_inv.Columns[6].Width = 50;
                dataGridView_inv.Columns[6].ToolTipText = "Количество кристаллов получаемое когда ломаешь шмотку";
                dataGridView_inv.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridView_inv.Columns[7].HeaderText = "вес";
                dataGridView_inv.Columns[7].ReadOnly = true;
                dataGridView_inv.Columns[7].Width = 50;
                dataGridView_inv.Columns[7].ToolTipText = "Вес шмотки";
                dataGridView_inv.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridView_inv.Columns[8].HeaderText = "цена";
                dataGridView_inv.Columns[8].ReadOnly = true;
                dataGridView_inv.Columns[8].Width = 50;
                dataGridView_inv.Columns[8].ToolTipText = "Цена продажи шмотки в магазин";
            }
            else
            {
                dataGridView_inv.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridView_inv.Columns[3].HeaderText = "pDef";
                dataGridView_inv.Columns[3].ReadOnly = true;
                dataGridView_inv.Columns[3].Width = 50;
                dataGridView_inv.Columns[3].ToolTipText = "физ защита";
                dataGridView_inv.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridView_inv.Columns[4].HeaderText = "mDef";
                dataGridView_inv.Columns[4].ReadOnly = true;
                dataGridView_inv.Columns[4].Width = 50;
                dataGridView_inv.Columns[4].ToolTipText = "маг защита";
                dataGridView_inv.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridView_inv.Columns[5].HeaderText = "time";
                dataGridView_inv.Columns[5].ReadOnly = true;
                dataGridView_inv.Columns[5].Width = 50;
                dataGridView_inv.Columns[5].ToolTipText = "время жизни шмота (после окончания времени предмет исчезает)";
                dataGridView_inv.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridView_inv.Columns[6].HeaderText = "кри";
                dataGridView_inv.Columns[6].ReadOnly = true;
                dataGridView_inv.Columns[6].Width = 50;
                dataGridView_inv.Columns[6].ToolTipText = "Количество кристаллов получаемое когда ломаешь шмотку";
                dataGridView_inv.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridView_inv.Columns[7].HeaderText = "вес";
                dataGridView_inv.Columns[7].ReadOnly = true;
                dataGridView_inv.Columns[7].Width = 50;
                dataGridView_inv.Columns[7].ToolTipText = "Вес шмотки";
                dataGridView_inv.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridView_inv.Columns[8].HeaderText = "цена";
                dataGridView_inv.Columns[8].ReadOnly = true;
                dataGridView_inv.Columns[8].Width = 50;
                dataGridView_inv.Columns[8].ToolTipText = "Цена продажи шмотки в магазин";
            }

            DataTableReader dtr = t1.CreateDataReader();
            int z = t1.Rows.Count;
            for (int i = 0; i < z; i++)
            {
                dtr.Read();
                if (radioButton_inv_hair.Checked)
                {
                    if (i == 0) { dataGridView_inv.ColumnCount = 5; dataGridView_inv.RowCount = z; }  
                    dataGridView_inv.Rows[i].Cells[3].Value = dtr["add_name_ru"].ToString();
                    dataGridView_inv.Rows[i].Cells[4].Value = dtr["desc_ru"].ToString();
                }
                else if (radioButton_inv_deco.Checked)
                {
                    if (i == 0) { dataGridView_inv.ColumnCount = 4; dataGridView_inv.RowCount = z; }
                    dataGridView_inv.Rows[i].Cells[3].Value = dtr["desc_ru"].ToString();
                }
                else if (radioButton_inv_sword.Checked | radioButton_inv_blunt.Checked | radioButton_inv_dualfist.Checked | radioButton_inv_dual.Checked
                        | radioButton_inv_bigblunt.Checked | radioButton_inv_pole.Checked | radioButton_inv_dagger.Checked | radioButton_inv_bigsword.Checked
                        | radioButton_inv_bow.Checked | radioButton_inv_crossbow.Checked | radioButton_inv_rapier.Checked | radioButton_inv_ancientswo.Checked
                        | radioButton_inf_spilbook.Checked)
                {
                    if (i == 0) { dataGridView_inv.ColumnCount = 9; dataGridView_inv.RowCount = z; }
                    dataGridView_inv.Rows[i].Cells[3].Value = dtr["pAtk"].ToString();
                    dataGridView_inv.Rows[i].Cells[4].Value = dtr["mAtk"].ToString();
                    dataGridView_inv.Rows[i].Cells[5].Value = dtr["pAtkSpd"].ToString();
                    dataGridView_inv.Rows[i].Cells[6].Value = dtr["crystal_count"].ToString();
                    dataGridView_inv.Rows[i].Cells[7].Value = dtr["weight"].ToString();
                    dataGridView_inv.Rows[i].Cells[8].Value = Convert.ToInt32(dtr["price"]).ToString("### ### ### ###");
                }
                else
                {
                    if (i == 0) { dataGridView_inv.ColumnCount = 9; dataGridView_inv.RowCount = z; }
                    dataGridView_inv.Rows[i].Cells[3].Value = dtr["pDef"].ToString();
                    dataGridView_inv.Rows[i].Cells[4].Value = dtr["mDef"].ToString();
                    dataGridView_inv.Rows[i].Cells[5].Value = dtr["time"].ToString();
                    dataGridView_inv.Rows[i].Cells[6].Value = dtr["crystal_count"].ToString();
                    dataGridView_inv.Rows[i].Cells[7].Value = dtr["weight"].ToString();
                    dataGridView_inv.Rows[i].Cells[8].Value = Convert.ToInt32(dtr["price"]).ToString("### ### ### ###");
                }
                dataGridView_inv.Rows[i].Cells[0].Value = dtr["item_id"].ToString();
                try // Если нет картинки вставяет пустую стандартную
                {
                    dataGridView_inv.Rows[i].Cells[1].Value = Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dtr["icon"].ToString() + ".png");
                    dataGridView_inv.Rows[i].Cells[1].ToolTipText = dtr["item_id"].ToString();
                }
                catch { dataGridView_inv.Rows[i].Cells[1].Value = Properties.Resources.none_imge; dataGridView_inv.Rows[i].Cells[1].ToolTipText = dtr["item_id"].ToString(); }
                if (radioButton_lang_ru.Checked)
                {
                    dataGridView_inv.Rows[i].Cells[2].Value = dtr["name_ru"].ToString();
                }
                else
                {
                    dataGridView_inv.Rows[i].Cells[2].Value = dtr["name"].ToString();
                }
                if (dtr["recipeId"].ToString() != "")
                { dataGridView_inv.Rows[i].Cells[2].Style.BackColor = Color.FromName("MistyRose"); }
            }

            dtr.Close();
            dataGridView_inv.ClearSelection();
        }

        private void dataGridView_inv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ClassMob.Items_Id = Convert.ToInt32(dataGridView_inv.CurrentRow.Cells[0].Value);
            ClassMob.Items_Name = Convert.ToString(dataGridView_inv.CurrentRow.Cells[2].Value);
            items_info form2 = new items_info();
            form2.Text = "Подробное описание " + dataGridView_inv.CurrentRow.Cells[2].Value.ToString(); form2.ShowDialog();
        }

        private void dataGridView_inv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            treeView_inventar(sender, e); // Забивает в инвентаре крафт
            dataGridView_inv_npc(sender, e); // Забивает споил и дроп
            dataGridView_inv_shop(sender, e); // Забивает npc во кладку магазин
        }

        private void tabPage_inv_drop_spoil_Layout(object sender, LayoutEventArgs e) // Дублирует вывод инфы в гриды
        {
            if (dataGridView_inv.RowCount == 0) { return; }
            if (tabControl_inv.SelectedIndex == 1) { dataGridView_inv_npc(sender, e); } // Забивает споил и дроп
            if (tabControl_inv.SelectedIndex == 2) { dataGridView_inv_shop(sender, e); } // Забивает магазин
        }

        private void dataGridView_inventar_npc_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            tabControl_home.SelectedIndex = 0;
            list_mobs_output(Convert.ToInt32(dataGridView_inventar_npc.CurrentRow.Cells[5].Value), ""); // Функция вывода мобов в dataGridView_list_mobs вкладка мобы
            
            tabPage_mobs.Refresh();
            dataGridView_list_mobs_CellMouseClick(sender, e); // дроп и споил с мобов
        }

        private void dataGridView_invetar_shop_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ClassMob.Info_Id = Convert.ToInt32(dataGridView_invetar_shop.CurrentRow.Cells[2].Value);
            ClassMob.Info_Name = Convert.ToString(dataGridView_invetar_shop.CurrentRow.Cells[0].Value);
            int count = 0;
            string CommandText1 = "SELECT COUNT(*) FROM spawnlist WHERE npc_templateid = " + Convert.ToInt32(dataGridView_invetar_shop.CurrentRow.Cells[2].Value) + "";
            DataTable t3 = new DataTable();
            ClassConnect.GridView_list_mobs(t3, CommandText1);
            DataTableReader dtr = t3.CreateDataReader(); dtr.Read();
            count = Convert.ToInt32(dtr[0]);
            dtr.Close();
            ClassMob.Map_count = count;

            if (count == 0)
            {
                MessageBox.Show("Моба нет на карте", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                mob_map form2 = new mob_map();
                form2.Text = "Имя - (" + ClassMob.Info_Name + ")  количество  точек респа - (" + count + ")";
                form2.ShowDialog();
            }
        }
        
        #endregion

        #region вкладка Скилы

        private void skill_human(object sender, MouseEventArgs e) { radioButton_skil_human.Image = Properties.Resources.human; }
        private void skill_elf(object sender, MouseEventArgs e) { radioButton_skil_elf.Image = Properties.Resources.elf; }
        private void skill_dark_elf(object sender, MouseEventArgs e) { radioButton_skil_dark_elf.Image = Properties.Resources.darc; }
        private void skill_ork(object sender, MouseEventArgs e) { radioButton_skil_ork.Image = Properties.Resources.orc; }
        private void skill_kamael(object sender, MouseEventArgs e) { radioButton_skil_kamael.Image = Properties.Resources.kamael; }
        private void skill_dwarf(object sender, MouseEventArgs e) { radioButton_skil_dwarf.Image = Properties.Resources.dwarf; }

        private void OffGroupBoxSelectionSkils(object sender, EventArgs e)
        {
            if (radioButton_skil_human.Focused == false & radioButton_skil_human.Checked == false) radioButton_skil_human.Image = Properties.Resources.human50;
            if (radioButton_skil_elf.Focused == false & radioButton_skil_elf.Checked == false) radioButton_skil_elf.Image = Properties.Resources.elf50;
            if (radioButton_skil_dark_elf.Focused == false & radioButton_skil_dark_elf.Checked == false) radioButton_skil_dark_elf.Image = Properties.Resources.darc50;
            if (radioButton_skil_ork.Focused == false & radioButton_skil_ork.Checked == false) radioButton_skil_ork.Image = Properties.Resources.orc50;
            if (radioButton_skil_kamael.Focused == false & radioButton_skil_kamael.Checked == false) radioButton_skil_kamael.Image = Properties.Resources.kamael50;
            if (radioButton_skil_dwarf.Focused == false & radioButton_skil_dwarf.Checked == false) radioButton_skil_dwarf.Image = Properties.Resources.dwarf50;
        }

        private void treeView_skill(object sender, EventArgs e)
        {
            dataGridView_classs_skils.Columns.Clear();
            string textConect = "";
            ClassMob.skil_node_name = null;
            if (radioButton_skil_human.Checked) { textConect = " WHERE id in (0 , 10) "; }
            else if (radioButton_skil_elf.Checked) { textConect = " WHERE id in (18 , 25) "; }
            else if (radioButton_skil_dark_elf.Checked) { textConect = " WHERE id in (31 , 38) "; }
            else if (radioButton_skil_ork.Checked) { textConect = " WHERE id in (44 , 49) "; }
            else if (radioButton_skil_dwarf.Checked) { textConect = " WHERE id = 53 "; }
            else if (radioButton_skil_kamael.Checked) { textConect = " WHERE id in (123 , 124) "; }

            treeView_skills.Nodes.Clear();
            ClassConnect tt = new ClassConnect();
            SQLiteConnection myConnection = new SQLiteConnection(tt.ToString());
            try
            {
                myConnection.Open();
            }
            catch (Exception l)
            {
                MessageBox.Show("Ошибка!\r\n (" + l.Message + ")", "Подключение к базе данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SQLiteCommand myCommand = myConnection.CreateCommand();
            ImageList myImageList = new ImageList();
            myImageList.ImageSize = new Size(32, 32);
            myImageList.ColorDepth = ColorDepth.Depth32Bit;
            int z = 0;
            myCommand.CommandText = "SELECT COUNT(*) FROM class_list "+textConect+"";
            int count = Convert.ToInt32(myCommand.ExecuteScalar());
            int w = 0;
            int[] id = new int[count];

            string[] name = new string[count];

            myCommand.CommandText = "SELECT * FROM class_list"
                                  + " LEFT JOIN char_templates as cht ON cht.ClassId = id" + textConect + "";
            SQLiteDataReader dataRead = myCommand.ExecuteReader();
            while (dataRead.Read())
            {
                id[w] = Convert.ToInt32(dataRead["id"]);
                if (radioButton_lang_ru.Checked)
                {
                    name[w] = dataRead["ClassName_ru"].ToString() + " (" + dataRead["ClassName_en"].ToString() + ")";
                }
                else
                {
                    name[w] = dataRead["ClassName_en"].ToString() + " (" + dataRead["ClassName_ru"].ToString() + ")";
                }
                w++;
            }
            for (int i = 0; i < id.Length; i++)
            {
                myImageList.Images.Add(Image.FromFile(Application.StartupPath + @"\images\class\" + id[i] + ".png"));
                treeView_skills.ImageList = myImageList;
                TreeNode rootNode = new TreeNode(name[i] + "");
                rootNode.ImageIndex = z;
                rootNode.SelectedImageIndex = z;
                rootNode.Name = id[i].ToString();
                z++;
                treeView_skills.Nodes.Add(rootNode);
                dataRead.Close();

                myCommand.CommandText = "SELECT COUNT(*) FROM class_list WHERE parent_id= " + id[i] + "";
                int count2 = Convert.ToInt32(myCommand.ExecuteScalar());
                int w2 = 0;
                int[] id2 = new int[count2];
                string[] name2 = new string[count2];
                myCommand.CommandText = "SELECT * FROM class_list"
                                      + " LEFT JOIN char_templates as cht ON cht.ClassId = id WHERE parent_id= " + id[i] + "";
                SQLiteDataReader dataRead2 = myCommand.ExecuteReader();
                while (dataRead2.Read())
                {
                    id2[w2] = Convert.ToInt32(dataRead2["id"]);
                    if (radioButton_lang_ru.Checked)
                    {
                        name2[w2] = dataRead2["ClassName_ru"].ToString() + " (" + dataRead2["ClassName_en"].ToString() + ")";
                    }
                    else
                    {
                        name2[w2] = dataRead2["ClassName_en"].ToString() + " (" + dataRead2["ClassName_ru"].ToString() + ")";
                    }
                    w2++;
                }
                for (int i2 = 0; i2 < id2.Length; i2++)
                {
                    myImageList.Images.Add(Image.FromFile(Application.StartupPath + @"\images\class\" + id2[i2] + ".png"));
                    TreeNode rootNode2 = new TreeNode(name2[i2]);
                    rootNode2.ImageIndex = z;
                    rootNode2.SelectedImageIndex = z;
                    rootNode2.Name = id2[i2].ToString();
                    z++;
                    treeView_skills.Nodes[i].Nodes.Add(rootNode2);
                    dataRead2.Close();

                    myCommand.CommandText = "SELECT COUNT(*) FROM class_list WHERE parent_id= " + id2[i2] + "";
                    int count3 = Convert.ToInt32(myCommand.ExecuteScalar());
                    int w3 = 0;
                    int[] id3 = new int[count3];
                    string[] name3 = new string[count3];
                    myCommand.CommandText = "SELECT * FROM class_list"
                                          + " LEFT JOIN char_templates as cht ON cht.ClassId = id WHERE parent_id= " + id2[i2] + "";
                    SQLiteDataReader dataRead3 = myCommand.ExecuteReader();
                    while (dataRead3.Read())
                    {
                        id3[w3] = Convert.ToInt32(dataRead3["id"]);
                        if (radioButton_lang_ru.Checked)
                        {
                            name3[w3] = dataRead3["ClassName_ru"].ToString() + " (" + dataRead3["ClassName_en"].ToString() + ")";
                        }
                        else
                        {
                            name3[w3] = dataRead3["ClassName_en"].ToString() + " (" + dataRead3["ClassName_ru"].ToString() + ")";
                        }
                        w3++;
                    }
                    for (int i3 = 0; i3 < id3.Length; i3++)
                    {
                        myImageList.Images.Add(Image.FromFile(Application.StartupPath + @"\images\class\" + id3[i3] + ".png"));
                        TreeNode rootNode3 = new TreeNode(name3[i3]);
                        rootNode3.ImageIndex = z;
                        rootNode3.SelectedImageIndex = z;
                        rootNode3.Name = id3[i3].ToString();
                        z++;
                        treeView_skills.Nodes[i].Nodes[i2].Nodes.Add(rootNode3);
                        dataRead3.Close();

                        myCommand.CommandText = "SELECT COUNT(*) FROM class_list WHERE parent_id= " + id3[i3] + "";
                        int count4 = Convert.ToInt32(myCommand.ExecuteScalar());
                        int w4 = 0;
                        int[] id4 = new int[count4];
                        string[] name4 = new string[count4];
                        myCommand.CommandText = "SELECT * FROM class_list"
                                              + " LEFT JOIN char_templates as cht ON cht.ClassId = id WHERE parent_id= " + id3[i3] + "";
                        SQLiteDataReader dataRead4 = myCommand.ExecuteReader();
                        while (dataRead4.Read())
                        {
                            id4[w4] = Convert.ToInt32(dataRead4["id"]);
                            if (radioButton_lang_ru.Checked)
                            {
                                name4[w4] = dataRead4["ClassName_ru"].ToString() + " (" + dataRead4["ClassName_en"].ToString() + ")";
                            }
                            else
                            {
                                name4[w4] = dataRead4["ClassName_en"].ToString() + " (" + dataRead4["ClassName_ru"].ToString() + ")";
                            }
                            w4++;
                        }
                        for (int i4 = 0; i4 < id4.Length; i4++)
                        {
                            myImageList.Images.Add(Image.FromFile(Application.StartupPath + @"\images\class\" + id4[i4] + ".png"));
                            TreeNode rootNode4 = new TreeNode(name4[i4]);
                            rootNode4.ImageIndex = z;
                            rootNode4.SelectedImageIndex = z;
                            rootNode4.Name = id4[i4].ToString();
                            z++;
                            treeView_skills.Nodes[i].Nodes[i2].Nodes[i3].Nodes.Add(rootNode4);
                            dataRead4.Close();
                        }
                    }
                }
                dataRead.Close();
            }
            treeView_skills.EndUpdate();
            myConnection.Close();
        }


        private void treeView_skills_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ClassMob.skil_node_name = e.Node.Name;
            ClassMob.skil_node_level = e.Node.Level;
            skill_viev(sender, e);
        }

        private void skill_viev(object sender, EventArgs e)
        {
            if (ClassMob.skil_node_name == null) { return; }
            checkBox_skills.Refresh();
            ClassConnect tt = new ClassConnect();
            SQLiteConnection myConnection = new SQLiteConnection(tt.ToString());
            try
            {
                myConnection.Open();
            }
            catch (Exception l)
            {
                MessageBox.Show("Ошибка!\r\n (" + l.Message + ")", "Подключение к базе данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SQLiteCommand myCommand = myConnection.CreateCommand();
            dataGridView_classs_skils.Refresh();
            string CommandText = "SELECT *  FROM skill_ru as sk_ru "
                               + " LEFT JOIN skill_trees as sk_tr ON sk_ru.skill_id = sk_tr.skill_id AND sk_ru.level = sk_tr.level"
                               + " WHERE sk_tr.class_id IN ('" + ClassMob.skil_node_name + "', '35" + ClassMob.skil_node_level + "') ORDER BY sk_tr.min_level ";
            DataTable t_skills = new DataTable(); 
            ClassConnect.GridView_list_mobs(t_skills, CommandText); // Создан отдельный файл с класом коннект (обозреватель решений)
            DataTableReader dtr_skill = t_skills.CreateDataReader();
            int z = t_skills.Rows.Count;

            dataGridView_classs_skils.Columns.Clear();
            dataGridView_classs_skils.Refresh();
            dataGridView_classs_skils.Columns.Add(new DataGridViewImageColumn());
            dataGridView_classs_skils.Columns[0].HeaderText = "Вид";
            dataGridView_classs_skils.Columns[0].Width = 40;
            dataGridView_classs_skils.Columns[0].DefaultCellStyle.Padding = new Padding(3);
            dataGridView_classs_skils.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_classs_skils.Columns[1].HeaderText = "lvl";
            dataGridView_classs_skils.Columns[1].ReadOnly = true;
            dataGridView_classs_skils.Columns[1].Width = 35;
            dataGridView_classs_skils.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_classs_skils.Columns[2].HeaderText = "Название";
            dataGridView_classs_skils.Columns[2].ReadOnly = true;
            dataGridView_classs_skils.Columns[2].Width = 150;            
            dataGridView_classs_skils.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_classs_skils.Columns[3].HeaderText = "Описание";
            dataGridView_classs_skils.Columns[3].ReadOnly = true;
            dataGridView_classs_skils.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView_classs_skils.Columns[3].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView_classs_skils.Columns[3].DefaultCellStyle.Padding = new Padding(2);
            dataGridView_classs_skils.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView_classs_skils.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_classs_skils.Columns[4].HeaderText = "Тип";
            dataGridView_classs_skils.Columns[4].ReadOnly = true;
            dataGridView_classs_skils.Columns[4].Width = 150;
            dataGridView_classs_skils.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_classs_skils.Columns[5].HeaderText = "SP";
            dataGridView_classs_skils.Columns[5].ReadOnly = true;
            dataGridView_classs_skils.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView_classs_skils.Columns[5].Width = 60;
            dataGridView_classs_skils.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_classs_skils.Columns[6].HeaderText = "Тчк";
            dataGridView_classs_skils.Columns[6].ReadOnly = true;
            dataGridView_classs_skils.Columns[6].Width = 40;
            dataGridView_classs_skils.Columns[6].Name = "tchk";
            dataGridView_classs_skils.Columns[6].ToolTipText = "Отоброжает имеет ли скил возможность заточки";
            dataGridView_classs_skils.Columns.Add(new DataGridViewTextBoxColumn());
            if (checkBox_skills.Checked == false)
            {
                dataGridView_classs_skils.Columns[6].Visible = false;
            }
            dataGridView_classs_skils.Columns[7].Visible = false;
            dataGridView_classs_skils.RowCount = z;
            progressBar_skills.Value = 0;
            progressBar_skills.Refresh();
            progressBar_skills.Maximum = z;
            string[] mas = new string[z];
            string ping_text = ClassMob.ping_text;
            for (int i = 0; i < z; i++) // Циклзаполнения вклади клан шмот
            {
                dtr_skill.Read();
                progressBar_skills.Refresh();
                progressBar_skills.PerformStep();
                try // Если нет картинки вставяет пустую стандартную
                {
                    if (dtr_skill["skill_id"].ToString() == "239")
                        dataGridView_classs_skils.Rows[i].Cells[0].Value = Image.FromFile(Application.StartupPath + @"\images\skills\" + dtr_skill["skill_id"].ToString() + "_" + dtr_skill["level"].ToString() + ".png");
                    else
                        dataGridView_classs_skils.Rows[i].Cells[0].Value = Image.FromFile(Application.StartupPath + @"\images\skills\" + dtr_skill["skill_id"].ToString() + ".png");
                    dataGridView_classs_skils.Rows[i].Cells[0].ToolTipText = dtr_skill["skill_id"].ToString();
                }
                catch { dataGridView_classs_skils.Rows[i].Cells[0].Value = Properties.Resources.none_imge; dataGridView_classs_skils.Rows[i].Cells[0].ToolTipText = dtr_skill["skill_id"].ToString(); }
                dataGridView_classs_skils.Rows[i].Cells[1].Value = dtr_skill["min_level"].ToString(); // лвл перса
                if (radioButton_lang_ru.Checked)
                {
                    dataGridView_classs_skils.Rows[i].Cells[2].Value = dtr_skill["name_ru"].ToString() + " ур. " + dtr_skill["level"].ToString(); // название
                }
                else
                {
                    dataGridView_classs_skils.Rows[i].Cells[2].Value = dtr_skill["name"].ToString() + " ур. " + dtr_skill["level"].ToString(); // название
                }
                    if (ClassMob.ping == 0)
                {
                    dataGridView_classs_skils.Rows[i].Cells[3].Value = dtr_skill["desc_ru"].ToString(); // описание
                }
                else
                {
                    dataGridView_classs_skils.Rows[i].Cells[3].Value = ping_text;
                }
                dataGridView_classs_skils.Rows[i].Cells[4].Value = dtr_skill["oper_tupe"].ToString(); // тип
                dataGridView_classs_skils.Rows[i].Cells[7].Value = dtr_skill["skill_id"].ToString(); // тип
                dataGridView_classs_skils.Rows[i].Cells[5].Value = Convert.ToInt32(dtr_skill["sp"]).ToString("### ### ###"); // SP
                if (radioButton_skil_dwarf.Checked == false & dtr_skill["skill_id"].ToString() == "1368")
                { dataGridView_classs_skils.Rows[i].Visible = false; }
                if (i != 0)
                {
                    if (Convert.ToInt32(dtr_skill["min_level"]) != Convert.ToInt32(dataGridView_classs_skils.Rows[i - 1].Cells[1].Value))
                    {
                        for (int i2 = 0; i2 < 7; i2++)
                            dataGridView_classs_skils.Rows[i].Cells[i2].Style.BackColor = Color.FromName("LightGreen");
                    }
                }
                if (checkBox_skills.Checked)
                {
                    if (mas.Contains(dtr_skill["skill_id"].ToString()))
                    {
                        dataGridView_classs_skils.Rows[i].Cells[6].Value = "+";
                        continue;
                    }
                    myCommand.CommandText = "SELECT level FROM skill_ru"
                          + " WHERE skill_id = " + dtr_skill["skill_id"].ToString() + " AND level > 100 ";
                    if (Convert.ToInt32(myCommand.ExecuteScalar()) >= 100)
                    {
                        mas[i] = dtr_skill["skill_id"].ToString();
                        dataGridView_classs_skils.Rows[i].Cells[6].Value = "+";
                    }
                }
            }
            dtr_skill.Close();
            myConnection.Close();
            dataGridView_classs_skils.ClearSelection();
        }

        private void dataGridView_classs_skils_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (checkBox_skills.Checked == false)
            if (DialogResult.Cancel == MessageBox.Show("У вас отключена галочка «Заточка скилов» включить?\r\n"
                + "Включение данной опции значительно увеличит время отображения скилов.", "Информация",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            { return; }
            else
            { checkBox_skills.Checked = true; return; }

            if (dataGridView_classs_skils.CurrentRow.Cells[6].Value != "+")

            { MessageBox.Show("Скилл не затачивается", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            ClassMob.skil_name = dataGridView_classs_skils.CurrentRow.Cells[2].Value.ToString();
            ClassMob.skil_id = Convert.ToInt32(dataGridView_classs_skils.CurrentRow.Cells[7].Value);
            skill_enchant form2 = new skill_enchant();
            form2.Text = "Описание заточек скила " + dataGridView_classs_skils.CurrentRow.Cells[2].Value.ToString(); form2.ShowDialog();

        }

        bool doubleClicked = false;
        private void treeView_skills_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (doubleClicked)
            {
                doubleClicked = false;
                e.Cancel = true;
            }
        }

        private void treeView_skills_MouseDown(object sender, MouseEventArgs e)
        {
            TreeNode node;
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                node = treeView_skills.GetNodeAt(e.X, e.Y);
                if (node != null)
                {
                    doubleClicked = true;
                }
            }
        }

        private void button_Expand_All_Click(object sender, EventArgs e)
        {
            treeView_skills.ExpandAll();
            dataGridView_classs_skils.Columns.Clear();
        }

        private void button_Collapse_All_Click(object sender, EventArgs e)
        {
            treeView_skills.CollapseAll();
            dataGridView_classs_skils.Columns.Clear();
        }

        private void tabControl_skills_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Name_grid = "_skils_clan";
            int Pages = 1;
            string Tip = "";

            if (tabControl_skills.SelectedIndex == 1)
            {
                Name_grid = "_skils_clan"; Pages = 1;
                Tip = "SELECT *  FROM skill_ru as sk_ru "
                                 + " LEFT JOIN skill_clan as sk_tr ON sk_ru.skill_id = sk_tr.skillId AND sk_ru.level = sk_tr.skillLvl"
                                 + " LEFT JOIN etcitem as etc ON etc.item_id = sk_tr.itemsId"
                                 + " WHERE sk_ru.skill_id = sk_tr.skillId";
            }
            else if (tabControl_skills.SelectedIndex == 2)
            {
                Name_grid = "_skils_fish"; Pages = 2; 
                Tip = "SELECT *  FROM skill_ru as sk_ru "
                     + " LEFT JOIN skill_fishing as sk_tr ON sk_ru.skill_id = sk_tr.skillId AND sk_ru.level = sk_tr.skillLvl"
                     + " LEFT JOIN etcitem as etc ON etc.item_id = sk_tr.itemsId"
                     + " WHERE sk_ru.skill_id = sk_tr.skillId";
            }
            else if (tabControl_skills.SelectedIndex == 3)
            {
                Name_grid = "_castl_fort"; Pages = 3;
                Tip = "SELECT *  FROM skill_ru as sk_ru "
                     + " LEFT JOIN skill_castl_fort as sk_tr ON sk_ru.skill_id = sk_tr.skillId AND sk_ru.level = sk_tr.skillLvl"
                     + " WHERE sk_ru.skill_id = sk_tr.skillId";
            }
            else if (tabControl_skills.SelectedIndex == 4)
            {
                Name_grid = "_skill_territory"; Pages = 4;
                Tip = "SELECT *  FROM skill_ru as sk_ru "
                     + " LEFT JOIN skill_territory as sk_tr ON sk_ru.skill_id = sk_tr.skillId AND sk_ru.level = sk_tr.skillLvl"
                     + " WHERE sk_ru.skill_id = sk_tr.skillId";
            }
            else if (tabControl_skills.SelectedIndex == 5)
            {
                Name_grid = "_skill_transfer"; Pages = 5;
                Tip = "SELECT *  FROM skill_ru as sk_ru "
                     + " LEFT JOIN skill_transfer as sk_tr ON sk_ru.skill_id = sk_tr.skillId AND sk_ru.level = sk_tr.skillLvl"
                     + " LEFT JOIN char_templates as chr ON chr.ClassId = sk_tr.classId"
                     + " WHERE sk_ru.skill_id = sk_tr.skillId ORDER BY chr.ClassName_en";
            }
            else if (tabControl_skills.SelectedIndex == 6)
            {
                Name_grid = "_skill_subClass"; Pages = 6;
                Tip = "SELECT *  FROM skill_ru as sk_ru "
                     + " LEFT JOIN skill_subClass as sk_tr ON sk_ru.skill_id = sk_tr.skillId AND sk_ru.level = sk_tr.skillLvl"
                     + " LEFT JOIN etcitem as etc ON etc.item_id = sk_tr.itemsId"
                     + " WHERE sk_ru.skill_id = sk_tr.skillId  ORDER BY sk_tr.subClassLvlNumber";
            }
            else if (tabControl_skills.SelectedIndex == 7)
            {
                Name_grid = "_sub_pledge"; Pages = 7;
                Tip = "SELECT *  FROM skill_ru as sk_ru "
                     + " LEFT JOIN skill_sub_pledge as sk_tr ON sk_ru.skill_id = sk_tr.skillId AND sk_ru.level = sk_tr.skillLvl"
                     + " LEFT JOIN etcitem as etc ON etc.item_id = sk_tr.itemsId"
                     + " WHERE sk_ru.skill_id = sk_tr.skillId";
            }
            else if (tabControl_skills.SelectedIndex == 8)
            {
                Name_grid = "_skill_transform"; Pages = 8;
                Tip = "SELECT *  FROM skill_ru as sk_ru "
                     + " LEFT JOIN skill_transform as sk_tr ON sk_ru.skill_id = sk_tr.skillId AND sk_ru.level = sk_tr.skillLvl"
                     + " LEFT JOIN etcitem as etc ON etc.item_id = sk_tr.itemsId"
                     + " WHERE sk_ru.skill_id = sk_tr.skillId  ORDER BY sk_tr.getLevel";
            }
            else { return; }

            string CommandText = Tip;
            DataTable t_skills = new DataTable();
            ClassConnect.GridView_list_mobs(t_skills, CommandText); // Создан отдельный файл с класом коннект (обозреватель решений)
            DataTableReader dtr_skill = t_skills.CreateDataReader();
            (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns.Clear();
            (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Refresh();

            (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns.Add(new DataGridViewImageColumn());
            (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[0].HeaderText = "Вид";
            (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[0].Width = 40;
            (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[0].DefaultCellStyle.Padding = new Padding(3);
            (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns.Add(new DataGridViewTextBoxColumn());
            (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[1].HeaderText = "lvl";
            (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[1].ReadOnly = true;
            (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[1].Width = 35;
            (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns.Add(new DataGridViewTextBoxColumn());
            (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[2].HeaderText = "Название";
            (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[2].ReadOnly = true;
            (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[2].Width = 150;
            (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns.Add(new DataGridViewTextBoxColumn());
            (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[3].HeaderText = "Описание";
            (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[3].ReadOnly = true;
            (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[3].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[3].DefaultCellStyle.Padding = new Padding(2);
            (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            if (tabControl_skills.SelectedIndex == 1 | tabControl_skills.SelectedIndex == 2 | tabControl_skills.SelectedIndex == 7 | tabControl_skills.SelectedIndex == 8) // клан + рыбалка + отряд + трансформация
            {
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns.Add(new DataGridViewImageColumn());
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[4].HeaderText = "Вид";
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[4].ToolTipText = "Ингредиент необходимый для изучения скила";
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[4].Width = 40;
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[4].DefaultCellStyle.Padding = new Padding(3);
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns.Add(new DataGridViewTextBoxColumn());
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[5].ToolTipText = "Количество ингредиентов необходимых для изучения скила";
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[5].HeaderText = "Кол-во";
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[5].ReadOnly = true;
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[5].Width = 60;
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns.Add(new DataGridViewTextBoxColumn());
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[6].ToolTipText = "Лвл клана необходимый для изучения скила";
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[6].HeaderText = "lvl клана";
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[6].ReadOnly = true;
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[6].Width = 60;
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns.Add(new DataGridViewTextBoxColumn());
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[7].ToolTipText = "SP необходимый для изучения скила";
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[7].HeaderText = "sp";
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[7].ReadOnly = true;
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[7].Width = 60;
            }

            if (tabControl_skills.SelectedIndex == 2 | tabControl_skills.SelectedIndex == 8) // клан + трансформация
            {
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[7].Visible = false;
            }

            if (tabControl_skills.SelectedIndex == 3) // замок форт
            {
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns.Add(new DataGridViewTextBoxColumn());
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[4].ToolTipText = "Замок или Форт который содержит скил";
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[4].HeaderText = "Замок - Форт";
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[4].ReadOnly = true;
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[4].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[4].Width = 150;
            }

            if (tabControl_skills.SelectedIndex == 5) // трансфер
            {
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns.Add(new DataGridViewTextBoxColumn());
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[4].HeaderText = "Класс";
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[4].ReadOnly = true;
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[4].Width = 150; 
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns.Add(new DataGridViewTextBoxColumn());
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[5].ToolTipText = "уровень с которого можно использовать скил";
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[5].HeaderText = "lvl";
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[5].ReadOnly = true;
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[5].Width = 60;
            }

            if (tabControl_skills.SelectedIndex == 6) // Саб класс
            {
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns.Add(new DataGridViewImageColumn());
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[4].HeaderText = "Вид";
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[4].ToolTipText = "Ингредиент необходимый для изучения скила";
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[4].Width = 40;
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[4].DefaultCellStyle.Padding = new Padding(3);
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns.Add(new DataGridViewTextBoxColumn());
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[5].ToolTipText = "Количество ингредиентов необходимых для изучения скила";
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[5].HeaderText = "Кол-во";
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[5].ReadOnly = true;
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[5].Width = 60;
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns.Add(new DataGridViewTextBoxColumn());
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[6].ToolTipText = "Лвл Саб класса необходимый для изучения скила";
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[6].HeaderText = "lvl саб к..";
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[6].ReadOnly = true;
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Columns[6].Width = 80;
            }

            (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).RowCount = t_skills.Rows.Count;
            string ping_text = ClassMob.ping_text;

            for (int i = 0; i < t_skills.Rows.Count; i++) 
            {
                dtr_skill.Read();
                try // Если нет картинки вставяет пустую стандартную
                {
                    if(Convert.ToInt32(dtr_skill["skill_id"]) > 541 & Convert.ToInt32(dtr_skill["skill_id"]) <= 558)
                        (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[0].Value = Image.FromFile(Application.StartupPath + @"\images\skills\541.png");
                    else
                        (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[0].Value = Image.FromFile(Application.StartupPath + @"\images\skills\" + dtr_skill["skill_id"].ToString() + ".png");
                    (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[0].ToolTipText = dtr_skill["skillId"].ToString();
                }
                catch
                { 
                    (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[0].Value = Properties.Resources.none_imge;
                    (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[0].ToolTipText = dtr_skill["skillId"].ToString();
                }
                (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[1].Value = dtr_skill["skillLvl"].ToString(); // лвл 
                
                if (radioButton_lang_ru.Checked)
                    (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[2].Value = dtr_skill["name_ru"].ToString() + " ур. " + dtr_skill["level"].ToString(); // название
                else
                    (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[2].Value = dtr_skill["skillName"].ToString() + " ур. " + dtr_skill["level"].ToString(); // название
                
                if (ClassMob.ping == 0)
                {
                    (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[3].Value = dtr_skill["desc_ru"].ToString(); // описание
                }
                else
                {
                    (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[3].Value = ping_text;
                }
                if (tabControl_skills.SelectedIndex == 1 | tabControl_skills.SelectedIndex == 7) // клан + отряд
                {
                    try // Если нет картинки вставяет пустую стандартную
                    {
                        (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[4].Value = Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dtr_skill["icon"].ToString() + ".png");
                    }
                    catch
                    {
                        (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[4].Value = Properties.Resources.none_imge;
                    }
                    (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[4].ToolTipText = dtr_skill["name"].ToString();
                    (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[5].Value = Convert.ToInt32(dtr_skill["itemsCount"]).ToString("### ### ###");
                    (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[6].Value = dtr_skill["getLevel"].ToString();
                    (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[7].Value = dtr_skill["levelUpSp"].ToString(); 
                }
                else if (tabControl_skills.SelectedIndex == 2 | tabControl_skills.SelectedIndex == 8) // рыбалка + трансформ
                {
                    try // Если нет картинки вставяет пустую стандартную
                    {
                        (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[4].Value = Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dtr_skill["icon"].ToString() + ".png");
                    }
                    catch
                    {
                        (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[4].Value = Properties.Resources.none_imge;
                    }
                    (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[4].ToolTipText = dtr_skill["name"].ToString();
                    (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[5].Value = Convert.ToInt32(dtr_skill["itemsCount"]).ToString("### ### ###");
                    (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[6].Value = dtr_skill["getLevel"].ToString();
                }
                else if (tabControl_skills.SelectedIndex == 3) // замок - форт
                {
                    string[] b;
                if (radioButton_lang_ru.Checked)
                    b = Regex.Split(dtr_skill["residencelds_ru"].ToString(), @"	");
                else
                    b = Regex.Split(dtr_skill["residenceIds"].ToString(), @"	");

                    for (int i2 = 1; i2 < b.Length; i2++)
                    {
                        (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[4].Value += "\r\n" + b[i2]; 
                    }
                }
                else if (tabControl_skills.SelectedIndex == 5) // трансфер
                {
                    if (radioButton_lang_ru.Checked)
                        (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[4].Value = dtr_skill["ClassName_ru"].ToString();
                    else
                        (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[4].Value = dtr_skill["ClassName_en"].ToString();

                    (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[5].Value = dtr_skill["getLevel"].ToString();
                }
                else if (tabControl_skills.SelectedIndex == 6) // Саб класс 
                {
                    try // Если нет картинки вставяет пустую стандартную
                    {
                        (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[4].Value = Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dtr_skill["icon"].ToString() + ".png");
                    }
                    catch
                    {
                        (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[4].Value = Properties.Resources.none_imge;
                    }
                    (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[4].ToolTipText = dtr_skill["name"].ToString();
                    (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[5].Value = Convert.ToInt32(dtr_skill["itemsCount"]).ToString("### ### ###");
                    (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).Rows[i].Cells[6].Value = dtr_skill["subClassLvlNumber"].ToString();
                }
            }
            dtr_skill.Close();
            (tabControl_skills.TabPages[Pages].Controls["dataGridView" + Name_grid.ToString()] as DataGridView).ClearSelection();
        }

        #endregion

        #region вкладка Сеты

        private void set_human(object sender, MouseEventArgs e) { radioButton_set_human.Image = Properties.Resources.human; }
        private void set_elf(object sender, MouseEventArgs e) { radioButton_set_elf.Image = Properties.Resources.elf; }
        private void set_dark_elf(object sender, MouseEventArgs e) { radioButton_set_dark_elf.Image = Properties.Resources.darc; }
        private void set_ork(object sender, MouseEventArgs e) { radioButton_set_orc.Image = Properties.Resources.orc; }
        private void set_kamael(object sender, MouseEventArgs e) { radioButton_set_kamael.Image = Properties.Resources.kamael; }
        private void set_dwarf(object sender, MouseEventArgs e) { radioButton_set_dwarf.Image = Properties.Resources.dwarf; }

        private void set_s84(object sender, MouseEventArgs e) { radioButton_set_s84.Image = Properties.Resources._239_7; }
        private void set_s80(object sender, MouseEventArgs e) { radioButton_set_s80.Image = Properties.Resources._239_6; }
        private void set_s(object sender, MouseEventArgs e) { radioButton_set_s.Image = Properties.Resources._239_5; }
        private void set_a(object sender, MouseEventArgs e) { radioButton_set_a.Image = Properties.Resources._239_4; }
        private void set_b(object sender, MouseEventArgs e) { radioButton_set_b.Image = Properties.Resources._239_3; }
        private void set_c(object sender, MouseEventArgs e) { radioButton_set_c.Image = Properties.Resources._239_2; }
        private void set_d(object sender, MouseEventArgs e) { radioButton_set_d.Image = Properties.Resources._239_1; }

        private void set_robe(object sender, MouseEventArgs e) { radioButton_set_robe.Image = Properties.Resources.robe; }
        private void set_light(object sender, MouseEventArgs e) { radioButton_set_light.Image = Properties.Resources.light; }
        private void set_heavy(object sender, MouseEventArgs e) { radioButton_set_heavy.Image = Properties.Resources.heavy; }

        private void set_women(object sender, MouseEventArgs e) { radioButton_set_women.Image = Properties.Resources.women; }
        private void set_men(object sender, MouseEventArgs e) { radioButton_set_men.Image = Properties.Resources.men; }

        private void set_mage(object sender, MouseEventArgs e) { radioButton_set_mage.Image = Properties.Resources.mage; }
        private void set_fighter(object sender, MouseEventArgs e) { radioButton_set_fighter.Image = Properties.Resources.fighter; }

        private void Off_Group_Box_Selection_Set(object sender, EventArgs e)
        {
            if (radioButton_set_human.Focused == false & radioButton_set_human.Checked == false) radioButton_set_human.Image = Properties.Resources.human50;
            if (radioButton_set_elf.Focused == false & radioButton_set_elf.Checked == false) radioButton_set_elf.Image = Properties.Resources.elf50;
            if (radioButton_set_dark_elf.Focused == false & radioButton_set_dark_elf.Checked == false) radioButton_set_dark_elf.Image = Properties.Resources.darc50;
            if (radioButton_set_orc.Focused == false & radioButton_set_orc.Checked == false) radioButton_set_orc.Image = Properties.Resources.orc50;
            if (radioButton_set_kamael.Focused == false & radioButton_set_kamael.Checked == false) radioButton_set_kamael.Image = Properties.Resources.kamael50;
            if (radioButton_set_dwarf.Focused == false & radioButton_set_dwarf.Checked == false) radioButton_set_dwarf.Image = Properties.Resources.dwarf50;

            if (radioButton_set_s84.Focused == false & radioButton_set_s84.Checked == false) radioButton_set_s84.Image = Properties.Resources._239_7_50;
            if (radioButton_set_s80.Focused == false & radioButton_set_s80.Checked == false) radioButton_set_s80.Image = Properties.Resources._239_6_50;
            if (radioButton_set_s.Focused == false & radioButton_set_s.Checked == false) radioButton_set_s.Image = Properties.Resources._239_5_50;
            if (radioButton_set_a.Focused == false & radioButton_set_a.Checked == false) radioButton_set_a.Image = Properties.Resources._239_4_50;
            if (radioButton_set_b.Focused == false & radioButton_set_b.Checked == false) radioButton_set_b.Image = Properties.Resources._239_3_50;
            if (radioButton_set_c.Focused == false & radioButton_set_c.Checked == false) radioButton_set_c.Image = Properties.Resources._239_2_50;
            if (radioButton_set_d.Focused == false & radioButton_set_d.Checked == false) radioButton_set_d.Image = Properties.Resources._239_1_50;

            if (radioButton_set_robe.Focused == false & radioButton_set_robe.Checked == false) radioButton_set_robe.Image = Properties.Resources.robe_50;
            if (radioButton_set_light.Focused == false & radioButton_set_light.Checked == false) radioButton_set_light.Image = Properties.Resources.light_50;
            if (radioButton_set_heavy.Focused == false & radioButton_set_heavy.Checked == false) radioButton_set_heavy.Image = Properties.Resources.heavy_50;

            if (radioButton_set_women.Focused == false & radioButton_set_women.Checked == false) radioButton_set_women.Image = Properties.Resources.women_50;
            if (radioButton_set_men.Focused == false & radioButton_set_men.Checked == false) radioButton_set_men.Image = Properties.Resources.men_50;

            if (radioButton_set_mage.Focused == false & radioButton_set_mage.Checked == false) radioButton_set_mage.Image = Properties.Resources.mage_50;
            if (radioButton_set_fighter.Focused == false & radioButton_set_fighter.Checked == false) radioButton_set_fighter.Image = Properties.Resources.fighter_50;
        }

        private void set_viev(object sender, EventArgs e)
        {
            string crystal_type = "";
            string type_set = "";
            string type_set2 = "";
            if (radioButton_set_s84.Checked) { crystal_type = "s84"; }
            if (radioButton_set_s80.Checked) { crystal_type = "s80"; }
            if (radioButton_set_s.Checked) { crystal_type = "s"; }
            if (radioButton_set_a.Checked) { crystal_type = "a"; }
            if (radioButton_set_b.Checked) { crystal_type = "b"; }
            if (radioButton_set_c.Checked) { crystal_type = "c"; }
            if (radioButton_set_d.Checked) { crystal_type = "d"; }

            if (radioButton_set_kamael.Checked)
            { radioButton_set_light.Checked = true; }

            if (radioButton_set_robe.Checked) { type_set = "robe"; type_set2 = "noble_robe"; }
            if (radioButton_set_light.Checked) { type_set = "light"; type_set2 = "noble_light"; }
            if (radioButton_set_heavy.Checked) { type_set = "heavy"; type_set2 = "noble_heavy"; }

            string CommandText = "SELECT * FROM armorset as ars "
                               + " LEFT JOIN etcitem as etc ON etc.item_id = ars.chest"
                               + " WHERE etc.crystal_type = '" + crystal_type + "' AND  ars.tip_sets IN ('" + type_set + "', '" + type_set2 + "')";
            DataTable t_set = new DataTable();
            ClassConnect.GridView_list_mobs(t_set, CommandText); // Создан отдельный файл с класом коннект (обозреватель решений)
            DataTableReader dtr_set = t_set.CreateDataReader();
            int zx = t_set.Rows.Count;

            dataGridView_set.Columns.Clear();
            dataGridView_set.Refresh();
            dataGridView_set.Columns.Add(new DataGridViewImageColumn());
            dataGridView_set.Columns[0].HeaderText = "Вид";
            dataGridView_set.Columns[0].Width = 40;
            dataGridView_set.Columns[0].DefaultCellStyle.Padding = new Padding(3);
            dataGridView_set.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_set.Columns[1].HeaderText = "Название";
            dataGridView_set.Columns[1].ReadOnly = true;
            dataGridView_set.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView_set.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView_set.Columns[1].DefaultCellStyle.Padding = new Padding(2);
            dataGridView_set.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView_set.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_set.Columns[2].Visible = false;

            dataGridView_set.RowCount = zx;

            for (int i = 0; i < zx; i++) // Циклзаполнения вклади клан шмот
            {
                dtr_set.Read();
                try // Если нет картинки вставяет пустую стандартную
                {
                    dataGridView_set.Rows[i].Cells[0].Value = Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dtr_set["icon"].ToString() + ".png");
                    dataGridView_set.Rows[i].Cells[0].ToolTipText = dtr_set["name"].ToString();
                }
                catch { dataGridView_set.Rows[i].Cells[0].Value = Properties.Resources.none_imge; dataGridView_set.Rows[i].Cells[0].ToolTipText = dtr_set["item_id "].ToString(); }
                if (radioButton_lang_ru.Checked)
                    dataGridView_set.Rows[i].Cells[1].Value = dtr_set["skill_name_ru"].ToString();
                else
                    dataGridView_set.Rows[i].Cells[1].Value = dtr_set["skill_name_en"].ToString();
                dataGridView_set.Rows[i].Cells[2].Value = dtr_set["id"].ToString();            
            }
            dtr_set.Close();
            if (dataGridView_set.RowCount > 0) { set_img(sender, e); set_img_viev(sender, e); }
            dataGridView_set.ClearSelection();
        }

        private void set_img(object sender, EventArgs e)
        {
            textBox_set.Text = "";
            if (dataGridView_set.RowCount == 0) { return; }
            string grade = "";
            string class_name = "";
            string sex = "";
            string char_tip = "";

            string CommandText = "SELECT tip_sets, name_set FROM armorset WHERE id = " + dataGridView_set.CurrentRow.Cells[2].Value.ToString();
            DataTable t_set = new DataTable();
            ClassConnect.GridView_list_mobs(t_set, CommandText); // Создан отдельный файл с класом коннект (обозреватель решений)
            DataTableReader dtr_set = t_set.CreateDataReader();
            dtr_set.Read();
            radioButton_set_robe.Enabled = true;
            radioButton_set_light.Enabled = true;
            radioButton_set_heavy.Enabled = true;

            if (radioButton_set_s84.Checked) { grade = "S84"; }
            else if (radioButton_set_s80.Checked) { grade = "S80"; }
            else if (radioButton_set_s.Checked) { grade = "S"; }
            else if (radioButton_set_a.Checked) { grade = "A"; }
            else if (radioButton_set_b.Checked) { grade = "B"; }
            else if (radioButton_set_c.Checked) { grade = "C"; }


            if (radioButton_set_human.Checked) { class_name = "human"; groupBox_sex.Enabled = true; }
            else if (radioButton_set_elf.Checked) { class_name = "elf"; groupBox_sex.Enabled = false; }
            else if (radioButton_set_dark_elf.Checked) { class_name = "delf"; groupBox_sex.Enabled = false; }
            else if (radioButton_set_orc.Checked) { class_name = "orc"; groupBox_sex.Enabled = true; }
            else if (radioButton_set_kamael.Checked)
            {
                class_name = "kamael"; groupBox_sex.Enabled = false;
                radioButton_set_heavy.Enabled = false; radioButton_set_robe.Enabled = false;
                radioButton_set_light.Checked = true; radioButton_set_light.Image = Properties.Resources.light;}
            else if (radioButton_set_dwarf.Checked) { class_name = "dwarf"; groupBox_sex.Enabled = false; }

            if (radioButton_set_women.Checked) { sex = "w"; }
            else if (radioButton_set_men.Checked) { sex = "m"; }

            if (radioButton_set_mage.Checked & groupBox_sex.Enabled) { char_tip = "_m"; }
            else if (radioButton_set_fighter.Checked & groupBox_sex.Enabled) { char_tip = "_f"; }

            string tip_sets = dtr_set["tip_sets"].ToString();
            string name_sets = dtr_set["name_set"].ToString();

            if (radioButton_set_s84.Checked)
            {
                if (radioButton_set_human.Checked & radioButton_set_robe.Checked & radioButton_set_fighter.Checked)
                { if (Regex.IsMatch(tip_sets, "noble", RegexOptions.IgnoreCase)) { tip_sets = "noble_light"; } else { tip_sets = "light"; } }
                else if (radioButton_set_human.Checked & radioButton_set_robe.Checked & radioButton_set_mage.Checked)
                { if (Regex.IsMatch(tip_sets, "noble", RegexOptions.IgnoreCase)) { tip_sets = "noble_light"; } else { tip_sets = "light"; } }
                else if (radioButton_set_human.Checked & radioButton_set_heavy.Checked & radioButton_set_mage.Checked)
                { if (Regex.IsMatch(tip_sets, "noble", RegexOptions.IgnoreCase)) { tip_sets = "noble_light"; } else { tip_sets = "light"; } }
                else if (radioButton_set_orc.Checked & radioButton_set_robe.Checked & radioButton_set_fighter.Checked)
                { if (Regex.IsMatch(tip_sets, "noble", RegexOptions.IgnoreCase)) { tip_sets = "noble_light"; } else { tip_sets = "light"; } }
                else if (radioButton_set_orc.Checked & radioButton_set_robe.Checked & radioButton_set_mage.Checked)
                { if (Regex.IsMatch(tip_sets, "noble", RegexOptions.IgnoreCase)) { tip_sets = "noble_light"; } else { tip_sets = "light"; } }
                else if (radioButton_set_orc.Checked & radioButton_set_heavy.Checked & radioButton_set_mage.Checked)
                { if (Regex.IsMatch(tip_sets, "noble", RegexOptions.IgnoreCase)) { tip_sets = "noble_light"; } else { tip_sets = "light"; } }
                else if (radioButton_set_dwarf.Checked & radioButton_set_robe.Checked)
                { if (Regex.IsMatch(tip_sets, "noble", RegexOptions.IgnoreCase)) { tip_sets = "noble_light"; } else { tip_sets = "light"; } }
                else if (radioButton_set_dwarf.Checked & radioButton_set_heavy.Checked)
                { if (Regex.IsMatch(tip_sets, "noble", RegexOptions.IgnoreCase)) { tip_sets = "noble_light"; } else { tip_sets = "light"; } }
            }
            else if (radioButton_set_s80.Checked)
            {
                if (radioButton_set_human.Checked & radioButton_set_robe.Checked & radioButton_set_fighter.Checked){ tip_sets = "light"; }
                else if (radioButton_set_human.Checked & radioButton_set_robe.Checked & radioButton_set_mage.Checked) {  tip_sets = "light";  }
                else if (radioButton_set_human.Checked & radioButton_set_heavy.Checked & radioButton_set_mage.Checked){  tip_sets = "light";  }
                else if (radioButton_set_orc.Checked & radioButton_set_robe.Checked & radioButton_set_fighter.Checked){  tip_sets = "light"; }
                else if (radioButton_set_orc.Checked & radioButton_set_robe.Checked & radioButton_set_mage.Checked){  tip_sets = "light"; }
                else if (radioButton_set_orc.Checked & radioButton_set_heavy.Checked & radioButton_set_mage.Checked){ tip_sets = "light";  }
                else if (radioButton_set_dwarf.Checked & radioButton_set_robe.Checked){ tip_sets = "light"; }
                else if (radioButton_set_dwarf.Checked & radioButton_set_heavy.Checked){ tip_sets = "light"; }
            }
            else if (radioButton_set_s.Checked)
            {
                if (radioButton_set_human.Checked & radioButton_set_robe.Checked & radioButton_set_fighter.Checked)
                { if (Regex.IsMatch(name_sets, "dynasty", RegexOptions.IgnoreCase)) { tip_sets = "light"; } }
                else if (radioButton_set_human.Checked & radioButton_set_robe.Checked & radioButton_set_mage.Checked)
                { if (Regex.IsMatch(name_sets, "dynasty", RegexOptions.IgnoreCase)) { tip_sets = "light"; } }
                else if (radioButton_set_human.Checked & radioButton_set_heavy.Checked & radioButton_set_mage.Checked)
                { if (Regex.IsMatch(name_sets, "dynasty", RegexOptions.IgnoreCase)) { tip_sets = "light"; } }
                else if (radioButton_set_orc.Checked & radioButton_set_robe.Checked & radioButton_set_fighter.Checked)
                { if (Regex.IsMatch(name_sets, "dynasty", RegexOptions.IgnoreCase)) { tip_sets = "light"; } }
                else if (radioButton_set_orc.Checked & radioButton_set_robe.Checked & radioButton_set_mage.Checked)
                { if (Regex.IsMatch(name_sets, "dynasty", RegexOptions.IgnoreCase)) { tip_sets = "light"; } }
                else if (radioButton_set_orc.Checked & radioButton_set_heavy.Checked & radioButton_set_mage.Checked)
                { if (Regex.IsMatch(name_sets, "dynasty", RegexOptions.IgnoreCase)) { tip_sets = "light"; } }
                else if (radioButton_set_dwarf.Checked & radioButton_set_robe.Checked)
                { if (Regex.IsMatch(name_sets, "dynasty", RegexOptions.IgnoreCase)) { tip_sets = "light"; } }
                else if (radioButton_set_dwarf.Checked & radioButton_set_heavy.Checked)
                { if (Regex.IsMatch(name_sets, "dynasty", RegexOptions.IgnoreCase)) { tip_sets = "light"; } }
            }
            try
            {
              pictureBox_sets.Image = Image.FromFile(Application.StartupPath
                  + @"\images\sets\" + grade + @"\" + class_name + "" + char_tip + @"\" + sex + @"\" + tip_sets + "_" + dtr_set["name_set"].ToString() + ".jpg");
              pictureBox_sets.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch
            {
                pictureBox_sets.Image = Properties.Resources.none_imge;
                pictureBox_sets.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            dtr_set.Close();
        }

        private void set_img_viev(object sender, EventArgs e)
        {
            ClassConnect tt = new ClassConnect();
            SQLiteConnection myConnection = new SQLiteConnection(tt.ToString());
            try
            {
                myConnection.Open();
            }
            catch (Exception l)
            {
                MessageBox.Show("Ошибка!\r\n (" + l.Message + ")", "Подключение к базе данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                SQLiteCommand myCommand = myConnection.CreateCommand();

                myCommand.CommandText = "SELECT * FROM armorset as ars"
                                      + " WHERE ars.id = " + dataGridView_set.CurrentRow.Cells[2].Value.ToString();
                SQLiteDataReader dataRead2 = myCommand.ExecuteReader();
                dataRead2.Read();
                Int32[] mas = { Convert.ToInt32(dataRead2["head"]), Convert.ToInt32(dataRead2["chest"]), Convert.ToInt32(dataRead2["legs"]), Convert.ToInt32(dataRead2["gloves"]), Convert.ToInt32(dataRead2["shield"]), Convert.ToInt32(dataRead2["feet"]) };
                string[] mas2 = { "head", "chest", "legs", "gloves", "shield", "feet" };                
                dataRead2.Close();

                for (int i = 0; i < mas.Length; i++)
                {
                    if (mas[i] == 0) { continue; }
                    myCommand.CommandText = "SELECT etc.icon, etc.name, etc.pDef, itd.name_ru FROM item_desc as itd"
                                       + " LEFT JOIN etcitem as etc ON etc.item_id = " + mas[i]
                                       + " WHERE itd.id = " + mas[i];
                    SQLiteDataReader dataRead = myCommand.ExecuteReader();
                    dataRead.Read();
                    if (dataRead["icon"].ToString() != "")
                    {
                        (tabPage_sets.Controls["pictureBox_set_" + mas2[i]] as PictureBox).Visible = true;
                        (tabPage_sets.Controls["pictureBox_set_" + mas2[i]] as PictureBox).Image = Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dataRead["icon"].ToString() + ".png");
                        if (radioButton_lang_ru.Checked)
                            toolTipHome.SetToolTip((tabPage_sets.Controls["pictureBox_set_" + mas2[i]] as PictureBox), dataRead["name_ru"].ToString() + "  (P.def: " + dataRead["pDef"].ToString() + ")");
                        else
                            toolTipHome.SetToolTip((tabPage_sets.Controls["pictureBox_set_" + mas2[i]] as PictureBox), dataRead["name"].ToString() + "  (P.def: " + dataRead["pDef"].ToString() + ")");
                    }
                    else
                    {
                        (tabPage_sets.Controls["pictureBox_set_" + mas2[i]] as PictureBox).Visible = false;
                    }
                    dataRead.Close();
                }
            }
            catch (Exception l)
            {
                MessageBox.Show("Ошибка!\r\n (" + l.Message + ")", "Вкладка сеты (Картинки части сета)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                myConnection.Close();
            }
        }

        private void set_info(object sender, EventArgs e)
        {
            if (checkBox_sets_vkl.Checked) { textBox_set.Text = ""; return; } // отключает показ скилов сетов
            string set_skil = "";
            string set_skil_dop = "";
            string set_skil_pvp = "";
            string set_skil_shield = "";
            ClassConnect tt = new ClassConnect();
            SQLiteConnection myConnection = new SQLiteConnection(tt.ToString());
            try
            {
                myConnection.Open();
            }
            catch (Exception l)
            {
                MessageBox.Show("Ошибка!\r\n (" + l.Message + ")", "Подключение к базе данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                SQLiteCommand myCommand = myConnection.CreateCommand();
                myCommand.CommandText = "SELECT * FROM armorset"
                                       + " WHERE id = " + dataGridView_set.CurrentRow.Cells[2].Value.ToString();
                SQLiteDataReader dataRead = myCommand.ExecuteReader();
                dataRead.Read();
                int skill_name_id = Convert.ToInt32(dataRead["skill_name_id"]);
                int skill_name_lvl = Convert.ToInt32(dataRead["skill_name_lvl"]);
                int skill_dop_id = Convert.ToInt32(dataRead["skill_dop_id"]);
                int skill_pvp_id = Convert.ToInt32(dataRead["skill_pvp_id"]);
                int shield_skill_id = Convert.ToInt32(dataRead["shield_skill_id"]);
                dataRead.Close();
                myCommand.CommandText = "SELECT desc_ru FROM skill_ru"
                                       + " WHERE skill_id = " + skill_name_id + " AND level = " + skill_name_lvl;
                set_skil = myCommand.ExecuteScalar().ToString();

                if (skill_dop_id != 0)
                {
                    myCommand.CommandText = "SELECT desc_ru FROM skill_ru"
                                          + " WHERE skill_id = " + skill_dop_id + " AND level = 1";
                    set_skil_dop = myCommand.ExecuteScalar().ToString();
                }
                if (skill_pvp_id != 0)
                {
                    myCommand.CommandText = "SELECT desc_ru FROM skill_ru"
                                          + " WHERE skill_id = " + skill_pvp_id + " AND level = 1";
                    set_skil_pvp = myCommand.ExecuteScalar().ToString();
                }
                if (shield_skill_id != 0)
                {
                    myCommand.CommandText = "SELECT desc_ru FROM skill_ru"
                                          + " WHERE skill_id = " + shield_skill_id + " AND level = 1";
                    set_skil_shield = myCommand.ExecuteScalar().ToString();
                }
                string probel = "\r\n";
                if (skill_dop_id == 0 & skill_pvp_id == 0) { probel = ""; }

                textBox_set.Text = set_skil + "\r\n" + set_skil_dop + "  " + set_skil_pvp + probel + set_skil_shield;
            }
            catch (Exception l)
            {
                MessageBox.Show("Ошибка!\r\n (" + l.Message + ")", "Вкладка сеты (информация о сете)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                myConnection.Close();
            }
        }

        private void dataGridView_set_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView_set.RowCount < 0) { return; }
            set_img(sender, e); // добовляет путь до картинки сета
            set_img_viev(sender, e); // добовляет картинки частей сета
            set_info(sender, e); // отображает информацию о сете
        }

        #endregion

        #region вкладка Магазин

        private void expander_function_shop(int this_object, int this_height) // функция раздвижного меню
        {
            panel_shop.AutoScroll = true;
            int all_buton = 12; //  Всего кнопок
            int this_indent = 27; // Отступ от кнопки в низ
            int first_position = 6; // Первая кнопка её Y положение
            int this_widht = 240; // Ширина кнопок и панелей
            for (int i = 1; i <= all_buton; i++)
            {
                (panel_shop.Controls["panel_shop_" + i.ToString()] as Panel).Size = new System.Drawing.Size(this_widht, 0);
                if (i == 1 & this_object == 1)
                {
                    (panel_shop.Controls["radioButton_shop_" + i.ToString()] as RadioButton).Location = new System.Drawing.Point(4, first_position);
                    (panel_shop.Controls["panel_shop_" + i.ToString()] as Panel).Location = new System.Drawing.Point(4, +
                    (panel_shop.Controls["radioButton_shop_" + i.ToString()] as RadioButton).Location.Y + this_indent);
                    (panel_shop.Controls["panel_shop_" + i.ToString()] as Panel).Size = new System.Drawing.Size(this_widht, this_height);
                    (panel_shop.Controls["panel_shop_" + i.ToString()] as Panel).Visible = true;
                    continue;
                }
                if (i == 1)
                {
                    (panel_shop.Controls["radioButton_shop_" + i.ToString()] as RadioButton).Location = new System.Drawing.Point(4, first_position);
                    continue;
                }
                if (this_object == i)
                {
                    (panel_shop.Controls["panel_shop_" + i.ToString()] as Panel).Location = new System.Drawing.Point(4, +
                    (panel_shop.Controls["radioButton_shop_" + (this_object).ToString()] as RadioButton).Location.Y + this_indent);

                    (panel_shop.Controls["panel_shop_" + i.ToString()] as Panel).Size = new System.Drawing.Size(this_widht, this_height);

                    (panel_shop.Controls["radioButton_shop_" + i.ToString()] as RadioButton).Location = new System.Drawing.Point(4, +
                    (panel_shop.Controls["radioButton_shop_" + (this_object - 1).ToString()] as RadioButton).Location.Y + this_indent);

                    (panel_shop.Controls["panel_shop_" + i.ToString()] as Panel).Visible = true;
                    //MessageBox.Show("" + ((groupBox_shop.Controls["radioButton_item_" + (this_object).ToString()] as RadioButton).Location.Y + this_indent) + "");
                    continue;
                }
                if (i == (this_object + 1))
                {
                    (panel_shop.Controls["radioButton_shop_" + i.ToString()] as RadioButton).Location = new System.Drawing.Point(4, +
                    (panel_shop.Controls["radioButton_shop_" + (this_object).ToString()] as RadioButton).Location.Y + this_height + this_indent);
                    (panel_shop.Controls["panel_shop_" + i.ToString()] as Panel).Visible = false;
                    continue;
                }
                (panel_shop.Controls["panel_shop_" + i.ToString()] as Panel).Visible = false;
                (panel_shop.Controls["radioButton_shop_" + i.ToString()] as RadioButton).Location = new System.Drawing.Point(4, +
                (panel_shop.Controls["radioButton_shop_" + (i - 1).ToString()] as RadioButton).Location.Y + this_indent);
            }
        }


        private void expander_button_shop(object sender, EventArgs e)
        {
            panel_shop.AutoScroll = false;
            panel_shop_seeds.Visible = false;
            panel_shop_home.Visible = true;
            string where = "";
            string CommandText = "SELECT * FROM item_desc as itm"
            + " LEFT JOIN multisell as mul ON mul.product_id = itm.id"
            + " LEFT JOIN etcitem as etc ON etc.item_id = mul.product_id";       

            if (radioButton_shop_2.Checked)
            {
                expander_function_shop(2, 155); expander_function_shop(2, 155);
                where = ((ClassAddOn)comboBox_seven_signs.SelectedItem).INDEX;
            }
            else if (radioButton_shop_3.Checked)
            {
                expander_function_shop(3, 175); expander_function_shop(3, 175);
                if (radioButton_luxsor_items.Checked)
                { where = " WHERE mul.id = 300984002"; }
                else if (radioButton_luxsor_weapon.Checked)
                { where = " WHERE mul.id = 300974001"; }
                else if (radioButton_luxsor_armor.Checked)
                { where = " WHERE mul.id = 300984001"; }
            }
            else if (radioButton_shop_4.Checked)
            {
                expander_function_shop(4, 210); expander_function_shop(4, 210);
                where = ((ClassAddOn)comboBox_adventure.SelectedItem).INDEX;
            }
            else if (radioButton_shop_5.Checked)
            {
                expander_function_shop(5, 185); expander_function_shop(5, 185);
                where = ((ClassAddOn)comboBox_shop_yang.SelectedItem).INDEX;
            }
            else if (radioButton_shop_6.Checked)
            {
                expander_function_shop(6, 190); expander_function_shop(6, 190);
                where = ((ClassAddOn)comboBox_shop_reputation.SelectedItem).INDEX;
            }
            else if (radioButton_shop_7.Checked)
            {
                expander_function_shop(7, 185); expander_function_shop(7, 185);
                where = ((ClassAddOn)comboBox_shop_kanore.SelectedItem).INDEX;
            }
            else if (radioButton_shop_8.Checked)
            {
                expander_function_shop(8, 158); expander_function_shop(8, 158);
                where = " WHERE mul.id = 325460001";
            }
            else if (radioButton_shop_9.Checked)
            {
                expander_function_shop(9, 185); expander_function_shop(9, 185);
                where = ((ClassAddOn)comboBox_shop_ishuma.SelectedItem).INDEX;
            }
            else if (radioButton_shop_10.Checked)
            {
                expander_function_shop(10, 185); expander_function_shop(10, 185);
                where = ((ClassAddOn)comboBox_shop_shadai.SelectedItem).INDEX;
            }
            else if (radioButton_shop_11.Checked)
            {
                expander_function_shop(11, 205); expander_function_shop(11, 205);
                where = ((ClassAddOn)comboBox_shop_castl.SelectedItem).INDEX;
            }
            else if (radioButton_shop_12.Checked)
            {
                expander_function_shop(12, 205); expander_function_shop(12, 205);
                where = ((ClassAddOn)comboBox_shop_hiro.SelectedItem).INDEX;
            }
            panel_shop.Refresh();
            dataGridView_shop_product.Focus();
            DataTable t_set = new DataTable();
            ClassConnect.GridView_list_mobs(t_set, CommandText + where); // Создан отдельный файл с класом коннект (обозреватель решений)
            DataTableReader dtr_set = t_set.CreateDataReader();
            int zx = t_set.Rows.Count;
            dataGridView_shop_ingredient.Columns.Clear();
            dataGridView_shop_product.Columns.Clear();
            dataGridView_shop_product.Refresh();
            dataGridView_shop_product.Columns.Add(new DataGridViewImageColumn());
            dataGridView_shop_product.Columns[0].HeaderText = "Вид";
            dataGridView_shop_product.Columns[0].Width = 40;
            dataGridView_shop_product.Columns[0].DefaultCellStyle.Padding = new Padding(3);
            dataGridView_shop_product.Columns.Add(new DataGridViewImageColumn());
            dataGridView_shop_product.Columns[1].HeaderText = "Тип";
            dataGridView_shop_product.Columns[1].Width = 40;
            dataGridView_shop_product.Columns[1].DefaultCellStyle.Padding = new Padding(3);
            dataGridView_shop_product.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_shop_product.Columns[2].HeaderText = "Кол-во";
            dataGridView_shop_product.Columns[2].ReadOnly = true;
            dataGridView_shop_product.Columns[2].Width = 50;
            dataGridView_shop_product.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_shop_product.Columns[3].HeaderText = "Название";
            dataGridView_shop_product.Columns[3].ReadOnly = true;
            dataGridView_shop_product.Columns[3].Width = 200;
            dataGridView_shop_product.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_shop_product.Columns[4].HeaderText = "Описание";
            dataGridView_shop_product.Columns[4].ReadOnly = true;
            dataGridView_shop_product.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView_shop_product.Columns[4].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView_shop_product.Columns[4].DefaultCellStyle.Padding = new Padding(2);
            dataGridView_shop_product.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView_shop_product.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_shop_product.Columns[5].Visible = false;
            dataGridView_shop_product.RowCount = zx;

            string ping_text = ClassMob.ping_text;

            for (int i = 0; i < zx; i++) // Циклзаполнения вклади клан шмот
            {
                dtr_set.Read();
                try // Если нет картинки вставяет пустую стандартную
                {
                    dataGridView_shop_product.Rows[i].Cells[0].Value = Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dtr_set["icon"].ToString() + ".png");
                    dataGridView_shop_product.Rows[i].Cells[0].ToolTipText = dtr_set["item_id"].ToString();
                }
                catch { dataGridView_shop_product.Rows[i].Cells[0].Value = Properties.Resources.none_imge; dataGridView_shop_product.Rows[i].Cells[0].ToolTipText = dtr_set["item_id"].ToString(); }
                try // Если нет картинки вставяет пустую стандартную
                {
                    dataGridView_shop_product.Rows[i].Cells[1].Value = Image.FromFile(Application.StartupPath + @"\images\crystal\" + dtr_set["crystal_type"].ToString() + ".gif");
                }
                catch { dataGridView_shop_product.Rows[i].Cells[1].Value = Properties.Resources.none_imge;}
                dataGridView_shop_product.Rows[i].Cells[2].Value = dtr_set["product_count"].ToString();

                if(radioButton_lang_ru.Checked)
                    dataGridView_shop_product.Rows[i].Cells[3].Value = dtr_set["name_ru"].ToString();
                else
                    dataGridView_shop_product.Rows[i].Cells[3].Value = dtr_set["name"].ToString();

                if (ClassMob.ping == 0)
                {
                    dataGridView_shop_product.Rows[i].Cells[4].Value = dtr_set["desc_ru"].ToString();
                }
                else
                {
                    dataGridView_shop_product.Rows[i].Cells[4].Value = ping_text;
                }
                dataGridView_shop_product.Rows[i].Cells[5].Value = dtr_set["id1"].ToString();
            }
            dtr_set.Close();
            //groupBox_shop.Refresh();
        }

        private void radioButton_merchant_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_seven_signs.Items.Clear();
            if (radioButton_merchant.Checked)
            {
                comboBox_seven_signs.Enabled = false;
                if (radioButton_lang_ru.Checked)
                {
                    comboBox_seven_signs.Items.Add(new ClassAddOn(" WHERE mul.id = 311132501", "Купить Etc товары"));
                    comboBox_seven_signs.Text = "Купить Etc товары";
                }
                else
                {
                    comboBox_seven_signs.Items.Add(new ClassAddOn(" WHERE mul.id = 311132501", "Buy Etc Items"));
                    comboBox_seven_signs.Text = "Buy Etc Items";
                }
            }
            else if (radioButton_blacksmith.Checked)
            {
                comboBox_seven_signs.Enabled = true;
                if (radioButton_lang_ru.Checked)
                {
                    comboBox_seven_signs.Items.Add(new ClassAddOn(" WHERE mul.id = 311262502", "A-grade сдвоенные"));
                    comboBox_seven_signs.Items.Add(new ClassAddOn(" WHERE mul.id = 311262501", "SA: S-Grade"));
                    comboBox_seven_signs.Items.Add(new ClassAddOn(" WHERE mul.id = 311262503", "сдвоенные S-Grade"));
                    comboBox_seven_signs.Items.Add(new ClassAddOn(" WHERE mul.id = 311262504", "распечатать S-Grade доспехи"));
                    comboBox_seven_signs.Items.Add(new ClassAddOn(" WHERE mul.id = 311262505", "распечатать S-grade Аксессуары"));
                    comboBox_seven_signs.Items.Add(new ClassAddOn(" WHERE mul.id = 311262506", "A-Grade доспехи"));
                    comboBox_seven_signs.Items.Add(new ClassAddOn(" WHERE mul.id = 311262507", "A-grade Аксессуары"));
                    comboBox_seven_signs.Items.Add(new ClassAddOn(" WHERE mul.id = 311262508", "A-grade доспехи"));

                    comboBox_seven_signs.Items.Add(new ClassAddOn(" WHERE mul.id = 311262510", "SA: A-grade"));
                    comboBox_seven_signs.Items.Add(new ClassAddOn(" WHERE mul.id = 311262511", "Оружие обновить"));
                    comboBox_seven_signs.Items.Add(new ClassAddOn(" WHERE mul.id = 311262513", "Фонд товаров"));
                    comboBox_seven_signs.Text = "A-grade сдвоенные";
                }
                else
                {
                    comboBox_seven_signs.Items.Add(new ClassAddOn(" WHERE mul.id = 311262502", "A-grade duals"));
                    comboBox_seven_signs.Items.Add(new ClassAddOn(" WHERE mul.id = 311262501", "SA: S-Grade"));
                    comboBox_seven_signs.Items.Add(new ClassAddOn(" WHERE mul.id = 311262503", "Duals S-Grade"));
                    comboBox_seven_signs.Items.Add(new ClassAddOn(" WHERE mul.id = 311262504", "Unseal S-Grade Armor"));
                    comboBox_seven_signs.Items.Add(new ClassAddOn(" WHERE mul.id = 311262505", "Unseal S-grade Accesories"));
                    comboBox_seven_signs.Items.Add(new ClassAddOn(" WHERE mul.id = 311262506", "A-Grade Armor"));
                    comboBox_seven_signs.Items.Add(new ClassAddOn(" WHERE mul.id = 311262507", "A-grade Accesories"));
                    comboBox_seven_signs.Items.Add(new ClassAddOn(" WHERE mul.id = 311262508", "A-grade Armor"));

                    comboBox_seven_signs.Items.Add(new ClassAddOn(" WHERE mul.id = 311262510", "SA: A-grade"));
                    comboBox_seven_signs.Items.Add(new ClassAddOn(" WHERE mul.id = 311262511", "Weapon Upgrade"));
                    comboBox_seven_signs.Items.Add(new ClassAddOn(" WHERE mul.id = 311262513", "Foundation Items"));
                    comboBox_seven_signs.Text = "A-grade duals";
                }
            }
            else if (radioButton_priest.Checked)
            {
                comboBox_seven_signs.Enabled = false;
                comboBox_seven_signs.Items.Add(new ClassAddOn(" WHERE mul.id = 500", "Items")); 
                comboBox_seven_signs.Text = "Items";
            }
            else if (radioButton_marketeer.Checked)
            {
                comboBox_seven_signs.Enabled = false;
                comboBox_seven_signs.Items.Add(new ClassAddOn(" WHERE mul.id = 310922002", "Black Market Items"));
                comboBox_seven_signs.Text = "Black Market Items";
            }
        }

        private void radioButton_shop_3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_lang_ru.Checked)
            {
                comboBox_adventure.Items.Clear();
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 317325002", "Коробка авантюриста"));
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 317325003", "Кристаллы жизни"));
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 320825001", "Кристалл для А оружия"));
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 320825002", "Кристалл для А доспехов"));
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 317325001", "Кристалл жизни  Шутгарт"));
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 318055002", "Кристалл жизни Гиран"));
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 318145003", "Кристалл жизни Орен"));
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 318275004", "Кристалл жизни Адэн"));
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 318375005", "Кристалл жизни Годдард"));
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 318235006", "Кристалл жизни Город охотников"));
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 318195007", "Кристалл жизни Хаен"));
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 318335008", "Кристалл жизни Руна"));
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 317885009", "Кристалл жизни Глудин"));
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 317925010", "Кристалл жизни Глудио"));
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 317975011", "Кристалл жизни Дион"));
                comboBox_adventure.Text = "Коробка авантюриста";
            }
            else
            {
                comboBox_adventure.Items.Clear();
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 317325002", "Adventurer's Box"));
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 317325003", "Exchange Life Crystals"));
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 320825001", "Crystal for A weapons"));
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 320825002", "Crystal for A armors"));
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 317325001", "Life Crystal Schuttgart"));
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 318055002", "Life Crystal Giran"));
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 318145003", "Life Crystal Oren"));
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 318275004", "Life Crystal Aden"));
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 318375005", "Life Crystal Goddard"));
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 318235006", "Life Crystal Hunters Village"));
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 318195007", "Life Crystal Heine"));
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 318335008", "Life Crystal Rune"));
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 317885009", "Life Crystal Gludin"));
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 317925010", "Life Crystal Gludio"));
                comboBox_adventure.Items.Add(new ClassAddOn(" WHERE mul.id = 317975011", "Life Crystal Dion"));
                comboBox_adventure.Text = "Adventurer's Box";
            }
        }

        private void dataGridView_shop_product_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ClassConnect tt = new ClassConnect();
            SQLiteConnection myConnection = new SQLiteConnection(tt.ToString());
            try
            {
                myConnection.Open();
            }
            catch (Exception l)
            {
                MessageBox.Show("Ошибка!\r\n (" + l.Message + ")", "Подключение к базе данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SQLiteCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "SELECT * FROM multisell"
                                  + " WHERE id = " + dataGridView_shop_product.CurrentRow.Cells[5].Value
                                  + " AND product_id = " + dataGridView_shop_product.CurrentRow.Cells[0].ToolTipText;
            SQLiteDataReader dataRead0 = myCommand.ExecuteReader();
            dataRead0.Read();
            string[] ingredient_id = Regex.Split(dataRead0["ingredient_id"].ToString(), @"	");
            string[] ingredient_count = Regex.Split(dataRead0["ingredient_count"].ToString(), @"	");
            dataRead0.Close();

            dataGridView_shop_ingredient.Columns.Clear();
            dataGridView_shop_ingredient.Refresh();
            dataGridView_shop_ingredient.Columns.Add(new DataGridViewImageColumn());
            dataGridView_shop_ingredient.Columns[0].HeaderText = "Вид";
            dataGridView_shop_ingredient.Columns[0].Width = 40;
            dataGridView_shop_ingredient.Columns[0].DefaultCellStyle.Padding = new Padding(3);
            dataGridView_shop_ingredient.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_shop_ingredient.Columns[1].HeaderText = "Кол-во";
            dataGridView_shop_ingredient.Columns[1].ReadOnly = true;
            dataGridView_shop_ingredient.Columns[1].Width = 100;
            dataGridView_shop_ingredient.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_shop_ingredient.Columns[2].HeaderText = "Название";
            dataGridView_shop_ingredient.Columns[2].ReadOnly = true;
            dataGridView_shop_ingredient.Columns[2].Width = 200;
            dataGridView_shop_ingredient.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView_shop_ingredient.Columns[3].HeaderText = "Описание";
            dataGridView_shop_ingredient.Columns[3].ReadOnly = true;
            dataGridView_shop_ingredient.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView_shop_ingredient.Columns[3].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView_shop_ingredient.Columns[3].DefaultCellStyle.Padding = new Padding(2);
            dataGridView_shop_ingredient.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dataGridView_shop_ingredient.RowCount = ingredient_id.Length;

            string ping_text = ClassMob.ping_text;

            for (int i = 0; i < ingredient_id.Length; i++) 
            {
                myCommand.CommandText = "SELECT * FROM item_desc as itm"
                                      + " LEFT JOIN etcitem as etc ON etc.item_id = itm.id"
                                      + " WHERE itm.id = " + ingredient_id[i];
                SQLiteDataReader dtr = myCommand.ExecuteReader();
                dtr.Read();
                try // Если нет картинки вставяет пустую стандартную
                {
                    dataGridView_shop_ingredient.Rows[i].Cells[0].Value = Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dtr["icon"].ToString() + ".png");
                    dataGridView_shop_ingredient.Rows[i].Cells[0].ToolTipText = dtr["item_id"].ToString();
                }
                catch { dataGridView_shop_ingredient.Rows[i].Cells[0].Value = Properties.Resources.none_imge; dataGridView_shop_ingredient.Rows[i].Cells[0].ToolTipText = dtr["item_id"].ToString(); }
                dataGridView_shop_ingredient.Rows[i].Cells[1].Value = Convert.ToInt32(ingredient_count[i]).ToString("### ### ###");
                if (radioButton_lang_ru.Checked)
                    dataGridView_shop_ingredient.Rows[i].Cells[2].Value = dtr["name_ru"].ToString();
                else
                    dataGridView_shop_ingredient.Rows[i].Cells[2].Value = dtr["name"].ToString();
                if (ClassMob.ping == 0)
                {
                dataGridView_shop_ingredient.Rows[i].Cells[3].Value = dtr["desc_ru"].ToString();
                }
                else
                {
                    dataGridView_shop_ingredient.Rows[i].Cells[3].Value = ping_text;
                }
                dtr.Close();
            }
            dataGridView_shop_ingredient.ClearSelection();
            myConnection.Close();
        }

        private void pictureBox13_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClassMob.Info_Id = 30098;
            ClassMob.Info_level =70;
            ClassMob.Info_Name = "Спец Магазин (Luxor shop)";
            ClassMob.Map_count = 1;
            mob_map form2 = new mob_map();
            form2.Text = "Название магазина - (" + ClassMob.Info_Name + ")";
            form2.ShowDialog();
        }

        private void expander_button_shop_seed_all(object sender, EventArgs e)
        {
            panel_shop.AutoScroll = false;
            panel_shop_seeds.Visible = true;
            panel_shop_home.Visible = false;
            if (radioButton_manor_cl.Checked)
            {
                checkBox_manor_alternative.Enabled = false;

                ClassConnect tt = new ClassConnect();
                SQLiteConnection myConnection = new SQLiteConnection(tt.ToString());
                try
                {
                    myConnection.Open();
                }
                catch (Exception l)
                {
                    MessageBox.Show("Ошибка!\r\n (" + l.Message + ")", "Подключение к базе данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                SQLiteCommand myCommand = myConnection.CreateCommand();
                DataTable t_seed = new DataTable();
                ClassConnect.GridView_list_mobs(t_seed, "SELECT * FROM multisell WHERE id = 35098000" + ((ClassAddOn)comboBox_shop_seeds.SelectedItem).INDEX); // Создан отдельный файл с класом коннект (обозреватель решений)

                dataGridView_shop_seeds.Columns.Clear();
                dataGridView_shop_seeds.DataSource = t_seed;
                for (int i = 0; i < 5; i++)
                { dataGridView_shop_seeds.Columns[i].Visible = false; }

                if (radioButton_shop_1.Checked)
                {
                    expander_function_shop(1, 259); expander_function_shop(1, 259);
                }
                panel_shop.Refresh();
                //myCommand.CommandText = "SELECT COUNT(*) FROM seeds";
                //int cooun = Convert.ToInt32(myCommand.ExecuteScalar());
                int zx = t_seed.Rows.Count;
                dataGridView_shop_seeds.Columns.Add(new DataGridViewImageColumn());
                dataGridView_shop_seeds.Columns[5].HeaderText = "вид";
                dataGridView_shop_seeds.Columns[5].Width = 40;
                dataGridView_shop_seeds.Columns[5].DefaultCellStyle.Padding = new Padding(3);
                dataGridView_shop_seeds.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridView_shop_seeds.Columns[6].HeaderText = "Продукты";
                dataGridView_shop_seeds.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView_shop_seeds.Columns[6].ReadOnly = true;
                dataGridView_shop_seeds.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_shop_seeds.Columns[6].Width = 140;
                dataGridView_shop_seeds.Columns.Add(new DataGridViewImageColumn());
                dataGridView_shop_seeds.Columns[7].HeaderText = "вид";
                dataGridView_shop_seeds.Columns[7].Width = 40;
                dataGridView_shop_seeds.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridView_shop_seeds.Columns[8].HeaderText = "Плоды";
                dataGridView_shop_seeds.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_shop_seeds.Columns[8].ReadOnly = true;
                dataGridView_shop_seeds.Columns[8].Width = 140;
                dataGridView_shop_seeds.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridView_shop_seeds.Columns[9].HeaderText = "кол-во";
                dataGridView_shop_seeds.Columns[9].ReadOnly = true;
                dataGridView_shop_seeds.Columns[9].Width = 60;
                dataGridView_shop_seeds.Columns.Add(new DataGridViewImageColumn());
                dataGridView_shop_seeds.Columns[10].HeaderText = "вид";
                dataGridView_shop_seeds.Columns[10].Width = 40;
                dataGridView_shop_seeds.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridView_shop_seeds.Columns[11].HeaderText = "цена";
                dataGridView_shop_seeds.Columns[11].ReadOnly = true;
                dataGridView_shop_seeds.Columns[11].Width = 60;

                for (int i = 0; i < zx; i++) // Циклзаполнения вклади клан шмот
                {
                    myCommand.CommandText = "SELECT item_id, name_ru, name, icon FROM etcitem "
                                         + " LEFT JOIN item_desc as itd ON itd.id = item_id"
                                         + " WHERE item_id =" + dataGridView_shop_seeds.Rows[i].Cells[1].Value;
                    SQLiteDataReader dataRead = myCommand.ExecuteReader();
                    dataRead.Read();
                    try // Если нет картинки вставяет пустую стандартную
                    {
                        dataGridView_shop_seeds.Rows[i].Cells[5].Value = Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dataRead["icon"].ToString() + ".png");
                        dataGridView_shop_seeds.Rows[i].Cells[5].ToolTipText = dataRead["item_id"].ToString();
                    }
                    catch { dataGridView_shop_seeds.Rows[i].Cells[5].Value = Properties.Resources.none_imge; dataGridView_shop_seeds.Rows[i].Cells[5].ToolTipText = dataRead["item_id"].ToString(); }
                    
                    if (radioButton_lang_ru.Checked)
                        dataGridView_shop_seeds.Rows[i].Cells[6].Value = dataRead["name_ru"].ToString();
                    else
                        dataGridView_shop_seeds.Rows[i].Cells[6].Value = dataRead["name"].ToString();

                    dataRead.Close();

                    string[] ingredient_id = Regex.Split(dataGridView_shop_seeds.Rows[i].Cells[3].Value.ToString(), @"	");
                    string[] ingredient_count = Regex.Split(dataGridView_shop_seeds.Rows[i].Cells[4].Value.ToString(), @"	");

                    myCommand.CommandText = "SELECT item_id, name, name_ru, icon FROM etcitem"
                                          + " LEFT JOIN item_desc as itd ON itd.id = item_id"
                                          + " WHERE item_id =" + ingredient_id[0];
                    SQLiteDataReader dataRead1 = myCommand.ExecuteReader();
                    dataRead1.Read();
                    try // Если нет картинки вставяет пустую стандартную
                    {
                        dataGridView_shop_seeds.Rows[i].Cells[7].Value = Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dataRead1["icon"].ToString() + ".png");
                        dataGridView_shop_seeds.Rows[i].Cells[7].ToolTipText = dataRead1["item_id"].ToString();
                    }
                    catch { dataGridView_shop_seeds.Rows[i].Cells[7].Value = Properties.Resources.none_imge; dataGridView_shop_seeds.Rows[i].Cells[7].ToolTipText = dataRead1["item_id"].ToString(); }
                    
                    if (radioButton_lang_ru.Checked)
                        dataGridView_shop_seeds.Rows[i].Cells[8].Value = dataRead1["name_ru"].ToString();
                    else
                        dataGridView_shop_seeds.Rows[i].Cells[8].Value = dataRead1["name"].ToString();
                    
                    dataRead1.Close();
                    dataGridView_shop_seeds.Rows[i].Cells[9].Value = ingredient_count[0];

                    try // Если нет картинки вставяет пустую стандартную
                    {
                        dataGridView_shop_seeds.Rows[i].Cells[10].Value = Image.FromFile(Application.StartupPath + @"\images\etcitem\etc_adena_i00.png");
                    }
                    catch { dataGridView_shop_seeds.Rows[i].Cells[10].Value = Properties.Resources.none_imge; }
                    dataGridView_shop_seeds.Rows[i].Cells[11].Value = ingredient_count[1];
                }
                myConnection.Close();
            }
            else if (radioButton_manor_mass.Checked)
            {
                checkBox_manor_alternative.Enabled = true;
                ClassConnect tt = new ClassConnect();
                SQLiteConnection myConnection = new SQLiteConnection(tt.ToString());
                try
                {
                    myConnection.Open();
                }
                catch (Exception l)
                {
                    MessageBox.Show("Ошибка!\r\n (" + l.Message + ")", "Подключение к базе данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                SQLiteCommand myCommand = myConnection.CreateCommand();
                int alternative = 0;
                if (checkBox_manor_alternative.Checked)
                { alternative = 1; }

                DataTable t_seed = new DataTable();
                ClassConnect.GridView_list_mobs(t_seed, "SELECT * FROM seeds WHERE alternative = " + alternative + " AND castle = " + ((ClassAddOn)comboBox_shop_seeds.SelectedItem).INDEX); // Создан отдельный файл с класом коннект (обозреватель решений)               
                dataGridView_shop_seeds.Columns.Clear();
                dataGridView_shop_seeds.DataSource = t_seed;
                for (int i = 0; i < 10; i++)
                { dataGridView_shop_seeds.Columns[i].Visible = false; }

                panel_shop_seeds.Visible = true;
                if (radioButton_shop_1.Checked)
                {
                    expander_function_shop(1, 259); expander_function_shop(1, 259);
                }
                
                //myCommand.CommandText = "SELECT COUNT(*) FROM seeds";
                //int cooun = Convert.ToInt32(myCommand.ExecuteScalar());
                int zx = t_seed.Rows.Count;
                dataGridView_shop_seeds.Columns.Add(new DataGridViewImageColumn());
                dataGridView_shop_seeds.Columns[10].HeaderText = "вид";
                dataGridView_shop_seeds.Columns[10].Width = 40;
                dataGridView_shop_seeds.Columns[10].DefaultCellStyle.Padding = new Padding(3);
                dataGridView_shop_seeds.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridView_shop_seeds.Columns[11].HeaderText = "Семечки";
                dataGridView_shop_seeds.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView_shop_seeds.Columns[11].ReadOnly = true;
                dataGridView_shop_seeds.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_shop_seeds.Columns[11].Width = 140;
                dataGridView_shop_seeds.Columns.Add(new DataGridViewImageColumn());
                dataGridView_shop_seeds.Columns[12].HeaderText = "вид";
                dataGridView_shop_seeds.Columns[12].Width = 40;
                dataGridView_shop_seeds.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridView_shop_seeds.Columns[13].HeaderText = "Плоды";
                dataGridView_shop_seeds.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_shop_seeds.Columns[13].ReadOnly = true;
                dataGridView_shop_seeds.Columns[13].Width = 140;
                dataGridView_shop_seeds.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridView_shop_seeds.Columns[14].HeaderText = "Лвл";
                dataGridView_shop_seeds.Columns[14].ReadOnly = true;
                dataGridView_shop_seeds.Columns[14].Width = 50;
                dataGridView_shop_seeds.Columns.Add(new DataGridViewImageColumn());
                dataGridView_shop_seeds.Columns[15].HeaderText = "вид";
                dataGridView_shop_seeds.Columns[15].Width = 40;
                dataGridView_shop_seeds.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridView_shop_seeds.Columns[16].HeaderText = "Награда-1";
                dataGridView_shop_seeds.Columns[16].ReadOnly = true;
                dataGridView_shop_seeds.Columns[16].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_shop_seeds.Columns[16].Width = 100;
                dataGridView_shop_seeds.Columns.Add(new DataGridViewImageColumn());
                dataGridView_shop_seeds.Columns[17].HeaderText = "вид";
                dataGridView_shop_seeds.Columns[17].Width = 40;
                dataGridView_shop_seeds.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridView_shop_seeds.Columns[18].HeaderText = "Награда-2";
                dataGridView_shop_seeds.Columns[18].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_shop_seeds.Columns[18].ReadOnly = true;
                dataGridView_shop_seeds.Columns[18].Width = 100;

                for (int i = 0; i < zx; i++) // Циклзаполнения вклади клан шмот
                {
                    myCommand.CommandText = "SELECT item_id, name, name_ru, icon FROM etcitem"
                                          + " LEFT JOIN item_desc as itd ON itd.id = item_id"
                                          + " WHERE item_id =" + dataGridView_shop_seeds.Rows[i].Cells[2].Value;
                    SQLiteDataReader dataRead = myCommand.ExecuteReader();
                    dataRead.Read();
                    try // Если нет картинки вставяет пустую стандартную
                    {
                        dataGridView_shop_seeds.Rows[i].Cells[10].Value = Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dataRead["icon"].ToString() + ".png");
                        dataGridView_shop_seeds.Rows[i].Cells[10].ToolTipText = dataRead["item_id"].ToString();
                    }
                    catch { dataGridView_shop_seeds.Rows[i].Cells[10].Value = Properties.Resources.none_imge; dataGridView_shop_seeds.Rows[i].Cells[10].ToolTipText = dataRead["item_id"].ToString(); }

                    if (radioButton_lang_ru.Checked)
                        dataGridView_shop_seeds.Rows[i].Cells[11].Value = dataRead["name_ru"].ToString();
                    else
                        dataGridView_shop_seeds.Rows[i].Cells[11].Value = dataRead["name"].ToString();

                    dataRead.Close();

                    myCommand.CommandText = "SELECT item_id, name, name_ru, icon FROM etcitem"
                                          + " LEFT JOIN item_desc as itd ON itd.id = item_id"
                                          + " WHERE item_id =" + dataGridView_shop_seeds.Rows[i].Cells[1].Value;
                    SQLiteDataReader dataRead2 = myCommand.ExecuteReader();
                    dataRead2.Read();
                    try // Если нет картинки вставяет пустую стандартную
                    {
                        dataGridView_shop_seeds.Rows[i].Cells[12].Value = Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dataRead2["icon"].ToString() + ".png");
                        dataGridView_shop_seeds.Rows[i].Cells[12].ToolTipText = dataRead2["item_id"].ToString();
                    }
                    catch { dataGridView_shop_seeds.Rows[i].Cells[12].Value = Properties.Resources.none_imge; dataGridView_shop_seeds.Rows[i].Cells[12].ToolTipText = dataRead2["item_id"].ToString(); }
                    if (radioButton_lang_ru.Checked)
                        dataGridView_shop_seeds.Rows[i].Cells[13].Value = dataRead2["name_ru"].ToString();
                    else
                        dataGridView_shop_seeds.Rows[i].Cells[13].Value = dataRead2["name"].ToString();
                    dataRead2.Close();

                    dataGridView_shop_seeds.Rows[i].Cells[14].Value = dataGridView_shop_seeds.Rows[i].Cells[7].Value + " - " + (Convert.ToInt32(dataGridView_shop_seeds.Rows[i].Cells[7].Value) + 10);

                    myCommand.CommandText = "SELECT item_id, name, name_ru, icon FROM etcitem"
                                          + " LEFT JOIN item_desc as itd ON itd.id = item_id"
                                          + " WHERE item_id =" + dataGridView_shop_seeds.Rows[i].Cells[4].Value;
                    SQLiteDataReader dataRead3 = myCommand.ExecuteReader();
                    dataRead3.Read();
                    try // Если нет картинки вставяет пустую стандартную
                    {
                        dataGridView_shop_seeds.Rows[i].Cells[15].Value = Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dataRead3["icon"].ToString() + ".png");
                        dataGridView_shop_seeds.Rows[i].Cells[15].ToolTipText = dataRead3["item_id"].ToString();
                    }
                    catch { dataGridView_shop_seeds.Rows[i].Cells[15].Value = Properties.Resources.none_imge; dataGridView_shop_seeds.Rows[i].Cells[15].ToolTipText = dataRead3["item_id"].ToString(); }
                    if (radioButton_lang_ru.Checked)
                        dataGridView_shop_seeds.Rows[i].Cells[16].Value = dataRead3["name_ru"].ToString();
                    else
                        dataGridView_shop_seeds.Rows[i].Cells[16].Value = dataRead3["name"].ToString();
                    dataRead3.Close();

                    myCommand.CommandText = "SELECT item_id, name, name_ru, icon FROM etcitem"
                                          + " LEFT JOIN item_desc as itd ON itd.id = item_id"
                                          + " WHERE item_id =" + dataGridView_shop_seeds.Rows[i].Cells[5].Value;
                    SQLiteDataReader dataRead4 = myCommand.ExecuteReader();
                    dataRead4.Read();
                    try // Если нет картинки вставяет пустую стандартную
                    {
                        dataGridView_shop_seeds.Rows[i].Cells[17].Value = Image.FromFile(Application.StartupPath + @"\images\etcitem\" + dataRead4["icon"].ToString() + ".png");
                        dataGridView_shop_seeds.Rows[i].Cells[17].ToolTipText = dataRead4["item_id"].ToString();
                    }
                    catch { dataGridView_shop_seeds.Rows[i].Cells[17].Value = Properties.Resources.none_imge; dataGridView_shop_seeds.Rows[i].Cells[17].ToolTipText = dataRead4["item_id"].ToString(); }
                    if (radioButton_lang_ru.Checked)
                        dataGridView_shop_seeds.Rows[i].Cells[18].Value = dataRead4["name_ru"].ToString();
                    else
                        dataGridView_shop_seeds.Rows[i].Cells[18].Value = dataRead4["name"].ToString();
                    dataRead4.Close();
                }
                dataGridView_shop_seeds.Focus();
                myConnection.Close();
                
            }
        }

        private void pictureBox_shop_yang_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClassMob.Info_Id = 32326;
            ClassMob.Info_level = 70;
            ClassMob.Info_Name = "Attribute Master Yang (Учитель Ян)";
            ClassMob.Map_count = 1;
            mob_map form2 = new mob_map();
            form2.Text = "Название магазина - (" + ClassMob.Info_Name + ")";
            form2.ShowDialog();
        }

        private void pictureBox_shop_rapidus_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClassMob.Info_Id = 36479;
            ClassMob.Info_level = 70;
            ClassMob.Info_Name = "Reputation Manager Rapidus (Управляющий Репутацией)";
            ClassMob.Map_count = 1;
            mob_map form2 = new mob_map();
            form2.Text = "Название магазина - (" + ClassMob.Info_Name + ")";
            form2.ShowDialog();
        }

        private void pictureBox_shop_scipio_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClassMob.Info_Id = 36480;
            ClassMob.Info_level = 70;
            ClassMob.Info_Name = "Reputation Manager Scipio (Управляющий Репутацией)";
            ClassMob.Map_count = 1;
            mob_map form2 = new mob_map();
            form2.Text = "Название магазина - (" + ClassMob.Info_Name + ")";
            form2.ShowDialog();
        }

        private void pictureBox_shop_kanore_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClassMob.Info_Id = 32610;
            ClassMob.Info_level = 70;
            ClassMob.Info_Name = "Weaver Olf Kanore (Ольф Каноре)";
            ClassMob.Map_count = 1;
            mob_map form2 = new mob_map();
            form2.Text = "Название магазина - (" + ClassMob.Info_Name + ")";
            form2.ShowDialog();
        }

        private void pictureBox_shop_asyatei_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClassMob.Info_Id = 32546;
            ClassMob.Info_level = 70;
            ClassMob.Info_Name = "Asyatei (Асъятэ Торговец Душами)";
            ClassMob.Map_count = 1;
            mob_map form2 = new mob_map();
            form2.Text = "Название магазина - (" + ClassMob.Info_Name + ")";
            form2.ShowDialog();
        }

        private void pictureBox_shop_ishuma_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClassMob.Info_Id = 32615;
            ClassMob.Info_level = 70;
            ClassMob.Info_Name = "Maestro Ishuma (Мастер Ишума )";
            ClassMob.Map_count = 1;
            mob_map form2 = new mob_map();
            form2.Text = "Название магазина - (" + ClassMob.Info_Name + ")";
            form2.ShowDialog();
        }

        private void pictureBox_shop_shadai_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClassMob.Info_Id = 32347;
            ClassMob.Info_level = 70;
            ClassMob.Info_Name = "Legendary Blacksmith Shadai (Легендарный Кузнец Шадай)";
            ClassMob.Map_count = 1;
            mob_map form2 = new mob_map();
            form2.Text = "Название магазина - (" + ClassMob.Info_Name + ")";
            form2.ShowDialog();
        }

        #endregion

        #region вкладка настройка

        private void linkLabel_setting_info_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"http://l2mega.net");
        }

        private void button_seting_save_Click(object sender, EventArgs e)
        {
            try
            {
                int lang = 0;
                if (radioButton_lang_ru.Checked)
                {
                    lang = 1;
                }
                else
                {
                    lang = 0;
                }
                string CommandText = "UPDATE config_settings  SET xp = " + numericUpDown_seting_xp.Value + ", sp =" + numericUpDown_seting_sp.Value
                                      + ", drops = " + numericUpDown_seting_drop.Value + ", spoil = " + numericUpDown_seting_spoil.Value
                                      + ", dropRb = " + numericUpDown_seting_dropRb.Value + ", adena = " + numericUpDown_seting_adena.Value
                                      + ", language = " + lang + ", seting_z = " + numericUpDown_seting_z.Value + "";
                DataTable t_conf = new DataTable();
                ClassConnect.GridView_list_mobs(t_conf, CommandText); // Создан отдельный файл с класом коннект (обозреватель решений) 
            }
            catch (Exception l)
            {
                MessageBox.Show("Ошибка!\r\n (" + l.Message + ")", "Сохранения настроек", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            button_seting_save.Enabled = false;
            ClassMob.Seting_z = Convert.ToInt32(numericUpDown_seting_z.Value);
        }

        private void button_seting_loading()
        {
            try
            {
                string CommandText = "SELECT * FROM config_settings ";
                DataTable t_conf = new DataTable();
                ClassConnect.GridView_list_mobs(t_conf, CommandText); // Создан отдельный файл с класом коннект (обозреватель решений)
                DataTableReader dtr_conf = t_conf.CreateDataReader();
                dtr_conf.Read();
                numericUpDown_seting_xp.Value = Convert.ToInt32(dtr_conf["xp"]);
                numericUpDown_seting_sp.Value = Convert.ToInt32(dtr_conf["sp"]);
                numericUpDown_seting_drop.Value = Convert.ToInt32(dtr_conf["drops"]);
                numericUpDown_seting_spoil.Value = Convert.ToInt32(dtr_conf["spoil"]);
                numericUpDown_seting_dropRb.Value = Convert.ToInt32(dtr_conf["dropRb"]);
                numericUpDown_seting_adena.Value = Convert.ToInt32(dtr_conf["adena"]);
                numericUpDown_seting_z.Value = Convert.ToInt32(dtr_conf["seting_z"]);
                if (Convert.ToInt32(dtr_conf["language"]) == 1)
                {
                    radioButton_lang_ru.Checked = true;
                    ClassMob.Seting_lang = 1;
                }
                else
                {
                    radioButton_lang_ru.Checked = false;
                }
                if (Convert.ToInt32(dtr_conf["language"]) == 0)
                {
                    ClassMob.Seting_lang = 0;
                    radioButton_lang_en.Checked = true;
                }
                else
                {
                    radioButton_lang_en.Checked = false;
                }
                dtr_conf.Close();
            }
            catch (Exception l)
            {
                MessageBox.Show("Ошибка!\r\n (" + l.Message + ")", "Загрузки настроек", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void numericUpDown_seting_xp_ValueChanged(object sender, EventArgs e)
        {
            ClassMob.Seting_xp = Convert.ToInt32(numericUpDown_seting_xp.Value);
            ClassMob.Seting_sp = Convert.ToInt32(numericUpDown_seting_sp.Value);
            button_seting_save.Enabled = true;
        }

        private void numericUpDown_seting_xp_Click(object sender, EventArgs e)
        {
            button_seting_save.Enabled = true;
        }

        private void radioButton_lang_ru_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_lang_ru.Checked)
                ClassMob.Seting_lang = 1;
            else
                ClassMob.Seting_lang = 0;
        }

        private void pictureBox_inventar_ful_Paint(object sender, PaintEventArgs e)
        {
            Bitmap bmp;
            try
            {
                bmp = new Bitmap(Application.StartupPath + @"\images\my_character\character.png");
            }
            catch
            {
                bmp = new Bitmap(Properties.Resources.character);
            }
            //bmp.MakeTransparent(Color.White);
            Rectangle rect = new Rectangle(50, 68, 120, 270);
            e.Graphics.DrawImage((Image)bmp, rect);
        }

        private void button_seting_char_Click(object sender, EventArgs e)
        {
            if (openFileDialog_seting_char.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Image image = Image.FromFile(openFileDialog_seting_char.FileName);
                    Bitmap bmp2 = new Bitmap(image, new Size(120, 270));
                    //bmp2.MakeTransparent(Color.White);
                    image.Dispose();
                    bmp2.Save(Application.StartupPath + @"\images\my_character\character.png");
                }
                catch (Exception l)
                {
                    MessageBox.Show("Ошибка! (Фотографии чара)\r\n (" + l.Message + ")", "ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void button_seting_char_clean_Click(object sender, EventArgs e)
        {
            try
            {
                System.IO.File.Delete(Application.StartupPath + @"\images\my_character\character.png");
            }
            catch { }
        }

        #endregion

        #region Реклама

        int iNumber = 0;
        private void DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e) //выводит в pictureBoxReclama картинку и даёт ссылку
        {
            try
            {
                string sBuffer = e.Result;
                List<string> MyLinks = new List<string>();
                List<string> MyLinks2 = new List<string>();

                while (common.BlockExist(sBuffer))
                {
                    MyLinks.Add(common.GetString(ref sBuffer));
                    MyLinks2.Add(common.GetString2(ref sBuffer));
                }

                //Random RND = new Random(DateTime.Now.Millisecond);
                //iNumber = RND.Next(MyLinks.Count);
                pictureBoxReclama.ImageLocation = "http://" + MyLinks[iNumber];
                pictureBoxReclama.Tag = "http://" + MyLinks2[iNumber];
                MyLinks.RemoveAt(0);
                iNumber++;
            }
            catch (Exception)
            {
                iNumber = 0;
                //pictureBox2.Image = Properties.Resources._1277558650_lineage2; // выводит в пиктуребокс изображение с ресурса
            }
        }

        private void timerReclama_Tick(object sender, EventArgs e) // запускает процес рекламы
        {
            //try
            //{
            //    MyRequest = new WebClient();
            //    MyRequest.DownloadStringCompleted += DownloadStringCompleted; // передаёт рули функции
            //    MyRequest.DownloadStringAsync(new Uri("http://www.l2mega.net/download/global.txt")); // передаёт путь до файли где лежит реклама
            //}
            //catch { }
            //timerReclama.Interval = 20000;
        }

        private void pictureBoxReclama_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(((PictureBox)sender).Tag.ToString());
            }
            catch
            {}
        }


        private void pictureBoxReclama2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://l2mega.net/");
        }


        private void linkLabel_l2mega_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://l2mega.net/");
        }

        #endregion

        #region Новости + проверка пинга до сайта

        string info_sevis = "";
        private void timer_info_sevis_Tick(object sender, EventArgs e)
        {
            try
            {
                timer_info_sevis.Interval = 10000;
                DataTable t_info = new DataTable();
                string CommandText = "SELECT * FROM config_settings ";
                try
                {
                    ClassConnect.GridView_list_mobs(t_info, CommandText); // Создан отдельный файл с класом коннект (обозреватель решений)
                }
                catch { return; }
                    DataTableReader dtr_info = t_info.CreateDataReader();
                dtr_info.Read();
                info_sevis = dtr_info["info_servis"].ToString();
                string start_info = dtr_info["star_info"].ToString();
                dtr_info.Close();
                Thread.Sleep(100);
                MyRequest2 = new WebClient();
                MyRequest2.DownloadStringCompleted += Download_Info; // передаёт рули функции
                MyRequest2.DownloadStringAsync(new Uri("http://www.l2mega.net/download/info/global.txt")); // передаёт путь до файли где лежит реклама
                Thread.Sleep(200);
                string info_news = ClassMob.info_news;
                if (info_sevis == info_news) { return; }
                if (info_news == null) { return; }
                string CommandText2 = "UPDATE config_settings  SET info_servis = '" + info_news + "'";
                DataTable t_conf = new DataTable();
                ClassConnect.GridView_list_mobs(t_conf, CommandText2); // Создан отдельный файл с класом коннект (обозреватель решений) 

                if (start_info == "0")
                {
                    string CommandText3 = "UPDATE config_settings  SET star_info = 1";
                    DataTable t_conf2 = new DataTable();
                    ClassConnect.GridView_list_mobs(t_conf2, CommandText3); // Создан отдельный файл с класом коннект (обозреватель решений) 
                    return;
                }
                Form_info form2 = new Form_info();
                form2.Text = "Новости l2mega.net";
                form2.ShowDialog();
                ClassMob.info_news = null;
            }
            catch
            {}
        }

        int iNumber_info = 0;
        private void Download_Info(object sender, DownloadStringCompletedEventArgs e) //выводит в pictureBoxReclama картинку и даёт ссылку
        {
            try
            {
                string sBuffer = e.Result;
                List<string> MyLinks = new List<string>();
                List<string> MyLinks2 = new List<string>();
                List<string> MyLinks3 = new List<string>();

                while (common.BlockExist(sBuffer))
                {
                    MyLinks.Add(common.GetString(ref sBuffer));
                    MyLinks2.Add(common.GetString2(ref sBuffer));
                    MyLinks3.Add(common.GetString3(ref sBuffer));
                }

                ClassMob.info_webBrowser = MyLinks[iNumber_info];
                ClassMob.info_linkLabel = MyLinks2[iNumber_info];
                ClassMob.info_news = MyLinks3[iNumber_info];
                MyLinks.RemoveAt(0);
                iNumber_info++;
            }
            catch (Exception)
            {
                iNumber_info = 0;
            }
        }

        private void timer_test_ping_Tick(object sender, EventArgs e)
        {
            //System.Net.NetworkInformation.IPStatus status = System.Net.NetworkInformation.IPStatus.Unknown;
            //try
            //{
            //    status = new System.Net.NetworkInformation.Ping().Send("l2mega.net").Status;
            //}
            //catch { ClassMob.ping = 1; }
            //if (status == System.Net.NetworkInformation.IPStatus.Success)
            //{
            //    ClassMob.ping = 0;
            //}
            //else
            //{
            //    ClassMob.ping = 1;
            //}
            //timer_test_ping.Interval = 30000;
        }

        #endregion






    }
}

namespace ASC.Data.SQLite
{

    /// <summary>
    /// Класс переопределяет функцию Lower() в SQLite, т.к. встроенная функция некорректно работает с символами > 128
    /// </summary>
    [SQLiteFunction(Name = "lower", Arguments = 1, FuncType = FunctionType.Scalar)]
    public class LowerFunction : SQLiteFunction
    {

        /// <summary>
        /// Вызов скалярной функции Lower().
        /// </summary>
        /// <param name="args">Параметры функции</param>
        /// <returns>Строка в нижнем регистре</returns>
        public override object Invoke(object[] args)
        {
            if (args.Length == 0 || args[0] == null) return null;
            return ((string)args[0]).ToLower();
        }
    }

    /// <summary>
    /// Класс переопределяет функцию Upper() в SQLite, т.к. встроенная функция некорректно работает с символами > 128
    /// </summary>
    [SQLiteFunction(Name = "upper", Arguments = 1, FuncType = FunctionType.Scalar)]
    public class UpperFunction : SQLiteFunction
    {

        /// <summary>
        /// Вызов скалярной функции Upper().
        /// </summary>
        /// <param name="args">Параметры функции</param>
        /// <returns>Строка в верхнем регистре</returns>
        public override object Invoke(object[] args)
        {
            if (args.Length == 0 || args[0] == null) return null;
            return ((string)args[0]).ToUpper();
        }
    }
} //Переопределяет lower Upper дайт возможность поиска в sql коректно руских слов
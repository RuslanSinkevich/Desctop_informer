using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;

namespace l2mega_informer
{

    public partial class mob_map : Form
    {

        int[] MasX;
        int[] MasY;
        int[] MasZ;
        string[] location;
        int HereticX = 470;
        int HereticY = 2000;
        int ForbiddenX = 480;
        int ForbiddenY = 1680;
        int ApostateX = 475;
        int ApostateY = 1620;
        public mob_map()
        {
            InitializeComponent();
            if (ClassMob.Seting_lang == 0)
            {
                this.pictureBox_map.Image = global::l2mega_informer.Properties.Resources.map_en;
            }
            else
            {
                this.pictureBox_map.Image = global::l2mega_informer.Properties.Resources.map_ru;
            }

            try
            {
                //this.toolTip1.SetToolTip(this.pictureBox1, "Имя моба - (" + ClassMob.Info_Name + ")\r\nлвл моба - (" + ClassMob.Info_level + ")\r\nколичество  точек респа - (" + ClassMob.Map_count + ")");

                MasX = new int[ClassMob.Map_count];
                MasY = new int[ClassMob.Map_count];
                MasZ = new int[ClassMob.Map_count];
                location = new string[ClassMob.Map_count];

                string CommandText = "SELECT locx, locY, locZ, location FROM spawnlist WHERE npc_templateid = " + ClassMob.Info_Id + "";
                DataTable t2 = new DataTable();
                ClassConnect.GridView_list_mobs(t2, CommandText); // Создан отдельный файл с класом коннект (обозреватель решений)
                int i = 0;                
                foreach (DataRow row in t2.Rows)
                {
                    MasX[i] = Convert.ToInt32(row[0]);
                    MasY[i] = Convert.ToInt32(row[1]);
                    MasZ[i] = Convert.ToInt32(row[2]);
                    location[i] = Convert.ToString(row[3]);

                    i++;
                }
                ClassMob.Map_mini_x = MasX;
                ClassMob.Map_mini_y = MasY;
                ClassMob.Map_mini_z = MasZ;
                ClassMob.Map_mini_location = location;
            }
            catch (Exception l)
            {
                MessageBox.Show("Ошибка!\r\n (" + l.Message + ")", "Ошибка приложения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private bool isDragging = false;
        private int currentX, currentY;

        private void Muv_img(object sender, MouseEventArgs e)
        {
            int i = 10;
            if (isDragging)
            {
                if (currentX + i < e.X) { }
                else if (currentX > e.X)
                { panel1.AutoScrollPosition = new Point(Math.Abs(panel1.AutoScrollPosition.X) + i, Math.Abs(panel1.AutoScrollPosition.Y));  }
                if (currentX + i > e.X) { }
                else if (currentX < e.X)
                { panel1.AutoScrollPosition = new Point(Math.Abs(panel1.AutoScrollPosition.X) - i, Math.Abs(panel1.AutoScrollPosition.Y)); }
                
                if (currentY + i < e.Y) { }
                else if (currentY > e.Y)
                { panel1.AutoScrollPosition = new Point(Math.Abs(panel1.AutoScrollPosition.X) , Math.Abs(panel1.AutoScrollPosition.Y) + i ); }
                if (currentY + i > e.Y) { }
                else if (currentY < e.Y)
                { panel1.AutoScrollPosition = new Point(Math.Abs(panel1.AutoScrollPosition.X), Math.Abs(panel1.AutoScrollPosition.Y) - i); }
            }
        }

        private void Down_img(object sender, MouseEventArgs e)
        {
            isDragging = true;
            currentX = e.X;
            currentY = e.Y;
        }

        private void Up_img(object sender, MouseEventArgs e)
        {
            isDragging = false;
            //this.toolTip1.SetToolTip(this.pictureBox1, "Имя моба - (" + ClassMob.Info_Name + ")\r\nлвл моба - (" + ClassMob.Info_level + ")\r\nколичество  точек респа - (" + ClassMob.Map_count + ")");
        }

        private void mob_map_Load(object sender, EventArgs e)
        {
            int xxx = MasX[0]; // x     Положение перса
            int yyy = MasY[0]; ; // y     Положение перса
            int xxx2 = 0;
            int yyy2 = 0;
            xxx2 = (xxx + 324800) / 200;
            yyy2 = (yyy + 262300) / 200;
            int xxx3 = 0;
            int yyy3 = 0;
            if (yyy2 > 2370)
            {
                yyy3 = (2620 - yyy2) + yyy2;
            }
            else
            {
                yyy3 = 250 + yyy2;
            }

            if (xxx2 > 2356)
            {
                xxx3 = (2796 - xxx2) + xxx2;
            }
            else
            {
                xxx3 = 440 + xxx2;
            }
            if (location[0] == "HereticsCatacomb")
            {
                pictureBoxCursor.Location = new Point(xxx3 + HereticX, yyy3 + HereticY);
                //pictureBox1.Location = new Point(x2 - 100 + HereticX, y2 - 100 + HereticY);
            }
            else if (location[0] == "CatacombOfTheForbiddenPath")
            {
                pictureBoxCursor.Location = new Point(xxx3 + ForbiddenX, yyy3 + ForbiddenY);
                //pictureBox1.Location = new Point(x2 - 100 + ForbiddenX, y2 - 100 + ForbiddenY);
            }
            else if (location[0] == "ApostateCatacomb")
            {
                pictureBoxCursor.Location = new Point(xxx3 + ApostateX, yyy3 + ApostateY);
                //pictureBox1.Location = new Point(x2 - 100 + ApostateX, y2 - 100 + ApostateY);
            }
            else
            {
                pictureBoxCursor.Location = new Point(xxx3, yyy3);
                //pictureBox1.Location = new Point(x2 - 100, y2 - 100);
            }
            pictureBoxCursor.Focus();
        }

        private void pictureBox_map_Paint(object sender, PaintEventArgs e)
        {
            Bitmap bmp;
            for (int i = 0; i < MasX.Length; i++)
            {
                if (MasZ[i] < ClassMob.Seting_z)
                {
                     bmp = new Bitmap(Properties.Resources.tochkaBlu);
                }
                else
                {
                     bmp = new Bitmap(Properties.Resources.tochka);
                }
                bmp.MakeTransparent(Color.White);
                if (location[i] == "HereticsCatacomb")
                {
                    e.Graphics.DrawImage((Image)bmp, new Point(((MasX[i] + 324800) / 200) + HereticX, ((MasY[i] + 262300) / 200) + HereticY));
                }
                else if (location[i] == "CatacombOfTheForbiddenPath")
                {
                    e.Graphics.DrawImage((Image)bmp, new Point(((MasX[i] + 324800) / 200) + ForbiddenX, ((MasY[i] + 262300) / 200) + ForbiddenY));
                }
                else if (location[i] == "ApostateCatacomb")
                {
                    e.Graphics.DrawImage((Image)bmp, new Point(((MasX[i] + 324800) / 200) + ApostateX, ((MasY[i] + 262300) / 200) + ApostateY));
                }
                else
                {
                    e.Graphics.DrawImage((Image)bmp, new Point((MasX[i] + 324800) / 200, (MasY[i] + 262300) / 200));
                }

            }
        }

        private void pictureBox_map_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }

        // карты городов

        private void leaveMaus(object sender, EventArgs e) 
        { 
            pictureBoxHeretic.Image = Properties.Resources.ukaska50;
            pictureBoxBranded.Image = Properties.Resources.ukaska50;
            pictureBoxSaints.Image = Properties.Resources.ukaska50;
            pictureBoxMartyrdom.Image = Properties.Resources.ukaska50;
            pictureBoxWorship.Image = Properties.Resources.ukaska50;
            pictureBoxPiligrim.Image = Properties.Resources.ukaska50;
            pictureBoxSacrifice.Image = Properties.Resources.ukaska50;
            pictureBoxWitch.Image = Properties.Resources.ukaska50;
            pictureBoxForbidden.Image = Properties.Resources.ukaska50;
            pictureBoxApostate.Image = Properties.Resources.ukaska50;
            pictureBoxDevotion.Image = Properties.Resources.ukaska50;
            pictureBoxPatriot.Image = Properties.Resources.ukaska50;
            pictureBoxDarkOmens.Image = Properties.Resources.ukaska50;
            pictureBoxDisciple.Image = Properties.Resources.ukaska50;
            
            pictureBoxHeine.Image = Properties.Resources.ukaska50;
            pictureBoxGiran.Image = Properties.Resources.ukaska50;
            pictureBoxDion.Image = Properties.Resources.ukaska50;
            pictureBoxGludio.Image = Properties.Resources.ukaska50;
            pictureBoxGludin.Image = Properties.Resources.ukaska50;
            pictureBoxHunter.Image = Properties.Resources.ukaska50;
            pictureBoxOren.Image = Properties.Resources.ukaska50;
            pictureBoxAden.Image = Properties.Resources.ukaska50;
            pictureBoxRune.Image = Properties.Resources.ukaska50;
            pictureBoxSchuttgart.Image = Properties.Resources.ukaska50;
            pictureBoxGddard.Image = Properties.Resources.ukaska50;
            pictureBoxTalkingIsland.Image = Properties.Resources.ukaska50;
            pictureBoxElven.Image = Properties.Resources.ukaska50;
            pictureBoxKamael.Image = Properties.Resources.ukaska50;
            pictureBoxOrc.Image = Properties.Resources.ukaska50;
            pictureBoxDarkElven.Image = Properties.Resources.ukaska50;
            pictureBoxFloran.Image = Properties.Resources.ukaska50;
            pictureBoxDwarven.Image = Properties.Resources.ukaska50;

            pictureBoxAntharasLair.Image = Properties.Resources.ukaska50;
            pictureBoxDevilIsle.Image = Properties.Resources.ukaska50;
            pictureBoxAntNest.Image = Properties.Resources.ukaska50;
            pictureBoxStakatoNest.Image = Properties.Resources.ukaska50;

        }
        private void MuvHeretic(object sender, MouseEventArgs e) { pictureBoxHeretic.Image = Properties.Resources.ukaska; }
        private void MuvBranded(object sender, MouseEventArgs e) { pictureBoxBranded.Image = Properties.Resources.ukaska; }
        private void MuvSaints(object sender, MouseEventArgs e) { pictureBoxSaints.Image = Properties.Resources.ukaska; }
        private void Martyrdom(object sender, MouseEventArgs e) { pictureBoxMartyrdom.Image = Properties.Resources.ukaska; }
        private void Worship(object sender, MouseEventArgs e) { pictureBoxWorship.Image = Properties.Resources.ukaska; }
        private void Piligrim(object sender, MouseEventArgs e) { pictureBoxPiligrim.Image = Properties.Resources.ukaska; }
        private void Sacrifice(object sender, MouseEventArgs e) { pictureBoxSacrifice.Image = Properties.Resources.ukaska; }
        private void Witch(object sender, MouseEventArgs e) { pictureBoxWitch.Image = Properties.Resources.ukaska; }
        private void Forbidden(object sender, MouseEventArgs e) { pictureBoxForbidden.Image = Properties.Resources.ukaska; }
        private void Apostate(object sender, MouseEventArgs e) { pictureBoxApostate.Image = Properties.Resources.ukaska; }
        private void Devotion(object sender, MouseEventArgs e) { pictureBoxDevotion.Image = Properties.Resources.ukaska; }
        private void Patriot(object sender, MouseEventArgs e) { pictureBoxPatriot.Image = Properties.Resources.ukaska; }
        private void DarkOmens(object sender, MouseEventArgs e) { pictureBoxDarkOmens.Image = Properties.Resources.ukaska; }
        private void Disciple(object sender, MouseEventArgs e) { pictureBoxDisciple.Image = Properties.Resources.ukaska; }

        private void Heine(object sender, MouseEventArgs e) { pictureBoxHeine.Image = Properties.Resources.ukaska; }
        private void Giran(object sender, MouseEventArgs e) { pictureBoxGiran.Image = Properties.Resources.ukaska; }
        private void Dion(object sender, MouseEventArgs e) { pictureBoxDion.Image = Properties.Resources.ukaska; }
        private void Gludio(object sender, MouseEventArgs e) { pictureBoxGludio.Image = Properties.Resources.ukaska; }
        private void Gludin(object sender, MouseEventArgs e) { pictureBoxGludin.Image = Properties.Resources.ukaska; }
        private void Hunter(object sender, MouseEventArgs e) { pictureBoxHunter.Image = Properties.Resources.ukaska; }
        private void Oren(object sender, MouseEventArgs e) { pictureBoxOren.Image = Properties.Resources.ukaska; }
        private void Aden(object sender, MouseEventArgs e) { pictureBoxAden.Image = Properties.Resources.ukaska; }
        private void Rune(object sender, MouseEventArgs e) { pictureBoxRune.Image = Properties.Resources.ukaska; }
        private void Schuttgart(object sender, MouseEventArgs e) { pictureBoxSchuttgart.Image = Properties.Resources.ukaska; }
        private void Gddard(object sender, MouseEventArgs e) { pictureBoxGddard.Image = Properties.Resources.ukaska; }
        private void TalkingIsland(object sender, MouseEventArgs e) { pictureBoxTalkingIsland.Image = Properties.Resources.ukaska; }
        private void Elven(object sender, MouseEventArgs e) { pictureBoxElven.Image = Properties.Resources.ukaska; }
        private void Kamael(object sender, MouseEventArgs e) { pictureBoxKamael.Image = Properties.Resources.ukaska; }
        private void Orc(object sender, MouseEventArgs e) { pictureBoxOrc.Image = Properties.Resources.ukaska; }
        private void DarkElven(object sender, MouseEventArgs e) { pictureBoxDarkElven.Image = Properties.Resources.ukaska; }
        private void Floran(object sender, MouseEventArgs e) { pictureBoxFloran.Image = Properties.Resources.ukaska; }
        private void Dwarven(object sender, MouseEventArgs e) { pictureBoxDwarven.Image = Properties.Resources.ukaska; }

        private void AntharasLair(object sender, MouseEventArgs e) { pictureBoxAntharasLair.Image = Properties.Resources.ukaska; }
        private void DevilIsle(object sender, MouseEventArgs e) { pictureBoxDevilIsle.Image = Properties.Resources.ukaska; }
        private void AntNest(object sender, MouseEventArgs e) { pictureBoxAntNest.Image = Properties.Resources.ukaska; }
        private void StakatoNest(object sender, MouseEventArgs e) { pictureBoxStakatoNest.Image = Properties.Resources.ukaska; }

        private void pictureBoxBranded_Click(object sender, EventArgs e)
        {
            pictureBoxBranded.Focus();
            ClassMob.Map_mini_img = 0; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "Catacomb of the Branded   имя моба - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxHeretic_Click(object sender, EventArgs e)
        {
            pictureBoxHeretic.Focus();
            ClassMob.Map_mini_img = 1; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "Catacomb of the Heretic   имя моба - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxSaints_Click(object sender, EventArgs e)
        {
            pictureBoxSaints.Focus();
            ClassMob.Map_mini_img = 2; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "The Saints Necropolis   имя моба - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxMartyrdom_Click(object sender, EventArgs e)
        {
            pictureBoxMartyrdom.Focus();
            ClassMob.Map_mini_img = 3; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "Necropolis of Martyrdom   имя моба - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxWorship_Click(object sender, EventArgs e)
        {
            pictureBoxWorship.Focus();
            ClassMob.Map_mini_img = 4; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "Necropolis of Worship   имя моба - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxPiligrim_Click(object sender, EventArgs e)
        {
            pictureBoxPiligrim.Focus();
            ClassMob.Map_mini_img = 5; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "The Pilgrims Necropolis   имя моба - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxSacrifice_Click(object sender, EventArgs e)
        {
            pictureBoxSacrifice.Focus();
            ClassMob.Map_mini_img = 6; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "Necropolis of Sacrifice   имя моба - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxWitch_Click(object sender, EventArgs e)
        {
            pictureBoxWitch.Focus();
            ClassMob.Map_mini_img = 7; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "Catacomb of the Witch  имя моба - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxForbidden_Click(object sender, EventArgs e)
        {
            pictureBoxForbidden.Focus();
            ClassMob.Map_mini_img = 8; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "Catacomb of the Forbidden Path  имя моба - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxApostate_Click(object sender, EventArgs e)
        {
            pictureBoxApostate.Focus();
            ClassMob.Map_mini_img = 9; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "Catacomb of the Apostate  имя моба - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxDevotion_Click(object sender, EventArgs e)
        {
            pictureBoxDevotion.Focus();
            ClassMob.Map_mini_img = 10; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "Necropolis of Devotion  имя моба - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxPatriot_Click(object sender, EventArgs e)
        {
            pictureBoxPatriot.Focus();
            ClassMob.Map_mini_img = 11; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "The Patriots Necropolis  имя моба - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxDarkOmens_Click(object sender, EventArgs e)
        {
            pictureBoxDarkOmens.Focus();
            ClassMob.Map_mini_img = 12; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "Catacomb of Dark Omens  имя моба - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxDisciple_Click(object sender, EventArgs e)
        {
            pictureBoxDisciple.Focus();
            ClassMob.Map_mini_img = 13; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "The Discipls Necropoliss  имя моба - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxHeine_Click(object sender, EventArgs e) // Город
        {
            pictureBoxHeine.Focus();
            ClassMob.Map_mini_img = 20; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "Город - (Heine)   Имя - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxGiran_Click(object sender, EventArgs e)
        {
            pictureBoxGiran.Focus();
            ClassMob.Map_mini_img = 21; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "Город - (Giran)   Имя - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxDion_Click(object sender, EventArgs e)
        {
            pictureBoxDion.Focus();
            ClassMob.Map_mini_img = 22; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "Город - (Dion)   Имя - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxGludio_Click(object sender, EventArgs e)
        {
            pictureBoxGludio.Focus();
            ClassMob.Map_mini_img = 23; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "Город - (Gludio)   Имя - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxGludin_Click(object sender, EventArgs e)
        {
            pictureBoxGludin.Focus();
            ClassMob.Map_mini_img = 24; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "Город - (Gludin)   Имя - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxHunter_Click(object sender, EventArgs e)
        {
            pictureBoxHunter.Focus();
            ClassMob.Map_mini_img = 25; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "Город - (Hunter)   Имя - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxOren_Click(object sender, EventArgs e)
        {
            pictureBoxOren.Focus();
            ClassMob.Map_mini_img = 26; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "Город - (Oren)   Имя - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxAden_Click(object sender, EventArgs e)
        {
            pictureBoxAden.Focus();
            ClassMob.Map_mini_img = 27; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "Город - (Aden)   Имя - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxRune_Click(object sender, EventArgs e)
        {
            pictureBoxRune.Focus();
            ClassMob.Map_mini_img = 28; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "Город - (Rune)   Имя - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxSchuttgart_Click(object sender, EventArgs e)
        {
            pictureBoxSchuttgart.Focus();
            ClassMob.Map_mini_img = 29; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "Город - (Schuttgart)   Имя - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxGddard_Click(object sender, EventArgs e)
        {
            pictureBoxGddard.Focus();
            ClassMob.Map_mini_img = 30; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "Город - (Goddard)   Имя - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxTalkingIsland_Click(object sender, EventArgs e)
        {
            pictureBoxTalkingIsland.Focus();
            ClassMob.Map_mini_img = 31; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "Город - (Talking Island)   Имя - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxElven_Click(object sender, EventArgs e)
        {
            pictureBoxElven.Focus();
            ClassMob.Map_mini_img = 32; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "Город - (Elven)   Имя - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxKamael_Click(object sender, EventArgs e)
        {
            pictureBoxKamael.Focus();
            ClassMob.Map_mini_img = 33; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "Город - (Kamael)   Имя - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxOrc_Click(object sender, EventArgs e)
        {
            pictureBoxOrc.Focus();
            ClassMob.Map_mini_img = 34; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "Город - (Orc)   Имя - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxDarkElven_Click(object sender, EventArgs e)
        {
            pictureBoxDarkElven.Focus();
            ClassMob.Map_mini_img = 35; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "Город - (Dark Elven)   Имя - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxFloran_Click(object sender, EventArgs e)
        {
            pictureBoxFloran.Focus();
            ClassMob.Map_mini_img = 36; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "Город - (Floran)   Имя - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxDwarven_Click(object sender, EventArgs e)
        {
            pictureBoxDwarven.Focus();
            ClassMob.Map_mini_img = 37; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "Город - (Dwarven)   Имя - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxAntharasLair_Click(object sender, EventArgs e)
        {
            pictureBoxAntharasLair.Focus();
            ClassMob.Map_mini_img = 40; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "место - (Antharas Lair)   Имя - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxDevilIsle_Click(object sender, EventArgs e)
        {
            pictureBoxDevilIsle.Focus();
            ClassMob.Map_mini_img = 41; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "место - (Devil Isle)   Имя - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxAntNest_Click(object sender, EventArgs e)
        {
            pictureBoxAntNest.Focus();
            ClassMob.Map_mini_img = 42; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "место - (Ant Nest)   Имя - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBoxStakatoNest_Click(object sender, EventArgs e)
        {
            pictureBoxStakatoNest.Focus();
            ClassMob.Map_mini_img = 43; mob_map_mini form2 = new mob_map_mini();
            form2.Text = "место - (Stakato Nest)   Имя - (" + ClassMob.Info_Name + ")"; form2.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }




    } 

}

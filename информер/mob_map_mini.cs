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
    public partial class mob_map_mini : Form
    {
        public mob_map_mini()
        {
            InitializeComponent();
            try
            {
                if (ClassMob.Map_mini_img == 0) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\nc-branded.jpg"); }
                else if (ClassMob.Map_mini_img == 1) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\nc-heretic.jpg"); }
                else if (ClassMob.Map_mini_img == 2) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\nc-saints.jpg"); }
                else if (ClassMob.Map_mini_img == 3) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\nc-martyrs.jpg"); }
                else if (ClassMob.Map_mini_img == 4) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\nc-worship.jpg"); }
                else if (ClassMob.Map_mini_img == 5) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\nc-pilgrims.jpg"); }
                else if (ClassMob.Map_mini_img == 6) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\nc-sacrifice.jpg"); }
                else if (ClassMob.Map_mini_img == 7) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\nc-witch.jpg"); }
                else if (ClassMob.Map_mini_img == 8) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\nc-forbidpath.jpg"); }
                else if (ClassMob.Map_mini_img == 9) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\nc-apo.jpg"); }
                else if (ClassMob.Map_mini_img == 10) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\nc-devotion.jpg"); }
                else if (ClassMob.Map_mini_img == 11) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\nc-patriots.jpg"); }
                else if (ClassMob.Map_mini_img == 12) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\nc-darkomen.jpg"); }
                else if (ClassMob.Map_mini_img == 13) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\nc-disciples.jpg"); }
                else if (ClassMob.Map_mini_img == 20) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\town-heine.jpg"); }
                else if (ClassMob.Map_mini_img == 21) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\town-giran.jpg"); }
                else if (ClassMob.Map_mini_img == 22) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\town-dion.jpg"); }
                else if (ClassMob.Map_mini_img == 23) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\town-gludio.jpg"); }
                else if (ClassMob.Map_mini_img == 24) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\town-gludin.jpg"); }
                else if (ClassMob.Map_mini_img == 25) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\town-hunters.jpg"); }
                else if (ClassMob.Map_mini_img == 26) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\town-oren.jpg"); }
                else if (ClassMob.Map_mini_img == 27) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\town-aden.jpg"); }
                else if (ClassMob.Map_mini_img == 28) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\town-rune.jpg"); }
                else if (ClassMob.Map_mini_img == 29) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\town-schuttgart.jpg"); }
                else if (ClassMob.Map_mini_img == 30) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\town-godard.jpg"); }
                else if (ClassMob.Map_mini_img == 31) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\town-talkin.jpg"); }
                else if (ClassMob.Map_mini_img == 32) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\town-elven.jpg"); }
                else if (ClassMob.Map_mini_img == 33) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\town-kamael.jpg"); }
                else if (ClassMob.Map_mini_img == 34) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\town-orc.jpg"); }
                else if (ClassMob.Map_mini_img == 35) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\town-dark.jpg"); }
                else if (ClassMob.Map_mini_img == 36) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\town-floran.jpg"); }
                else if (ClassMob.Map_mini_img == 37) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\town-dwarven.jpg"); }
                else if (ClassMob.Map_mini_img == 40) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\zo-lair.jpg"); }
                else if (ClassMob.Map_mini_img == 41) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\zo-devil.jpg"); }
                else if (ClassMob.Map_mini_img == 42) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\zo-antnest.jpg"); }
                else if (ClassMob.Map_mini_img == 43) { pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\images\maps\zo-stakato.jpg"); }
            }
            catch (Exception l)
            {
                MessageBox.Show("Ошибка!\r\n (" + l.Message + ")", "Ошибка доступа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //MessageBox.Show("" + (ClassMob.Map_mini_x[0] - ClassMob.xxxxx) / 34 + " - " + (ClassMob.Map_mini_y[0] + ClassMob.yyyyy) / 34 + " - " + ClassMob.Map_mini_location[0] + "");
        }

        private void pictureBox_map_Paint(object sender, PaintEventArgs e)
        {
            Bitmap bmp = new Bitmap(Properties.Resources.tochka);
            for (int i = 0; i < ClassMob.Map_mini_x.Length; i++)
            {
                bmp.MakeTransparent(Color.White);
                if (ClassMob.Map_mini_img == 0 & ClassMob.Map_mini_location[i] == "CatacombBranded")
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] - 46300) / 17, (ClassMob.Map_mini_y[i] - 169900) / 17)); }
                else if (ClassMob.Map_mini_img == 1 & ClassMob.Map_mini_location[i] == "HereticsCatacomb")
                {e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] + 53200) / 17, (ClassMob.Map_mini_y[i] + 250700) / 17));}
                else if (ClassMob.Map_mini_img == 2 & ClassMob.Map_mini_location[i] == "SaintsNecropolis")
                {e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] - 83000) / 17, (ClassMob.Map_mini_y[i] + -208800) / 17));}
                else if (ClassMob.Map_mini_img == 3 & ClassMob.Map_mini_location[i] == "MartyrsNecropolis")
                {e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] - 118300) / 17, (ClassMob.Map_mini_y[i] - 132500) / 17));}
                else if (ClassMob.Map_mini_img == 4 & ClassMob.Map_mini_location[i] == "WorshipersNecropolis")
                {e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] - 111400) / 17, (ClassMob.Map_mini_y[i] - 173600) / 17));}
                else if (ClassMob.Map_mini_img == 5 & ClassMob.Map_mini_location[i] == "PilgrimsNecropolis")
                {e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] - 45000) / 17, (ClassMob.Map_mini_y[i] - 113100) / 17));}
                else if (ClassMob.Map_mini_img == 6 & ClassMob.Map_mini_location[i] == "NecropolisSacrifice")
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] + 52200) / 17, (ClassMob.Map_mini_y[i] - 209600) / 17)); }
                else if (ClassMob.Map_mini_img == 7 & ClassMob.Map_mini_location[i] == "CatacombOfTheWitch")
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] - 140500) / 17, (ClassMob.Map_mini_y[i] - 79450) / 17)); }
                else if (ClassMob.Map_mini_img == 8 & ClassMob.Map_mini_location[i] == "CatacombOfTheForbiddenPath")
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] - 12600) / 17, (ClassMob.Map_mini_y[i] + 248900) / 17)); }
                else if (ClassMob.Map_mini_img == 9 & ClassMob.Map_mini_location[i] == "ApostateCatacomb")
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] + 20500) / 17, (ClassMob.Map_mini_y[i] + 251200) / 17)); }
                else if (ClassMob.Map_mini_img == 10 & ClassMob.Map_mini_location[i] == "DevotionNecropolis")
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] + 52100) / 17, (ClassMob.Map_mini_y[i] - 78700) / 17)); }
                else if (ClassMob.Map_mini_img == 11 & ClassMob.Map_mini_location[i] == "PatriotsNecropolis")
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] + 21580) / 17, (ClassMob.Map_mini_y[i] - 77000) / 17)); }
                else if (ClassMob.Map_mini_img == 12 & ClassMob.Map_mini_location[i] == "CatacombDarkOmen")
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] + 19500) / 17, (ClassMob.Map_mini_y[i] - 13200) / 17)); }
                else if (ClassMob.Map_mini_img == 13 & ClassMob.Map_mini_location[i] == "DisciplesNecropolis")
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] - 172200) / 17, (ClassMob.Map_mini_y[i] + 18000) / 17)); }
                else if (ClassMob.Map_mini_img == 20)
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] - 103100) / 20, (ClassMob.Map_mini_y[i] - 213580) / 20)); }
                else if (ClassMob.Map_mini_img == 21)
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] - 75300) / 20, (ClassMob.Map_mini_y[i] - 140050) / 20)); }
                else if (ClassMob.Map_mini_img == 22)
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] - 11770) / 20, (ClassMob.Map_mini_y[i] - 138550) / 20)); }
                else if (ClassMob.Map_mini_img == 23)
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] + 19980) / 20, (ClassMob.Map_mini_y[i] - 117500) / 20)); }
                else if (ClassMob.Map_mini_img == 24)
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] + 88900) / 20, (ClassMob.Map_mini_y[i] - 146000) / 20)); }
                else if (ClassMob.Map_mini_img == 25)
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] - 111180) / 20, (ClassMob.Map_mini_y[i] - 69990) / 20)); }
                else if (ClassMob.Map_mini_img == 26)
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] - 75100) / 20, (ClassMob.Map_mini_y[i] - 48700) / 20)); }
                else if (ClassMob.Map_mini_img == 27)
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] - 140140) / 20, (ClassMob.Map_mini_y[i] - 17050) / 20)); }
                else if (ClassMob.Map_mini_img == 28)
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] - 31500) / 20, (ClassMob.Map_mini_y[i] + 55400) / 20)); }
                else if (ClassMob.Map_mini_img == 29)
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] - 82350) / 20, (ClassMob.Map_mini_y[i] + 147000) / 20)); }
                else if (ClassMob.Map_mini_img == 30)
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] - 140200) / 20, (ClassMob.Map_mini_y[i] + 64200) / 20)); }
                else if (ClassMob.Map_mini_img == 31)
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] + 90510) / 20, (ClassMob.Map_mini_y[i] - 236550) / 20)); }
                else if (ClassMob.Map_mini_img == 32)
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] - 39245) / 20, (ClassMob.Map_mini_y[i] - 42720) / 20)); }
                else if (ClassMob.Map_mini_img == 33)
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] + 125000) / 20, (ClassMob.Map_mini_y[i] - 38700) / 20)); }
                else if (ClassMob.Map_mini_img == 34)
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] + 52750) / 20, (ClassMob.Map_mini_y[i] + 120370) / 20)); }
                else if (ClassMob.Map_mini_img == 35)
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] - 4850) / 20, (ClassMob.Map_mini_y[i] - 9700) / 20)); }
                else if (ClassMob.Map_mini_img == 36)
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] - 11900) / 20, (ClassMob.Map_mini_y[i] - 165200) / 20)); }
                else if (ClassMob.Map_mini_img == 37)
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] - 111660) / 20, (ClassMob.Map_mini_y[i] + 187630) / 20)); }
                else if (ClassMob.Map_mini_img == 40)
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] - 130000) / 35, (ClassMob.Map_mini_y[i] - 105600) / 35)); }
                else if (ClassMob.Map_mini_img == 41)
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] - 40100) / 34, (ClassMob.Map_mini_y[i] - 201400) / 34)); }
                else if (ClassMob.Map_mini_img == 42)
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] + 31700) / 34, (ClassMob.Map_mini_y[i] - 173000) / 34)); }
                else if (ClassMob.Map_mini_img == 43)
                { e.Graphics.DrawImage((Image)bmp, new Point((ClassMob.Map_mini_x[i] - 73500) / 36, (ClassMob.Map_mini_y[i] + 57500) / 36)); }
            }
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

    }
}

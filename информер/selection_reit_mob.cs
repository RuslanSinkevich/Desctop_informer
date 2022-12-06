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
    public partial class selection_reit_mob : Form
    {
        public selection_reit_mob()
        {
            InitializeComponent();
            if (ClassMob.S_Reit > 0)
            {
                
                comboBox1.Text = "x" + Convert.ToString(ClassMob.S_Reit) + "";
                comboBox2.Text = "x" + Convert.ToString(ClassMob.PO_Reit) + "";
            }
            if (ClassMob.S_Reit == 14) { comboBox1.Text = "x1/4"; }
            else if (ClassMob.S_Reit == 13) { comboBox1.Text = "x1/2"; }
            if (ClassMob.PO_Reit == 14) { comboBox2.Text = "x1/4"; }
            else if (ClassMob.PO_Reit == 13) { comboBox2.Text = "x1/2"; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int text1 = 0;
                int text2 = 0;
                if (comboBox1.Text == "x1/4") { text1 = 14; }
                else if (comboBox1.Text == "x1/2") { text1 =13; }
                for (int i = 1; i < 13; i++)
                {
                if (comboBox1.Text == "x" + i + "") { text1 = i; }
                }

                if (comboBox2.Text == "x1/4") { text2 = 14; }
                else if (comboBox2.Text == "x1/2") { text2 = 13; }
                for (int i = 1; i < 13; i++)
                {
                    if (comboBox2.Text == "x" + i + "") { text2 = i; }
                }

                //MessageBox.Show(Convert.ToString("" + text1 + " - " + text2 + " "));

                if (text1 >= 1 && text1 <= 14 &&
                    text2 >= 1 && text2 <= 14)
                {
                    ClassMob.S_Reit =  text1; // в главной форме клас который добовляет данные
                    ClassMob.PO_Reit = text2;
                    this.Close();
                }
                else
                {
                    comboBox1.Text = "x1/4";
                    comboBox2.Text = "x10";
                    MessageBox.Show("Ошибка! значение должно быть от x1/4 до x10", "Выбор рейта мобов", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                comboBox1.Text = "x1/4";
                comboBox2.Text = "x10";
                MessageBox.Show("Ошибка! не верное значение поля", "Выбор рейта мобов", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}

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
    public partial class selection_level_mob : Form
    {
        public selection_level_mob()
        {
            InitializeComponent();
            if (ClassMob.S_level > 0)
            {
                comboBox1.Text = Convert.ToString(ClassMob.S_level);
                comboBox2.Text = Convert.ToString(ClassMob.PO_level);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(comboBox1.Text) >= 1 && Convert.ToInt32(comboBox1.Text) <= 87 &&
                    Convert.ToInt32(comboBox2.Text) >= 1 && Convert.ToInt32(comboBox2.Text) <= 87)
                {
                    ClassMob.S_level = Convert.ToInt32(comboBox1.Text); // в главной форме клас который добовляет данные
                    ClassMob.PO_level = Convert.ToInt32(comboBox2.Text);

                    this.Close();
                }
                else
                {
                    comboBox1.Text = "1";
                    comboBox2.Text = "87";
                    MessageBox.Show("Ошибка! значение должно быть от 1 до 87", "Выбор уровня мобов", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                comboBox1.Text = "1";
                comboBox2.Text = "87";
                MessageBox.Show("Ошибка! не верное значение поля", "Выбор уровня мобов", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }


    }
}

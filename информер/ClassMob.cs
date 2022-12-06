using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace l2mega_informer
{
    class ClassMob // Класс передаюший параметры из окна selection_level_mob (выбор лвла мобов)
    {
        public static int S_level { get; set; }
        public static int PO_level { get; set; }
        public static int S_Reit { get; set; }
        public static int PO_Reit { get; set; }

        public static string Info_Form_Text { get; set; }

        public static int Info_Id { get; set; }
        public static int Info_level { get; set; }
        public static string Info_Name { get; set; }

        public static int Map_count { get; set; }        
        public static int Map_mini_img { get; set; }
        public static int[] Map_mini_x { get; set; }
        public static int[] Map_mini_y { get; set; }
        public static int[] Map_mini_z { get; set; }
        public static string[] Map_mini_location { get; set; }

        public static int Items_Id { get; set; }
        public static string Items_Name { get; set; }

        public static int Info_SkillId { get; set; }
        public static int Info_SkillLevl { get; set; }

        public static string skil_node_name { get; set; }
        public static int skil_node_level { get; set; }
        public static string skil_name { get; set; }
        public static int skil_id { get; set; }

        public static int Seting_xp { get; set; }
        public static int Seting_sp { get; set; }
        public static int Seting_z { get; set; }
        public static int Seting_lang { get; set; }

        public static string info_webBrowser { get; set; }
        public static string info_linkLabel { get; set; }
        public static string info_news { get; set; }

        public static int ping { get; set; }
        public static string ping_text { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace l2mega_informer
{
    class ClassAddOn // клас подстовляюший вместо текста другое значени comboBox 
    {
        string index;
        string text;
        public ClassAddOn(string _ind, string _txt)
        {
            this.index = _ind;
            this.text = _txt;
        }
        public override string ToString()
        {
            return this.text;
        }
        public string INDEX
        {
            get
            {
                return this.index;
            }

        }
    }
}

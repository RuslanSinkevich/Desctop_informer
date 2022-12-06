using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace l2mega_informer
{
    class common
    {
        public static string GetString(ref string Value) // Функция выделяет всё что находиться между скобок остольное удалает
        {
            try
            {
                string sResult = Value;

                sResult = sResult.Remove(0, sResult.IndexOf("[") + 1);
                sResult = sResult.Remove(sResult.IndexOf("]"));

                Value = Value.Remove(Value.IndexOf("["), Value.IndexOf("]") - Value.IndexOf("[") + 1);

                return sResult;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetString2(ref string Value)// Функция выделяет всё что находиться между скобок остольное удалает
        {
            try
            {
                string sResult = Value;

                sResult = sResult.Remove(0, sResult.IndexOf("(") + 1);
                sResult = sResult.Remove(sResult.IndexOf(")"));

                Value = Value.Remove(Value.IndexOf("("), Value.IndexOf(")") - Value.IndexOf("(") + 1);

                return sResult;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetString3(ref string Value)// Функция выделяет всё что находиться между скобок остольное удалает
        {
            try
            {
                string sResult = Value;

                sResult = sResult.Remove(0, sResult.IndexOf("{") + 1);
                sResult = sResult.Remove(sResult.IndexOf("}"));

                Value = Value.Remove(Value.IndexOf("{"), Value.IndexOf("}") - Value.IndexOf("{") + 1);

                return sResult;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static bool BlockExist(string Value)
        {
            int IndexBegin = Value.IndexOf("[");
            int IndexBreak = Value.IndexOf("]");

            if (IndexBegin < 0 || IndexBreak < 0) return false;
            if (IndexBegin > IndexBreak) return false;

            return true;
        }
    }
}

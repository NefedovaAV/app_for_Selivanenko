using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace DateClass
{
static class  LetsCheckDate
    {
        internal static string MakeDateString(string s)
        {
            DateTime dateValue;
            dateValue = DateTime.Parse(s);
            var datetime = dateValue.ToString("u");
            return datetime.TrimEnd('Z');
        }


        internal static void CheckDateTime(MaskedTextBox mtb)
        {
            int numbers = DateHasNumbers(mtb);
            if (numbers != 0 && (!DateTime.TryParse(mtb.Text, out DateTime dateValue) || (dateValue > DateTime.Now)))
            {
                MessageBox.Show("Неверный формат даты и/или времени");
                mtb.ResetText();
            }
        }
        internal static bool CheckDateBfSaving(MaskedTextBox mtb)
        {
            int numbers = DateHasNumbers(mtb);
            if (numbers == 0)
            {
                MessageBox.Show("Введите время эксперимента");
                return false;
            }
            else return true;
        }
        internal static int DateHasNumbers(MaskedTextBox mtb)
        {
            Regex rg = new Regex("\\d");
            int numbers = rg.Matches(mtb.Text).Count;
            return numbers;

        }

    }
    
}

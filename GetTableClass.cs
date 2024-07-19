using DocumentFormat.OpenXml.Vml;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wfa_Selivanenko
{
    class GetTable
    {
        readonly string s;
        TakeTable tt { get; }

        DataBase DataBase;
        internal GetTable(TakeTable tt)
        {
            this.tt = tt;
            s = tt.MakeQueryString();
        }

        internal List<string> MakeFile()
        {
            var dt = new System.Data.DataTable();
            MySqlDataAdapter adapter1 = new MySqlDataAdapter();

            adapter1.SelectCommand = DataBase.SqlCommand(s);

            adapter1.Fill(dt);
            var lines = new List<string>();

            string[] columnNames = dt.Columns
                .Cast<DataColumn>()
                .Select(column => column.ColumnName)
                .ToArray();

            var header = string.Join(";", columnNames.Select(name => $"\"{name}\""));
            lines.Add(header);

            var valueLines = dt.AsEnumerable()
                .Select(row => string.Join(";", row.ItemArray.Select(val => $"\"{val}\"")));

            lines.AddRange(valueLines);
            return lines;

        }

        internal void SaveTable(string save_filename)
        {

            File.WriteAllLines(save_filename, MakeFile(), Encoding.UTF8);


        }

    }
}

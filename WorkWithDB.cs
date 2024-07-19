using Dictionary_Variables;
using MySql.Data.MySqlClient;
using System.Text;
using System.Windows.Forms;

namespace wfa_Selivanenko
{
    class DataBase
    {
        internal ApplicationContext db;
        internal DataBase(ApplicationContext db) => this.db = db;
        internal MySqlCommand SqlCommand(string query) => new MySqlCommand(query, db.getConnection()); 
    }


        abstract class Query
    {
        internal DataBase DataBase = new DataBase(db);
        internal ListBox lb;
        internal static ApplicationContext db;
        internal Query() { }
        internal Query(ListBox lb, ApplicationContext db) { this.lb = lb; Query.db = db; }
        internal abstract string MakeQueryString();
        internal virtual void QueryToDB()
        {
            MySqlCommand command = DataBase.SqlCommand(MakeQueryString());
            command.ExecuteNonQuery();
        }

    }


    class CreateTable : Query
    {
        internal string d1 { get; set; }
        internal string d2 { get; set; }
        internal CreateTable(ListBox lb, ApplicationContext db) : base(lb, db) { }

        internal override string MakeQueryString()
        {
            StringBuilder values = new StringBuilder();
            for (int i = 1; i <= lb.Items.Count; i++)
            {
                values.Append($"v{i} DOUBLE, ");
            }
            string V = values.ToString();
            string queryString = $"CREATE TABLE IF NOT EXISTS chosen_variables  (t datetime(3), {V} primary key (t));" +
                $"insert into chosen_variables(t) select distinct t from trends where  (trends.t between '{d1}' and '{d2}') order by trends.t; ";

            return queryString;
        }
    }

    class UpdateTable : Query
    {
        internal int i = 0;

        internal UpdateTable(ListBox lb, ApplicationContext db) : base(lb, db) { }
        internal override string MakeQueryString()
        {
            StringBuilder values = new StringBuilder();
            for (int i = 0 ; i < lb.Items.Count; i++)
            {
                Parameters.valub.TryGetValue(lb.Items[i].ToString(), out int d);
                values.Append($"UPDATE chosen_variables SET chosen_variables.v{i + 1} = (SELECT v FROM trends where chosen_variables.t = trends.t and trends.id={d} ); ");
            }
            return values.ToString();
        }
        
    }
    

      class TakeTable : Query
        {
            internal TakeTable(ListBox lb, ApplicationContext db) : base(lb, db) { }
            internal override string MakeQueryString()
            {
                StringBuilder values2 = new StringBuilder();
                foreach (string parametr in lb.Items)
                {
                 values2.Append($"v{(lb.Items.IndexOf(parametr) + 1)} as '{parametr}', ");
                }
                string s = values2.ToString().TrimEnd(',');

                return $"SELECT t, {s} FROM chosen_variables; ";

            }

        }

      class DropTable : Query
        {
        internal DropTable() : base() { }

        internal override string MakeQueryString()
            {
                return "DROP IF EXISTS TABLE chosen_variables;";

            }

        }

    }

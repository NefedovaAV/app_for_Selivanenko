using System;
using System.ComponentModel;
using System.Windows.Forms;


using Dictionary_Variables;
using DateClass;



namespace wfa_Selivanenko
{
    public partial class Form1 : Form

    {
        public readonly string NameDataBase_1 = "trends";
        public Form1()
        {
            InitializeComponent();
            Button_OpenDB.Click += Button_OpenDB_Click;
            openFileDialog1.Filter = "MySql files(*.sql)|*.sql|All files(*.*)|*.*";
            saveFileDialog1.Filter = "Excel files(*.csv)|*.csv|All files(*.*)|*.*";
            checkedListBox1.CheckOnClick = true;
         
            
        }

        private void Button_OpenDB_Click(object sender, EventArgs e)
        {

         
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                listBox1.Items.Clear();
                checkedListBox1.Items.Clear();
            }
            //string filename = openFileDialog1.FileName;
           
            foreach (var variable in Parameters.valub)
            { checkedListBox1.Items.Add(variable.Key); }

        }

        
        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) { }
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (string s in checkedListBox1.CheckedItems)
            {
                listBox1.Items.Add(s);
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {   
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {  
        }
        private void maskedTextBox1_Leave(object sender, EventArgs e)
        {
            LetsCheckDate.CheckDateTime(maskedTextBox1);
        }
        private void maskedTextBox2_Leave(object sender, EventArgs e)
        {
            LetsCheckDate.CheckDateTime(maskedTextBox2);
        }

        



        private void Button_Save_Click(object sender, EventArgs e)
        {
            while (!LetsCheckDate.CheckDateBfSaving(maskedTextBox1) || !LetsCheckDate.CheckDateBfSaving(maskedTextBox2))
            {
                return;
            }

                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string save_filename = saveFileDialog1.FileName;
           

            
            ApplicationContext db = new ApplicationContext();
            


            db.openConnection();
            CreateTable ct = new CreateTable(listBox1, db);
            ct.d1 = LetsCheckDate.MakeDateString(maskedTextBox1.Text);
            ct.d2 = LetsCheckDate.MakeDateString(maskedTextBox2.Text);
            AllAction(ct);

            UpdateTable ut = new UpdateTable(listBox1, db);
            AllAction(ut);
            
            
            GetTable gt = new GetTable(new TakeTable(listBox1, db));

            gt.SaveTable(save_filename);


            DropTable drt = new DropTable();
            AllAction(drt);
              

                db.closeConnection();


         void AllAction(Query q) 
            { q.MakeQueryString();
                q.QueryToDB();
            }  
        }
 delegate void Sth(Query q);

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
    }
}

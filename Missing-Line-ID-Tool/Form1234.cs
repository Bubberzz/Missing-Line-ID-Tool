using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ExcelDataReader;
using System.Data.Linq;
using System.Linq.Expressions;
using System.Data.Linq.Mapping;
using System.Data.Linq.SqlClient.Implementation;
using System.Web;



namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string fileLocation = "";
        OpenFileDialog open = new OpenFileDialog();
        private void button2_Click(object sender, EventArgs e)
        {
            
            open.Filter = "CSV|*.csv";
            if (open.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = open.SafeFileName;
                fileLocation = open.FileName;
            }
                return;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DataClasses1DataContext conn = new DataClasses1DataContext();
            conn.ExecuteCommand("delete from [MerretTranslator].[dbo].[MDADiagnostic]");
     
            FileStream stream = new FileStream(fileLocation, FileMode.Open);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateCsvReader(stream);
            DataSet result = excelReader.AsDataSet();

            foreach (DataTable table in result.Tables)
            {
                foreach (DataRow dr in table.Rows)
                {
                    if (table.Rows.IndexOf(dr) != 0)
                    {
                          MDADiagnostic addtable = new MDADiagnostic()
                                {
                                    MDANumber = Convert.ToDecimal(dr[0])
                                };
                            conn.MDADiagnostics.InsertOnSubmit(addtable);
                    }
                }
            }
            conn.SubmitChanges();
            excelReader.Close();
            stream.Close();

            conn.ExecuteCommand("EXEC MerretTranslator.dbo.TestProcedure");
            MessageBox.Show("Upload has completed successfully");
        }

        private void panel1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            string fileLocation = e.Data.GetData(DataFormats.FileDrop) as string;
            


        }
    }
}
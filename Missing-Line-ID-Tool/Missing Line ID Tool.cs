using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using ExcelDataReader;
using MissingLineIDForm;

namespace MissingLineIDTool
{
    public partial class MissingLineId : Form
    {
        public MissingLineId()
        {
            InitializeComponent();
        }

        private string _fileName = "";
        private readonly OpenFileDialog _open = new OpenFileDialog();

        // Browse button - initiates a file select dialog, stores filename in _fileName variable
        private void Browse_Click(object sender, EventArgs e)
        {
            _open.Filter = "CSV|*.csv";
            if (_open.ShowDialog() != DialogResult.OK) return;
            textBox1.Text = _open.SafeFileName;
            _fileName = _open.FileName;
        }

        // Run button - builds an in-memory DataTable from CSV file, converting it to decimal and inserting it into the SQL table
        private void Run_Click(object sender, EventArgs e)
        {
            if (_fileName == "")
            {
                return;
            }
            var conn = new DataClasses1DataContext();
            // The SQL table is first cleared
            conn.ExecuteCommand("DELETE Database.ReferenceStg.Table");
            // The FileStream and ExcelDataReader objects will read data from CSV file and write it into a DataTable instance
            try
            {
                var stream = new FileStream(_fileName, FileMode.Open);
                var excelReader = ExcelReaderFactory.CreateCsvReader(stream);
                var result = excelReader.AsDataSet();
                foreach (DataTable table in result.Tables)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        if (table.Rows.IndexOf(dr) == 0) continue;
                        var addTable = new MDADiagnostic()
                        {
                            MDANumber = Convert.ToDecimal(dr[0])
                        };
                        conn.MDADiagnostics.InsertOnSubmit(addTable);
                    }
                }
                // Once DataTable is built, it will be inserted into the SQL table defined in DataClasses1.designer.cs
                conn.SubmitChanges();
                excelReader.Close();
                stream.Close();
                //  Runs stored procedure
                conn.ExecuteCommand("EXEC Database.ReferenceStg.RunStoredPRocedure_Manual");
                Console.WriteLine("Success");
                MessageBox.Show("Upload has completed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error uploading file");
                MessageBox.Show(ex.Message, "File did not upload");
            }
        }

        // Drag/Drop effect
        private void DropBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        // This code allows the drag and drop into the dropBox panel displaying the file name in the text box
        private void DropBox_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            var filename = Path.GetFileName(files[0]);
            foreach (var file in files)
            {
                _fileName = file;
                textBox1.Text = filename;
            }
        }
    }
}
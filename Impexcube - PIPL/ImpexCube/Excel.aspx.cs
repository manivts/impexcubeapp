using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;
namespace Excelread
{
    public partial class Excel : System.Web.UI.Page
    {
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        private string FilePath = "";
        OleDbConnection oledbConn;
        System.Data.DataTable dt = null;
        public string templatevalue;
        public string templatedb;
        string strconn="";
        string value = "";


        Classsql obj = new Classsql();
       
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnreadsheet_Click(object sender, EventArgs e)
        {
            string path = "";
            if (FileUpload1.HasFile)
            {
                string paths = AppDomain.CurrentDomain.BaseDirectory;
                string pathdir = Path.Combine(paths, @"pdf\");
                path = pathdir + Path.GetFileName(FileUpload1.PostedFile.FileName);
                string[] filePaths = Directory.GetFiles(@pathdir);

                foreach (string filePath in filePaths)
                {
                    if (!CheckIfFileIsBeingUsed(filePath))
                    {
                        File.Delete(filePath);
                    }
                   

                }
                FileUpload1.SaveAs(pathdir + Path.GetFileName(FileUpload1.PostedFile.FileName));
                FilePath += path;
            }

            string name = FileUpload1.FileName;
            string filename = Path.GetFileName(FileUpload1.FileName);
            Session["file"] = "";
            Session["file"] = path;
            Session["filename"] = "";
            Session["filename"] = filename;
            loads(path);
            
        }

        public void loads(string SlnoAbbreviation)
        {
            string path = System.IO.Path.GetFullPath(@SlnoAbbreviation);


            oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
              path + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';");
            oledbConn.Open();
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataAdapter oleda = new OleDbDataAdapter();
            DataSet ds = new DataSet();


            cmd.Connection = oledbConn;
            cmd.CommandType = CommandType.Text;

            dt = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            if (dt == null)
            {

            }

            String[] excelSheets = new String[dt.Rows.Count];
            int i = 0;


            foreach (DataRow row in dt.Rows)
            {
                excelSheets[i] = row["TABLE_NAME"].ToString();
                i++;
            }
            for (int j = 0; j < excelSheets.Length; j++)
            {
                drfile.Items.Add(excelSheets[j]);
            }
            drfile.Items.Insert(0, new ListItem("~Select~", "0"));
        }

        public bool CheckIfFileIsBeingUsed(string fileName)
        {
            try
            {
                FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                fs.Close();
                fs.Dispose();
            }

            catch (Exception exp)
            {

                return true;

            }

            return false;

        }

        private void GenerateExcelData(string SlnoAbbreviation, string sheetname)
        {
          
            string path = System.IO.Path.GetFullPath(@SlnoAbbreviation);
            oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
              path + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';");
            oledbConn.Open();
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataAdapter oleda = new OleDbDataAdapter();
            DataSet ds = new DataSet();
            cmd.Connection = oledbConn;
            cmd.CommandType = CommandType.Text;
          
            cmd.CommandText = "SELECT * " +
                  "  FROM [" + sheetname + "]";
            cmd.Parameters.AddWithValue("Slno_Abbreviation", SlnoAbbreviation);
            oleda = new OleDbDataAdapter(cmd);
          
            oleda.Fill(ds, "SQLTABLE");
            Session["datasetvalue"] = ds;
            String[] excelSheets = new String[ds.Tables["SQLTABLE"].Columns .Count];
            int i = 0;
            foreach (DataColumn column in ds.Tables["SQLTABLE"].Columns) 
          
            {
                excelSheets[i] = column.ColumnName; 
                i++;
            }
            for (int j = 0; j < excelSheets.Length; j++)
            {
                lstShowField.Items.Add(excelSheets[j]);
            }
        }

        protected void drfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnreadsheet.Enabled = true;
            string path = (string)Session["file"];

            GenerateExcelData(path, drfile.SelectedValue);
            GetTableName();
        }

        private void GetTableName()
        {
            obj.clsData(strcon);

            string query = "SELECT TABLE_NAME  FROM INFORMATION_SCHEMA.TABLES order by TABLE_NAME";
            DataTable dt = obj.GetTable(query);
            drtable.DataSource = dt;
            drtable.DataTextField = "TABLE_NAME";
            drtable.DataValueField = "TABLE_NAME";
            drtable.DataBind();
            drtable.Items.Insert(0, new ListItem("~Select~", "0"));  
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            MoveListBoxItems(lstShowField, lstView);
        }

        protected void Addall_Click(object sender, EventArgs e)
        {
            int listcount = lstShowField.Items.Count;
            if (listcount != 0)
            {
                for (int i = 0; i < listcount; i++)
                {
                    ListItem item = new ListItem();
                    item.Text = lstShowField.Items[i].Text;
                    
                    lstView.Items.Add(item);

                }

            }



            lstShowField.Items.Clear();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            MoveBackListBoxItems(lstView, lstShowField);
        }

        private void MoveListBoxItems(ListBox from, ListBox to)
        {
            for (int i = 0; i < lstShowField.Items.Count; i++)
            {
                if (lstShowField.Items[i].Selected)
                {
                    to.Items.Add(from.SelectedItem);
                    from.Items.Remove(from.SelectedItem);
                }
            }
            from.SelectedIndex = -1;
            to.SelectedIndex = -1;
        }

        private void MoveBackListBoxItems(ListBox from, ListBox to)
        {
                for (int i = 0; i < lstView.Items.Count; i++)
                {
                    if (lstView.Items[i].Selected)
                    {
                        to.Items.Add(from.SelectedItem);
                        from.Items.Remove(from.SelectedItem);
                    }
                }
                from.SelectedIndex = -1;
                to.SelectedIndex = -1;
           
        }

        protected void RemoveAll_Click(object sender, EventArgs e)
        {
            int _count = lstView.Items.Count;
            if (_count != 0)
            {
                for (int i = 0; i < _count; i++)
                {
                    ListItem item = new ListItem();
                    item.Text = lstView.Items[i].Text;
                   
                    lstShowField.Items.Add(item);
                }

            }

            lstView.Items.Clear();
        }

        protected void empty_Click(object sender, EventArgs e)
        {
                    lstView.Items.Add("0");
                
        }

        protected void drtable_SelectedIndexChanged(object sender, EventArgs e)
        {            
                obj.clsData(strcon);

                string query = "SELECT Column_NAME  FROM INFORMATION_SCHEMA.columns where TABLE_NAME='" + drtable .SelectedItem.Text + "' order by Column_NAME";
                
                DataTable dt = obj.GetTable(query);
               
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    dbview.Items.Add(dt.Rows[j]["Column_NAME"].ToString());
                }
           
        }

        protected void Button5_Click(object sender, EventArgs e)
        {            
            obj.clsData(strcon);
           
            int valuecount = lstView.Items.Count;
            int dbcount = dbview.Items.Count;
            DataSet ds = null;
            ds = (DataSet)Session["datasetvalue"];
          //  ds.Tables["data"].Select("BranchId='TCHE' and EnqDate='" + DateTime.Now.ToString("MM/dd/yyyy") + "'");
          
                for (int j = 0; j < ds.Tables["SQLTABLE"].Rows.Count; j++)
                {
                    Hashtable ht = new Hashtable();
                    for (int i = 0,k=0; i < dbcount && k<valuecount ; i++,k++)
                    {
                        if (lstView.Items[k].Text != "0")
                        {
                            ht.Add(dbview.Items[i].Text, ds.Tables["SQLTABLE"].Rows[j][lstView.Items[i].Text].ToString());
                        }
                   
                   }
                    obj.DoInsert(drtable.SelectedItem.Text, ht);
                    //double c = Excelread.pdf.Class1
                    
            }
                Response.Write("<script> alert('Saved Successfully')</script>");
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("HomePage.aspx");
        }
  
    }
}
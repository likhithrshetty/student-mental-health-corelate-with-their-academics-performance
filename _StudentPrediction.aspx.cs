using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Collections;
using System.Threading;
using System.Configuration;

namespace educationalProject
{
    public partial class _StudentPrediction : System.Web.UI.Page
    {
        public static OleDbConnection oledbConn;
        DataTable dt = new DataTable();
        DataTable dtDistinct = new DataTable();
        static ArrayList _arrayPatientsCnt = new ArrayList();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
                TrainingDS();
            }
            catch
            {

            }
        }

        private void TrainingDS()
        {
            string FileName = "TrainingDataset.xls";

            string Extension = ".xls";

            string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

            string _Location = "TrainingDataset";

            string FilePath = Server.MapPath(FolderPath + FileName);

            ImportTrainingDS(FilePath, Extension, _Location);
        }

        private void ImportTrainingDS(string FilePath, string Extension, string _Location)
        {
            string conStr = "";

            switch (Extension)
            {
                case ".xls": //Excel 97-03

                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]

                             .ConnectionString;

                    break;

                case ".xlsx": //Excel 07

                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]

                              .ConnectionString;

                    break;

            }

            conStr = String.Format(conStr, FilePath, _Location);

            OleDbConnection connExcel = new OleDbConnection(conStr);

            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbCommand cmdDistinct = new OleDbCommand();
            OleDbCommand cmdPatientsCnt = new OleDbCommand();

            OleDbDataAdapter oda = new OleDbDataAdapter();
            OleDbDataAdapter odaDistinct = new OleDbDataAdapter();

            cmdExcel.Connection = connExcel;
            cmdDistinct.Connection = connExcel;
            cmdPatientsCnt.Connection = connExcel;
            //Get the name of First Sheet

            connExcel.Open();

            DataTable dtExcelSchema;

            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

            connExcel.Close();

            //Read Data from First Sheet

            connExcel.Open();

            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            cmdDistinct.CommandText = "SELECT DISTINCT(Result) From [" + SheetName + "]";

            oda.SelectCommand = cmdExcel;
            odaDistinct.SelectCommand = cmdDistinct;

            oda.Fill(dt);
            odaDistinct.Fill(dtDistinct);

            //BLL obj = new BLL();

            if (dt.Rows.Count > 0)
            {
                if (dtDistinct.Rows.Count > 0)
                {
                    //for (int i = 0; i < dtDistinct.Rows.Count; i++)
                    //{
                    //    cmdPatientsCnt.CommandText = "SELECT COUNT(*) From [" + SheetName + "] where result=" + dtDistinct.Rows[i]["result"].ToString() + "";
                    //    _arrayPatientsCnt.Add(cmdPatientsCnt.ExecuteScalar());
                    //}
                }

                connExcel.Close();

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('No Training Dataset!!!')</script>");
            }
        }

        static string _output;

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string _data = DropDownList1.SelectedValue + "," +
                                  DropDownList2.SelectedValue + "," +
                                 DropDownList3.SelectedValue + "," +
                                  DropDownList4.SelectedValue + "," +
                                  DropDownList5.SelectedValue + "," +
                                  DropDownList6.SelectedValue + "," +
                                  DropDownList7.SelectedValue + "," +
                                  DropDownList8.SelectedValue + "," +
                                  DropDownList9.SelectedValue + "," +
                                  DropDownList10.SelectedValue + "," +
                                  DropDownList11.SelectedValue + "," +
                                  DropDownList12.SelectedValue;


                string[] values = _data.Split(',');

                _output = NaiveBayes(values);

                lblResult.ForeColor = System.Drawing.Color.Green;
                lblResult.Font.Bold = true;
                lblResult.Font.Size = 16;
                lblResult.Text = "Result: " + _output;

               
               

                //Response.Redirect(string.Format("StudentGraph.aspx?p1={0}", _output));
            }
            catch
            {

            }
        }

     

        ArrayList output = new ArrayList();
        ArrayList mul = new ArrayList();

        //function to load subject
        public ArrayList GetSubject()
        {

            ArrayList s = new ArrayList();

            if (dtDistinct.Rows.Count > 0)
            {
                s.Clear();

                for (int i = 0; i < dtDistinct.Rows.Count; i++)
                {
                    if (dtDistinct.Rows[i]["Result"].Equals(""))
                    {
                    }
                    else
                    {
                        s.Add(dtDistinct.Rows[i]["Result"].ToString());
                    }
                }
            }

            return s;

        }

        double pi;
        int nc, n;
        double result;

        //function which contains the algorithm steps
        private string NaiveBayes(string[] values)
        {
            ArrayList s = new ArrayList();
            output.Clear();

            //try
            //{
            s = GetSubject();

            int m = 12;
            double numer = 1.0;
            double dino = double.Parse(s.Count.ToString());
            double p = numer / dino;

           

            for (int i = 0; i < s.Count; i++)
            {
                mul.Clear();

                for (int j = 0; j < 12; j++)
                {
                    n = 0;
                    nc = 0;

                    for (int d = 0; d < dt.Rows.Count; d++)
                    {
                        if (dt.Rows[d][j].ToString().Equals(values[j]))
                        {
                            ++n;

                            if (dt.Rows[d][m].ToString().Equals(s[i]))

                                ++nc;
                        }
                    }

                    double x = m * p;
                    double y = n + m;
                    double z = nc + x;

                    pi = z / y;
                    mul.Add(Math.Abs(pi));
                }

                double mulres = 1.0;

                for (int z = 0; z < mul.Count; z++)
                {
                    mulres *= double.Parse(mul[z].ToString());
                }

                result = mulres * p;
                output.Add(Math.Abs(result));
            }

            ArrayList list1 = new ArrayList();

            for (int x = 0; x < s.Count; x++)
            {
                list1.Add(output[x]);
            }

            list1.Sort();
            list1.Reverse();

            string _output = null;

            for (int y = 0; y < s.Count; y++)
            {
                if (output[y].Equals(list1[0]))
                {
                    _output = s[y].ToString();
                    return _output;
                }
            }
            //}
            //catch
            //{

            //}

            return _output;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            BLL obj = new BLL();

            if (obj.CheckOutput(Session["RegNo"].ToString()))
            {
                obj.InsertResult(Session["RegNo"].ToString(), DropDownList1.SelectedItem.Text, DropDownList2.SelectedItem.Text, DropDownList3.SelectedItem.Text, DropDownList4.SelectedItem.Text,
                    DropDownList5.SelectedItem.Text, DropDownList6.SelectedItem.Text, DropDownList7.SelectedItem.Text,
                    DropDownList8.SelectedItem.Text, DropDownList9.SelectedItem.Text, DropDownList10.SelectedItem.Text, DropDownList11.SelectedItem.Text, DropDownList12.SelectedItem.Text, _output);

                ClientScript.RegisterStartupScript(this.GetType(), "key", "<script>alert('Successful')</script>");
            }
            else
            {
                DataTable tab = new DataTable();
                tab = obj.GetResultsByRegNo(Session["RegNo"].ToString());

                obj.UpdateResult(Session["RegNo"].ToString(), DropDownList1.SelectedItem.Text, DropDownList2.SelectedItem.Text, DropDownList3.SelectedItem.Text, DropDownList4.SelectedItem.Text,
                    DropDownList5.SelectedItem.Text, DropDownList6.SelectedItem.Text, DropDownList7.SelectedItem.Text,
                    DropDownList8.SelectedItem.Text, DropDownList9.SelectedItem.Text, DropDownList10.SelectedItem.Text, DropDownList11.SelectedItem.Text, DropDownList12.SelectedItem.Text, _output, int.Parse(tab.Rows[0]["SolutionId"].ToString()));

                ClientScript.RegisterStartupScript(this.GetType(), "key", "<script>alert('Successful')</script>");
            }
        }

       
    }
}
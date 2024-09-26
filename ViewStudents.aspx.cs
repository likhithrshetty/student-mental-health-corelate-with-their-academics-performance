using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace educationalProject
{
    public partial class ViewStudents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminId"] == null)
            {
                Session.Abandon();
                Response.Redirect("UserLogin.aspx");
            }
            else
            {
                LoadAllStudents();
            }
        }

        //function to load all students
        private void LoadAllStudents()
        {
            try
            {
                DataTable tab = new DataTable();
                BLL obj = new BLL();

                int serialNo = 1;

                tab = obj.GetAllStudents();

                if (tab.Rows.Count > 0)
                {
                    tableStudents.Rows.Clear();

                    tableStudents.BorderStyle = BorderStyle.Double;
                    tableStudents.GridLines = GridLines.Both;
                    tableStudents.BorderColor = System.Drawing.Color.DarkGray;

                    TableRow mainrow = new TableRow();
                    mainrow.Height = 30;
                    mainrow.ForeColor = System.Drawing.Color.WhiteSmoke;

                    mainrow.BackColor = System.Drawing.Color.DeepSkyBlue;

                    TableCell cell1 = new TableCell();
                    cell1.Text = "<b>SerialNo</b>";
                    mainrow.Controls.Add(cell1);

                    TableCell cell21 = new TableCell();
                    cell21.Text = "<b>RegNo</b>";
                    mainrow.Controls.Add(cell21);

                    TableCell cell2 = new TableCell();
                    cell2.Text = "<b>Name</b>";
                    mainrow.Controls.Add(cell2);

                    TableCell cell5 = new TableCell();
                    cell5.Text = "<b>DeptName</b>";
                    mainrow.Controls.Add(cell5);

                    TableCell cell62 = new TableCell();
                    cell62.Text = "<b>Sem</b>";
                    mainrow.Controls.Add(cell62);

                    TableCell cell63 = new TableCell();
                    cell63.Text = "<b>Contact no</b>";
                    mainrow.Controls.Add(cell63);

                    TableCell cell64 = new TableCell();
                    cell64.Text = "<b>Email Id</b>";
                    mainrow.Controls.Add(cell64);
                                        
                    TableCell cell65 = new TableCell();
                    cell65.Text = "<b>Edit</b>";
                    mainrow.Controls.Add(cell65);

                    TableCell cell66 = new TableCell();
                    cell66.Text = "<b>Delete</b>";
                    mainrow.Controls.Add(cell66);

                    tableStudents.Controls.Add(mainrow);

                    for (int i = 0; i < tab.Rows.Count; i++)
                    {
                        TableRow row = new TableRow();

                        TableCell cellSerialNo = new TableCell();
                        cellSerialNo.Width = 50;
                        cellSerialNo.Text = serialNo + i + ".";
                        row.Controls.Add(cellSerialNo);

                        TableCell cellRegNo = new TableCell();
                        cellRegNo.Width = 150;
                        cellRegNo.Text = tab.Rows[i]["RegNo"].ToString();
                        row.Controls.Add(cellRegNo);

                        TableCell cellName = new TableCell();
                        cellName.Width = 150;
                        cellName.Text = tab.Rows[i]["Name"].ToString();
                        row.Controls.Add(cellName);

                        TableCell cellCourse = new TableCell();
                        cellCourse.Width = 150;
                        cellCourse.Text = tab.Rows[i]["DeptName"].ToString();
                        row.Controls.Add(cellCourse);

                        TableCell cellSem = new TableCell();
                        cellSem.Width = 80;
                        cellSem.Text = tab.Rows[i]["Sem"].ToString();
                        row.Controls.Add(cellSem);

                        TableCell cellContactNo = new TableCell();
                        cellContactNo.Width = 150;
                        cellContactNo.Text = tab.Rows[i]["Mobile"].ToString();
                        row.Controls.Add(cellContactNo);

                        TableCell cellEmailId = new TableCell();
                        cellEmailId.Width = 150;
                        cellEmailId.Text = tab.Rows[i]["EmailId"].ToString();
                        row.Controls.Add(cellEmailId);
                                                
                        TableCell cell_edit = new TableCell();
                        cell_edit.Width = 100;
                        LinkButton lbtn_edit = new LinkButton();
                        lbtn_edit.ForeColor = System.Drawing.Color.Red;
                        lbtn_edit.Font.Bold = true;
                        lbtn_edit.Text = "Edit";
                        lbtn_edit.ID = "Edit~" + tab.Rows[i]["RegNo"].ToString();
                        lbtn_edit.Click += new EventHandler(lbtn_edit_Click);
                        cell_edit.Controls.Add(lbtn_edit);
                        row.Controls.Add(cell_edit);

                        TableCell cell_delete = new TableCell();
                        cell_delete.Width = 100;
                        LinkButton lbtn_delete = new LinkButton();
                        lbtn_delete.ForeColor = System.Drawing.Color.Red;
                        lbtn_delete.Font.Bold = true;
                        lbtn_delete.Text = "Delete";

                        lbtn_delete.ID = "Delete~" + tab.Rows[i]["RegNo"].ToString();
                        lbtn_delete.OnClientClick = "return confirm('Are you sure want to delete this Student?')";
                        lbtn_delete.Click += new EventHandler(lbtn_delete_Click);
                        cell_delete.Controls.Add(lbtn_delete);
                        row.Controls.Add(cell_delete);

                        tableStudents.Controls.Add(row);
                    }
                }
                else
                {
                    tableStudents.Rows.Clear();

                    TableHeaderRow rno = new TableHeaderRow();
                    TableHeaderCell cellno = new TableHeaderCell();
                    cellno.ForeColor = System.Drawing.Color.Red;
                    cellno.Text = "No Students Found!!!";

                    rno.Controls.Add(cellno);
                    tableStudents.Controls.Add(rno);
                }
            }
            catch
            {

            }
        }

        void lbtn_delete_Click(object sender, EventArgs e)
        {
            BLL obj = new BLL();
            LinkButton lbtn = (LinkButton)sender;
            string[] s = lbtn.ID.ToString().Split('~');

            try
            {
                obj.DeleteStudent(s[1]);

                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('Student Deleted Successfully!!!')</script>");
                LoadAllStudents();

            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('Server Error!!!')</script>");
            }
        }

        void lbtn_edit_Click(object sender, EventArgs e)
        {
            BLL obj = new BLL();
            LinkButton lbtn = (LinkButton)sender;
            string[] s = lbtn.ID.ToString().Split('~');

            try
            {
                Response.Redirect(string.Format("AddStudents.aspx?regNo={0}", s[1]));
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('Server Error!!!')</script>");
            }
        }
    }
}
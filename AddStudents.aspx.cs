using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace educationalProject
{
    public partial class AddStudents : System.Web.UI.Page
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
                if (!this.IsPostBack)
                {                   
                    txtRegNo.Focus();

                    if (Request.QueryString["regNo"] == null)
                    {

                    }
                    else
                    {
                        LoadStudent();
                    }
                }

               
            }
        }

        //function to load student details
        private void LoadStudent()
        {
            BLL obj = new BLL();
            DataTable tab = new DataTable();
            tab = obj.GetStudentById(Request.QueryString["regNo"]);

            Session["regNo"] = null;
            Session["regNo"] = Request.QueryString["regNo"];                       

            if (tab.Rows.Count > 0)
            {
                txtRegNo.Text = tab.Rows[0]["RegNo"].ToString();
                //txtPassword.Text = tab.Rows[0]["Password"].ToString();
                txtName.Text = tab.Rows[0]["Name"].ToString();                
                txtMobile.Text = tab.Rows[0]["Mobile"].ToString();
                txtEmailId.Text = tab.Rows[0]["EmailId"].ToString();
                txtDept.Text = tab.Rows[0]["DeptName"].ToString();
                txtSem.Text = tab.Rows[0]["Sem"].ToString();              
            }

            btnSubmit.Text = "Update Student";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BLL obj = new BLL();

            if (btnSubmit.Text.Equals("Add Student"))
            {
                try
                {
                    if (obj.CheckStudentRegNo(txtRegNo.Text))
                    {
                        obj.InsertStudent(txtRegNo.Text, txtPassword.Text, txtName.Text, txtMobile.Text, txtEmailId.Text, txtDept.Text, int.Parse(txtSem.Text));
                        ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('New Student Added Successfully!!!')</script>");
                        ClearTxts();                       
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('RegNo Exists!!!')</script>");
                    }
                }
                catch
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('Server Error!')</script>");
                }
            }
            else if (btnSubmit.Text.Equals("Update Student"))
            {
                if (txtRegNo.Text.Equals(Session["regNo"].ToString()))
                {
                    try
                    {
                        obj.UpdateStudent(txtRegNo.Text, txtPassword.Text, txtName.Text, txtMobile.Text, txtEmailId.Text, txtDept.Text, int.Parse(txtSem.Text));

                        ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('Student Updated Successfully!!!')</script>");
                        ClearTxts();

                        btnSubmit.Text = "Add Student";

                    }
                    catch
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('Server Error!')</script>");
                    }
                }
                else
                {
                    if (obj.CheckStudentRegNo(txtRegNo.Text))
                    {
                        try
                        {
                            obj.UpdateStudent(txtRegNo.Text, txtPassword.Text, txtName.Text, txtMobile.Text, txtEmailId.Text, txtDept.Text, int.Parse(txtSem.Text));

                            ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('Student Updated Successfully!!!')</script>");
                            ClearTxts();

                            btnSubmit.Text = "Add Student";

                        }
                        catch
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('Server Error!')</script>");
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('RegNo Exists!!!')</script>");
                    }
                }
            }
        }

        //function to clear text box contents
        private void ClearTxts()
        {
            txtMobile.Text = txtEmailId.Text = txtName.Text = txtRegNo.Text = txtSem.Text = txtDept.Text = string.Empty;
            
        }

    }
}
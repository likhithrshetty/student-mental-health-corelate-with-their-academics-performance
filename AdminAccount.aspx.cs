using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace educationalProject
{
    public partial class AdminAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtOldPassword.Focus();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable tab = new DataTable();
                BLL obj = new BLL();
                tab.Rows.Clear();

                tab = obj.GetAdminById(Session["AdminId"].ToString());
                string oldPassword = tab.Rows[0]["Password"].ToString();

                if (txtOldPassword.Text.Equals(oldPassword))
                {
                    obj.UpdateAdminPassword(txtNewPassword.Text, Session["AdminId"].ToString());
                    ClientScript.RegisterStartupScript(this.GetType(), "key", "<script>alert('Admin Password changed successfully')</script>");

                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "key", "<script>alert('Admin Old password incorrect')</script>");
                }
            }
            catch
            {

            }
        }
    }
}
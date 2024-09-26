using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace educationalProject
{
    public partial class _StudentData : System.Web.UI.Page
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
                LoadData();
            }
        }

        //function to load all students
        private void LoadData()
        {
            try
            {
                DataTable tab = new DataTable();
                BLL obj = new BLL();

                int serialNo = 1;

                tab = obj.GetStudentData();

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
                    cell2.Text = "<b>Gender</b>";
                    mainrow.Controls.Add(cell2);

                    TableCell cell5 = new TableCell();
                    cell5.Text = "<b>Pressure</b>";
                    mainrow.Controls.Add(cell5);

                    TableCell cell62 = new TableCell();
                    cell62.Text = "<b>FamilyIssues</b>";
                    mainrow.Controls.Add(cell62);

                    TableCell cell63 = new TableCell();
                    cell63.Text = "<b>Fear</b>";
                    mainrow.Controls.Add(cell63);

                    TableCell cell64 = new TableCell();
                    cell64.Text = "<b>Anxiety</b>";
                    mainrow.Controls.Add(cell64);

                    TableCell cell65 = new TableCell();
                    cell65.Text = "<b>Obession</b>";
                    mainrow.Controls.Add(cell65);

                    TableCell cell651 = new TableCell();
                    cell651.Text = "<b>Paranoid</b>";
                    mainrow.Controls.Add(cell651);

                    TableCell cell652 = new TableCell();
                    cell652.Text = "<b>Nervousness</b>";
                    mainrow.Controls.Add(cell652);

                    TableCell cell653 = new TableCell();
                    cell653.Text = "<b>Injury</b>";
                    mainrow.Controls.Add(cell653);

                    TableCell cell654 = new TableCell();
                    cell654.Text = "<b>Interpersonal_sensitivity</b>";
                    mainrow.Controls.Add(cell654);

                    TableCell cell655 = new TableCell();
                    cell655.Text = "<b>Stage_fear</b>";
                    mainrow.Controls.Add(cell655);

                    TableCell cell656 = new TableCell();
                    cell656.Text = "<b>Stress</b>";
                    mainrow.Controls.Add(cell656);

                    TableCell cellres = new TableCell();
                    cellres.Text = "<b>Result</b>";
                    mainrow.Controls.Add(cellres);
                    

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

                        TableCell cellgender = new TableCell();
                        cellgender.Width = 150;
                        cellgender.Text = tab.Rows[i]["Gender"].ToString();
                        row.Controls.Add(cellgender);

                        TableCell cellpressure = new TableCell();
                        cellpressure.Width = 150;
                        cellpressure.Text = tab.Rows[i]["Pressure"].ToString();
                        row.Controls.Add(cellpressure);

                        TableCell cellfamilyissues = new TableCell();
                        cellfamilyissues.Width = 80;
                        cellfamilyissues.Text = tab.Rows[i]["FamilyIssues"].ToString();
                        row.Controls.Add(cellfamilyissues);

                        TableCell cellfear = new TableCell();
                        cellfear.Width = 150;
                        cellfear.Text = tab.Rows[i]["Fear"].ToString();
                        row.Controls.Add(cellfear);

                        TableCell cellanxiety= new TableCell();
                        cellanxiety.Width = 150;
                        cellanxiety.Text = tab.Rows[i]["Anxiety"].ToString();
                        row.Controls.Add(cellanxiety);

                        TableCell cellObsession = new TableCell();
                        cellObsession.Width = 150;
                        cellObsession.Text = tab.Rows[i]["Obsession"].ToString();
                        row.Controls.Add(cellObsession);

                        TableCell cellParanoid = new TableCell();
                        cellParanoid.Width = 150;
                        cellParanoid.Text = tab.Rows[i]["Paranoid"].ToString();
                        row.Controls.Add(cellParanoid);

                        TableCell cellNervousness = new TableCell();
                        cellNervousness.Width = 150;
                        cellNervousness.Text = tab.Rows[i]["Nervousness"].ToString();
                        row.Controls.Add(cellNervousness);

                        TableCell cellInjury = new TableCell();
                        cellInjury.Width = 150;
                        cellInjury.Text = tab.Rows[i]["Injury"].ToString();
                        row.Controls.Add(cellInjury);


                        TableCell cellInterpersonal_sensitivity = new TableCell();
                        cellInterpersonal_sensitivity.Width = 150;
                        cellInterpersonal_sensitivity.Text = tab.Rows[i]["Interpersonal_sensitivity"].ToString();
                        row.Controls.Add(cellInterpersonal_sensitivity);

                        TableCell cellStage_fear = new TableCell();
                        cellStage_fear.Width = 150;
                        cellStage_fear.Text = tab.Rows[i]["Stage_fear"].ToString();
                        row.Controls.Add(cellStage_fear);


                        TableCell cellStress = new TableCell();
                        cellStress.Width = 150;
                        cellStress.Text = tab.Rows[i]["Stress"].ToString();
                        row.Controls.Add(cellStress);

                        TableCell cellResult = new TableCell();
                        cellResult.Width = 150;
                        cellResult.Text = tab.Rows[i]["Result"].ToString();
                        row.Controls.Add(cellResult);
                       

                        tableStudents.Controls.Add(row);
                    }
                }
                else
                {
                    tableStudents.Rows.Clear();

                    TableHeaderRow rno = new TableHeaderRow();
                    TableHeaderCell cellno = new TableHeaderCell();
                    cellno.ForeColor = System.Drawing.Color.Red;
                    cellno.Text = "No Students Data Found!!!";

                    rno.Controls.Add(cellno);
                    tableStudents.Controls.Add(rno);
                }
            }
            catch
            {

            }
        }

       
    }
}
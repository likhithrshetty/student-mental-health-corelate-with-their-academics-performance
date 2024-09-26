<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMasterpage.Master" AutoEventWireup="true" CodeBehind="_StudentPrediction.aspx.cs" Inherits="educationalProject._StudentPrediction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="Panel1" runat="server">
    <!-- Start contact Area -->  
    <div id="about" class="about-area area-padding">
   <div class="container">
      <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
          <div class="section-headline text-center">
            <h2>Result Prediction - Enter Parameters</h2>
          </div>
        </div>
      </div>
      <div class="row">
        <!-- single-well start-->
       
        <!-- single-well end-->
        <div class="col-md-6 col-sm-6 col-xs-12">
          <div class="well-middle">
            <div class="single-well">
              
                <h4 class="sec-head">Student Parameters</h4>
                <table style="width:100%;">
                    <tr>
                        <td>
                            <p>
                                Enter Gender</p>
                        </td>
                        <td>
                            <p>
                                Enter Pressure</p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="DropDownList1" runat="server">
                                <asp:ListItem Value="1">male</asp:ListItem>
                                <asp:ListItem Value="2">female</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList2" runat="server" 
                                >
                                <asp:ListItem Value="0">no</asp:ListItem>
                                <asp:ListItem Value="1">yes</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h6>
                                Gender: 1- male , 2 - female</h6>
                        </td>
                        <td>
                            <h6>
                                Pressure: 0- No , 1 - Yes</h6>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <p>
                                Enter Family_Issues</p>
                        </td>
                        <td>
                            <p>
                                Enter Fear</p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="DropDownList3" runat="server" 
                               >
                                <asp:ListItem Value="0">no</asp:ListItem>
                                <asp:ListItem Value="1">yes</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList4" runat="server">
                                <asp:ListItem Value="0">no</asp:ListItem>
                                <asp:ListItem Value="1">yes</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h6>
                                Family_Issues: 0- No , 1 - Yes</h6>
                        </td>
                        <td>
                            <h6>
                                Fear: 0- No , 1 - Yes</h6>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <p>
                                Enter Anxiety</p>
                        </td>
                        <td>
                            <p>
                                Enter Obsession</p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="DropDownList5" runat="server" 
                                >
                                <asp:ListItem Value="1">yes</asp:ListItem>
                                <asp:ListItem Value="0">no</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList6" runat="server" 
                               >
                                <asp:ListItem Value="0">no</asp:ListItem>
                                <asp:ListItem Value="1">yes</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h6>
                                Anxiety: 1- Fair / 2- Not Good.</h6>
                        </td>
                        <td>
                            <h6>
                                Obsession: 0- No , 1 - Yes</h6>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <p>
                                Enter Paranoid</p>
                        </td>
                        <td>
                            <p>
                                Enter Nervousness</p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="DropDownList7" runat="server" 
                               >
                                <asp:ListItem Value="0">no</asp:ListItem>
                                <asp:ListItem Value="1">yes</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList8" runat="server">
                                <asp:ListItem Value="0">yes</asp:ListItem>
                                <asp:ListItem Value="1">no</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h6>
                                Paranoid: 0- No , 1 - Yes</h6>
                        </td>
                        <td>
                            <h6>
                                Nervousness: 0- No , 1 - Yes</h6>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <p>
                                Enter injury</p>
                        </td>
                        <td>
                            <p>
                                Enter Interpersonal sensitivity</p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="DropDownList9" runat="server" 
                                >
                                <asp:ListItem Value="0">no</asp:ListItem>
                                <asp:ListItem Value="1">yes</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList10" runat="server"  
                                >
                                <asp:ListItem Value="0">no</asp:ListItem>
                                <asp:ListItem Value="1">yes</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h6>
                                injury: 0- No , 1 - Yes</h6>
                        </td>
                        <td>
                            <h6>
                                Interpersonal sensitivity: 0- No , 1 - Yes</h6>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <p>
                                Enter Stage fear</p>
                        </td>
                        <td>
                            <p>
                                Enter Stress</p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="DropDownList11" runat="server">
                                <asp:ListItem Value="1">no</asp:ListItem>
                                <asp:ListItem Value="2">yes</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList12" runat="server">
                                <asp:ListItem Value="0">yes</asp:ListItem>
                                <asp:ListItem Value="1">no</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h6>
                                Stage fear: 1- No , 2 - Yes</h6>
                        </td>
                        <td>
                            <h6>
                                Stress: 1- 0- No , 1 - Yes</h6>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
             
              
             

               

                <br />

     <div>           
    <asp:Button ID="btnSubmit" runat="server" Text="Predict Academic Result" 
             ValidationGroup="a" onclick="btnSubmit_Click" Height="50px" 
              />
               <br />
               <br />
         <br />
         <asp:Label ID="lblResult" runat="server"></asp:Label>
               <br />
         <br />
         <p>
             <asp:Button ID="btnUpload" runat="server" Height="50px" 
                 onclick="btnUpload_Click" Text="Click here to Upload Data" 
                 ValidationGroup="a" />
         </p>
        
                </div>
             


            </div>
          </div>
        </div>
        <!-- End col-->
      </div>
    </div>
    </div>
  <!-- End Contact Area -->


    </asp:Panel>

</asp:Content>

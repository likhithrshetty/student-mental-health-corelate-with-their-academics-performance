<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterpage.Master" AutoEventWireup="true" CodeBehind="AddStudents.aspx.cs" Inherits="educationalProject.AddStudents" %>
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
            <h2>Add Students and thier Details</h2>
          </div>
        </div>
      </div>
      <div class="row">
        <!-- single-well start-->
       
        <!-- single-well end-->
        <div class="col-md-6 col-sm-6 col-xs-12">
          <div class="well-middle">
            <div class="single-well">
              <a href="#">
                <h4 class="sec-head">Register Students</h4>
              </a>
              
             

               

                <div class="form-group">
                <p>Enter RegNo</p>

            <asp:TextBox ID="txtRegNo" runat="server" Width="400px" Height="30px"></asp:TextBox>
                
                    
                    <br />
                
                    
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtRegNo" ErrorMessage="Enter RegNo" 
                        ValidationGroup="a" CssClass="error"></asp:RequiredFieldValidator>
                
                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator5" 
                        runat="server" ControlToValidate="txtRegNo" CssClass="error" ErrorMessage="*" 
                        ToolTip="Only Alphabets and Numbers Allowed" 
                        ValidationExpression="[a-zA-Z0-9]*$" ValidationGroup="a">Only Alphabets and Numbers Allowed</asp:RegularExpressionValidator>
                
                </div>
                <div class="form-group">
                 <p>Enter Password</p>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="400px" 
                        Height="30px"></asp:TextBox>
                 
                   
                    <br />
                 
                   
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtPassword" ErrorMessage="Enter Password" 
                        ValidationGroup="a" CssClass="error"></asp:RequiredFieldValidator>
                 
                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator4" 
                        runat="server" ControlToValidate="txtPassword" CssClass="error" 
                        Display="Dynamic" 
                        ErrorMessage="Password Must be 8 Characters including 1 Uppercase letter, 1 Special Character and Alphanumeric Characters." 
                        ForeColor="#FF3300" 
                        ToolTip="Password Must be 8 Characters including 1 Uppercase letter, 1 Special Character and Alphanumeric Characters." 
                        ValidationExpression="(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$" 
                        ValidationGroup="a">Password 
                                    Must be 8 Characters including 1 Uppercase letter, 1 Special Character and 
                                    Alphanumeric Characters.</asp:RegularExpressionValidator>
                 
                </div>

                 <div class="form-group">
                  <p>Enter Name</p>
                  <asp:TextBox ID="txtName" runat="server" Width="400px" Height="30px"></asp:TextBox>
                
                   
                     <br />
                
                   
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="txtName" ErrorMessage="Enter Name" 
                        ValidationGroup="a" CssClass="error"></asp:RequiredFieldValidator>
                     &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                         runat="server" ControlToValidate="txtName" CssClass="error" ErrorMessage="*" 
                         ToolTip="Only Alphabetes" ValidationExpression="^[a-zA-Z ]*$" 
                         ValidationGroup="a">Only Alphabetes</asp:RegularExpressionValidator>
                </div>

                 <div class="form-group">
                  <p>Enter Mobile</p>
                  <asp:TextBox ID="txtMobile" runat="server" Width="400px" Height="30px"></asp:TextBox>
                
                   
                     <br />
                
                   
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="txtMobile" ErrorMessage="Enter Mobile" 
                        ValidationGroup="a" CssClass="error"></asp:RequiredFieldValidator>
                     &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                         runat="server" ControlToValidate="txtMobile" CssClass="error" ErrorMessage="*" 
                         ToolTip="Invalid Mobile Number" ValidationExpression="^[6-9]\d{9}$" 
                         ValidationGroup="a">Invalid Mobile Number</asp:RegularExpressionValidator>
                </div>

                 <div class="form-group">
                  <p>Enter EmailId</p>
                  <asp:TextBox ID="txtEmailId" runat="server" Width="400px" Height="30px"></asp:TextBox>
                
                   
                     <br />
                
                   
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ControlToValidate="txtEmailId" ErrorMessage="Enter Email Id" 
                        ValidationGroup="a" CssClass="error"></asp:RequiredFieldValidator>
                     &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator3" 
                         runat="server" ControlToValidate="txtEmailId" CssClass="error" ErrorMessage="*" 
                         ToolTip="Invalid EmailId" 
                         ValidationExpression=".+@(gmail|yahoo|Gmail|Yahoo|rediff|Rediff)\.com$" 
                         ValidationGroup="a">Invalid EmailId</asp:RegularExpressionValidator>
                </div>

                 <div class="form-group">
                  <p>Enter Course</p>
                  <asp:TextBox ID="txtDept" runat="server" Width="400px" Height="30px"></asp:TextBox>
                
                   
                     <br />
                
                   
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                        ControlToValidate="txtDept" ErrorMessage="Enter Dept Name" 
                        ValidationGroup="a" CssClass="error"></asp:RequiredFieldValidator>
                     &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator6" 
                         runat="server" ControlToValidate="txtDept" CssClass="error" ErrorMessage="*" 
                         ToolTip="Only Alphabetes" ValidationExpression="^[a-zA-Z ]*$" 
                         ValidationGroup="a">Only Alphabetes</asp:RegularExpressionValidator>
                </div>

                 <div class="form-group">
                  <p>Enter Semester</p>
                  <asp:TextBox ID="txtSem" runat="server" Width="400px" Height="30px"></asp:TextBox>
                 <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                        ControlToValidate="txtSem" ErrorMessage="Enter Semester" 
                        ValidationGroup="a" CssClass="error"></asp:RequiredFieldValidator>
                     &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator7" 
                         runat="server" ControlToValidate="txtSem" CssClass="error" ErrorMessage="*" 
                         ToolTip="Numbers Allowed" ValidationExpression="[0-9]*$" ValidationGroup="a">Numbers Allowed</asp:RegularExpressionValidator>
                </div>

     <div>           
    <asp:Button ID="btnSubmit" runat="server" Text="Add Student" ValidationGroup="a" onclick="btnSubmit_Click" 
              />
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

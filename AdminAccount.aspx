<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterpage.Master" AutoEventWireup="true" CodeBehind="AdminAccount.aspx.cs" Inherits="educationalProject.AdminAccount" %>
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
            <h2>Administrator Change Password</h2>
          </div>
        </div>
      </div>
      <div class="row">
        <!-- single-well start-->
       
        <!-- single-well end-->
       
              <a href="#">
                <h4 class="sec-head">update password</h4>
              </a>
              
                                                                       
                <br />

			
            
             
             
               <div class="field your-name form-group">
               <p>Old Password</p>
                    <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" Height="30px" Width="400px"></asp:TextBox>
                
                  <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="txtOldPassword" ErrorMessage="Enter Old Password" 
                        ValidationGroup="a" CssClass="error"></asp:RequiredFieldValidator>
                
               </div>

               

               
               <div class="field your-name form-group">
                <p>New Password</p>
                    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" Height="30px" Width="400px"></asp:TextBox>
                
                  <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtNewPassword" ErrorMessage="Enter New Password" 
                        ValidationGroup="a" CssClass="error"></asp:RequiredFieldValidator>
                
               </div>

               
               <div class="field your-name form-group">
                <p>Confirm Password</p>
                     <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" Height="25px" 
                         Width="385px"></asp:TextBox>
                 
                   <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtConfirmPassword" ErrorMessage="Enter Confirm Password" 
                        ValidationGroup="a" CssClass="error"></asp:RequiredFieldValidator>
                 
                </div>
            <div>           
            <asp:Button ID="btnUpdate" runat="server" Text="Change Password" 
                    class="btn btn-theme pull-left" ValidationGroup="a" onclick="btnUpdate_Click" 
             />
               </div>
               <br />
              <marquee scrolldelay="150" behavior="alternate">
          <img src="../img/Edu_Banner1.jpg"" width="460" height="200" alt="" /> &nbsp
          <img src="../img/Edu_Banner2.jpg" width="460" height="200" alt="" /> &nbsp
         <img src="../img/Edu_Banner3.jpg" width="460" height="200" alt="" /> &nbsp
          </marquee>


    </asp:Panel>

</asp:Content>

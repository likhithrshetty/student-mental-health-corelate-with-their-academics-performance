<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterpage.Master" AutoEventWireup="true" CodeBehind="Contactus.aspx.cs" Inherits="educationalProject.Contactus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <asp:Panel ID="Panel1" runat="server">
   
  <div id="contact" class="contact-area">
    <div class="contact-inner area-padding">
      <div class="contact-overly"></div>
      <div class="container ">
        <div class="row">
          <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="section-headline text-center">
              <h2>Contact us</h2>
            </div>
          </div>
        </div>
       
       <table style="width:100%;"><tr><td><p>
				<h3>P.E.S College of Engineering</h3>
<p>K V Shankaragowda Rd, </p>
<p>PES College Campus, </p> 
<p>Mandya, Karnataka 571401  </p>

<p>Mobile No: 9448282588  </p> 

<p>Office No: 08232 220043  </p> 


			</p></td><td>
<asp:Image ID="Image4" runat="server" ImageUrl="~/img/contact-us.png"></asp:Image>

			    </td></tr><tr><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td></tr></table>



      </div>
    </div>
  </div>
  <!-- End Contact Area -->


    </asp:Panel>
</asp:Content>

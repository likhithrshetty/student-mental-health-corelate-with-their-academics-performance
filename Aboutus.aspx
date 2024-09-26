<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterpage.Master" AutoEventWireup="true" CodeBehind="Aboutus.aspx.cs" Inherits="educationalProject.Aboutus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <asp:Panel ID="Panel1" runat="server">
     <!-- Start About area -->
  <div id="about" class="about-area area-padding">
    <div class="container">
      <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
          <div class="section-headline text-center">
            <h2>About Education Sector</h2>
          </div>
        </div>
      </div>
      <div class="row">
        <!-- single-well start-->
        <div class="col-md-6 col-sm-6 col-xs-12">
          <div class="well-left">
            <div class="single-well">
              <a href="#">
								  <img src="img/aboutus.jpeg" alt="">
								</a>
            </div>
          </div>
        </div>
        <!-- single-well end-->
        <div class="col-md-6 col-sm-6 col-xs-12">
          <div class="well-middle">
            <div class="single-well">
              <a href="#">
                <h4 class="sec-head">P.E.S College of Engineering</h4>
              </a>
              <p>
               P. E. S. College of Engineering (PESCE, standing for "People's Education Society College of Engineering") is an autonomous institute and technical engineering college located in Mandya, Karnataka, India. Established in 1962, it is run by the People's Education Trust.[1] It is an autonomous institute from the year 2008 under Visvesvaraya Technological University, Belgaum[2] and is recognized by AICTE.
              </p>
              
            </div>
          </div>
        </div>
        <!-- End col-->
      </div>
    </div>
  </div>
  <!-- End About area -->


    </asp:Panel>
</asp:Content>

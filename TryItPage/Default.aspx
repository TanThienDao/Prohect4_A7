<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TryItPage._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Project 4 A7</h1>
        <p class="lead">Click on the tabs above for service.</p>
        
    </div>

    <div>
        <asp:XmlDataSource ID="XmlDataSource1" runat="server" 
        DataFile="http://www.public.asu.edu/~ttdao3/Computer.xml" 
        XPath="/Computers/Computer/*"></asp:XmlDataSource>
   

   <asp:TreeView ID="TreeView1" runat="server" DataSourceID="XmlDataSource1">
       <DataBindings>
           
       </DataBindings>
    </asp:TreeView>
        
    </div>

</asp:Content>

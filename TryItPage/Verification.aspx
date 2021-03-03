<%@ Page Title="Verification" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Verification.aspx.cs" Inherits="TryItPage.Verification" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <p>Web operation “verification” takes the URL of an XML (.xml) file and the URL of the
            corresponding XMLS (.xsd) file as input and validates the XML file against the corresponding
            XMLS (XSD) file.

        </p>
    </div>
    <div>

        <asp:Label ID="Label1" runat="server" Text="Enter the URL for XML file (.xml)"></asp:Label>
        <br />
        <asp:TextBox ID="XmlBox" runat="server" Width="643px"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Enter the URL for the XMLS (XSD) file (.xsd) "></asp:Label>
        <br />
        <asp:TextBox ID="XSDBox" runat="server" Width="640px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="VerifyButton" runat="server" OnClick="VerifyButton_Click" Text="Verify" />
        <br />
        <asp:Label ID="ResultLabel" runat="server" Text="Result:"></asp:Label>

    </div>
    </asp:Content>

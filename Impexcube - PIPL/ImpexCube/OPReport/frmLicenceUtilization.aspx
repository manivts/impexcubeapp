<%@ Page Title="" Language="C#" MasterPageFile="~/OPReport/OperationReport.Master" AutoEventWireup="true" 
CodeBehind="frmLicenceUtilization.aspx.cs" Inherits="ImpexCube.OPReport.frmLicenceUtilization" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            font-family: Verdana;
            font-size: 8pt;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="1000px">
    <tr>
    <td>
        <asp:Label ID="Label1" runat="server" Text="Licence Reference No." CssClass="fontsize"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="txtLicrefno" runat="server" CssClass="textbox200"></asp:TextBox>
    </td>
    <td>
        <asp:Label ID="Label3" runat="server" Text="Report Type" CssClass="fontsize"></asp:Label>
    </td>
    <td>
        <asp:DropDownList ID="ddlReporttype" runat="server" CssClass="ddl150">
        </asp:DropDownList>
    </td>
    <td>
        <asp:Button ID="btngenerate" runat="server" Text="Generate" 
            CssClass="stylebutton" onclick="btngenerate_Click"/>
    </td>
    <td>
        <asp:Button ID="btnprint" runat="server" Text="Print" CssClass="stylebutton"/>
    </td>
    <td>
        <asp:Button ID="btnsendmail" runat="server" Text="Send Mail" CssClass="stylebutton"/>
    </td>
    </tr></table>
    <table style="width: 989px"><tr>
    <td colspan="6">&nbsp;</td>
    </tr>
        <tr>
    <td><asp:Label ID="Label2" runat="server" Text="Owner" CssClass="style1"></asp:Label></td><td><asp:Label ID="lblowner" runat="server" Text="" CssClass="fontsize"></asp:Label></td>
    </tr>
    <tr>
    <td><asp:Label ID="Label5" runat="server" Text="Licence No." CssClass="style1"></asp:Label></td><td><asp:Label ID="lbllicenceno" runat="server" Text="" CssClass="fontsize"></asp:Label></td><td>
        <asp:Label ID="Label8" runat="server" Text="Licence Date" CssClass="style1"></asp:Label></td><td><asp:Label ID="lblLicenceDate" runat="server" Text="" CssClass="fontsize"></asp:Label></td><td>
        <asp:Label ID="Label10" runat="server" Text="Expiry Date" CssClass="style1"></asp:Label></td><td><asp:Label ID="lblExpirydate" runat="server" Text="" CssClass="fontsize"></asp:Label></td>
    </tr>
    <tr>
    <td><asp:Label ID="Label11" runat="server" Text="EDI Regn No." CssClass="style1"></asp:Label></td><td><asp:Label ID="lblediregnno" runat="server" Text="" CssClass="fontsize"></asp:Label></td><td>
        <asp:Label ID="Label13" runat="server" Text="Regn Date" CssClass="style1"></asp:Label></td><td><asp:Label ID="lblregndate" runat="server" Text="" CssClass="fontsize"></asp:Label></td><td>
        <asp:Label ID="Label15" runat="server" Text="Port Of Regn." CssClass="style1"></asp:Label></td><td><asp:Label ID="lblportofregn" runat="server" Text="" CssClass="fontsize"></asp:Label></td>
    </tr>
    <tr>
    <td><asp:Label ID="Label17" runat="server" Text="Type" CssClass="style1"></asp:Label></td><td><asp:Label ID="lbltype" runat="server" Text="" CssClass="fontsize"></asp:Label></td><td>
        <asp:Label ID="Label19" runat="server" Text="Available Value" CssClass="style1"></asp:Label></td>
        <td colspan="3"><asp:Label ID="lblavailablevalue" runat="server" Text="" CssClass="fontsize"></asp:Label></td>
    </tr>
    <tr>
    <td colspan="6">&nbsp;</td>
    </tr>
   </table>
   <table width="1000px" >
   <tr><td>
        <cc1:TabContainer runat="server" ActiveTabIndex="1" 
            Width="980px" height="400px" BackColor="White" CssClass="cs-accbutton">
            <cc1:TabPanel runat="server" HeaderText="Import" ID="TabPanelImport">
            </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="Export" ID="TabPanel2">
            </cc1:TabPanel>
        </cc1:TabContainer></td></tr>
    </table>
</asp:Content>

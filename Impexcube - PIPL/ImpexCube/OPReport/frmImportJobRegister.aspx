<%@ Page Title="" Language="C#" MasterPageFile="~/OPReport/OperationReport.Master" AutoEventWireup="true" CodeBehind="frmImportJobRegister.aspx.cs" Inherits="ImpexCube.OPReport.frmImportJobRegister" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlImportJobRegister" runat="server">
    <table>
    <tr>
    <td>
        <asp:Label ID="lblTransportMode" runat="server" Text="Transport Mode" 
            CssClass="fontsize"></asp:Label>
    </td>
    <td>
        <asp:DropDownList ID="ddlTransportMode" runat="server" CssClass="ddl150"> 
            <asp:ListItem>Any</asp:ListItem>
            <asp:ListItem>Air</asp:ListItem>
            <asp:ListItem>Sea</asp:ListItem>
        </asp:DropDownList>
    </td>
    <td>
        <asp:Label ID="lblImporter" runat="server" Text="Importer" CssClass="fontsize"></asp:Label>
    </td>
    <td>
    
        <asp:TextBox ID="txtImporter" runat="server" CssClass="textbox150"></asp:TextBox></td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="lblJobStatus" runat="server" Text="JobStatus" 
            CssClass="fontsize"></asp:Label>
    </td>
    <td>
        <asp:DropDownList ID="ddlJobStatus" runat="server" CssClass="ddl150" >
            <asp:ListItem>All</asp:ListItem>
            <asp:ListItem>pending</asp:ListItem>
            <asp:ListItem>Completed</asp:ListItem>
        </asp:DropDownList>
    </td>
    <td>
        <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="fontsize"></asp:Label>

    </td>
    <td>
    
        <asp:TextBox ID="txtDate" runat="server" CssClass="textbox150"></asp:TextBox></td>
        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate">
                                                    </cc1:CalendarExtender>
    </tr>
    <tr>
    <td>
        <asp:Label ID="lblBranch" runat="server" Text="Branch" CssClass="fontsize"></asp:Label>
    </td>
    <td>
        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="ddl150">
            
        </asp:DropDownList>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Button ID="btnGenerate" runat="server" Text="Generate" 
            CssClass="stylebutton" onclick="btnGenerate_Click" />
    </td>
    <td>
    
        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="stylebutton" />
        </td>
        <td class="fontsize">
            <asp:Button ID="btnSendMail" runat="server" Text="Send Mail" 
                CssClass="stylebutton" />
        </td>
    </tr>
    </table>
    <div>
    <table>
    
    <tr>
    <td>
        <asp:GridView ID="gvImporterJob" runat="server" AutoGenerateColumns="False" CssClass="table-wrapper">
      <Columns>
      <asp:BoundField DataField="Importer" HeaderText="Importer"  />
       <asp:BoundField DataField="JobNo" HeaderText="Job No"  />
       <asp:BoundField DataField="BEType" HeaderText="Type of BE" />
       <asp:BoundField DataField="Description" HeaderText="Description"  />
       <asp:BoundField DataField="InvoiceNumber" HeaderText="Invoice Number" />
       <asp:BoundField DataField="InvoiceDate" HeaderText="Invoice Date" />
       <asp:BoundField DataField="Vessel/Freight" HeaderText="Vessel/Freight"  />
       <asp:BoundField DataField="PortOfShipment" HeaderText="Port Of Shipment"  />
       <asp:BoundField DataField="PortOfDestination" HeaderText="Port Of Destination"  />
       <asp:BoundField DataField="GrossWeight" HeaderText="Gross Weight"  />
      </Columns>
        </asp:GridView>
    </td>
    </tr>
    </table>
    </div>
    </asp:Panel>
</asp:Content>

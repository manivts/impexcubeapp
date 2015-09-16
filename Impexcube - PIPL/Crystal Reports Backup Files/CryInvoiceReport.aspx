<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="CryInvoiceReport.aspx.cs" Inherits="ImpexCube.CryInvoiceReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div>
         <div>
   
    <table style="width: 100%; height: 400px; border: 1px;">
    
        <tr>
      <td style="height:90%; vertical-align: top; background-color: white; width: 975px;">
    
    <table>
    <tr>
    <td>
        <asp:Label ID="Label1" runat="server" Text="Select Export Type :" Font-Names="verdana" Font-Size="8pt"></asp:Label>
    </td>
    <td>
        <asp:DropDownList ID="drTpye" Font-Names="verdana" Font-Size="8pt" runat="server">
            <asp:ListItem Value="PD">PDF File Format</asp:ListItem>
            <asp:ListItem Value="WF">Word Format</asp:ListItem>
        </asp:DropDownList>
    </td>
    <td>
        <asp:Button ID="BtnExport" runat="server" Font-Names="verdana" Font-Size="8pt" 
            Text="Export File" onclick="BtnExport_Click" />
    </td>
    </tr>
    </table>
                                        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                                            AutoDataBind="true"/>
     </td>
        </tr>
        
        </table>
    </div>
        
        <br />
        </div>
</asp:Content>

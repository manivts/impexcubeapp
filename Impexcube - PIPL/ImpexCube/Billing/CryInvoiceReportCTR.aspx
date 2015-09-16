<%@ Page Language="C#" MasterPageFile="~/Billing/MasterPage1.master" AutoEventWireup="true" Inherits="CryInvoiceReportCTR" Codebehind="CryInvoiceReportCTR.aspx.cs" %>
<%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        &nbsp;&nbsp;&nbsp;
        <table>
         <tr>
     <td>
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
                                        <%--<CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                                            AutoDataBind="true"  />--%>
       
     </td>
     </tr>
        </table>
        </div>
   
</asp:Content> 
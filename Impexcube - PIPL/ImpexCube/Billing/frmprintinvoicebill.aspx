<%@ Page Title="" Language="C#" MasterPageFile="~/Billing/MasterPage.master" AutoEventWireup="true" CodeBehind="frmprintinvoicebill.aspx.cs" Inherits="ImpexCube.Billing.frmprintinvoicebill" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div>
        <table>
            <tr>
                <td style="height: 90%; vertical-align: top; background-color: white; width: 100%;">
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
            Text="Export File" />
    </td>
    </tr>
                   
                    </table>
                    
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

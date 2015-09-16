<%@ Page Language="C#" MasterPageFile="~/Billing/MasterPage.master" AutoEventWireup="true" Inherits="frmjobupdate" Title="Untitled Page" Codebehind="frmjobupdate.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
    <table align="center">
        <tr>
            <td colspan="4" style="text-align: left">
             <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" EnableCaching="true"
                    MinimumPrefixLength="1" ServiceMethod="GetJNO" CompletionListCssClass="completionList"
                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                    ServicePath="~/AutoComplete.asmx" TargetControlID="txtjobno">
                </cc1:AutoCompleteExtender>
                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" TargetControlID="txtjobno"
                    WatermarkCssClass="waterText" WatermarkText="70000" runat="server">
                </cc1:TextBoxWatermarkExtender>
               
                <asp:SqlDataSource ID="ConnectionImpex" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PIPLConnectionString5 %>" 
                    SelectCommand="SELECT DISTINCT [invoice] FROM [iec_invoiceNew]">
                </asp:SqlDataSource>
               
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <asp:Label ID="Label23" runat="server" Font-Size="12pt" ForeColor="#000066" Text="Job Update"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center; height: 18px;">
                <asp:Label ID="Label25" runat="server" Font-Size="10pt" Text="Invoice No"></asp:Label>
            </td>
            <td style="text-align: left; height: 18px;">
                <asp:DropDownList ID="ddlinvoiceno" runat="server" AppendDataBoundItems="True" 
                    AutoPostBack="True" DataSourceID="ConnectionImpex" DataTextField="invoice" 
                    DataValueField="invoice" 
                    onselectedindexchanged="DropDownList1_SelectedIndexChanged" Width="130px">
                    <asp:ListItem>~Select~</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="text-align: center; height: 18px;">
                <asp:Label ID="Label26" runat="server" Font-Size="10pt" Text="Invoice Date"></asp:Label>
            </td>
            <td style="text-align: center; height: 18px;">
                <asp:TextBox ID="txtinvdate" runat="server" Enabled="False"></asp:TextBox>
                  <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtinvdate">
    </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td style="text-align: center; height: 18px;">
                <asp:Label ID="Label24" runat="server" Font-Size="10pt" Text="Job No"></asp:Label>
            </td>
            <td style="text-align: left; height: 18px;">
                <asp:TextBox ID="txtjobno" runat="server" style="margin-top: 0px" Width="130px"></asp:TextBox>
            </td>
            <td style="text-align: center; height: 18px;">
                <asp:Button ID="btnadd" runat="server" onclick="btnadd_Click" Text="Add" />
            </td>
            <td style="text-align: left; height: 18px;">
                <asp:ListBox ID="lbjobno" runat="server" Height="100px" Width="120px" 
                    SelectionMode="Multiple" style="text-align: left; margin-top: 0px">
                </asp:ListBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: center; height: 16px;" colspan="4">
                &nbsp;
                &nbsp;
                <asp:Button ID="btnupdate" runat="server" onclick="btnupdate_Click" 
                    Text="Update" />
                &nbsp;
                &nbsp;
                <asp:Button ID="btnexit" runat="server" onclick="btnexit_Click" Text="Exit" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;
            </td>
            <td style="text-align: center">
                &nbsp;
            </td>
            <td style="text-align: center">
                &nbsp;
            </td>
            <td style="text-align: center">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>

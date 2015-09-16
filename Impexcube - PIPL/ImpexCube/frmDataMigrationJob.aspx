<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmDataMigrationJob.aspx.cs" Inherits="ImpexCube.frmDataMigrationJob" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.60919.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script type="text/javascript">
     function Validate() {
         var JobNO = document.getElementById('ContentPlaceHolder1_txtJobNo').value
         var FYear = document.getElementById('ContentPlaceHolder1_ddlFyear').value
         var Job = "IMP/" + JobNO + "/" + FYear;
         var status = confirm("Do You Want To Sync The Particular Job - "+Job+"  ???");
         if (status == true) {
             return true;
         }
         else {
             return false;
         }
     }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 30%;" align="center">
        <tr>
            <td colspan="2">
                </td>
            <td>
                </td>
            <td>
                </td>
            <td>
                </td>
            <td colspan="2">
                </td>
            <td>
                </td>
            <td>
                </td>
            <td colspan="2">
                </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
                <asp:Label ID="Label1" runat="server" Text="Job No" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtJobNo" runat="server" ></asp:TextBox>
                <cc1:AutoCompleteExtender ID="autoComplete2" runat="server" CompletionListCssClass="completionList"
                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                    EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetMySQLJob" ServicePath="~/AutoComplete.asmx"
                    TargetControlID="txtJobNo">
                </cc1:AutoCompleteExtender>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="FYear" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlFyear" runat="server">
                 <asp:ListItem>2015-2016</asp:ListItem>
                    <asp:ListItem>2014-2015</asp:ListItem>
                    <asp:ListItem>2013-2014</asp:ListItem>
                    <asp:ListItem>2012-2013</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td colspan="2">
                </td>
            <td>
                </td>
            <td>
                </td>
            <td colspan="2">
                </td>
        </tr>
        <tr>
            <td colspan="11">
                
                <asp:Label ID="lblJobNo" runat="server"></asp:Label>
                
            </td>
        </tr>
        <tr>
            <td colspan="11">
                </td>
        </tr>
        <tr>
            <td align="center" colspan="11">
                <asp:Button ID="btnJobCreation" runat="server" CssClass="stylebutton" Text="Sync Job"
                    OnClick="btnJobCreation_Click" OnClientClick="javascript:return Validate();"  />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="11">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                </td>
            <td colspan="2">
                <asp:Button ID="btnShipment" runat="server" CssClass="stylebutton" Text="Shipment"
                    OnClick="btnShipment_Click" Visible="False" />
            </td>
            <td colspan="3">
                <asp:Button ID="btnShipmentCon" runat="server" CssClass="stylebutton" Text="Shipment Container"
                    OnClick="btnShipmentCon_Click" Visible="False" />
            </td>
            <td colspan="4">
                <asp:Button ID="btnInvoice" runat="server" CssClass="stylebutton" Text="Invoice"
                    OnClick="btnInvoice_Click" Visible="False" />
            </td>
            <td>
                <asp:Button ID="btnProduct" runat="server" CssClass="stylebutton" Text="Product"
                    OnClick="btnProduct_Click" Visible="False" />
            </td>
        </tr>
        <tr>
            <td colspan="11">
                
                </td>
        </tr>
    </table>
</asp:Content>

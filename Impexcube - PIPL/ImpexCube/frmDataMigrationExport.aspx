<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="frmDataMigrationExport.aspx.cs" Inherits="ImpexCube.frmDataMigrationExport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function isNumeric(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
        return true;
    }
</script>
    <style type="text/css">
        .style1
        {
            font-size: small;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div>
<table align="center" style="height: 52px; width: 32%">
<tr>
<td colspan="2" style="text-align: center">

    Job Sync</td>
</tr>
<tr>
<td class="fontsize">

    Job No:</td>
<td>

    <asp:TextBox ID="txtJobNo" runat="server" CssClass="textbox175" MaxLength="7" 
        onkeypress="javascript:return isNumeric(event)"></asp:TextBox>

                <asp:DropDownList ID="ddlFyear" runat="server">
                 <asp:ListItem>2015-2016</asp:ListItem>
                    <asp:ListItem>2014-2015</asp:ListItem>
                    <asp:ListItem>2013-2014</asp:ListItem>
                    <asp:ListItem>2012-2013</asp:ListItem>
                </asp:DropDownList>

</td>
</tr>
<tr>
<td class="fontsize">

    &nbsp;</td>
<td>

    <asp:Label ID="lblJobNo" runat="server"></asp:Label>

</td>
</tr>
<tr>
<td class="fontsize">

    &nbsp;</td>
<td>

    <asp:Button ID="btnJobSync" runat="server" CssClass="blue" 
        onclick="btnJobSync_Click" Text="Sync" />

</td>
</tr>
<tr>
<td colspan="2" style="text-align: center">

    <asp:Label ID="lblmsg" runat="server" style="font-weight: 700"></asp:Label>
    </td>
</tr>
</table>
</div>
</asp:Content>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frmSchemeDetails.ascx.cs"
    Inherits="ImpexCube.frmSchemeDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .style1
    {
        height: 23px;
    }
</style>
<link href="Content/Styles/StandardTool.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    function scheme() {
        try {
            var scheme = document.getElementById('ContentPlaceHolder1_schemedetails_ddlSchemeName');
            var selectedText = scheme.options[scheme.selectedIndex].text;
            var table = document.getElementById('ContentPlaceHolder1_schemedetails_gvScheme');
            var grid = document.getElementById("<%= gvScheme.ClientID %>");
            if (selectedText != "DUTIABLE") {
                grid.rows[0].cells[5].innerHTML = selectedText;
            }
            else {
                grid.rows[0].cells[5].innerHTML = "Scheme Type";
            }
        }
        catch (err) {
            alert(err);
        }
    }

</script>
<table style="width: 75%;">
    <tr>
        <td colspan="12" align="center">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="12" align="center">
            &nbsp;
            <asp:Label ID="Label12" runat="server" Text="SCHEME DETAILS" Font-Bold="True" Font-Italic="True"
                Font-Names="Verdana" ForeColor="#993333" Width="200px"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="12" align="center">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="userctrllabel">
            Scheme Name :
        </td>
        <td colspan="11">
            <asp:DropDownList ID="ddlSchemeName" runat="server" Width="300px" CssClass="span3v required"
                onchange="javascript: scheme();" 
                onselectedindexchanged="ddlSchemeName_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Text="EDIRegNo" CssClass="userctrllabel"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label3" runat="server" Text="Date" CssClass="userctrllabel"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label4" runat="server" Text="ItemSnoinLic" CssClass="userctrllabel"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label1" runat="server" Text="SchemeLicNo" CssClass="userctrllabel"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label5" runat="server" Text="SchemeLicDate" CssClass="userctrllabel"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label6" runat="server" Text="SchemeType" CssClass="userctrllabel"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label7" runat="server" Text="CIFValue" CssClass="userctrllabel"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label8" runat="server" Text="Qty" CssClass="userctrllabel"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label9" runat="server" Text="Unit" CssClass="userctrllabel"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label10" runat="server" Text="ValueDebited" CssClass="userctrllabel"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label11" runat="server" Text="RegPort" CssClass="userctrllabel"></asp:Label>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <asp:TextBox ID="txtEDIRegNo" runat="server" CssClass="userctrltextbox"></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="txtDate" runat="server" CssClass="userctrltextbox"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate">
            </cc1:CalendarExtender>
        </td>
        <td>
            <asp:TextBox ID="txtItemSnoinLic" runat="server" CssClass="userctrltextbox"></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="txtSchemeLicNo" runat="server" CssClass="userctrltextbox"></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="txtSchemeLicDate" runat="server" CssClass="userctrltextbox"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtSchemeLicDate">
            </cc1:CalendarExtender>
        </td>
        <td>
            <asp:TextBox ID="txtSchemeType" runat="server" CssClass="userctrltextbox"></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="txtCifValue" runat="server" CssClass="userctrltextbox"></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="txtQty" runat="server" CssClass="userctrltextbox"></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="txtUnit" runat="server" CssClass="userctrltextbox"></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="txtValueDebited" runat="server" CssClass="userctrltextbox"></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="txtRegPort" runat="server" CssClass="userctrltextbox"></asp:TextBox>
        </td>
        <td>
            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" CssClass="orange"
                Width="55px" />
        </td>
    </tr>
    <tr>
        <td colspan="12">
            <div class="d">
                <asp:GridView ID="gvScheme" runat="server" AutoGenerateColumns="False" CssClass="table-wrapper">
                    <RowStyle CssClass="table-header light" />
                    <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" ForeColor="#EE2521" />
                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                    <AlternatingRowStyle BackColor="#E7E7FF" />
                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                    <Columns>
                        <asp:BoundField HeaderText="EDIRegNo" DataField="EDIRegNo" />
                        <asp:BoundField HeaderText="Date" DataField="Date" />
                        <asp:BoundField HeaderText="ItemSnoinLic" DataField="ItemSnoinLic" />
                        <asp:BoundField HeaderText="SchemeLicNo" DataField="SchemeLicNo" />
                        <asp:BoundField HeaderText="SchemeLicDate" DataField="SchemeLicDate" />
                        <asp:BoundField HeaderText="SchemeType" DataField="SchemeType" />
                        <asp:BoundField HeaderText="CIFValue" DataField="CIFValue" />
                        <asp:BoundField HeaderText="Qty" DataField="Qty" />
                        <asp:BoundField HeaderText="Unit" DataField="Unit" />
                        <asp:BoundField HeaderText="ValueDebited" DataField="ValueDebited" />
                        <asp:BoundField HeaderText="RegPort" DataField="RegPort" />
                    </Columns>
                </asp:GridView>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="12">
            &nbsp;
        </td>
    </tr>
</table>

<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmShippingLinerMaster.aspx.cs" Inherits="ImpexCube.frmShippingLinerMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="col-2ex">
    <table>
        <tr>
            <td class="center" colspan="4">
                <asp:Label ID="Label1" runat="server" CssClass="header" Text="Shipping Line Master"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="c-s2-b1">
                <asp:Label ID="lblCfsName" runat="server" Text="Shipper Name" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtShipperName" runat="server" CssClass="postmsgg23"></asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="c-s2-b1">
                <asp:Label ID="lblAddress" runat="server" Text="Address" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtAddress" runat="server" CssClass="postmsgg23"></asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="c-s2-b1">
                <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtContactPerson" runat="server" CssClass="postmsgg23"></asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="c-s2-b1">
                <asp:Label ID="lblEmail" runat="server" Text="Email" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="postmsgg23"></asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="c-s2-b1">
                <asp:Label ID="lblInFavour" runat="server" Text="In Favor" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtInfavor" runat="server" CssClass="postmsgg23"></asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Button ID="btnSave" CssClass="blue"  runat="server" Text="Save" OnClick="btnSave_Click" />
            </td>
            <td>
                <asp:Button ID="btnUpdate" CssClass="blue"  runat="server" Text="Update" OnClick="btnUpdate_Click" />
                <asp:Button ID="btnCancel" CssClass="blue"  runat="server" Text="Cancel" 
                    onclick="btnCancel_Click" />
            </td>
        </tr>
    </table>
    <div class="table-wrapper">
        <asp:GridView ID="gvShipperMaster" runat="server" 
            onselectedindexchanged="gvShipperMaster_SelectedIndexChanged">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
              <RowStyle CssClass="table-header light" />
                            <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" />
                            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                            <AlternatingRowStyle BackColor="#E7E7FF" />
        </asp:GridView>
    </div>
    </div>
</asp:Content>

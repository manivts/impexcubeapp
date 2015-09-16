<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="efrmExchangeRate.aspx.cs" Inherits="ImpexCube.efrmExchangeRate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td colspan="6" align="center" style="color: #008080; font-style: italic; font-weight: bold;
                font-size: large">
                <asp:Label ID="lblExchangeRate" runat="server" Text="Exchange Rate"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCurrencyName" runat="server" Text="Currency Name" 
                    CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCode" runat="server" Text="Code" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblExchRate" runat="server" Text="Exchange Rate" 
                    CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblBankName" runat="server" Text="Bank Name" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCertificateNo" runat="server" Text="Certificate No" 
                    CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="fontsize"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtCurrencyName" runat="server" CssClass="textbox150"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtCode" runat="server" CssClass="textbox150"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtExchRate" runat="server" CssClass="textbox150"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtBankName" runat="server" CssClass="textbox150"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtCertificateNo" runat="server" CssClass="textbox150"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtDate" runat="server" CssClass="textbox150"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td colspan="6" >
         </td>
        </tr>
        <tr>
        <td colspan="6" align="right">
            <asp:Button ID="btnUpdate" runat="server" Text="Update" 
                CssClass="masterbutton" />
            <asp:Button ID="btnUpdateToday" runat="server" Text="Update with Todays" 
                CssClass="orange" />
        </td>
        </tr>
    </table>
    <table>
    <tr>
    <td>
    <div class="grid_scroll-2">
        <asp:GridView ID="gvExchange" runat="server" CssClass="table-wrapper" AutoGenerateColumns="False" Width="600">
        <Columns>
        <asp:BoundField HeaderText="Currency Name" DataField="Currency Name" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
        <asp:BoundField HeaderText="Code" DataField="Code" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
        <asp:BoundField HeaderText="Exchange Rate" DataField="Exchange Rate" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
        <asp:BoundField HeaderText="Bank Name" DataField="Bank Name" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
        <asp:BoundField HeaderText="Certificate No" DataField="Documentation" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
        <asp:BoundField HeaderText="Date" DataField="Date" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
        </Columns>
         <RowStyle CssClass="table-header light" />
                <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" />
                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                <AlternatingRowStyle BackColor="#E7E7FF" />
        </asp:GridView>
    </div>
    </td>
    </tr>
    </table>
</asp:Content>

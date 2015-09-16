<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmUserAuthorization.aspx.cs" Inherits="ImpexCube.frmUserAuthorization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="plUser" runat="server" Width="650px" HorizontalAlign="Left">
        <table class="style1">
            <tr>
                <td class="style2" colspan="4">
                    <asp:Label ID="Label4" runat="server" Text="User Type" Width="103px" Font-Names="Arial"
                        Font-Size="9pt"></asp:Label>
                    <asp:DropDownList ID="ddlUserType" runat="server" Font-Size="9pt" Width="220px" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="style6">
                    <asp:Label ID="Label5" runat="server" Text="List of Form details :-" Font-Names="Arial"
                        Font-Size="9pt" Font-Underline="True" ForeColor="CornflowerBlue"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="vertical-align: top;" class="style7">
                    <asp:GridView ID="GrdForms" runat="server" BorderColor="#C0C0FF" BorderStyle="Dotted"
                        BorderWidth="0px" CellPadding="3" Font-Names="Arial" Font-Size="8pt" AutoGenerateColumns="False"
                        Width="626px" BackColor="#C0C0FF" CellSpacing="2">
                        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                        <RowStyle BackColor="#FFF7E7" ForeColor="#404040" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#C0C0FF" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:BoundField DataField="FormShortName" HeaderText="Form Name" SortExpression="FormShortName">
                                <ItemStyle Wrap="False" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Read">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkRead" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Write">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkWrite" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Approval">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkApproval" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <asp:Button ID="BtnSubmit" runat="server" Height="32px" Text="Confirm Authentication"
                        Width="140px" BackColor="#FF8080" BorderColor="#FF8080" BorderStyle="Solid" BorderWidth="1px"
                        Font-Names="Arial" Font-Size="9pt" ForeColor="DarkSlateGray" OnClientClick="return confirm ('Are your sure want to give the above permission to this user ?');"
                        OnClick="BtnSubmit_Click" />
                    <asp:Button ID="BtnSUB_Exit" runat="server" Height="32px" Text="Close" Width="99px"
                        BackColor="#FF8080" BorderColor="#FF8080" BorderStyle="Solid" BorderWidth="1px"
                        Font-Names="Arial" Font-Size="9pt" ForeColor="DarkSlateGray" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

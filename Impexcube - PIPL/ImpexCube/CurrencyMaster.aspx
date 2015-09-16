<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="CurrencyMaster.aspx.cs" Inherits="ImpexCube.CurrencyMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function valsave() {
            if (document.getElementById('ContentPlaceHolder1_txtCurrencymaster').value == "") {
                alert('Please Enter The Currency');
                return false;
            }
            if (document.getElementById('ContentPlaceHolder1_txtShortname').value == "") {
                alert('Please Enter The ShortName');
                return false;
            }
            if (document.getElementById('ContentPlaceHolder1_txtEffectiveFrom').value == "") {
                alert('Please Enter The Effective From');
                return false;
            }
        }
        function exit() {
            var status = confirm("Do You Want To Exit!");
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <style type="text/css">
                .table-wrapper
                {
                    width: 300px;
                    color: Black;
                    background: #E0E0E0;
                    height: 14px;
                    font-size: 8pt;
                    text-align: left;
                }
                
                a
                {
                    color: #324143;
                }
                
                .postmsgg23
                {
                    color: #000;
                    width: 200px;
                    border: 1px solid #c8c6c6;
                    height: 20px;
                }
            </style>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <table class="style5" style="text-align: left">
                <tr>
                    <td colspan="2" align="center" style="color: #008080; font-style: italic; font-weight: bold;
                        font-size: large">
                        Currency Master
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td colspan="1" class="fontsize">
                                    Currency
                                </td>
                                <td class="style2">
                                    <asp:TextBox ID="txtCurrencymaster" runat="server" CssClass="textbox150"></asp:TextBox><font
                                        color="red"><strong>*</strong></font>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="1" class="fontsize">
                                    Short Name(For Printing)
                                </td>
                                <td>
                                    <asp:TextBox ID="txtShortname" runat="server" CssClass="textbox150"></asp:TextBox><font
                                        color="red"><strong>*</strong></font>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="1" class="fontsize">
                                    Currency Code
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCurrency" runat="server" CssClass="textbox150"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="1" class="fontsize">
                                    Currency Code(For EDI)
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCurencycode" runat="server" CssClass="textbox150"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="1" class="fontsize">
                                    Currency Code(For B\E)
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCurrencyBe" runat="server" CssClass="textbox150"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="1" class="fontsize">
                                    Standard Currency
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlstandardcurrency" runat="server" CssClass="ddl156">
                                        <asp:ListItem>~Select~</asp:ListItem>
                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                        <asp:ListItem Value="No">No</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize">
                                    Unit
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCurrencyUnit" runat="server" CssClass="textbox150"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize">
                                    Conversion Factor
                                </td>
                                <td>
                                    <asp:TextBox ID="txtConversion" runat="server" CssClass="textbox150"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize">
                                    Exchange Rate(Import)
                                </td>
                                <td>
                                    <asp:TextBox ID="txtexchangeimp" runat="server" CssClass="textbox150"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize">
                                    Export Rate(Export)
                                </td>
                                <td>
                                    <asp:TextBox ID="txtExchangeExp" runat="server" CssClass="textbox150"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize">
                                    Effective From
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEffectiveFrom" runat="server" CssClass="textbox150"></asp:TextBox><font
                                        color="red"><strong>*</strong></font>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtEffectiveFrom">
                                    </cc1:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Button ID="btnNew" runat="server" Text="New" Width="100px" OnClick="btnNew_Click"
                                        CssClass="masterbutton" />
                                </td>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" OnClick="btnSave_Click"
                                        CssClass="masterbutton" OnClientClick="javascript:return valsave();" />
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="100px" OnClick="btnUpdate_Click"
                                        CssClass="masterbutton" />
                                    <asp:Button ID="btnExit" runat="server" Text="Exit" Width="100px" OnClick="btnExit_Click"
                                        CssClass="masterbutton" OnClientClick="javascript:return exit();" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top">
                        <asp:TextBox ID="txtSearch" runat="server" Width="300px"></asp:TextBox>
                        <asp:DropDownList
                            ID="ddlstandCurySearch" runat="server" CssClass="ddl100">
                            <asp:ListItem>~Select~</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        <div class="grid_scroll-2">
                            <asp:GridView ID="gvCurrency" runat="server" CssClass="table-wrapper" AutoGenerateSelectButton="true"
                                OnSelectedIndexChanged="gvCurrency_SelectedIndexChanged" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField HeaderText="CurrencyId" DataField="CurrencyId" HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-CssClass="hiddenid" ItemStyle-CssClass="hiddenid"></asp:BoundField>
                                    <asp:BoundField HeaderText="CurrencyName" DataField="CurrencyName" HeaderStyle-HorizontalAlign="Center">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="CurrencyShortName" DataField="CurrencyShortName" HeaderStyle-HorizontalAlign="Center">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="LastChange" DataField="LastChange" HeaderStyle-HorizontalAlign="Center">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="IMP CurrencyRate" DataField="IMPCurrencyRate" HeaderStyle-HorizontalAlign="Center">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="EXP CurrencyRate" DataField="EXPCurrencyRate" HeaderStyle-HorizontalAlign="Center">
                                    </asp:BoundField>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

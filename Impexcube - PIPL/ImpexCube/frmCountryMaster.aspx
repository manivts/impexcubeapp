<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmCountryMaster.aspx.cs" Inherits="ImpexCube.frmCountryMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://localhost:2875/Content/Scripts/ProductDetails.js" type="text/javascript"></script>
    <script type="text/javascript">
        function valsave() {
            if (document.getElementById('ContentPlaceHolder1_txtCountry').value == "") {
                alert('Please Type Country');
                document.getElementById('ContentPlaceHolder1_txtCountry').focus();
                return false;
            }
            if (document.getElementById('ContentPlaceHolder1_txtCountryCode').value == "") {
                alert('Please Type Country Code');
                document.getElementById('ContentPlaceHolder1_txtCountryCode').focus();
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <table class="style5">
                    <tr>
                        <td colspan="4" align="center" style="color: #008080; font-style: italic; font-weight: bold;
                            font-size: large">
                            Country Master
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize" style="width: 100px;">
                            Country
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtCountry" runat="server" CssClass="textbox150"></asp:TextBox>
                            <font color="red">*</font>
                        </td>
                        <td class="fontsize">
                            Language
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtLanguage" runat="server" CssClass="textbox150"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize" style="width: 100px;">
                            Country Code
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtCountryCode" runat="server" CssClass="textbox150"></asp:TextBox><font
                                color="red">*</font>
                        </td>
                        <td class="fontsize">
                            Currency
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtCurrency" runat="server" CssClass="textbox150"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize" style="width: 100px;">
                            Short Code
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtShortCode" runat="server" CssClass="textbox150" OnKeyPress="javascript:return isFloat(event);"></asp:TextBox>
                        </td>
                        <td class="fontsize">
                            Currency Code</td>
                        <td class="style2">
                            <asp:TextBox ID="txtCurrencyCode" runat="server" CssClass="textbox150"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize" style="width: 100px;">
                            Capital
                        </td>
                        <td class="style3">
                            <asp:TextBox ID="txtCapital" runat="server" CssClass="textbox150"></asp:TextBox>
                        </td>
                        <td class="style3">
                            &nbsp;</td>
                        <td class="style3">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="fontsize" style="width: 100px;">
                            &nbsp;</td>
                        <td class="style7">
                            &nbsp;</td>
                        <td class="style7">
                            &nbsp;</td>
                        <td class="style7">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CssClass="masterbutton" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="masterbutton"
                                OnClientClick="javascript:return valsave();" />
                            <asp:Button ID="BtnUpdate" runat="server" Text="Update" OnClick="BtnUpdate_Click"
                                CssClass="masterbutton" />
                            <asp:Button ID="btnDiscard" runat="server" Text="Exit" OnClick="btnDiscard_Click"
                                CssClass="masterbutton" OnClientClick="javascript:return exit();" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                        <asp:TextBox ID="txtSearch" runat="server" Width="300px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="Search" 
                    onclick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div class="grid_scroll-2">
                                <asp:GridView ID="gvCountry" runat="server" CssClass="table-wrapper" AutoGenerateSelectButton="True"
                                    OnSelectedIndexChanged="gvCountry_SelectedIndexChanged1" AutoGenerateColumns="False"
                                    OnPageIndexChanging="gvCountry_PageIndexChanging" Width="600px" Height="100px">
                                    <Columns>
                                        <asp:BoundField DataField="CountryId" HeaderText="CountryId" InsertVisible="False"
                                            ReadOnly="True" SortExpression="CountryId" />
                                        <asp:BoundField DataField="CountryName" HeaderText="CountryName" InsertVisible="False"
                                            ReadOnly="True" SortExpression="CountryName" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="CountryCode" HeaderText="CountryCode" HeaderStyle-HorizontalAlign="Center"
                                            SortExpression="CountryCode" />
                                        <asp:BoundField DataField="Currency" HeaderText="Currency" HeaderStyle-HorizontalAlign="Center"
                                            SortExpression="Currency" />
                                        <asp:BoundField DataField="CurrencyCode" HeaderText="CurrencyCode" HeaderStyle-HorizontalAlign="Center"
                                            SortExpression="CurrencyCode" />
                                    </Columns>
                                    <RowStyle CssClass="table-header light" />
                                    <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" />
                                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                    <AlternatingRowStyle BackColor="#E7E7FF" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </table>
            <table>
                <tr>
                    <td class="style2" colspan="5">
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

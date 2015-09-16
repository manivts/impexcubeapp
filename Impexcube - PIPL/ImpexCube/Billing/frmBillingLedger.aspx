<%@ Page Language="C#" MasterPageFile="~/Billing/MasterPage.master" AutoEventWireup="true"
    Inherits="frmBillingLedger" Title="::PIPLBilling || Billing Summary Report Status "
    CodeBehind="frmBillingLedger.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td style="width: 45%">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table style="width: 50%;">
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td style="width: 80px; height: 30px;">
                                                <asp:Label ID="Label6" runat="server" Font-Names="arial" Font-Size="8pt" Text="Shipment Type"></asp:Label>
                                            </td>
                                            <td align="left" style="height: 30px">
                                                <asp:RadioButtonList ID="rbSHp" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                    RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbSHp_SelectedIndexChanged">
                                                    <asp:ListItem Value="IMP">Import</asp:ListItem>
                                                    <asp:ListItem Value="EXP">Export</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td style="width: 50px; height: 30px;">
                                                <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="8pt" Text="Bill Type"></asp:Label>
                                            </td>
                                            <td align="left" style="height: 30px">
                                                <asp:RadioButtonList ID="rbBill" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                    RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbBill_SelectedIndexChanged"
                                                    Width="150px">
                                                    <asp:ListItem Value="SB">Invoice</asp:ListItem>
                                                    <asp:ListItem Value="DB">Debit Note</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr valign="middle">
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text="From" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFrom" runat="server" Font-Names="Arial" Font-Size="8pt" Width="60px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text="To" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTo" runat="server" Font-Names="Arial" Font-Size="8pt" Width="60px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkIMP" runat="server" AutoPostBack="true" Checked="false" Font-Names="Arial"
                                                                Font-Size="8pt" OnCheckedChanged="chkIMP_CheckedChanged" Text="Importer" Width="60px" />
                                                        </td>
                                                        <td style="font-family: Arial; font-size: 8pt;">
                                                            <asp:TextBox ID="txtPName" runat="server" AutoPostBack="true" Font-Names="Arial"
                                                                Font-Size="8pt" Width="160px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkLedger" runat="server" AutoPostBack="true" Checked="false" Font-Names="Arial"
                                                                Font-Size="8pt" OnCheckedChanged="chkLedger_CheckedChanged" Text="Ledger Name"
                                                                Width="90px" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtLedger" runat="server" Font-Names="Arial" Font-Size="8pt" Enabled="false"
                                                                Width="143px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Service Tax
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlServiceTax" runat="server">
                                                                <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                                <asp:ListItem Value="12.00">12</asp:ListItem>
                                                                <asp:ListItem Value="3.00">3</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFrom">
                                                </cc1:CalendarExtender>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtTo">
                                                </cc1:CalendarExtender>
                                                <cc1:AutoCompleteExtender ID="ACE1" runat="server" EnableCaching="true" MinimumPrefixLength="0"
                                                    ServiceMethod="GetCompany" ServicePath="~/AutoComplete.asmx" TargetControlID="txtPName">
                                                </cc1:AutoCompleteExtender>
                                                <cc1:AutoCompleteExtender ID="autoComplete1" runat="server" EnableCaching="true"
                                                    MinimumPrefixLength="0" ServiceMethod="GetLedger" CompletionListCssClass="completionList"
                                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                    ServicePath="~/AutoComplete.asmx" TargetControlID="txtLedger">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td align="left" style="width: 30%;">
                <table style="width: 60%">
                    <tr style="width: 30%">
                        <td style="width: 30%;">
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Button ID="Btn_search" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Search"
                                Width="70px" OnClick="Btn_search_Click" CssClass="button_image1" Height="25px" />
                        </td>
                        <td align="left">
                            <asp:Button ID="btnExport" runat="server" Text="Export to Excel" Font-Names="Arial"
                                Font-Size="8pt" OnClick="btnExport_Click" Width="90px" CssClass="button_image1"
                                Height="25px" />
                        </td>
                        <td align="left">
                            <asp:Button ID="ExportPDF" runat="server" Text="Export to PDF" Font-Names="Arial"
                                Font-Size="8pt" Width="90px" Height="25px" CssClass="button_image1" OnClick="ExportPDF_Click" />
                        </td>
                        <td style="width: 183px;">
                            <br />
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="tr1" style="background-color: Red;" runat="server">
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table>
                    <tr>
                        <td colspan="6" align="left">
                            <div id="DivTag" runat="server" class="grid_scroll">
                                <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="false" BorderColor="#DEDFDE"
                                    BorderStyle="None" BorderWidth="1px" Font-Size="8pt" CellPadding="4" BackColor="White"
                                    ForeColor="Black" GridLines="Vertical" ShowFooter="True" >
                                    <FooterStyle BackColor="Silver" BorderStyle="None" Font-Bold="True" Font-Names="Arial"
                                        Font-Size="8pt" HorizontalAlign="Right" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.NO">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="InvDate" HeaderText="BILL DATE" SortExpression="Desc"
                                            DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="jobno" HeaderText="JOB NO" SortExpression="Type">
                                            <ItemStyle Wrap="true" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="invoice" HeaderText="INV.NO" SortExpression="Desc" />
                                        <asp:BoundField DataField="compname" HeaderText="IMPORTER" SortExpression="Desc" />
                                        <%--<asp:BoundField DataField="suffix" HeaderText="DESC" SortExpression="Desc" />--%>
                                        <asp:BoundField DataField="SERVICETAXAMOUNT" HeaderText="SERVICE TAX" SortExpression="Desc" />
                                        <asp:BoundField DataField="Edu_cess" HeaderText="EDU CESS" SortExpression="Desc" />
                                        <asp:BoundField DataField="SEC_Chess" HeaderText="SHC CESS" SortExpression="Desc" />
                                        <asp:BoundField DataField="Total" HeaderText="Tax TOTAL" SortExpression="Desc" />
                                     <asp:BoundField DataField="Amount" HeaderText="Charge Total" SortExpression="Desc" />
                                    </Columns>
                                    <RowStyle HorizontalAlign="Left" Font-Names="Arial" Font-Size="8pt" BackColor="#F7F7DE"
                                        Font-Overline="False" />
                                    <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#3399FF" Font-Names="Arial"
                                        Font-Size="7pt" />
                                    <RowStyle BackColor="Snow" BorderColor="Gainsboro" BorderStyle="Solid" BorderWidth="1px" />
                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                            </div>

                             <div id="Div1" runat="server" class="grid_scroll">
                                <asp:GridView ID="gvService" runat="server" AutoGenerateColumns="false" BorderColor="#DEDFDE"
                                    BorderStyle="None" BorderWidth="1px" Font-Size="8pt" CellPadding="4" BackColor="White"
                                    ForeColor="Black" GridLines="Vertical" ShowFooter="True" >
                                    <FooterStyle BackColor="Silver" BorderStyle="None" Font-Bold="True" Font-Names="Arial"
                                        Font-Size="8pt" HorizontalAlign="Right" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.NO">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="InvDate" HeaderText="BILL DATE" SortExpression="Desc"
                                            DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="jobno" HeaderText="JOB NO" SortExpression="Type">
                                            <ItemStyle Wrap="true" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="invoice" HeaderText="INV.NO" SortExpression="Desc" />
                                        <asp:BoundField DataField="compname" HeaderText="IMPORTER" SortExpression="Desc" />
                                        <%--<asp:BoundField DataField="suffix" HeaderText="DESC" SortExpression="Desc" />--%>
                                        <asp:BoundField DataField="SERVICETAXAMOUNT" HeaderText="SERVICE TAX" SortExpression="Desc" />
                                        <asp:BoundField DataField="Edu_cess" HeaderText="EDU CESS" SortExpression="Desc" />
                                        <asp:BoundField DataField="SEC_Chess" HeaderText="SHC CESS" SortExpression="Desc" />
                                        <asp:BoundField DataField="Total" HeaderText="Tax TOTAL" SortExpression="Desc" />
                                   <%--  <asp:BoundField DataField="Amount" HeaderText="Charge Total" SortExpression="Desc" />--%>
                                    </Columns>
                                    <RowStyle HorizontalAlign="Left" Font-Names="Arial" Font-Size="8pt" BackColor="#F7F7DE"
                                        Font-Overline="False" />
                                    <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#3399FF" Font-Names="Arial"
                                        Font-Size="7pt" />
                                    <RowStyle BackColor="Snow" BorderColor="Gainsboro" BorderStyle="Solid" BorderWidth="1px" />
                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                            </div>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

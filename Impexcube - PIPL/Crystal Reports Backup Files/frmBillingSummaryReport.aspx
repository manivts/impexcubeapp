﻿<%@ Page Language="C#" MasterPageFile="~/Billing/MasterPage.master" AutoEventWireup="true" Inherits="frmBillingSummaryReport" Title="::PIPLBilling || Billing Summary Report Status " Codebehind="frmBillingSummaryReport.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <table>
    <tr>
    <td align="center" >
        <asp:Label ID="lblShortName" runat="server" Text="" Font-Bold="True" Font-Names="Verdana" Font-Size="10pt" ForeColor="#719BDF"></asp:Label>
     
        <asp:Label ID="Label3" runat="server" Text=" - Billing Summary Report " 
            Font-Bold="True" Font-Names="Verdana" Font-Size="10pt" ForeColor="#719BDF" ></asp:Label>
    </td>
    </tr>
        <tr>
            <td style="width: 725px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
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
                                        Font-Size="8pt" Width="160px" ontextchanged="txtPName_TextChanged"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label23" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        Text="Summary"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="drSummary" runat="server" Font-Names="Arial" 
                                        Font-Size="8pt" Width="100px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFrom">
                        </cc1:CalendarExtender>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtTo">
                        </cc1:CalendarExtender>
                        <cc1:AutoCompleteExtender ID="ACE1" runat="server" EnableCaching="true" MinimumPrefixLength="0"
                            ServiceMethod="GetCompanyS" ServicePath="~/AutoComplete.asmx" TargetControlID="txtPName">
                        </cc1:AutoCompleteExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td align="left" style="vertical-align: top;">
                <asp:Button ID="Btn_search" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Search"
                    Width="70px" OnClick="Btn_search_Click" Height="25px" CssClass="button_image1" />
            </td>
            <td align="left">
                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" Font-Names="Arial"
                    Font-Size="8pt" OnClick="btnExport_Click" Width="90px" Height="25px" CssClass="button_image1" />
            </td>
            <td style="width: 200px;" align="left">
                &nbsp;
            </td>
        </tr>
        <tr id="trRow" runat="server" >
        <td colspan="6" style=" background-color: Red; border: solid 1px;"></td>
        </tr>
        <tr>
            <td colspan="6" align="left">
                <div id="DivTag" runat="server" class="grid_scroll">
                    <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" BorderColor="#DEDFDE"
                        BorderStyle="None" BorderWidth="1px" Font-Size="8pt" CellPadding="4" 
                        BackColor="White" OnRowDataBound="gvReport_RowDataBound"
                        ForeColor="Black" GridLines="Vertical" ShowFooter="True">
                        <FooterStyle BackColor="Silver" BorderStyle="None" Font-Bold="True" Font-Names="Arial"
                            Font-Size="8pt" HorizontalAlign="Right" />
                        <Columns>
                            <asp:TemplateField HeaderText="S.NO">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="invoiceDate" HeaderText="BILL DATE" SortExpression="Desc" />
                            <asp:BoundField DataField="jobno" HeaderText="JOB NO" SortExpression="Type">
                                <ItemStyle Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="INV" HeaderText="INV.NO" SortExpression="Desc" />
                            <asp:TemplateField HeaderText="AGENCY">
                                <ItemTemplate>
                                    <asp:Label ID="lblAgency" Font-Names="arial" Font-Size="8pt" runat="server" CssClass="mAlign" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="S.TAX">
                                <ItemTemplate>
                                    <asp:Label ID="lblStax" Font-Names="arial" Font-Size="8pt" runat="server" CssClass="mAlign" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TOTAL">
                                <ItemTemplate>
                                    <asp:Label ID="lbliTotal" Font-Names="arial" Font-Size="8pt" CssClass="mAlign" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DN" HeaderText="DN.NO" SortExpression="Desc" />
                             <asp:TemplateField HeaderText="CFS/AAI">
                                <ItemTemplate>
                                    <asp:Label ID="lblCFS" Font-Names="arial" Font-Size="8pt" CssClass="mAlign" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="INT/DIFF ON DUTY">
                                <ItemTemplate>
                                    <asp:Label ID="lblINT" Font-Names="arial" Font-Size="8pt"  runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DO">
                                <ItemTemplate>
                                    <asp:Label ID="lblDO" Font-Names="arial" Font-Size="8pt" CssClass="mAlign" runat="server"  ></asp:Label>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:BoundField DataField="less_advance"  HeaderText="ADVANCE RECEVIED" >
                              <ItemStyle CssClass="mAlign" />
                              </asp:BoundField>
                            <asp:TemplateField HeaderText="TOTAL">
                                <ItemTemplate>
                                    <asp:Label ID="lbldTtotal" Font-Names="arial" Font-Size="8pt" CssClass="mAlign" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TOTAL">
                                <ItemTemplate>
                                    <asp:Label ID="lblgTotal" Font-Names="arial" Font-Size="8pt" CssClass="mAlign" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle HorizontalAlign="Left" Font-Names="Arial" Font-Size="8pt" 
                            BackColor="#F7F7DE" Font-Overline="False" />
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
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmInvoiceReport.aspx.cs" Inherits="ImpexCube.frmInvoiceReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table style="width: 100%">
            <tr>
                <td align="left" style="height: 22px; width: 400px;" colspan="2">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </td>
                <td style="width: 900px;">
                    &nbsp;</td>
                <td style="width: 900px;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" style="height: 22px; width: 200px;">
                    <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="7pt" Text="From"
                        Width="40px" Font-Strikeout="False" ForeColor="Black"></asp:Label>
                    <asp:TextBox ID="txtFdate" runat="server" Font-Names="Arial" Font-Size="7pt" Width="76px"
                        Font-Strikeout="False"></asp:TextBox>
                          <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFdate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
                </td>
                <td align="left" style="height: 22px; width: 200px;">
                    <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="8pt" Text="To"
                        Width="14px"></asp:Label>
                    <asp:TextBox ID="txtTdate" runat="server" Font-Names="Arial" Font-Size="7pt" Width="70px"
                        Font-Strikeout="False"></asp:TextBox>
                          <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTdate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
                </td>
                <td style="width: 1000px;">
                   <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>--%>
                            <asp:Label ID="Label7" runat="server" Font-Names="arial" 
                        Font-Size="7pt" Font-Strikeout="False"
                                Text="Importer  " Width="50px" ForeColor="Black" Height="16px"></asp:Label>
                            <asp:TextBox ID="txtPName" runat="server" Font-Names="Arial" Font-Size="7pt" Width="110px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="autoComplete2" runat="server" TargetControlID="txtPName"
                                ServicePath="~/AutoComplete.asmx" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                CompletionListCssClass="completionList" ServiceMethod="GetJobCustomer" MinimumPrefixLength="1">
                            </cc1:AutoCompleteExtender>
                            <asp:Label ID="Label1" runat="server" Text="Mode" Font-Size="7pt"
                                Font-Names="arial" Width="40px" Font-Strikeout="False" 
                        ForeColor="Black" Height="16px"></asp:Label>
                            <asp:DropDownList ID="ddlMode" runat="server" Font-Names="Arial" Font-Size="7pt"
                                Width="70px" AppendDataBoundItems="True">
                                <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                <asp:ListItem Value="A">Air</asp:ListItem>
                                <asp:ListItem Value="S">Sea</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="Label9" runat="server" Text="Select Report Name :" Font-Size="7pt"
                                Font-Names="arial" Width="100px" Font-Strikeout="False" ForeColor="Black" Height="16px"></asp:Label>
                            <asp:DropDownList ID="ddlReportName" runat="server" Font-Names="Arial" Font-Size="7pt"
                                Width="136px" AppendDataBoundItems="True">
                                <asp:ListItem Selected="True">~Select~</asp:ListItem>
                            </asp:DropDownList>
                      <%--  </ContentTemplate>
                    </asp:UpdatePanel>--%>
                </td>
                <td style="width: 900px;">
                    <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Search"
                        CssClass="button_image1" Width="70px"></asp:Button>
                    <asp:Button ID="ExportPage" runat="server" CssClass="button_image1" OnClick="ExportPage_Click"
                        Text="Export Excel" Width="85px" />
                    <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="button_image1"
                        Width="60px" OnClick="btnExit_Click" />
                </td>
            </tr>
            <tr id="tr1" runat="server">
                <td colspan="4">
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    <asp:GridView ID="gvInvoice" runat="server" CellPadding="4" ForeColor="#333333" 
                        GridLines="None" Font-Size="10pt">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" 
                            Font-Size="10pt" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

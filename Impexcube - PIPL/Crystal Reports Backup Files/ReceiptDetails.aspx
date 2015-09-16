﻿<%@ Page Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="ReceiptDetails.aspx.cs" Inherits="ImpexCube.Accounts.ReceiptDetails"
    Title="Receipt Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .waterText
        {
            font-family: Arial;
            font-size: 8pt;
            color: Fuchsia;
            overflow: auto;
            background-color: #FFFFFF;
        }
        .style8
        {
            font-family: Verdana;
            font-size: 11pt;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image ID="Image1" runat="server" 
                ImageUrl="~/Content/Images/progress.gif" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table align="center">
                <tr>
                    <td colspan="3" style="text-align: center">
                        <asp:Label ID="Label1" runat="server" Text="Receipt Details"
                  CssClass="labeltitle"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">
                        <asp:LinkButton ID="lnkNew" runat="server" Style="text-align: left" OnClick="lnkNew_Click"
                            CssClass="style8">New</asp:LinkButton>
                        &nbsp;&nbsp;
                        <asp:LinkButton ID="lnkExit" runat="server" PostBackUrl="~/MainMenu.aspx" CssClass="style8">Exit</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">
                        <asp:TextBox ID="txtKeyword" runat="server" CssClass="textbox140"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" TargetControlID="txtKeyword"
                            WatermarkCssClass="waterText" WatermarkText="VchNo/AccName" runat="server">
                        </cc1:TextBoxWatermarkExtender>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" TargetControlID="txtKeyword"
                            FilterType="numbers" ValidChars="0123456789" runat="server">
                        </cc1:FilteredTextBoxExtender>
                        &nbsp; &nbsp;
                        <asp:TextBox ID="txtFrom" runat="server" MaxLength="10" CssClass="textbox140"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFrom">
                        </cc1:CalendarExtender>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" TargetControlID="txtFrom"
                            WatermarkCssClass="waterText" WatermarkText="From Date" runat="server">
                        </cc1:TextBoxWatermarkExtender>
                        <cc1:FilteredTextBoxExtender ID="FTEDate" TargetControlID="txtFrom" FilterType="Numbers,Custom"
                            ValidChars="01/01/1999" runat="server">
                        </cc1:FilteredTextBoxExtender>
                        &nbsp;&nbsp;
                        <asp:TextBox ID="txtTo" runat="server" MaxLength="10" CssClass="textbox140"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtTo_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                            TargetControlID="txtTo">
                        </cc1:CalendarExtender>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" TargetControlID="txtTo"
                            WatermarkCssClass="waterText" WatermarkText="To Date" runat="server">
                        </cc1:TextBoxWatermarkExtender>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtTo"
                            FilterType="Numbers,Custom" ValidChars="01/01/1999" runat="server">
                        </cc1:FilteredTextBoxExtender>
                        &nbsp;&nbsp;
                        <asp:Button ID="btnSearch" runat="server" Text="Search" 
                            OnClick="btnSearch_Click" CssClass="masterbutton" />
                        &nbsp;
                        <asp:Label ID="lblResult" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
                        <asp:GridView ID="gvReceiptDetail" runat="server" Style="text-align: left; font-family: Verdana;
                            font-size: 9pt;" AutoGenerateColumns="true" BorderColor="Black" BorderStyle="Solid"
                            Font-Names="Arial" BorderWidth="1px" Font-Size="10pt" ForeColor="Black" ShowFooter="True"
                            ShowHeader="true" OnSelectedIndexChanged="gvReceiptDetail_SelectedIndexChanged"
                            Width="100%" AllowSorting="True" OnPageIndexChanging="gvReceiptDetail_PageIndexChanging"
                            OnSorting="gvReceiptDetail_Sorting" AllowPaging="True">
                            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" BorderColor="Black" BorderStyle="Solid"
                                BorderWidth="1px" />
                            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                            <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="#E7E7FF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" ItemStyle-Font-Bold="false" ItemStyle-Font-Names="Arial"
                                    ShowSelectButton="true" SelectImageUrl="~/Content/Images/view_icon.gif" HeaderText="Select">
                                    <ItemStyle Font-Names="Arial" Font-Bold="False"></ItemStyle>
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="Print">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Content/Images/print-icon.png" ToolTip="Print"
                                            OnClick="btnPrint_Click" />
                                        <%--<asp:Button ID="ButtonPrint" runat="server" OnClick="ButtonPrint_Click" Text="Print" />--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
</asp:Content>
<%@ Page Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="JournalDetails.aspx.cs" Inherits="ImpexCube.Accounts.JournalDetails"
    Title="Journal Details" %>

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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Content/Images/progress.gif" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table align="center" class="labeltitle">
                <tr>
                    <td colspan="3" style="text-align: center">
                        <asp:Label ID="Label1" runat="server"  Text="Journal Details"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">
                        <asp:LinkButton ID="lnkNew" runat="server" OnClick="lnkNew_Click" Style="text-align: left">New</asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="lnkExit" runat="server" OnClick="lnkExit_Click">Exit</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">
                        <asp:TextBox ID="txtKeyword" runat="server" CssClass="textbox140"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtKeyword"
                            WatermarkCssClass="waterText" WatermarkText="Keyword">
                        </cc1:TextBoxWatermarkExtender>
                        &nbsp; &nbsp;
                        <asp:TextBox ID="txtFrom" runat="server" CssClass="textbox140"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFrom">
                        </cc1:CalendarExtender>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtFrom"
                            WatermarkCssClass="waterText" WatermarkText="From Date">
                        </cc1:TextBoxWatermarkExtender>
                        &nbsp;&nbsp;
                        <asp:TextBox ID="txtTo" runat="server" CssClass="textbox140"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtTo_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                            TargetControlID="txtTo">
                        </cc1:CalendarExtender>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtTo"
                            WatermarkCssClass="waterText" WatermarkText="To Date">
                        </cc1:TextBoxWatermarkExtender>
                        &nbsp;&nbsp;
                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" 
                            Text="Search" CssClass="masterbutton" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        <asp:GridView ID="gvJournalDetails" runat="server" AutoGenerateColumns="true" BorderColor="Black"
                            BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="10pt" ForeColor="Black"
                            GridLines="Vertical" OnSelectedIndexChanged="gvJournalDetails_SelectedIndexChanged"
                            ShowFooter="false" ShowHeader="true" Style="text-align: left; font-size: 9pt;"
                            Width="100%" AllowPaging="True">
                            <FooterStyle BackColor="#C6C3C6" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                ForeColor="Black" />
                            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                            <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="#E7E7FF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" SelectImageUrl="~/Content/Images/view_icon.gif" ItemStyle-Font-Bold="false"
                                    ItemStyle-Font-Names="Arial" ShowSelectButton="true" HeaderText="View">
                                    <ItemStyle Font-Bold="False" Font-Names="Arial" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="Print">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Content/Images/print-icon.png" 
                                            ToolTip="Print" onclick="btnPrint_Click" />
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
    <p>
        <br />
    </p>
</asp:Content>

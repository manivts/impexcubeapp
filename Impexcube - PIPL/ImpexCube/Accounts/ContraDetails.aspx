<%@ Page Language="C#" MasterPageFile="~/Accounts/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="ContraDetails.aspx.cs" Inherits="ImpexCube.Accounts.ContraDetails"
    Title="Contra Details" %>

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
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image ID="Image1" runat="server" 
                ImageUrl="~/Accounts/AccImages/progress.gif" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table align="center" width="75%">
                <tr>
                    <td colspan="3" style="text-align: center">
                        <asp:Label ID="Label1" runat="server"  
                            Text="Contra Details" CssClass="labeltitle"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">
                        <asp:LinkButton ID="lnkNew" runat="server" OnClick="lnkNew_Click" 
                            Style="text-align: left">New</asp:LinkButton>
                        &nbsp;&nbsp;
                        <asp:LinkButton ID="lnkExit" runat="server" OnClick="lnkExit_Click" 
                            PostBackUrl="~/Accounts/MainMenu.aspx">Exit</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">
                        <asp:TextBox ID="txtKeyword" runat="server" CssClass="textbox140"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" 
                            TargetControlID="txtKeyword" WatermarkCssClass="waterText" 
                            WatermarkText="Keyword">
                        </cc1:TextBoxWatermarkExtender>
                        &nbsp; &nbsp;
                        <asp:TextBox ID="txtFrom" runat="server" CssClass="textbox140"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                            TargetControlID="txtFrom">
                        </cc1:CalendarExtender>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" 
                            TargetControlID="txtFrom" WatermarkCssClass="waterText" 
                            WatermarkText="From Date">
                        </cc1:TextBoxWatermarkExtender>
                        &nbsp;&nbsp;
                        <asp:TextBox ID="txtTo" runat="server" CssClass="textbox140"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtTo_CalendarExtender" runat="server" 
                            Format="dd/MM/yyyy" TargetControlID="txtTo">
                        </cc1:CalendarExtender>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" 
                            TargetControlID="txtTo" WatermarkCssClass="waterText" WatermarkText="To Date">
                        </cc1:TextBoxWatermarkExtender>
                        &nbsp;&nbsp;
                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" 
                            Text="Search" CssClass="btn70" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        <asp:GridView ID="gvContraDetails" runat="server" AutoGenerateColumns="true" 
                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" 
                            Font-Size="10pt" ForeColor="Black" GridLines="Vertical" 
                            OnSelectedIndexChanged="gvContraDetails_SelectedIndexChanged" 
                            ShowFooter="false" ShowHeader="true" 
                            Style="text-align: left; font-size: 9pt;" Width="100%"  AllowPaging="True">
                            <FooterStyle BackColor="#C6C3C6" BorderColor="Black" BorderStyle="Solid" 
                                BorderWidth="1px" ForeColor="Black"  />
                            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                            <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="#E7E7FF" />
                            <AlternatingRowStyle BackColor="White" />
                    <%--<RowStyle Font-Bold="false" HorizontalAlign="Left" Font-Names="Arial" />
                    <RowStyle BackColor="Snow" BorderColor="Gainsboro" BorderStyle="Solid" BorderWidth="1px" />
                    <EditRowStyle BackColor="#2461BF" />
                    <SelectedRowStyle BackColor="#D1ECF8" Font-Bold="False" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" Font-Names="Arial" />
                    <HeaderStyle BackColor="#2461bf" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />--%>
                            <Columns>
                            <asp:CommandField ButtonType="Image" SelectImageUrl="~/Accounts/AccImages/view_icon.gif" ItemStyle-Font-Bold="false"
                                    ItemStyle-Font-Names="Arial" ShowSelectButton="true" HeaderText="View">
                                    <ItemStyle Font-Bold="False" Font-Names="Arial" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="Print">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Accounts/AccImages/print-icon.png" 
                                            ToolTip="Print" onclick="btnPrint_Click" />
                                        <%--<asp:Button ID="ButtonPrint" runat="server" OnClick="ButtonPrint_Click" Text="Print" />--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:CommandField ButtonType="Link" ItemStyle-Font-Bold="false" 
                                    ItemStyle-Font-Names="Arial" SelectText="Select" ShowSelectButton="true">
                                <ItemStyle Font-Bold="False" Font-Names="Arial" />
                                </asp:CommandField>--%>
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

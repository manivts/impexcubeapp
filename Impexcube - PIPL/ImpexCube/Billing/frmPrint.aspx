<%@ Page Language="C#" MasterPageFile="~/Billing/MasterPage.master" AutoEventWireup="true"
    Inherits="frmPrint" CodeBehind="frmPrint.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up1">
        <ProgressTemplate>
            <span style="font-size: small; color: #000066">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                Please Wait...</span><asp:Image ID="Image123" runat="server" ImageUrl="~/image/progress.gif" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div>
                <table style="width: 100%; height: 400px; border: 1px;">
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="height: 90%; vertical-align: top; background-color: white; width: 975px;
                                        border: solid 1px #2461bf;">
                                        <br />
                                        <br />
                                        <asp:Panel ID="Panel3" runat="server" Height="450px" Width="100%" BackColor="white">
                                            <center>
                                                <table>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Panel ID="Panel1" runat="server" Height="30px" BackColor="#2461bf" Width="500px">
                                                                <center>
                                                                    <asp:Label ID="Label3" runat="server" Text="Print Invoice" Font-Names="Arial" Font-Size="12pt"
                                                                        ForeColor="White"></asp:Label>
                                                                </center>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:Panel ID="Panel2" runat="server" Height="200px" Width="500px">
                                                    <table>
                                                        <tr>
                                                            <td style="width: 52px">
                                                                <asp:Image ID="Image1" runat="server" Height="128px" ImageUrl="~/image/printer_logo.jpg"
                                                                    Width="92px" />
                                                            </td>
                                                            <td>
                                                                <table id="tblHeader" runat="server" style="border-bottom-style: solid; border-bottom-color: #FFE0C0;
                                                                    border: 1px; border-left-color: #FFE0C0; border-left-style: solid; border-right-color: #FFE0C0;
                                                                    border-right-style: solid; border-top-color: #FFE0C0; border-top-style: solid;
                                                                    width: 399px;">
                                                                    <tr>
                                                                        <td align="left">
                                                                            <asp:Label ID="Label4" runat="server" Text="Select Shipment Mode" Font-Names="Arial"
                                                                                Font-Size="8pt"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:RadioButtonList ID="rbSHp" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbSHp_SelectedIndexChanged">
                                                                                <asp:ListItem Value="IMP">Import</asp:ListItem>
                                                                                <asp:ListItem Value="EXP">Export</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left">
                                                                            <asp:Label ID="Label2" runat="server" Text="Select Bill Type" Font-Names="Arial"
                                                                                Font-Size="8pt"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:RadioButtonList ID="rbBill" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbBill_SelectedIndexChanged"
                                                                                Width="150px">
                                                                                <asp:ListItem Value="SB">Invoice</asp:ListItem>
                                                                                <asp:ListItem Value="DB">Debit Note</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" style="height: 35px">
                                                                            <asp:Label ID="Label1" runat="server" Height="31px" Text="Enter Invoice  Number "
                                                                                Width="129px" Font-Names="Arial" Font-Size="8pt" Font-Bold="False"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtInvNo" runat="server" Height="28px" Width="184px" Font-Names="Arial"
                                                                                Font-Size="8pt"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table style="border-bottom-style: solid; border-bottom-color: #FFE0C0; border: 1px;
                                                                    border-left-color: #FFE0C0; border-left-style: solid; border-right-color: #FFE0C0;
                                                                    border-right-style: solid; border-top-color: #FFE0C0; border-top-style: solid;
                                                                    width: 400px; height: 76px;">
                                                                    <tr>
                                                                        <td colspan="2" align="center" style="width: 366px">
                                                                            <br />
                                                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="101px" OnClick="btnSubmit_Click"
                                                                                Height="32px" Font-Names="Arial" Font-Size="9pt" CssClass="button_image1" />
                                                                            <asp:Button ID="Button1" runat="server" Height="31px" PostBackUrl="~/index.aspx"
                                                                                Text="Exit" Width="101px" CausesValidation="False" CssClass="button_image1" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table style="border: 1px; border-top-color: #FFE0C0; border-top-style: solid; width: 400px;
                                                                    height: 5px;">
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </center>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <cc1:AutoCompleteExtender ID="autoComplete1" runat="server" EnableCaching="true"
                MinimumPrefixLength="0" ServiceMethod="GetBill" CompletionListCssClass="completionList"
                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                ServicePath="~/AutoComplete.asmx" TargetControlID="txtInvNo">
            </cc1:AutoCompleteExtender>
            <cc1:RoundedCornersExtender ID="RCE1" TargetControlID="Panel1" Radius="10" Corners="top"
                runat="server">
            </cc1:RoundedCornersExtender>
            <cc1:RoundedCornersExtender ID="RCE2" TargetControlID="Panel2" Radius="10" BorderColor="#2461bf"
                Corners="none" runat="server">
            </cc1:RoundedCornersExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

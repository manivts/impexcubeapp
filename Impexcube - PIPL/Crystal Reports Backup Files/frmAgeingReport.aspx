<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="frmAgeingReport.aspx.cs" Inherits="ImpexCube.frmAgeingReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td style="vertical-align: top; height: 419px;">
                            <table>
                                <tr>
                                    <td align="left" style="vertical-align: top;">
                                        <table>
                                            <tr style="background-color: #f0f5f9;">
                                                <td style="border-bottom: solid 1px Lavender;" align="center">
                                                    <asp:Label ID="Label7" runat="server" Text="PIPL -Ageing Reports" Font-Bold="True"
                                                        Font-Names="verdana" Font-Size="10pt" Height="18px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="background-color: #f0f5f9;">
                                                <td style="vertical-align: top;">
                                                    <table>
                                                        <tr>
                                                            <td align="left" id="tblMst" runat="server" style="vertical-align: top;">
                                                                <table style="height: 22px;">
                                                                    <tr style="background-color: #f0f5f9;">
                                                                        <td align="left" style="height: 14px;">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lbldoc" runat="server" Font-Names="Arial" Font-Size="7pt" Text="From Date :"
                                                                                            Width="54px"></asp:Label>
                                                                                    </td>
                                                                                    <td align="left">
                                                                                        <asp:TextBox ID="txtFdate" runat="server" CausesValidation="True" Font-Names="Arial"
                                                                                            Font-Size="7pt" Width="50px"></asp:TextBox>
                                                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFdate"
                                                                                            Format="dd/MM/yyyy">
                                                                                        </cc1:CalendarExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblto" runat="server" Font-Names="Arial" Font-Size="7pt" Text="TO:"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtTdate" runat="server" Font-Names="Arial" Font-Size="7pt" Width="50px"></asp:TextBox>
                                                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTdate"
                                                                                            Format="dd/MM/yyyy">
                                                                                        </cc1:CalendarExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="7pt" Text="Mode"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="drshipment" Font-Names="Arial" Font-Size="7pt" runat="server"
                                                                                            Width="70px" Font-Overline="False">
                                                                                            <asp:ListItem Value="0">~select~</asp:ListItem>
                                                                                            <asp:ListItem Value="A">AIR</asp:ListItem>
                                                                                            <asp:ListItem Value="S">SEA</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:CheckBox ID="chkIMP" runat="server" Font-Names="Arial" AutoPostBack="true" Font-Size="7pt"
                                                                                                        OnCheckedChanged="chkIMP_CheckedChanged" Text="ImpName" />
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtPName" runat="server" Font-Names="Arial" Font-Size="7pt" Width="110px"></asp:TextBox>
                                                                                                    <cc1:AutoCompleteExtender ID="autoComplete2" runat="server" TargetControlID="txtPName"
                                                                                                        ServicePath="~/AutoComplete.asmx" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                                                                        CompletionListCssClass="completionList" ServiceMethod="GetPartyMaster" MinimumPrefixLength="1">
                                                                                                    </cc1:AutoCompleteExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="7pt" Text="Status"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="drStatus" runat="server" Font-Names="Arial" Font-Size="7pt">
                                                                                            <asp:ListItem Value="0">~select~</asp:ListItem>
                                                                                            <asp:ListItem Value="-1">Pending</asp:ListItem>
                                                                                            <asp:ListItem Value="1">Completed</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Button ID="BtnSubmit" runat="server" CssClass="button_image1" OnClick="BtnSubmit_Click"
                                                                                            Text="Search" Width="55px" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Button ID="Button1" runat="server" CssClass="button_image1" PostBackUrl="~/PIPL/frmMarketmodulePIPL.aspx"
                                                                                            Text="Exit" Width="55px" OnClick="Button1_Click" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <center>
                                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                            DisplayAfter="3000" DynamicLayout="true">
                                                            <ProgressTemplate>
                                                                <span style="font-family: Verdana; font-size: 10px; color: #2461BF;">Loading ...
                                                                    <img id="ig1" src="../../images/blprog.gif" width="50px" height="50px" /></span>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                        <asp:Label ID="lblResult" runat="server" Font-Names="Calibri" Font-Size="10pt" ForeColor="Red"></asp:Label>
                                                    </center>
                                                    <div id="GridScroll" class="grid_scroll12" style="width: 900px;">
                                                        <asp:GridView ID="Grdiworkreg" runat="server" BackColor="White" BorderColor="#3366CC"
                                                            BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Arial" Font-Size="8pt"
                                                            OnRowDataBound="Grdiworkreg_RowDataBound" AutoGenerateColumns="False">
                                                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                                            <RowStyle HorizontalAlign="Left" Font-Names="Arial" Font-Size="8pt" BackColor="White"
                                                                ForeColor="#003399" />
                                                            <HeaderStyle Font-Bold="True" ForeColor="#CCCCFF" BackColor="#073088" Font-Names="Verdana"
                                                                Font-Size="8pt" />
                                                            <RowStyle BackColor="Snow" BorderColor="Gainsboro" BorderStyle="Solid" BorderWidth="1px" />
                                                            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S.NO">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="jobsno" HeaderText="JNO" SortExpression="jobsno">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="transport_mode" HeaderText="MODE" SortExpression="transport_mode">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="party_name" HeaderText="IMPORTER" SortExpression="party_name">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="inv_dtl" HeaderText="BE Heading" SortExpression="inv_dtl">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="TOT_ASS_VL" HeaderText="A/VALUE" SortExpression="TOT_ASS_VL">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="TOT_DUTY" HeaderText="TOTAL DUTY" SortExpression="TOT_DUTY">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="NO_OF_PKG" HeaderText="NOP" SortExpression="NO_OF_PKG">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PKG_UNIT" HeaderText="PKG Type" SortExpression="PKG_UNIT">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="doc_received_date" HeaderText="Doc Recv Date" SortExpression="doc_received_date">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="gld" HeaderText="Flt/GLD Date" SortExpression="gld">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="BE_NO" HeaderText="BE Number" SortExpression="BE_NO">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="BE_DATE" HeaderText="BE Date" SortExpression="BE_DATE">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="dutyInform_date" HeaderText="Duty Info" SortExpression="dutyInform_date">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="dutyRecd_date" HeaderText="Duty Recv" SortExpression="dutyRecd_date">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="dutyPaid_date" HeaderText="Duty Paid" SortExpression="dutyPaid_date">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OOC_Date" HeaderText="OOC Taken" SortExpression="OOC_Date">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="DELIVERY_DATE" HeaderText="Delivery" SortExpression="DELIVERY_DATE">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="forwardAC_DATE" HeaderText="Forwarded to A/C" SortExpression="forwardAC_DATE">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Billing_date" HeaderText="Billing" SortExpression="Billing_date" >
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Despatch Date" SortExpression="Billing_date">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <%--<asp:TemplateField HeaderText="Despatch Date">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtdespatch" Font-Names="Arial" Font-Size="7pt" Height="20px" Width="70px"
                                                                    runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                                <asp:BoundField DataField="BILL_NO" HeaderText="Bill Number" SortExpression="BILL_NO">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="BILL_DATE" HeaderText="Bill Date" SortExpression="BILL_DATE">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="DB_NOTE_NO" HeaderText="Debit Note No" SortExpression="DB_NOTE_NO">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="DB_DATE" HeaderText="Debit Note Date" SortExpression="DB_DATE">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                 <asp:BoundField DataField="cntr_move_date" HeaderText="Cntr Move Date" SortExpression="DB_DATE">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Billing for after Delivery" HeaderStyle-Wrap="false" SortExpression="Billing_date">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                 <asp:BoundField HeaderText="Inward To OOC Taken" HeaderStyle-Wrap="false" SortExpression="Billing_date">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                 <asp:BoundField HeaderText="BE Date To OOC Taken" HeaderStyle-Wrap="false" SortExpression="Billing_date">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                  <asp:BoundField HeaderText="Duty Paid To OOC Taken" HeaderStyle-Wrap="false" SortExpression="Billing_date">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                 <asp:BoundField HeaderText="Cntr Move To OOC Taken" HeaderStyle-Wrap="false" SortExpression="Billing_date">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                  <asp:BoundField HeaderText="Doc Received To OOC Taken" HeaderStyle-Wrap="false" SortExpression="Billing_date">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table>
            <tr>
                <td>
                    <asp:Button ID="ExportPage" OnClick="ExportPage_Click" runat="server" Text="Export to  Excel"
                        Font-Size="8pt" Font-Names="Arial" Height="25px" Width="100px"></asp:Button>
                    <asp:Label ID="lbROnly" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="Red"
                        Width="383px"></asp:Label>
                    <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="Red"
                        Width="383px"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

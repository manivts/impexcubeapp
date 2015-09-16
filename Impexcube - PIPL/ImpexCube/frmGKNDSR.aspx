<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmGKNDSR.aspx.cs" Inherits="ImpexCube.frmGKNDSR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <span style="color: #000066">Please wait...</span><asp:Image ID="Image1" runat="server"
                ImageUrl="~/Content/Images/progress.gif" Width="35px" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFdate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTdate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:AutoCompleteExtender ID="autoComplete2" runat="server" TargetControlID="txtPName"
                ServicePath="~/AutoComplete.asmx" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                CompletionListCssClass="completionList" ServiceMethod="GetJobCustomer" MinimumPrefixLength="1">
            </cc1:AutoCompleteExtender>
            <cc1:AutoCompleteExtender ID="autoComplete1" runat="server" TargetControlID="txtJNO"
                ServicePath="~/AutoComplete.asmx" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                CompletionListCssClass="completionList" ServiceMethod="GetJobNo" MinimumPrefixLength="1">
            </cc1:AutoCompleteExtender>
            <table style="width: 100%">
                <tr>
                    <td style="vertical-align: top; height: 419px;">
                        <table style="width: 100%;">
                            <tr>
                                <td align="left" style="vertical-align: top; width: 90%;">
                                    <table style="width: 100%;">
                                        <tr style="background-color: #f0f5f9;">
                                            <td style="border-bottom: solid 1px Lavender;" align="center">
                                                <asp:Label ID="Label7" runat="server" Text="PIPL - GKN Reports" Width="100%" Font-Bold="True"
                                                    Font-Names="Arial" Font-Size="10pt" Height="18px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="background-color: #f0f5f9;">
                                            <td style="vertical-align: top;">
                                                <table style="width: 90%;">
                                                    <tr>
                                                        <td align="left" id="tblMst" runat="server" style="vertical-align: top; width: 600px;">
                                                            <table style="width: 100%; height: 22px;">
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
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblto" runat="server" Font-Names="Arial" Font-Size="7pt" Text="TO:"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtTdate" runat="server" Font-Names="Arial" Font-Size="7pt" Width="50px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="7pt" Text="Report"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drReport" Font-Names="Arial" Font-Size="7pt" runat="server"
                                                                                        Width="130px" Font-Overline="False">
                                                                                        <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                                                        <asp:ListItem>SeaGIT</asp:ListItem>
                                                                                        <asp:ListItem>SeaCleared</asp:ListItem>
                                                                                        <asp:ListItem>AirGIT</asp:ListItem>
                                                                                        <asp:ListItem>AirCleared</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:CheckBox ID="chkIMP" runat="server" Font-Names="Arial" Font-Size="7pt" OnCheckedChanged="chkIMP_CheckedChanged"
                                                                                        Text="ImpName" AutoPostBack="True" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtPName" runat="server" Font-Names="Arial" Font-Size="7pt" Width="110px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:CheckBox ID="chkJNO" runat="server" Font-Names="Arial" Font-Size="7pt" OnCheckedChanged="chkJNO_CheckedChanged"
                                                                                        Text="JNO" AutoPostBack="True" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtJNO" runat="server" Font-Names="Arial" Font-Size="7pt" Width="100px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Button ID="BtnSubmit" runat="server" CssClass="button_image" OnClick="BtnSubmit_Click"
                                                                                        Text="Search" Width="55px" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Button ID="ExportPage" OnClick="ExportPage_Click" runat="server" Text="Export Excel"
                                                                                        CssClass="button_image" Width="83px"></asp:Button>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Button ID="Button1" runat="server" CssClass="button_image" PostBackUrl="~/PIPL/frmMarketmodulePIPL.aspx"
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
                                                <div id="GridScroll" class="grid_scroll12" style="width: 920px;">
                                                    <asp:GridView ID="Grdiworkreg" runat="server" BackColor="White" BorderColor="#DEDFDE"
                                                        BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Arial" Font-Size="8pt"
                                                        OnRowDataBound="Grdiworkreg_RowDataBound" ForeColor="Black" GridLines="Vertical"
                                                        AutoGenerateColumns="False">
                                                        <RowStyle HorizontalAlign="Left" Font-Names="Arial" Font-Size="8pt" BackColor="#F7F7DE" />
                                                        <HeaderStyle BackColor="#073088" CssClass="header" Font-Names="verdana" Font-Size="8pt"
                                                            ForeColor="#CCCCFF" />
                                                        <RowStyle BackColor="Snow" BorderColor="Gainsboro" BorderStyle="Solid" BorderWidth="1px" />
                                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.NO">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="jobno" HeaderText="ShipmentSrNo" SortExpression="jobno">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <%--  <asp:BoundField DataField="ETAMonth" HeaderText="Job Month" SortExpression="ETAMonth">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="JobWeek" HeaderText="Job Week" SortExpression="JobWeek">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>--%>
                                                            <asp:BoundField DataField="currentStatus" HeaderText="QuickStatus" SortExpression="currentStatus">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="" HeaderText="FF Refernce No." SortExpression="">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="JobReceivedDate" HeaderText="PreAlertReceivedDate" SortExpression="JobReceivedDate">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="eta" HeaderText="ETA" SortExpression="eta">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Supplier Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCnsrName" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="CountryOrigin" HeaderText="Country" SortExpression="CountryOrigin">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Invoice No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblInvoiceNo" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Invoice Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblInvoiceDate" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Inv Value">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblInvoiceValue" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Foreign Currency">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCurrency" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="NoOfPackages" HeaderText="No Of Packages" SortExpression="NoOfPackages">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NetWeight" HeaderText="Net Weight(In Kg)" SortExpression="">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <%--
                                                            <asp:BoundField DataField="gross_wt" HeaderText="Gross Weight(In Kg)" SortExpression="gross_wt">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>--%>
                                                            <asp:TemplateField HeaderText="Gross Weight(In Kg)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGrossWt" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="" HeaderText="CBM">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Cont Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblContType" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="ShippingLine" HeaderText="Forwarder" SortExpression="ShippingLine">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ShippingLine" HeaderText="LINNER/CO-LOADER" SortExpression="">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="" HeaderText="DESTINATION AGENT" SortExpression="">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="VesselName" HeaderText="Vessel Name" SortExpression="VesselName">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="VoyageNo" HeaderText="Voyage No" SortExpression="VoyageNo">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="MasterBLNo" HeaderText="MasterBillOfLading" SortExpression="MasterBLNo">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>

                                                           <%-- <asp:BoundField DataField="hawb_no" HeaderText="FBLorHBLNo" SortExpression="hawb_no">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>--%>
                                                              <asp:TemplateField HeaderText="FBLorHBLNo">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHBL" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:BoundField DataField="PortOfShipment" HeaderText="Port Of Loading" SortExpression="PortOfShipment">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="" HeaderText="Chargeble Weight ( In Kg.)" SortExpression="">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Port Of Discharge">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label2" runat="server" Text="Chennai"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="InvoiceTerms" HeaderText="InCoTerms" SortExpression="InvoiceTerms">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Cont No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblContNo" runat="server" Text=""></asp:Label></r>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Cont Size">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblContSize" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="" HeaderText="PickUpDate" SortExpression="">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="HouseBLDate" HeaderText="ATD" SortExpression="HouseBLDate">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="GLDInwardDate" HeaderText="ATA" SortExpression="GLDInwardDate">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="BENo" HeaderText="BENo" SortExpression="BENo">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="BEDate" HeaderText="BEDate" SortExpression="BEDate">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="TotalAssVal" HeaderText="AssValue" SortExpression="TotalAssVal">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="TotalDuty" HeaderText="ActualDuty" SortExpression="TotalDuty">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="16" HeaderText="Date of Duty Request" SortExpression="">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="17" HeaderText="Date of Duty Paid" SortExpression="">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="21" HeaderText="CustomsClearanceDate" SortExpression="">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="" HeaderText="Actual Date of Shipment received at Plant"
                                                                SortExpression="">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="" HeaderText="GRN No" SortExpression="">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <%-- <asp:TemplateField HeaderText="Description Of Goods">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDesc" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="gross_unit" HeaderText="Gross Unit" SortExpression="gross_unit">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="tot_duty" HeaderText="Approx.Cust Duty in INR" SortExpression="tot_duty">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="mawb_date" HeaderText="Date of B/L" SortExpression="mawb_date">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="GLD" HeaderText="IGMDate" SortExpression="GLD">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="GLD" HeaderText="IGMSplitDate" SortExpression="GLD">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="46" HeaderText="RMS/AssessmentDate" SortExpression="46">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="26" HeaderText="Date of Original BOE's Sent to GDI" SortExpression="">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="26" HeaderText="PI Invoice Submission Date" SortExpression="">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="26" HeaderText="Payment Receipt Date" SortExpression="">
                                                                <ItemStyle Wrap="False" />
                                                            </asp:BoundField>--%>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Label ID="lbROnly" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="Red"
                                        Width="383px"></asp:Label>
                                    <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="Red"
                                        Width="383px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ExportPage" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

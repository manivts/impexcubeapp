<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmJobStatusGroupWiseReport.aspx.cs" Inherits="ImpexCube.frmJobStatusGroupWiseReport" %>

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
                ImageUrl="~/images/progress.gif" Width="35px" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <cc1:CalendarExtender ID="CE1" TargetControlID="txtFdate" Format="dd/MM/yyyy" runat="server">
            </cc1:CalendarExtender>
            <cc1:CalendarExtender ID="CE2" TargetControlID="txtTdate" Format="dd/MM/yyyy" runat="server">
            </cc1:CalendarExtender>
            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtFdate"
                FilterType="Custom" ValidChars="0123456789/" runat="server">
            </cc1:FilteredTextBoxExtender>
            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtTdate"
                FilterType="Custom" ValidChars="0123456789/" runat="server">
            </cc1:FilteredTextBoxExtender>
            <cc1:TextBoxWatermarkExtender ID="TWE1" TargetControlID="txtFdate" WatermarkText="dd/MM/yyyy"
                WatermarkCssClass="wDATE" runat="server">
            </cc1:TextBoxWatermarkExtender>
            <cc1:TextBoxWatermarkExtender ID="TWE2" TargetControlID="txtTdate" WatermarkText="dd/MM/yyyy"
                WatermarkCssClass="wDATE" runat="server">
            </cc1:TextBoxWatermarkExtender>
            <table style="background-color: White; width: 100%;">
                <tr style="background-color: #f0f5f9;">
                    <td style="border-bottom: solid 1px Lavender;" align="center">
                        <asp:Label ID="Label1" runat="server" Text="Job Status - Group Wise Reports" Width="100%"
                            Font-Bold="True" Font-Names="verdana" Font-Size="10pt" Height="18px"></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: #f0f5f9;">
                    <td>
                        <table style="background-color: #f0f5f9;">
                            <tr style="background-color: #f0f5f9;">
                                <td align="left">
                                    <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="7pt" Text="Doc Recd From:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFdate" runat="server" Font-Names="Arial" Font-Size="7pt" Width="55px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="7pt" Text="To "
                                        Width="15px"></asp:Label>
                                    <asp:TextBox ID="txtTdate" runat="server" Font-Names="Arial" Font-Size="7pt" Width="55px"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:Label ID="Label4" runat="server" Text="Shipment Type " Font-Names="Arial" Font-Size="7pt"
                                        Width="77px"></asp:Label>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbMode" runat="server" Font-Names="Arial" Font-Size="7pt"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Value="A">AIR</asp:ListItem>
                                        <asp:ListItem Value="S">SEA</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Font-Size="7pt" Font-Names="Arial" Width="73px"
                                        Text="Job Stage Group "></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="drStageGroup" runat="server" Font-Size="7pt" Font-Names="Arial"
                                        Width="130px">
                                    </asp:DropDownList>
                                </td>
                                <td align="left">
                                    <asp:RadioButtonList ID="RBStage" runat="server" Font-Names="Arial" Font-Size="7pt"
                                        RepeatDirection="Horizontal" Style="margin-left: 0px">
                                        <asp:ListItem Value="0">Pending</asp:ListItem>
                                        <asp:ListItem Value="1">Completed</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:Button ID="BtnSubmit" OnClick="BtnSubmit_Click" runat="server" CssClass="button_image1"
                                        Text="Search" Width="60px"></asp:Button>
                                </td>
                                <td>
                                    <asp:Button ID="ExportPage" runat="server" CssClass="button_image1" OnClick="ExportPage_Click"
                                        Text="Export Excel" Width="80px" />
                                </td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" CssClass="button_image1" Width="60px" Text="Exit"
                                        PostBackUrl="~/PIPL/frmMarketmodulePIPL.aspx"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="height: 650px; vertical-align: top;">
                        <div id="GridScroll" class="grid_scroll12">
                            <asp:GridView ID="Grdiworkreg" runat="server" BackColor="White" BorderColor="#3366CC"
                                BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Arial" Font-Size="7pt"
                                AutoGenerateColumns="False" OnRowDataBound="Grdiworkreg_RowDataBound">
                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                <RowStyle BackColor="White" ForeColor="#003399" />
                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                <HeaderStyle BackColor="#073088" Font-Names="verdana" Font-Size="7pt" ForeColor="#CCCCFF" />
                                <Columns>
                                    <asp:TemplateField HeaderText="S.NO">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="jobsno" HeaderText="JOB NO" SortExpression="jobsno" />
                                    <asp:BoundField DataField="job_date" HeaderText="DATE" SortExpression="job_date" />
                                    <asp:BoundField DataField="party_name" HeaderText="PARTY NAME" SortExpression="party_name">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CONT_ORIG" HeaderText="SUPPLIER COUNTRY" SortExpression="CONT_ORIG" />
                                    <asp:BoundField DataField="carrier" HeaderText="S/LINE" SortExpression="jobsno" />
                                    <asp:BoundField DataField="mawb_no" HeaderText="B/L NO" SortExpression="mawb_no" />
                                    <asp:BoundField DataField="mawb_date" HeaderText="B/L DATE" SortExpression="mawb_date" />
                                    <asp:BoundField DataField="INV_DTL" HeaderText="DESCRIPTION" SortExpression="INV_DTL">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="PKGS">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "NO_OF_PKG") %>
                                            -
                                            <%# DataBinder.Eval(Container.DataItem, "PKG_UNIT") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GR.WT">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "GROSS_WT") %>
                                            -
                                            <%# DataBinder.Eval(Container.DataItem, "GROSS_UNIT") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="be_no" HeaderText="B/E NO" SortExpression="be_no" />
                                    <asp:BoundField DataField="be_date" HeaderText="BE DATE" SortExpression="be_date" />
                                    <asp:BoundField DataField="Remarks" HeaderText="STATUS OF THE BILL - PENDING REMARKS"
                                        SortExpression="Remarks">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="eta" HeaderText="ETA" SortExpression="eta" />
                                    <asp:BoundField DataField="JobStage" HeaderText="STAGE" SortExpression="JobStage">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="JobStatus" HeaderText="STATUS" SortExpression="JobStatus">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Remarks" HeaderText="REMARKS" SortExpression="Remarks">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="transport_mode" HeaderText="SHIPMENT TYPE" SortExpression="transport_mode">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="bill_no" HeaderText="Bill No" SortExpression="bill_no">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                            <asp:Label ID="lbROnly" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="Red"
                                Width="383px"></asp:Label>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ExportPage" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

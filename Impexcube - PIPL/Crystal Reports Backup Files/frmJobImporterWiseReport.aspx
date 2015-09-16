<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="frmJobImporterWiseReport.aspx.cs" Inherits="ImpexCube.frmJobImporterWiseReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <span style="color: #000066">Please wait...</span><asp:Image ID="Image1" runat="server"
                ImageUrl="~/images/progress.gif" Width="35px" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFdate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTdate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtFdate"
                FilterType="Custom" ValidChars="0123456789/" runat="server">
            </cc1:FilteredTextBoxExtender>
            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtTdate"
                FilterType="Custom" ValidChars="0123456789/" runat="server">
            </cc1:FilteredTextBoxExtender>
            <table style="width: 100%;">
                <tr>
                    <td style="vertical-align: top;">
                        <table style="width: 100%;">
                            <tr style="background-color: #f0f5f9;">
                                <td style="border-bottom: solid 1px Lavender;" align="center">
                                    <asp:Label ID="Label4" runat="server" Text="Job Status - Importer Wise Reports" Width="100%"
                                        Font-Bold="True" Font-Names="verdana" Font-Size="10pt" Height="18px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" id="tblMst" runat="server" style="vertical-align: top; width: 829px;
                                    background-color: #f0f5f9;">
                                    <table style="width: 903px">
                                        <tr>
                                            <td align="left" style="height: 22px; width: 400px;">
                                                <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="7pt" Text="Doc Received From"
                                                    Width="89px" Font-Strikeout="False" ForeColor="Black"></asp:Label>
                                                <asp:TextBox ID="txtFdate" runat="server" Font-Names="Arial" Font-Size="7pt" Width="76px"
                                                    Font-Strikeout="False"></asp:TextBox>
                                                <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="8pt" Text="To"
                                                    Width="14px"></asp:Label>
                                                <asp:TextBox ID="txtTdate" runat="server" Font-Names="Arial" Font-Size="7pt" Width="70px"
                                                    Font-Strikeout="False"></asp:TextBox>
                                            </td>
                                            <td style="width: 900px;">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:RadioButtonList ID="rbBill" runat="server" Font-Names="Arial" Font-Size="7pt"
                                                            RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbBill_SelectedIndexChanged"
                                                            Width="159px">
                                                            <asp:ListItem Value="DP">Direct Party</asp:ListItem>
                                                            <asp:ListItem Value="TP">Third Party</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                        <asp:Label ID="Label7" runat="server" Font-Names="arial" Font-Size="7pt" Font-Strikeout="False"
                                                            Text="Importer  " Width="64px" ForeColor="Black" Height="16px"></asp:Label>
                                                        <asp:DropDownList ID="drCustomer" runat="server" Font-Names="Arial" Font-Size="7pt"
                                                            Font-Strikeout="False" Width="180px" OnSelectedIndexChanged="drCustomer_SelectedIndexChanged"
                                                            AutoPostBack="True">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="Label9" runat="server" Text="Select Report Name :" 
                                                            Font-Size="7pt" Font-Names="arial"
                                                            Width="100px" Font-Strikeout="False" ForeColor="Black" Height="16px"></asp:Label>
                                                        <asp:DropDownList ID="drClientRPT" runat="server" Font-Names="Arial" Font-Size="7pt"
                                                            Width="136px">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr id="tr1" runat="server">
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Text="Pending Job Stage" Font-Size="7pt" Font-Names="arial"
                                                    Width="88px" Font-Strikeout="False" ForeColor="Black" Height="16px"></asp:Label>
                                                <asp:DropDownList ID="drPenStage" runat="server" Font-Size="7pt" Font-Names="Arial"
                                                    Width="154px" Font-Strikeout="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="7pt" Text="E-Mail Address "
                                                    ForeColor="Black" Font-Strikeout="False"></asp:Label>
                                                <asp:TextBox ID="TextBox1" runat="server" Font-Names="Arial" Font-Size="8pt" Width="165px"></asp:TextBox>
                                                <asp:Button ID="BtnSubmit" OnClick="BtnSubmit_Click" runat="server" Text="Search"
                                                    CssClass="button_image1" Width="70px"></asp:Button>
                                                <asp:Button ID="BtnSendMail" runat="server" CssClass="button_image1" Text="Send mail"
                                                    Width="70px" OnClick="BtnSendMail_Click" />
                                                <asp:Button ID="ExportPage" runat="server" CssClass="button_image1" OnClick="ExportPage_Click"
                                                    Text="Export Excel" Width="85px" />
                                                <asp:Button ID="Button1" runat="server" Text="Exit" CssClass="button_image1" PostBackUrl="http://version6/pipl/PIPL/frmMarketmodulePIPL.aspx"
                                                    Width="60px" OnClick="Button1_Click" />
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                      
                                    </table>
                                </td>
                            </tr>
                            <tr id="trMail" runat="server">
                                <td style="width: 829px">
                                    <div id="GridScroll" class="grid_scroll" style="width: 920px;">
                                        <asp:GridView ID="Grdiworkreg" runat="server" BackColor="White" BorderColor="#3366CC"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Arial" Font-Size="8pt"
                                            AutoGenerateColumns="False" OnRowDataBound="Grdiworkreg_RowDataBound">
                                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                            <RowStyle BackColor="White" ForeColor="#003399" />
                                            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#073088" Font-Bold="True" ForeColor="#CCCCFF" Font-Size="8pt" />
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
                                                <asp:TemplateField HeaderText="SUPPLIER">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSupplier" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="false" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CONT_ORIG" HeaderText="COUNTRY" SortExpression="CONT_ORIG" />
                                                <asp:BoundField DataField="carrier" HeaderText="S/LINE" SortExpression="jobsno" />
                                                <asp:BoundField DataField="mawb_no" HeaderText="MAWB/MBL NO" SortExpression="mawb" />
                                                <asp:BoundField DataField="mawb_date" HeaderText="MAWB/MBL DATE" SortExpression="mawb_date" />
                                                <asp:BoundField DataField="hawb_no" HeaderText="HAWB/HBL NO" SortExpression="hawb" />
                                                <asp:BoundField DataField="hawb_date" HeaderText="HAWB/HBL Date" SortExpression="hawb_date" />
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
                                            </Columns>
                                        </asp:GridView>
                                        <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#3366CC"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Arial" Font-Overline="False"
                                            Font-Size="7pt" OnRowDataBound="GridView1_RowDataBound">
                                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                            <RowStyle BackColor="White" ForeColor="#003399" />
                                            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                            <HeaderStyle BackColor="#073088" Font-Bold="True" ForeColor="#CCCCFF" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox1"
                            ErrorMessage="Invalid E-Mail address" Font-Names="Arial" Font-Size="7pt" Height="10px"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Width="144px"></asp:RegularExpressionValidator>
                        <asp:Label ID="lbROnly" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"
                            Width="383px"></asp:Label>
                    </td>
                </tr>
                <%--     <table style=" width: 100%;">--%>
                <tr>
                    <td align="left" style="vertical-align: top; width: 90%;">
                        <asp:Label ID="lblError" runat="server" Width="283px"></asp:Label>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ExportPage" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmJobStatusStage.aspx.cs" Inherits="ImpexCube.frmJobStatusStage" %>

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
                ImageUrl="~/image/progress.gif" Width="35px" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <cc1:calendarextender id="CE1" targetcontrolid="txtFdate" format="dd/MM/yyyy" runat="server">
            </cc1:calendarextender>
            <cc1:filteredtextboxextender id="FilteredTextBoxExtender1" targetcontrolid="txtFdate"
                filtertype="Custom" validchars="0123456789/" runat="server">
            </cc1:filteredtextboxextender>
            <cc1:calendarextender id="CE2" targetcontrolid="txtTdate" format="dd/MM/yyyy" runat="server">
            </cc1:calendarextender>
            <cc1:filteredtextboxextender id="FilteredTextBoxExtender2" targetcontrolid="txtTdate"
                filtertype="Custom" validchars="0123456789/" runat="server">
            </cc1:filteredtextboxextender>
            <cc1:textboxwatermarkextender id="TWE1" targetcontrolid="txtFdate" watermarktext="dd/MM/yyyy"
                watermarkcssclass="wDATE" runat="server">
            </cc1:textboxwatermarkextender>
            <cc1:textboxwatermarkextender id="TWE2" targetcontrolid="txtTdate" watermarktext="dd/MM/yyyy"
                watermarkcssclass="wDATE" runat="server">
            </cc1:textboxwatermarkextender>
            <table style="width: 100%">
                <tr>
                    <td style="vertical-align: top; height: 419px;">
                        <table style="width: 100%;">
                            <tr>
                                <td align="left" style="vertical-align: top; width: 90%;">
                                    <table style="width: 100%;">
                                        <tr style="background-color: #f0f5f9;">
                                            <td style="border-bottom: solid 1px Lavender;" align="center">
                                                <asp:Label ID="Label6" runat="server" Text="Job Status - Stage Wise Reports" Width="100%"
                                                    Font-Bold="True" Font-Names="verdana" Font-Size="10pt" Height="18px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="background-color: #f0f5f9;">
                                            <td align="left" style="vertical-align: top;">
                                                <table>
                                                    <tr>
                                                        <td align="left" id="tblMst" runat="server" style="vertical-align: top;">
                                                            <table>
                                                                <tr style="background-color: #f0f5f9;">
                                                                    <td align="left">
                                                                        <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="7pt" Text="Doc Recd From:"
                                                                            Font-Strikeout="False"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFdate" runat="server" Font-Names="Arial" Font-Size="7pt" Width="55px"
                                                                            Font-Strikeout="False"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="8pt" Text="To"
                                                                            Width="14px"></asp:Label>
                                                                    </td>
                                                                    <td style="margin-left: 80px">
                                                                        <asp:TextBox ID="txtTdate" runat="server" Font-Names="Arial" Font-Size="7pt" Width="55px"
                                                                            Font-Strikeout="False"></asp:TextBox>
                                                                    </td>
                                                                     <td style="margin-left: 80px">
                                                                       
                                                                         <asp:Label ID="Label9" runat="server" Font-Names="arial" Font-Size="7pt" 
                                                                             Font-Strikeout="False" Text="Mode"></asp:Label>
                                                                       
                                                                    </td>
                                                                     <td style="margin-left: 80px">
                                                                       
                                                                         <asp:DropDownList ID="ddlMode" runat="server" Font-Names="Arial" 
                                                                             Font-Size="7pt" Font-Strikeout="False" Width="50px">
                                                                             <asp:ListItem>Air</asp:ListItem>
                                                                             <asp:ListItem>Sea</asp:ListItem>
                                                                             <asp:ListItem>Both</asp:ListItem>
                                                                         </asp:DropDownList>
                                                                       
                                                                    </td>
                                                                    <td style="vertical-align: middle; width: 100px; height: 16px;">
                                                                        <asp:RadioButtonList ID="RBStage" runat="server" Font-Names="Arial" Font-Size="7pt"
                                                                            RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="0">Pending</asp:ListItem>
                                                                            <asp:ListItem Value="1">Completed</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label7" runat="server" Font-Names="arial" Font-Size="7pt" Font-Strikeout="False"
                                                                            Text="Select Customer" Width="84px"></asp:Label><br />
                                                                        <asp:DropDownList ID="drCustomer" runat="server" Font-Names="Arial" Font-Size="7pt"
                                                                            Font-Strikeout="False" Width="130px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label8" runat="server" Text="Select Job Stage" Font-Size="7pt" Font-Names="arial"
                                                                            Width="84px" Font-Strikeout="False" CssClass="style3"></asp:Label><br />
                                                                        <asp:DropDownList ID="drPenStage" runat="server" Font-Size="7pt" Font-Names="Arial"
                                                                            Width="110px" Font-Strikeout="False" CssClass="style4">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td valign="bottom">
                                                                        <asp:Button ID="BtnSubmit" OnClick="BtnSubmit_Click" runat="server" Text="Search"
                                                                            CssClass="button_image1" Width="60px"></asp:Button>
                                                                    </td>
                                                                    <td valign="bottom">
                                                                        <asp:Button ID="ExportPage" OnClick="ExportPage_Click" runat="server" Text="Export Excel"
                                                                            CssClass="button_image1" Width="90px"></asp:Button>
                                                                    </td>
                                                                    <td valign="bottom" style="width: 6px">
                                                                        <asp:Button ID="btnExit" runat="server" CssClass="button_image1" PostBackUrl="HomePage.aspx"
                                                                            Text="Exit" Width="60px" />
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
                                                <asp:Label ID="lbROnly" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="Red"
                                                    Width="383px"></asp:Label>
                                               <div id="GridScroll" class="grid_scroll12" style="width: 890px;">
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
                                                        <asp:BoundField DataField="jobno" HeaderText="JOB NO" SortExpression="jobno" />
                                                        <asp:BoundField DataField="JobReceivedDate" HeaderText="DATE" SortExpression="JobReceivedDate" />
                                                        <asp:BoundField DataField="Mode" HeaderText="Mode" SortExpression="JobReceivedDate" />
                                                        <asp:BoundField DataField="Importer" HeaderText="PARTY NAME" SortExpression="Importer">
                                                            <ItemStyle Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ShipmentCountry" HeaderText="SUPPLIER COUNTRY" SortExpression="ShipmentCountry" />
                                                        <%--<asp:BoundField DataField="ShippingLine" HeaderText="S/LINE" SortExpression="ShippingLine" />--%>
                                                        <asp:BoundField DataField="MasterBLNo" HeaderText="B/L NO" SortExpression="MasterBLNo" />
                                                        <asp:BoundField DataField="MasterBLDate" HeaderText="B/L DATE" SortExpression="MasterBLDate" />
                                                        <asp:BoundField DataField="BEHeading" HeaderText="DESCRIPTION" SortExpression="InvoiceDetail">
                                                            <ItemStyle Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="PKGS">
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container.DataItem, "NoOfPackages")%>
                                                                -
                                                                <%# DataBinder.Eval(Container.DataItem, "PackagesUnit")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="GR.WT">
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container.DataItem, "GrossWeight")%>
                                                                -
                                                                <%# DataBinder.Eval(Container.DataItem, "GrossWeightUnit")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="BENO" HeaderText="B/E NO" SortExpression="BENO" />
                                                        <asp:BoundField DataField="BEDATE" HeaderText="BE DATE" SortExpression="BEDATE" />
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
                                                      <%--  <asp:BoundField DataField="pend_jobstage" HeaderText="PENDING STAGE" SortExpression="pend_jobstage">
                                                            <ItemStyle Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="comp_remark" HeaderText="PENDING REMARKS" SortExpression="pend_remark">
                                                            <ItemStyle Wrap="False" />
                                                        </asp:BoundField>--%>
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
        <Triggers>
            <asp:PostBackTrigger ControlID="ExportPage" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

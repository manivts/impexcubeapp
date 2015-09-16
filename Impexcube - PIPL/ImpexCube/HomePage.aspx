<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="HomePage.aspx.cs" Inherits="VTS.ImpexCube.Web.HomePage" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .uppercase
        {
            text-transform: uppercase;
            font-family: Tahoma;
            color: #5241B1;
        }
        
        .style12
        {
            width: 168px;
        }
        .grid_scroll-3
        {
            height: 270px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
        Width="1200px">
        <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1" >
            <HeaderTemplate>
                JobStatus
            </HeaderTemplate>
            <ContentTemplate>
                <table align="center" style="width: 900px;">
                    <tr>
                        <td align="center" colspan="4">
                            <asp:Label ID="lblUserDashBoard" runat="server" CssClass="uppercase"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 250px">
                            <table ID="tblImport" runat="server" visible="False" width="250px">
                                <tr runat="server">
                                    <td align="left" runat="server">
                                        <strong>Import</strong></td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server">
                                        <asp:LinkButton ID="lnkImpJob" runat="server" CssClass="fontsize" 
                                            OnClick="lnkImpJob_Click">Job Creation</asp:LinkButton>
                                        <asp:Label ID="lblImpJob" runat="server" CssClass="fontsize" ForeColor="Maroon"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server">
                                        <asp:LinkButton ID="lnkImpShip" runat="server" CssClass="fontsize" 
                                            OnClick="lnkImpShip_Click">ShipMent</asp:LinkButton>
                                        <asp:Label ID="lblImpShip" runat="server" CssClass="fontsize" 
                                            ForeColor="Maroon"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server">
                                        <asp:LinkButton ID="lnkImpInvoice" runat="server" CssClass="fontsize" 
                                            OnClick="lnkImpInvoice_Click">Invoice</asp:LinkButton>
                                        <asp:Label ID="lblImpInvoice" runat="server" CssClass="fontsize" 
                                            ForeColor="Maroon"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server">
                                        <asp:LinkButton ID="lnkImpProduct" runat="server" CssClass="fontsize" 
                                            OnClick="lnkImpProduct_Click">Product Completed</asp:LinkButton>
                                        <asp:Label ID="lblImpProduct" runat="server" CssClass="fontsize" 
                                            ForeColor="Maroon"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server">
                                        <asp:LinkButton ID="lnkImpBE" runat="server" CssClass="fontsize" 
                                            OnClick="lnkImpBE_Click">BE File Pending</asp:LinkButton>
                                        <asp:Label ID="lblImpBE" runat="server" CssClass="fontsize" ForeColor="Maroon"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 250px">
                            <table ID="tblExport" runat="server" visible="False" width="250px">
                                <tr runat="server">
                                    <td align="left" runat="server">
                                        <strong>Export</strong></td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server">
                                        <asp:LinkButton ID="lnkExpJob" runat="server" CssClass="fontsize" 
                                            OnClick="lnkExpJob_Click">Job Creation</asp:LinkButton>
                                        <asp:Label ID="lblExpJob" runat="server" CssClass="fontsize" ForeColor="Maroon"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server">
                                        <asp:LinkButton ID="lnkExpShipment" runat="server" CssClass="fontsize" 
                                            OnClick="lnkExpShipment_Click">ShipMent</asp:LinkButton>
                                        <asp:Label ID="lblExpShipment" runat="server" CssClass="fontsize" 
                                            ForeColor="Maroon"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server">
                                        <asp:LinkButton ID="lnkExpInvoice" runat="server" CssClass="fontsize" 
                                            OnClick="lnkExpInvoice_Click">Invoice</asp:LinkButton>
                                        <asp:Label ID="lblExpInvoice" runat="server" CssClass="fontsize" 
                                            ForeColor="Maroon"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server">
                                        <asp:LinkButton ID="lnkExpProduct" runat="server" CssClass="fontsize" 
                                            OnClick="lnkExpProduct_Click">Product Completed</asp:LinkButton>
                                        <asp:Label ID="lblExpProduct" runat="server" CssClass="fontsize" 
                                            ForeColor="Maroon"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server">
                                        <asp:LinkButton ID="lnkExpSB" runat="server" CssClass="fontsize" 
                                            OnClick="lnkExpSB_Click">SB File Pending</asp:LinkButton>
                                        <asp:Label ID="lblExpSB" runat="server" CssClass="fontsize" ForeColor="Maroon"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 250px" valign="top">
                            <table ID="tblCRM" runat="server" visible="False" width="250px">
                                <tr runat="server">
                                    <td align="left" runat="server">
                                        <strong>CRM</strong></td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server">
                                        <asp:LinkButton ID="lnkEnquiry" runat="server" CssClass="fontsize" 
                                            OnClick="lnkEnquiry_Click">Enquriy</asp:LinkButton>
                                        <asp:Label ID="lblEnquiry" runat="server" CssClass="fontsize" 
                                            ForeColor="Maroon"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server">
                                        <asp:LinkButton ID="lnkStandard" runat="server" CssClass="fontsize">Standard Rate</asp:LinkButton>
                                        <asp:Label ID="lblStandard" runat="server" CssClass="fontsize" 
                                            ForeColor="Maroon"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server">
                                        <asp:LinkButton ID="lnkQuote" runat="server" CssClass="fontsize">Quote</asp:LinkButton>
                                        <asp:Label ID="lblQuote" runat="server" CssClass="fontsize" ForeColor="Maroon"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server">
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server">
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 250px" valign="top">
                            <table ID="tblFund" runat="server" visible="False" width="250px">
                                <tr runat="server">
                                    <td align="left" runat="server">
                                        <strong>Fund Request</strong></td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server">
                                        <asp:LinkButton ID="lnkFundReq" runat="server" CssClass="fontsize" 
                                            OnClick="lnkFundReq_Click">Fund Request</asp:LinkButton>
                                        <asp:Label ID="lblFund" runat="server" CssClass="fontsize" ForeColor="Maroon"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server">
                                        <asp:LinkButton ID="lnkOpMgr" runat="server" CssClass="fontsize" 
                                            OnClick="lnkOpMgr_Click" Width="120px">Operational Manager</asp:LinkButton>
                                        <asp:Label ID="lblOperation" runat="server" CssClass="fontsize" 
                                            ForeColor="Maroon"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server">
                                        <asp:LinkButton ID="lnkAccMgr" runat="server" CssClass="fontsize" 
                                            OnClick="lnkAccMgr_Click">Accounts Manager</asp:LinkButton>
                                        <asp:Label ID="lblAccMgr" runat="server" CssClass="fontsize" ForeColor="Maroon"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server">
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                        </td>
                        <td class="style12" valign="top">
                        </td>
                        <td align="center" valign="top">
                        </td>
                        <td valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4">
                            <div class="grid_scroll" style="width: 900px">
                                <asp:GridView ID="gvDashBoard" runat="server" CssClass="table-wrapper" 
                                    HorizontalAlign="Left">
                                    <RowStyle CssClass="table-header light" HorizontalAlign="Center" 
                                        VerticalAlign="Middle" Wrap="True" />
                                    <HeaderStyle BackColor="#E7E7FF" CssClass="table-row" HorizontalAlign="Left" />
                                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                    <AlternatingRowStyle BackColor="#E7E7FF" />
                                    <SelectedRowStyle ForeColor="Blue" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
    <br />
    <%--<div id="container">
    <asp:LinkButton ID="lnkFeedBack" runat="server" PostBackUrl="~/frmFeedback.aspx"
            Font-Bold="True" Font-Italic="True" Font-Size="14pt">We welcome your feedback</asp:LinkButton></div>--%>
    <br />
    <br />
</asp:Content>

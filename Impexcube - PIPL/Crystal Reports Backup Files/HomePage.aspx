<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="HomePage.aspx.cs" Inherits="VTS.ImpexCube.Web.HomePage" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <div id="container">
        <asp:LinkButton ID="lnkFeedBack" runat="server" 
            PostBackUrl="~/frmFeedback.aspx">We welcome your feedback</asp:LinkButton>
        <br />
        <div class="container-area">

            <table style="display: none;" align="center">
                <tr>
                    <td style="width: 350px;">
                        <asp:Image ID="imgPurchase" runat="server" ImageUrl="~/Content/Images/money icon.png" />
                        <asp:LinkButton ID="lnkRequestPending" runat="server" Text="Fund Request[Pending]"
                            OnClick="lnkRequestPending_Click" Font-Size="10pt"></asp:LinkButton>
                        <asp:Label ID="lblRequestPending" runat="server" ForeColor="Red" 
                            Font-Size="10pt"></asp:Label>
                    </td>
                    <td style="width: 350px; text-align: left;">
                        <asp:Image ID="imgSales" runat="server" ImageUrl="~/Content/Images/Enquiry-icon.gif" />
                        <asp:LinkButton ID="lnKStageUpdate" runat="server" Text="Job Stage Status [Pending]"
                            OnClick="lnKStageUpdate_Click" Font-Size="10pt"></asp:LinkButton>
                        <asp:Label ID="lblStagePending" runat="server" ForeColor="Red" Font-Size="10pt"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 350px;">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Content/Images/jobCreated.jpg" />
                        <asp:LinkButton ID="lnkJobCreated" runat="server" Text="Job Created[Today]" 
                            OnClick="lnkJobCreated_Click" Font-Size="10pt"></asp:LinkButton>
                        <asp:Label ID="lblJobCreated" runat="server" ForeColor="Red" Font-Size="10pt"></asp:Label>
                    </td>
                    <td style="width: 350px; text-align: left;">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Content/Images/jobCreated.jpg" />
                        <asp:LinkButton ID="lnKStageUpdate0" runat="server" Text="Delivery-Billing Report"
                            OnClick="lnKStageUpdate0_Click" Font-Size="10pt"></asp:LinkButton>
                    </td>
                    <td rowspan="7">
                    </td>
                </tr>
                <%-- <tr>
                    <td style="width: 350px; text-align: left;">
                        <asp:Image ID="imgPayment" runat="server" ImageUrl="~/Image/PaymentIcon.jpg" Visible="False" />
                        <asp:LinkButton ID="lnkPayment" runat="server" Text="Payment [Approval]" OnClick="lnkPayment_Click"
                            Visible="False"></asp:LinkButton>
                        <asp:Label ID="lblPaymentPending" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 350px; text-align: left;">
                        <asp:Image ID="imgReceipt" runat="server" ImageUrl="~/Image/Receipt.jpg" Visible="False" />
                        <asp:LinkButton ID="lnkReceipt" runat="server" Text="Receipt [Approval]" OnClick="lnkReceipt_Click"
                            Visible="False"></asp:LinkButton>
                        <asp:Label ID="lblReceiptPending" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 350px; text-align: center;">
                        &nbsp;
                    </td>
                    <td style="width: 350px; text-align: center;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 350px;">
                        <asp:Image ID="imgJournal" runat="server" ImageUrl="~/Image/Journal.jpg" Width="25px"
                            Visible="False" />
                        <asp:LinkButton ID="lnkJournal" runat="server" Text="Journal [Approval]" OnClick="lnkJournal_Click"
                            Visible="False"></asp:LinkButton>
                        <asp:Label ID="lblJournalPending" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 350px; text-align: left;">
                        <asp:Image ID="imgOutStanding" runat="server" ImageUrl="~/Image/Outstanding.jpg"
                            Width="25px" Visible="False" />
                        <asp:LinkButton ID="lnkOutStanding" runat="server" Text="No. of OutStanding" OnClick="lnkOutStanding_Click"
                            Visible="False"></asp:LinkButton>
                        <asp:Label ID="lblOutstanding" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                    </td>
                </tr>--%>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;<asp:Label ID="lblChartFormat" runat="server" Text=" Chart Format" 
                            Visible="False" Font-Size="10pt"></asp:Label>
&nbsp;
                        <asp:DropDownList ID="ddlChartFormat" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlChartFormat_SelectedIndexChanged" Width="200px" 
                            Visible="False" Font-Size="10pt">
                            <asp:ListItem>Doughnut</asp:ListItem>
                            <asp:ListItem>Pie</asp:ListItem>
                            <asp:ListItem>Pyramid</asp:ListItem>
                            <asp:ListItem>Bar</asp:ListItem>
                            <asp:ListItem>Column</asp:ListItem>
                            <asp:ListItem>Funnel</asp:ListItem>
                            <asp:ListItem>Spline Area</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr align="left">
                <td>
                <asp:Chart ID="Chart1" runat="server" Width="600px" 
                            DataSourceID="SqlDataSourceTday" Palette="Fire" 
                        ToolTip="Delivery Chart" Height="400px" Visible="False" >
                            <Titles>
                                <asp:Title ShadowOffset="3" Name="Title1" />
                            </Titles>
                            <Legends>
                                <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
                                    LegendStyle="Row" />
                            </Legends>
                            <Series>
                                <asp:Series Name="Default" ChartType="Doughnut" XValueMember="Grade" 
                                    YValueMembers="tday" />
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
                            </ChartAreas>
                        </asp:Chart>
                        <asp:SqlDataSource ID="SqlDataSourceTday" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:Constr %>" 
                            SelectCommand="SELECT [NoofCount], [tday], [Grade] FROM [View_Graph]">
                        </asp:SqlDataSource>
                </td>
                    <td colspan="3" style="text-align: Center">
                        <div class="grid_scroll-b">
                            <asp:GridView ID="gvPendingList" runat="server" AutoGenerateColumns="true" BorderColor="Black"
                                BorderStyle="Solid" Font-Names="Cambria" BorderWidth="1px" 
                                Font-Size="10pt" ForeColor="Black"
                                ShowFooter="false" ShowHeader="true" Style="text-align: left" 
                                OnRowDataBound="gvPendingList_RowDataBound">
                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" BorderColor="Black" BorderStyle="Solid"
                                    BorderWidth="1px" />
                                <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="#E7E7FF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        
                    </td>
                </tr>
            </table>
            
            <table align="right" style="width: 26%;">
                <tr>
                    <td bgcolor="#339966">
                        <strong>Updations Done in the Application </strong>
                    </td>
                </tr>
                <tr>
                    <td>
                    <marquee behavior="scroll"  direction="up" scrollamount="3">
                         <table>
                         
                <tr>
                    <td style="text-align: left">
&nbsp;<strong>12.12.2013</strong></td>
                </tr>
                <tr>
                    <td class="style1">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                        1. Import Module Job Creation</td>
                </tr>
                <tr>
                    <td class="style1">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                        2. Import Module Shipment</td>
                </tr>
                <tr>
                    <td class="style1">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                        3. Import Module Invoice&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                        4. Import Module Product Details</td>
                </tr>
                <tr>
                    <td class="style1">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                        5. Import Module Check List</td>
                </tr></table></marquee>
                 </td>
                </tr>
            </table>
           <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
            <asp:Label ID="lblAppState" runat="server"></asp:Label>
    <br />
    <br />
    <br />
    <br />
    <br />

        </div>
    </div>
    
</asp:Content>

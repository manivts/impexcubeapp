<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MainMaster.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="DayBook.aspx.cs" Inherits="AccountsManagement.DayBook" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/Styles/AccountsStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
function validation() {
    var LedgerName = document.getElementById('<%=txtDate.ClientID%>').value;
        if (LedgerName == "") {
            alert("Please fill the Date ");
            return false;
            document.getElementById("txtDate").focus();
        }
        }
        </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    &nbsp;<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="a" runat="server">
    <ContentTemplate>
     <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
        <table align="center" style="width: 76%;" >
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Label ID="lblDayBook"  runat="server" 
                        Text="Day Book" CssClass="labeltitle"></asp:Label>
                </td>
            </tr>
            <tr>
            <td colspan="2"></td>
            </tr>
            
            <tr>
         
                <td style="text-align: left; width:50px;">
                    <asp:Label ID="lblDate" runat="server" Text="Date"  
                        CssClass="labelsize"></asp:Label>
                        </td>
                        <td>
                    <asp:TextBox ID="txtDate" runat="server" CssClass="txtbox100" ></asp:TextBox> 
                                
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate">
                    </cc1:CalendarExtender>
                    <%-- <cc1:filteredtextboxextender id="FTEDate" runat="server" filtertype="Numbers,Custom"
                    targetcontrolid="txtDate" >
                        </cc1:filteredtextboxextender>--%>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                    <asp:Button ID="btnGetRport" runat="server" Text="GetReport" OnClientClick="javascript:return validation();"
                        OnClick="btnGetRport_Click" CssClass="btn100" />
                    <asp:Button ID="btnExportExcel" runat="server" Text="ExportToExcel" 
                        OnClick="btnExportExcel_Click" CssClass="btn100" />
                    <asp:Button ID="btnExit" runat="server" Text="Exit" OnClick="btnExit_Click" 
                        CssClass="btn70" />
                </td>
            </tr>
        
    </table>
   
    </asp:Panel>
        <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Left">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblSales" runat="server"  Text="Sales Report" 
                        CssClass="labeltitle"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left">
                    <asp:GridView ID="gvDayBook_Sales" runat="server" Style="text-align: left" AutoGenerateColumns="true"
                        BorderColor="Black" BorderStyle="Solid" Font-Names="Arial" BorderWidth="1px"
                        Font-Size="10pt" ForeColor="Black" GridLines="Vertical" ShowFooter="True" ShowHeader="true"
                        Width="100%" AllowSorting="True" OnDataBound="gvDayBook_Sales_DataBound">
                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" BorderColor="Black" BorderStyle="Solid"
                            BorderWidth="1px" Width="350px" />
                        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                        <HeaderStyle BackColor="#2461BF" Font-Bold="True"  ForeColor="#E7E7FF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
                <tr>
                    <td>
                        <asp:Label ID="lblPayment" runat="server" Text="Payment Report" 
                            CssClass="labeltitle"></asp:Label>
                    </td>
                </tr>
            <tr>
                <td colspan="2" style="text-align: left">
                    <asp:GridView ID="gvDayBook_Payment" runat="server" Style="text-align: left" AutoGenerateColumns="true"
                        BorderColor="Black" BorderStyle="Solid" Font-Names="Arial" BorderWidth="1px"
                        Font-Size="10pt" ForeColor="Black" GridLines="Vertical" ShowFooter="True" ShowHeader="true"
                        Width="100%" AllowSorting="True" 
                        ondatabound="gvDayBook_Payment_DataBound" >
                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" BorderColor="Black" BorderStyle="Solid"
                            BorderWidth="1px" Width="350px" />
                        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                        <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="#E7E7FF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
                <tr>
                    <td>
                        <asp:Label ID="lblReceipt" runat="server" Text="Receipt Report" 
                            CssClass="labeltitle"></asp:Label>
                    </td>
                </tr>
            <tr>
                <td colspan="2" style="text-align: left">
                    <asp:GridView ID="gvDayBook_Receipt" runat="server" Style="text-align: left" AutoGenerateColumns="true"
                        BorderColor="Black" BorderStyle="Solid" Font-Names="Arial" BorderWidth="1px"
                        Font-Size="10pt" ForeColor="Black" GridLines="Vertical" ShowFooter="True" ShowHeader="true"
                        Width="100%" AllowSorting="True" OnDataBound="gvDayBook_Receipt_DataBound">
                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" BorderColor="Black" BorderStyle="Solid"
                            BorderWidth="1px" Width="350px" />
                        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                        <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="#E7E7FF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
                <tr>
                    <td>
                        <asp:Label ID="lblJournal" runat="server" Text="Journal Report" 
                            CssClass="labeltitle"></asp:Label>
                    </td>
                </tr>
            <tr>
                <td colspan="2" style="text-align: left">
                    <asp:GridView ID="gvDayBook_Journal" runat="server" Style="text-align: left" AutoGenerateColumns="true"
                        BorderColor="Black" BorderStyle="Solid" Font-Names="Arial" BorderWidth="1px"
                        Font-Size="10pt" ForeColor="Black" GridLines="Vertical" ShowFooter="True" ShowHeader="true"
                        Width="100%" AllowSorting="True" OnDataBound="gvDayBook_Journal_DataBound">
                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" BorderColor="Black" BorderStyle="Solid"
                            BorderWidth="1px" Width="350px" />
                        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                        <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="#E7E7FF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left">
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    </ContentTemplate>
    <Triggers>
    <asp:PostBackTrigger ControlID="btnExportExcel" />
    </Triggers>
    </asp:UpdatePanel>
   
</asp:Content>

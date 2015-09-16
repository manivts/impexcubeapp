<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmInvoiceDeliveryReport.aspx.cs" Inherits="ImpexCube.frmInvoiceDeliveryReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="50%">
        <tr>
            <td align="center" colspan="6">
            </td>
        </tr>
        <tr>
            <td align="center" class="fontsize">
                From Date
            </td>
            <td>
                <asp:TextBox ID="txtfromdate" runat="server" CssClass="textbox100"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfromdate"
                    Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
            </td>
            <td align="center" class="fontsize">
                To Date
            </td>
            <td>
                <asp:TextBox ID="txttodate" runat="server" CssClass="textbox100"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txttodate"
                    Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Generate"
                    Font-Size="10px" CssClass="stylebutton" />
            </td>
            <td>
                <asp:Button ID="btnexporttoexc" runat="server" OnClick="btnexporttoexc_Click" Text="Export To Excel"
                    Font-Size="10px" CssClass="stylebutton" />
            </td>
        </tr>
        <tr>
            <td colspan="6">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <div class="grid_scroll-2">
                    <asp:GridView ID="GridView2" runat="server" CssClass="gridview" AutoGenerateSelectButton="false"
                        AutoGenerateColumns="False" Width="800px">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField HeaderText="JobNo" DataField="JobNo" HeaderStyle-Font-Size="12px"
                                FooterStyle-Font-Size="8px"></asp:BoundField>
                            <asp:BoundField HeaderText="Importer Name" DataField="ImporterName" HeaderStyle-Font-Size="12px"
                                FooterStyle-Font-Size="8px"></asp:BoundField>
                            <asp:BoundField HeaderText="Mode" DataField="Mode" HeaderStyle-Font-Size="12px" FooterStyle-Font-Size="8px">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Load Type" DataField="LoadType" HeaderStyle-Font-Size="12px"
                                FooterStyle-Font-Size="8px"></asp:BoundField>
                            <asp:BoundField HeaderText="Job Status" DataField="JobStatus" HeaderStyle-Font-Size="12px"
                                FooterStyle-Font-Size="8px"></asp:BoundField>
                            <asp:BoundField HeaderText="Status Date" DataField="StatusDate" HeaderStyle-Font-Size="12px"
                                FooterStyle-Font-Size="8px"></asp:BoundField>
                            <asp:BoundField HeaderText="Invoice" DataField="invoice" HeaderStyle-Font-Size="12px"
                                FooterStyle-Font-Size="8px"></asp:BoundField>
                            <asp:BoundField HeaderText="Invoice Date" DataField="invoiceDate" HeaderStyle-Font-Size="12px"
                                FooterStyle-Font-Size="8px" ></asp:BoundField>
                        </Columns>
                        <RowStyle CssClass="table-header light" />
                        <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" />
                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                        <AlternatingRowStyle BackColor="#E7E7FF" />
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

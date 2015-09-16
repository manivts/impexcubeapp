<%@ Page Title="" Language="C#" MasterPageFile="~/OPReport/OperationReport.Master" AutoEventWireup="true" CodeBehind="frmPendingJobs.aspx.cs" Inherits="ImpexCube.OPReport.frmPendingJobs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 58px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="1000px">
    <tr>
    <td >
        <asp:Label ID="Label1" runat="server" Text="Party" CssClass="fontsize"></asp:Label></td>
    <td>
        <asp:TextBox ID="txtparty" runat="server" CssClass="textbox200"></asp:TextBox></td>
    <td><asp:Label ID="Label2" runat="server" Text="Trans Mode" CssClass="fontsize"></asp:Label></td>
    <td>
        <asp:DropDownList ID="ddltransmode" runat="server" CssClass="ddl100">
        </asp:DropDownList>
    </td>
    <td><asp:Label ID="Label3" runat="server" Text="Job Type" CssClass="fontsize"></asp:Label></td>
    <td><asp:DropDownList ID="ddljobtype" runat="server" CssClass="ddl100">
        </asp:DropDownList></td>
    </tr>
    <tr>
    <td ><asp:Label ID="Label4" runat="server" Text="Branch" CssClass="fontsize"></asp:Label></td>
    <td><asp:DropDownList ID="ddlbranch" runat="server" CssClass="ddl200">
        </asp:DropDownList></td>
    <td><asp:Label ID="Label5" runat="server" Text="Date" CssClass="fontsize"> </asp:Label></td>
    <td><asp:TextBox ID="txtdate" runat="server" CssClass="textbox200"></asp:TextBox></td>
    <td>
        <asp:Button ID="btngenerate" runat="server" Text="Generate" 
            CssClass="stylebutton" onclick="btngenerate_Click" /></td>
    <td><asp:Button ID="btnprint" runat="server" Text="Print" CssClass="stylebutton"/></td>
    <td><asp:Button ID="btrnsendmail" runat="server" Text="Send Mail" CssClass="stylebutton"/></td>
    </tr>
    <tr>
    <td ><asp:Label ID="Label6" runat="server" Text="Sort By" CssClass="fontsize"></asp:Label></td>
    <td><asp:DropDownList ID="ddlsortby" runat="server" CssClass="ddl100">
        </asp:DropDownList></td>
    </tr>
    <tr>
    <td colspan="8" >
        <asp:GridView ID="gvPendingJobs" runat="server" AutoGenerateColumns="False" 
                        CssClass="table-wraper"
                        Width="800px" Font-Size="Small" Height="100px" TabIndex="48">
                        <Columns>
                            <asp:BoundField HeaderText="Job No" DataField="JobNo" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Job Date" DataField="JobDate" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Type" DataField="JobReceivedOn" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Mode" DataField="TransportMode" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Party Name" DataField="CustomHouse" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="BE/SB No." DataField="Filling" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="BE/SB Date" DataField="Filling" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Invoice No" DataField="Filling" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Invoice Date" DataField="Filling" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Invoice Header" DataField="Filling" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Load Port" DataField="Filling" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Dest Port" DataField="Filling" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="No Of Packages" DataField="Filling" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Gross Wt" DataField="Filling" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Net Wt" DataField="Filling" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Pending for" DataField="Filling" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                        </Columns>
                        <RowStyle CssClass="table-header light" />
                        <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" />
                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                        <AlternatingRowStyle BackColor="#E7E7FF" />
                    </asp:GridView>
    </td>
    </tr>
    </table>
</asp:Content>

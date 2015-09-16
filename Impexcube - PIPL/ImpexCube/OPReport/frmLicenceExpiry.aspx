<%@ Page Title="" Language="C#" MasterPageFile="~/OPReport/OperationReport.Master" AutoEventWireup="true" CodeBehind="frmLicenceExpiry.aspx.cs" Inherits="ImpexCube.OPReport.frmLicenceExpiry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="850px">
    <tr>
    <td >
        <asp:Label ID="Label1" runat="server" Text="Licences Expiring in" CssClass="fontsize"></asp:Label></td>
    <td>
        <asp:TextBox ID="txtlicencesexpire" runat="server" CssClass="textbox200"></asp:TextBox>&nbsp;
        <asp:Label ID="Label2" runat="server" Text="Days" CssClass="fontsize"></asp:Label></td>
    <td>
        <asp:Button ID="btngenerate" runat="server" Text="Generate" 
            CssClass="stylebutton" onclick="btngenerate_Click"/></td>
    <td>
        <asp:Button ID="btnprint" runat="server" Text="Print" CssClass="stylebutton"/></td>
    <td>
        <asp:Button ID="btnsendmail" runat="server" Text="Send Mail" CssClass="stylebutton"/></td>
    </tr>
    <tr>
    <td colspan="5" >
        &nbsp;</td>
    </tr>
    <tr>
    <td colspan="5">
        <asp:GridView ID="gvLicenceExpiry" runat="server" AutoGenerateColumns="False" 
                        CssClass="table-wraper"
                        Width="800px" Font-Size="Small" Height="100px" TabIndex="48">
                        <Columns>
                            <asp:BoundField HeaderText="Lic No" DataField="JobNo" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Lic Date" DataField="JobDate" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Expiry Date" DataField="JobReceivedOn" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Owner" DataField="TransportMode" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="EDI Regn.No" DataField="CustomHouse" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Regn.Date" DataField="Filling" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Port Of Regn" DataField="Filling" HeaderStyle-HorizontalAlign="Center">
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

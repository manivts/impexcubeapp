<%@ Page Title="" Language="C#" MasterPageFile="~/OPReport/OperationReport.Master" AutoEventWireup="true" CodeBehind="frmLicenceList.aspx.cs" Inherits="ImpexCube.OPReport.frmLicenceList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width="750px">
<tr>
<td><asp:Label ID="Label1" runat="server" Text="Organization" CssClass="fontsize" 
        BackColor="White" ForeColor="#CC3300"></asp:Label></td>
<td>
    <asp:TextBox ID="txtorganization" runat="server" CssClass="textbox200"></asp:TextBox></td>
<td><asp:Label ID="Label2" runat="server" Text="Type" CssClass="fontsize"></asp:Label></td>
<td>
    <asp:DropDownList ID="ddltype" runat="server" CssClass="ddl200">
    </asp:DropDownList>
</td>
</tr>
<tr>
<td><asp:Label ID="Label3" runat="server" Text="Date" CssClass="fontsize"></asp:Label></td>
<td><asp:TextBox ID="txtdate" runat="server" CssClass="textbox200"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtdate">
                                                    </cc1:CalendarExtender></td>
    
<td><asp:Label ID="Label4" runat="server" Text="Custom House" CssClass="fontsize"></asp:Label></td>
<td> <asp:DropDownList ID="ddlcustomhouse" runat="server" CssClass="ddl200">
    </asp:DropDownList></td>
</tr>
<tr>
<td><asp:Label ID="Label5" runat="server" Text="Show" CssClass="fontsize"></asp:Label></td>
<td> <asp:DropDownList ID="ddlshow" runat="server" CssClass="ddl200">
    </asp:DropDownList></td>
<td colspan="2">
    <asp:Button ID="btngenerate" runat="server" Text="Generate" 
        CssClass="stylebutton" onclick="btngenerate_Click"/>&nbsp; <asp:Button ID="btnprint" runat="server" Text="Print" CssClass="stylebutton"/>&nbsp; <asp:Button ID="btnsendmail" runat="server" Text="Send Mail" CssClass="stylebutton"/></td>
</tr>
<tr>
<td colspan="4">&nbsp;</td>
</tr>
<tr>
<td colspan="4">
<asp:GridView ID="gvLicenceList" runat="server" AutoGenerateColumns="False" 
                        CssClass="table-wraper"
                        Width="800px" Font-Size="Small" Height="100px" TabIndex="48">
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True" />
                                                <%--<asp:BoundField HeaderText="Sr.No" DataField="Sr.No"></asp:BoundField>--%>
                                                <asp:BoundField HeaderText="Lic No" DataField="InvoiceNo" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Lic Date" DataField="InvoiceDate" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Expiry Date" DataField="TOI" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Type" DataField="Currency" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="EDI Regn.No" DataField="CurrencyRate" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Regn.Date" DataField="InvoiceValue" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Port Of Regn." DataField="ProductValue" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Avail Value" DataField="InvoiceINRAmount" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Used Value" DataField="InvoiceINRAmount" HeaderStyle-HorizontalAlign="Center">
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

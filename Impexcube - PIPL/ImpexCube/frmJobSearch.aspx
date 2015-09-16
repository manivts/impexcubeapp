<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="frmJobSearch.aspx.cs" Inherits="ImpexCube.frmImpJobSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:71%;">
                <tr>
                    <td align="right" style="text-align: center">
                        <asp:Label ID="lblkeyword" runat="server" Text="Keyword"></asp:Label>
                        <asp:DropDownList ID="ddlkeyword" runat="server" CssClass="ddl200" AppendDataBoundItems="true">
                            <asp:ListItem>~Select~</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="txtkeyword" runat="server" Width="300px"></asp:TextBox>
                        <asp:Button ID="btnsearch" runat="server" onclick="btnsearch_Click1" 
                            Text="Search" />
                    </td>
                </tr>
                <tr>
                    <td >
                        <table style="width:100%;">
                            <tr>
                                <td style="text-align: center">

                                 <div  runat="server" style="height: 300px; width: 850px; overflow: auto;">
                                    <asp:GridView ID="gvsearch" runat="server" AutoGenerateColumns="False" 
                                        CssClass="table-wrapper" Font-Size="9pt" GridLines="None" Width="800px">
                                        <RowStyle CssClass="table-header light" Font-Size="9pt" />
                                        <Columns>
                                            <asp:BoundField DataField="JobNo" HeaderText="Job No" />
                                        </Columns>
                                        <Columns>
                                            <asp:BoundField DataField="JobReceivedDate" HeaderText="Job Date" />
                                        </Columns>
                                        <Columns>
                                            <asp:BoundField DataField="Importer" HeaderText="Importer Name" />
                                        </Columns>
                                        <HeaderStyle BackColor="#E7E7FF" CssClass="table-row" Font-Size="9pt" />
                                        <PagerStyle BackColor="#C6C3C6" Font-Size="9pt" ForeColor="Black" 
                                            HorizontalAlign="Right" />
                                        <AlternatingRowStyle BackColor="#E7E7FF" Font-Size="9pt" />
                                    </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <br />
</asp:Content>

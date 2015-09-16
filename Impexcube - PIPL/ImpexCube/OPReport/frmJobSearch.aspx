<%@ Page Title="" Language="C#" MasterPageFile="~/OPReport/OperationReport.Master" AutoEventWireup="true" CodeBehind="frmJobSearch.aspx.cs" Inherits="ImpexCube.OPReport.frmJobSearch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 797px">
    <tr>
    <td>
        <asp:Label ID="Label1" runat="server" Text="Select" CssClass="fontsize"></asp:Label>
    </td>
    <td>
        <asp:DropDownList ID="ddlJobSearch" runat="server" CssClass="ddl200" 
            onselectedindexchanged="ddlJobSearch_SelectedIndexChanged" 
            AutoPostBack="True">
        </asp:DropDownList>
    </td>
    </tr>
    <tr>
    <td>        
       <asp:Label ID="lbljobsearch" runat="server" Text="Label" CssClass="fontsize"></asp:Label></td>
       <td>
           <asp:TextBox ID="txtjobsearch" runat="server" CssClass="textbox200"></asp:TextBox></td>
    </tr>
    </table>
    <table>
    <tr><td>
        <asp:Panel ID="Panel1" runat="server">       
    <tr>
    <td colspan="2">
        <asp:Label ID="Label3" runat="server" Text="From" CssClass="fontsize"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
            <asp:TextBox ID="txtfrom" runat="server" CssClass="textbox200"></asp:TextBox><cc1:CalendarExtender ID="cclExdate" TargetControlID="txtfrom" runat="server"
                                                        Format="dd/MM/yyyy">
                                                    </cc1:CalendarExtender>&nbsp;
        <asp:Label ID="Label4" runat="server" Text="To" CssClass="fontsize"></asp:Label>&nbsp;
            <asp:TextBox ID="txtto" runat="server" CssClass="textbox200"></asp:TextBox><cc1:CalendarExtender ID="cclExdate1" TargetControlID="txtto" runat="server"
                                                        Format="dd/MM/yyyy">
                                                    </cc1:CalendarExtender></td>
    </tr>
     </asp:Panel>
    <tr>
    <td align="left" colspan="2">
        <asp:Button ID="btngenerate" runat="server" Text="Generate" 
            CssClass="stylebutton" onclick="btngenerate_Click"/></td>
    </tr>
    <tr>
    <td colspan="2"> <asp:GridView ID="gvJobSearch" runat="server" AutoGenerateColumns="true" 
                        CssClass="table-wraper"
                        Width="800px" Font-Size="Small" Height="100px" TabIndex="48">
                        <Columns>
                            
                        </Columns>
                        <RowStyle CssClass="table-header light" />
                        <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" />
                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                        <AlternatingRowStyle BackColor="#E7E7FF" />
                    </asp:GridView></td>
    </tr>
    </td></tr>
    </table>
</asp:Content>

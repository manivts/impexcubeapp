<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" 
CodeBehind="frmDeveloperGuideReport.aspx.cs" Inherits="ImpexCube.frmDeveloperGuideReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        $('#<%=gvdevelopers.ClientID %>').Scrollable();
    }
)
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width:91%;">
        <tr>
            <td align="left" colspan="1" rowspan="1" width="200px">
                <asp:Label ID="Label1" runat="server" Text="Developer Name" Width="110px"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddldevelopername" runat="server" AutoPostBack="True" 
                    CssClass="ddl200" 
                    onselectedindexchanged="ddldevelopername_SelectedIndexChanged">
                    <asp:ListItem>ALL</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="1" rowspan="1" width="200px">
                <asp:Label ID="lblmodulename" runat="server" Text="Module Name :"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlmodulename" runat="server" AutoPostBack="True" 
                    CssClass="ddl200" AppendDataBoundItems="true" 
                    onselectedindexchanged="ddlmodulename_SelectedIndexChanged">
                    <asp:ListItem>ALL</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="1" rowspan="1" width="200px">
                <asp:Label ID="lblformname" runat="server" Text="Form Name :"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlfromname" runat="server" AutoPostBack="True" 
                    CssClass="ddl200">
                    <asp:ListItem>ALL</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="1" rowspan="1" width="200px">
                <asp:Label ID="lblfromdate" runat="server" Text="From Date"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp; </td>
            <td>
                <asp:TextBox ID="txtfromdate" runat="server" 
                    OnKeyPress="javascript:return false;" CssClass="textbox150"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfromdate"
                        Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lbltodate" runat="server" Text="To Date"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txttodate" runat="server" 
                    OnKeyPress="javascript:return false;" CssClass="textbox150"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txttodate"
                        Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td align="right" width="200px">
                &nbsp;</td>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btngetreport" runat="server" Text="Get Report" 
                    onclick="btngetreport_Click" />
            &nbsp;<asp:Button ID="btnexport" runat="server" Text="Export To Excel" 
                    onclick="btnexport_Click" style="height: 26px" />
            </td>
        </tr>
        <tr>
            <td align="left" width="200px">
                &nbsp;</td>
            <td>
                <asp:GridView ID="gvdevelopers" runat="server" Style="text-align: center; margin-top:10px" GridLines="Vertical"
                            CssClass="table-wrapper" AutoGenerateColumns="False"
                            Width="900px">
                <RowStyle CssClass="table-header light" />
                            <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" ForeColor="#EE2521" />
                            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                            <AlternatingRowStyle BackColor="#E7E7FF" />
                            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                            


                <Columns>
                                
                                <asp:BoundField DataField="transid" HeaderText="TransId" />
                                <asp:BoundField DataField="TransDate" HeaderText="Date" />
                                <asp:BoundField DataField="ModuleName" HeaderText="ModuleName" />
                                <asp:BoundField DataField="formname" HeaderText="FormName" />
                                <asp:BoundField DataField="description" HeaderText="Description" />
                                <asp:BoundField DataField="developername" HeaderText="Developers Name" />
                            </Columns>




                </asp:GridView>
            </td>
        </tr>
    </table>
    <br />
</asp:Content>

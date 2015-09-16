<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="frmUserNotificationList.aspx.cs" Inherits="ImpexCube.frmUserNotificationList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script type="text/javascript" src="Content/Scripts/script.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <table style="width: 71%;">
        <tr>
            <td colspan="4" style="text-align: center">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="fontsize">
                <asp:Label ID="Label5" runat="server" Text="Duty Name"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDutyName" runat="server" CssClass="textbox150">
                    <asp:ListItem>BCD</asp:ListItem>
                    <asp:ListItem>CVD</asp:ListItem>
                    <asp:ListItem>SAD</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="fontsize">
                <asp:Label ID="Label1" runat="server" Text="Chapter No"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlchaterno" runat="server" CssClass="textbox150">
                    <asp:ListItem>All</asp:ListItem>
                    <asp:ListItem>84</asp:ListItem>
                    <asp:ListItem>85</asp:ListItem>
                    <asp:ListItem>40</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="fontsize">
                <asp:Label ID="Label2" runat="server" Text="Notification"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtnot" runat="server" CssClass="textbox150"></asp:TextBox>
            </td>
            <td class="fontsize">
                <asp:Label ID="Label3" runat="server" Text="Serial No"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtseralno" runat="server" CssClass="textbox150"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="fontsize">
                <asp:Label ID="Label4" runat="server" Text="Duty"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtduty" runat="server" CssClass="textbox150" OnKeyPress="javascript:return isFloat(event);"></asp:TextBox>
            </td>
            <td class="fontsize">
                <asp:Label ID="Label6" runat="server" Text="Remarks"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtremarks" runat="server" CssClass="textbox150"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="fontsize">
                <asp:Label ID="Label8" runat="server" Text="Effective Date"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txteffdate" runat="server" CssClass="textbox150"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txteffdate"
                    Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
            </td>
            <td colspan="2">
                <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="stylebutton" 
                    onclick="btnsave_Click" />
                <asp:Button ID="btnUdate" runat="server" Text="Update" CssClass="stylebutton" 
                    onclick="btnUdate_Click" />
                     <asp:Button ID="btnNew" runat="server" Text="New" CssClass="stylebutton" onclick="btnNew_Click" 
                    />
                    </td>
        </tr>
        <tr>
            <td class="fontsize">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="fontsize">
                <asp:Label ID="lblkeyfiled" runat="server" Text="Key Field"></asp:Label></td>
            <td>
               <asp:TextBox ID="txtkeyfield" runat="server" CssClass="textbox150"></asp:TextBox></td>
            <td>
               <asp:Button ID="txtsearch" runat="server" Text="Search" CssClass="stylebutton" 
                     /></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvuserNotify" runat="server" AutoGenerateColumns="false" 
                    CssClass="table-wrapperInv"
                                             BorderColor="Black" 
                    BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="calibri" Font-Size="10pt" 
                    ForeColor="Black" GridLines="Vertical"
                                            ShowFooter="false" ShowHeader="true" 
                    Style="text-align: center; font-size: 9pt;" Width="750px" 
                    onselectedindexchanged="gvuserNotify_SelectedIndexChanged">
                    <Columns>                   
                        <asp:CommandField ShowSelectButton="True" />
                         <asp:BoundField DataField="TransId" HeaderText="TransID" 
                            ItemStyle-HorizontalAlign="Center" SortExpression="TransID" />
                        <asp:BoundField DataField="DutyName" HeaderText="Duty Name" 
                            ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="ChapterNo" HeaderText="ChaPter No" 
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Notification" HeaderText="Notification" 
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SerialNo" HeaderText="Serial No" 
                            ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Duty" HeaderText="Duty" 
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" 
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="EffectiveDate" HeaderText="Effective Date" 
                            ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <RowStyle CssClass="table-header light" />
                    <HeaderStyle BackColor="#E7E7FF" CssClass="table-row" />
                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                    <AlternatingRowStyle BackColor="#E7E7FF" />
                </asp:GridView></td>
        </tr>
        
        <tr>
            <td colspan="4">
                &nbsp;</td>
        </tr>
        
    </table>
</asp:Content>

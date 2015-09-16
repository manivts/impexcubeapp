<%@ Page Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" Inherits="ImpexCube.Dashboard_frmOutwardUpdate" Title=" :: Front Desk || OUTWARD BILL UPDATE" Codebehind="frmOutwardUpdate.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%;">
        <tr style="background-color: #719ddb;">
            <td align="center" colspan="4">
                <asp:Label ID="Label2" Font-Names="calibri" Font-Size="12pt" runat="server" Text="Outward Update Information"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="8pt" Text="AirWay Bill No:"></asp:Label>
            </td>
            <td style="width: 199px">
                <asp:TextBox ID="txtAWBNo" Font-Names="arial" Font-Size="8pt" runat="server" Width="130px"
                    Style="margin-left: 0px"></asp:TextBox>
                <asp:Button ID="BtnSearch" runat="server" Font-Names="arial" Font-Size="8pt" 
                    Text="Search" onclick="BtnSearch_Click" />
            </td>
            <td style="width: 99px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label30" Font-Names="arial" Font-Size="8pt" runat="server" Text="Outward Date :"></asp:Label>
            </td>
            <td style="width: 199px">
                <asp:TextBox ID="txtDate" Font-Names="arial" Font-Size="8pt" runat="server" Width="200px"></asp:TextBox>
                
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 124px">
                <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="8pt" Text="Consignee Name:"></asp:Label>
            </td>
            <td style="width: 199px">
                <asp:TextBox ID="txtConsignee" Font-Names="arial" Font-Size="8pt" runat="server"
                    Width="200px"></asp:TextBox>
            </td>
            <td align="right" style="width: 99px">
                <asp:Label ID="Label5" runat="server" Font-Names="arial" Font-Size="8pt" Text="Job No :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtJobs" runat="server" Font-Names="arial" Font-Size="8pt" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 124px">
                <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="8pt" Text="City:"></asp:Label>
            </td>
            <td style="width: 199px">
                <asp:TextBox ID="txtCity" runat="server" Font-Names="arial" Font-Size="8pt" Width="200px"></asp:TextBox>
            </td>
            <td align="right" style="width: 99px">
                <asp:Label ID="Label6" runat="server" Font-Names="arial" Font-Size="8pt" Text="Sent By:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSentBy" runat="server" Font-Names="arial" Font-Size="8pt" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
         <td align="right" style="vertical-align: top; ">
                <asp:Label ID="Label9" Font-Names="arial" Font-Size="8pt" runat="server" Text="Document Details :"
                    Width="100px"></asp:Label>
            </td>
            <td style="vertical-align: top;">
                <asp:TextBox ID="txtDDetails" Font-Names="arial" Font-Size="8pt" Height="80px" runat="server"
                    Width="200px" TextMode="MultiLine"></asp:TextBox>
            </td>
            <td align="right" style="vertical-align: top;">
                <asp:Label ID="Label7" runat="server" Font-Names="arial" Font-Size="8pt" Text="if any Remarks:"></asp:Label>
            </td>
            <td >
                <asp:TextBox ID="txtRmks" runat="server" Font-Names="arial" Font-Size="8pt" Height="80px"
                    TextMode="MultiLine" Width="200px"></asp:TextBox>
            </td>
           
        </tr>
        <tr style="background-color: #719bdf;">
            <td colspan="4">
                <asp:Label ID="Label31" runat="server" Font-Bold="True" Font-Names="Arial" 
                    Font-Size="8pt" ForeColor="White" Text="Sent Info : -"></asp:Label>
            </td>
        </tr>
        <tr>
        <td align="right" >
                <asp:Label ID="Label10" runat="server" Font-Names="arial" Font-Size="8pt" Text="Received Date:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtRecDate" runat="server" Font-Names="arial" Font-Size="8pt" Width="200px"></asp:TextBox>
                <cc1:CalendarExtender ID="CE1" runat="server" TargetControlID="txtRecDate"  Format="dd/MM/yyyy">
    </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 124px">
                <asp:Label ID="Label8" runat="server" Font-Names="arial" Font-Size="8pt" Text="Received By:"></asp:Label>
            </td>
            <td style="width: 199px">
                <asp:TextBox ID="txtRecBy" runat="server" Font-Names="arial" Font-Size="8pt" Width="200px"></asp:TextBox>
            </td>
            
        </tr>
        <tr>
        <td align="right" style="vertical-align: top;" >
        <asp:Label ID="Label11" runat="server" Font-Names="arial" Font-Size="8pt" Text="Remarks:"></asp:Label>
        </td>
            <td colspan="3" align="left" style="width: 124px">
                
            
                <asp:TextBox ID="txtRecRmks" runat="server" Font-Names="arial" Font-Size="8pt" 
                    Width="300px" Height="50px" TextMode="MultiLine"></asp:TextBox>
            </td>
            
        </tr>
        <tr>
        <td></td>
        <td>
            <asp:Button ID="btnSubmit" runat="server" Font-Names="verdana" Font-Size="9pt" 
                Text="Save" Height="25px" Width="80px" onclick="btnSubmit_Click" />
            <asp:Button ID="btnSubmit0" runat="server" Font-Names="verdana" Font-Size="9pt" 
                Text="Cancel" Height="25px" Width="80px" 
                PostBackUrl="~/Dashboard/frmDashboardMain.aspx" />
        </td>
        </tr>
    </table>
</asp:Content>

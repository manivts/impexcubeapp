<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="efrmJobCreation.aspx.cs" Inherits="ImpexCube.efrmJobCreation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table>
        <tr>
            <td colspan="6" align="center">
                <asp:Label ID="lblJobCreation" runat="server" Text="Job Creation" Style="font-weight: 700;
                    font-family: Arial, Helvetica, sans-serif; font-size: 14px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="6" align="center" style="color: #008080; font-style: italic; font-weight: bold;
                font-size: large">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td width="80px">
                <asp:Label ID="lblJobNumber" runat="server" Text="Job No." CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblJobCode" runat="server" Text="" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblJobDate" runat="server" Text="Job Received Date" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtJobDate" runat="server" CssClass="textbox150"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtJobDate"
                    Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
            </td>
            <td>
                <asp:Label ID="lblJobReceived" runat="server" Text="Job Received On" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtJobReceived" runat="server" CssClass="textbox150"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtJobReceived"
                    Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTransportMode" runat="server" Text="Mode" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlTransportMode" runat="server" Width="140px" CssClass="ddl150">
                    <asp:ListItem Text="~Select~" Value="~Select~"></asp:ListItem>
                    <asp:ListItem Text="Air" Value="Air"></asp:ListItem>
                    <asp:ListItem Text="Sea" Value="Sea"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblCustomHouse" runat="server" Text="Custom" CssClass="fontsize"></asp:Label>
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlCustomHouse" runat="server" CssClass="ddl156" OnSelectedIndexChanged="ddlCustomHouse_SelectedIndexChanged"
                    AppendDataBoundItems="True">
                    <asp:ListItem Text="~Select~" Value="~Select~"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblFilling" runat="server" Text="Filling" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlFilling" runat="server" Width="140px" CssClass="ddl150">
                    <asp:ListItem>Online</asp:ListItem>
                    <asp:ListItem Selected="True">Manual</asp:ListItem>
                    <asp:ListItem>E-mail</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td colspan="4" align="right">
                <asp:Button ID="btnStandardDocument" runat="server" Text="Standard Document" 
                    CssClass="stylebutton" Visible="False" />
            </td>
        </tr>
        <tr>
            <td colspan="6" align="center">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="stylebutton" OnClick="btnSave_Click" />
                &nbsp;
                <asp:Button ID="btnUpdate" runat="server" CssClass="stylebutton" Text="Update" OnClick="btnUpdate_Click" />
                &nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="stylebutton" OnClick="btnCancel_Click" />
                &nbsp;
                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="stylebutton" OnClick="btnClose_Click" />
                &nbsp;
                <asp:Button ID="bntForward" runat="server" Text="Go to Exporter" 
                    CssClass="stylebutton" onclick="bntForward_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="6" align="center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <div class="grid_scroll-2">
                    <asp:GridView ID="gvJobCreation" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True"
                        OnSelectedIndexChanged="gvJobCreation_SelectedIndexChanged" CssClass="table-wraper"
                        Width="800" Font-Size="Small">
                        <Columns>
                            <asp:BoundField HeaderText="Job Number" DataField="JobNo" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Job Date" DataField="JobDate" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Job Received On" DataField="JobReceivedOn" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Transport Mode" DataField="TransportMode" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Custom House" DataField="CustomHouse" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Filling" DataField="Filling" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
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

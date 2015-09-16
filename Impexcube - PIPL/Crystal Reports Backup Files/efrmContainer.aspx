<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="efrmContainer.aspx.cs" Inherits="ImpexCube.efrmContainer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .ColHidden
        {
            display: none;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table>
        <tr>
            <td>
            <td colspan="4" align="center" style="color: #008080; font-style: italic; font-weight: bold;
                font-size: large">
                <asp:Label ID="lblContainer" runat="server" Text="Container Details"></asp:Label>
                &nbsp;
            </td>
            <td align="center" style="color: #008080; font-style: italic; font-weight: bold;
                font-size: large" rowspan="6">
                <table border="0.5" style="border-width: 1px; border-style: solid">
                    <tr>
                        <td align="center" colspan="2">
                            &nbsp; &nbsp; &nbsp; Job Details
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize">
                            Job No
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlJobNoContainer" runat="server" AppendDataBoundItems="True"
                                AutoPostBack="True" CssClass="ddl100" Height="20px" Width="130px" OnSelectedIndexChanged="ddlJobNoContainer_SelectedIndexChanged">
                                <asp:ListItem>~Select~</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize">
                            Job Date
                        </td>
                        <td>
                            <asp:Label ID="lblJobDate" runat="server" CssClass="arealaber1a"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize">
                            Currency
                        </td>
                        <td>
                            <asp:Label ID="lblCurrency" runat="server" CssClass="arealaber1a"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize">
                            ExRate
                        </td>
                        <td>
                            <asp:Label ID="lblExRate" runat="server" CssClass="arealaber1a"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize">
                            Inv No
                        </td>
                        <td>
                            <asp:Label ID="lblInvNo" runat="server" CssClass="arealaber1a"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize">
                            Inv Value
                        </td>
                        <td>
                            <asp:Label ID="lblInvValue" runat="server" CssClass="arealaber1a"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize">
                            Mode
                        </td>
                        <td>
                            <asp:Label ID="lblMode" runat="server" CssClass="arealaber1a"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize">
                            Custom
                        </td>
                        <td>
                            <asp:Label ID="lblCustom" runat="server" CssClass="arealaber1a"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize">
                            Doc Flling Status
                        </td>
                        <td>
                            <asp:Label ID="lblStatus" runat="server" CssClass="arealaber1a"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize">
                            SB No
                        </td>
                        <td>
                            <asp:Label ID="lblBeNo" runat="server" CssClass="arealaber1a"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize">
                            SB Date
                        </td>
                        <td>
                            <asp:Label ID="lblBeDate" runat="server" CssClass="arealaber1a"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize">
                            Job Approved By
                        </td>
                        <td>
                            <asp:Label ID="lblApprovedBy" runat="server" CssClass="arealaber1a"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize">
                            Duty Payment Dt
                        </td>
                        <td>
                            <asp:Label ID="lblPaymentDate" runat="server" CssClass="arealaber1a"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize">
                            Overseas Date
                        </td>
                        <td>
                            <asp:Label ID="lblOverseasDate" runat="server" CssClass="arealaber1a"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblmsg" runat="server" CssClass="fontsize" Style="font-weight: 700"></asp:Label>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="btnReturn" runat="server" CssClass="stylebutton" Text="Back To Exporter Details"
                                Width="134px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="5" align="left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblContainerNo" runat="server" Text="Container No" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSealNo" runat="server" Text="Seal No" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSealDate" runat="server" Text="Seal Date" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblType" runat="server" Text="Type" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblPktsStuffed" runat="server" Text="No of Pkts Stuffed" CssClass="fontsize"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtContainerNo" runat="server" CssClass="textbox150"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtSealNo" runat="server" CssClass="textbox150"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtSealDate" runat="server" CssClass="textbox150"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSealDate"
                    Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
            </td>
            <td>
                <asp:TextBox ID="txtType" runat="server" CssClass="textbox150"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtPktsStuffed" runat="server" CssClass="textbox150"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="5" align="right">
                <asp:Button ID="btnAdd" runat="server" Text="ADD" CssClass="masterbutton" OnClick="btnAdd_Click" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="masterbutton" OnClick="btnUpdate_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="masterbutton" 
                    onclick="btnCancel_Click" />
                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="masterbutton" 
                    onclick="btnClose_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <div class="grid_scroll-2">
                    <asp:GridView ID="gvContainer" runat="server" CssClass="table-wrapper" AutoGenerateColumns="False"
                        OnSelectedIndexChanged="gvContainer_SelectedIndexChanged" AutoGenerateSelectButton="True">
                        <Columns>
                            <asp:BoundField HeaderText="ID" DataField="ID" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-CssClass="ColHidden" ItemStyle-CssClass="ColHidden"></asp:BoundField>
                            <asp:BoundField HeaderText="Container No" DataField="ContainerNo" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Seal NO" DataField="SealNO" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Seal Date " DataField="SealDate" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Type" DataField="Type" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="No of Pkts Stuffed" DataField="NoofPktsStuffed" HeaderStyle-HorizontalAlign="Center">
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
        </td> </tr>
    </table>
</asp:Content>

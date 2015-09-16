<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmDetentionListJobNo.aspx.cs" Inherits="ImpexCube.frmDetentionListJobNo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="background-color: White;">
        <tr>
            <td>
                &nbsp;<table>
                    <tr>
                        <td style="width: 655px; background-color: #ccccff;">
                            <center>
                                <asp:Label ID="Label35" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="12pt"
                                    Text="Ground Rent & Detention Reports"></asp:Label></center>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%; height: 54px; border-color: #2461BF; border-style: solid;
                                border-width: 2px;">
                                <tr>
                                    <td align="left" style="width: 30px; height: 250px; vertical-align: top;">
                                        <asp:Label ID="lbllistHead" runat="server" Text="Data Field" Width="126px" BackColor="#2461BF"
                                            Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White"></asp:Label>
                                        <asp:ListBox ID="lstShowField" runat="server" Height="229px" Width="122px" SelectionMode="Multiple"
                                            EnableTheming="True" Font-Names="Arial" Font-Size="7pt"></asp:ListBox>
                                        &nbsp;
                                    </td>
                                    <td style="width: 24px; height: 250px;">
                                        <asp:Button ID="BtnAdd" runat="server" Text=">" Width="42px" Height="24px" OnClick="BtnAdd_Click" /><br />
                                        <br />
                                        <asp:Button ID="BtnAddAll" runat="server" Text=">>" Width="42px" Height="24px" OnClick="BtnAddAll_Click" /><br />
                                        <br />
                                        <asp:Button ID="BtnRemove" runat="server" Text="<" Width="42px" Height="24px" OnClick="BtnRemove_Click" />
                                        <br />
                                        <br />
                                        <asp:Button ID="BtnRemoveAll" runat="server" Text="<<" Width="42px" Height="24px"
                                            OnClick="BtnRemoveAll_Click" />
                                    </td>
                                    <td align="left" style="width: 86px; height: 250px; vertical-align: top;">
                                        <asp:Label ID="lbllistHead1" runat="server" Text="Custom Field" BackColor="#2461BF"
                                            Font-Bold="True" Width="124px" Font-Names="Verdana" Font-Size="8pt" ForeColor="White"></asp:Label>
                                        <asp:ListBox ID="lstView" runat="server" Height="229px" Width="124px" SelectionMode="Multiple"
                                            Rows="1" Font-Names="Arial" Font-Size="7pt"></asp:ListBox>
                                    </td>
                                    <td align="center" style="height: 250px; width: 76px; vertical-align: middle;">
                                        <asp:Button ID="BtnSearch" runat="server" Text="Search" Height="28px" Width="98px"
                                            Font-Names="Arial" Font-Size="8pt" OnClick="BtnSearch_Click" /><br />
                                        &nbsp;
                                        <asp:Button ID="BtnCancel" runat="server" Font-Names="Arial" Font-Size="9pt" OnClick="BtnCancel_Click"
                                            Text="Cancel" CausesValidation="False" Height="28px" Width="98px" /><br />
                                        <br />
                                        <asp:Button ID="ExportPage" runat="server" Font-Names="Arial" Font-Size="8pt" Height="28px"
                                            OnClick="ExportPage_Click" Text="Export to Excel" Width="98px" />
                                    </td>
                                    <td style="width: 438px">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 655px; height: 154px;">
                            <asp:GridView ID="GridView1" runat="server" CellPadding="4" Font-Names="Arial" Font-Size="8pt"
                                OnRowDataBound="GridView1_RowDataBound" BackColor="White" BorderColor="#E0E0E0"
                                BorderStyle="Solid" BorderWidth="1px">
                                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                <RowStyle BackColor="White" ForeColor="Black" />
                                <EditRowStyle ForeColor="Black" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" Wrap="False"
                                    HorizontalAlign="Left" />
                                <AlternatingRowStyle ForeColor="Black" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

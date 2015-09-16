<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmMasterImporter.aspx.cs" Inherits="ImpexCube.frmMasterImporter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Content/Styles/Accordion.css" />
    <link rel="stylesheet" type="text/css" href="Content/Styles/jquery-ui.css" />
    <script type="text/javascript" src="Content/Scripts/Accordion.js"></script>
    <script type="text/javascript" src="Content/Scripts/jquery-ui.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="width100">
        <table class="table100">
            <tr>
                <td class="center" colspan="4">
                    <asp:Label ID="Label1" runat="server" Text="Importer Master"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" CssClass="fontsize" Text="Importer"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox40"></asp:TextBox>
                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="ddl400">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" CssClass="fontsize" Text="Group"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="ddl150">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" CssClass="fontsize" Text="Remarks"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox400" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="center" colspan="4">
                    <asp:Label ID="Label5" runat="server" Text="Address" CssClass="accordion_toggle1"></asp:Label>
                    <div id="accordion">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmDocumentMaster.aspx.cs" Inherits="ImpexCube.frmDocumentMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function valsave() {
            if (document.getElementById('ContentPlaceHolder1_txtDocumentName').value == "") {
                alert('Please Enter The Document Name');
                return false;
            }
        }

        function exit() {
            var status = confirm("Do You Want To Exit!");
            if (status == true) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <div style="text-align: center">
        <table align="center">
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="lblDocumentMaster" runat="server" align="center" Style="color: #008080;
                        font-style: italic; font-weight: bold; font-size: large" Text="Document Master"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td class="fontsize">
                    Document Name
                    <asp:TextBox ID="txtDocumentName" runat="server" CssClass="textbox150"></asp:TextBox><font
                        color="red"><strong>*</strong></font>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Button ID="btnNew" runat="server" CssClass="masterbutton" Text="New" OnClick="btnNew_Click"
                        OnClientClick="javascript:return validate();" />
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" OnClientClick="javascript:return valsave();"
                        CssClass="masterbutton" />
                    <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                        Visible="False" OnClientClick="return confirm('Do you want to Update');" CssClass="masterbutton" />
                    <asp:Button ID="btnExit" runat="server" Text="Exit" OnClick="btnExit_Click" CssClass="masterbutton"
                        OnClientClick="javascript:return exit();" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left" class="style1" align="center">
                    <div class="grid_scroll-2">
                        <asp:GridView ID="gvDocument" runat="server" CssClass="table-wrapper" AutoGenerateSelectButton="True"
                            OnSelectedIndexChanged="gvDocument_SelectedIndexChanged" AutoGenerateColumns="False"
                            Width="500px">
                            <Columns>
                                <asp:BoundField HeaderText="Id" DataField="ID" HeaderStyle-HorizontalAlign="Center">
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Document Name" HeaderStyle-HorizontalAlign="Center" DataField="DocumentName">
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
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style1
        {
            height: 247px;
        }
        .style2
        {
            width: 220px;
        }
    </style>
</asp:Content>

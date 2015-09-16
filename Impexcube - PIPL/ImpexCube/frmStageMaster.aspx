﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmStageMaster.aspx.cs" Inherits="ImpexCube.frmStageMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function exit() {
            var status = confirm("Do You Want To Exit!");
            if (status == true) {
                return true;
            }
            else {
                return false;
            }
        }

        function valsave() {
            if (document.getElementById('ContentPlaceHolder1_txtStageName').value == "") {
                alert('Please Enter The Stage Name');
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Label ID="lblStage" align="center" Style="color: #008080; font-style: italic;
                    font-weight: bold; font-size: large" runat="server" Text="Stage Master"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" class="fontsize">
                Stage Id :
            </td>
            <td>
                <asp:TextBox ID="txtStageId" CssClass="textbox150" runat="server" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" class="fontsize">
                Stage Name :
            </td>
            <td>
                <asp:TextBox ID="txtStageName" CssClass="textbox150" runat="server"></asp:TextBox><font
                    color="red"><strong>*</strong></font>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnNew" runat="server" CssClass="masterbutton" Text="New" OnClick="btnNew_Click" />
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="masterbutton" OnClick="btnSave_Click"
                    OnClientClick="javascript:return valsave();"></asp:Button>
                <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="masterbutton" OnClick="btnExit_Click"
                    OnClientClick="javascript:return exit();"></asp:Button>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div class="grid_scroll-2" align="center" style="height: 250px">
                    <asp:GridView ID="gvStageDetails" runat="server" CssClass="table-wrapper" AutoGenerateSelectButton="True"
                        OnSelectedIndexChanged="gvStageDetails_SelectedIndexChanged" AutoGenerateColumns="False"
                        Width="400px">
                        <Columns>
                            <asp:BoundField DataField="StageId" HeaderText="StageId" InsertVisible="False" ReadOnly="True"
                                SortExpression="StageId" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Stage" HeaderText="Stage" HeaderStyle-HorizontalAlign="Center"
                                SortExpression="Stage" />
                        </Columns>
                        <RowStyle CssClass="table-header light" />
                        <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" />
                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                        <AlternatingRowStyle BackColor="#E7E7FF" />
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td colspan="2" class="style12">
            </td>
        </tr>
    </table>
</asp:Content>

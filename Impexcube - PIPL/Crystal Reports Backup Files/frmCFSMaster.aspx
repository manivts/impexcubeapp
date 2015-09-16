<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmCFSMaster.aspx.cs" Inherits="ImpexCube.frmCFSMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function validate() {
        var cfsname = document.getElementById("<%= txtCfsName.ClientID %>").value;
        if (cfsname == "") {
            alert('Please Enter CFS Name');
            document.getElementById("<%= txtCfsName.ClientID %>").focus();
            return false;
        }
        return true;
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table>
            <tr>
                <td class="center" colspan="2" style="color: #008080; font-style: italic; font-weight: bold;
                        font-size: large">
                    CFS Master
                </td>
            </tr>
            <tr>
                <td colspan="2"> </td>
            </tr>
            <tr>
                <td style="width: 100px;">
                    <asp:Label ID="lblCfsName" runat="server" Text="CFS Name" CssClass="fontsize"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCfsName" runat="server" CssClass="textbox150"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px;">
                    <asp:Label ID="lblAddress" runat="server" class="fontsize" Text="Address"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="textbox150"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px;">
                    <asp:Label ID="lblContactPerson" class="fontsize" runat="server" Text="Contact Person"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtContactPerson" runat="server" CssClass="textbox150"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px;">
                    <asp:Label ID="lblEmail" runat="server" Text="Email" CssClass="fontsize"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox150"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px;" >
                    <asp:Label ID="lblFavor" class="fontsize" runat="server" Text="Favor"></asp:Label>
             
                </td>
                <td>
                    <asp:TextBox ID="txtInfavor" runat="server" CssClass="textbox150"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" >
                    <asp:Button ID="btnSave" CssClass="masterbutton" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="javascript:return validate();" />
                    <asp:Button ID="btnUpdate" CssClass="masterbutton" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                    <asp:Button ID="btnCancel" CssClass="masterbutton" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
       <tr>
       <td colspan="2">
        <div class="grid_scroll-2">
            <asp:GridView ID="gvCFSMaster" runat="server" CssClass="table-wrapper" OnSelectedIndexChanged="gvCFSMaster_SelectedIndexChanged"
                AutoGenerateColumns="False" Width="600" Height="100px">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:BoundField HeaderText="ID" DataField="Id" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                    <asp:BoundField HeaderText="CFS Name" DataField="CFS" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                    <asp:BoundField HeaderText="Address" DataField="Address" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                    <asp:BoundField HeaderText="Contact Pesron" DataField="Contact"></asp:BoundField>
                    <asp:BoundField HeaderText="Email" DataField="Email" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                    <asp:BoundField HeaderText="Favor" DataField="Favor" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
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

<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="frmCustomMaster.aspx.cs" Inherits="ImpexCube.frmCustomMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
     function valsave() {
         var custom = document.getElementById('ContentPlaceHolder1_txtCustom').value;
         var branch = document.getElementById('ContentPlaceHolder1_txtBranch').value;
         if (custom == "") {
             alert('Please Enter Custom');
             return false;
         }
         if (branch == "") {
             alert('Please Enter Branch');
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
    <style type="text/css">
        .style1
        {
            height: 25px;
        }
    .style2
    {
        height: 19px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table align="left">
        <tr>
             <td colspan="2" align="center" style="color: #008080; font-style: italic; font-weight: bold;
                            font-size: large">
                            Custom House Master
                        </td>
        </tr>
        <tr>
             <td colspan="2" align="center" style="color: #008080; font-style: italic; font-weight: bold;
                            font-size: large" class="style1">
                            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCustom" runat="server" Text="Custom" CssClass="fontsize"></asp:Label>
              </td>
            <td>
                <asp:TextBox ID="txtCustom" runat="server" CssClass="textbox150" ></asp:TextBox><font color="red">*</font>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="lblBranch" runat="server" Text="Branch" 
                    CssClass="fontsize"></asp:Label>
                </td>
            <td class="style2">
                <asp:TextBox ID="txtBranch" runat="server" CssClass="textbox150"></asp:TextBox><font color="red">*</font>
            </td>
        </tr>
        <tr>
            <td  colspan="2">
               </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                        <asp:Button ID="btnNew" runat="server" Text="New" CssClass="masterbutton" 
                            onclick="btnNew_Click" />
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="masterbutton"
                   OnClientClick="javascript:return valsave();" onclick="btnSave_Click" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update"
                    CssClass="masterbutton" onclick="btnUpdate_Click" />
                <asp:Button ID="btnDiscard" runat="server" Text="Exit"
                    CssClass="masterbutton" onclick="btnDiscard_Click" OnClientClick="javascript:return exit();"  />
            
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                        &nbsp;</td>
        </tr>
        <tr>
        <td colspan="2" >
            <div  class="grid_scroll-2">
                    <asp:GridView ID="gvCustom" runat="server" CssClass="table-wrapper" AutoGenerateSelectButton="True"
                         AutoGenerateColumns="False"
                         Width="400px" onselectedindexchanged="gvCustom_SelectedIndexChanged">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns >
                            <asp:BoundField HeaderText="TransId" DataField="TransId" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                            <asp:BoundField HeaderText="Custom" DataField="Custom" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                            <asp:BoundField HeaderText="Branch" DataField="Branch" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
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
          <input type="hidden" id="hdnCustomID" runat="server" />
</asp:Content>

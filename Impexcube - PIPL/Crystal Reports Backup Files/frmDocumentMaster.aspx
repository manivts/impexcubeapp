<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="frmDocumentMaster.aspx.cs" Inherits="ImpexCube.frmDocumentMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
    function validate() {
        var Doc = document.getElementById("<%= txtDocumentName.ClientID %>").value;
        if (Doc == "") {
            alert('Please Enter Document Name');
            document.getElementById("<%= txtDocumentName.ClientID %>").focus();
            return false;
        }
        return true;
    }
</script>
    <div style="text-align: center">
        <table align="center">
           <tr>
           <td style="text-align: center">
           
               <asp:Label ID="lblDocumentMaster" runat="server" 
                   align="center" style="color: #008080; font-style: italic; font-weight: bold;
                            font-size: large"
                   Text="Document Master"></asp:Label>
           </td>
           </tr>
           <tr>
           <td ></td>
           </tr>
           <tr>
           <td class="fontsize" >
               Document Name
               <asp:TextBox ID="txtDocumentName" runat="server" CssClass="textbox150"></asp:TextBox>
           
           </td>
           </tr>

           <tr>
           <td style="text-align: left" >
               &nbsp;</td>
           </tr>

           <tr>
           <td style="text-align: center">
               <asp:Button ID="btnNew" runat="server" CssClass="masterbutton" Text="New" 
                   onclick="btnNew_Click" OnClientClick="javascript:return validate();" />
               <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Save" 
                   onclientclick="return confirm('Do you want to Save');" 
                   CssClass="masterbutton" />
                  
               <asp:Button ID="btnUpdate" runat="server" onclick="btnUpdate_Click" 
                   Text="Update" Visible="False" 
                   onclientclick="return confirm('Do you want to Update');" 
                   CssClass="masterbutton" />
               <asp:Button ID="btnExit" runat="server" Text="Exit" 
                   onclick="btnExit_Click" CssClass="masterbutton" />
           </td>
           </tr>
           <tr>
           <td style="text-align: left"  class="style1" align="center">
           <div class="grid_scroll-2">
               <asp:GridView ID="gvDocument" runat="server" CssClass="table-wrapper" AutoGenerateSelectButton="True"                    
                   onselectedindexchanged="gvDocument_SelectedIndexChanged" 
                   AutoGenerateColumns="False" Width="500px">                  
                   <Columns>
                       <asp:BoundField HeaderText="Id" DataField="ID" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField HeaderText="Document Name" HeaderStyle-HorizontalAlign="Center" DataField="DocumentName" ></asp:BoundField>
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
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
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


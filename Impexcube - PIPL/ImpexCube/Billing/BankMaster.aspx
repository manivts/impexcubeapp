<%@ Page Language="C#" MasterPageFile="~/Billing/MasterPage.master" AutoEventWireup="true" Inherits="Master_BankMaster" Codebehind="BankMaster.aspx.cs" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">
function OpenPopup()
{
window.open('Bank.aspx', '_blank','width=300,height=150,titlebar=no, menubar=no, scrollbars=yes,header=no toolbar=no, location=no, resizable=no, left=455, top=400');
false;
}

</script>
  
 <table style="width: 100%; height: 400px; border: 1px;">
        <tr>
        <td>
       <table style="width: 100%;">
      

     <tr>
     <td align="center"  style="vertical-align: top; background-color: white; width: 975px; ">
 
    
    
    <table>
     <tr>
    <td align="center" style="vertical-align: top;">
    <asp:Panel ID="Cargo" runat="server" Font-Names="Arial" 
        GroupingText="Bank Master" Font-Size="8pt" Width="350px">
     <table width="250">
   
     <tr>
            <td align="left">
                <asp:Label ID="lblbname" runat="server" Text="Bank Name" Font-Names="Arial" 
                    Font-Size="8pt"></asp:Label>
                <asp:Label ID="Label23" runat="server" Font-Names="Arial" Font-Size="8pt" 
                    ForeColor="Red" Text="*"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txt_bname" runat="server" CausesValidation="True" 
                    Font-Names="Arial" Font-Size="8pt" Width="133px"></asp:TextBox>
           
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
         ControlToValidate="txt_bname" ErrorMessage="*" Font-Size="8pt"></asp:RequiredFieldValidator>
            </td>
        </tr>
        
        <tr>
            <td align="left">
                <asp:Label ID="lblaccno" runat="server" Text="Acc No" Font-Names="Arial" 
                    Font-Size="8pt"></asp:Label>
                <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" 
                    ForeColor="Red" Text="*"></asp:Label>
                </td>
            <td align="left">
                <asp:TextBox ID="txt_accno" runat="server" Font-Names="Arial" Font-Size="8pt" 
                    Width="134px"></asp:TextBox>
           
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
         ControlToValidate="txt_accno" ErrorMessage="*" 
         Font-Size="8pt"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lbladdress" runat="server" Text="Address" Font-Names="Arial" 
                    Font-Size="8pt"></asp:Label>
                <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" 
                    ForeColor="Red" Text="*"></asp:Label></td>
            <td align="left">
                <asp:TextBox ID="txt_address" runat="server" Font-Names="Arial" Font-Size="8pt" 
                    Width="134px"></asp:TextBox>
           
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
         ControlToValidate="txt_address" ErrorMessage="*" 
         Font-Size="8pt"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lblcity" runat="server" Text="City" Font-Names="Arial" 
                    Font-Size="8pt"></asp:Label>
                <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" 
                    ForeColor="Red" Text="*"></asp:Label></td>
            <td align="left">
                <asp:TextBox ID="txt_city" runat="server" Font-Names="Arial" Font-Size="8pt" 
                    Width="134px"></asp:TextBox>
           
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
         ControlToValidate="txt_city" ErrorMessage="*" 
         Font-Size="8pt"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lblpincode" runat="server" Text="Pincode" Font-Names="Arial" 
                    Font-Size="8pt"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txt_pincode" runat="server" Font-Names="Arial" Font-Size="8pt" 
                    Width="134px"></asp:TextBox>
           
            </td>
        </tr>
        <tr>
        <td>
            <asp:Label ID="lbl" runat="server" Visible="false"></asp:Label>
        </td></tr>
        
            
        
        
     </table>
     <table>
                <tr>
                <td>
                        <asp:Button ID="btn_add" runat="server" OnClick="btn_add_Click" Text="Add" 
                            CausesValidation="False" Font-Names="Arial" Font-Size="8pt"  
                            Width="44px" CssClass="button_image1" Height="25px" />
                    
                        <asp:Button ID="btn_view" runat="server" OnClick="btn_view_Click" Text="View" 
                            CausesValidation="False" Font-Names="Arial" Font-Size="8pt" Width="44px" 
                            CssClass="button_image1" Height="25px" />
                   
                        <asp:Button ID="btn_modify" runat="server" OnClick="btn_modify_Click" 
                            Text="Modify" CausesValidation="False" Font-Names="Arial" Font-Size="8pt"  
                            Width="44px" CssClass="button_image1" Height="25px"/>
                                                   
                    
                    <asp:Button ID="btn_save" runat="server" OnClick="btn_save_Click" Text="Save" 
                            Font-Names="Arial" Font-Size="8pt"  Width="44px" CssClass="button_image1" 
                            Height="25px" />
              
                      <asp:Button ID="btn_clear" runat="server" OnClick="btn_clear_Click" Text="Clear" 
                            CausesValidation="False" Font-Names="Arial" Font-Size="8pt"  Width="44px" 
                            CssClass="button_image1" Height="25px"/>
                     
                       <asp:Button ID="btn_exit" runat="server" OnClick="btn_exit_Click" Text="Exit" 
                            CausesValidation="False" Font-Names="Arial" Font-Size="8pt"  Width="44px" 
                            PostBackUrl="~/Billing/index.aspx" CssClass="button_image1" Height="25px"/></td></tr>                    
                                                  
                            
                            
                           </table>
     <table>
     <tr>
     <td>
     <asp:Label ID="lblresult" Visible="false" runat="server"></asp:Label>
     </td>
     </tr>
     </table>  
    <table>
    <tr>
    <td>
        <asp:GridView ID="gvBank" runat="server" AutoGenerateColumns="False" 
            AutoGenerateSelectButton="True" CellPadding="4" Font-Size="8pt" 
            ForeColor="#333333" GridLines="None" 
            onselectedindexchanged="gvBank_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
       <Columns>
       <asp:BoundField DataField="TransId" HeaderText="S No" />
       <asp:BoundField DataField="BankName" HeaderText="Bank Name" />
       <asp:BoundField DataField="Accno" HeaderText="Acc No" />
       <asp:BoundField DataField="Address" HeaderText="Address" />
       <asp:BoundField DataField="City" HeaderText="City" />
       <asp:BoundField DataField="PinCode" HeaderText="PinCode" />
       </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" Font-Size="8pt" 
                ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    </td>
    </tr>
    </table>
     </asp:Panel>
    </td>
    </tr> 
    </table>
        

     </td>
     </tr>
     </table>
        </td>
        </tr>
        
        </table>
  </asp:Content>
    
    
  
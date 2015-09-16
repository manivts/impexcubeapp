<%@ Page Language="C#" MasterPageFile="~/Billing/MasterPage.master" AutoEventWireup="true" Inherits="frmChargeDtl" Codebehind="frmChargeDtl.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
<div>

    <table style="width: 100%; height: 400px; border: 1px;">
        <tr>
        <td>
       
       <table style="width: 100%;">
      
   

     <tr>
     <td style="height:90%; vertical-align: top; background-color: white; width: 100%; border: solid 1px #2461bf;">
    
    
    <asp:Panel ID="Panel3" runat="server" Height="600px"  Width="100%"  
             BackColor="white"  >
     <table style=" width: 100%;">
    <tr>
    <td  >
    <table >
   
    <tr>
    <td>
     <asp:Panel ID="Panel2" runat="server" GroupingText="Description" 
            Font-Names="Arial" Font-Size="8pt"  Height="120px"  Width="600px"><center>
    <table>
         <tr>
         <td align="right" >
             <asp:Label ID="Label2" runat="server" Text="Charge Code :" Font-Names="Arial" Font-Size="8pt"></asp:Label>
         </td>
         <td align="left" >
             <asp:TextBox ID="txtcCode" runat="server" Font-Names="Arial" Font-Size="8pt" 
                 ReadOnly="True"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtcCode"
                 ErrorMessage="*"></asp:RequiredFieldValidator></td>
         </tr>
         <tr>
         <td align="right" >
             <asp:Label ID="Label3" runat="server" Text="Charge Description :" Font-Names="Arial" Font-Size="8pt" Width="100px"></asp:Label>
         </td>
         <td align="left" >
             <asp:TextBox ID="txtcDesc" runat="server" Font-Names="Arial" Font-Size="8pt" Width="200px"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtcDesc"
                 ErrorMessage="*"></asp:RequiredFieldValidator>
         </td>
         </tr>
         <tr>
         <td align="center" colspan="2">
             <asp:Button ID="BTNADD" runat="server" Text="ADD" 
                 Font-Names="Arial" Font-Size="8pt" Height="30px" Width="60px" 
                 CausesValidation="False" OnClick="BTNADD_Click" CssClass="button_image1" />
             &nbsp;<asp:Button ID="BtnSubmit" runat="server" Text="SAVE" Font-Names="Arial" 
                 Font-Size="8pt" Height="30px" Width="60px" OnClick="BtnSubmit_Click" 
                 CssClass="button_image1" />
             <asp:Button ID="BTNView" runat="server" Text="VIEW" Font-Names="Arial" 
                 Font-Size="8pt" Height="30px" Width="60px" CausesValidation="False" 
                 OnClick="BTNView_Click" CssClass="button_image1" />
             <asp:Button ID="BtnDelete" runat="server" Text="DELETE" Font-Names="Arial" 
                 Font-Size="8pt" Height="30px" Width="60px" OnClick="BtnDelete_Click" 
                 OnClientClick="return confirm ('Are your sure want to Delete ?');" 
                 CssClass="button_image1" />
             <asp:Button ID="BTNClear" runat="server" Text="CLEAR" Font-Names="Arial" 
                 Font-Size="8pt" Height="30px" Width="60px" CausesValidation="False" 
                 OnClick="BTNClear_Click" CssClass="button_image1" />
             <asp:Button ID="BTNEXIT" runat="server" Text="EXIT" Font-Names="Arial" 
                 Font-Size="8pt" Height="30px" Width="60px" CausesValidation="False" 
                 OnClick="BTNEXIT_Click" PostBackUrl="~/index.aspx" 
                 CssClass="button_image1"  /></td>
         </tr>
         </table>  
     </center></asp:Panel>
    </td>
    </tr>
    <tr>
    <td style="height: 16px"></td>
    </tr>
    <tr>
     <td>
     <asp:Panel ID="Panel1" runat="server" GroupingText="Description" Font-Names="Arial" 
             Font-Size="8pt"  Height="450px"  Width="800px">
    <table id="grdTbl" runat="Server"  style=" width: 799px;">
    <tr>
    <td align="left" ><div id="GridScroll" class="grid_scroll" style="width: 766px" 
            runat="server">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="charge_code"
            Font-Names="Arial" Font-Size="8pt" OnSelectedIndexChanged="GridView1_selectChanged"
            Width="268px" GridLines="Horizontal">
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
            <Columns>
            <asp:ButtonField ButtonType="Button" CommandName="select" ControlStyle-Font-Names="arial" ControlStyle-Font-Size="8pt" Text="select" />
                <asp:BoundField DataField="charge_code" HeaderText="Code" SortExpression="charge_code">
                    <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="charge_desc" HeaderText="Description" SortExpression="charge_desc">
                    <ItemStyle Wrap="False" />
                </asp:BoundField>
                
            </Columns>
            <AlternatingRowStyle BackColor="#F7F7F7" />
        </asp:GridView>
    </div> 
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
        
</asp:Panel>  
     </td>
     </tr>
     </table>
        </td>
        </tr>
        
        </table>
    </div>
   
   
       
        <cc1:FilteredTextBoxExtender ID="FTE1" FilterType="Numbers" TargetControlID="txtcCode" runat="server">
        </cc1:FilteredTextBoxExtender>
   </asp:Content> 

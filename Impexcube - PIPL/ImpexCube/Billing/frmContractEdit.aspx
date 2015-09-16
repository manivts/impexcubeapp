<%@ Page Language="C#" MasterPageFile="~/Billing/MasterPage.master" AutoEventWireup="true" Inherits="frmContractEdit" Title="Untitled Page" Codebehind="frmContractEdit.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="FreeLibrary" Namespace="FreeLibrary" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div>
    <table style="width: 100%; height: 400px; border: 1px;">
        <tr>
        <td style="height: 20%" >
          
    
       <table style="width: 100%;">
       
   

     <tr>
     <td style="height:90%; vertical-align: top; background-color: white; ">
    
    
    <asp:Panel ID="Panel3" runat="server" Height="700px" Width="100%" BackColor="white"  >
        <table>
    <tr>
    <td align="center" >
    <table>
    <tr>
    <td style="width: 100%;border: solid 1px #2461bf;">
    
    <center>
            <asp:Label ID="Label1" runat="server" Text="Update Contract Information for"  Font-Names="Arial" Font-Size="12pt" Font-Bold="true"  ForeColor="#2461bf"></asp:Label>
        <asp:Label ID="lblShortName" runat="server" Text="" Font-Names="Arial" Font-Size="12pt" Font-Bold="true"  ForeColor="#2461bf"></asp:Label>
        </center>
      
    </td>
    </tr>
    <tr>
    <td align="left"  style="width: 873px; vertical-align: top; border: solid 1px #2461bf;">
        
        <table>
        <tr style="border-bottom: solid">
        <td style="height: 224px; vertical-align: top;">
        <table>
        <tr>
        <td>
        <table>
         <tr>
         <td colspan="2" align="left" >
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
             <ContentTemplate>
             <table>
             <tr>
         <td align="right" >
             <asp:Label ID="Label2" runat="server" Text="Select Customer Name :" Font-Names="Arial" Font-Size="8pt" Width="117px"></asp:Label>
             
             
         </td>
         <td align="left" style="width: 525px" >
           
             <asp:DropDownList ID="drCustomer" runat="server" Font-Names="Arial" Font-Size="8pt" Width="196px" AutoPostBack="True" OnSelectedIndexChanged="drCustomer_SelectedIndexChanged">
             </asp:DropDownList>
            
            
             
             </td>
         </tr>
          <tr>
         <td align="right" >
             <asp:Label ID="Label3" runat="server" Text="Select Contract :" Font-Names="Arial" Font-Size="8pt" Width="117px"></asp:Label>
         </td>
         <td align="left" style="width: 525px" >
              <asp:DropDownList ID="drContract" runat="server" Font-Names="Arial" Font-Size="8pt" Width="194px">
             </asp:DropDownList>
             </td>
         </tr>
             </table>
             </ContentTemplate>
             </asp:UpdatePanel>
         </td>
         </tr>
         <tr>
         <td align="right" >
             <asp:Label ID="txtContrName" runat="server"></asp:Label>&nbsp;</td>
         <td align="left" style="width: 525px" >
             
             <asp:Button ID="BtnSearch" runat="server" Text="Search" 
                 OnClick="BtnSearch_Click" CausesValidation="False" CssClass="button_image1" 
                 Height="25px" Width="115px" /></td>
         </tr>
         
         <tr>
         <td align="right" >
             <asp:Label ID="Label4" runat="server" Text="Approved By :" Font-Names="Arial" Font-Size="8pt" Width="100px"></asp:Label>
         </td>
         <td align="left" style="width: 525px" >
             <asp:TextBox ID="txtApproved" runat="server" Font-Names="Arial" Font-Size="8pt" Width="120px"></asp:TextBox>       
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtApproved"
                 ErrorMessage="*" Font-Names="Arial" Font-Size="8pt"></asp:RequiredFieldValidator></td>
         </tr>
         <tr>
         <td align="right" style="vertical-align: top;" >
             <asp:Label ID="Label5" runat="server" Text="Contract Valid From :" Font-Names="Arial" Font-Size="8pt" Width="108px"></asp:Label>
         </td>
         <td align="left" style="width: 525px" >
             <asp:TextBox ID="txtFrom" runat="server" Font-Names="Arial" Font-Size="8pt" Width="86px"></asp:TextBox>&nbsp;
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFrom"
                 ErrorMessage="*" Font-Names="Arial" Font-Size="8pt"></asp:RequiredFieldValidator>
             <asp:Label ID="Label6" runat="server" Text=" To :" Font-Names="Arial" Font-Size="8pt" Width="12px"></asp:Label>
             <asp:TextBox ID="txtTo" runat="server" Font-Names="Arial" Font-Size="8pt" Width="86px"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTo"
                 ErrorMessage="*" Font-Names="Arial" Font-Size="8pt"></asp:RequiredFieldValidator></td>
         </tr>
         <tr>
         <td align="right" style=" vertical-align: top;" >
             <asp:Label ID="Label16" runat="server" Text="Contract Status :" Font-Names="Arial" Font-Size="8pt" Width="100px"></asp:Label>&nbsp;</td>
         <td align="left" style="width: 525px" >
             <asp:DropDownList ID="drStatus" runat="server" Font-Names="Arial" Font-Size="8pt" Width="140px">
                <asp:ListItem Value="0">select</asp:ListItem>
                 <asp:ListItem>Active</asp:ListItem>
                 <asp:ListItem>In Active</asp:ListItem>
             </asp:DropDownList></td>
         </tr>
         
        </table>
        </td>
        <td style="vertical-align: bottom;">
        <table id="tblRenewal" runat="server" >
         <tr>
         <td align="right" >
             &nbsp;</td>
         <td align="left" style="width: 525px" >
             &nbsp;</td>
         </tr>
         
         <tr>
         <td align="left" colspan="2" style="height: 18px" >
             <asp:Label ID="Label29" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                 Text="Renewal Info :" Width="117px" Font-Underline="True" ForeColor="#C00000"></asp:Label>&nbsp;</td>
         </tr>
         <tr>
         <td align="right" >
             <asp:Label ID="Label25" runat="server" Text="Renewal By :" Font-Names="Arial" Font-Size="8pt" Width="66px"></asp:Label>
         </td>
         <td align="left" style="width: 525px" >
             <asp:TextBox ID="txtRenewalBy" runat="server" Font-Names="Arial" Font-Size="8pt" Width="116px"></asp:TextBox>       
             <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtRenewalBy"
                 ErrorMessage="*" Font-Names="Arial" Font-Size="8pt"></asp:RequiredFieldValidator></td>
         </tr>
         <tr>
         <td align="right" style="vertical-align: top;"  >
             <asp:Label ID="Label26" runat="server" Text="Renewal Valid From :" Font-Names="Arial" Font-Size="8pt" Width="108px"></asp:Label>
         </td>
         <td align="left" style="width: 525px" >
             <asp:TextBox ID="txtRenFrom" runat="server" Font-Names="Arial" Font-Size="8pt" Width="70px"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtFrom"
                 ErrorMessage="*" Font-Names="Arial" Font-Size="8pt"></asp:RequiredFieldValidator>
             <asp:Label ID="Label27" runat="server" Text="To :" Font-Names="Arial" Font-Size="8pt" Width="20px"></asp:Label>
             <asp:TextBox ID="txtRenTo" runat="server" Font-Names="Arial" Font-Size="8pt" Width="70px"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtTo"
                 ErrorMessage="*" Font-Names="Arial" Font-Size="8pt"></asp:RequiredFieldValidator></td>
         </tr>
         
         
        </table>
        
        
        
        </td>
        </tr>
        </table>
        </td>
        </tr>
        <tr>
         <td colspan="2" style="background-color: #2461bf; height:2px;"></td>
         </tr>
         <tr id="tbHeader" runat="server"  >
         <td align="left"  colspan="2" style="vertical-align: bottom; height: 64px;">
         <table style="background-color: #2461bf;">
         <tr>
         <td align="center" style=" width:30px;">
             <asp:Label ID="Label15" runat="server" Font-Bold="true"  ForeColor="white"  Font-Names="arial" Font-Size="8pt" Text="SNO"></asp:Label>
         </td>
         <td align="center"  style=" width:170px;">
             <asp:Label ID="Label7" runat="server" Font-Bold="true"  ForeColor="white"  Font-Names="arial" Font-Size="8pt" Text="Description"></asp:Label>
         </td>
         <td align="center" style=" width:90px;">
             <asp:Label ID="Label8" runat="server" Font-Bold="true"  ForeColor="white"  Font-Names="arial" Font-Size="8pt" Text="Product"></asp:Label>
         </td>
         <td align="center" style=" width:80px;">
             <asp:Label ID="Label9" runat="server" Font-Bold="true"  ForeColor="white"  Font-Names="arial" Font-Size="8pt" Text="Unit"></asp:Label>
         </td>
         <td align="center" style=" width:70px;">
             <asp:Label ID="Label10" runat="server" Font-Bold="true"  ForeColor="white"  Font-Names="arial" Font-Size="8pt" Text="AIR Charges"></asp:Label>
         </td>
         <td align="center" style=" width:70px;">
             <asp:Label ID="Label11" runat="server" Font-Bold="true"  ForeColor="white"  Font-Names="arial" Font-Size="8pt" Text="BREAK BULK"></asp:Label>
         </td>
         <td align="center" style=" width:65px;">
             <asp:Label ID="Label12" runat="server" Font-Bold="true"  ForeColor="white"  Font-Names="arial" Font-Size="8pt" Text="LCL"></asp:Label>
         </td>
         <td align="center" style=" width:65px;">
             <asp:Label ID="Label13" runat="server" Font-Bold="true"  ForeColor="white"  Font-Names="arial" Font-Size="8pt" Text="20 FT"></asp:Label>
         </td>
         <td align="center" style=" width:65px;">
             <asp:Label ID="Label14" runat="server" Font-Bold="true"  ForeColor="white"  Font-Names="arial" Font-Size="8pt" Text="40 FT"></asp:Label>
         </td>
         <td align="center" style=" width:65px;">
             <asp:Label ID="Label17" runat="server" Font-Bold="True"  ForeColor="White"  Font-Names="arial" Font-Size="8pt" Text="Sales Bill/ Debit Note" Width="101px"></asp:Label>
         </td>
         
         </tr>
         </table>
         </td></tr>
         
         <tr id="gridID" runat="server" >
         <td colspan="2" align="left" style="vertical-align: top;" >
         <div id="GridScroll" class="grid_scroll" style="width: 855px; height:235px;">
             &nbsp;<cc2:NewRowGridView ID="myNewRowGridView" runat="server" AutoGenerateColumns="False"
             NewRowCount="30" Font-Names="Arial" Font-Size="8pt" Font-Bold="True" >
            <Columns>
                <asp:TemplateField HeaderText="SNO">
                <ItemTemplate>
                    <asp:Label ID="lblsno" Font-Names="arial" Font-Size="8pt" Width="20px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"sno") %>'></asp:Label>
                
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                <ItemTemplate>
                    <asp:DropDownList ID="drCharge" Width="180px" Font-Names="arial" Font-Size="8pt" runat="server">
                    </asp:DropDownList>
                
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Product">
                <ItemTemplate>
                    <asp:DropDownList ID="drProduct" Font-Names="arial" Font-Size="8pt" Width="80px" runat="server">
                    </asp:DropDownList>
                
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit">
                <ItemTemplate>
                    <asp:DropDownList ID="drUnit" Font-Names="arial" Font-Size="8pt" Width="95px" runat="server">
                    </asp:DropDownList>
                
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="AIR Charges">
                <ItemTemplate>
                    <asp:TextBox ID="txtAIR" Font-Names="arial" Width="60px" Font-Size="8pt" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"AIR") %>'></asp:TextBox>
                
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="BREAK BULK">
                <ItemTemplate>
                    <asp:TextBox ID="txtBB" Font-Names="arial" Width="60px" Font-Size="8pt" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"break_bulk") %>'></asp:TextBox>
                
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="LCL">
                <ItemTemplate>
                    <asp:TextBox ID="txtLCL" Font-Names="arial" Width="60px" Font-Size="8pt" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LCL") %>'></asp:TextBox>
                
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="20 FEET">
                <ItemTemplate>
                    <asp:TextBox ID="txt20" Font-Names="arial" Width="60px" Font-Size="8pt" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ft20") %>'></asp:TextBox>
                
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="40 FEET">
                <ItemTemplate>
                    <asp:TextBox ID="txt40" Font-Names="arial" Width="60px" Font-Size="8pt" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ft40") %>'></asp:TextBox>
                
                </ItemTemplate>
                </asp:TemplateField>
              <asp:TemplateField HeaderText="Sales Bill">
                <ItemTemplate>
                    <asp:CheckBox ID="chkSB" runat="server" Font-Names="Arial" Font-Size="8pt" />
                
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Debit Note">
                <ItemTemplate>
                    <asp:CheckBox ID="chkDB" runat="server" Font-Names="Arial" Font-Size="8pt" />
                
                </ItemTemplate>
                </asp:TemplateField>
             </Columns>
                 <RowStyle Font-Bold="True" HorizontalAlign="Left" />
                 <HeaderStyle Font-Bold="True" ForeColor="Black" />
            
        </cc2:NewRowGridView></div> 
         </td>
         </tr>
         
         
         <tr id="btnID" runat="server" >
         <td colspan="2" align="center" >
             &nbsp;<asp:Button ID="BtnSubmit" runat="server" Text="Submit" 
                 Font-Names="Arial" Font-Size="9pt" Height="25px" OnClick="BtnSubmit_Click" 
                 Width="100px" CssClass="button_image1" />
             <asp:Button ID="Button1" runat="server" CausesValidation="False" Height="25px" OnClick="Button1_Click"
                 Text="Exit" Width="89px" PostBackUrl="~/index.aspx" 
                 CssClass="button_image1" /></td>
         </tr>
         </table>
        
        
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
        
                <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFrom" Format="dd/MM/yyyy"  runat="server">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtTo" Format="dd/MM/yyyy"  runat="server">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="CalendarExtender3" TargetControlID="txtRenFrom" Format="dd/MM/yyyy"  runat="server">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="CalendarExtender4" TargetControlID="txtRenTo" Format="dd/MM/yyyy"  runat="server">
        </cc1:CalendarExtender>
        <cc1:TextBoxWatermarkExtender ID="TWE1" TargetControlID="txtFrom" WatermarkText="dd/MM/yyyy" WatermarkCssClass="txtWater" runat="server">
        </cc1:TextBoxWatermarkExtender>
        <cc1:TextBoxWatermarkExtender ID="TWE2" TargetControlID="txtTo" WatermarkText="dd/MM/yyyy" WatermarkCssClass="txtWater" runat="server">
        </cc1:TextBoxWatermarkExtender>
        <cc1:TextBoxWatermarkExtender ID="TWE3" TargetControlID="txtRenFrom" WatermarkText="dd/MM/yyyy" WatermarkCssClass="txtWater" runat="server">
        </cc1:TextBoxWatermarkExtender>
        <cc1:TextBoxWatermarkExtender ID="TWE4" TargetControlID="txtRenTo" WatermarkText="dd/MM/yyyy" WatermarkCssClass="txtWater" runat="server">
        </cc1:TextBoxWatermarkExtender>

</asp:Content>


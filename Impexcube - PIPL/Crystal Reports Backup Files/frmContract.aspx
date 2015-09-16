<%@ Page Language="C#" MasterPageFile="~/Billing/MasterPage.master" AutoEventWireup="true" Inherits="frmContract" Title="::PIPL || Contract Info" Codebehind="frmContract.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
 <table style="width: 100%; height: 370px; border: 1px;">
 <tr>
 <td>
  <table style="width: 100%;">
  <tr>
     <td style="vertical-align: top; background-color: white; width: 975px; ">
     <asp:Panel ID="Panel3" runat="server" Width="100%" BackColor="white">
     
      <table>
    <tr>
    <td align="center">
    <table>
    <tr>
    <td style="width: 873px; border: solid 1px #2461bf;">
   
       <asp:Label ID="lblShortName" runat="server" Text=""  Font-Names="Arial" Font-Bold="true"  Font-Size="12pt" ForeColor="#2461bf"></asp:Label>
        
    </td>
    </tr>
    <tr>
    <td style="width: 873px; border: solid 1px #2461bf; vertical-align:top;">
    <table id="tblMst" runat="server">
    <tr>
    <td align="left" style="width:200px;"  colspan="2">
       
        <table>
        <tr>
        <td align="right" >
            <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Select Contract Type :"
                Width="117px"></asp:Label></td>
                <td align="left" style="width: 195px" >


<asp:RadioButtonList ID="rbBill" runat="server" Font-Names="Arial" Font-Size="8pt" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbBill_SelectedIndexChanged" Width="159px">
            <asp:ListItem Value="DP">Direct Party</asp:ListItem>
            <asp:ListItem Value="TP">Third Party</asp:ListItem>
        </asp:RadioButtonList>


        
    </td>
        
        </tr>
        <tr>
         <td align="right" style="height: 22px" >
             <asp:Label ID="Label2" runat="server" Text="Select Customer Name :" Font-Names="Arial" Font-Size="8pt" Width="117px"></asp:Label>
         </td>
         <td align="left" style="width: 525px; height: 22px;" >
             <asp:DropDownList ID="drCustomer" runat="server" Font-Names="Arial" Font-Size="8pt" Width="188px">
             </asp:DropDownList>
             <asp:CompareValidator ID="CompareValidator1" runat="server" 
                 ControlToValidate="drCustomer" ErrorMessage="*" Font-Names="Arial" 
                 Font-Size="8pt" Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
             &nbsp;
         </td>
         </tr>
        </table>
       
    </td>
    <td align="left"  colspan="2">
    <table>
     <tr>
        <td></td>
        </tr>
        <tr>
        <td></td>
        </tr>
        <tr>
        <td>
            <asp:Button ID="BtnCustInfo" runat="server" Text="Check Customer Info"  
                CausesValidation="False" Font-Names="Arial" Font-Size="8pt" 
                onclick="BtnCustInfo_Click" CssClass="button_image1" Height="25px" />
        </td>
        </tr>
    </table>
    </td>
    </tr>
     <tr>
         <td align="right" style="width: 111px" >
             <asp:Label ID="Label3" runat="server" Text="Contract Name :" Font-Names="Arial" Font-Size="8pt" Width="118px"></asp:Label>
         </td>
         <td align="left" style="width: 200px;"  >
             <asp:TextBox ID="txtContrName" runat="server" Font-Names="Arial" Font-Size="8pt" Width="150px"></asp:TextBox>       
             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtContrName"
                 ErrorMessage="*" Font-Names="Arial" Font-Size="8pt"></asp:RequiredFieldValidator></td>
        
         <td align="right" >
             <asp:Label ID="Label4" runat="server" Text="Approved By :" Font-Names="Arial" Font-Size="8pt" Width="100px"></asp:Label>
         </td>
         <td align="left" style="width: 525px" >
             <asp:TextBox ID="txtApproved" runat="server" Font-Names="Arial" Font-Size="8pt" Width="150px"></asp:TextBox>       
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtApproved"
                 ErrorMessage="*" Font-Names="Arial" Font-Size="8pt"></asp:RequiredFieldValidator></td>
         </tr>
         <tr>
         <td align="right" style="width: 111px" >
             <asp:Label ID="Label5" runat="server" Text="Contract Valid From :" Font-Names="Arial" Font-Size="8pt" Width="118px"></asp:Label>
         </td>
         <td align="left">
             <asp:TextBox ID="txtFrom" runat="server" Font-Names="Arial" Font-Size="8pt" Width="150px"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFrom"
                 ErrorMessage="*" Font-Names="Arial" Font-Size="8pt"></asp:RequiredFieldValidator></td>
         
         <td align="right" >
             <asp:Label ID="Label6" runat="server" Text="Contract Valid To :" Font-Names="Arial" Font-Size="8pt" Width="100px"></asp:Label>
         </td>
         <td align="left" style="width: 525px" >
             <asp:TextBox ID="txtTo" runat="server" Font-Names="Arial" Font-Size="8pt" Width="150px"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTo"
                 ErrorMessage="*" Font-Names="Arial" Font-Size="8pt"></asp:RequiredFieldValidator></td>
         </tr>
         
          <tr>
         <td colspan="4" align="left" >
         <div id="GridScroll" class="grid_scroll" style="width: 855px; height: 235px;">
         <asp:GridView id="GridView1" runat="server" Font-Names="Arial" Font-Size="8pt" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellSpacing="1" CellPadding="3" GridLines="None" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBond" OnPreRender="GridView1_PreRender">
            <FooterStyle BackColor="#C6C3C6" ForeColor="Black"  />
            <RowStyle BackColor="#DEDFDE" ForeColor="Black"  />
            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White"  />
            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right"  />
            <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="#E7E7FF"  />
            <Columns>
            
                <asp:TemplateField Visible="false"  HeaderText="SNO">
                <ItemTemplate>
                    <asp:Label ID="lblsno"  runat="server" Text='<%# Bind("t1")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                <ItemTemplate>
                    <asp:DropDownList ID="drCharge" Width="200px" Font-Names="arial" Font-Size="8pt" runat="server">
                        
                    </asp:DropDownList>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Product">
                <ItemTemplate>
                    <asp:DropDownList ID="drProduct" Width="80px" Font-Names="arial" Font-Size="8pt" runat="server">
                       
                    </asp:DropDownList>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit">
                <ItemTemplate>
                    <asp:DropDownList ID="drUnit" Width="80px" Font-Names="arial" Font-Size="8pt" runat="server">
                       
                    </asp:DropDownList>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="AIR Charges">
                <ItemTemplate>
                    <asp:TextBox ID="txtAIR" Font-Names="arial" Width="60px" Font-Size="8pt" runat="server"></asp:TextBox>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="BREAK BULK">
                <ItemTemplate>
                    <asp:TextBox ID="txtBB" Font-Names="arial" Width="60px" Font-Size="8pt" runat="server"></asp:TextBox>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="LCL">
                <ItemTemplate>
                    <asp:TextBox ID="txtLCL" Font-Names="arial" Width="60px" Font-Size="8pt" runat="server"></asp:TextBox>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="20 FEET">
                <ItemTemplate>
                    <asp:TextBox ID="txt20" Font-Names="arial" Width="60px" Font-Size="8pt" runat="server"></asp:TextBox>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="40 FEET">
                <ItemTemplate>
                    <asp:TextBox ID="txt40" Font-Names="arial" Width="60px" Font-Size="8pt" runat="server"></asp:TextBox>
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
        </asp:GridView></div> 
         </td>
         </tr>
         <tr>
         <td style="width: 111px">
         
         </td>
         <td align="left" colspan="2">
             <asp:Button ID="BtnSubmit" OnClick="BtnSubmit_Click" runat="server" 
                 Text="Submit" Font-Names="Arial" Font-Size="9pt" Height="25px" Width="100px" 
                 CssClass="button_image1" />
             <asp:Button ID="Button1" runat="server" CausesValidation="False" Height="25px" OnClick="Button1_Click"
                 Text="Exit" Width="89px" PostBackUrl="~/index.aspx" 
                 CssClass="button_image1" />
         </td>
         </tr>
    </table>
    <table id="tblAddr" runat="server" >
        <tr>
        <td>
        <div id="Div3" class="grid_scroll" style="width: 800px">
        <asp:GridView ID="GrdPaddr" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" DataKeyNames="addr_num"
            Font-Names="Arial" Font-Size="8pt" Height="154px" OnSelectedIndexChanged="GrdPaddr_SelectedIndexChanged"
            Width="674px">
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <Columns>
                <asp:BoundField DataField="party_code" HeaderText="PCODE" SortExpression="party_code" />
                <asp:BoundField DataField="addr_num" HeaderText="Branch" SortExpression="addr_num" />
                <asp:BoundField DataField="address" HeaderText="Address" SortExpression="address" />
                <asp:BoundField DataField="city" HeaderText="City" SortExpression="city" />
                <asp:BoundField DataField="state" HeaderText="State" SortExpression="Pin" />
                <asp:BoundField DataField="pin" HeaderText="Pin" SortExpression="pin" />
                <asp:CommandField ButtonType="Image" HeaderText="CL" SelectImageUrl="~/image/select.jpg"
                    ShowSelectButton="True">
                    <HeaderStyle Height="5px" />
                    <ItemStyle Height="5px" Width="5px" />
                </asp:CommandField>
            </Columns>
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
        </div> 
        </td>
        </tr>
        </table>
    <table id="tblThird" runat="server" >
        <tr>
        <td align="right" >
            <asp:Label ID="Label9" runat="server" Text="Party Name :" Font-Names="Arial" Font-Size="8pt"></asp:Label>
        </td>
        <td align="left" >
            <asp:TextBox ID="txtCust" runat="server" Font-Names="Arial" Font-Size="8pt" Width="212px"></asp:TextBox>
        </td>
        </tr>
        <tr>
        <td align="right"  style="vertical-align: top;">
            <asp:Label ID="Label10" runat="server" Text="Address :" Font-Names="Arial" Font-Size="8pt"></asp:Label>
        </td>
        <td align="left" >
            <asp:TextBox ID="txtAddr" runat="server" Font-Names="Arial" Font-Size="8pt" TextMode="MultiLine" Width="213px"></asp:TextBox>
        </td>
        </tr>
        <tr>
        <td align="right" >
            <asp:Label ID="Label11" runat="server" Text="City :" Font-Names="Arial" Font-Size="8pt"></asp:Label>
        </td>
        <td align="left" >
            <asp:TextBox ID="txtCity" runat="server" Font-Names="Arial" Font-Size="8pt" 
                Width="212px"></asp:TextBox>
        </td>
        </tr>
        <tr>
        <td align="right" >
            <asp:Label ID="Label12" runat="server" Text="State :" Font-Names="Arial" Font-Size="8pt"></asp:Label>
        </td>
        <td align="left" >
            <asp:TextBox ID="txtState" runat="server" Font-Names="Arial" Font-Size="8pt" 
                Width="212px"></asp:TextBox>
        </td>
        </tr>
        <tr>
        <td align="right" >
            <asp:Label ID="Label13" runat="server" Text="Pin :" Font-Names="Arial" Font-Size="8pt"></asp:Label>
        </td>
        <td align="left" >
            <asp:TextBox ID="txtPin" runat="server" Font-Names="Arial" Font-Size="8pt" Width="68px"></asp:TextBox>
            <asp:Label ID="Label14" runat="server" Text="Phone :" Font-Names="Arial" Font-Size="8pt"></asp:Label>
            <asp:TextBox ID="txtPhone" runat="server" Font-Names="Arial" Font-Size="8pt" Width="96px"></asp:TextBox>
        </td>
        </tr>
        <tr>
        <td colspan="2">
            <asp:Button ID="BtnThird" runat="server" Text="Submit" Font-Names="Arial" Font-Size="8pt" OnClick="BtnThird_Click" />
        </td>
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
  <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFrom">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtTo">
        </cc1:CalendarExtender>
        </ContentTemplate>
        </asp:UpdatePanel> 
</div>
</asp:Content>


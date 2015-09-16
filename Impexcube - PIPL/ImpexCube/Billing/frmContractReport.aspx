<%@ Page Language="C#" MasterPageFile="~/Billing/MasterPage.master" AutoEventWireup="true" Inherits="frmContractReport" Title="::PIPLBilling || Contract Report Status " Codebehind="frmContractReport.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up1">
<ProgressTemplate>
            <span style="font-size: small; color: #000066">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            Please Wait...</span><asp:Image ID="Image123" runat="server" ImageUrl="~/image/progress.gif" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="up1" runat="server" >
    
<contenttemplate>

   
    <table width="100%">
  
   
   <tr>
   <td style =" width:100%;">
    
   <table>
   <tr valign="middle">
   <td>
       <asp:Label ID="Label1" runat="server" Text="From" Font-Names="Arial" Font-Size="8pt"></asp:Label>
   
   </td>
   <td>
       <asp:TextBox ID="txtFrom" runat="server" Font-Names="Arial" Font-Size="8pt" 
           Width="75px"></asp:TextBox>
   </td>
    <td>
       <asp:Label ID="Label2" runat="server" Text="To" Font-Names="Arial" Font-Size="8pt"></asp:Label>
   
   </td>
   <td>
       <asp:TextBox ID="txtTo" runat="server" Font-Names="Arial" Font-Size="8pt" 
           Width="75px"></asp:TextBox>
   </td>
  
  
    <td>
       <asp:Label ID="Label4" runat="server" Text="Party Name" Font-Names="Arial" Font-Size="8pt"></asp:Label>
   
   </td>
   <td style="font-family:Arial; font-size:8pt;">
       <asp:TextBox ID="txtPName" runat="server" Font-Names="Arial" Font-Size="8pt" 
           Width="223px"></asp:TextBox>
   </td>
  <td>
   <asp:Label ID="Label3" runat="server" Text="Status" Font-Names="Arial" Font-Size="8pt"></asp:Label>
  </td>
  <td>
      <asp:DropDownList ID="chkStatus" runat="server" Font-Names="Arial" 
          Font-Size="8pt" Width="90px">
       <asp:ListItem Value ="All" Text="~Select~" Selected="True"></asp:ListItem> 
    <asp:ListItem Value="Active" Text="Active"></asp:ListItem>
       <asp:ListItem Value="In Active" Text="In Active"></asp:ListItem>
      
      </asp:DropDownList>
     
     
  </td>
   <td>
   
       <asp:Button ID="btnSearch" runat="server" Text="Search" Font-Names="Arial" 
           Font-Size="8pt" onclick="btnSearch_Click" CssClass="button_image1" 
           Height="25px" />
       
   </td>
   <td>
   
       <asp:Button ID="btnExport" runat="server" Text="Export to Excel" 
           Font-Names="Arial" Font-Size="8pt" onclick="btnExport_Click" 
           CssClass="button_image1" Height="25px" />
       
   </td>
   </tr>
   </table>
  
   </td>
   </tr>
   <tr>
   <td style="width:100%;">
   <div style=" height:490px; width:100%" runat="server" id="DivTag" class="grid_scroll">
   
   <asp:GridView ID="gvSelect" runat="server" AutoGenerateColumns="False"    
           BorderColor="Gainsboro" BorderStyle="Solid" 
        BorderWidth="1px" Font-Size="8pt" ForeColor="Black" GridLines="Vertical"  DataKeyNames="contr_code"
        ShowFooter="false" ShowHeader="true" Visible="True" CellPadding="1" 
           onselectedindexchanged="gvReport_SelectedIndexChanged" Width="90%">
        <FooterStyle BackColor="#507CD1" BorderStyle="None" Font-Bold="True"  Width="99%" ForeColor="White" />
            <Columns>
                   <asp:BoundField DataField="contr_code" HeaderText="Code" SortExpression="Type">
                   <ItemStyle Width="80px" />
                   </asp:BoundField>
                    <asp:BoundField DataField="contr_name" HeaderText="Contract Name" SortExpression="Desc">
                    <ItemStyle Wrap="false"/>
                   </asp:BoundField>
                    <asp:BoundField DataField="customer_name" HeaderText="Party Name" SortExpression="Type" >
                    <ItemStyle Wrap="false"/>
                   </asp:BoundField>
                    <asp:BoundField DataField="approved_by" HeaderText="Approved By" SortExpression="Desc">
                    <ItemStyle Wrap="false"/>
                   </asp:BoundField>
                    <asp:BoundField DataField="contr_valid_from" HeaderText="Valid From" SortExpression="Type">
                    <ItemStyle Wrap="false"/>
                   </asp:BoundField>
                    <asp:BoundField DataField="contr_valid_to" HeaderText="Valid To" SortExpression="Desc">
                    <ItemStyle Wrap="false"/>
                   </asp:BoundField>
                    <asp:BoundField DataField="contr_status" HeaderText="Status" SortExpression="Type" />                   
                    <asp:CommandField ButtonType="Link" ItemStyle-Font-Bold="false" 
                        SelectText="View" ItemStyle-Font-Names="Arial" ShowSelectButton="true" >
                               
<ItemStyle Font-Names="Arial" Font-Bold="False"></ItemStyle>
                    </asp:CommandField>
                               
            </Columns>
            <RowStyle Font-Bold="False" HorizontalAlign="Left" Font-Names="Arial" 
            Font-Size="8pt" />
        <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#3399FF" 
            Font-Names="Arial" Font-Size="8pt" />
        <RowStyle BackColor="Snow" BorderColor="Gainsboro" BorderStyle="Solid" 
            BorderWidth="1px" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1ECF8" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
        
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
   <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" 
           BorderColor="Gainsboro" BorderStyle="Solid" 
        BorderWidth="1px" Font-Size="8pt" ForeColor="Black" GridLines="Vertical"  DataKeyNames="contr_code"
        ShowFooter="false" ShowHeader="true" Visible="False" CellPadding="1" 
           Width="99%">
        <FooterStyle BackColor="#507CD1" BorderStyle="None" Font-Bold="True" ForeColor="White" />
            <Columns>
                    <asp:BoundField DataField="contr_code" HeaderText="Contract Code" SortExpression="Type" />
                    <asp:BoundField DataField="contr_name" HeaderText="Contract Name" SortExpression="Desc" />
                    <asp:BoundField DataField="customer_name" HeaderText="Party Name" SortExpression="Type" />
                    <asp:BoundField DataField="approved_by" HeaderText="Approved By" SortExpression="Desc" />
                    <asp:BoundField DataField="contr_valid_from" HeaderText="Valid From" SortExpression="Type" />
                    <asp:BoundField DataField="contr_valid_to" HeaderText="Valid To" SortExpression="Desc" />
                    <asp:BoundField DataField="contr_status" HeaderText="Status" SortExpression="Type" />
                                                  
            </Columns>
            <RowStyle Font-Bold="False" HorizontalAlign="Left" Font-Names="Arial" 
            Font-Size="8pt" />
        <HeaderStyle Font-Bold="True" ForeColor="Black" />
        <RowStyle BackColor="Snow" BorderColor="Gainsboro" BorderStyle="Solid" 
            BorderWidth="1px" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1ECF8" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
        <HeaderStyle BackColor="White" Font-Bold="True" Font-Names="arial" 
            Font-Size="8pt" ForeColor="Black" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
   
       <br />
       
  
   
   </div>
   </td>
   </tr>
   </table>
    <cc1:CalendarExtender runat="server" ID="CE1" TargetControlID="txtFrom" Format="MM/dd/yyyy" ></cc1:CalendarExtender>
       <cc1:CalendarExtender runat="server" ID="CE2" TargetControlID="txtTo" Format="MM/dd/yyyy" ></cc1:CalendarExtender>
            <cc1:AutoCompleteExtender ID="ACE1" runat="server" EnableCaching="true" MinimumPrefixLength="1" ServiceMethod="GetContractParty" 
                    ServicePath="~/AutoComplete.asmx" TargetControlID="txtPName"></cc1:AutoCompleteExtender>
    </contenttemplate>
    <Triggers>
    <asp:PostBackTrigger ControlID="btnExport" />
    
    </Triggers>
    </asp:UpdatePanel>   
   </asp:Content> 
   
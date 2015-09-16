<%@ Page Language="C#" MasterPageFile="~/ImpexCube.Master" EnableEventValidation="false"  AutoEventWireup="true" Inherits="ImpexCube.frmOutwardDetails" Title=":: Front Desk || OUTWARD DETAILS" Codebehind="frmOutwardDetails.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table >
<tr>
<td style=" border-bottom: thick solid Red; width: 878px;" align="center">
    <asp:Label ID="Label5" runat="server" Text="Documents Outward Details - Panel" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="#2461bf"></asp:Label></td>
</tr>
<tr >
<td style=" border-bottom: thick solid Red; vertical-align: top; width: 878px;">
   
    <table>
<tr>
<td>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table>
    <tr>
    <td align="left" style="width: 52px" >
    <asp:Label ID="Label7" runat="server" Text="From Date" Font-Names="Arial" Font-Size="8pt" Width="56px"></asp:Label>
</td>

<td align="left" >
    <asp:TextBox ID="txtFrom" runat="server" Width="70px" Font-Names="Arial" 
        Font-Size="8pt" AutoPostBack="True" ontextchanged="txtFrom_TextChanged"></asp:TextBox></td>
<td align="left" style="width: 52px">
    <asp:Label ID="Label9" runat="server" Font-Names="Arial" Font-Size="8pt"
        Text="To Date " Width="48px"></asp:Label></td>
        
<td align="left" >
    <asp:TextBox ID="txtTo" runat="server" Font-Names="Arial" Font-Size="8pt" 
        Width="70px" AutoPostBack="True" ontextchanged="txtTo_TextChanged"></asp:TextBox></td>
     <td><asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt"
        Text="Consignee Name " Width="85px"></asp:Label></td>
    <td>
    <asp:DropDownList ID="drConsignee" Font-Names="arial" Font-Size="8pt" runat="server" Width="200px">
                                                            </asp:DropDownList>
    </td>
    </tr>
    </table>
     <cc1:CalendarExtender ID="CE1" runat="server" TargetControlID="txtFrom"  Format="dd/MM/yyyy">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CE2" runat="server" TargetControlID="txtTo"  Format="dd/MM/yyyy">
    </cc1:CalendarExtender>
         <cc1:FilteredTextBoxExtender ID="FTEiDate" TargetControlID="txtFrom" FilterType="Custom" ValidChars="0123456789/" runat="server">
                      </cc1:FilteredTextBoxExtender>
                      <cc1:FilteredTextBoxExtender ID="FTEoDate" TargetControlID="txtTo" FilterType="Custom" ValidChars="0123456789/" runat="server">
                      </cc1:FilteredTextBoxExtender>
    </ContentTemplate>
    </asp:UpdatePanel>
</td>
   <td >
    <asp:Button ID="BtnSearch" runat="server" Text="Search" Height="24px" 
           OnClick="BtnSearch_Click" Width="80px" Font-Names="Arial" Font-Size="8pt" />
    </td>
    <td>
        <asp:Button id="Export" onclick="Export_Click" runat="server" 
           Text="Export to Excel" Width="100px" Height="25px" Font-Names="Arial" 
           Font-Size="8pt"></asp:Button></td>
    <td>
        <asp:Button ID="BtnCancel" runat="server" Height="25px" OnClick="BtnCancel_Click"
        Text="Cancel" Width="80px" Font-Names="Arial" Font-Size="8pt" />
        </td> 
</tr>

</table>
    
  
</td>

</tr>

<tr style="border: solid 1px red;">

<td style="height: 307px; vertical-align: top; width: 878px;">
<div id="GridScroll" class="grid_scroll" style="width: 806px">
<asp:GridView ID="GrdRpt" runat="server" AutoGenerateColumns="False" BorderColor="#CCCCCC"
            BorderStyle="None" BorderWidth="1px" CellPadding="3" 
        Font-Names="Arial" Font-Size="8pt"
            Font-Strikeout="False" Width="805px" PageSize="15" DataKeyNames="SNO" 
        BackColor="White" onrowdatabound="GrdRpt_RowDataBound">
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <Columns>
            
            <asp:BoundField DataField="sno" Visible="False"  HeaderText="SNO"  ReadOnly="True" SortExpression="SNO">
                    <ItemStyle HorizontalAlign="Left"  Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="S.No">
                <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
                <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:BoundField DataField="Consignee" HeaderText="Consignee Name"  SortExpression="Consignee">
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"  Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Date" HeaderText="Date"  SortExpression="Date">
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="city" HeaderText="City"  SortExpression="city">
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Details" HeaderText="Doc_Details"  SortExpression="details">
                    <ItemStyle HorizontalAlign="Left" Width="400px" Wrap="True"  />
                </asp:BoundField>
                <asp:BoundField DataField="jobno" HeaderText="Job No."  SortExpression="Jobno">
                    <ItemStyle HorizontalAlign="Left" Width="100px" VerticalAlign="Top" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="awbno" HeaderText="AWB No."  SortExpression="awbno">
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="remarks" HeaderText="Remarks" SortExpression="remarks">
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundField>
                 <asp:BoundField DataField="empcode" HeaderText="Entry By" ReadOnly="True" SortExpression="empcode">
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="sentby" HeaderText="Sender" ReadOnly="True" SortExpression="sentby">
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="False" />
                </asp:BoundField>
            </Columns>
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#719DDB" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
     </div> 
</td>
</tr>
<tr> 
<td style="width: 878px" >
    <asp:Label ID="lblresult" runat="server"></asp:Label>
</td>
</tr>
</table>
   
</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/Billing/MasterPage.master" AutoEventWireup="true"
    Inherits="BillingReport" Title="::PIPLBilling || Billing Report Status " Codebehind="frmBillingReport.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up1">
<ProgressTemplate>
            <span style="font-size: small; color: #000066">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            Please Wait...</span><asp:Image ID="Image123" runat="server" ImageUrl="~/image/progress.gif" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="up1" runat="server" >
    
<contenttemplate>

   
    <table >
       
        <tr>
        <td style="width: 725px" >
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                 <table>
                    <tr valign="middle">
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="From" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFrom" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                Width="60px"></asp:TextBox>
                            
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="To" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTo" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                Width="60px"></asp:TextBox>
                            
                        </td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Bill Type" Font-Names="Arial" 
                                Font-Size="8pt" Width="40px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpBType" runat="server" 
                                OnSelectedIndexChanged="drpBType_SelectedIndexChanged" Font-Names="Arial" 
                                Font-Size="8pt" AutoPostBack="True">
                                <asp:ListItem Text="~select~" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Invoice - Imports" Value="SB"></asp:ListItem>
                                 <asp:ListItem Value="EXPSB">Invoice - Exports</asp:ListItem>
                                <asp:ListItem Value="ATLSB">Apollo Invoice</asp:ListItem>
                                <asp:ListItem Text="Debit Note - Imports" Value="DB"></asp:ListItem>
                                <asp:ListItem Value="EXPDB">Debit Note - Exports</asp:ListItem>
                                <asp:ListItem Value="ATLDB">Apollo Debit Note </asp:ListItem>
                                <asp:ListItem Value="CD">Customs Bill</asp:ListItem>
                                <asp:ListItem Value="ATLDEM">Demurate</asp:ListItem>
                                <asp:ListItem Value="IA">Invoice All</asp:ListItem>
                                <asp:ListItem Value="DA">Debit Note All</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIMP" runat="server" AutoPostBack="true" Checked="false" 
                                Font-Names="Arial" Font-Size="8pt" OnCheckedChanged="chkIMP_CheckedChanged" 
                                Text="Importer" Width="60px" />
                        </td>
                        <td style="font-family: Arial; font-size: 8pt;">
                            <asp:TextBox ID="txtPName" runat="server" AutoPostBack="true" Font-Names="Arial"
                                Font-Size="8pt" Width="160px"></asp:TextBox>
                           
                        </td>
                        
                        <td>
                            <asp:CheckBox ID="chkJobNo" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="chkJobNo_CheckedChanged"
                                Font-Names="Arial" Font-Size="8pt" Text="Job No" Width="60px" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtJobNo" runat="server" Font-Names="Arial" Font-Size="8pt" Enabled="false"
                                Width="143px"></asp:TextBox>
                        </td>
                        
                        
                    </tr>
                </table>
                 <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFrom">
                            </cc1:CalendarExtender>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtTo">
                            </cc1:CalendarExtender>
                             <cc1:AutoCompleteExtender ID="ACE1" runat="server" EnableCaching="true" MinimumPrefixLength="1"
                                ServiceMethod="GetCompany" ServicePath="~/AutoComplete.asmx" TargetControlID="txtPName">
                            </cc1:AutoCompleteExtender>
                             <cc1:AutoCompleteExtender id="autoComplete1" runat="server" EnableCaching="true" MinimumPrefixLength="1" 
                             ServiceMethod="GetInvJobNo" CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted" 
                             CompletionListItemCssClass="listItem" ServicePath="~/AutoComplete.asmx" TargetControlID="txtJobNo">
        </cc1:AutoCompleteExtender>
                </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        <td align="left" style="vertical-align: top;">
                            <asp:Button ID="Btn_search" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                Text="Search" Width="70px" onclick="Btn_search_Click" Height="25px" 
                                CssClass="button_image1" />
                                </td>
         <td align="left" >
                            <asp:Button ID="btnExport" runat="server" Text="Export to Excel" Font-Names="Arial"
                                Font-Size="8pt" OnClick="btnExport_Click" Width="90px" Height="25px" 
                                CssClass="button_image1" />
                        </td>
                         <td align="left" >
                            <asp:Button ID="ExportPDF" runat="server" Text="Export to PDF" Font-Names="Arial"
                                Font-Size="8pt" Width="90px" Height="25px" onclick="ExportPDF_Click" 
                                 CssClass="button_image1" />
                        </td>
        </tr>
        <tr>
            <td colspan="4" align="left" >
               <div id="DivTag" runat="server" class="grid_scroll">
                    <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" BorderColor="#DEDFDE"
                        BorderStyle="None" BorderWidth="1px" Font-Size="7pt" CellPadding="4" BackColor="White"
                        OnSelectedIndexChanged="gvReport_SelectedIndexChanged" OnRowDataBound="gvReport_RowDataBound"
                        ForeColor="Black" GridLines="Vertical" ShowFooter="True" 
                        DataKeyNames="invoice">
                        <FooterStyle BackColor="Silver" BorderStyle="None" Font-Bold="True" 
                            Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" />
                        <Columns>
                         <asp:CommandField ButtonType="Link" ItemStyle-Font-Bold="false" SelectText="View"
                                ItemStyle-Font-Names="Arial" ShowSelectButton="true">
                                <ItemStyle Font-Names="Arial" Font-Bold="False"></ItemStyle>
                            </asp:CommandField>
                            <asp:BoundField DataField="invoice" HeaderText="Invoice" SortExpression="Type" />
                            <asp:BoundField DataField="invoiceDate" HeaderText="Date" SortExpression="Desc" />
                            <asp:BoundField DataField="compName" HeaderText="Party Name" SortExpression="Type">
                            <ItemStyle Wrap="true"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="jobno" HeaderText="Job No" SortExpression="Type" >
                             <ItemStyle Wrap="true"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="partyReff" HeaderText="Party Ref" SortExpression="Type" />
                            <asp:BoundField DataField="blno" HeaderText="BL No" SortExpression="Desc" />
                            <asp:BoundField DataField="BEnoDate" HeaderText="BE No Date" SortExpression="Type" />
                            <asp:BoundField DataField="importItem" HeaderText="Item Imported " SortExpression="Desc" />
                            <asp:BoundField DataField="Container_No" HeaderText="Container No" SortExpression="Type" />
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Type" />
                            <asp:BoundField DataField="ass_Value" HeaderText="Ass.Value" SortExpression="Desc">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="invoiceType" HeaderText="Type" SortExpression="invoiceType" />
                             <asp:TemplateField HeaderText="TOTAL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotal" runat="server" Font-Names="arial" Font-Size="8pt"></asp:Label>
                                                   
                                            </ItemTemplate >
                                        </asp:TemplateField>
                           
                              <asp:BoundField DataField="service_tax" HeaderText="Service Tax" SortExpression="Desc">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="edu_cess" HeaderText="EDU CESS" SortExpression="Desc">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="sec_chess" HeaderText="SHC CESS" SortExpression="Desc">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="grand_Total" HeaderText="GROSS TOTAL" SortExpression="Desc">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="less_advance" HeaderText="ADVANCE" SortExpression="Desc">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="net_Total" HeaderText="NET TOTAL" SortExpression="Desc">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                          
                           
                        </Columns>
                        <RowStyle HorizontalAlign="Left" Font-Names="Arial" Font-Size="7pt" BackColor="#F7F7DE" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#3399FF" Font-Names="Arial"
                            Font-Size="7pt" />
                        <RowStyle BackColor="Snow" BorderColor="Gainsboro" BorderStyle="Solid" BorderWidth="1px" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    <asp:GridView ID="gvReport0" runat="server" AutoGenerateColumns="False" BorderColor="#CCCCCC"
                        BorderStyle="None" BorderWidth="1px" Font-Size="8pt" CellPadding="3" BackColor="White"
                        OnRowDataBound="gvReport0_RowDataBound" ShowFooter="True" 
                        DataKeyNames="invoice" Font-Names="Arial">
                        <FooterStyle BackColor="White" BorderStyle="None" ForeColor="#000066" 
                            Font-Names="Arial" Font-Size="8pt" />
                        <Columns>
                            <asp:BoundField DataField="invoice" HeaderText="Invoice" SortExpression="Type" />
                            <asp:BoundField DataField="invoiceDate" HeaderText="Date" SortExpression="Desc" />
                            <asp:BoundField DataField="compName" HeaderText="Party Name" SortExpression="Type" />
                            <asp:BoundField DataField="jobno" HeaderText="Job No" SortExpression="Type" />
                           <asp:BoundField DataField="partyReff" HeaderText="Party Ref" SortExpression="Type" />
                          <asp:BoundField DataField="blno" HeaderText="BL No" SortExpression="Desc" />
                            <asp:BoundField DataField="BEnoDate" HeaderText="BE No Date" SortExpression="Type" />
                            <asp:BoundField DataField="importItem" HeaderText="Item Imported " SortExpression="Desc" />
                            <asp:BoundField DataField="Container_No" HeaderText="Container No" SortExpression="Type" />
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Type" />
                            <asp:BoundField DataField="ass_Value" HeaderText="Ass.Value" SortExpression="Desc">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="invoiceType" HeaderText="Type" SortExpression="invoiceType" />
                            <asp:TemplateField HeaderText="TOTAL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotal" runat="server" Font-Names="arial" Font-Size="8pt"></asp:Label>
                                                   
                                            </ItemTemplate >
                                        </asp:TemplateField>
                           
                              <asp:BoundField DataField="service_tax" HeaderText="Service Tax" SortExpression="Desc">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="edu_cess" HeaderText="EDU CESS" SortExpression="Desc">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="sec_chess" HeaderText="SHC CESS" SortExpression="Desc">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="grand_Total" HeaderText="GROSS TOTAL" SortExpression="Desc">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="less_advance" HeaderText="ADVANCE" SortExpression="Desc">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="net_Total" HeaderText="NET TOTAL" SortExpression="Desc">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <RowStyle HorizontalAlign="Left" Font-Names="Arial" Font-Size="8pt" 
                            ForeColor="#000066" />
                        <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                            ForeColor="White" BackColor="#006699" />
                        <RowStyle BackColor="Snow" BorderColor="Gainsboro" BorderStyle="Solid" BorderWidth="1px" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        
                        <EditRowStyle Font-Names="Arial" Font-Size="8pt" />
                        
                    </asp:GridView>
               </div>
            </td>
        </tr>
    </table>
   </contenttemplate>
   <Triggers>
   <asp:PostBackTrigger ControlID="btnExport"  />
   <asp:PostBackTrigger ControlID="ExportPDF" />
   </Triggers>
    </asp:UpdatePanel>   
                            
  </asp:Content> 

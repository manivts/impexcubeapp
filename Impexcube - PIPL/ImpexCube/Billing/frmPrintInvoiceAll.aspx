<%@ Page Language="C#" MasterPageFile="~/Billing/MasterPage.master" AutoEventWireup="true" Inherits="frmPrintInvoiceAll" Title="::PIPL || Print Billing" Codebehind="frmPrintInvoiceAll.aspx.cs" %>
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

     <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFrom">
                            </cc1:CalendarExtender>
        <table style="width:98%;">
            
            <tr style="background-color: Scrollbar;" >
            <td style="width:40px;" >
                <asp:Label ID="Label7" runat="server" Font-Names="arial" Font-Size="8pt" 
                    Text="From Date:" Width="55px"></asp:Label>
            </td>
            <td align="left" style="width:60px;" >
                <asp:TextBox ID="txtFrom" runat="server" Font-Names="arial" Font-Size="8pt" 
                    Width="60px"></asp:TextBox>
                
                </td>
            
            <td style="width:40px;">
                <asp:Label ID="Label8" runat="server" Font-Names="arial" Font-Size="8pt" 
                    Text="To:"></asp:Label>
                </td>
            <td align="left" style="width:60px;" >
           

                <asp:TextBox ID="txtTo" runat="server" Font-Names="arial" Font-Size="8pt" 
                    Width="60px"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtTo">
                </cc1:CalendarExtender>
                </td>
            <td style="width:40px;">
                <asp:Label ID="Label6" runat="server" Font-Names="arial" Font-Size="8pt" 
                    Text="Shipment Type" Width="75px"></asp:Label>
                </td>
                <td style="width:70px;">
                
                    <asp:RadioButtonList ID="rbSHp" runat="server" AutoPostBack="True" 
                        Font-Names="Arial" Font-Size="8pt" 
                        onselectedindexchanged="rbSHp_SelectedIndexChanged" 
                        RepeatDirection="Horizontal" Width="110px">
                        <asp:ListItem Value="IMP">Import</asp:ListItem>
                        <asp:ListItem Value="EXP">Export</asp:ListItem>
                    </asp:RadioButtonList>
                
                </td>
                <td style="width:50px;">
                
                    <asp:Label ID="Label9" runat="server" Font-Names="arial" Font-Size="8pt" 
                        Text="Bill Type" Width="40px"></asp:Label>
                
                </td>
                <td style="width:80px;">
                
                    <asp:RadioButtonList ID="rbBill" runat="server" AutoPostBack="True" 
                        Font-Names="Arial" Font-Size="8pt" 
                        OnSelectedIndexChanged="rbBill_SelectedIndexChanged" 
                        RepeatDirection="Horizontal" Width="150px">
                        <asp:ListItem Value="SB">Invoice</asp:ListItem>
                        <asp:ListItem Value="DB">Debit Note</asp:ListItem>
                    </asp:RadioButtonList>
                
                </td>
                <td style="width:600px;">
                
                </td>
            </tr>
            <tr>
                <td align="right" style="width:70px;">
                    <asp:CheckBox ID="chkImp" runat="server" AutoPostBack="True" Font-Names="arial" 
                        Font-Size="8pt" oncheckedchanged="chkImp_CheckedChanged" Text="Importer" />
                </td>
               
                <td colspan="8" align="left" style="height: 26px">
                   
                    <asp:TextBox ID="txtPname" runat="server" Font-Names="arial" Font-Size="8pt" 
                        Width="170px"></asp:TextBox>
                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" 
                        CompletionListCssClass="completionList" 
                        CompletionListHighlightedItemCssClass="itemHighlighted" 
                        CompletionListItemCssClass="listItem" EnableCaching="true" 
                        MinimumPrefixLength="0" ServiceMethod="GetPartyEDIT" 
                        ServicePath="~/AutoComplete.asmx" TargetControlID="txtPname">
                    </cc1:AutoCompleteExtender>
               
                       <asp:Button ID="BtnSubmit" runat="server" Font-Names="arial" Font-Size="8pt" 
                           OnClick="BtnSubmit_Click" Text="Search" Width="76px" 
                        CssClass="button_image1" Height="25px" />
                
            
                <asp:Button ID="BtnPDF" runat="server" Text="Export PDF" Font-Names="arial" Font-Size="8pt"
                        Width="76px" CausesValidation="False" onclick="BtnPDF_Click" 
                        CssClass="button_image1" Height="25px" />
                
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Names="arial" Font-Size="8pt"
                        Width="76px" PostBackUrl="~/index.aspx" CausesValidation="False" 
                        CssClass="button_image1" Height="25px" />
                </td>
            </tr>
            <tr>
            <td align="left"  colspan="9">
            <div id="DivTag" runat="server" style="height: 450px;" class="grid_scroll">
                <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" 
                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="4" DataKeyNames="invoice" Font-Size="8pt" ForeColor="Black" 
                    GridLines="Vertical" OnSelectedIndexChanged="gvReport_SelectedIndexChanged" 
                    ShowFooter="false" onrowdatabound="gvReport_RowDataBound">
                    <FooterStyle BackColor="Silver" BorderStyle="None" Font-Bold="True" 
                        Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" />
                    <Columns>
                        
                        <asp:BoundField DataField="invoice" HeaderText="Invoice" 
                            SortExpression="Type" />
                        <asp:BoundField DataField="invoiceDate" HeaderText="Date" 
                            SortExpression="Desc" />
                        <asp:BoundField DataField="compName" HeaderText="Party Name" 
                            SortExpression="Type">
                            <ItemStyle Wrap="true" />
                        </asp:BoundField>
                        <asp:BoundField DataField="jobno" HeaderText="Job No" SortExpression="Type">
                            <ItemStyle Wrap="true" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="blno" HeaderText="BL No" SortExpression="Desc" />
                        <asp:BoundField DataField="BEnoDate" HeaderText="BE No Date" 
                            SortExpression="Type" />
                        <asp:BoundField DataField="importItem" HeaderText="Item Imported " 
                            SortExpression="Desc" />
                        
                    </Columns>
                    <RowStyle BackColor="#F7F7DE" Font-Names="Arial" Font-Size="8pt" 
                        HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#3399FF" Font-Bold="True" Font-Names="Arial" 
                        Font-Size="7pt" ForeColor="White" />
                    <RowStyle BackColor="Snow" BorderColor="Gainsboro" BorderStyle="Solid" 
                        BorderWidth="1px" />
                    <SelectedRowStyle BackColor="#FF99FF" Font-Bold="True" ForeColor="#000066" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                </div> 
            </td>
            </tr>
        </table>
        </contenttemplate>
        </asp:UpdatePanel>
   
</asp:Content>

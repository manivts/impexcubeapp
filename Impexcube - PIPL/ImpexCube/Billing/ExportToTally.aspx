<%@ Page Language="C#" MasterPageFile="~/Billing/MasterPage.master" AutoEventWireup="true" Inherits="ExportToTally" Title=":: PIPL || EXPORT BILLING FORMAT - TALLY" Codebehind="ExportToTally.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" 
        AssociatedUpdatePanelID="up1">
<ProgressTemplate>
            <span style="font-size: small; color: #000066">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            Please Wait</span><asp:Image ID="Image123" runat="server" ImageUrl="~/image/progress.gif" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="up1" runat="server" >
    
<contenttemplate>--%>
    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFrom">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtTo">
    </cc1:CalendarExtender>
    
    <table>
        <tr>
        <td align="left" >
        <table>
        <tr>
            <td align="left" >
                <asp:Label ID="Label1" runat="server" Text="From Date:" Font-Names="arial" 
                    Font-Size="8pt" Width="55px"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtFrom" runat="server" Font-Names="arial" Font-Size="8pt" 
                    Width="70px"></asp:TextBox>
            </td>
            <td align="left">
                <asp:Label ID="Label2" runat="server" Text="To Date:" Font-Names="arial" 
                    Font-Size="8pt" Width="40px"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtTo" runat="server" Font-Names="arial" Font-Size="8pt" 
                    Width="70px"></asp:TextBox>
            </td>
           <td colspan="6" align="left" >
               <asp:UpdatePanel ID="UpdatePanel1" runat="server">
               <ContentTemplate>
                <td align="left">
                <asp:Label ID="Label3" runat="server" Text="Bill Type:" Font-Names="arial" Font-Size="8pt"></asp:Label>
            </td>
            <td align="left">
                <asp:DropDownList ID="drBill" runat="server" 
                                OnSelectedIndexChanged="drBill_SelectedIndexChanged" Font-Names="Arial" 
                                Font-Size="8pt" AutoPostBack="True">
                                <asp:ListItem Text="~select~" Value="0" Selected="True"></asp:ListItem>
                               <asp:ListItem Text="Invoice - Imports" Value="SB"></asp:ListItem>
                                <asp:ListItem Value="EXPSB">Invoice - Exports</asp:ListItem>
                                <asp:ListItem Text="Debit Note - Imports" Value="DB"></asp:ListItem>
                                <asp:ListItem Value="EXPDB">Debit Note - Exports</asp:ListItem>
                                <asp:ListItem Value="CD">Customs Bill</asp:ListItem>
                            </asp:DropDownList>
            </td>
              <td>
                            <asp:CheckBox ID="chkIMP" runat="server" AutoPostBack="true" Checked="false" 
                                Font-Names="Arial" Font-Size="8pt" OnCheckedChanged="chkIMP_CheckedChanged" 
                                Text="Importer" Width="60px" />
                        </td>
                        <td style="font-family: Arial; font-size: 8pt;">
                            <asp:TextBox ID="txtPName" runat="server" AutoPostBack="true" Font-Names="Arial"
                                Font-Size="8pt" Width="150px"></asp:TextBox>
                           <cc1:AutoCompleteExtender ID="ACE1" runat="server" EnableCaching="true" MinimumPrefixLength="1"
                                ServiceMethod="GetCompanyTally" ServicePath="~/AutoComplete.asmx" TargetControlID="txtPName">
                            </cc1:AutoCompleteExtender>
                        </td>
           <td align="left">
                &nbsp;</td>
            <td align="left">
             <asp:CheckBox ID="chkJNO" runat="server" Font-Names="Arial" Font-Size="7pt" 
                    AutoPostBack="True" Text="JOB" oncheckedchanged="chkJNO_CheckedChanged" />
                <asp:TextBox ID="txtJNO" runat="server" Font-Names="arial" Font-Size="8pt" 
                    Width="130px"></asp:TextBox>
                     <cc1:AutoCompleteExtender ID="autoComplete1" runat="server" EnableCaching="true"
        MinimumPrefixLength="1" ServiceMethod="GetBillJNOTALLY" CompletionListCssClass="completionList"
        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
        ServicePath="~/AutoComplete.asmx" TargetControlID="txtJNO">
        </cc1:AutoCompleteExtender>
            </td>
               </ContentTemplate>
               </asp:UpdatePanel>
           
           </td>
            <td align="left">
                <asp:Button ID="BtnSearch" runat="server" Text="Generate " Font-Names="arial" Font-Size="8pt"
                    OnClick="BtnSearch_Click" Width="82px" CssClass="button_image1" 
                    Height="25px" />
            </td>
            <td align="left">
                <asp:Button ID="ExportTally" runat="server" Text="Convert to Tally" 
                    Font-Names="arial" Font-Size="8pt"
                    Width="100px" CssClass="button_image1" 
                    Height="25px" onclick="ExportTally_Click" />
                </td>
           
        </tr>
        </table>
        </td>
        </tr>
       
        <tr>
            <td align="left" colspan="10">
                <asp:GridView ID="Grdiworkreg" runat="server" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Names="Arial" Font-Size="8pt"
                    AutoGenerateColumns="False" onrowdatabound="Grdiworkreg_RowDataBound" 
                    DataKeyNames="invoice" ShowFooter="True">
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" Font-Size="8pt" />
                    <Columns>
                        
                        <asp:BoundField DataField="invoiceDate" HeaderText="BILL DATE" SortExpression="invoiceDate" />
                        <asp:BoundField DataField="jobNo" HeaderText="REFERENCE" SortExpression="jobNo" />
                        <asp:BoundField DataField="compName" HeaderText="PARTY NAME" SortExpression="compName" />
                        <asp:BoundField DataField="invoiceNo" HeaderText="NEW REF" SortExpression="invoiceNo" />
                        <asp:BoundField DataField="NET_TOTAL" HeaderText="TOTAL AMOUNT" SortExpression="NET_TOTAL">
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="charge_desc" HeaderText="PARTICULARS" SortExpression="charge_desc" />
                        <asp:BoundField DataField="amount" HeaderText="AMOUNT" SortExpression="amount">
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="NARATION">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "blno") %>
                                /
                                <%# DataBinder.Eval(Container.DataItem, "beNoDate") %>
                                /
                                <%# DataBinder.Eval(Container.DataItem, "importItem") %>
                                /
                                <%# DataBinder.Eval(Container.DataItem, "Quantity") %>
                                /
                                <%# DataBinder.Eval(Container.DataItem, "partyReff") %>
                               
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
<%--    </contenttemplate>
    </asp:UpdatePanel> --%>
   
</asp:Content>

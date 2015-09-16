<%@ Page Language="C#" MasterPageFile="~/Billing/MasterPage.master" AutoEventWireup="true" Inherits="frmEditInvoice" Title="::PIPL || Edit Bill" Codebehind="frmEditInvoice.aspx.cs" %>
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
<cc1:AutoCompleteExtender ID="autoComplete2" runat="server" TargetControlID="txtInvoiceNo"
        ServicePath="~/AutoComplete.asmx" ServiceMethod="GetInvNoEDIT" MinimumPrefixLength="0"
        EnableCaching="true" CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
        CompletionListItemCssClass="listItem">
    </cc1:AutoCompleteExtender>
    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtBE"
        ServicePath="~/AutoComplete.asmx" ServiceMethod="GetBeEDIT" MinimumPrefixLength="0"
        EnableCaching="true" CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
        CompletionListItemCssClass="listItem">
    </cc1:AutoCompleteExtender>
   
    <cc1:AutoCompleteExtender ID="autoComplete4" runat="server" TargetControlID="txtPname"
        ServicePath="~/AutoComplete.asmx" ServiceMethod="GetPartyEDIT" MinimumPrefixLength="0"
        EnableCaching="true" CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
        CompletionListItemCssClass="listItem">
    </cc1:AutoCompleteExtender>
     <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtJobNo"
        ServicePath="~/AutoComplete.asmx" ServiceMethod="GetInvJobNoEdit" MinimumPrefixLength="0"
        EnableCaching="true" CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
        CompletionListItemCssClass="listItem">
    </cc1:AutoCompleteExtender>
   
        <table>
            
            <tr style="background-color: Scrollbar;" >
            <td style="height: 30px;">
            <asp:Label ID="Label6" runat="server" Font-Names="arial" Font-Size="8pt" 
                    Text="Shipment"></asp:Label>
            </td>
            <td align="left" style="height: 30px" >
            <asp:RadioButtonList ID="rbSHp" runat="server" Font-Names="Arial" Font-Size="8pt" 
        RepeatDirection="Horizontal" AutoPostBack="True" 
                    onselectedindexchanged="rbSHp_SelectedIndexChanged" Width="110px" >
            <asp:ListItem Value="IMP">Import</asp:ListItem>
            <asp:ListItem Value="EXP">Export</asp:ListItem>
        </asp:RadioButtonList>
            </td>
            
            <td style="width:50px; height: 30px;">
                <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="8pt" Text="Bill Type"></asp:Label>
            </td>
            <td align="left" style="height: 30px" >
           

<asp:RadioButtonList ID="rbBill" runat="server" Font-Names="Arial" Font-Size="8pt" 
        RepeatDirection="Horizontal" AutoPostBack="True" 
        OnSelectedIndexChanged="rbBill_SelectedIndexChanged" Width="140px">
            <asp:ListItem Value="SB">Invoice</asp:ListItem>
            <asp:ListItem Value="DB">Debit Note</asp:ListItem>
        </asp:RadioButtonList>

            </td>
            <td colspan="7" style="height: 30px"></td>
            </tr>
            <tr>
                <td align="right" style="height: 26px">
                    <asp:CheckBox ID="chkBill" Text="Bill No" Font-Names="arial" Font-Size="8pt" 
                        runat="server" AutoPostBack="True" 
                        oncheckedchanged="chkBill_CheckedChanged" Width="50px" />
                   
                </td>
               
                <td align="left" style="height: 26px">
                    <asp:TextBox ID="txtInvoiceNo" Font-Names="arial" Font-Size="8pt" runat="server"
                        Width="100px"></asp:TextBox>
                </td>
                <td style="height: 26px">
                     <asp:CheckBox ID="chkBE" Text="BE No" Font-Names="arial" Font-Size="8pt" 
                         runat="server" oncheckedchanged="chkBE_CheckedChanged" 
                         AutoPostBack="True" Width="60px" />

                </td>
                <td align="left" style="height: 26px">
                    <asp:TextBox ID="txtBE" Font-Names="arial" Font-Size="8pt" runat="server"
                        Width="140px"></asp:TextBox>
                </td>
                   <td style="width:60px; height: 26px;">
                     <asp:CheckBox ID="chkImp" Text="Importer" Font-Names="arial" Font-Size="8pt" 
                           runat="server" AutoPostBack="True" 
                           oncheckedchanged="chkImp_CheckedChanged" Width="60px" />

                </td>
                <td align="left" style="height: 26px">
                    <asp:TextBox ID="txtPname" Font-Names="arial" Font-Size="8pt" runat="server"
                        Width="150px"></asp:TextBox>
                </td>
            <td>
            <asp:CheckBox ID="chkJobs" Text="JobNo" Font-Names="arial" Font-Size="8pt" 
                           runat="server" AutoPostBack="True" Width="50px" 
                    oncheckedchanged="chkJobs_CheckedChanged"  />
            </td>
            <td>
             <asp:TextBox ID="txtJobNo" Font-Names="arial" Font-Size="8pt" runat="server"
                        Width="120px"></asp:TextBox>
            </td>
                
                <td align="left" style="width: 447px; height: 26px;">
                    <asp:Button ID="BtnSubmit" runat="server" Text="Search" Font-Names="arial" Font-Size="8pt"
                        Width="76px" OnClick="BtnSubmit_Click" CssClass="button_image1" 
                        Height="25px" />
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Names="arial" Font-Size="8pt"
                        Width="76px" PostBackUrl="~/index.aspx" CausesValidation="False" 
                        CssClass="button_image1" Height="25px" />
                </td>
            </tr>
            <tr>
            <td colspan="9">
            <div id="DivTag" runat="server" class="grid_scroll">
                <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" 
                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="4" DataKeyNames="invoice" Font-Size="8pt" ForeColor="Black" 
                    GridLines="Vertical" OnSelectedIndexChanged="gvReport_SelectedIndexChanged" 
                    ShowFooter="false" onrowdatabound="gvReport_RowDataBound">
                    <FooterStyle BackColor="Silver" BorderStyle="None" Font-Bold="True" 
                        Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" />
                    <Columns>
                        <asp:CommandField ButtonType="Link" ItemStyle-Font-Bold="false" 
                            ItemStyle-Font-Names="Arial" SelectText="View" ShowSelectButton="true">
                            <ItemStyle Font-Bold="False" Font-Names="Arial" />
                        </asp:CommandField>
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
                        <asp:BoundField DataField="invoiceType" HeaderText="Bill" 
                            SortExpression="Desc" />
                            <asp:BoundField DataField="contr_code" HeaderText="Bill Code" 
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

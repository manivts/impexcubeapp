<%@ Page Language="C#" MasterPageFile="~/Billing/MasterPage.master" AutoEventWireup="true" Inherits="Enclose" Codebehind="Enclose.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
      
        <cc1:AutoCompleteExtender ID="autoComplete1" runat="server" CompletionListCssClass="completionList"
            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
            EnableCaching="true" MinimumPrefixLength="1" ServiceMethod="GetParty" ServicePath="~/AutoComplete.asmx"
            TargetControlID="txtPName">
        </cc1:AutoCompleteExtender>
       
        <table style="width: 100%;">
           
           
            <tr>
                <td align="right">
                    <asp:LinkButton ID="Print" runat="server" CausesValidation="False" OnClick="Print_Click">Print All</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Panel ID="PLDATA" Width="700px" Height="880px" runat="server">
                        <table>
                            <tr>
                                <td align="left">
                                    <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 679px;">
                                        <tbody>
                                            <tr style="border-color: #2461BF; border-style: solid; border-width: 1px;">
                                                <td rowspan="2" style="width: 77px">
                                                    <asp:Image ID="Image1" runat="server" Height="122px" ImageUrl="~/image/plogo.jpg"
                                                        Width="80px" />
                                                </td>
                                                <td align="center" style="width: 586px">
                                                    <asp:Label ID="lblCompName" runat="server" Text="" Font-Size="18pt"
                                                        Font-Names="Verdana"></asp:Label><br />
                                                    <asp:Label ID="lblCHANO" runat="server" Font-Size="10pt" 
                                                        Text=""></asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblSTRegno" runat="server" Font-Size="10pt" 
                                                        Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 586px; height: 52px;">
                                                    <hr style="border-right: #2461bf thin solid; border-top: #2461bf thin solid; border-left: #2461bf thin solid;
                                                        border-bottom: #2461bf thin solid; background-color: #2461bf" />
                                                    <asp:Label ID="lbladdress" runat="server" Font-Names="Verdana" Font-Size="8pt"                                                
                                                        Text=""></asp:Label>
                                                    <br />
                                                    <asp:Label ID="lbladdress1" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        
                                                        
                                                        Text=""></asp:Label>
                                                    <br />
                                                    <br />
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 679px;">
                                        <tr>
                                            <td class="style1">
                                                <asp:Label ID="Label45" runat="server" Font-Names="Arial" Font-Size="8pt" Text="To. M/s."></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPName" runat="server" Font-Names="Arial" Font-Size="8pt" Width="254px"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label19" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Date :"></asp:Label>
                                                <asp:TextBox ID="txtDate" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Kind Attn"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtKindAttn" runat="server" Font-Names="Arial" Font-Size="8pt" Width="254px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Dear Sir,"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                            </td>
                                            <td align="left" colspan="2">
                                                <asp:Label ID="Label46" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Please find enclosed here the following documents against your undermentioned "
                                                    Width="391px" Height="16px"></asp:Label>
                                                <asp:TextBox ID="txtStype" runat="server" Font-Names="Arial" Font-Size="8pt" Width="77px">Import/Export</asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="3">
                                                <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Shipment"
                                                    against="" documents="" enclosed="" find="" following="" here="" impor="" please=""
                                                    the="" undermentioned="" your=""></asp:Label>
                                                <asp:TextBox ID="txtCommodity" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                    Width="300px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="3">
                                                <asp:Label ID="Label9" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Shipper / Consignee / Name"
                                                    against="" documents="" enclosed="" find="" following="" here="" impor="" please=""
                                                    the="" undermentioned="" your=""></asp:Label>
                                                <asp:TextBox ID="txtConsignee" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                    Width="300px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr style="background-color: Red; height: 10px;">
                                            <td colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="3">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Invoice NO."
                                                                against="" documents="" enclosed="" find="" following="" here="" impor="" please=""
                                                                the="" undermentioned="" your=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtInvNo" runat="server" Font-Names="Arial" Font-Size="8pt" Width="96px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label11" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Date :"
                                                                against="" documents="" enclosed="" find="" following="" here="" impor="" please=""
                                                                the="" undermentioned="" your=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtInvDate" runat="server" Font-Names="Arial" Font-Size="8pt" Width="96px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label12" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Job No :"
                                                                against="" documents="" enclosed="" find="" following="" here="" impor="" please=""
                                                                the="" undermentioned="" your=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtInvJob" runat="server" Font-Names="Arial" Font-Size="8pt" Width="96px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label13" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Debit Note NO."
                                                                against="" documents="" enclosed="" find="" following="" here="" impor="" please=""
                                                                the="" undermentioned="" your=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDebit" runat="server" Font-Names="Arial" Font-Size="8pt" Width="96px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label14" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Date :"
                                                                against="" documents="" enclosed="" find="" following="" here="" impor="" please=""
                                                                the="" undermentioned="" your=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDebitDate" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Width="96px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label15" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Job No :"
                                                                against="" documents="" enclosed="" find="" following="" here="" impor="" please=""
                                                                the="" undermentioned="" your=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDebitJob" runat="server" Font-Names="Arial" Font-Size="8pt" Width="96px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label16" runat="server" Font-Names="Arial" Font-Size="8pt" Text="TR6- Challan NO."
                                                                against="" documents="" enclosed="" find="" following="" here="" impor="" please=""
                                                                the="" undermentioned="" your=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTRNo" runat="server" Font-Names="Arial" Font-Size="8pt" Width="96px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr style="background-color: Red; height: 10px;">
                                            <td colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                                CellPadding="4" Font-Names="Arial" Font-Size="8pt" GridLines="Horizontal" Width="154px"
                                                                ShowHeader="False">
                                                                <FooterStyle BackColor="White" ForeColor="#333333" />
                                                                <RowStyle BackColor="White" ForeColor="#333333" />
                                                                <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                                <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSel" Font-Names="arial" Font-Size="8px" Width="20px" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="t1" SortExpression="t1">
                                                                        <ItemStyle Wrap="false" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                        <td>
                                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                                CellPadding="4" Font-Names="Arial" Font-Size="8pt" GridLines="Horizontal" Width="154px"
                                                                Font-Strikeout="False" Height="157px" ShowHeader="False">
                                                                <FooterStyle BackColor="White" ForeColor="#333333" />
                                                                <RowStyle BackColor="White" ForeColor="#333333" />
                                                                <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                                <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSel" Width="20px" Font-Names="arial" Font-Size="8px" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="t1" SortExpression="t1">
                                                                        <ItemStyle Wrap="false" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr style="background-color: Red; height: 10px;">
                                            <td colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Label ID="Label47" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Kindly acknowledge the receipt"
                                                    against="" documents="" enclosed="" find="" following="" here="" impor="" please=""
                                                    the="" undermentioned="" your=""></asp:Label>
                                                <br />
                                                <asp:Label ID="Label48" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Thanking you"
                                                    against="" documents="" enclosed="" find="" following="" here="" impor="" please=""
                                                    the="" undermentioned="" your=""></asp:Label>
                                                <br />
                                                <br />
                                                <asp:Label ID="Label49" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Your faithfully"
                                                    against="" documents="" enclosed="" find="" following="" here="" impor="" please=""
                                                    the="" undermentioned="" your=""></asp:Label><br />
                                                <asp:Label ID="Label1" runat="server" Text="For"></asp:Label>
                                                <asp:Label ID="lblCompName1" runat="server" Font-Names="Arial" Font-Size="8pt" Text=""
                                                    against="" documents="" enclosed="" find="" following="" here="" impor="" please=""
                                                    the="" undermentioned="" your=""></asp:Label><br />
                                                <br />
                                                <br />
                                                <asp:Label ID="Label18" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Authorised Signatory"
                                                    against="" documents="" enclosed="" find="" following="" here="" impor="" please=""
                                                    the="" undermentioned="" your=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3">
                                                <asp:Button ID="BtnSubmit" runat="server" Text="Continue..." OnClick="BtnSubmit_Click" />
                                                <asp:Button ID="BtnPrint" runat="server" Text="Print" Width="67px" OnClick="BtnPrint_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:LinkButton ID="Print1" runat="server" CausesValidation="False" OnClick="Print_Click">Print All</asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtInvDate">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDebitDate">
    </cc1:CalendarExtender>
   
</asp:Content> 
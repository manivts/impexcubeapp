<%@ Page Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="Receipt.aspx.cs" Inherits="ImpexCube.Accounts.Receipt" Title="Receipt Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style11
        {
    }
        .style8
        {
            width: 35px;
        }
        .style4
        {
            height: 26px;
        }
        .style6
        {
            width: 297px;
        }
        .completionList
        {
            border: solid 1px #444444;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
            background-color: white;
            font-family: verdana;
            font-size: 8px;
        }
        .listItem
        {
            color: #666666;
            background-color: white;
            font-family: verdana;
            font-size: 12px;
        }
        .itemHighlighted
        {
            background-color: #ffc0c0;
        }
        .style12
        {
            font-size: small;
        }
        .style13
        {
            font-size: 10pt;
        }
        .Hide
        {
            display: none;
        }
        .ajax__combobox_buttoncontainer button
        {
            background-image: url(mvwres://AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e/AjaxControlToolkit.ComboBox.arrow-down.gif);
            background-position: center;
            background-repeat: no-repeat;
            border-color: ButtonFace;
            height: 15px;
            width: 15px;
        }
        .style22
        {
            text-align: center;
        }
    </style>
    <script type="text/javascript">
        function validate() {

            if (document.getElementById('<%=ddlAccountDr.ClientID%>').value == "0") {
                alert("Please select Account before clicking the save button"); // prompt user
                return false;
                document.getElementById("ddlAccountDr").focus();
                //set focus back to control               
            }

            alert('Do you Want to Save?')
        }
        function ddl2_OnChanged() {
            //alert('check');
            var ddl1 = document.getElementById('<%=ddlAccountDr.ClientID%>');
            var ddl2 = document.getElementById('<%=ddlAccountCr.ClientID%>');

            var ddl1Value = ddl1.options[ddl1.selectedIndex].text;
            var ddl2Value = ddl2.options[ddl2.selectedIndex].text;

            if (ddl1Value == ddl2Value) {
                alert('The respective term has already been selected in Account');
                ddl2.selectedIndex = 0;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image ID="Image1" runat="server" 
                ImageUrl="~/Content/Images/progress.gif" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 76%;" align="center" border="1">
                <tr>
                    <td style="text-align: center" colspan="3">
                        <asp:Label ID="Label1" runat="server" Text="Receipt" CssClass="labeltitle"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right" class="style11" colspan="3">
                        <asp:Label ID="Label2" runat="server" Text="Voucher No " CssClass="fontsize"></asp:Label>
                        <asp:TextBox ID="txtVchNo" runat="server" ReadOnly="True" Width="142px" 
                            CssClass="textbox140"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label3" runat="server" CssClass="fontsize" Text="Voucher Date "></asp:Label>
                        <asp:TextBox ID="txtVchDate" runat="server" MaxLength="10" 
                            CssClass="textbox140"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                            TargetControlID="txtVchDate">
                        </cc1:CalendarExtender>
                        <cc1:FilteredTextBoxExtender ID="fteVchDate" runat="server" 
                            FilterType="Numbers,Custom" TargetControlID="txtVchDate" 
                            ValidChars="01/01/1999">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left;" class="style4">
                        <table style="width: 100%;">
                            <tr>
                                <td bgcolor="#2461BF" class="style22" colspan="2">
                                    <asp:Label ID="Label5" runat="server" CssClass="style13" ForeColor="White" Text="Account [Dr]"></asp:Label>
                                </td>
                                <td style="text-align: center" bgcolor="#2461BF">
                                    <asp:Label ID="Label8" runat="server" CssClass="style16" Enabled="False" ForeColor="#E7E7FF"
                                        Text="Cheque No"></asp:Label>
                                </td>
                                <td style="text-align: center;" bgcolor="#2461BF">
                                    <asp:Label ID="Label9" runat="server" CssClass="style16" Enabled="False" ForeColor="#E7E7FF"
                                        Text="Cheque Date"></asp:Label>
                                </td>
                                <td style="text-align: center;" bgcolor="#2461BF">
                                    <asp:Label ID="lblAmount0" runat="server" CssClass="style16" ForeColor="#E7E7FF"
                                        Text="Amount"></asp:Label>
                                </td>
                                <td bgcolor="#2461BF">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlAccountDr" runat="server" 
                                        AppendDataBoundItems="True"  CssClass="ddl400">
                                        <asp:ListItem Value="0">~Select~</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtChqNo" runat="server" Font-Names="arial" Font-Size="8pt" 
                                        Width="100px" CssClass="textbox100"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtChqDate" runat="server" Font-Names="arial" Font-Size="8pt" MaxLength="10"
                                        Width="100px" CssClass="textbox100"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FTEDate" runat="server" FilterType="Numbers,Custom"
                                        TargetControlID="txtChqDate" ValidChars="01/01/1999">
                                    </cc1:FilteredTextBoxExtender>
                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtChqDate">
                                    </cc1:CalendarExtender>
                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtamt2" runat="server" AutoPostBack="false" Font-Names="arial"
                                        Font-Size="8pt" Style="text-align: right" Width="100px" 
                                        CssClass="textbox100"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtamt2_FilteredTextBoxExtender" runat="server"
                                        FilterType="Custom" TargetControlID="txtamt2" ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:CheckBox ID="Chk" runat="server" Visible="False" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center;" bgcolor="#2461BF">
                                    <asp:Label ID="lblDr" runat="server" CssClass="style16" Text=" Particulars " Style="font-size: 10pt"
                                        ForeColor="#E7E7FF"></asp:Label>
                                </td>
                                <td class="style15" style="text-align: center;" bgcolor="#2461BF">
                                    <asp:Label ID="Label7" runat="server" CssClass="style16" Text="Method of Adj." Style="font-size: 10pt"
                                        ForeColor="#E7E7FF"></asp:Label>
                                </td>
                                <td style="text-align: center;" bgcolor="#2461BF">
                                    <asp:Label ID="lblRef" runat="server" CssClass="style16" Text=" Reference " Style="font-size: 10pt"
                                        ForeColor="#E7E7FF"></asp:Label>
                                </td>
                                <td style="text-align: center" bgcolor="#2461BF">
                                    <asp:Label ID="lblJobNo" runat="server" CssClass="style16" Text="Cost Center" Style="font-size: 10pt"
                                        ForeColor="#E7E7FF"></asp:Label>
                                </td>
                                <td style="text-align: center;" bgcolor="#2461BF">
                                    <asp:Label ID="lblAmount" runat="server" CssClass="style16" Text="Amount" Style="font-size: 10pt"
                                        ForeColor="#E7E7FF"></asp:Label>
                                </td>
                                <td bgcolor="#2461BF">
                                </td>
                            </tr>
                            <tr style="border-style: ridge;">
                                <td>
                                    <asp:DropDownList ID="ddlAccountCr" runat="server" AppendDataBoundItems="True" 
                                       OnSelectedIndexChanged="ddlAccountCr_SelectedIndexChanged" 
                                        AutoPostBack="True" CssClass="ddl200">
                                        <asp:ListItem Value="0">~Select~</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="style15">
                                    <asp:DropDownList ID="ddlMethod" runat="server" AppendDataBoundItems="True" 
                                        CssClass="ddl100" >
                                        <asp:ListItem Value="0">~Select~</asp:ListItem>
                                        <asp:ListItem>On Account</asp:ListItem>
                                        <asp:ListItem>Advance</asp:ListItem>
                                        <asp:ListItem>Aget Ref</asp:ListItem>
                                        <asp:ListItem>New Ref.</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDetails" runat="server"  
                                        AutoPostBack="True" ontextchanged="txtDetails_TextChanged" 
                                        CssClass="textbox100"></asp:TextBox>
                                    <cc1:AutoCompleteExtender runat="server" ID="acetxtdetails" TargetControlID="txtDetails"
                                        CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
                                        CompletionListItemCssClass="listItem" EnableCaching="true" MinimumPrefixLength="0"
                                        ServiceMethod="GetInvoiceDebitNote" ServicePath="~/AutoComplete.asmx">
                                    </cc1:AutoCompleteExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCost" runat="server" Font-Names="arial" Font-Size="8pt" 
                                        CssClass="textbox100"></asp:TextBox>
                                    <cc1:AutoCompleteExtender runat="server" ID="acetxtCost" TargetControlID="txtCost"
                                        CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
                                        CompletionListItemCssClass="listItem" EnableCaching="true" MinimumPrefixLength="0"
                                        ServiceMethod="GetCostCenter" ServicePath="~/AutoComplete.asmx">
                                    </cc1:AutoCompleteExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtamt1" runat="server" AutoPostBack="false" Font-Names="arial"
                                        Font-Size="8pt" Style="text-align: right" Width="100px" 
                                        CssClass="textbox100"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FTE1" runat="server" FilterType="Custom" TargetControlID="txtamt1"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Content/Images/AddNew.png" 
                                        OnClick="btnAdd_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8">
                                    <asp:GridView ID="gvReceiptDetails" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" CellSpacing="1"
                                        Font-Names="Arial" Font-Size="8pt" GridLines="None" Width="100%" 
                                        OnSelectedIndexChanged="gvReceiptDetails_SelectedIndexChanged" 
                                        ondatabound="gvReceiptDetails_DataBound" ShowFooter="True">
                                        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="True" />
                                            <asp:BoundField DataField="TransId" HeaderText="ReceiptId" InsertVisible="False"
                                                ReadOnly="True" SortExpression="ReceiptId" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide" />
                                            <asp:BoundField DataField="AccountCrName" HeaderText="AccountCr" SortExpression="AccountCrName" />
                                            <asp:BoundField DataField="MethodOfAdj" HeaderText="MethodOfAdj" SortExpression="MethodOfAdj" />
                                            <asp:BoundField DataField="CostCenter" HeaderText="CostCenter" SortExpression="CostCenter" />
                                            <asp:BoundField DataField="Reference" HeaderText="Reference" SortExpression="Reference" />
                                            <%-- <asp:BoundField DataField="Chq_No" HeaderText="Chq/DD No" SortExpression="Chq_No" />
                                            <asp:BoundField DataField="Chq_Date" HeaderText="Chq/DD Date" SortExpression="Chq_Date" />--%>
                                            <asp:BoundField DataField="AmountCr" HeaderText="Amount" SortExpression="AmountCr" />
                                        </Columns>
                                        <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="#E7E7FF" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <tr>
                        <td align="left" colspan="3" style="color: Red;">
                            * Indicates the mandatory field
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblNarration" runat="server" Text="Narration" 
                                CssClass="fontsize"></asp:Label>
                        </td>
                        <td class="style4" style="text-align: left">
                            <asp:TextBox ID="txtNarration" runat="server" Style="text-align: left" 
                              CssClass="textbox400"></asp:TextBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkApproved" runat="server" AutoPostBack="True" OnCheckedChanged="chkApproved_CheckedChanged"
                                Text="Approved" Style="font-size: 10pt" CssClass="fontsize" />
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr id="rwRemarks" runat="server">
                        <td style="text-align: right">
                            <asp:Label ID="Label6" runat="server" Text="Remarks" CssClass="fontsize"></asp:Label>
                        </td>
                        <td class="style4" colspan="2" style="text-align: left">
                            <asp:TextBox ID="txtRemarks" runat="server" Enabled="false" Style="text-align: left"
                               CssClass="textbox400"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4" colspan="3" style="text-align: center">
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" OnClientClick="javascript:return validate();"
                                Text="Save" Width="70px" Enabled="False" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" OnClientClick="return confirm('Do you want to update?');"
                                Text="Update" Width="70px" CssClass="masterbutton" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnNew" runat="server" CausesValidation="false" OnClick="btnNew_Click"
                                Text="New" Width="70px" CssClass="masterbutton" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Print" Visible="false"
                                Width="70px" CssClass="masterbutton" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnExit" runat="server" CausesValidation="false" OnClick="btnExit_Click"
                                OnClientClick="return confirm('Do you want to leave this page?');" Text="Exit"
                                Width="70px" CssClass="masterbutton" />
                            &nbsp;&nbsp;
                            <asp:Label ID="lblResult" runat="server"></asp:Label>
                            <asp:Button ID="btnPrevious" runat="server" Text="<<" 
                                onclick="btnPrevious_Click" CssClass="masterbutton" />
                            <asp:Button ID="btnNext" runat="server" Text=">>" onclick="btnNext_Click" 
                                CssClass="masterbutton" />
                        </td>

                    </tr>
                    <tr>
                        <td class="style6" colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

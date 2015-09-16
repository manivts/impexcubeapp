<%@ Page Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="Payment.aspx.cs" Inherits="ImpexCube.Accounts.Payment" Title="Payment Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
        .ColHidden
        {
            display: none;
        }
    </style>
    <style type="text/css">
        .style4
        {
            height: 35px;
        }
        .style6
        {
            width: 297px;
        }
        .style7
        {
            height: 26px;
            width: 35px;
        }
        .style8
        {
            width: 35px;
            height: 14px;
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
            font-size: 10px;
        }
        .itemHighlighted
        {
            background-color: #ffc0c0;
        }
        .style13
        {
            font-size: small;
        }
        .style16
        {
            font-size: 10pt;
        }
        .Hide
        {
            display: none;
        }
        .style17
        {
            height: 11px;
        }
        .style21
        {
            height: 16px;
        }
        .style22
        {
            text-align: center;
        }
    </style>
    <script type="text/javascript">
        function visiblehidden() {
         
//            alert(document.getElementById('ctl00_ContentPlaceHolder1_LabelPayment').firstChild.data);
            var test = document.getElementById('ctl00_ContentPlaceHolder1_LabelPayment').firstChild.data;
//            alert(test);
            if (test == "Cash Payment") {
//                alert('a');
                document.getElementById('<%=a001.ClientID%>').style.display = "none";
                document.getElementById('<%=td003.ClientID%>').style.display = "none";
                document.getElementById('<%=a002.ClientID%>').style.display = "none";
                document.getElementById('<%=td004.ClientID%>').style.display = "none";
                document.getElementById('<%=td005.ClientID%>').style.display = "none"; 
                document.getElementById('<%=td006.ClientID%>').style.display = "none";
            }
            else {
            }
        }
        function validate() {

            if (document.getElementById('<%=ddlAccountCr.ClientID%>').value == "0") {
                alert("Please select AccountCr before clicking the save button"); // prompt user
                return false;
                document.getElementById("ddlAccountCr").focus();
                //set focus back to control               
            }

            var gv = document.getElementById('<%=gvPaymentDetails.ClientID%>');
            var items = gv.getElementsByTagName('select');
            for (var i = 0; i < items.length; i++) {
                if (items.item(i).value == '0') //If dropdown has no selected value   
                {
                    alert("Please select Dr before clicking the save button");
                    return false;
                }
            }


            //get target base & child control.           
            var TargetBaseControl = document.getElementById('<%=gvPaymentDetails.ClientID%>');
            var amt = "txtamt1";
            //get all the control of the type INPUT in the base control.
            var amount = TargetBaseControl.getElementsByTagName("input");
            for (var n = 0; n < amount.length; ++n) {
                if (amount[n].type == 'text' && amount[n].id.indexOf(amt, 0) >= 0) {
                    if (amount[n].value == "") {
                        alert('Please fill "Amount" before clicking the save button');
                        return false;
                    }
                }
            }
        }
        function ddl2_OnChanged() {
            var ddl1 = document.getElementById('<%=ddlAccountCr.ClientID%>');
            var ddl2 = document.getElementById('<%=ddlAccountDr.ClientID%>');

            var ddl1Value = ddl1.options[ddl1.selectedIndex].text;
            var ddl2Value = ddl2.options[ddl2.selectedIndex].text;

            if (ddl1Value == ddl2Value) {
                alert('The respective term has already been selected in Dr');
                ddl2.selectedIndex = 0;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image ID="Image1" runat="server" 
                ImageUrl="~/Content/Images/progress.gif" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table align="center" style="width: 70%;">
                <tr>
                    <td colspan="3" style="text-align: center">
                        <asp:Label ID="LabelPayment" runat="server" CssClass="labeltitle"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">
                        <asp:Label ID="Label2" runat="server" CssClass="fontsize" Style="text-align: left"
                            Text="Voucher No "></asp:Label>
                        &nbsp;&nbsp;
                        <asp:TextBox ID="txtVchNo" runat="server" ReadOnly="True" CssClass="textbox140"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label3" runat="server" CssClass="fontsize" Text="Voucher Date "></asp:Label>
                        &nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtVchDate" runat="server" MaxLength="10" 
                            CssClass="textbox140"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtVchDate">
                        </cc1:CalendarExtender>
                        <cc1:FilteredTextBoxExtender ID="fteVchDate" runat="server" FilterType="Numbers,Custom"
                            TargetControlID="txtVchDate" ValidChars="01/01/1999">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td class="style4" colspan="3" style="text-align: left">
                        <table style="width: 100%;">
                            <tr>
                                <td bgcolor="#3399FF" class="style22" colspan="2">
                                    <asp:Label ID="Label4" runat="server" CssClass="style13" ForeColor="White" Text="Accounts [Cr]"></asp:Label>
                                </td>

                                <td id='a001' runat="server"  bgcolor="#3399FF" style="text-align: center">
                                    <asp:Label ID="lblChequeNo" runat="server" CssClass="style16" Enabled="false" ForeColor="#E7E7FF"
                                        Text="Cheque No"></asp:Label>
                                </td >
                                <td id='a002' runat="server"  bgcolor="#3399FF" style="text-align: center;">
                                    <asp:Label ID="lblChequeDate" runat="server" CssClass="style16" Enabled="False" ForeColor="#E7E7FF"
                                        Text="Cheque Date"></asp:Label>
                                </td>
                                <td bgcolor="#3399FF" style="text-align: center;">
                                    <asp:Label ID="lblAmount0" runat="server" CssClass="style16" ForeColor="#E7E7FF"
                                        Text="Amount"></asp:Label>
                                </td>

                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlAccountCr" runat="server"  Style="margin-top: 0px;"
                                       onselectedindexchanged="ddlAccountCr_SelectedIndexChanged" 
                                        AppendDataBoundItems="true" CssClass="textbox400">
                                        <asp:ListItem Text="~Select~" Value="~Select~"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>

                                <td id='td003' runat="server"   style="text-align: center">
                                    <asp:TextBox ID="txtChqNo" runat="server" Enabled="False" CssClass="textbox100" 
                                      ></asp:TextBox>
                                </td>
                                <td id='td004' runat="server" style="text-align: center;">
                                    <asp:TextBox ID="txtChqDate" runat="server" Enabled="False" Font-Names="arial" Font-Size="8pt"
                                        MaxLength="10" Width="100px" CssClass="textbox100"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FTEDate" runat="server" FilterType="Numbers,Custom"
                                        TargetControlID="txtChqDate" ValidChars="01/01/1999">
                                    </cc1:FilteredTextBoxExtender>
                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtChqDate">
                                    </cc1:CalendarExtender>
                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtamt2" runat="server" AutoPostBack="false" Font-Names="arial"
                                        Font-Size="8pt" Style="text-align: right"  
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
                                <td bgcolor="#3399FF" style="text-align: center;">
                                    <asp:Label ID="lblDr" runat="server" CssClass="style16" ForeColor="#E7E7FF" Text=" Particulars "></asp:Label>
                                </td>
                                <td id='td005' runat="server" bgcolor="#3399FF" style="text-align: center;">
                                    <asp:Label ID="Label5" runat="server" CssClass="style16" ForeColor="#E7E7FF" Text="Method of Adj."></asp:Label>
                                </td>
                                <td bgcolor="#3399FF" style="text-align: center">
                                    <asp:Label ID="lblRef" runat="server" CssClass="style16" ForeColor="#E7E7FF" Text=" Reference "></asp:Label>
                                </td>
                                <td bgcolor="#3399FF" style="text-align: center;">
                                    <asp:Label ID="lblJobNo" runat="server" CssClass="style16" ForeColor="#E7E7FF" Text="Cost Center"></asp:Label>
                                </td>
                                <td bgcolor="#3399FF" style="text-align: center;">
                                    <asp:Label ID="lblAmount" runat="server" CssClass="style16" ForeColor="#E7E7FF" Text="Amount"></asp:Label>
                                </td>
                                <td bgcolor="#3399FF">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlAccountDr" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                       OnSelectedIndexChanged="ddlAccountDr_SelectedIndexChanged" CssClass="textbox300" 
                                        >
                                        <asp:ListItem Value="~Select~">~Select~</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td id='td006' runat="server">
                                    <asp:DropDownList ID="ddlMethod" runat="server" AppendDataBoundItems="True"
                                    CssClass="ddl150">
                                        <asp:ListItem Value="~Select~">~Select~</asp:ListItem>
                                        <asp:ListItem>On Account</asp:ListItem>
                                        <asp:ListItem>Advance</asp:ListItem>
                                        <asp:ListItem>Aget Ref</asp:ListItem>
                                        <asp:ListItem>New Ref.</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDetails" runat="server" Font-Names="arial" Font-Size="8pt" 
                                        CssClass="textbox100"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="acetxtdetails" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetInvoiceNo" ServicePath="~/AutoComplete.asmx"
                                        TargetControlID="txtDetails">
                                    </cc1:AutoCompleteExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCost" runat="server" Font-Names="arial" Font-Size="8pt" 
                                        CssClass="textbox100"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="acetxtCost" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        DelimiterCharacters="" EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetCostCenter"
                                        ServicePath="~/AutoComplete.asmx" TargetControlID="txtCost">
                                    </cc1:AutoCompleteExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtamt1" runat="server" AutoPostBack="false" Font-Names="arial"
                                        Font-Size="8pt" Style="text-align: right" 
                                        CssClass="textbox100"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FTE1" runat="server" FilterType="Custom" TargetControlID="txtamt1"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/content/Images/AddNew.png" OnClick="btnAdd_Click1"
                                        Width="24px" />
                                </td>
                            </tr>
                            <tr id="rwGridViewDetails" runat="server">
                                <td colspan="6">
                                    <asp:GridView ID="gvPaymentDetails" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" CellSpacing="1"
                                        Font-Names="Arial" Font-Size="8pt" OnSelectedIndexChanged="gvPaymentDetails_SelectedIndexChanged"
                                        Width="100%" ondatabound="gvPaymentDetails_DataBound" ShowFooter="True" 
                                        GridLines="None">
                                        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="True" />
                                            <asp:BoundField DataField="TransId" HeaderStyle-CssClass="Hide" HeaderText="SlNo"
                                                InsertVisible="False" ItemStyle-CssClass="Hide" ReadOnly="True" SortExpression="TransId" />
                                            <asp:BoundField DataField="AccountDrName" HeaderText="Particulars" SortExpression="AccCode" />
                                            <asp:BoundField DataField="MethodOfAdj" HeaderText="Method of Adj" SortExpression="MethodOfAdj" />
                                            <asp:BoundField DataField="Reference" HeaderText="Reference" SortExpression="Reference" />
                                            <asp:BoundField DataField="CostCenter" HeaderText="Cost Center" SortExpression="CostCenter" />
                                            <asp:BoundField DataField="AmountDr" HeaderText="Amount" SortExpression="Amount" />
                                        </Columns>
                                        <%--<HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="#E7E7FF" />#3399FF--%>
                                        <HeaderStyle BackColor="#3399FF" Font-Bold="True" ForeColor="#E7E7FF" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="style17" style="text-align: left">
                        <asp:Label ID="lblNarration" runat="server" CssClass="style16" Text="Narration"></asp:Label>
                    </td>
                    <td class="style17" style="text-align: left">
                        <asp:TextBox ID="txtNarration" runat="server" AutoPostBack="false" 
                            Style="text-align: left" CssClass="textbox400" ></asp:TextBox>
                    </td>
                    <td class="style17">
                        <asp:CheckBox ID="chkApproved" runat="server" AutoPostBack="true" Text="Approved" 
                            Visible="False" CssClass="fontsize" />
                    </td>
                </tr>
                <tr>
                    <td class="style21" colspan="3" style="text-align: center">
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" OnClientClick="javascript:return validate();"
                            Text="Save" Width="70px" Enabled="False" CssClass="masterbutton" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" OnClientClick="return confirm('Do you want to update?');"
                            Text="Update" Width="70px" CssClass="masterbutton" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnNew" runat="server" CausesValidation="false" OnClick="btnNew_Click"
                            Style="height: 26px" Text="New" Width="70px" CssClass="masterbutton" />
                        &nbsp;&nbsp;
                        <asp:Button ID="ButtonPrint" runat="server" OnClick="ButtonPrint_Click" Text="Print"
                            Visible="False" Width="70px" CssClass="masterbutton" />
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
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <br />
</asp:Content>

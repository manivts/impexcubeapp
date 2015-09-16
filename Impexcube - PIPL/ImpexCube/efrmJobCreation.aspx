<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="efrmJobCreation.aspx.cs" Inherits="ImpexCube.efrmJobCreation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Content/JQuery/JSONJobDetails.js" type="text/javascript"></script>
    <script type="text/javascript">

        function Validate() {
            debugger
            //            document.getElementById('ErrorMessage').innerHTML = "";
            if (document.getElementById('ContentPlaceHolder1_txtJobNo').value == "") {
                document.getElementById('ContentPlaceHolder1_txtJobNo').focus();
                alert("Please Enter The JobNo");
                return false;
            }
            if (document.getElementById('ContentPlaceHolder1_txtJobDate').value == "") {
                alert('Please Enter The JobDate');
                document.getElementById('ContentPlaceHolder1_txtJobDate').focus();
                return false;
            }
            var ddlfilling = document.getElementById('ContentPlaceHolder1_ddlFilling');
            var fillingtext = ddlfilling.options[ddlfilling.selectedIndex].text;
            if (fillingtext == "~Select~") {
                alert("Please Select Doc Filling");
                return false;
            }
            var ddlmode = document.getElementById('ContentPlaceHolder1_ddlTransportMode');
            var modetext = ddlmode.options[ddlmode.selectedIndex].text;
            if (modetext == "~Select~") {
                alert("Please Select Mode");
                return false;
            }
            var ddlcustom = document.getElementById('ContentPlaceHolder1_ddlCustomHouse');
            var customtext = ddlcustom.options[ddlcustom.selectedIndex].text;
            if (customtext == "~Select~") {
                alert("Please Select Custom House");
                return false;
            }
//            var ddldoctype = document.getElementById('ContentPlaceHolder1_ddlDocFillingStatus');
//            var doctypetext = ddldoctype.options[ddldoctype.selectedIndex].text;
//            if (doctypetext == "~Select~") {
//                alert("Please Select Doc Type");
//                return false;
//            }
//            var ddlbetype = document.getElementById('ContentPlaceHolder1_ddlBEType');
//            var betypetext = ddlbetype.options[ddlbetype.selectedIndex].text;
//            if (betypetext == "~Select~") {
//                alert("Please Select BE Type");
//                return false;
//            }
            if (document.getElementById('ContentPlaceHolder1_txtExporter').value == "") {
                document.getElementById('ContentPlaceHolder1_txtExporter').focus();
                alert("Please Enter Exporter  Name");
                return false;
            }
            if (document.getElementById('ContentPlaceHolder1_txtExporterRefNo').value == "") {
                document.getElementById('ContentPlaceHolder1_txtExporterRefNo').focus();
                alert("Please Enter ExpRef Code ");
                return false;
            }
            if (document.getElementById('ContentPlaceHolder1_txtExporterAddress').value == "") {
                document.getElementById('ContentPlaceHolder1_txtExporterAddress').focus();
                alert("Please Enter Exporter Address ");
                return false;
            }
            var totinv = 0;
            var totinvalue = 0.00;
            totinv = document.getElementById('ContentPlaceHolder1_txtTotalNoOfInvoice').value;
            totinvalue = document.getElementById('ContentPlaceHolder1_txtTotalInvoiceValue').value;
            if ((totinv !== "0") || (totinv != 0)) {
                if ((totinvalue == "0") || (totinvalue == 0)) {
                    alert("Please Enter Total Invoice Value ");
                    document.getElementById('ContentPlaceHolder1_txtTotalInvoiceValue').focus();
                    return false;
                }
            }
            if ((totinvalue !== "0") || (totinvalue != 0)) {
                if ((totinv == "0") || (totinv == 0)) {
                    alert("Please Enter Total No Of Invoice ");
                    document.getElementById('ContentPlaceHolder1_txtTotalNoOfInvoice').focus();
                    return false;
                }
            }
            var retVal = confirmation();
            if (retVal == true) {
                return true;
            }
            else {
                return false;
            }

        }


        function confirmation() {
            debugger

            var res = confirm("Are you sure to save?");
            if (res == false) {
                return false;
            }
            else {
                return true;
            }

        }
     
        function TabAllow(e) {
            if (e.shiftKey || e.keyCode == 9) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <style type="text/css">
    input[type=], textarea
        {
            border: 1px solid #ccc;
        }
        input[type=text]:focus, textarea:focus
        {
            background-color: #F0F0F0;
            border: 1px solid #ccc;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="div1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td rowspan="17" style="border-color: #C0C0C0; border-left-style: ridge;">
            </td>
            <td align="center" colspan="4">
                <strong>Exporter Details</strong>
            </td>
        </tr>
<%--   <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
                <asp:Label ID="lblExporter0" runat="server" Text="Serarch Exporter" CssClass="fontsize"></asp:Label>
            </td>
            <td colspan="3">
             <asp:TextBox ID="txtSearchExp" runat="server" CssClass="textbox400" AutoPostBack="True"
                    OnTextChanged="txtSearchExp_TextChanged" TabIndex="19" Width="425px"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                    EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetSearchAccountName"
                    ServicePath="~/AutoComplete.asmx" TargetControlID="txtSearchExp">
                </cc1:AutoCompleteExtender>
            </td>
        </tr>--%>
        <tr>
            <td>
                <asp:Label ID="lblJobNumber" runat="server" Text="Job No." CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtJobNo" runat="server" CssClass="textbox200" 
                    TabIndex="1" Width="112px"></asp:TextBox>
                    <asp:Button ID="btnSearchJob" runat="server" Height="18px" OnClick="btnSearchJob_Click"
                                        Text="::" />
              <%--  <cc1:CalendarExtender ID="txtJobNo_CalendarExtender" runat="server" TargetControlID="txtJobNo"
                    Format="dd/MM/yyyy">
                </cc1:CalendarExtender>--%>
            </td>
            
            <td>
                <asp:Label ID="lblJobDate" runat="server" Text="Job Received Date" CssClass="fontsize"
                    Width="109px"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtJobDate" runat="server" CssClass="textbox200" TabIndex="2" onkeypress="Javascript:return TabAllow(event);"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtJobDate"
                    Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
            </td>
            <td>
                <asp:Label ID="lblExporter" runat="server" Text="Exporter" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtExporter" runat="server" CssClass="textbox200" 
                    TabIndex="20"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblBranchSNo" runat="server" Text="Branch SNo" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtBranchSNo" runat="server" CssClass="textbox75" 
                    Width="113px" TabIndex="21"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTransportMode" runat="server" Text="Mode" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlTransportMode" runat="server" Width="206px" CssClass="ddl150"
                    OnSelectedIndexChanged="ddlTransportMode_SelectedIndexChanged" 
                    AutoPostBack="True" TabIndex="3">
                    <asp:ListItem Text="~Select~" Value="~Select~"></asp:ListItem>
                    <asp:ListItem Text="Air" Value="A"></asp:ListItem>
                    <asp:ListItem Text="Sea" Value="S"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblCustomHouse" runat="server" Text="Custom" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlCustomHouse" runat="server" CssClass="ddl156" AppendDataBoundItems="True"
                    Width="206px" Enabled="False" TabIndex="4">
                    <asp:ListItem Text="~Select~" Value="~Select~"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblExporterRef" runat="server" Text="Expr Ref No/Date" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtExporterRefNo" runat="server" CssClass="textbox200" 
                    Width="114px" TabIndex="22"></asp:TextBox>
                <asp:TextBox ID="txtExporterRefDate" runat="server" CssClass="textbox75" 
                    TabIndex="23" onkeypress="Javascript:return TabAllow(event);"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtExporterRefDate"
                    Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
            </td>
            <td>
                <asp:Label ID="lblRbiNo" runat="server" Text="RBI Appr.No & Date" CssClass="fontsize"
                    Width="98px"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtRbiNo" runat="server" CssClass="textbox200" Width="113px" 
                    TabIndex="24"></asp:TextBox>
                <asp:TextBox ID="txtRbiDate" runat="server" CssClass="textbox75" TabIndex="25" onkeypress="Javascript:return TabAllow(event);"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtRbiDate"
                    Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblSBNo" runat="server" Text="SB No" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSBNo" runat="server" CssClass="textbox200" TabIndex="5"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblSBNo0" runat="server" Text="SB Date" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSBDate" runat="server" CssClass="textbox200" TabIndex="6" onkeypress="Javascript:return TabAllow(event);"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtSBDate"
                    Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
            </td>
            <td rowspan="2">
                <asp:Label ID="lblExporterAddress" runat="server" Text="Address" CssClass="fontsize"></asp:Label>
            </td>
            <td rowspan="2">
                <asp:TextBox ID="txtExporterAddress" runat="server" TextMode="MultiLine" 
                    CssClass="textboxMulti200" TabIndex="26"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblRbiWavierNo" runat="server" Text="RBI Wavier No" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtRbiWavierNo" runat="server" CssClass="textbox200" 
                    Width="114px" TabIndex="27"></asp:TextBox>
                <asp:TextBox ID="txtRbiWavierExtn" runat="server" CssClass="textbox75" 
                    TabIndex="28" onkeypress="Javascript:return TabAllow(event);"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtRbiWavierExtn"
                    Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblFilling" runat="server" Text="Filling" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlFilling" runat="server" Width="206px" 
                    CssClass="ddl156" TabIndex="7">
                    <asp:ListItem Selected="True">~Select~</asp:ListItem>
                    <asp:ListItem>Online</asp:ListItem>
                    <asp:ListItem>Manual</asp:ListItem>
                    <asp:ListItem>E-mail</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblExporterType" runat="server" Text="Exp Type" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlExporterType" runat="server" CssClass="ddl156" 
                    Width="206px" TabIndex="8">
                    <asp:ListItem Text="~Select~" Value="~Select~"></asp:ListItem>
                    <asp:ListItem Text="Manufacturer Exporter" Value="Manufacturer Exporter"></asp:ListItem>
                    <asp:ListItem Text="Merchant Exporter" Value="Merchant Exporter"></asp:ListItem>
                    <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblGRWaived" runat="server" Text="GR Waived" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="cbGRWaived" runat="server" Text="GR No" CssClass="fontsize" 
                    TabIndex="29" />
                <asp:TextBox ID="txtWavierNo" runat="server" CssClass="textbox140" Width="53px" 
                    TabIndex="30"></asp:TextBox>
                <asp:TextBox ID="txtWavierNoExtn" runat="server" CssClass="textbox75" 
                    TabIndex="31" onkeypress="Javascript:return TabAllow(event);"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtWavierNoExtn"
                    Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label14" runat="server" CssClass="fontsize" Text="Total No of Invoice"
                    Width="109px"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtTotalNoOfInvoice" runat="server" CssClass="textbox200" OnKeyPress="javascript:return isFloat(event);"
                    MaxLength="3" TabIndex="9">0</asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblCurrency0" runat="server" CssClass="fontsize" Text="Currency"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlCurrency" runat="server" AppendDataBoundItems="True" CssClass="ddl156"
                    TabIndex="10" Width="206px">
                    <asp:ListItem Selected="True" Value="~Select~">~Select~</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblStateProvince" runat="server" Text="State/Province" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtStateProvince" runat="server" CssClass="textbox200" 
                    TabIndex="32"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblEpzCode" runat="server" Text="EPZ Code" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEpzCode" runat="server" CssClass="textbox75" Width="113px" 
                    TabIndex="33"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label15" runat="server" CssClass="fontsize" Text="Total Invoice Value"
                    Width="111px"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtTotalInvoiceValue" runat="server" CssClass="textbox200" OnKeyPress="javascript:return isFloat(event);"
                    TabIndex="11">0</asp:TextBox>
            </td>
            <td>
                
            </td>
            <td>
                
            </td>
            <td>
                
            </td>
            <td>
                
            </td>
            <td>
                
            </td>
            <td>
                
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <strong>Notify Details</strong>
            </td>
            <td align="center" colspan="2">
                <strong>Buyer Details</strong>
            </td>
            <td>
                <asp:Label ID="lblBankDealerCode" runat="server" Text="Bank/Dealer Code" CssClass="fontsize"
                    Width="108px"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtBankDealerCode" runat="server" CssClass="textbox200" 
                    TabIndex="34"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblIECode" runat="server" Text="IE Code No" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtIECode" runat="server" CssClass="textbox200" TabIndex="35"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblNotify0" runat="server" Text="Search Notify" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSearchNotify" runat="server" CssClass="textbox200" AutoPostBack="True"
                    OnTextChanged="txtSearchNotify_TextChanged" TabIndex="12"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionListCssClass="completionList"
                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                    EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetSearchAccountName"
                    ServicePath="~/AutoComplete.asmx" TargetControlID="txtSearchNotify">
                </cc1:AutoCompleteExtender>
            </td>
            <td>
                <asp:Label ID="lblNotify1" runat="server" Text="Search Buyer" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSearchBuyer" runat="server" CssClass="textbox200" AutoPostBack="True"
                    OnTextChanged="txtSearchBuyer_TextChanged" TabIndex="15"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" CompletionListCssClass="completionList"
                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                    EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetSearchAccountName"
                    ServicePath="~/AutoComplete.asmx" TargetControlID="txtSearchBuyer">
                </cc1:AutoCompleteExtender>
            </td>
            <td rowspan="2">
                <asp:Label ID="lblBankDealer" runat="server" Text="Bank/Dealer" CssClass="fontsize"></asp:Label>
            </td>
            <td rowspan="2">
                <asp:TextBox ID="txtBankDealer" runat="server" TextMode="MultiLine" 
                    CssClass="textboxMulti200" TabIndex="36"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblAccNo" runat="server" Text="A/C No" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtAccNo" runat="server" CssClass="textbox200" TabIndex="37"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblNotify" runat="server" Text="Notify" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtNotify" runat="server" CssClass="textbox200" TabIndex="13"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblBuyer" runat="server" Text="Buyer" CssClass="fontsize"></asp:Label>
                <asp:CheckBox ID="cbBuyer" runat="server" CssClass="fontsize" 
                    onChange="javascript:return DisableBuyer();" TabIndex="16" />
            </td>
            <td>
                <asp:TextBox ID="txtBuyer" runat="server" CssClass="textbox200" TabIndex="17"></asp:TextBox>
            </td>
            <td>
                
            </td>
            <td>
                
            </td>
        </tr>
        <tr>
            <td rowspan="2">
                <asp:Label ID="lblAddress" runat="server" Text="Address" CssClass="fontsize"></asp:Label>
            </td>
            <td rowspan="2">
                <asp:TextBox ID="txtAddressExtn" runat="server" TextMode="MultiLine" 
                    CssClass="textboxMulti200" TabIndex="14"></asp:TextBox>
            </td>
            <td rowspan="2">
                <asp:Label ID="lblBuyerAddress" runat="server" Text="Address" CssClass="fontsize"></asp:Label>
            </td>
            <td rowspan="2">
                <asp:TextBox ID="txtBuyerAddress" runat="server" TextMode="MultiLine" 
                    CssClass="textboxMulti200" TabIndex="18"></asp:TextBox>
            </td>
            <td align="center" colspan="4" style="font-weight: 700">
                Consignee Details
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblConsginee0" runat="server" Text="Search Consignee" CssClass="fontsize"></asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtSearchConsignee" runat="server" CssClass="textbox400" AutoPostBack="True"
                    OnTextChanged="txtSearchConsignee_TextChanged" TabIndex="38" Width="425px"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" CompletionListCssClass="completionList"
                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                    EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetSearchAccountName"
                    ServicePath="~/AutoComplete.asmx" TargetControlID="txtSearchConsignee">
                </cc1:AutoCompleteExtender>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
                <asp:Label ID="lblConsginee" runat="server" Text="Consignee" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtConsignee" runat="server" CssClass="textbox200" 
                    TabIndex="39"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblCosigneeCountry" runat="server" Text="Cons Country" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCosigneeCountry" runat="server" CssClass="textbox200" 
                    TabIndex="40"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td rowspan="2">
                <asp:Label ID="lblConsigneeAddress" runat="server" Text="Address" CssClass="fontsize"></asp:Label>
            </td>
            <td rowspan="2">
                <asp:TextBox ID="txtConsigneeAddress" runat="server" CssClass="textboxMulti200" 
                    TextMode="MultiLine" TabIndex="41"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="9">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="stylebutton"
                    OnClick="btnSave_Click" TabIndex="43" />
                     <%--OnClientClick="return Validate();--%>
                <asp:Button ID="btnUpdate" runat="server" CssClass="stylebutton" Text="Update" 
                    OnClick="btnUpdate_Click" TabIndex="44" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="stylebutton" 
                    OnClick="btnCancel_Click" TabIndex="45" />
                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="stylebutton" 
                    OnClick="btnClose_Click" TabIndex="46" />
                <asp:Button ID="bntForward" runat="server" Text="Go to Shipment" CssClass="stylebutton"
                    OnClick="bntForward_Click" TabIndex="42" />
                <asp:Button ID="btnStandardDocument" runat="server" Text="Standard Document" CssClass="stylebutton"
                    Visible="False" Height="24px" TabIndex="47" />
            </td>
        </tr>
        </table>
        </div>
        <div id="div2" runat="server" style="height: 350px; width: 1200px;  text-align: center" tabindex="-1">
        <table>
          <tr>
                                <td colspan="3" align="center">
                                    Search
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="textbox200" TabIndex="45"></asp:TextBox>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="stylebutton" Text="Search" OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnNew" runat="server" CssClass="stylebutton" OnClick="btnNew_Click"
                                        TabIndex="48" Text="New Job" />
                                </td>
                            </tr>
        <tr>
            <td align="center" colspan="9">
                <div class="grid_scroll-2" style="height: 350PX">
                    <asp:GridView ID="gvJobCreation" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True"
                        OnSelectedIndexChanged="gvJobCreation_SelectedIndexChanged" CssClass="table-wraper"
                        Width="1000px" Height="140px" TabIndex="48" Font-Size="Small">
                        <Columns>
                            <asp:BoundField HeaderText="Job Number" DataField="JobNo" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Job Date" DataField="JobDate" HeaderStyle-HorizontalAlign="Center">  
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Job Received On" DataField="JobReceivedOn" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Transport Mode" DataField="TransportMode" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Custom House" DataField="CustomHouse" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <%-- <asp:BoundField DataField="BEType" HeaderText="BE Type" HtmlEncode="False" />
                             <asp:BoundField DataField="BENo" HeaderText="BE No" HtmlEncode="False" />
                             <asp:BoundField DataField="BEDate" HeaderText="BE Date" HtmlEncode="False" /> --%>                                               
                             <asp:BoundField DataField="ExporterName" HeaderText="Exporter Name" HtmlEncode="False" />
                          <%--  <asp:BoundField HeaderText="Filling" DataField="Filling" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>--%>
                        </Columns>
                        <RowStyle CssClass="table-header light" />
                        <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" />
                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                        <AlternatingRowStyle BackColor="#E7E7FF" />
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="9">
                
            </td>
        </tr>
    </table>
    </div>
</asp:Content>

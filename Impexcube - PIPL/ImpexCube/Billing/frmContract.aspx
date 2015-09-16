<%@ Page Title="" Language="C#" MasterPageFile="~/Billing/MasterPage.master" AutoEventWireup="true" 
CodeBehind="frmContract.aspx.cs" Inherits="ImpexCube.Billing.frmContract" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        
        $(document).ready(function () {
            $("#hide").click(function () {
                $("#hide").hide();
            });
        });

    function actualair() {
        var AirSelect = document.getElementById('<%=txtairAtAcutal.ClientID%>').value;
        //var selectedText = AirSelect.options[AirSelect.selectedIndex].text;
        if (AirSelect != "") {
            document.getElementById('<%=txtairFixed.ClientID%>').disabled = true;
            document.getElementById('<%=txtairminimum.ClientID%>').disabled = true;
            document.getElementById('<%=txtairMaximum.ClientID%>').disabled = true;
            document.getElementById('<%=txtairVariable.ClientID%>').disabled = true;

            return false;
        }

        else {
            document.getElementById('<%=txtairFixed.ClientID%>').disabled = false;
            document.getElementById('<%=txtairminimum.ClientID%>').disabled = false;
            document.getElementById('<%=txtairMaximum.ClientID%>').disabled = false;
            document.getElementById('<%=txtairVariable.ClientID%>').disabled = false;

            return false;
        }
    }

    function validateInputs() 
    {debugger
        if (document.getElementById('<%=ddlCusName.ClientID%>').selectedIndex == 0)
         {
            alert('Customer cannot be empty');
            document.getElementById('<%=ddlCusName.ClientID%>').focus();
            return false;
        }

         if ((document.getElementById('<%=ddl20FeetCharges.ClientID%>').selectedIndex == 0) &&
          (document.getElementById('<%=ddl40FeetCharges.ClientID%>').selectedIndex == 0) &&
          (document.getElementById('<%=ddlLCLCharges.ClientID%>').selectedIndex == 0) &&
          (document.getElementById('<%=ddlAIRCharges.ClientID%>').selectedIndex == 0))
          {
          alert('Please select atleast one charge');
            document.getElementById('<%=ddl20FeetCharges.ClientID%>').focus();
            return false;
        }

        var a = document.getElementById('<%=txt20feetMinimum.ClientID%>').value;
        var b = document.getElementById('<%=txt20feetMaximum.ClientID%>').value;

        if ((a != '') && (b != '')) {
            if (a > b) {
                alert('20 feet Minimum amount must be less than its Maximum amount');
                document.getElementById('<%=txt20feetMinimum.ClientID%>').focus();
                return false;
            }
        }

        var x = document.getElementById('<%=txt40FeetMinimum.ClientID%>').value;
        var y = document.getElementById('<%=txt40FeetMaximum.ClientID%>').value;

        if ((x != '') && (y != '')) {
            if (x > y) {
                alert('40 feet Minimum amount must be less than its Maximum amount');
                document.getElementById('<%=txt40FeetMinimum.ClientID%>').focus();
                return false;
            }
        }

        var p = document.getElementById('<%=txtLCLMinimum.ClientID%>').value;
        var q = document.getElementById('<%=txtLCLMaximum.ClientID%>').value;


        if ((p != '') && (q != '')) {
            if (p > q) {
                alert('LCL Minimum amount must be less than its Maximum amount');
                document.getElementById('<%=txtLCLMinimum.ClientID%>').focus();
                return false;
            }
        }

        var airmn = document.getElementById('<%=txtairminimum.ClientID%>').value;
        var airmx = document.getElementById('<%=txtairMaximum.ClientID%>').value;

        if ((airmn != '') && (airmx != '')) {
            if ((airmn > airmx) {
                alert('AIR Minimum amount must be less than its Maximum amount');
                document.getElementById('<%=txtairminimum.ClientID%>').focus();
                return false;
            }
        }

        
        if ((document.getElementById('<%=ddl20FeetCharges.ClientID%>').selectedIndex != 0) && 
        (document.getElementById('<%=ddl20FeetUnit.ClientID%>').selectedIndex == 0) &&
        (document.getElementById('<%=txt20FeetAtActual.ClientID%>').value == '') &&
        (document.getElementById('<%=txt20feetMinimum.ClientID%>').value == '') &&
        (document.getElementById('<%=txt20feetVariable.ClientID%>').value == '') &&
        (document.getElementById('<%=txt20feetMaximum.ClientID%>').value == '') &&
        (document.getElementById('<%=txt20feetFixed.ClientID%>').value == ''))
        {
            alert('Please give values for the selected charge');
            document.getElementById('<%=ddl20FeetCharges.ClientID%>').focus();
            return false;
        }

        if ((document.getElementById('<%=ddl40FeetCharges.ClientID%>').selectedIndex != 0) &&
        (document.getElementById('<%=ddl40feetUnit.ClientID%>').selectedIndex == 0) &&
        (document.getElementById('<%=txt40FeetAtActual.ClientID%>').value == '') &&
        (document.getElementById('<%=txt40FeetMinimum.ClientID%>').value == '') &&
        (document.getElementById('<%=txt40FeetVariable.ClientID%>').value == '') &&
        (document.getElementById('<%=txt40FeetMaximum.ClientID%>').value == '') &&
        (document.getElementById('<%=txt40FeetFixed.ClientID%>').value == '')) 
        {
            alert('Please give values for the selected charge');
            document.getElementById('<%=ddl40FeetCharges.ClientID%>').focus();
            return false;
        }


        if ((document.getElementById('<%=ddlLCLCharges.ClientID%>').selectedIndex != 0) &&
        (document.getElementById('<%=ddlLCLUnit.ClientID%>').selectedIndex == 0) &&
        (document.getElementById('<%=txtLCLAtActual.ClientID%>').value == '') &&
        (document.getElementById('<%=txtLCLMinimum.ClientID%>').value == '') &&
        (document.getElementById('<%=txtLCLVariable.ClientID%>').value == '') &&
        (document.getElementById('<%=txtLCLMaximum.ClientID%>').value == '') &&
        (document.getElementById('<%=txtLCLFixed.ClientID%>').value == ''))
         {
            alert('Please give values for the selected charge');
            document.getElementById('<%=ddlLCLCharges.ClientID%>').focus();
            return false;
        }

        if ((document.getElementById('<%=ddlAIRCharges.ClientID%>').selectedIndex != 0) &&
        (document.getElementById('<%=ddlAirUnit.ClientID%>').selectedIndex == 0) &&
        (document.getElementById('<%=txtairAtAcutal.ClientID%>').value == '') &&
        (document.getElementById('<%=txtairminimum.ClientID%>').value == '') &&
        (document.getElementById('<%=txtairVariable.ClientID%>').value == '') &&
        (document.getElementById('<%=txtairMaximum.ClientID%>').value == '') &&
        (document.getElementById('<%=txtairFixed.ClientID%>').value == '')) 
        {
            alert('Please give values for the selected charge');
            document.getElementById('<%=ddlAIRCharges.ClientID%>').focus();
            return false;
        }            

    }


    function minair() {
        var AirSelect = document.getElementById('<%=txtairminimum.ClientID%>').value;
        if (AirSelect != "") {
            document.getElementById('<%=txtairFixed.ClientID%>').disabled = true;
            document.getElementById('<%=txtairAtAcutal.ClientID%>').disabled = true;        

            return false;
        }

        else {
            document.getElementById('<%=txtairFixed.ClientID%>').disabled = false;
            document.getElementById('<%=txtairAtAcutal.ClientID%>').disabled = false;          

            return false;
        }
    }



    function DisableAirFixed() {
        var AirSelectF = document.getElementById('<%=txtairFixed.ClientID%>').value;
        if (AirSelectF == "") {

            document.getElementById('<%=txtairminimum.ClientID%>').disabled = false;
            document.getElementById('<%=txtairMaximum.ClientID%>').disabled = false; 
            document.getElementById('<%=txtairVariable.ClientID%>').disabled = false;
            document.getElementById('<%=txtairAtAcutal.ClientID%>').disabled = false;
            return false;
        }

        else {

            document.getElementById('<%=txtairminimum.ClientID%>').disabled = true;
            document.getElementById('<%=txtairMaximum.ClientID%>').disabled = true;
            document.getElementById('<%=txtairVariable.ClientID%>').disabled = true;
            document.getElementById('<%=txtairAtAcutal.ClientID%>').disabled = true;
            return false;
        }
    }
    

    function actuallcl() {
        var LCLSelect = document.getElementById('<%=txtLCLAtActual.ClientID%>').value;
        //var selectedText = LCLSelect.options[LCLSelect.selectedIndex].text;
        if (LCLSelect != "") {
            document.getElementById('<%=txtLCLFixed.ClientID%>').disabled = true;
            document.getElementById('<%=txtLCLMinimum.ClientID%>').disabled = true;
            document.getElementById('<%=txtLCLMaximum.ClientID%>').disabled = true;
            document.getElementById('<%=txtLCLVariable.ClientID%>').disabled = true;
            
            return false;
        }

        else {
            document.getElementById('<%=txtLCLFixed.ClientID%>').disabled = false;
            document.getElementById('<%=txtLCLMinimum.ClientID%>').disabled = false;
            document.getElementById('<%=txtLCLMaximum.ClientID%>').disabled = false;
            document.getElementById('<%=txtLCLVariable.ClientID%>').disabled = false;
            
            return false;
        }
    }

    function minlcl() {
        var lclSelect = document.getElementById('<%=txtLCLMinimum.ClientID%>').value;
        if (lclSelect != "") {
            document.getElementById('<%=txtLCLFixed.ClientID%>').disabled = true;
            document.getElementById('<%=txtLCLAtActual.ClientID%>').disabled = true;

            return false;
        }

        else {
            document.getElementById('<%=txtLCLFixed.ClientID%>').disabled = false;
            document.getElementById('<%=txtLCLAtActual.ClientID%>').disabled = false;

            return false;
        }
    }

    function DisableLCLFixed() {
        var LCLSelectF = document.getElementById('<%=txtLCLFixed.ClientID%>').value;
        if (LCLSelectF == "") {

            document.getElementById('<%=txtLCLMinimum.ClientID%>').disabled = false;
            document.getElementById('<%=txtLCLMaximum.ClientID%>').disabled = false;
            document.getElementById('<%=txtLCLVariable.ClientID%>').disabled = false;
            document.getElementById('<%=txtLCLAtActual.ClientID%>').disabled = false;
            return false;
        }

        else {

            document.getElementById('<%=txtLCLMinimum.ClientID%>').disabled = true;
            document.getElementById('<%=txtLCLMaximum.ClientID%>').disabled = true;
            document.getElementById('<%=txtLCLVariable.ClientID%>').disabled = true;
            document.getElementById('<%=txtLCLAtActual.ClientID%>').disabled = true;
            return false;
        }
    }

    function actual20() {
        var twentySelect = document.getElementById('<%=txt20FeetAtActual.ClientID%>').value;
       // var selectedText = twentySelect.options[twentySelect.selectedIndex].text;
        if (twentySelect != "") {
            document.getElementById('<%=txt20feetFixed.ClientID%>').disabled = true;
            document.getElementById('<%=txt20feetMinimum.ClientID%>').disabled = true;
            document.getElementById('<%=txt20feetMaximum.ClientID%>').disabled = true;
            document.getElementById('<%=txt20feetVariable.ClientID%>').disabled = true;
            
            return false;
        }

        else {
            document.getElementById('<%=txt20feetFixed.ClientID%>').disabled = false;
            document.getElementById('<%=txt20feetMinimum.ClientID%>').disabled = false;
            document.getElementById('<%=txt20feetMaximum.ClientID%>').disabled = false;
            document.getElementById('<%=txt20feetVariable.ClientID%>').disabled = false;
            
            return false;
        }
    }

    function min20() {
        var lcl20 = document.getElementById('<%=txt20feetMinimum.ClientID%>').value;
        if (lcl20 != "") {
            document.getElementById('<%=txt20feetFixed.ClientID%>').disabled = true;
            document.getElementById('<%=txt20FeetAtActual.ClientID%>').disabled = true;

            return false;
        }

        else {
            document.getElementById('<%=txt20feetFixed.ClientID%>').disabled = false;
            document.getElementById('<%=txt20FeetAtActual.ClientID%>').disabled = false;

            return false;
        }
    }

    function Disable20Fixed() {
        var twentySelectF = document.getElementById('<%=txt20feetFixed.ClientID%>').value;
        if (twentySelectF == "") {

            document.getElementById('<%=txt20feetMinimum.ClientID%>').disabled = false;
            document.getElementById('<%=txt20feetMaximum.ClientID%>').disabled = false;
            document.getElementById('<%=txt20feetVariable.ClientID%>').disabled = false;
            document.getElementById('<%=txt20FeetAtActual.ClientID%>').disabled = false;
            return false;
        }

        else {

            document.getElementById('<%=txt20feetMinimum.ClientID%>').disabled = true;
            document.getElementById('<%=txt20feetMaximum.ClientID%>').disabled = true;
            document.getElementById('<%=txt20feetVariable.ClientID%>').disabled = true;
            document.getElementById('<%=txt20FeetAtActual.ClientID%>').disabled = true;
            return false;
        }
    }

    function actual40() {
        var fourtySelect = document.getElementById('<%=txt40FeetAtActual.ClientID%>').value;
        //var selectedText = fourtySelect.options[fourtySelect.selectedIndex].text;
        if (fourtySelect == "") {
            document.getElementById('<%=txt40FeetFixed.ClientID%>').disabled = false;
            document.getElementById('<%=txt40FeetMinimum.ClientID%>').disabled = false;
            document.getElementById('<%=txt40FeetMaximum.ClientID%>').disabled = false;
            document.getElementById('<%=txt40FeetVariable.ClientID%>').disabled = false;
            
            return false;
        }

        else {
            document.getElementById('<%=txt40FeetFixed.ClientID%>').disabled = true;
            document.getElementById('<%=txt40FeetMinimum.ClientID%>').disabled = true;
            document.getElementById('<%=txt40FeetMaximum.ClientID%>').disabled = true;
            document.getElementById('<%=txt40FeetVariable.ClientID%>').disabled = true;
            
            return false;
        }
    }

    function min40() {
        var lcl40 = document.getElementById('<%=txt40FeetMinimum.ClientID%>').value;
        if (lcl40 != "") {
            document.getElementById('<%=txt40FeetFixed.ClientID%>').disabled = true;
            document.getElementById('<%=txt40FeetAtActual.ClientID%>').disabled = true;

            return false;
        }

        else {
            document.getElementById('<%=txt40FeetFixed.ClientID%>').disabled = false;
            document.getElementById('<%=txt40FeetAtActual.ClientID%>').disabled = false;

            return false;
        }
    }

    function Disable40Fixed() {
        var fourtySelectF = document.getElementById('<%=txt40FeetFixed.ClientID%>').value;
        if (fourtySelectF == "") {

            document.getElementById('<%=txt40FeetMinimum.ClientID%>').disabled = false;
            document.getElementById('<%=txt40FeetMaximum.ClientID%>').disabled = false;
            document.getElementById('<%=txt40FeetVariable.ClientID%>').disabled = false;
            document.getElementById('<%=txt40FeetAtActual.ClientID%>').disabled = false;
            return false;
        }

        else {

            document.getElementById('<%=txt40FeetMinimum.ClientID%>').disabled = true;
            document.getElementById('<%=txt40FeetMaximum.ClientID%>').disabled = true;
            document.getElementById('<%=txt40FeetVariable.ClientID%>').disabled = true;
            document.getElementById('<%=txt40FeetAtActual.ClientID%>').disabled = true;
            return false;
        }
    }
    </script>
<%--</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">--%>
    <div style="width: 100%">
        <table style="width: 69%" align="center">
            <tr>
                <td colspan="12" style="text-align: center">
                    Quote
                </td>
            </tr>
            <tr>
                <td colspan="4" width="620px">
                    Customer Name&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlCusName" runat="server" AppendDataBoundItems="True" 
                        AutoPostBack="True" Width="400px" 
                        onselectedindexchanged="ddlCusName_SelectedIndexChanged" Height="26px">
                        <asp:ListItem>~Select~</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                <td colspan="3">
                    <asp:Label ID="Label1" runat="server" Text="Quote No"></asp:Label>
                    :&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblQuoteNo" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="center" colspan="12">
                    <strong>20 FEET</strong>
                </td>
            </tr>
            <tr>
                <td class="center">
                    <%--<asp:Panel ID="Panel1" runat="server" GroupingText="20 FEET" 
        style="font-weight: 700; font-size: small">
    </asp:Panel>--%>
                    Descripiton
                </td>
                <td class="center">
                    Unit
                </td>
                <td class="center">
                    At Actual
                </td>
                <td class="center">
                    Minimum
                </td>
                <td class="center">
                    Variable
                </td>
                <td class="center">
                    Maximum
                </td>
                <td align="left" width="80px">
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    Fixed
                </td>
                <td class="center" width="50px">
                    &nbsp;</td>
                <td class="center">
                    &nbsp;</td>
                <td class="center">
                    &nbsp;</td>
                <td class="center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="left">
                    <asp:DropDownList ID="ddl20FeetCharges" runat="server" Width="200px" 
                        AppendDataBoundItems="True" AutoPostBack="True" 
                        onselectedindexchanged="ddl20FeetCharges_SelectedIndexChanged">
                        <asp:ListItem>~Select~</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="left">
                    <asp:DropDownList ID="ddl20FeetUnit" runat="server" Width="100px" onchange="javascript:return Disable20();">
                        <asp:ListItem>~Select~</asp:ListItem>
                        <asp:ListItem>PER B/E</asp:ListItem>
                        <asp:ListItem>PER shipment</asp:ListItem>
                        <asp:ListItem>PER Flat Rate</asp:ListItem>
                        <asp:ListItem>PER Kg</asp:ListItem>
                        <asp:ListItem>PER Contr</asp:ListItem>
                        <asp:ListItem>PER TON</asp:ListItem>
                        <asp:ListItem>PER PKG</asp:ListItem>
                        <asp:ListItem>PER LICENSE</asp:ListItem>
                        <asp:ListItem>PER TRA</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="left">
                    <asp:TextBox ID="txt20FeetAtActual" runat="server" Width="80px" onblur="javascript:return actual20();"></asp:TextBox>
                </td>
                <td class="left">
                    <asp:TextBox ID="txt20feetMinimum" runat="server" Width="80px" onblur="javascript:return min20();"></asp:TextBox>
                </td>
                <td class="left">
                    <asp:TextBox ID="txt20feetVariable" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="left">
                    <asp:TextBox ID="txt20feetMaximum" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="left" width="80px">
                    <asp:TextBox ID="txt20feetFixed" runat="server" Width="80px" onblur="javascript:return Disable20Fixed()"></asp:TextBox>
                </td>
                <td class="left">
                    &nbsp;</td>
                <td class="left">
                    &nbsp;</td>
                <td class="left">
                    &nbsp;</td>
                <td class="left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left" colspan="2">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;</td>
                <td class="left">
                    &nbsp;</td>
                <td class="left">
                    &nbsp;</td>
                <td class="left">
                    &nbsp;</td>
                <td class="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="center" colspan="12">
                    <strong>40 FEET</strong>
                </td>
            </tr>
            <tr>
                <td class="center">
                    <%--<asp:Panel ID="Panel1" runat="server" GroupingText="20 FEET" 
        style="font-weight: 700; font-size: small">
    </asp:Panel>--%>
                    Descripiton
                </td>
                <td class="center">
                    Unit
                </td>
                <td class="center">
                    At Actual
                </td>
                <td class="center">
                    Minimum
                </td>
                <td class="center">
                    Variable
                </td>
                <td class="center">
                    Maximum
                </td>
                <td align="left" width="80px">
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    Fixed
                </td>
                <td class="center" width="50px">
                    &nbsp;</td>
                <td class="center">
                    &nbsp;</td>
                <td class="center">
                    &nbsp;</td>
                <td class="center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="left">
                    <asp:DropDownList ID="ddl40FeetCharges" runat="server" Width="200px" 
                        AppendDataBoundItems="True" AutoPostBack="True">
                        <asp:ListItem>~Select~</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="left">
                    <asp:DropDownList ID="ddl40feetUnit" runat="server" Width="100px" onchange="javascript:return Disable40();">
                        <asp:ListItem>~Select~</asp:ListItem>
                        <asp:ListItem>PER B/E</asp:ListItem>
                        <asp:ListItem>PER shipment</asp:ListItem>
                        <asp:ListItem>PER Flat Rate</asp:ListItem>
                        <asp:ListItem>PER Kg</asp:ListItem>
                        <asp:ListItem>PER Contr</asp:ListItem>
                        <asp:ListItem>PER TON</asp:ListItem>
                        <asp:ListItem>PER PKG</asp:ListItem>
                        <asp:ListItem>PER LICENSE</asp:ListItem>
                        <asp:ListItem>PER TRA</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="left">
                    <asp:TextBox ID="txt40FeetAtActual" runat="server" Width="80px" onchange="javascript:return actual40();"></asp:TextBox>
                </td>
                <td class="left">
                    <asp:TextBox ID="txt40FeetMinimum" runat="server" Width="80px" onblur="javascript:return min40();"></asp:TextBox>
                </td>
                <td class="left">
                    <asp:TextBox ID="txt40FeetVariable" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="left">
                    <asp:TextBox ID="txt40FeetMaximum" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="left" width="80px">
                    <asp:TextBox ID="txt40FeetFixed" runat="server" Width="80px" onchange="javascript:return Disable40Fixed()"></asp:TextBox>
                </td>
                <td class="left">
                    &nbsp;</td>
                <td class="left">
                    &nbsp;</td>
                <td class="left">
                    &nbsp;</td>
                <td class="left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left" colspan="2">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;</td>
                <td class="left">
                    &nbsp;</td>
                <td class="left">
                    &nbsp;</td>
                <td class="left">
                    &nbsp;</td>
                <td class="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="center" colspan="12">
                    <strong>LCL</strong>
                </td>
            </tr>
            <tr>
                <td class="center">
                    <%--<asp:Panel ID="Panel1" runat="server" GroupingText="20 FEET" 
        style="font-weight: 700; font-size: small">
    </asp:Panel>--%>
                    Descripiton
                </td>
                <td class="center">
                    Unit
                </td>
                <td class="center">
                    At Actual
                </td>
                <td class="center">
                    Minimum
                </td>
                <td class="center">
                    Variable
                </td>
                <td class="center">
                    Maximum
                </td>
                <td align="left" width="80px">
                    &nbsp;&nbsp;&nbsp;
                    Fixed
                </td>
                <td class="center" width="50px">
                    &nbsp;</td>
                <td class="center">
                    &nbsp;</td>
                <td class="center">
                    &nbsp;</td>
                <td class="center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="left">
                    <asp:DropDownList ID="ddlLCLCharges" runat="server" Width="200px" 
                        AppendDataBoundItems="True" AutoPostBack="True">
                        <asp:ListItem>~Select~</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="left">
                    <asp:DropDownList ID="ddlLCLUnit" runat="server" Width="100px" onchange="javascript:return DisableLCL();">
                        <asp:ListItem>~Select~</asp:ListItem>
                        <asp:ListItem>PER B/E</asp:ListItem>
                        <asp:ListItem>PER shipment</asp:ListItem>
                        <asp:ListItem>PER Flat Rate</asp:ListItem>
                        <asp:ListItem>PER Kg</asp:ListItem>
                        <asp:ListItem>PER Contr</asp:ListItem>
                        <asp:ListItem>PER TON</asp:ListItem>
                        <asp:ListItem>PER PKG</asp:ListItem>
                        <asp:ListItem>PER LICENSE</asp:ListItem>
                        <asp:ListItem>PER TRA</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="left">
                    <asp:TextBox ID="txtLCLAtActual" runat="server" Width="80px" onblur="javascript:return actuallcl();"></asp:TextBox>
                </td>
                <td class="left">
                    <asp:TextBox ID="txtLCLMinimum" runat="server" Width="80px" onblur="javascript:return minlcl();"></asp:TextBox>
                </td>
                <td class="left">
                    <asp:TextBox ID="txtLCLVariable" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="left">
                    <asp:TextBox ID="txtLCLMaximum" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="left" width="80px">
                    <asp:TextBox ID="txtLCLFixed" runat="server" Width="80px" onchange="javascript:return DisableLCLFixed()"></asp:TextBox>
                </td>
                <td class="left">
                    &nbsp;</td>
                <td class="left">
                    &nbsp;</td>
                <td class="left">
                    &nbsp;</td>
                <td class="left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left" colspan="2">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;</td>
                <td class="left">
                    &nbsp;</td>
                <td class="left">
                    &nbsp;</td>
                <td class="left">
                    &nbsp;</td>
                <td class="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="center" colspan="12">
                    <strong>AIR</strong>
                </td>
            </tr>
            <tr>
                <td class="center">
                    <%--<asp:Panel ID="Panel1" runat="server" GroupingText="20 FEET" 
        style="font-weight: 700; font-size: small">
    </asp:Panel>--%>
                    Descripiton
                </td>
                <td class="center">
                    Unit
                </td>
                <td class="center">
                    At Actual
                </td>
                <td class="center">
                    Minimum
                </td>
                <td class="center">
                    Variable
                </td>
                <td class="center">
                    Maximum
                </td>
                <td align="left" width="80px">
                    &nbsp;&nbsp;&nbsp;
                    Fixed
                </td>
                <td class="center" width="50px">
                    &nbsp;</td>
                <td class="center">
                    &nbsp;</td>
                <td class="center">
                    &nbsp;</td>
                <td class="center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="left">
                    <asp:DropDownList ID="ddlAIRCharges" runat="server" Width="200px" 
                        AppendDataBoundItems="True" AutoPostBack="True">
                        <asp:ListItem>~Select~</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="left">
                    <asp:DropDownList ID="ddlAirUnit" runat="server" Width="100px" onchange="javascript:return DisableAir();">
                        <asp:ListItem>~Select~</asp:ListItem>
                        <asp:ListItem>PER B/E</asp:ListItem>
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>PER shipment</asp:ListItem>
                        <asp:ListItem>PER Flat Rate</asp:ListItem>
                        <asp:ListItem>PER Kg</asp:ListItem>
                        <asp:ListItem>PER Contr</asp:ListItem>
                        <asp:ListItem>PER TON</asp:ListItem>
                        <asp:ListItem>PER PKG</asp:ListItem>
                        <asp:ListItem>PER LICENSE</asp:ListItem>
                        <asp:ListItem>PER TRA</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="left">
                    <asp:TextBox ID="txtairAtAcutal" runat="server" Width="80px" onchange="javascript:return actualair();"></asp:TextBox>
                </td>
                <td class="left">
                    <asp:TextBox ID="txtairminimum" runat="server" Width="80px" onblur="javascript:return minair();"></asp:TextBox>
                </td>
                <td class="left">
                    <asp:TextBox ID="txtairVariable" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="left">
                    <asp:TextBox ID="txtairMaximum" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="left" width="80px">
                    <asp:TextBox ID="txtairFixed" runat="server" Width="80px" onchange="javascript:return DisableAirFixed()"></asp:TextBox>
                </td>
                <td class="left">
                    &nbsp;</td>
                <td class="left">
                    &nbsp;</td>
                <td class="left">
                    &nbsp;</td>
                <td class="left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left" align="right">
                   <asp:Button ID="btnSave" runat="server" Text="Add" onclick="btnSave_Click" Width="80px" OnClientClick = "javascript:return validateInputs() "/>
                </td>
                <td class="left">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" 
                        onclick="btnUpdate_Click" Visible="False" Width="70px" />
                </td>
                <td class="left">
                    <asp:Button ID="Button2" runat="server" Text="Cancel" onclick="Button2_Click" />
                </td>
                <td class="left">
                    <asp:Button ID="btnPrint" runat="server" Text="Print" onclick="btnPrint_Click" 
                        Width="62px" Visible="False" />
                </td>
                <td class="left" colspan="2">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;</td>
                <td class="left">
                    &nbsp;</td>
                <td class="left">
                    &nbsp;</td>
                <td class="left">
                    &nbsp;</td>
                <td class="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="12">
                <div style="overflow:scroll;height:200px;width:800px;">
                    <asp:GridView ID="GvQuote" runat="server" AutoGenerateSelectButton="True" 
                        onselectedindexchanged="GvQuote_SelectedIndexChanged" 
                        BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
                        CellPadding="2" ForeColor="Black" GridLines="None">
                        <AlternatingRowStyle BackColor="PaleGoldenrod" />
                        <FooterStyle BackColor="Tan" />
                        <HeaderStyle BackColor="Tan" Font-Bold="True" />
                        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
                            HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                        <SortedAscendingCellStyle BackColor="#FAFAE7" />
                        <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                        <SortedDescendingCellStyle BackColor="#E1DB9C" />
                        <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                    </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

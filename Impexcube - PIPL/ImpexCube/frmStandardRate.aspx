<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmStandardRate.aspx.cs" Inherits="ImpexCube.CRM.frmStandardRate1" %>

    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .left
        {
        }
        .style1
        {
            font-size: large;
            color: #61615F;
            font-family: Arial;
        }
        .accordionHeader
        {
            border: 1px solid #2F4F4F;
            color: white;
            background-color: #2E4d7B;
            font-family: Arial, Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
            text-align: center;
        }
        
        .accordionHeaderSelected
        {
            border: 1px solid #2F4F4F;
            color: white;
            background-color: #5078B3;
            font-family: Arial, Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
            text-align: center;
        }
        
        .accordionContent
        {
            background-color: #D3DEEF;
            border: 1px dashed #2F4F4F;
            border-top: none;
            padding: 5px;
            padding-top: 10px;
        }
    </style>
    <script type="text/javascript">
        function mandatory() {
            var aircharges = document.getElementById('ContentPlaceHolder1_ddlAIRCharges');
            var selectedaircharges = aircharges.options[aircharges.selectedIndex].text;

            var Desc = document.getElementById('ContentPlaceHolder1_ddlShipModesir');
            var Descrip = Desc.options[Desc.selectedIndex].text;
            if (Descrip == "~Select~") {
                alert('Please Select Shipment Mode');
                return false;
            }
            else if (selectedaircharges == "~Select~") {
                alert('Please Select Description');
                return false;
            }
        }
        function modeair() {
            var Desc = document.getElementById('ContentPlaceHolder1_ddlShipModesir');
            var Descrip = Desc.options[Desc.selectedIndex].text;
            document.getElementById('ContentPlaceHolder1_ddlShipModelcl').value = Descrip;
            document.getElementById('ContentPlaceHolder1_ddlShipMode20').value = Descrip;
            document.getElementById('ContentPlaceHolder1_ddlShipMode40').value = Descrip;
        }
        function Descript() {
            try {
                var Desc = document.getElementById('<%=ddl20FeetCharges.ClientID %>');
                var Descrip = Desc.options[Desc.selectedIndex].text;

                document.getElementById('<%=ddl40FeetCharges.ClientID %>').value = Descrip;
                document.getElementById('<%=ddlLCLCharges.ClientID %>').value = Descrip;
                document.getElementById('<%=ddlAIRCharges.ClientID %>').value = Descrip;
                return true;
            }

            catch (err) {
                alert(err.Message);
            }
            return true;
        }

        function DisableAir() {

            var Air = document.getElementById('ContentPlaceHolder1_ddlAirUnit');
            var AirUnit = Air.options[Air.selectedIndex].text;
            if (AirUnit == "At Actual") {
                document.getElementById('<%=txtairFixed.ClientID%>').disabled = true;
                document.getElementById('<%=txtairminimum.ClientID%>').disabled = true;
                document.getElementById('<%=txtairMaximum.ClientID%>').disabled = true;
                document.getElementById('<%=txtairVariable.ClientID%>').disabled = true;
                document.getElementById('<%=ddlvarair.ClientID%>').disabled = true;
                return false;
            }

            else {
                document.getElementById('<%=txtairFixed.ClientID%>').disabled = false;
                document.getElementById('<%=txtairminimum.ClientID%>').disabled = false;
                document.getElementById('<%=txtairMaximum.ClientID%>').disabled = false;
                document.getElementById('<%=txtairVariable.ClientID%>').disabled = false;
                document.getElementById('<%=ddlvarair.ClientID%>').disabled = false;
                return false;
            }
        }

        function DisableLCL() {
            var Air = document.getElementById('ContentPlaceHolder1_ddlLCLUnit');
            var AirUnit = Air.options[Air.selectedIndex].text;
            if (AirUnit == "At Actual") {
                document.getElementById('<%=txtLCLFixed.ClientID%>').disabled = true;
                document.getElementById('<%=txtLCLMinimum.ClientID%>').disabled = true;
                document.getElementById('<%=txtLCLMaximum.ClientID%>').disabled = true;
                document.getElementById('<%=txtLCLVariable.ClientID%>').disabled = true;
                document.getElementById('<%=ddlvarlcl.ClientID%>').disabled = true;
                return false;
            }

            else {
                document.getElementById('<%=txtLCLFixed.ClientID%>').disabled = false;
                document.getElementById('<%=txtLCLMinimum.ClientID%>').disabled = false;
                document.getElementById('<%=txtLCLMaximum.ClientID%>').disabled = false;
                document.getElementById('<%=txtLCLVariable.ClientID%>').disabled = false;
                document.getElementById('<%=ddlvarlcl.ClientID%>').disabled = false;
                return false;
            }
        }

        function Disable20FE() {
            var Air = document.getElementById('ContentPlaceHolder1_ddl20FeetUnit');
            var AirUnit = Air.options[Air.selectedIndex].text;
            if (AirUnit == "At Actual") {
                document.getElementById('<%=txt20feetFixed.ClientID%>').disabled = true;
                document.getElementById('<%=txt20feetMinimum.ClientID%>').disabled = true;
                document.getElementById('<%=txt20feetMaximum.ClientID%>').disabled = true;
                document.getElementById('<%=txt20feetVariable.ClientID%>').disabled = true;
                document.getElementById('<%=ddlvar20.ClientID%>').disabled = true;
                return false;
            }

            else {
                document.getElementById('<%=txt20feetFixed.ClientID%>').disabled = false;
                document.getElementById('<%=txt20feetMinimum.ClientID%>').disabled = false;
                document.getElementById('<%=txt20feetMaximum.ClientID%>').disabled = false;
                document.getElementById('<%=txt20feetVariable.ClientID%>').disabled = false;
                document.getElementById('<%=ddlvar20.ClientID%>').disabled = false;
                return false;
            }
        }

        function Disable40FE() {
            var Air = document.getElementById('ContentPlaceHolder1_ddl40feetUnit');
            var AirUnit = Air.options[Air.selectedIndex].text;
            if (AirUnit == "At Actual") {
                document.getElementById('<%=txt40FeetFixed.ClientID%>').disabled = true;
                document.getElementById('<%=txt40FeetMinimum.ClientID%>').disabled = true;
                document.getElementById('<%=txt40FeetMaximum.ClientID%>').disabled = true;
                document.getElementById('<%=txt40FeetVariable.ClientID%>').disabled = true;
                document.getElementById('<%=ddlvar40.ClientID%>').disabled = true;
                return false;
            }

            else {
                document.getElementById('<%=txt40FeetFixed.ClientID%>').disabled = false;
                document.getElementById('<%=txt40FeetMinimum.ClientID%>').disabled = false;
                document.getElementById('<%=txt40FeetMaximum.ClientID%>').disabled = false;
                document.getElementById('<%=txt40FeetVariable.ClientID%>').disabled = false;
                document.getElementById('<%=ddlvar40.ClientID%>').disabled = false;
                return false;
            }
        }

        function DisableAirFixed() {         
                        
            if (document.getElementById('ContentPlaceHolder1_txtairFixed').value != "") {
                
                document.getElementById('<%=txtairFixed.ClientID%>').disabled = false;
                document.getElementById('<%=txtairminimum.ClientID%>').disabled = true;
                document.getElementById('<%=txtairMaximum.ClientID%>').disabled = true;
                document.getElementById('<%=txtairVariable.ClientID%>').disabled = true;
                document.getElementById('<%=ddlvarair.ClientID%>').disabled = true;
                return false;
            }
            else {

                document.getElementById('<%=txtairFixed.ClientID%>').disabled = true;
                document.getElementById('<%=txtairminimum.ClientID%>').disabled = false;
                document.getElementById('<%=txtairMaximum.ClientID%>').disabled = false;
                document.getElementById('<%=txtairVariable.ClientID%>').disabled = false;
                document.getElementById('<%=ddlvarair.ClientID%>').disabled = false;
                return false;
            }
        }

        function DisableLclFixed() {

            if (document.getElementById('ContentPlaceHolder1_txtLCLFixed').value != "") {

                document.getElementById('<%=txtLCLFixed.ClientID%>').disabled = false;
                document.getElementById('<%=txtLCLMinimum.ClientID%>').disabled = true;
                document.getElementById('<%=txtLCLMaximum.ClientID%>').disabled = true;
                document.getElementById('<%=txtLCLVariable.ClientID%>').disabled = true;
                document.getElementById('<%=ddlvarlcl.ClientID%>').disabled = true;
                return false;
            }
            else {

                document.getElementById('<%=txtLCLFixed.ClientID%>').disabled = true;
                document.getElementById('<%=txtLCLMinimum.ClientID%>').disabled = false;
                document.getElementById('<%=txtLCLMaximum.ClientID%>').disabled = false;
                document.getElementById('<%=txtLCLVariable.ClientID%>').disabled = false;
                document.getElementById('<%=ddlvarlcl.ClientID%>').disabled = false;
                return false;
            }
        }

        function Disable20Fixed() {

            if (document.getElementById('ContentPlaceHolder1_txt20feetFixed').value != "") {

                document.getElementById('<%=txt20feetFixed.ClientID%>').disabled = false;
                document.getElementById('<%=txt20feetMinimum.ClientID%>').disabled = true;
                document.getElementById('<%=txt20feetMaximum.ClientID%>').disabled = true;
                document.getElementById('<%=txt20feetVariable.ClientID%>').disabled = true;
                document.getElementById('<%=ddlvar20.ClientID%>').disabled = true;
                return false;
            }
            else {

                document.getElementById('<%=txt20feetFixed.ClientID%>').disabled = true;
                document.getElementById('<%=txt20feetMinimum.ClientID%>').disabled = false;
                document.getElementById('<%=txt20feetMaximum.ClientID%>').disabled = false;
                document.getElementById('<%=txt20feetVariable.ClientID%>').disabled = false;
                document.getElementById('<%=ddlvar20.ClientID%>').disabled = false;
                return false;
            }
        }

        function Disable40Fixed() {

            if (document.getElementById('ContentPlaceHolder1_txt40FeetFixed').value != "") {

                document.getElementById('<%=txt40FeetFixed.ClientID%>').disabled = false;
                document.getElementById('<%=txt40FeetMinimum.ClientID%>').disabled = true;
                document.getElementById('<%=txt40FeetMaximum.ClientID%>').disabled = true;
                document.getElementById('<%=txt40FeetVariable.ClientID%>').disabled = true;
                document.getElementById('<%=ddlvar40.ClientID%>').disabled = true;
                return false;
            }
            else {

                document.getElementById('<%=txt40FeetFixed.ClientID%>').disabled = true;
                document.getElementById('<%=txt40FeetMinimum.ClientID%>').disabled = false;
                document.getElementById('<%=txt40FeetMaximum.ClientID%>').disabled = false;
                document.getElementById('<%=txt40FeetVariable.ClientID%>').disabled = false;
                document.getElementById('<%=ddlvar40.ClientID%>').disabled = false;
                return false;
           }
        }


        function DisableAirMin() {
            if (document.getElementById('ContentPlaceHolder1_txtairminimum').value != "") {

                document.getElementById('<%=txtairFixed.ClientID%>').disabled = true;
                document.getElementById('<%=txtairminimum.ClientID%>').disabled = false;
                document.getElementById('<%=txtairMaximum.ClientID%>').disabled = false;
                document.getElementById('<%=txtairVariable.ClientID%>').disabled = false;
                document.getElementById('<%=ddlvarair.ClientID%>').disabled = false;
                return false;
            }
            else {

                document.getElementById('<%=txtairFixed.ClientID%>').disabled = false;
                document.getElementById('<%=txtairminimum.ClientID%>').disabled = true;
                document.getElementById('<%=txtairMaximum.ClientID%>').disabled = true;
                document.getElementById('<%=txtairVariable.ClientID%>').disabled = true;
                document.getElementById('<%=ddlvarair.ClientID%>').disabled = true;
                return false;
            }
        }

        function DisableLclMin() {
            if (document.getElementById('ContentPlaceHolder1_txtLCLMinimum').value != "") {

                document.getElementById('<%=txtLCLFixed.ClientID%>').disabled = true;
                document.getElementById('<%=txtLCLMinimum.ClientID%>').disabled = false;
                document.getElementById('<%=txtLCLMaximum.ClientID%>').disabled = false;
                document.getElementById('<%=txtLCLVariable.ClientID%>').disabled = false;
                document.getElementById('<%=ddlvarlcl.ClientID%>').disabled = false;
                return false;
            }
            else {

                document.getElementById('<%=txtLCLFixed.ClientID%>').disabled = false;
                document.getElementById('<%=txtLCLMinimum.ClientID%>').disabled = true;
                document.getElementById('<%=txtLCLMaximum.ClientID%>').disabled = true;
                document.getElementById('<%=txtLCLVariable.ClientID%>').disabled = true;
                document.getElementById('<%=ddlvarlcl.ClientID%>').disabled = true;
                return false;
           }
            
        }

        function Disable20Min() {
            if (document.getElementById('ContentPlaceHolder1_txt20feetMinimum').value != "") {

                document.getElementById('<%=txt20feetFixed.ClientID%>').disabled = true;
                document.getElementById('<%=txt20feetMinimum.ClientID%>').disabled = false;
                document.getElementById('<%=txt20feetMaximum.ClientID%>').disabled = false;
                document.getElementById('<%=txt20feetVariable.ClientID%>').disabled = false;
                document.getElementById('<%=ddlvar20.ClientID%>').disabled = false;
                return false;
            }
            else {

                document.getElementById('<%=txt20feetFixed.ClientID%>').disabled = false;
                document.getElementById('<%=txt20feetMinimum.ClientID%>').disabled = true;
                document.getElementById('<%=txt20feetMaximum.ClientID%>').disabled = true;
                document.getElementById('<%=txt20feetVariable.ClientID%>').disabled = true;
                document.getElementById('<%=ddlvar20.ClientID%>').disabled = true;
                return false;
            }
        }

        function Disable40Min() {
            if (document.getElementById('ContentPlaceHolder1_txt40FeetMinimum').value != "") {

                document.getElementById('<%=txt40FeetFixed.ClientID%>').disabled = true;
                document.getElementById('<%=txt40FeetMinimum.ClientID%>').disabled = false;
                document.getElementById('<%=txt40FeetMaximum.ClientID%>').disabled = false;
                document.getElementById('<%=txt40FeetVariable.ClientID%>').disabled = false;
                document.getElementById('<%=ddlvar40.ClientID%>').disabled = false;
                return false;
            }
            else {

                document.getElementById('<%=txt40FeetFixed.ClientID%>').disabled = false;
                document.getElementById('<%=txt40FeetMinimum.ClientID%>').disabled = true;
                document.getElementById('<%=txt40FeetMaximum.ClientID%>').disabled = true;
                document.getElementById('<%=txt40FeetVariable.ClientID%>').disabled = true;
                document.getElementById('<%=ddlvar40.ClientID%>').disabled = true;
                return false;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div >
        <table align="center">
            <tr>
                <td style="text-align: center" colspan="7">
                    <strong style="text-align: center"><span class="style1">Standard Rate</span></strong>
                </td>
            </tr>
            
             <tr>
                                <td colspan="8">
                                    <asp:Accordion ID="Accordion1" CssClass="accordion" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        runat="server">
                                        <Panes>
                                            <asp:AccordionPane ID="Pane4" runat="server">
                                                <Header>
                                                    Air
                                                    <%--<img id="hide" src="image/1.png" />--%>
                                                </Header>
                                                <Content>
                                                    <table>
                                                        <tr>
                                                            <td colspan="8">
                                                                <asp:Panel ID="pnlair" runat="server">
                                                                    <table>
                                                                        <tr>
                                                                            <td colspan="7" style="background: color:Black; text-align: center;">
                                                                                <strong>AIR</strong>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="center">
                                                                                ShipMode
                                                                            </td>
                                                                            <td class="center">
                                                                                Descripiton
                                                                            </td>
                                                                            <td class="center">
                                                                                Unit
                                                                            </td>
                                                                            <td class="center">
                                                                                Fixed
                                                                            </td>
                                                                            <td class="center">
                                                                                Minimum
                                                                            </td>
                                                                            <td class="center">
                                                                                Maximum
                                                                            </td>
                                                                            <td class="center">
                                                                                Variable
                                                                            </td>
                                                                            <td class="center">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="center">
                                                                                <asp:DropDownList ID="ddlShipModesir" runat="server" Width="100px" AppendDataBoundItems="True"
                                        AutoPostBack="True" onchange="javascript:return modeair();">
                                        <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                        <asp:ListItem>Imp</asp:ListItem>
                                        <asp:ListItem>Exp</asp:ListItem>
                                        <asp:ListItem>Both</asp:ListItem>
                                    </asp:DropDownList>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:DropDownList ID="ddlAIRCharges" runat="server" Width="200px" AppendDataBoundItems="True">
                                                                                    <asp:ListItem>~Select~</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:DropDownList ID="ddlAirUnit" runat="server" Width="100px" AutoPostBack="true"
                                                                                   onchange="javascript:return DisableAir()">
                                                                                 <%--   OnSelectedIndexChanged="ddlAirUnit_SelectedIndexChanged" --%>
                                                                                    <asp:ListItem>~Select~</asp:ListItem>
                                                                                    <asp:ListItem>Per B/E</asp:ListItem>
                                                                                    <asp:ListItem>Per Shipment</asp:ListItem>
                                                                                    <asp:ListItem>Per Kg</asp:ListItem>
                                                                                    <asp:ListItem>Per Flat Rate</asp:ListItem>
                                                                                    <asp:ListItem>Per TON</asp:ListItem>
                                                                                    <asp:ListItem>Per Contr</asp:ListItem>
                                                                                    <asp:ListItem>Per LICENSE</asp:ListItem>
                                                                                    <asp:ListItem>Per PKG</asp:ListItem>
                                                                                    <asp:ListItem>Per TRA</asp:ListItem>
                                                                                    <asp:ListItem>At Actual</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:TextBox ID="txtairFixed" runat="server" Width="80px" 
                                                                                    AutoPostBack="false" onblur="javascript:return DisableAirFixed()"></asp:TextBox>
                                                                                    <%--OnTextChanged="txtairFixed_TextChanged"--%>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:TextBox ID="txtairminimum" runat="server" Width="80px"
                                                                                    AutoPostBack="false" onblur="javascript:return DisableAirMin()"></asp:TextBox>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:TextBox ID="txtairMaximum" runat="server" Width="80px"></asp:TextBox>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:TextBox ID="txtairVariable" runat="server" Width="80px" Text="0"></asp:TextBox>
                                                                                <asp:DropDownList ID="ddlvarair" runat="server" AppendDataBoundItems="True" Width="50px">
                                                                                    <asp:ListItem>~Select~</asp:ListItem>
                                                                                    <asp:ListItem>Cav</asp:ListItem>
                                                                                    <asp:ListItem>Ass</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:Button ID="btnair" runat="server" Text="Add" OnClick="btnair_Click" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="8">
                                                                            <div id="dvair" style="overflow:scroll; height:220px;">
                                                                                <asp:GridView ID="GvAir" runat="server" CellPadding="4" Font-Names="Arial" AutoGenerateSelectButton="true"
                                                                                    Font-Size="10pt" Style="text-align: center; font-size: small;" PageSize="5"
                                                                                    ForeColor="#333333" Width="817px" AutoGenerateColumns="False" OnSelectedIndexChanged="GvAir_SelectedIndexChanged"
                                                                                     >
                                                                                    <FooterStyle BackColor="#507CD1" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                                                                        Font-Bold="True" ForeColor="White" Width="350px" />
                                                                                    <RowStyle BackColor="#EFF3FB" />
                                                                                    <RowStyle BackColor="#c9cbcc" ForeColor="White" Font-Size="Small" />
                                                                                    <SelectedRowStyle BackColor="#fffff2" ForeColor="White" />
                                                                                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                                                    <HeaderStyle BackColor="#3d99ea" ForeColor="White" />
                                                                                    <AlternatingRowStyle BackColor="#a8d6ff" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Id" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblId" Font-Names="verdana" CssClass="hiddencol" Text='<%#Bind("ID")%>'
                                                                                                    Font-Size="7pt" ForeColor="Black" Height="20px" Enabled="false" Width="70px" runat="server"></asp:Label>
                                                                                                <%--  Text='<%#Bind("ID")%>'--%>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Mode">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="ddlAirmode" runat="server" Font-Size="7pt" Enabled="false" Font-Names="verdana" Text='<%#Bind("ShipMode")%>' Height="20px" Width="70px"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Description">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtDescrip" Font-Names="verdana" Text='<%#Bind("Description")%>'
                                                                                                    Font-Size="7pt" Height="20px" Width="170px" Enabled="false" runat="server"></asp:TextBox>
                                                                                                <%-- Text='<%#Bind("Description")%>'--%>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Unit">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtUni" runat="server" Text='<%#Bind("Unit")%>' AppendDataBoundItems="True"
                                                                                                    Height="16px" Width="80px" Enabled="false">                        
                                                                                                </asp:TextBox>
                                                                                                <asp:AutoCompleteExtender ID="autoComplete1" runat="server" CompletionListCssClass="completionList"
                                                                                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                                                                    EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetQuoteUnit" ServicePath="~/AutoComplete.asmx"
                                                                                                    TargetControlID="txtUni">
                                                                                                </asp:AutoCompleteExtender>
                                                                                                <%-- <asp:DropDownList ID="ddlAir" runat="server" Width="100px" AutoPostBack="true">
                                                                                  
                                                                                    <asp:ListItem>~Select~</asp:ListItem>
                                                                                    <asp:ListItem>Per B/E</asp:ListItem>
                                                                                    <asp:ListItem>Per Shipment</asp:ListItem>
                                                                                    <asp:ListItem>Per Kg</asp:ListItem>
                                                                                    <asp:ListItem>Per Flat Rate</asp:ListItem>
                                                                                    <asp:ListItem>Per TON</asp:ListItem>
                                                                                    <asp:ListItem>Per Contr</asp:ListItem>
                                                                                    <asp:ListItem>Per LICENSE</asp:ListItem>
                                                                                    <asp:ListItem>Per PKG</asp:ListItem>
                                                                                    <asp:ListItem>Per TRA</asp:ListItem>
                                                                                    <asp:ListItem>At Actual</asp:ListItem>
                                                                                </asp:DropDownList>--%>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="FixedRate">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtFixRate" Font-Names="verdana" Font-Size="7pt" Text='<%#Bind("FixRate")%>'
                                                                                                    Height="20px" Width="70px" runat="server" Enabled="false"></asp:TextBox>
                                                                                                <%--Text='<%#Bind("FixRate")%>'--%>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="MinimumRate">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtMinRate" Font-Names="verdana" Font-Size="7pt" Text='<%#Bind("MinRate")%>'
                                                                                                    Height="20px" Width="70px" runat="server" Enabled="false"></asp:TextBox>
                                                                                                <%-- Text='<%#Bind("MinRate")%>'--%>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="MaximumRate">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtMaxRate" Font-Names="verdana" Font-Size="7pt" Text='<%#Bind("MaxRate")%>'
                                                                                                    Height="20px" Width="70px" runat="server" Enabled="false"></asp:TextBox>
                                                                                                <%--Text='<%#Bind("MaxRate")%>'--%>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="VariableRate">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtVarRate" Font-Names="verdana" Font-Size="7pt" Text='<%#Bind("VarRate")%>'
                                                                                                    Height="20px" Width="70px" runat="server" Enabled="false"></asp:TextBox>
                                                                                                <%-- Text='<%#Bind("VarRate")%>'--%>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="VariableType">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtvartype" Font-Names="verdana" Font-Size="7pt" Text='<%#Bind("VarType")%>'
                                                                                                    Height="20px" Width="70px" runat="server" Enabled="false"></asp:TextBox>
                                                                                                     <asp:AutoCompleteExtender ID="autoComplete2" runat="server" CompletionListCssClass="completionList"
                                                                                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                                                                    EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetVariableType" ServicePath="~/AutoComplete.asmx"
                                                                                                    TargetControlID="txtvartype">
                                                                                                </asp:AutoCompleteExtender>
                                                                                                <%-- <asp:DropDownList ID="ddlVarType" Font-Names="verdana" Font-Size="7pt" Height="20px"
                                                                                                    Width="70px" runat="server">
                                                                                                    <asp:ListItem>~Select~</asp:ListItem>
                                                                                                    <asp:ListItem>CAV</asp:ListItem>
                                                                                                    <asp:ListItem>ASS</asp:ListItem>
                                                                                                </asp:DropDownList>--%>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </Content>
                                            </asp:AccordionPane>
                                            <asp:AccordionPane ID="Pane3" runat="server">
                                                <Header>
                                                    Lcl</Header>
                                                <Content>
                                                    <table>
                                                        <tr>
                                                            <td colspan="8">
                                                                <asp:Panel ID="pnllcl" runat="server">
                                                                    <table>
                                                                        <tr>
                                                                            <td colspan="7" style="background; color: Black; text-align: center;">
                                                                                <strong>LCL</strong>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="center">
                                                                                ShipMode
                                                                            </td>
                                                                            <td class="center">
                                                                                Descripiton
                                                                            </td>
                                                                            <td class="center">
                                                                                Unit
                                                                            </td>
                                                                            <td class="center">
                                                                                Fixed
                                                                            </td>
                                                                            <td class="center">
                                                                                Minimum
                                                                            </td>
                                                                            <td class="center">
                                                                                Maximum
                                                                            </td>
                                                                            <td class="center">
                                                                                Variable
                                                                            </td>
                                                                            <td class="center">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="center">
                                                                                <asp:DropDownList ID="ddlShipModelcl" runat="server" Width="100px"
                                        AppendDataBoundItems="True" AutoPostBack="True">
                                        <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                        <asp:ListItem>Imp</asp:ListItem>
                                        <asp:ListItem>Exp</asp:ListItem>
                                        <asp:ListItem>Both</asp:ListItem>
                                    </asp:DropDownList>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:DropDownList ID="ddlLCLCharges" runat="server" Width="200px" AppendDataBoundItems="True">
                                                                                    <asp:ListItem>~Select~</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:DropDownList ID="ddlLCLUnit" runat="server" Width="100px" AutoPostBack="true"
                                                                                     onchange="javascript:return DisableLCL()">
                                                                                      <%--OnSelectedIndexChanged="ddlLCLUnit_SelectedIndexChanged"--%>
                                                                                    <asp:ListItem>~Select~</asp:ListItem>
                                                                                    <asp:ListItem>Per B/E</asp:ListItem>
                                                                                    <asp:ListItem>Per Shipment</asp:ListItem>
                                                                                    <asp:ListItem>Per Flat Rate</asp:ListItem>
                                                                                    <asp:ListItem>Per Kg</asp:ListItem>
                                                                                    <asp:ListItem>Per Contr</asp:ListItem>
                                                                                    <asp:ListItem>Per TON</asp:ListItem>
                                                                                    <asp:ListItem>Per PKG</asp:ListItem>
                                                                                    <asp:ListItem>Per LICENSE</asp:ListItem>
                                                                                    <asp:ListItem>Per TRA</asp:ListItem>
                                                                                    <asp:ListItem>At Actual</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:TextBox ID="txtLCLFixed" runat="server" Width="80px" onchange="javascript:return DisableLCLFixed()"
                                                                                    AutoPostBack="false" onblur="javascript:return DisableLclFixed();"></asp:TextBox>
                                                                                   <%-- OnTextChanged="txtLCLFixed_TextChanged"--%>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:TextBox ID="txtLCLMinimum" runat="server" Width="80px" 
                                                                                    AutoPostBack="false" onblur="javascript:return DisableLclMin();"></asp:TextBox>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:TextBox ID="txtLCLMaximum" runat="server" Width="80px"></asp:TextBox>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:TextBox ID="txtLCLVariable" runat="server" Width="80px" Text="0"></asp:TextBox>
                                                                                <asp:DropDownList ID="ddlvarlcl" runat="server" AppendDataBoundItems="True" Height="16px"
                                                                                    Width="50px">
                                                                                    <asp:ListItem>~Select~</asp:ListItem>
                                                                                    <asp:ListItem>Cav</asp:ListItem>
                                                                                    <asp:ListItem>Ass</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:Button ID="btnlcl" runat="server" Text="Add" OnClick="btnlcl_Click" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="8">
                                                                            <div id="dvlcl" style="overflow:scroll; height:220px;">
                                                                                <asp:GridView ID="GvLcl" runat="server" CellPadding="4" Font-Names="Arial" AutoGenerateSelectButton="true"
                                                                                    Font-Size="10pt" ForeColor="#333333" PageSize="5" Style="text-align: center; font-size: small;"
                                                                                    CssClass="grid-style" Width="817px" AutoGenerateColumns="False" OnSelectedIndexChanged="GvLcl_SelectedIndexChanged">
                                                                                    <FooterStyle BackColor="#507CD1" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                                                                        Font-Bold="True" ForeColor="White" Width="350px" />
                                                                                    <RowStyle BackColor="#EFF3FB" />
                                                                                    <RowStyle BackColor="#c9cbcc" ForeColor="White" Font-Size="Small" />
                                                                                    <SelectedRowStyle BackColor="#fffff2" ForeColor="White" />
                                                                                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                                                    <HeaderStyle BackColor="#3d99ea" ForeColor="White" />
                                                                                    <AlternatingRowStyle BackColor="#a8d6ff" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Id" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblId" Font-Names="verdana" Text='<%#Bind("ID")%>' CssClass="hiddencol"
                                                                                                    Font-Size="7pt" ForeColor="Black" Height="20px" Width="70px" Enabled="false" runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Mode">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="ddllclmode" runat="server" Text='<%#Bind("ShipMode")%>' Font-Names="verdana" Enabled="false" Font-Size="7pt" Height="20px" Width="70px"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Description">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtDescrip" Font-Names="verdana" Text='<%#Bind("Description")%>'
                                                                                                    Font-Size="7pt" Height="20px" Width="170px" runat="server" Enabled="false"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Unit">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtUni" runat="server" Text='<%#Bind("Unit")%>' AppendDataBoundItems="True"
                                                                                                    Height="16px" Width="80px" Enabled="false">                        
                                                                                                </asp:TextBox>
                                                                                                 <asp:AutoCompleteExtender ID="autoComplete11" runat="server" CompletionListCssClass="completionList"
                                                                                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                                                                    EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetQuoteUnit" ServicePath="~/AutoComplete.asmx"
                                                                                                    TargetControlID="txtUni">
                                                                                                </asp:AutoCompleteExtender>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="FixedRate">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtFixRate" Font-Names="verdana" Text='<%#Bind("FixRate")%>' Font-Size="7pt"
                                                                                                    Height="20px" Width="70px" runat="server" Enabled="false"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="MinimumRate">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtMinRate" Font-Names="verdana" Text='<%#Bind("MinRate")%>' Font-Size="7pt"
                                                                                                    Height="20px" Width="70px" runat="server" Enabled="false"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="MaximumRate">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtMaxRate" Font-Names="verdana" Text='<%#Bind("MaxRate")%>' Font-Size="7pt"
                                                                                                    Height="20px" Width="70px" runat="server" Enabled="false"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="VariableRate">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtVarRate" Font-Names="verdana" Text='<%#Bind("VarRate")%>' Font-Size="7pt"
                                                                                                    Height="20px" Width="70px" runat="server" Enabled="false"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="VariableType">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtVartype" Font-Names="verdana" Text='<%#Bind("VarType")%>' Font-Size="7pt"
                                                                                                    Height="20px" Width="70px" runat="server" Enabled="false"></asp:TextBox>
                                                                                                    <asp:AutoCompleteExtender ID="autoComplete12" runat="server" CompletionListCssClass="completionList"
                                                                                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                                                                    EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetVariableType" ServicePath="~/AutoComplete.asmx"
                                                                                                    TargetControlID="txtVartype">
                                                                                                </asp:AutoCompleteExtender>
                                                                                                <%--<asp:DropDownList ID="ddlVarType" Font-Names="verdana" Font-Size="7pt" Height="20px"
                                                                                                    Width="70px" runat="server">
                                                                                                    <asp:ListItem>~Select~</asp:ListItem>
                                                                                                    <asp:ListItem>CAV</asp:ListItem>
                                                                                                    <asp:ListItem>ASS</asp:ListItem>
                                                                                                </asp:DropDownList>--%>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="8">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </Content>
                                            </asp:AccordionPane>
                                            <asp:AccordionPane ID="Pane1" runat="server">
                                                <Header>
                                                    Feet20</Header>
                                                <Content>
                                                    <table>
                                                        <tr>
                                                            <td colspan="8">
                                                                <asp:Panel ID="pnl20feet" runat="server">
                                                                    <table>
                                                                        <tr>
                                                                            <td colspan="7" style="background: #; color: Black; text-align: Center;">
                                                                                <strong>20 FEET</strong>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="center">
                                                                                ShipMode
                                                                            </td>
                                                                            <td class="center">
                                                                                Descripiton
                                                                            </td>
                                                                            <td class="center">
                                                                                Unit
                                                                            </td>
                                                                            <td class="center">
                                                                                Fixed
                                                                            </td>
                                                                            <td class="center">
                                                                                Minimum
                                                                            </td>
                                                                            <td class="center">
                                                                                Maximum
                                                                            </td>
                                                                            <td class="center">
                                                                                Variable
                                                                            </td>
                                                                            <td class="center">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="center">
                                                                                <asp:DropDownList ID="ddlShipMode20" runat="server" Width="100px" AutoPostBack="True"
                                        AppendDataBoundItems="True" >
                                        <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                        <asp:ListItem>Imp</asp:ListItem>
                                        <asp:ListItem>Exp</asp:ListItem>
                                        <asp:ListItem>Both</asp:ListItem>
                                    </asp:DropDownList>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:DropDownList ID="ddl20FeetCharges" runat="server" Width="200px" AppendDataBoundItems="True"
                                                                                    >
                                                                                    <asp:ListItem>~Select~</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:DropDownList ID="ddl20FeetUnit" runat="server" Width="100px" AutoPostBack="true"
                                                                                   onchange="javascript:return Disable20FE()" AppendDataBoundItems="True">
                                                                                     <%--OnSelectedIndexChanged="ddl20FeetUnit_SelectedIndexChanged"--%>
                                                                                    <asp:ListItem>~Select~</asp:ListItem>
                                                                                    <asp:ListItem>Per B/E</asp:ListItem>
                                                                                    <asp:ListItem>Per shipment</asp:ListItem>
                                                                                    <asp:ListItem>Per Flat Rate</asp:ListItem>
                                                                                    <asp:ListItem>Per Kg</asp:ListItem>
                                                                                    <asp:ListItem>Per Contr</asp:ListItem>
                                                                                    <asp:ListItem>Per TON</asp:ListItem>
                                                                                    <asp:ListItem>Per PKG</asp:ListItem>
                                                                                    <asp:ListItem>Per LICENSE</asp:ListItem>
                                                                                    <asp:ListItem>Per TRA</asp:ListItem>
                                                                                    <asp:ListItem>At Actual</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:TextBox ID="txt20feetFixed" runat="server" Width="80px"  onchange="javascript:return Disable20Fixed()"
                                                                                    AutoPostBack="true" onblur="javascript:return Disable20Fixed();"></asp:TextBox>
                                                                                    <%--OnTextChanged="txt20feetFixed_TextChanged"--%>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:TextBox ID="txt20feetMinimum" runat="server" Width="80px"
                                                                                    AutoPostBack="true" onblur="javascript:return Disable20Min();"></asp:TextBox>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:TextBox ID="txt20feetMaximum" runat="server" Width="80px"></asp:TextBox>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:TextBox ID="txt20feetVariable" runat="server" Width="80px" Text="0"></asp:TextBox>
                                                                                <asp:DropDownList ID="ddlvar20" runat="server" AppendDataBoundItems="True" Width="50px">
                                                                                    <asp:ListItem>~Select~</asp:ListItem>
                                                                                    <asp:ListItem>Cav</asp:ListItem>
                                                                                    <asp:ListItem>Ass</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:Button ID="btnAdd20feet" runat="server" Text="Add" OnClick="btnAdd20feet_Click" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="8">
                                                                            <div id="dvfe20" style="overflow:scroll; height:220px;">
                                                                                <asp:GridView ID="Gv20feet" runat="server" CellPadding="4" Font-Names="Arial" AutoGenerateSelectButton="true"
                                                                                    Font-Size="10pt" ForeColor="#333333" PageSize="5" Style="text-align: center; font-size: small;"
                                                                                    CssClass="grid-style" Width="817px" AutoGenerateColumns="False" OnSelectedIndexChanged="Gv20feet_SelectedIndexChanged" >
                                                                                    <FooterStyle BackColor="#507CD1" BorderColor="#fff" BorderStyle="Solid" BorderWidth="1px"
                                                                                        Font-Bold="True" ForeColor="White" Width="350px" />
                                                                                    <RowStyle BackColor="#a8d6ff" />
                                                                                    <RowStyle BackColor="#c9cbcc" ForeColor="White" Font-Size="Small" />
                                                                                    <SelectedRowStyle BackColor="#a8d6ff" ForeColor="White" />
                                                                                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                                                    <HeaderStyle BackColor="#3d99ea" ForeColor="White" />
                                                                                    <AlternatingRowStyle BackColor="#cfe9ff" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Id" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblId" Font-Names="verdana" Text='<%#Bind("ID")%>' CssClass="hiddencol"
                                                                                                    Font-Size="7pt" ForeColor="Black" Height="20px" Width="70px" runat="server" Enabled="false"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Mode">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="ddl20femode" runat="server" Enabled="false" Font-Names="verdana" Text='<%#Bind("ShipMode")%>' Font-Size="7pt" Height="20px" Width="70px"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Description">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtDescrip" Font-Names="verdana" Text='<%#Bind("Description")%>'
                                                                                                    Font-Size="7pt" Height="20px" Width="170px" runat="server" Enabled="false"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Unit">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtUni" runat="server" Text='<%#Bind("Unit")%>' AppendDataBoundItems="True"
                                                                                                     Height="16px" Width="80px" Enabled="false">                        
                                                                                                </asp:TextBox>
                                                                                                 <asp:AutoCompleteExtender ID="autoComplete" runat="server" CompletionListCssClass="completionList"
                                                                                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                                                                    EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetQuoteUnit" ServicePath="~/AutoComplete.asmx"
                                                                                                    TargetControlID="txtUni">
                                                                                                </asp:AutoCompleteExtender>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="FixedRate">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtFixRate" Font-Names="verdana" Text='<%#Bind("FixRate")%>' Font-Size="7pt"
                                                                                                    Height="20px" Width="70px" runat="server" Enabled="false"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="MinimumRate">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtMinRate" Font-Names="verdana" Text='<%#Bind("MinRate")%>' Font-Size="7pt"
                                                                                                    Height="20px" Width="70px" runat="server" Enabled="false"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="MaximumRate">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtMaxRate" Font-Names="verdana" Text='<%#Bind("MaxRate")%>' Font-Size="7pt"
                                                                                                    Height="20px" Width="70px" runat="server" Enabled="false"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="VariableRate">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtVarRate" Font-Names="verdana" Text='<%#Bind("VarRate")%>' Font-Size="7pt"
                                                                                                    Height="20px" Width="70px" runat="server" Enabled="false"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="VariableType">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtVartype" Font-Names="verdana" Text='<%#Bind("VarType")%>' Font-Size="7pt"
                                                                                                    Height="20px" Width="70px" runat="server" Enabled="false"></asp:TextBox>
                                                                                                    <asp:AutoCompleteExtender ID="autoComplete32" runat="server" CompletionListCssClass="completionList"
                                                                                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                                                                    EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetVariableType" ServicePath="~/AutoComplete.asmx"
                                                                                                    TargetControlID="txtVartype">
                                                                                                </asp:AutoCompleteExtender>
                                                                                                <%--   <asp:DropDownList ID="ddlVarType" Font-Names="verdana" Font-Size="7pt" Height="20px"
                                                                                                    Width="70px" runat="server">
                                                                                                    <asp:ListItem>~Select~</asp:ListItem>
                                                                                                    <asp:ListItem>CAV</asp:ListItem>
                                                                                                    <asp:ListItem>ASS</asp:ListItem>
                                                                                                </asp:DropDownList>--%>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="8">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </Content>
                                            </asp:AccordionPane>
                                            <asp:AccordionPane ID="Pane2" runat="server">
                                                <Header>
                                                    Feet40
                                                </Header>
                                                <Content>
                                                    <table>
                                                        <tr>
                                                            <td colspan="8">
                                                                <asp:Panel ID="pnl40feet" runat="server">
                                                                    <table>
                                                                        <tr>
                                                                            <td colspan="7" style="background; color: Black; text-align: center;">
                                                                                <strong>40 FEET</strong>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="center">
                                                                                ShipMode
                                                                            </td>
                                                                            <td class="center">
                                                                                Descripiton
                                                                            </td>
                                                                            <td class="center">
                                                                                Unit
                                                                            </td>
                                                                            <td class="center">
                                                                                Fixed
                                                                            </td>
                                                                            <td class="center">
                                                                                Minimum
                                                                            </td>
                                                                            <td class="center">
                                                                                Maximum
                                                                            </td>
                                                                            <td class="center">
                                                                                Variable
                                                                            </td>
                                                                            <td class="center">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="center">
                                                                                <asp:DropDownList ID="ddlShipMode40" runat="server" Width="100px"
                                        AppendDataBoundItems="True" AutoPostBack="True">
                                        <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                        <asp:ListItem>Imp</asp:ListItem>
                                        <asp:ListItem>Exp</asp:ListItem>
                                        <asp:ListItem>Both</asp:ListItem>
                                    </asp:DropDownList>
                                                                            </td>
                                                                            <td class="center">
                                                                                <asp:DropDownList ID="ddl40FeetCharges" runat="server" Width="200px" AppendDataBoundItems="True">
                                                                                    <asp:ListItem>~Select~</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:DropDownList ID="ddl40feetUnit" runat="server" Width="100px" AutoPostBack="true"
                                                                                     onchange="javascript:return Disable40FE()">
                                                                                     <%--OnSelectedIndexChanged="ddl40feetUnit_SelectedIndexChanged"--%>
                                                                                    <asp:ListItem>~Select~</asp:ListItem>
                                                                                    <asp:ListItem>Per B/E</asp:ListItem>
                                                                                    <asp:ListItem>Per Shipment</asp:ListItem>
                                                                                    <asp:ListItem>Per Flat Rate</asp:ListItem>
                                                                                    <asp:ListItem>Per Kg</asp:ListItem>
                                                                                    <asp:ListItem>Per Contr</asp:ListItem>
                                                                                    <asp:ListItem>Per TON</asp:ListItem>
                                                                                    <asp:ListItem>Per PKG</asp:ListItem>
                                                                                    <asp:ListItem>Per LICENSE</asp:ListItem>
                                                                                    <asp:ListItem>Per TRA</asp:ListItem>
                                                                                    <asp:ListItem>At Actual</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:TextBox ID="txt40FeetFixed" runat="server" Width="80px" onchange="javascript:return Disable40Fixed()"
                                                                                    AutoPostBack="true" onblur="javascript:return Disable40Fixed();"></asp:TextBox>
                                                                                    <%--OnTextChanged="txt40FeetFixed_TextChanged"--%>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:TextBox ID="txt40FeetMinimum" runat="server" Width="80px" 
                                                                                    AutoPostBack="true" onblur="javascript:return Disable40Min();"></asp:TextBox>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:TextBox ID="txt40FeetMaximum" runat="server" Width="80px"></asp:TextBox>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:TextBox ID="txt40FeetVariable" runat="server" Width="80px" Text="0"></asp:TextBox>
                                                                                <asp:DropDownList ID="ddlvar40" runat="server" AppendDataBoundItems="True" Width="50px">
                                                                                    <asp:ListItem>~Select~</asp:ListItem>
                                                                                    <asp:ListItem>Cav</asp:ListItem>
                                                                                    <asp:ListItem>Ass</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:Button ID="btnAdd40feet" runat="server" Text="Add" OnClick="btnAdd40feet_Click" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="8">
                                                                            <div id="dvfe40" style="overflow:scroll; height:220px;">
                                                                                <asp:GridView ID="Gv40Feet" runat="server" CellPadding="4" Font-Names="Arial" AutoGenerateSelectButton="true"
                                                                                    Font-Size="10pt" ForeColor="#333333" PageSize="5" Style="text-align: center; font-size: small;"
                                                                                    CssClass="grid-style" Width="817px" AutoGenerateColumns="False" OnSelectedIndexChanged="Gv40Feet_SelectedIndexChanged" >
                                                                                    <FooterStyle BackColor="#507CD1" BorderColor="#fff" BorderStyle="Solid" BorderWidth="1px"
                                                                                        Font-Bold="True" ForeColor="White" Width="350px" />
                                                                                    <RowStyle BackColor="#EFF3FB" />
                                                                                    <RowStyle BackColor="#c9cbcc" ForeColor="White" Font-Size="Small" />
                                                                                    <SelectedRowStyle BackColor="#fffff2" ForeColor="White" />
                                                                                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                                                    <HeaderStyle BackColor="#3d99ea" ForeColor="White" />
                                                                                    <AlternatingRowStyle BackColor="#a8d6ff" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Id" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblId" Font-Names="verdana" Text='<%#Bind("ID")%>' CssClass="hiddencol"
                                                                                                    Font-Size="7pt" ForeColor="Black" Height="20px" Width="70px" Enabled="false" runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Mode">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="ddl40femode" runat="server" Enabled="false" Text='<%#Bind("ShipMode")%>' Font-Names="verdana" Font-Size="7pt" Height="20px" Width="70px"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Description">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtDescrip" Font-Names="verdana" Text='<%#Bind("Description")%>'
                                                                                                    Font-Size="7pt" Height="20px" Width="170px" runat="server" Enabled="false"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Unit">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtUni" runat="server" Text='<%#Bind("Unit")%>' AppendDataBoundItems="True"
                                                                                                    Height="16px" Width="80px" Enabled="false">                        
                                                                                                </asp:TextBox>
                                                                                                 <asp:AutoCompleteExtender ID="autoComplete16" runat="server" CompletionListCssClass="completionList"
                                                                                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                                                                    EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetQuoteUnit" ServicePath="~/AutoComplete.asmx"
                                                                                                    TargetControlID="txtUni">
                                                                                                </asp:AutoCompleteExtender>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="FixedRate">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtFixRate" Font-Names="verdana" Text='<%#Bind("FixRate")%>' Font-Size="7pt"
                                                                                                    Height="20px" Width="70px" runat="server" Enabled="false"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="MinimumRate">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtMinRate" Font-Names="verdana" Text='<%#Bind("MinRate")%>' Font-Size="7pt"
                                                                                                    Height="20px" Width="70px" runat="server" Enabled="false"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="MaximumRate">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtMaxRate" Font-Names="verdana" Text='<%#Bind("MaxRate")%>' Font-Size="7pt"
                                                                                                    Height="20px" Width="70px" runat="server" Enabled="false"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="VariableRate">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtVarRate" Font-Names="verdana" Text='<%#Bind("VarRate")%>' Font-Size="7pt"
                                                                                                    Height="20px" Width="70px" runat="server" Enabled="false"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="VariableType">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtVartype" Font-Names="verdana" Text='<%#Bind("VarType")%>' Font-Size="7pt"
                                                                                                    Height="20px" Width="70px" runat="server" Enabled="false"></asp:TextBox>
                                                                                                    <asp:AutoCompleteExtender ID="autoComplete15" runat="server" CompletionListCssClass="completionList"
                                                                                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                                                                    EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetVariableType" ServicePath="~/AutoComplete.asmx"
                                                                                                    TargetControlID="txtVartype">
                                                                                                </asp:AutoCompleteExtender>
                                                                                                <%-- <asp:DropDownList ID="ddlVarType" Font-Names="verdana" Font-Size="7pt" Height="20px"
                                                                                                    Width="70px" runat="server">
                                                                                                    <asp:ListItem>~Select~</asp:ListItem>
                                                                                                    <asp:ListItem>CAV</asp:ListItem>
                                                                                                    <asp:ListItem>ASS</asp:ListItem>
                                                                                                </asp:DropDownList>--%>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="8">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </Content>
                                            </asp:AccordionPane>
                                        </Panes>
                                    </asp:Accordion>
                                </td>
                            </tr>

            <tr>
                <td colspan="8">
                    <asp:Panel ID="pnlbuttons" runat="server">
                        <table>
                            <tr>
                                <%--<td class="left">
                    &nbsp;
                </td>--%>
                                <td align="center">
                                    <asp:Button ID="btnSave" CssClass="Button123" runat="server" Text="Add"
                                        Width="80px" OnClientClick="javascript:return mandatory();" 
                                        Visible="False" />
                                    <asp:Button ID="btnUpdate" CssClass="Button123" runat="server" Text="Update"
                                        Visible="False" Width="80px" />
                                    <asp:Button ID="Button2" runat="server" CssClass="Button123" Text="Cancel" OnClick="Button2_Click"
                                        Width="80px" Visible="False" />
                                </td>
                                <%--<td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>--%>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            
        </table>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmQuote.aspx.cs" Inherits="ImpexCube.CRM.frmQuote1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
         
         select.yourclasshere  
         {
             width:200px }
             
           #myselect {
                 width:230px;
               }
               #myselect option {
          width:220px;
           }
        .style1
        {
            font-size: large;
        }
        .style3
        {
            width: 508px;
        }
        .displaynone
        {
            display: none;
        }
        
        .accordion
        {
            width: 1100px;
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
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js" type="text/javascript">
    </script>
    <script type="text/javascript">

       

        $(document).ready(function () {
            $("#hide").click(function () {
                $("#hide").hide();
            });
        });

        function DisableAir() {
            var AirSelect = document.getElementById('<%=ddlAirUnit.ClientID%>');
            var selectedText = AirSelect.options[AirSelect.selectedIndex].text;
            if (selectedText == "At Actual") {
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

        function DisableAirFixed() {
            var AirSelectF = document.getElementById('<%=txtairFixed.ClientID%>').value;
            if (AirSelectF == "") {

                document.getElementById('<%=txtairminimum.ClientID%>').disabled = false;
                document.getElementById('<%=txtairMaximum.ClientID%>').disabled = false;
                document.getElementById('<%=txtairVariable.ClientID%>').disabled = false;
                document.getElementById('<%=ddlvarair.ClientID%>').disabled = false;
                return false;
            }

            else {

                document.getElementById('<%=txtairminimum.ClientID%>').disabled = true;
                document.getElementById('<%=txtairMaximum.ClientID%>').disabled = true;
                document.getElementById('<%=txtairVariable.ClientID%>').disabled = true;
                document.getElementById('<%=ddlvarair.ClientID%>').disabled = true;
                return false;
            }
        }

       

        function DisableLCL() {
            var LCLSelect = document.getElementById('<%=ddlLCLUnit.ClientID%>');
            var selectedText = LCLSelect.options[LCLSelect.selectedIndex].text;
            if (selectedText == "At Actual") {
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

        function DisableLCLFixed() {
            var LCLSelectF = document.getElementById('<%=txtLCLFixed.ClientID%>').value;
            if (LCLSelectF == "") {

                document.getElementById('<%=txtLCLMinimum.ClientID%>').disabled = false;
                document.getElementById('<%=txtLCLMaximum.ClientID%>').disabled = false;
                document.getElementById('<%=txtLCLVariable.ClientID%>').disabled = false;
                document.getElementById('<%=ddlvarlcl.ClientID%>').disabled = false;
                return false;
            }

            else {

                document.getElementById('<%=txtLCLMinimum.ClientID%>').disabled = true;
                document.getElementById('<%=txtLCLMaximum.ClientID%>').disabled = true;
                document.getElementById('<%=txtLCLVariable.ClientID%>').disabled = true;
                document.getElementById('<%=ddlvarlcl.ClientID%>').disabled = true;
                return false;
            }
        }

        function Disable20() {
            var twentySelect = document.getElementById('<%=ddl20FeetUnit.ClientID%>');
            var selectedText = twentySelect.options[twentySelect.selectedIndex].text;
            if (selectedText == "At Actual") {
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


        function Disable20Fixed() {
            var twentySelectF = document.getElementById('<%=txt20feetFixed.ClientID%>').value;
            if (twentySelectF == "") {

                document.getElementById('<%=txt20feetMinimum.ClientID%>').disabled = false;
                document.getElementById('<%=txt20feetMaximum.ClientID%>').disabled = false;
                document.getElementById('<%=txt20feetVariable.ClientID%>').disabled = false;
                document.getElementById('<%=ddlvar20.ClientID%>').disabled = false;
                return false;
            }

            else {

                document.getElementById('<%=txt20feetMinimum.ClientID%>').disabled = true;
                document.getElementById('<%=txt20feetMaximum.ClientID%>').disabled = true;
                document.getElementById('<%=txt20feetVariable.ClientID%>').disabled = true;
                document.getElementById('<%=ddlvar20.ClientID%>').disabled = true;
                return false;
            }
        }

        function Disable40() {
            var fourtySelect = document.getElementById('<%=ddl40feetUnit.ClientID%>');
            var selectedText = fourtySelect.options[fourtySelect.selectedIndex].text;
            if (selectedText == "At Actual") {
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

        function Disable40Fixed() {
            var fourtySelectF = document.getElementById('<%=txt40FeetFixed.ClientID%>').value;
            if (fourtySelectF == "") {

                document.getElementById('<%=txt40FeetMinimum.ClientID%>').disabled = false;
                document.getElementById('<%=txt40FeetMaximum.ClientID%>').disabled = false;
                document.getElementById('<%=txt40FeetVariable.ClientID%>').disabled = false;
                document.getElementById('<%=ddlvar40.ClientID%>').disabled = false;
                return false;
            }

            else {

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
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="width: 106%; margin-right: 28px;">
        <table style="width: 69%" align="center">
            <tr>
                <td colspan="7" style="text-align: center" class="style1">
                    <strong style="font-family: Arial">Quote </strong>
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <asp:Panel ID="pnlChecking" runat="server">
                        <table>
                            <tr>
                                <td align="center">
                                    <asp:CheckBox ID="chkPendEnq" runat="server" AutoPostBack="True" OnCheckedChanged="chkPendEnq_CheckedChanged"
                                        Text="Pending Enquiry" />
                                    <asp:CheckBox ID="chkQuotechrgs" runat="server" AutoPostBack="True" OnCheckedChanged="chkQuotechrgs_CheckedChanged"
                                        Text="Quote Charges" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <asp:Panel ID="pnlGridEnquiry" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <asp:GridView ID="GcEnquiry" runat="server" AllowPaging="True" AutoGenerateSelectButton="true"
                                        CellPadding="4" Font-Names="Arial" Font-Size="10pt" ForeColor="#333333" Style="text-align: center;
                                        font-size: small;" CssClass="grid-style" Width="864px" OnSelectedIndexChanged="GcEnquiry_SelectedIndexChanged"
                                        OnPageIndexChanging="GcEnquiry_PageIndexChanging" 
                                        onrowdatabound="GcEnquiry_RowDataBound">
                                        <RowStyle BackColor="#c9cbcc" ForeColor="White" Font-Size="Small" />
                                        <AlternatingRowStyle BackColor="#a8d6ff" />
                                        <FooterStyle BackColor="#507CD1" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Bold="True" ForeColor="White" Width="350px" />
                                        <HeaderStyle BackColor="#3d99ea" ForeColor="White" />
                                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                        <RowStyle BackColor="#EFF3FB" ForeColor="Black" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <asp:Panel ID="pnlEnqDetails" runat="server">
                        <table>
                            <tr>
                                <td colspan="6">
                                    <strong>Enquiry Details</strong></td>
                            </tr>
                            <tr>
                                <td>
                                    Quote No</td>
                                <td>
                                    <asp:Label ID="lblQuoteNo" runat="server"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td class="style3" colspan="3">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    Customer Name
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCustName" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    Commodity
                                </td>
                                <td class="style3" colspan="3">
                                    <asp:TextBox ID="txtCommodity" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Pod
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPol" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    Pol
                                </td>
                                <td class="style3" colspan="3">
                                    <asp:TextBox ID="txtPod" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Final Destination
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFinDest" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblEnqId" runat="server" Text="Enq Id" Visible="False"></asp:Label>
                                </td>
                                <td class="style3">
                                    <asp:TextBox ID="txtEnqId" runat="server" Visible="False"></asp:TextBox>
                                </td>
                                <td class="style3">
                                    <asp:Button ID="btnSavEnq" runat="server" BackColor="#76A7F8" ForeColor="White" 
                                        onclick="btnSavEnq_Click" Text="Save" />
                                </td>
                                <td class="style3">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <asp:Panel ID="pnlCustName" runat="server">
                        <table>
                            <tr>
                                <td>
                                    Customer Name&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlCustNam" runat="server" Width="180px">
                                    </asp:DropDownList>
                                    &nbsp;
                                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" 
                                        Text="Search" BackColor="#76A7F8" ForeColor="White" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="8">
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <asp:Panel ID="pnlMain" runat="server">
                        <table width="850px">
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
                                                    <table width="850px">
                                                        <tr>
                                                            <td colspan="8">
                                                                <asp:Panel ID="pnlair" runat="server">
                                                                    <table width="850px">
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
                                                                                <asp:Label ID="lblAirShipment" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:DropDownList ID="ddlAIRCharges" runat="server" Width="200px" AppendDataBoundItems="True" CssClass="yourclasshere">
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
                                                                                    AutoPostBack="true" onchange="javascript:return DisableAirFixed()"></asp:TextBox>
                                                                                    <%--OnTextChanged="txtairFixed_TextChanged"--%>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:TextBox ID="txtairminimum" runat="server" Width="80px" OnTextChanged="txtairminimum_TextChanged"
                                                                                    AutoPostBack="true"></asp:TextBox>
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
                                                                                <asp:GridView ID="GvAir" runat="server" PageSize="5" CellPadding="4" Font-Names="Arial"
                                                                                    Font-Size="10pt" Style="text-align: center; font-size: small;" CssClass="grid-style"
                                                                                    ForeColor="#333333" Width="817px" AutoGenerateColumns="False" OnSelectedIndexChanged="GvAir_SelectedIndexChanged"
                                                                                    OnRowDataBound="GvAir_RowDataBound">
                                                                                    <FooterStyle BackColor="#507CD1" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                                                                        Font-Bold="True" ForeColor="White" Width="350px" />
                                                                                    <RowStyle BackColor="#EFF3FB" />
                                                                                    <RowStyle BackColor="#c9cbcc" ForeColor="White" Font-Size="Small" />
                                                                                    <SelectedRowStyle BackColor="#fffff2" ForeColor="White" />
                                                                                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                                                    <HeaderStyle BackColor="#3d99ea" ForeColor="White" />
                                                                                    <AlternatingRowStyle BackColor="#a8d6ff" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Id" HeaderStyle-CssClass="displaynone" ItemStyle-CssClass="displaynone">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblId" Font-Names="verdana" CssClass="displaynone" Text='<%#Bind("ID")%>'
                                                                                                    Font-Size="7pt" ForeColor="Black" Height="20px" Width="70px" runat="server"></asp:Label>
                                                                                                <%--  Text='<%#Bind("ID")%>'--%>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Description">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtDescrip" Font-Names="verdana" Text='<%#Bind("Description")%>'
                                                                                                    Font-Size="7pt" Height="20px" Width="170px" runat="server"></asp:TextBox>
                                                                                                <%-- Text='<%#Bind("Description")%>'--%>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Unit">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtUni" runat="server" Text='<%#Bind("Unit")%>' AppendDataBoundItems="True"
                                                                                                    Height="16px" Width="80px">                        
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
                                                                                                    Height="20px" Width="70px" runat="server"></asp:TextBox>
                                                                                                <%--Text='<%#Bind("FixRate")%>'--%>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="MinimumRate">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtMinRate" Font-Names="verdana" Font-Size="7pt" Text='<%#Bind("MinRate")%>'
                                                                                                    Height="20px" Width="70px" runat="server"></asp:TextBox>
                                                                                                <%-- Text='<%#Bind("MinRate")%>'--%>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="MaximumRate">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtMaxRate" Font-Names="verdana" Font-Size="7pt" Text='<%#Bind("MaxRate")%>'
                                                                                                    Height="20px" Width="70px" runat="server"></asp:TextBox>
                                                                                                <%--Text='<%#Bind("MaxRate")%>'--%>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="VariableRate">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtVarRate" Font-Names="verdana" Font-Size="7pt" Text='<%#Bind("VarRate")%>'
                                                                                                    Height="20px" Width="70px" runat="server"></asp:TextBox>
                                                                                                <%-- Text='<%#Bind("VarRate")%>'--%>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="VariableType">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtvartype" Font-Names="verdana" Font-Size="7pt" Text='<%#Bind("VarType")%>'
                                                                                                    Height="20px" Width="70px" runat="server"></asp:TextBox>
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
                                                                                        <asp:TemplateField HeaderText="Charges">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkChargeair" runat="server" />
                                                                                            </ItemTemplate>
                                                                                            <EditItemTemplate>
                                                                                                <asp:CheckBox ID="chkChargeair" runat="server" />
                                                                                            </EditItemTemplate>
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
                                                    <table width="850px">
                                                        <tr>
                                                            <td colspan="8">
                                                                <asp:Panel ID="pnllcl" runat="server">
                                                                    <table width="850px">
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
                                                                                <asp:Label ID="lblLCLShipment" runat="server"></asp:Label>
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
                                                                                    AutoPostBack="true"></asp:TextBox>
                                                                                   <%-- OnTextChanged="txtLCLFixed_TextChanged"--%>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:TextBox ID="txtLCLMinimum" runat="server" Width="80px" OnTextChanged="txtLCLMinimum_TextChanged"
                                                                                    AutoPostBack="true"></asp:TextBox>
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
                                                                            <div id="Div1" style="overflow:scroll; height:220px;">
                                                                                <asp:GridView ID="GvLcl" runat="server" PageSize="5" CellPadding="4" Font-Names="Arial"
                                                                                    Font-Size="10pt" ForeColor="#333333" Style="text-align: center; font-size: small;"
                                                                                    CssClass="grid-style" Width="817px" AutoGenerateColumns="False" >
                                                                                    <FooterStyle BackColor="#507CD1" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                                                                        Font-Bold="True" ForeColor="White" Width="350px" />
                                                                                    <RowStyle BackColor="#EFF3FB" />
                                                                                    <RowStyle BackColor="#c9cbcc" ForeColor="White" Font-Size="Small" />
                                                                                    <SelectedRowStyle BackColor="#fffff2" ForeColor="White" />
                                                                                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                                                    <HeaderStyle BackColor="#3d99ea" ForeColor="White" />
                                                                                    <AlternatingRowStyle BackColor="#a8d6ff" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Id" HeaderStyle-CssClass="displaynone" ItemStyle-CssClass="displaynone">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblId" Font-Names="verdana" Text='<%#Bind("ID")%>' CssClass="displaynone"
                                                                                                    Font-Size="7pt" ForeColor="Black" Height="20px" Width="70px" runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Description">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtDescrip" Font-Names="verdana" Text='<%#Bind("Description")%>'
                                                                                                    Font-Size="7pt" Height="20px" Width="170px" runat="server"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Unit">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtUni" runat="server" Text='<%#Bind("Unit")%>' AppendDataBoundItems="True"
                                                                                                    Height="16px" Width="80px">                        
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
                                                                                                    Height="20px" Width="70px" runat="server"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="MinimumRate">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtMinRate" Font-Names="verdana" Text='<%#Bind("MinRate")%>' Font-Size="7pt"
                                                                                                    Height="20px" Width="70px" runat="server"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="MaximumRate">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtMaxRate" Font-Names="verdana" Text='<%#Bind("MaxRate")%>' Font-Size="7pt"
                                                                                                    Height="20px" Width="70px" runat="server"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="VariableRate">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtVarRate" Font-Names="verdana" Text='<%#Bind("VarRate")%>' Font-Size="7pt"
                                                                                                    Height="20px" Width="70px" runat="server"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="VariableType">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtVartype" Font-Names="verdana" Text='<%#Bind("VarType")%>' Font-Size="7pt"
                                                                                                    Height="20px" Width="70px" runat="server"></asp:TextBox>
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
                                                                                        <asp:TemplateField HeaderText="Charges">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkChargelcl" runat="server" />
                                                                                            </ItemTemplate>
                                                                                            <EditItemTemplate>
                                                                                                <asp:CheckBox ID="chkChargelcl" runat="server" />
                                                                                            </EditItemTemplate>
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
                                                    <table width="850px">
                                                        <tr>
                                                            <td colspan="8">
                                                                <asp:Panel ID="pnl20feet" runat="server">
                                                                    <table width="850px">
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
                                                                                <asp:Label ID="lbl20FeetShipment" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:DropDownList ID="ddl20FeetCharges" runat="server" Width="200px" AppendDataBoundItems="True"
                                                                                    OnSelectedIndexChanged="ddl20FeetCharges_SelectedIndexChanged">
                                                                                    <asp:ListItem>~Select~</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:DropDownList ID="ddl20FeetUnit" runat="server" Width="100px" AutoPostBack="true"
                                                                                   onchange="javascript:return Disable20()" AppendDataBoundItems="True">
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
                                                                                    AutoPostBack="true"></asp:TextBox>
                                                                                    <%--OnTextChanged="txt20feetFixed_TextChanged"--%>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:TextBox ID="txt20feetMinimum" runat="server" Width="80px" OnTextChanged="txt20feetMinimum_TextChanged"
                                                                                    AutoPostBack="true"></asp:TextBox>
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
                                                                            <div id="Div2" style="overflow:scroll; height:220px;">
                                                                                <asp:GridView ID="Gv20feet" runat="server" PageSize="5" CellPadding="4" Font-Names="Arial"
                                                                                    Font-Size="10pt" ForeColor="#333333" Style="text-align: center; font-size: small;"
                                                                                    CssClass="grid-style" Width="817px" AutoGenerateColumns="False">
                                                                                    <FooterStyle BackColor="#507CD1" BorderColor="#fff" BorderStyle="Solid" BorderWidth="1px"
                                                                                        Font-Bold="True" ForeColor="White" Width="350px" />
                                                                                    <RowStyle BackColor="#a8d6ff" />
                                                                                    <RowStyle BackColor="#c9cbcc" ForeColor="White" Font-Size="Small" />
                                                                                    <SelectedRowStyle BackColor="#a8d6ff" ForeColor="White" />
                                                                                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                                                    <HeaderStyle BackColor="#3d99ea" ForeColor="White" />
                                                                                    <AlternatingRowStyle BackColor="#cfe9ff" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Id" HeaderStyle-CssClass="displaynone" ItemStyle-CssClass="displaynone">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblId" Font-Names="verdana" Text='<%#Bind("ID")%>' CssClass="displaynone"
                                                                                                    Font-Size="7pt" ForeColor="Black" Height="20px" Width="70px" runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Description">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtDescrip" Font-Names="verdana" Text='<%#Bind("Description")%>'
                                                                                                    Font-Size="7pt" Height="20px" Width="170px" runat="server"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Unit">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtUni" runat="server" Text='<%#Bind("Unit")%>' AppendDataBoundItems="True"
                                                                                                     Height="16px" Width="80px">                        
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
                                                                                                    Height="20px" Width="70px" runat="server"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="MinimumRate">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtMinRate" Font-Names="verdana" Text='<%#Bind("MinRate")%>' Font-Size="7pt"
                                                                                                    Height="20px" Width="70px" runat="server"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="MaximumRate">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtMaxRate" Font-Names="verdana" Text='<%#Bind("MaxRate")%>' Font-Size="7pt"
                                                                                                    Height="20px" Width="70px" runat="server"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="VariableRate">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtVarRate" Font-Names="verdana" Text='<%#Bind("VarRate")%>' Font-Size="7pt"
                                                                                                    Height="20px" Width="70px" runat="server"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="VariableType">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtVartype" Font-Names="verdana" Text='<%#Bind("VarType")%>' Font-Size="7pt"
                                                                                                    Height="20px" Width="70px" runat="server"></asp:TextBox>
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
                                                                                        <asp:TemplateField HeaderText="Charges">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkCharge" runat="server" />
                                                                                            </ItemTemplate>
                                                                                            <EditItemTemplate>
                                                                                                <asp:CheckBox ID="chkCharge" runat="server" />
                                                                                            </EditItemTemplate>
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
                                                    <table width="850px">
                                                        <tr>
                                                            <td colspan="8">
                                                                <asp:Panel ID="pnl40feet" runat="server">
                                                                    <table width="850px">
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
                                                                                <asp:Label ID="lbl40feetShipment" runat="server" Text="Label"></asp:Label>
                                                                            </td>
                                                                            <td class="center">
                                                                                <asp:DropDownList ID="ddl40FeetCharges" runat="server" Width="200px" AppendDataBoundItems="True">
                                                                                    <asp:ListItem>~Select~</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:DropDownList ID="ddl40feetUnit" runat="server" Width="100px" AutoPostBack="true"
                                                                                     onchange="javascript:return Disable40()">
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
                                                                                    AutoPostBack="true"></asp:TextBox>
                                                                                    <%--OnTextChanged="txt40FeetFixed_TextChanged"--%>
                                                                            </td>
                                                                            <td class="left">
                                                                                <asp:TextBox ID="txt40FeetMinimum" runat="server" Width="80px" OnTextChanged="txt40FeetMinimum_TextChanged"
                                                                                    AutoPostBack="true"></asp:TextBox>
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
                                                                            <div id="Div3" style="overflow:scroll; height:220px;">
                                                                                <asp:GridView ID="Gv40Feet" runat="server" PageSize="5" CellPadding="4" Font-Names="Arial"
                                                                                    Font-Size="10pt" ForeColor="#333333" Style="text-align: center; font-size: small;"
                                                                                    CssClass="grid-style" Width="817px" AutoGenerateColumns="False">
                                                                                    <FooterStyle BackColor="#507CD1" BorderColor="#fff" BorderStyle="Solid" BorderWidth="1px"
                                                                                        Font-Bold="True" ForeColor="White" Width="350px" />
                                                                                    <RowStyle BackColor="#EFF3FB" />
                                                                                    <RowStyle BackColor="#c9cbcc" ForeColor="White" Font-Size="Small" />
                                                                                    <SelectedRowStyle BackColor="#fffff2" ForeColor="White" />
                                                                                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                                                    <HeaderStyle BackColor="#3d99ea" ForeColor="White" />
                                                                                    <AlternatingRowStyle BackColor="#a8d6ff" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Id" HeaderStyle-CssClass="displaynone" ItemStyle-CssClass="displaynone">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblId" Font-Names="verdana" Text='<%#Bind("ID")%>' CssClass="displaynone"
                                                                                                    Font-Size="7pt" ForeColor="Black" Height="20px" Width="70px" runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Description">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtDescrip" Font-Names="verdana" Text='<%#Bind("Description")%>'
                                                                                                    Font-Size="7pt" Height="20px" Width="170px" runat="server"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Unit">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtUni" runat="server" Text='<%#Bind("Unit")%>' AppendDataBoundItems="True"
                                                                                                    Height="16px" Width="80px">                        
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
                                                                                                    Height="20px" Width="70px" runat="server"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="MinimumRate">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtMinRate" Font-Names="verdana" Text='<%#Bind("MinRate")%>' Font-Size="7pt"
                                                                                                    Height="20px" Width="70px" runat="server"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="MaximumRate">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtMaxRate" Font-Names="verdana" Text='<%#Bind("MaxRate")%>' Font-Size="7pt"
                                                                                                    Height="20px" Width="70px" runat="server"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="VariableRate">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtVarRate" Font-Names="verdana" Text='<%#Bind("VarRate")%>' Font-Size="7pt"
                                                                                                    Height="20px" Width="70px" runat="server"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="VariableType">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtVartype" Font-Names="verdana" Text='<%#Bind("VarType")%>' Font-Size="7pt"
                                                                                                    Height="20px" Width="70px" runat="server"></asp:TextBox>
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
                                                                                        <asp:TemplateField HeaderText="Charges">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkCharge40feet" runat="server" />
                                                                                            </ItemTemplate>
                                                                                            <EditItemTemplate>
                                                                                                <asp:CheckBox ID="chkCharge40feet" runat="server" />
                                                                                            </EditItemTemplate>
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
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
            <td>
                <asp:Panel ID="pnlRepTem" runat="server">
                <table width="850px">
    <tr>
    <td colspan="2" align="center">
    Report Templates 
    </td>
    </tr>
    
    <tr>
    <td>
        <asp:CheckBox ID="chk1" runat="server" />
        </td>
        <td>
        <asp:TextBox ID="txt1" runat="server" Width="1000px"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td>
        <asp:CheckBox ID="chk2" runat="server" />
         </td>
        <td>
        <asp:TextBox ID="txt2" runat="server" Width="1000px"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td>
    <asp:CheckBox ID="chk3" runat="server" />
     </td>
        <td>
        <asp:TextBox ID="txt3" runat="server" Width="1000px"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td>
    <asp:CheckBox ID="chk4" runat="server" />
     </td>
        <td>
        <asp:TextBox ID="txt4" runat="server" Width="1000px"></asp:TextBox>
    </td>
    </tr>
     <tr>
    <td>
    <asp:CheckBox ID="chk5" runat="server" />
     </td>
        <td>
        <asp:TextBox ID="txt5" runat="server" Width="1000px"></asp:TextBox>
    </td>
    </tr>
     <tr>
    <td>
    <asp:CheckBox ID="chk6" runat="server" />
     </td>
        <td>
        <asp:TextBox ID="txt6" runat="server" Width="1000px"></asp:TextBox>
    </td>
    </tr>
     <tr>
    <td>
    <asp:CheckBox ID="chk7" runat="server" />
     </td>
        <td>
        <asp:TextBox ID="txt7" runat="server" Width="1000px"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="lblRemarks" runat="server" Text="Remarks"></asp:Label>
         </td>
        <td>
        <asp:TextBox ID="txtRem" runat="server" TextMode="MultiLine" Width="1000px" 
                ontextchanged="txtRem_TextChanged"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td colspan="2" align="center">
        &nbsp;</td>
    </tr>
    </table>
                </asp:Panel>
            </td>
            </tr>
            <tr>
                <td colspan="8">
                    <asp:Panel ID="pnlButtons" runat="server">
                        <table>
                            <tr>
                                <td>
                                </td>
                                <td align="center" colspan="4">
                                    <asp:Button ID="btnSave" runat="server" CssClass="ButtonV" 
                                        Text="Generate Quote" OnClick="btnSave_Click"
                                        Width="120px" OnClientClick="return confirm('Do you want to Generate Quote?')" />
                                    <asp:Button ID="btnPrint" runat="server" CssClass="ButtonV" Text="Print" OnClick="btnPrint_Click"
                                        Width="62px" OnClientClick="return confirm('Do you want to Print?')"  />
                                    <asp:Button ID="btnUpdate" runat="server" CssClass="ButtonV" 
                                        Text="Update Quote" OnClick="btnUpdate_Click"
                                        Visible="False" Width="100px" OnClientClick="return confirm('Do you want to Update?')"  />
                                    <asp:Button ID="Button2" runat="server" CssClass="ButtonV" Text="Cancel" OnClick="Button2_Click" OnClientClick="return confirm('Do you want to cancel?')" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <asp:Panel ID="pnlGridQuote" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <div style="overflow: scroll; width: 990px; height: 180px;">
                                        <asp:GridView ID="GvQuote" runat="server" AllowPaging="True" CellPadding="4" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="#333333" OnSelectedIndexChanged="GvQuote_SelectedIndexChanged"
                                            Style="text-align: center; font-size: small;" CssClass="grid-style" Width="954px"
                                            AutoGenerateSelectButton="True" 
                                            OnPageIndexChanging="GvQuote_PageIndexChanging" 
                                            onrowdatabound="GvQuote_RowDataBound">
                                            <RowStyle BackColor="#c9cbcc" ForeColor="White" Font-Size="Small" />
                                            <%--<SelectedRowStyle BackColor="#fffff2"  ForeColor="White" />--%>
                                            <AlternatingRowStyle BackColor="#a8d6ff" />
                                            <FooterStyle BackColor="#507CD1" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                                Font-Bold="True" ForeColor="White" Width="350px" />
                                            <HeaderStyle BackColor="#3d99ea" ForeColor="White" />
                                            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                            <RowStyle BackColor="#EFF3FB" ForeColor="Black" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                            <%-- <tr>
            <td colspan="8">
                <asp:Panel ID="pnlQuotedetails" runat="server">
                <table>
                <tr>
                <td>
                 <div style="overflow: scroll; width: 870px; height: 180px;">                   

                        <asp:GridView ID="GvQuoteDet" runat="server" AllowPaging="True" 
                            CellPadding="4" Font-Names="Arial" Font-Size="10pt" ForeColor="#333333"                          
                             
                            Style="text-align: center; font-size: small;" CssClass="grid-style" 
                            Width="817px" AutoGenerateSelectButton="True">
                            <FooterStyle BackColor="#507CD1" BorderColor="Black" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Bold="True" ForeColor="White" Width="350px" />
                            <RowStyle BackColor="#EFF3FB" />
                            <RowStyle BackColor="#7C7C7C" ForeColor="White" Font-Size="Small" />
                    <SelectedRowStyle BackColor="#fffff2"  ForeColor="White" />
                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right"/>
                    <HeaderStyle BackColor="#8bc455" ForeColor="White" />
                    <AlternatingRowStyle BackColor="#939393" />
                         
                        </asp:GridView>
                        </div>
                </td>
                </tr>
                </table>
                </asp:Panel>
            </td>
            </tr>--%>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

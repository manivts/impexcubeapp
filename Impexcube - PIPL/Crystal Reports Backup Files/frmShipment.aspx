﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmShipment.aspx.cs" Inherits="ImpexCube.frmShipment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="Content/Scripts/script.js"></script>
    <style type="text/css">
        .style2
        {
            font-size: 8pt;
            font-family: Arial;
        }
        .style3
        {
            font-size: 8pt;
            font-family: Arial;
        }
        .style4
        {
        }
        .style6
        {
            font-family: Arial;
            font-size: small;
        }
        .style8
        {
            height: 30px;
        }
        .style9
        {
            font-family: Arial;
            font-size: small;
            height: 20px;
        }
        .style10
        {
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td colspan="4">
                            <asp:Panel ID="Panel3" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="style3">
                                            <asp:Label ID="Label12" runat="server" Text="Country of Origin"></asp:Label>
                                        </td>
                                        <td class="style8">
                                            <asp:DropDownList ID="txtCountryOfOrigin" runat="server" CssClass="ddl200">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style3">
                                            <asp:Label ID="Label13" runat="server" Text="Country of Shipment"></asp:Label>
                                        </td>
                                        <td class="style8">
                                            <asp:DropDownList ID="txtCountryOfShipment" runat="server" AutoPostBack="True" 
                                                CssClass="ddl200" 
                                                OnSelectedIndexChanged="txtCountryOfShipment_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style3">
                                            <asp:Label ID="Label11" runat="server" Text="Port of Shipment"></asp:Label>
                                        </td>
                                        <td class="style8">
                                            <asp:DropDownList ID="txtPortofShipment" runat="server" CssClass="ddl200">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style3">
                                            <asp:Label ID="lblVessel" runat="server" Text="Vessel Name"></asp:Label>
                                        </td>
                                        <td class="style4">
                                            <asp:TextBox ID="txtVesselName" runat="server" CssClass="textbox200"></asp:TextBox>
                                        </td>
                                        <td class="style3">
                                            <asp:Label ID="lblVesselNo" runat="server" Text="Voyage Number"></asp:Label>
                                        </td>
                                        <td class="style4">
                                            <asp:TextBox ID="txtVoyageNo" runat="server" CssClass="textbox200"></asp:TextBox>
                                        </td>
                                        <td class="style3">
                                            <asp:Label ID="lblVessel5" runat="server" Text="ETA"></asp:Label>
                                        </td>
                                        <td class="style4">
                                            <asp:TextBox ID="txtETA" runat="server" CssClass="textbox200" onkeypress="Javascript:return txtboxdisable();"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtETA_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtETA">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style3">
                                            <asp:Label ID="lblCFSName" runat="server" Text="CFS Name"></asp:Label>
                                        </td>
                                        <td class="style4">
                                            <asp:DropDownList ID="ddlCFSName" runat="server" AppendDataBoundItems="True" CssClass="ddl200">
                                                <asp:ListItem>~Select~</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style3">
                                            <asp:Label ID="lblShipLine" runat="server" Text="Shipping Line"></asp:Label>
                                        </td>
                                        <td class="style4">
                                            <asp:DropDownList ID="ddlShippingLine" runat="server" AppendDataBoundItems="True"
                                                CssClass="ddl200">
                                                <asp:ListItem>~Select~</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style3">
                                            <asp:Label ID="lblAgentName" runat="server" Text="Agent Name"></asp:Label>
                                        </td>
                                        <td class="style4">
                                            <asp:DropDownList ID="ddlAgentName" runat="server" AppendDataBoundItems="True" CssClass="ddl200">
                                                <asp:ListItem>~Select~</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style3">
                                            <asp:Label ID="lblShipLine4" runat="server" Text="FF Name"></asp:Label>
                                        </td>
                                        <td class="style4">
                                            <asp:DropDownList ID="ddlFFName" runat="server" AppendDataBoundItems="True" CssClass="textbox200">
                                                <asp:ListItem>~Select~</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style3">
                                            <asp:Label ID="lblInwardDate" runat="server" Text="GLD/Inward Date"></asp:Label>
                                        </td>
                                        <td class="style4">
                                            <asp:TextBox ID="txtInwardDate" runat="server" CssClass="textbox100"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtInwardDate">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td class="style3">
                                            <asp:Label ID="lblShipLine0" runat="server" Text="Packages"></asp:Label>
                                        </td>
                                        <td class="style4">
                                            <asp:TextBox ID="txtPackages" runat="server" CssClass="textbox75"></asp:TextBox>
                                            <asp:DropDownList ID="ddlPackages" runat="server" AppendDataBoundItems="True" CssClass="textbox100">
                                                <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style3">
                                            <asp:Label ID="lblVessel6" runat="server" Text="GIGM No"></asp:Label>
                                        </td>
                                        <td class="style4">
                                            <asp:TextBox ID="txtGatewayIGMNo" runat="server" CssClass="textbox100"></asp:TextBox>
                                        </td>
                                        <td class="style3">
                                            <asp:Label ID="lblVessel7" runat="server" Text="GIGM Date"></asp:Label>
                                        </td>
                                        <td class="style4">
                                            <asp:TextBox ID="txtGatewayIGMDate" runat="server" CssClass="textbox100"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" TargetControlID="txtGatewayIGMDate">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td class="style3">
                                            <asp:Label ID="lblShipLine1" runat="server" Text="Gross Weight"></asp:Label>
                                        </td>
                                        <td class="style4">
                                            <asp:TextBox ID="txtGrossWeight" runat="server" CssClass="textbox75"></asp:TextBox>
                                            <asp:DropDownList ID="ddlGrossWeight" runat="server" AppendDataBoundItems="True"
                                                CssClass="textbox100">
                                                <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style3">
                                            <asp:Label ID="lblVessel1" runat="server" Text="Local IGM No"></asp:Label>
                                        </td>
                                        <td class="style4">
                                            <asp:TextBox ID="txtLocalIGMNo" runat="server" CssClass="textbox100"></asp:TextBox>
                                        </td>
                                        <td class="style3">
                                            <asp:Label ID="lblVessel2" runat="server" Text="Local IGM Date"></asp:Label>
                                        </td>
                                        <td class="style4">
                                            <asp:TextBox ID="txtLocalIGMDate" runat="server" CssClass="textbox100"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtLocalIGMDate">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td class="style3">
                                            <asp:Label ID="lblShipLine2" runat="server" Text="Net Weight"></asp:Label>
                                        </td>
                                        <td class="style4">
                                            <asp:TextBox ID="txtNetWeight" runat="server" CssClass="textbox75"></asp:TextBox>
                                            <asp:DropDownList ID="ddlNetWeight" runat="server" AppendDataBoundItems="True" CssClass="textbox100">
                                                <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style3">
                                            <asp:Label ID="lblMBL" runat="server" Text="Master BL No"></asp:Label>
                                        </td>
                                        <td class="style4">
                                            <asp:TextBox ID="txtMABLNo" runat="server" CssClass="textbox100"></asp:TextBox>
                                        </td>
                                        <td class="style3">
                                            <asp:Label ID="lblMBLD" runat="server" Text="Master BL Date"></asp:Label>
                                        </td>
                                        <td class="style4">
                                            <asp:TextBox ID="txtMABLDate" runat="server" CssClass="textbox100"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtMABLDate">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td class="style3" rowspan="2">
                                            <asp:Label ID="lblShipLine3" runat="server" Text="Marks &amp; Nos"></asp:Label>
                                        </td>
                                        <td class="style4" rowspan="2">
                                            <asp:TextBox ID="txtMarksNos" runat="server" CssClass="textbox150" Height="50px"
                                                TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style3">
                                            <asp:Label ID="lblMBL0" runat="server" Text="House BL No"></asp:Label>
                                        </td>
                                        <td class="style4">
                                            <asp:TextBox ID="txtHABLNo" runat="server" CssClass="textbox100"></asp:TextBox>
                                        </td>
                                        <td class="style3">
                                            <asp:Label ID="lblMBL1" runat="server" Text="House BL Date"></asp:Label>
                                        </td>
                                        <td class="style4">
                                            <asp:TextBox ID="txtHABLDate" runat="server" CssClass="textbox100"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtHABLDate">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style3">
                                            <asp:Label ID="lblMBL2" runat="server" Text="No of 20&quot;"></asp:Label>
                                        </td>
                                        <td class="style4">
                                            <asp:TextBox ID="txt20FtContainer" runat="server" CssClass="textbox100" OnKeyPress="javascript:return isFloat(event);"></asp:TextBox>
                                        </td>
                                        <td class="style3">
                                            <asp:Label ID="lblMBL3" runat="server" Text="No of 40&quot;"></asp:Label>
                                        </td>
                                        <td class="style4">
                                            <asp:TextBox ID="txt40ftContainer" runat="server" CssClass="textbox100" OnKeyPress="javascript:return isFloat(event);"></asp:TextBox>
                                        </td>
                                        <td class="style3">
                                            &nbsp;
                                        </td>
                                        <td class="style4">
                                            <asp:Button ID="btnSave" runat="server" CssClass="stylebutton" OnClick="btnSave_Click"
                                                Text="Save" Width="100px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style3" colspan="6">
                                            <table class="displaynon">
                                                <tr>
                                                    <td class="style3">
                                                        Transit Vessel
                                                    </td>
                                                    <td class="style4">
                                                        <asp:TextBox ID="txtTransit" runat="server" CssClass="textbox100"></asp:TextBox>
                                                        Line No
                                                        <asp:TextBox ID="txtLineNo" runat="server" CssClass="textbox100"></asp:TextBox>
                                                    </td>
                                                    <td class="style3">
                                                        (20ft)
                                                    </td>
                                                    <td class="style4">
                                                        &nbsp;
                                                    </td>
                                                    <td class="style3">
                                                        &nbsp;
                                                    </td>
                                                    <td class="style4">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style2">
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td class="style2">
                                                        No Of Container(40ft)
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td class="style2">
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style2">
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td class="style2">
                                                        Port Of Reporting
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtPort" runat="server" CssClass="textbox150"></asp:TextBox>
                                                    </td>
                                                    <td class="style2">
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style2">
                                                        Said To Contain
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSTC" runat="server" CssClass="textbox150"></asp:TextBox>
                                                    </td>
                                                    <td class="style2">
                                                        Unit/Type
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlSTC" runat="server" AppendDataBoundItems="True" CssClass="textbox100">
                                                            <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style2">
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style2">
                                                        Said To Contain(2)
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSTC2" runat="server" CssClass="textbox100"></asp:TextBox>
                                                    </td>
                                                    <td class="style2">
                                                        Unit/Type
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlSTC2" runat="server" AppendDataBoundItems="True" CssClass="ddl100">
                                                            <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style2">
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:Panel ID="Panel1" runat="server" Width="1020px">
                                                <div class="divAccordion" style="overflow: auto; width: 1020px">
                                                    <asp:GridView ID="gvShipmentDetails" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True"
                                                        CellPadding="4" Font-Size="8pt" ForeColor="#333333" OnSelectedIndexChanged="gvShipmentDetails_SelectedIndexChanged"
                                                        Width="1000px">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Content/Images/delete.gif"
                                                                        OnClientClick="return confirm('Do U Want Delete?');" OnClick="btnDelete_Click"
                                                                        Width="20px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="TransId" HeaderText="Id" />
                                                            <asp:BoundField DataField="VesselName" HeaderText="Vessel Name" />
                                                            <asp:BoundField DataField="ShippingLine" HeaderText="Shipping Line" />
                                                            <asp:BoundField DataField="LocalIGMNo" HeaderText="LocalIGM No" />
                                                            <asp:BoundField DataField="GatewayIGMNo" HeaderText="GatewayIGM No" />
                                                            <asp:BoundField DataField="MasterBLNo" HeaderText="MasterBL No" />
                                                            <asp:BoundField DataField="HouseBLNo" HeaderText="HouseBLNo" />
                                                            <asp:BoundField DataField="NoOfPackages" HeaderText="Packages" />
                                                            <asp:BoundField DataField="GrossWeight" HeaderText="GrossWt" />
                                                            <asp:BoundField HeaderText="NetWt" />
                                                            <asp:BoundField DataField="GrossWeightUnit" HeaderText="GrossWtUnit" />
                                                        </Columns>
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                    </asp:GridView>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="3">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Button ID="btnImport" runat="server" CssClass="stylebutton" OnClick="btnImport_Click"
                                                Text="Back to JobCreation" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnInvoice" runat="server" CssClass="stylebutton" OnClick="btnInvoice_Click"
                                                Text="Go to Invoice" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Panel ID="Panel2" runat="server" GroupingText="Container Details" CssClass="stylebutton"
                                Width="1000px">
                                <table style="width: 900px">
                                    <tr>
                                        <td class="style9">
                                            Container Type*
                                        </td>
                                        <td class="style10">
                                            Container No*
                                        </td>
                                        <td class="style10">
                                            Seal No*
                                        </td>
                                        <td class="style9">
                                            Load Type*
                                        </td>
                                        <td class="style9">
                                            Add
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlContainerType" runat="server" 
                                                CssClass="span3v required" AppendDataBoundItems="true">
                                                <asp:ListItem Text="~select~" Value="~select~"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td >
                                            <asp:TextBox ID="txtContainerNo" runat="server" CssClass="postmsgg236 required"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSealNo" runat="server" CssClass="postmsgg236 required"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlLoadType" runat="server" CssClass="span3v required">
                                                <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                <asp:ListItem>LCL</asp:ListItem>
                                                <asp:ListItem>FCL</asp:ListItem>
                                                <asp:ListItem>Break Bulk</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Content/Images/Add.jpg" OnClick="btnAdd_Click"
                                                Style="height: 24px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <asp:GridView ID="gvContainerInfo" runat="server" AutoGenerateColumns="False" CssClass="table-wrapper"
                                                GridLines="Vertical" OnSelectedIndexChanged="gvContainerInfo_SelectedIndexChanged"
                                                Style="text-align: center; margin-left: 10px;" Width="822px">
                                                <RowStyle CssClass="table-header light" />
                                                <HeaderStyle BackColor="#E7E7FF" CssClass="table-row" ForeColor="#EE2521" />
                                                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                <AlternatingRowStyle BackColor="#E7E7FF" />
                                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                <Columns>
                                                    <asp:CommandField HeaderText="Select" ShowSelectButton="True" />
                                                     <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnContainerDelete" runat="server" ImageUrl="~/Content/Images/delete.gif"
                                                                        OnClientClick="return confirm('Do U Want Delete?');" OnClick="btnContainerDelete_Click"
                                                                        Width="20px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                    <asp:BoundField DataField="TransId" HeaderStyle-CssClass="hiddencol" HeaderText="Id"
                                                        ItemStyle-CssClass="hiddencol" ItemStyle-HorizontalAlign="Center" 
                                                        SortExpression="TransId" >
                                                    <HeaderStyle CssClass="hiddencol" />
                                                    <ItemStyle CssClass="hiddencol" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ContainerType" HeaderText="Container Type" ItemStyle-HorizontalAlign="Center"
                                                        ReadOnly="True" HtmlEncodeFormatString="false" >
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ContainerNo" HeaderText="Container No" ItemStyle-HorizontalAlign="Center"
                                                        ReadOnly="True" HtmlEncodeFormatString="false" >
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SealNo" HeaderText="Seal No" ItemStyle-HorizontalAlign="Center"
                                                        ReadOnly="True" HtmlEncodeFormatString="false" >
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="LoadType" HeaderText="Load Type" ItemStyle-HorizontalAlign="Center"
                                                        ReadOnly="True" HtmlEncodeFormatString="false" >
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
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
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table width="250" style="height: 300px;">
                    <tr>
                        <td colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="center" colspan="2">
                            <asp:Label ID="Label30" runat="server" Text="History"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label31" runat="server" CssClass="fontsizehistory" Text="Job No"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlJobNo" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                CssClass="ddl150" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged">
                                <asp:ListItem Selected="True">~Select~</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label32" runat="server" CssClass="fontsizehistory" Text="Job Date"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblJobDate" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label33" runat="server" CssClass="fontsizehistory" Text="Mode"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblMode" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label34" runat="server" CssClass="fontsizehistory" Text="Custom"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCustom" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label35" runat="server" CssClass="fontsizehistory" Text="BE Type"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblBeType" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label36" runat="server" CssClass="fontsizehistory" Text="Doc Filling"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblStatus" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label37" runat="server" CssClass="fontsizehistory" Text="BE No"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblBeNo" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label38" runat="server" CssClass="fontsizehistory" Text="BE Date"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblBeDate" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        var slider1 = new accordions.slider("slider1");
        slider1.init("slider");
    </script>
    <script type="text/javascript">
        function accopen() {
            var slider1 = new accordion.slider("slider1");
            slider1.init("slider1", 1, "open");
        }
    </script>
</asp:Content>
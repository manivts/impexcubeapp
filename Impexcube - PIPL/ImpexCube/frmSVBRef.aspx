<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmSVBRef.aspx.cs" Inherits="ImpexCube.frmSVBRef" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 100%">
        <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table style="width: 53%">
            <tr>
                <td style="text-align: center; font-weight: 700" colspan="4">
                    Relation and SVB Details
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="lblConsignor" runat="server" CssClass="fontsize" Text="Consignor Name"></asp:Label>
                </td>
                <td class="style1">
                    <asp:TextBox ID="txtConsignorName" runat="server" CssClass="textbox200" 
                        AutoPostBack="True" ontextchanged="txtConsignorName_TextChanged"></asp:TextBox>
                     <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                        EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetConsignor"
                        ServicePath="~/AutoComplete.asmx" TargetControlID="txtConsignorName">
                    </cc1:AutoCompleteExtender>
                </td>
                <td class="style1">
                    <asp:Label ID="lblConsignee" runat="server" CssClass="fontsize" Text="Consignee Name"></asp:Label>
                </td>
                <td class="style1">
                    <asp:TextBox ID="txtConsigneeName" runat="server" CssClass="textbox200" 
                        AutoPostBack="True" ontextchanged="txtConsigneeName_TextChanged"></asp:TextBox>
                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionListCssClass="completionList"
                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                        EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetConsignee"
                        ServicePath="~/AutoComplete.asmx" TargetControlID="txtConsigneeName">
                    </cc1:AutoCompleteExtender>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="lblConsignorAddress" runat="server" CssClass="fontsize" Text="Consignor Address"></asp:Label>
                </td>
                <td class="style1">
                    <asp:TextBox ID="txtConsignorAddress" runat="server" CssClass="textboxMulti200" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td class="style1">
                    <asp:Label ID="lblConsigneeAddress" runat="server" CssClass="fontsize" Text="Consignee Address"></asp:Label>
                </td>
                <td class="style1">
                    <asp:TextBox ID="txtConsigneeAddress" runat="server" CssClass="textboxMulti200" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="lblConsignorCountry" runat="server" CssClass="fontsize" Text="Consignor Country"></asp:Label>
                </td>
                <td class="style1">
                    <asp:DropDownList ID="ddlConsignorCountry" runat="server" AppendDataBoundItems="True"
                        CssClass="textbox200">
                        <asp:ListItem Selected="True">~Select~</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style1">
                    <asp:Label ID="lblConsignorCountry0" runat="server" CssClass="fontsize" Text="Consignee Country"></asp:Label>
                </td>
                <td class="style1">
                    <asp:DropDownList ID="ddlConsigneeCountry" runat="server" AppendDataBoundItems="True"
                        CssClass="textbox200">
                        <asp:ListItem Selected="True">~Select~</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="4">
                    <asp:CheckBox ID="chkBuyer" runat="server" AutoPostBack="True" CssClass="fontsize"
                        OnCheckedChanged="chkBuyer_CheckedChanged" Text="Are Buyer and Seller Related ?" />
                    <asp:Panel ID="pnlBuyer" runat="server" BorderColor="Black" Width="300px">
                        <table>
                            <tr>
                                <td class="style11">
                                    <asp:Label ID="lblRelation" runat="server" CssClass="fontsize" Text="Relation"></asp:Label>
                                </td>
                                <td class="tdcolumn150">
                                    <asp:TextBox ID="txtRelation" runat="server" CssClass="textbox150" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style11">
                                    <asp:Label ID="lblBase" runat="server" CssClass="fontsize" Text="Base"></asp:Label>
                                </td>
                                <td class="tdcolumn150">
                                    <asp:TextBox ID="txtRelationBase" runat="server" CssClass="textbox150" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style11">
                                    <asp:Label ID="lblCondition" runat="server" CssClass="fontsize" Text="Condition"></asp:Label>
                                </td>
                                <td class="tdcolumn150">
                                    <asp:TextBox ID="txtRelationCondition" runat="server" CssClass="textbox150" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="4">
                    <asp:CheckBox ID="chkSVB" runat="server" AutoPostBack="true" CssClass="fontsize"
                        OnCheckedChanged="chkSVB_CheckedChanged" Text="SVB Loading ?" />
                    <asp:Panel ID="pnlSVB" runat="server" BorderColor="Black" Width="100%">
                        <table>
                            <tr>
                                <td class="tdcolumn200">
                                    <asp:Label ID="lblSVBRelation" runat="server" CssClass="fontsize" Text="SVB Reference No"
                                        Width="150px"></asp:Label>
                                </td>
                                <td class="tdcolumn75">
                                    <asp:TextBox ID="txtSVBRelation" runat="server" CssClass="textbox100" Enabled="False"></asp:TextBox>
                                </td>
                                <td class="tdcolumn75">
                                    <asp:TextBox ID="txtSVBDate" runat="server" CssClass="textbox75" Enabled="False"></asp:TextBox>
                                    <%-- <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtSVBDate">
                                    </cc1:CalendarExtender>--%>
                                </td>
                                <td>
                                    <asp:Label ID="lblCustomHouse" runat="server" CssClass="fontsize" Text="Custom House"
                                        Width="100px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCustomHouse" runat="server" CssClass="textbox75" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdcolumn200">
                                    <asp:Label ID="lblLoadingOn" runat="server" CssClass="fontsize" Text="Loading On"></asp:Label>
                                </td>
                                <td class="tdcolumn100">
                                    <asp:DropDownList ID="ddlLoadingOn" runat="server" AppendDataBoundItems="True" CssClass="ddl100"
                                        Enabled="False">
                                        <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                        <asp:ListItem>Assessable</asp:ListItem>
                                        <asp:ListItem>Duty</asp:ListItem>
                                        <asp:ListItem>Assessable &amp; Duty</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdcolumn200">
                                    <asp:Label ID="lblLoadingRate" runat="server" CssClass="fontsize" Text="Loading Rate(Assbl.)"></asp:Label>
                                </td>
                                <td class="tdcolumn75">
                                    <asp:TextBox ID="txtLoadingRateAssbl" runat="server" CssClass="textbox100" Enabled="False">0</asp:TextBox>
                                </td>
                                <td class="tdcolumn75">
                                    <asp:Label ID="lblLoadingAssblStatus" runat="server" CssClass="fontsize" Text="Status"></asp:Label>
                                </td>
                                <td class="tdcolumn75">
                                    <asp:DropDownList ID="ddlLoadingAssblStatus" runat="server" AppendDataBoundItems="True"
                                        CssClass="ddl100" Enabled="False">
                                        <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                        <asp:ListItem>Provisional</asp:ListItem>
                                        <asp:ListItem>Final</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdcolumn200">
                                    <asp:Label ID="lblLoadingDuty" runat="server" CssClass="fontsize" Text="Loading Rate(Duty)"></asp:Label>
                                </td>
                                <td class="tdcolumn75">
                                    <asp:TextBox ID="txtLoadingDuty" runat="server" CssClass="textbox75" Enabled="False">0</asp:TextBox>
                                </td>
                                <td class="tdcolumn75">
                                    <asp:Label ID="lblLoadingDutyStatus" runat="server" CssClass="fontsize" Text="Status"></asp:Label>
                                </td>
                                <td class="tdcolumn75">
                                    <asp:DropDownList ID="ddlLoadingDutyStatus" runat="server" AppendDataBoundItems="True"
                                        CssClass="ddl100" Enabled="False">
                                        <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                        <asp:ListItem>Provisional</asp:ListItem>
                                        <asp:ListItem>Final</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: right">
                    <asp:Button ID="btnSaveRelation" runat="server" CssClass="stylebutton" OnClick="btnSaveRelation_Click"
                        Text="Save" />
                    <asp:Button ID="btnUpdate" runat="server" CssClass="stylebutton" OnClick="btnUpdate_Click"
                        Text="Update" Visible="False" />
                    <asp:Button ID="btnCancelRelation" runat="server" CssClass="stylebutton" OnClick="btnCancelRelation_Click"
                        OnClientClick="return confirm ('Do you want to Clear the data?')" Text="Cancel" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <div class="grid_scroll">
                        <asp:GridView ID="gvSVBRef" runat="server" AutoGenerateSelectButton="True" CellPadding="4"
                            ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="gvSVBRef_SelectedIndexChanged"
                            AutoGenerateColumns="False" Font-Size="10px">
                            <AlternatingRowStyle BackColor="White" Font-Size="10px" />
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" Font-Size="10px" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            <Columns>
                                <asp:BoundField HeaderText="ID" DataField="ID" />
                                <asp:BoundField HeaderText="ConsignorName" DataField="ConsignorName" />
                                <asp:BoundField HeaderText="ConsignorAddress" DataField="ConsignorAddress" />
                                <asp:BoundField HeaderText="ConsignorCountry" DataField="ConsignorCountry" />
                                <asp:BoundField HeaderText="ConsigneeName" DataField="ConsigneeName" />
                                <asp:BoundField HeaderText="ConsigneeAddress" DataField="ConsigneeAddress" />
                                <asp:BoundField HeaderText="ConsigneeCountry" DataField="ConsigneeCountry" />
                                <asp:BoundField HeaderText="BuyerSellerRelated" DataField="BuyerSellerRelated" />
                                <asp:BoundField HeaderText="Relation" DataField="Relation" />
                                <asp:BoundField HeaderText="Base" DataField="Base" />
                                <asp:BoundField HeaderText="Condition" DataField="Condition" />
                                <asp:BoundField HeaderText="SVBLoad" DataField="SVBLoad" />
                                <asp:BoundField HeaderText="SVBRefOn" DataField="SVBRefOn" />
                                <asp:BoundField HeaderText="SVBRefDate" DataField="SVBRefDate" />
                                <asp:BoundField HeaderText="CustomHouse" DataField="CustomHouse" />
                                <asp:BoundField HeaderText="LoadingOn" DataField="LoadingOn" />
                                <asp:BoundField HeaderText="LoadingRateAssb" DataField="LoadingRateAssb" />
                                <asp:BoundField HeaderText="LoadingRateAssbStatus" DataField="LoadingRateAssbStatus" />
                                <asp:BoundField HeaderText="LoadingRateDuty" DataField="LoadingRateDuty" />
                                <asp:BoundField HeaderText="LoadingRateDutyStatus" DataField="LoadingRateDutyStatus" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

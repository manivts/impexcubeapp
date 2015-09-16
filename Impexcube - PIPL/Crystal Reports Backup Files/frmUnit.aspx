﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmUnit.aspx.cs" Inherits="ImpexCube.frmUnit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function valsave() {
        var ddlDesc = document.getElementById("<%= ddlDesc.ClientID %>");
        var selectedText = ddlDesc.options[ddlDesc.selectedIndex].text;
        if (selectedText == "~Select~") {
            alert('Please Select Unit Descripton');
            document.getElementById('<%=ddlDesc.ClientID%>').focus();
            return false;
        }
        return true;
    }
    </script>
    <script src="http://localhost:2875/Content/Scripts/ProductDetails.js" type="text/javascript"></script>
    <style type="text/css">
        .style2
        {
            width: 447px;
        }
        .style5
        {
            width: 193px;
        }
        .style8
        {
            width: 198px;
        }
        .style9
        {
            height: 145px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <table>
                    <tr>
                        <td colspan="2" align="center" style="color: #008080; font-style: italic; font-weight: bold;
                            font-size: large">
                            UNIT Master
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="fontsize" style="width: 100px;">
                            Unit Desc
                        </td>
                        <td class="style2">
                            <asp:DropDownList ID="ddlDesc" runat="server" AutoPostBack="True" CssClass="ddl156">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize" style="width: 100px;">
                            Short Name
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtShortname" runat="server" CssClass="textbox150"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize" style="width: 100px;">
                            Unit Code
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtUnitCode" runat="server" CssClass="textbox150" OnKeyPress="javascript:return isFloat(event);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize" style="width: 100px;">
                            EDI Code
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtEDICode" runat="server" CssClass="textbox150"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize" style="width: 100px;">
                            Type Of Unit
                        </td>
                        <td class="style2">
                            <asp:DropDownList ID="ddlTypeOfUnit" runat="server" AutoPostBack="True" CssClass="ddl156">
                                <asp:ListItem>~Select~</asp:ListItem>
                                <asp:ListItem>Numbers</asp:ListItem>
                                <asp:ListItem>Weight</asp:ListItem>
                                <asp:ListItem>Distance</asp:ListItem>
                                <asp:ListItem>Volume</asp:ListItem>
                                <asp:ListItem>Power</asp:ListItem>
                                <asp:ListItem>Acceleration</asp:ListItem>
                                <asp:ListItem>Area</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize" style="width: 100px;">
                            Conv Fact
                        </td>
                        <td class="style2">
                            <asp:DropDownList ID="ddlConvFact" runat="server" AutoPostBack="True" CssClass="ddl156">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize" style="width: 100px;">
                            Number(in Unit Pieces)
                        </td>
                        <td class="style2">
                            <asp:DropDownList ID="ddlNoOfPieces" runat="server" AutoPostBack="True" CssClass="ddl156">
                                <asp:ListItem>~Select~</asp:ListItem>
                                <asp:ListItem>NUM</asp:ListItem>
                                <asp:ListItem>GRM</asp:ListItem>
                                <asp:ListItem>CEN</asp:ListItem>
                                <asp:ListItem>CCM</asp:ListItem>
                                <asp:ListItem>SFT</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize" style="width: 100px;">
                            No. Of Decimals
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtNoOfDecimals" runat="server" CssClass="textbox150" OnKeyPress="javascript:return isFloat(event);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize" style="width: 100px;">
                            UNECE Code
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtUnece" runat="server" CssClass="textbox150" OnKeyPress="javascript:return isFloat(event);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="2">
                            <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CssClass="masterbutton" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="masterbutton" OnClientClick="javascript: return valsave();" />
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                                CssClass="masterbutton" />
                            <asp:Button ID="btnDiscard" runat="server" Text="Exit" OnClick="btnDiscard_Click"
                                CssClass="masterbutton" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="grid_scroll-2">
                                <asp:GridView ID="gvUnit" runat="server" CssClass="table-wrapper" AutoGenerateSelectButton="true"
                                    OnSelectedIndexChanged="gvUnit_SelectedIndexChanged" Width="852px" 
                                    OnPageIndexChanging="gvUnit_PageIndexChanging" AutoGenerateColumns="False">
                                    <Columns>
                                    <asp:BoundField DataField="Transid" HeaderText="Transid" InsertVisible="False"
                                            ReadOnly="True" SortExpression="Transid" />
                                        <asp:BoundField DataField="UnitDesc" HeaderText="UnitDesc" InsertVisible="False"
                                            ReadOnly="True" SortExpression="UnitDesc" />
                                        <asp:BoundField DataField="UnitShort" HeaderText="UnitShort" SortExpression="UnitShort" />
                                        <asp:BoundField DataField="UnitCode" HeaderText="UnitCode" SortExpression="UnitCode" />
                                        <asp:BoundField DataField="EDICode" HeaderText="EDICode" SortExpression="EDICode" />
                                        <asp:BoundField DataField="UnitType" HeaderText="UnitType" SortExpression="UnitType" />
                                        <asp:BoundField DataField="UnitConv" HeaderText="UnitConv" SortExpression="UnitConv" />
                                        <asp:BoundField DataField="BaseUnit" HeaderText="BaseUnit" SortExpression="BaseUnit" />
                                        <asp:BoundField DataField="NumDesc" HeaderText="NumDesc" SortExpression="NumDesc" />
                                        <asp:BoundField DataField="UneceCode" HeaderText="UneceCode" SortExpression="UneceCode" />
                                    </Columns>
                                    <RowStyle CssClass="table-header light" />
                                    <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" />
                                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                    <AlternatingRowStyle BackColor="#E7E7FF" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

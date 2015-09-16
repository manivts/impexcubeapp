﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmDeliveryReportParty.aspx.cs" Inherits="ImpexCube.frmDeliveryReportParty" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
        function OpenPopup() {
            window.open("frmImpJobStage.aspx", "List", "scrollbars=no,resizable=no,left=20,top=20,width=700,height=500");
            return false;
        }
    </script>
    <script type="text/javascript">
        function MouseEvents(objRef, evt) {
            var checkbox = objRef.getElementsByTagName("input")[0];
            if (evt.type == "mouseover") {
                objRef.style.backgroundColor = "orange";
            }
            else if (evt.type == "mouseout") {
                if (objRef.rowIndex % 2 == 0) {
                    //Alternating Row Color
                    objRef.style.backgroundColor = "#C2D69B";
                }
                else {
                    objRef.style.backgroundColor = "white";
                }
            }
        }
    </script>
    <table style="background-color: White; width: 100%;">
        <tr>
            <td>
                &nbsp;<cc1:AutoCompleteExtender ID="autoComplete3" runat="server" CompletionListCssClass="completionList"
                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                    EnableCaching="true" MinimumPrefixLength="1" ServiceMethod="GetPartyMaster" ServicePath="~/AutoComplete.asmx"
                    TargetControlID="txtPName">
                </cc1:AutoCompleteExtender>
                &nbsp;&nbsp;
                <table style="width: 85%; height: 54px; border-color: #2461BF; border-style: solid;
                    border-width: 2px;">
                    <tr style="background-color: #2461BF;">
                        <td align="center" style="width: 1080px">
                            <asp:Label ID="Label1" runat="server" Text="Delivery Reports " Width="804px" Font-Names="Verdana"
                                Font-Size="12pt" ForeColor="White" Height="20px" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 1080px; height: 14px;">
                            <table>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label2" runat="server" Text="Select Financial Year :" Font-Names="Arial"
                                            Font-Size="8pt"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="drYEar" runat="server" Font-Names="Arial" Font-Size="8pt" Width="193px">
                                            <asp:ListItem Value="0">select</asp:ListItem>
                                            <%--  <asp:ListItem>2005-2006</asp:ListItem>
                                            <asp:ListItem>2006-2007</asp:ListItem>
                                            <asp:ListItem>2007-2008</asp:ListItem>
                                            <asp:ListItem>2008-2009</asp:ListItem>
                                            <asp:ListItem>2009-2010</asp:ListItem>--%>
                                            <asp:ListItem>2010-2011</asp:ListItem>
                                            <asp:ListItem>2011-2012</asp:ListItem>
                                            <asp:ListItem>2012-2013</asp:ListItem>
                                            <asp:ListItem>2013-2014</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lbldoc" runat="server" Text="Select Party Name :" Font-Names="Arial"
                                            Font-Size="8pt"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPName" runat="server" CausesValidation="True" Font-Names="Arial"
                                            Font-Size="8pt" Width="239px"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="autoComplete2" runat="server" TargetControlID="txtPName"
                                            ServicePath="~/AutoComplete.asmx" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                            CompletionListCssClass="completionList" ServiceMethod="GetPartyMaster" MinimumPrefixLength="1"
                                            EnableCaching="true">
                                        </cc1:AutoCompleteExtender>
                                        <cc1:TextBoxWatermarkExtender ID="TWE1" runat="server" TargetControlID="txtPName"
                                            WatermarkText="Select party name" WatermarkCssClass="watermarked">
                                        </cc1:TextBoxWatermarkExtender>
                                    </td>
                                    <td align="left">
                                        <asp:Button ID="Search" runat="server" Text="Search" Font-Names="Arial" Font-Size="8pt"
                                            OnClick="Search_Click" />
                                        <asp:Button ID="BtnCancel" runat="server" Font-Names="Arial" Font-Size="9pt" OnClick="BtnCancel_Click"
                                            Text="Cancel" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%; height: 54px; border-color: #2461BF; border-style: solid;
                    border-width: 2px;">
                    <tr>
                        <td>
                            <table style="width: 100%; height: 54px; border-color: #2461BF; border-style: solid;
                                border-width: 2px;">
                                <tr>
                                    <td align="left" style="width: 30px; height: 250px; vertical-align: top;">
                                        <asp:Label ID="lbllistHead" runat="server" Text="Data Field" Width="126px" BackColor="#2461BF"
                                            Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White"></asp:Label>
                                        <asp:ListBox ID="lstShowField" runat="server" Height="229px" Width="122px" SelectionMode="Multiple"
                                            EnableTheming="True" Font-Names="Arial" Font-Size="7pt"></asp:ListBox>
                                        &nbsp;
                                    </td>
                                    <td style="width: 24px; height: 250px;">
                                        <asp:Button ID="BtnAdd" runat="server" Text=">" Width="42px" Height="24px" OnClick="BtnAdd_Click" /><br />
                                        <br />
                                        <asp:Button ID="BtnAddAll" runat="server" Text=">>" Width="42px" Height="24px" OnClick="BtnAddAll_Click" /><br />
                                        <br />
                                        <asp:Button ID="BtnRemove" runat="server" Text="<" Width="42px" Height="24px" OnClick="BtnRemove_Click" />
                                        <br />
                                        <br />
                                        <asp:Button ID="BtnRemoveAll" runat="server" Text="<<" Width="42px" Height="24px"
                                            OnClick="BtnRemoveAll_Click" />
                                    </td>
                                    <td align="left" style="width: 86px; height: 250px; vertical-align: top;">
                                        <asp:Label ID="lbllistHead1" runat="server" Text="Custom Field" BackColor="#2461BF"
                                            Font-Bold="True" Width="124px" Font-Names="Verdana" Font-Size="8pt" ForeColor="White"></asp:Label>
                                        <asp:ListBox ID="lstView" runat="server" Height="229px" Width="124px" SelectionMode="Multiple"
                                            Rows="1" Font-Names="Arial" Font-Size="7pt"></asp:ListBox>
                                    </td>
                                    <td align="center" style="height: 250px; width: 76px; vertical-align: middle;">
                                        <asp:Button ID="BtnSearch" runat="server" Text="Search" Height="39px" Width="97px"
                                            Font-Names="Arial" Font-Size="8pt" OnClick="BtnSearch_Click" />&nbsp;
                                        <br />
                                        <br />
                                    </td>
                                    <td style="width: 438px">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GrdAllJob" AllowPaging="True" runat="server" BackColor="White"
                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
                                Font-Names="Arial" Font-Size="8pt" OnPageIndexChanging="GrdAllJob_PageIndexChanging"
                                OnRowDataBound="GrdAllJob_RowDataBound" AutoGenerateColumns="False" DataKeyNames="job_no"
                                Font-Strikeout="False" PageSize="20">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <RowStyle ForeColor="#000066" />
                                <SelectedRowStyle Font-Bold="False" BorderColor="#E0E0E0" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="True" />
                                <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="jobsno" HeaderText="REF NO" SortExpression="jobsno">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="inv_dtl" HeaderText="ITEM" SortExpression="inv_dtl">
                                        <ItemStyle Wrap="True" Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cnsr_name" HeaderText="SUPPLIER" SortExpression="cnsr_name">
                                        <ItemStyle Wrap="True" Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="mawb_no" HeaderText="BL NO" SortExpression="mawb_no">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="mawb_date" HeaderText="BL Date" SortExpression="mawb_date">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="INV_NO" HeaderText="INV. NO." SortExpression="INV_NO">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="INV_DATE" HeaderText="INV DATE" SortExpression="INV_DATE">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_OF_PKG" HeaderText="NO.PKGS" SortExpression="NO_OF_PKG">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CNSR_CNTRY" HeaderText="DELIVERY PORT" SortExpression="CNSR_CNTRY">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="carrier" HeaderText="TRANSPORTER" SortExpression="carrier">
                                        <ItemStyle Wrap="true" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TR_VESSEL" HeaderText="TRUCK NUMBER" SortExpression="TR_VESSEL">
                                        <ItemStyle Wrap="true" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TR_VESSEL" HeaderText="NO.OF CONT." SortExpression="TR_VESSEL">
                                        <ItemStyle Wrap="true" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ROTN_NO" HeaderText="IGM NO" SortExpression="ROTN_NO">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ROTN_DATE" HeaderText="IGM DATE" SortExpression="ROTN_DATE">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <asp:RadioButtonList ID="RBLExp" runat="server" Font-Size="9pt" Font-Names="Arial"
                    Width="170px" Height="25px" RepeatDirection="Horizontal">
                    <asp:ListItem>Current Page</asp:ListItem>
                    <asp:ListItem>All Page</asp:ListItem>
                </asp:RadioButtonList>
                <asp:Button ID="ExportPage" OnClick="ExportPage_Click" runat="server" Text="Export to Excel"
                    Font-Size="8pt" Font-Names="Arial" Width="98px" Height="35px"></asp:Button><center>
                        <asp:Label ID="lbROnly" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="Red"
                            Width="383px"></asp:Label>
                        &nbsp;&nbsp;
                    </center>
                <center>
                    <asp:Label ID="lblerror" runat="server" Width="237px" Font-Names="Arial" Font-Size="8pt"></asp:Label>&nbsp;</center>
            </td>
        </tr>
    </table>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmUserReports.aspx.cs" Inherits="ImpexCube.frmUserReports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="container">
        <div class="container-area">
            <div id="col-2ex">
                <div class="d">
                    <div class="c-aWithouthight">
                        <div class="c-aEx1">
                            <div class="Col-titleext">
                                Reports</div>
                        </div>
                        <div class="c-aEx1">
                            <div class="c-accordion1">
                                <div class="content-work-increase">
                                    <div class="c-s-b1">
                                        From
                                    </div>
                                    <div class="c-s-b2fixeda">
                                        <asp:TextBox ID="txtForm" runat="server" CssClass="postmsgg231 required" OnKeyPress="javascript:return false;"></asp:TextBox>
                                        <cc1:CalendarExtender ID="ceDCOForm" runat="server" Format="dd/MM/yyyy" TargetControlID="txtForm">
                                        </cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="content-work-increase">
                                    <div class="c-s-b1">
                                        To
                                    </div>
                                    <div class="c-s-b2fixeda">
                                        <asp:TextBox ID="txtTo" runat="server" CssClass="postmsgg231 required" OnKeyPress="javascript:return false;"></asp:TextBox>
                                        <cc1:CalendarExtender ID="ceToForm" runat="server" Format="dd/MM/yyyy" TargetControlID="txtTo">
                                        </cc1:CalendarExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="c-accordion1">
                                <div class="content-work-increase">
                                    <div class="c-s-b1">
                                        Job No</div>
                                    <div class="c-s-b2fixeda">
                                        <asp:TextBox ID="txtJNO" runat="server" CssClass="postmsgg23 required"></asp:TextBox>
                                        <%--<cc1:AutoCompleteExtender ID="autoComplete2" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetJobNo" ServicePath="~/AutoComplete.asmx"
                                        TargetControlID="txtJNO">
                                    </cc1:AutoCompleteExtender>--%>
                                    </div>
                                </div>
                                <div class="content-work-increase">
                                    <div class="c-s-b1">
                                        Party Name</div>
                                    <div class="c-s-b2fixeda">
                                        <asp:TextBox ID="txtImporter" runat="server" CssClass="postmsgg23 required"></asp:TextBox>
                                        <%-- <cc1:AutoCompleteExtender ID="autoComplete1" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetCustomer" ServicePath="~/AutoComplete.asmx"
                                        TargetControlID="txtImporter">
                                    </cc1:AutoCompleteExtender>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="c-accordion1">
                                <div class="content-work-increase">
                                    <div class="c-s-b1">
                                        Stage
                                    </div>
                                    <div class="c-s-b2fixeda">
                                        <asp:DropDownList ID="ddlStage" runat="server" CssClass="spanddl required" OnSelectedIndexChanged="ddlStage_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="content-work-increase">
                                    <div class="c-s-b1">
                                        Status
                                    </div>
                                    <div class="c-s-b2fixeda">
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="spanddl required">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="c-accordion1">
                                <div class="content-work-extended">
                                    <div class="c-s-b1">
                                        Shipment Type
                                    </div>
                                    <div class="c-s-b2fixedaEx">
                                        <asp:DropDownList ID="ddlShipmentType" runat="server" CssClass="span3 required" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlShipmentType_SelectedIndexChanged">
                                            <asp:ListItem>-Select-</asp:ListItem>
                                            <asp:ListItem>Air</asp:ListItem>
                                            <asp:ListItem>Sea</asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlShipmentLoad" runat="server" CssClass="span3 required">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="menu-listacc">
                            <div class="cs2-accbutton">
                                <div style="float: right;">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="orange" OnClick="btnSearch_Click">
                                    </asp:Button>
                                    <asp:Button ID="btnExit" runat="server" Text="Cancel" CssClass="orange"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="d" id="GridDetails" runat="server" visible="false">
                    <div class="grid_scroll">
                        <asp:GridView ID="gvReportDetails" runat="server" CellPadding="4" GridLines="None"
                            CssClass="table-wrapper" AutoGenerateColumns="false" Font-Size="10pt" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="Job" HeaderText="Job No" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Importer" HeaderText="Importer" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Shipment" HeaderText="Shipment" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="ShipmentType" HeaderText="Shipment Type" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Stage" HeaderText="Stage" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-HorizontalAlign="Center" />
                                  <asp:BoundField DataField="Date" HeaderText="Status Date" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                            <RowStyle CssClass="table-header light" />
                            <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" />
                            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                            <AlternatingRowStyle BackColor="#E7E7FF" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="col-areabottom" id="btnExport" runat="server" visible="false">
                    <div style="clear: both;">
                    </div>
                    <div class="cs2-accbutton">
                        <div style="float: right;">
                            <asp:Button ID="btnExportExcel" runat="server" Text="Export to excel" CssClass="orange"
                                OnClick="btnExportExcel_Click"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmStageListView.aspx.cs" Inherits="ImpexCube.frmStageListView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="container">
        <div class="container-area">
            <div id="col-2">
                <div class="d">
                    <div class="c-al">
                        <div class="Col-title">
                            Job Status List</div>
                    </div>
                    <div class="c-a-textarea">
                        <div class="texta1">
                            Doc From</div>
                        <div class="texta2">
                            <asp:TextBox ID="txtDCOForm" runat="server" CssClass="postmsgg231 required" OnKeyPress="javascript:return false;"></asp:TextBox>
                            <cc1:CalendarExtender ID="ceDCOForm" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDCOForm">
                            </cc1:CalendarExtender>
                        </div>
                        <div class="textd">
                            <div class="texta12">
                                To</div>
                            <div class="texta2">
                                <asp:TextBox ID="txtTo" runat="server" CssClass="postmsgg231 required" OnKeyPress="javascript:return false;"></asp:TextBox>
                                <cc1:CalendarExtender ID="ceTo" runat="server" Format="dd/MM/yyyy" TargetControlID="txtTo">
                                </cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="textd">
                            <div class="texta1">
                                ImpName</div>
                            <div class="texta21">
                                <asp:TextBox ID="txtImporter" runat="server" CssClass="po required"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="autoComplete1" runat="server" CompletionListCssClass="completionList"
                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                    EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetJobCustomer" ServicePath="~/AutoComplete.asmx"
                                    TargetControlID="txtImporter">
                                </cc1:AutoCompleteExtender>
                            </div>
                        </div>
                        <div class="textd">
                            <div class="texta123">
                                JNO</div>
                            <div class="texta21">
                                <asp:TextBox ID="txtJNO" runat="server" CssClass="postmsgg2315 required"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="autoComplete2" runat="server" CompletionListCssClass="completionList"
                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                    EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetJobNo" ServicePath="~/AutoComplete.asmx"
                                    TargetControlID="txtJNO">
                                </cc1:AutoCompleteExtender>
                            </div>
                        </div>
                        <div class="textd">
                            <div class="texta1">
                                Stage</div>
                            <div class="texta21">
                                <asp:DropDownList ID="ddlJobStages" runat="server" CssClass="span3 required" OnSelectedIndexChanged="ddlJobStages_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="textdd">
                            <div class="texta1">
                                Status</div>
                            <div class="texta21">
                                <asp:DropDownList ID="ddlJobStageStatus" runat="server" CssClass="span3 required">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="menu-list">
                    <div class="col-area">
                        <div class="cs-abutton">
                            <div style="float: right;">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="orange" OnClick="btnSearch_Click">
                                </asp:Button>
                                <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="orange" OnClick="btnExit_Click">
                                </asp:Button>
                            </div>
                        </div>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div  align="center">
                    <div class="d_gridScrol">
                        <asp:GridView ID="gvJobStageStatus" runat="server" Style="text-align: center" GridLines="Vertical"
                            CssClass="table-wrapper" AutoGenerateColumns="False" 
                            OnSelectedIndexChanged="gvJobStageStatus_SelectedIndexChanged" 
                            Font-Names="Verdana" Font-Size="8pt">
                            <RowStyle CssClass="table-header light" Height="20px"/>
                            <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" ForeColor="#EE2521"/>
                            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                            <AlternatingRowStyle BackColor="#E7E7FF" />
                            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                            <Columns>
                                <%--<asp:BoundField DataField="TransId" HeaderText="Sl.No" ItemStyle-CssClass="hiddencol"
                                    HeaderStyle-CssClass="hiddencol" />--%>
                                <asp:CommandField ShowSelectButton="True" HeaderText="Select" />
                                <asp:BoundField DataField="Job" HeaderText="JobNo" />
                                <asp:BoundField DataField="Importer" HeaderText="Importer Name" />
                                <asp:BoundField DataField="Stage" HeaderText="Stage" />
                                <asp:BoundField DataField="Status" HeaderText="Status" />
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                            </Columns>
                            <SelectedRowStyle BackColor="#000066" ForeColor="White" Font-Names="Verdana" />
                        </asp:GridView>
                    </div>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div class="col-area">
                        <article></article>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

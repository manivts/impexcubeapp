<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmJobStageHistory.aspx.cs" Inherits="ImpexCube.frmTestRow" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="AutoComplete.asmx" />
        </Services>
    </asp:ScriptManager>
    <table style="width: 60%;">
        <tr>
            <td colspan="9">
            </td>
        </tr>
        <tr>
            <td colspan="9">
            </td>
        </tr>
        <tr>
            <td class="fontsize">
                From
            </td>
            <td class="fontsize" width="10px">
                <asp:TextBox ID="txtFrom" runat="server" CssClass="postmsgg236 required" OnKeyPress="javascript:return false;"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFrom">
                </cc1:CalendarExtender>
            </td>
            <td class="fontsize" width="80px4">
                To
            </td>
            <td class="fontsize">
                <asp:TextBox ID="txtTo" runat="server" CssClass="postmsgg236 required" OnKeyPress="javascript:return false;"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtTo">
                </cc1:CalendarExtender>
            </td>
            <td class="fontsize">
                JobNo
            </td>
            <td class="fontsize">
                <asp:TextBox ID="txtJobNo" runat="server" CssClass="postmsgg236 required"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="autoComplete2" runat="server" CompletionListCssClass="completionList"
                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                    EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetJobNo" ServicePath="~/AutoComplete.asmx"
                    TargetControlID="txtJobNo">
                </cc1:AutoCompleteExtender>
            </td>
            <td class="fontsize">
                Importer
            </td>
            <td>
                <asp:DropDownList ID="ddlImporter" runat="server" CssClass="span3v required">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="blue" OnClick="btnSearch_Click">
                </asp:Button>
                <asp:Button ID="btnExport" runat="server" Text="Export To Excel" CssClass="blue"
                    OnClick="btnExport_Click" Visible="False"></asp:Button>
            </td>
        </tr>
        <tr>
            <td colspan="9">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="9">
                <asp:Panel ID="pnlGrid" runat="server">
                    <div class="grid_scroll-2">
                        <asp:GridView ID="gvTest1" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvTest1_OnRowDataBound"
                            Width="600" DataKeyNames="JobNo" Height="500px" BorderColor="#3366CC" CellPadding="4"
                            Font-Names="Arial" Font-Size="8pt">
                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                            <RowStyle BackColor="White" ForeColor="#003399" />
                            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#073088" Font-Bold="True" ForeColor="#CCCCFF" Font-Size="8pt" />
                            <Columns>
                                <asp:TemplateField HeaderText="JobNo" Visible="true" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJobNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "JobNo") %>'
                                            ItemStyle-VerticalAlign="Top"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="JobReceivedDate" HeaderText="Job Date" ItemStyle-VerticalAlign="Top" />
                                <asp:BoundField DataField="Mode" HeaderText="Mode" ItemStyle-VerticalAlign="Top" />
                                <asp:BoundField DataField="ETA" HeaderText="ETA" ItemStyle-VerticalAlign="Top" />
                                <asp:BoundField DataField="BEType" HeaderText="BEType" ItemStyle-VerticalAlign="Top" />
                                <asp:BoundField DataField="BENo" HeaderText="BE No" ItemStyle-VerticalAlign="Top" />
                                <asp:BoundField DataField="BEDate" HeaderText="BE Date" ItemStyle-VerticalAlign="Top" />
                                <asp:BoundField DataField="Importer" HeaderText="Importer" ItemStyle-Width="200px"
                                    HeaderStyle-Width="200px" ItemStyle-VerticalAlign="Top" />
                                <asp:BoundField DataField="status_job" HeaderText="Status_Job" ItemStyle-VerticalAlign="Top" />
                                <%--  <asp:BoundField DataField="NoOfPackages" HeaderText="No Of Pkgs" />
                                <asp:BoundField DataField="PackagesUnit" HeaderText="Pkg Unit" />--%>
                                <%--   <asp:BoundField DataField="NetWeight" HeaderText="Net Wght" />
                                <asp:BoundField DataField="NetUint" HeaderText="NetWght Unit" />--%>
                                <%-- <asp:BoundField DataField="GrossWeight" HeaderText="GrossWeight" />
                                <asp:BoundField DataField="GrossWeightUnit" HeaderText="GrossWeightUnit" />
                                <asp:BoundField DataField="MarksNos" HeaderText="Marks & NOs" />--%>
                                <asp:TemplateField ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:GridView ID="gvTest2" runat="server" Width="500px" AutoGenerateColumns="false"
                                            BorderColor="#3366CC" CellPadding="4" Font-Names="Arial" Font-Size="8pt" HeaderStyle-VerticalAlign="Top">
                                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                            <RowStyle BackColor="White" ForeColor="#003399" />
                                            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#073088" Font-Bold="True" ForeColor="#CCCCFF" Font-Size="8pt"
                                                VerticalAlign="Top" />
                                            <Columns>
                                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" ItemStyle-VerticalAlign="Top" />
                                                <asp:BoundField DataField="StatusDate" HeaderText="StatusDate" ItemStyle-VerticalAlign="Top" />
                                            </Columns>
                                        </asp:GridView>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>

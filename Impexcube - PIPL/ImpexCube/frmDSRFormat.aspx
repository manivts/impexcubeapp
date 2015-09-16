<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmDSRFormat.aspx.cs" Inherits="ImpexCube.frmDSRFormat1" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="80%">
        <tr>
            <td align="center" colspan="9">
            </td>
        </tr>
        <tr>
            <td align="right" class="fontsize">
                Job No
            </td>
            <td width="100px" style="width: -200">
                <asp:TextBox ID="txtJobNo" runat="server" CssClass="textbox100"></asp:TextBox>
            </td>
            <td width="100px" class="fontsize">
                Importer
            </td>
            <td>
                <asp:DropDownList ID="ddlImporter" runat="server" Width="200px">
                </asp:DropDownList>
            </td>
            <td class="fontsize">
                Status</td>
            <td class="fontsize">
                <asp:DropDownList ID="ddlJobStatus" runat="server" Width="100px">
                    <asp:ListItem>Pending</asp:ListItem>
                    <asp:ListItem>Completed</asp:ListItem>
                    <asp:ListItem>Both</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td width="100px" style="width: -50">
                <asp:RadioButtonList ID="rbFormat" runat="server" RepeatDirection="Horizontal" Width="271px">
                    <asp:ListItem Value="Format1">Format 1</asp:ListItem>
                    <asp:ListItem Value="Format2">Format 2</asp:ListItem>
                    <asp:ListItem Value="Format3">Format 3</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="fontsize" colspan="2">
                <asp:Button ID="btnSearch" runat="server" Text="Generate" Font-Size="10px" CssClass="stylebutton"
                    OnClick="btnSearch_Click" />
                <asp:Button ID="btnexporttoexc" runat="server" Text="Export To Excel" Font-Size="10px"
                    CssClass="stylebutton" OnClick="btnexporttoexc_Click" />
            </td>
        </tr>
        <tr>
            <td align="right" class="fontsize">
                &nbsp;
            </td>
            <td width="100px" style="width: -200">
                &nbsp;
            </td>
            <td width="100px" class="fontsize">
                &nbsp;
            </td>
            <td width="100px" style="width: 25px" colspan="4">
                &nbsp;
            </td>
            <td class="fontsize" colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="right" class="fontsize">
                &nbsp;
            </td>
            <td class="fontsize" colspan="6">
                &nbsp;
            </td>
            <td class="fontsize" width="100px">
                &nbsp;
            </td>
            <td class="fontsize">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="9">
            </td>
        </tr>
        <tr>
            <td colspan="9">
                <div class="grid_scroll-2">
                    <asp:Panel ID="pnlGrid" runat="server">
                        <div class="grid_scroll-2" style="width: 1020px;">
                            <asp:GridView ID="gvDSR" runat="server" CssClass="gridview" AutoGenerateSelectButton="false"
                                AutoGenerateColumns="False" Width="800px">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="JobNo" Visible="true" ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "JobNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="JobDate" DataField="JobReceivedDate" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:BoundField HeaderText="PARTY NAME" DataField="Importer" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top" ItemStyle-Width="150px">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="SUPPLIER" DataField="Consignor" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top" ItemStyle-Width="150px">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="COUNTRY" DataField="ConsignorCountry" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top" ItemStyle-Width="150px">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="S/LINE" DataField="FFName" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:BoundField HeaderText="Original Doc Rcd." DataField="FFName" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:BoundField HeaderText="BLNO/AWB NO Date" DataField="MasterBLNo" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:BoundField HeaderText="DESCRIPTION" DataField="BEHeading" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:BoundField HeaderText="PKGS" DataField="NoOfPackages" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:BoundField HeaderText="GR.WT" DataField="GrossWeight" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:BoundField HeaderText="BE NO" DataField="BENoDate" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:BoundField HeaderText="ETA" DataField="ETA" HeaderStyle-Font-Size="12px" FooterStyle-Font-Size="8px"
                                        ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:BoundField HeaderText="PENDING REMARKS" DataField="Remarks" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                </Columns>
                                <RowStyle CssClass="table-header light" />
                                <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" />
                                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                <AlternatingRowStyle BackColor="#E7E7FF" />
                            </asp:GridView>
                            <asp:GridView ID="gvSEW" runat="server" CssClass="gridview" AutoGenerateSelectButton="false"
                                AutoGenerateColumns="False" Width="800px" OnRowDataBound="gvSEW_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="JobNo" Visible="true" ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "JobNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Mode Of Shipment" DataField="Mode" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:BoundField HeaderText="BLNO. /AWB NO." DataField="MasterBLNo" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:BoundField HeaderText="SUPPLIER" DataField="Importer" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top" ItemStyle-Width="150px">
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Invoice Details" ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>
                                            <asp:GridView ID="gvInvoice" runat="server" Width="500px" AutoGenerateColumns="true"
                                                BorderColor="#3366CC" CellPadding="4" Font-Names="Arial" Font-Size="8pt" ShowFooter="True">
                                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                                <RowStyle BackColor="White" ForeColor="#003399" />
                                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                                <HeaderStyle BackColor="#073088" Font-Bold="True" ForeColor="#CCCCFF" Font-Size="8pt" />
                                            </asp:GridView>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="AGENT/Shipping Line/FFNAME" DataField="FFName" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:TemplateField HeaderText="ContainerDetails" ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>
                                            <asp:GridView ID="gvContainer" runat="server" Width="500px" AutoGenerateColumns="false"
                                                BorderColor="#3366CC" CellPadding="4" Font-Names="Arial" Font-Size="8pt">
                                                <Columns>
                                                    <asp:BoundField HeaderText="ContainerNo" DataField="ContainerNo" HeaderStyle-Font-Size="12px"
                                                        FooterStyle-Font-Size="8px"></asp:BoundField>
                                                    <asp:BoundField HeaderText="CONTAINER DETAILS" DataField="b" HeaderStyle-Font-Size="12px"
                                                        FooterStyle-Font-Size="8px"></asp:BoundField>
                                                </Columns>
                                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                                <RowStyle BackColor="White" ForeColor="#003399" />
                                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                                <HeaderStyle BackColor="#073088" Font-Bold="True" ForeColor="#CCCCFF" Font-Size="8pt" />
                                            </asp:GridView>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:BoundField HeaderText="CONTAINER NO." DataField="" HeaderStyle-Font-Size="12px"
                                    FooterStyle-Font-Size="8px"></asp:BoundField>
                                <asp:BoundField HeaderText="CONTAINER DETAILS" DataField="" HeaderStyle-Font-Size="12px"
                                    FooterStyle-Font-Size="8px"></asp:BoundField>--%>
                                    <asp:BoundField HeaderText="WEIGHT" DataField="GrossWeight" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:BoundField HeaderText="NO. OF BOXES" DataField="NoOfPackages" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:BoundField HeaderText="DESCRIPTION" DataField="BEHeading" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:BoundField HeaderText="ORIGINAL DOCUMENTS RECEIVED ON" DataField="JobReceivedDate"
                                        HeaderStyle-Font-Size="12px" FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="FACTORY" DataField="" HeaderStyle-Font-Size="12px" FooterStyle-Font-Size="8px"
                                        ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:BoundField HeaderText="BE NO" DataField="BENo" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:BoundField HeaderText="ETA" DataField="ETA" HeaderStyle-Font-Size="12px" FooterStyle-Font-Size="8px"
                                        ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:BoundField HeaderText="ICD/FLIGHT  INWARD DT" DataField="GLDInwardDate" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <%-- <asp:BoundField HeaderText="DELIVERY PLAN" DataField="" HeaderStyle-Font-Size="12px"
                                    FooterStyle-Font-Size="8px"></asp:BoundField>--%>
                                    <asp:BoundField HeaderText="POL" DataField="ShipmentPort" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:BoundField HeaderText="POD" DataField="" HeaderStyle-Font-Size="12px" FooterStyle-Font-Size="8px"
                                        ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Status Details" ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>
                                            <asp:GridView ID="gvStatus" runat="server" Width="500px" AutoGenerateColumns="false"
                                                BorderColor="#3366CC" CellPadding="4" Font-Names="Arial" Font-Size="8pt" ItemStyle-VerticalAlign="Top">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Date of Activity" DataField="StatusDate" HeaderStyle-Font-Size="12px"
                                                        FooterStyle-Font-Size="8px"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Status / Activity REMARKS" DataField="Remarks" HeaderStyle-Font-Size="12px"
                                                        FooterStyle-Font-Size="8px"></asp:BoundField>
                                                </Columns>
                                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                                <RowStyle BackColor="White" ForeColor="#003399" />
                                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                                <HeaderStyle BackColor="#073088" Font-Bold="True" ForeColor="#CCCCFF" Font-Size="8pt" />
                                            </asp:GridView>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField HeaderText="Date of Activity" DataField="" HeaderStyle-Font-Size="12px"
                                    FooterStyle-Font-Size="8px"></asp:BoundField>
                                <asp:BoundField HeaderText="Status / Activity Remarks" DataField="" HeaderStyle-Font-Size="12px"
                                    FooterStyle-Font-Size="8px"></asp:BoundField>--%>
                                    <asp:BoundField HeaderText="RMS/OPEN" DataField="" HeaderStyle-Font-Size="12px" FooterStyle-Font-Size="8px"
                                        ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:BoundField HeaderText="ASS. VALUE " DataField="TotalAssVal" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:BoundField HeaderText="DUTY VALUE" DataField="TotalDuty" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:BoundField HeaderText="DEMMURAGE" DataField="" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                </Columns>
                                <RowStyle CssClass="table-header light" />
                                <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" />
                                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                <AlternatingRowStyle BackColor="#E7E7FF" />
                            </asp:GridView>
                            <asp:GridView ID="gvFormat3" runat="server" CssClass="gridview" AutoGenerateSelectButton="false"
                                AutoGenerateColumns="False" Width="800px" 
                                onrowdatabound="gvFormat3_RowDataBound" >
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="JobNo" Visible="true" ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "JobNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Gross Wt" DataField="GrossWeight" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:BoundField HeaderText="Supplier Name" DataField="Importer" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top" ItemStyle-Width="150px">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="MBLNO. /AWB NO." DataField="MasterBLNo" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Invoice Details" ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>
                                            <asp:GridView ID="gvInvoice1" runat="server" Width="500px" AutoGenerateColumns="true"
                                                BorderColor="#3366CC" CellPadding="4" Font-Names="Arial" Font-Size="8pt" ShowFooter="True">
                                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                                <RowStyle BackColor="White" ForeColor="#003399" />
                                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                                <HeaderStyle BackColor="#073088" Font-Bold="True" ForeColor="#CCCCFF" Font-Size="8pt" />
                                            </asp:GridView>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="DESCRIPTION" DataField="BEHeading" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:BoundField HeaderText="NO. OF PKGS" DataField="NoOfPackages" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:BoundField HeaderText="ETA" DataField="ETA" HeaderStyle-Font-Size="12px" FooterStyle-Font-Size="8px"
                                        ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:BoundField HeaderText="IGM/Item" DataField="LocalIGMNo" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:BoundField HeaderText="AGENT/Shipping Line/FFNAME" DataField="FFName" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:BoundField HeaderText="BE NO" DataField="BENoDate" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:TemplateField HeaderText="ContainerDetails" ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>
                                            <asp:GridView ID="gvContainer1" runat="server" Width="500px" AutoGenerateColumns="false"
                                                BorderColor="#3366CC" CellPadding="4" Font-Names="Arial" Font-Size="8pt">
                                                <Columns>
                                                    <asp:BoundField HeaderText="ContainerNo" DataField="ContainerNo" HeaderStyle-Font-Size="12px"
                                                        FooterStyle-Font-Size="8px"></asp:BoundField>
                                                    <asp:BoundField HeaderText="CONTAINER DETAILS" DataField="b" HeaderStyle-Font-Size="12px"
                                                        FooterStyle-Font-Size="8px"></asp:BoundField>
                                                </Columns>
                                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                                <RowStyle BackColor="White" ForeColor="#003399" />
                                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                                <HeaderStyle BackColor="#073088" Font-Bold="True" ForeColor="#CCCCFF" Font-Size="8pt" />
                                            </asp:GridView>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="ORIGINAL DOCUMENTS RECEIVED ON" DataField="JobReceivedDate"
                                        HeaderStyle-Font-Size="12px" FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="ASS. VALUE " DataField="TotalAssVal" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:BoundField HeaderText="DUTY Amount" DataField="TotalDuty" HeaderStyle-Font-Size="12px"
                                        FooterStyle-Font-Size="8px" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Status Details" ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>
                                            <asp:GridView ID="gvStatus1" runat="server" Width="500px" AutoGenerateColumns="false"
                                                BorderColor="#3366CC" CellPadding="4" Font-Names="Arial" Font-Size="8pt" ItemStyle-VerticalAlign="Top">
                                                <Columns>
                                                    <asp:BoundField HeaderText="REMARKS" DataField="Remark" HeaderStyle-Font-Size="12px"
                                                        FooterStyle-Font-Size="8px"></asp:BoundField>
                                                </Columns>
                                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                                <RowStyle BackColor="White" ForeColor="#003399" />
                                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                                <HeaderStyle BackColor="#073088" Font-Bold="True" ForeColor="#CCCCFF" Font-Size="8pt" />
                                            </asp:GridView>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle CssClass="table-header light" />
                                <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" />
                                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                <AlternatingRowStyle BackColor="#E7E7FF" />
                            </asp:GridView>
                        </div>
                    </asp:Panel>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/Billing/MasterPage1.master" AutoEventWireup="true"
    Inherits="CryInvoiceReportSTax" CodeBehind="CryInvoiceReportSTax.aspx.cs" %>

<%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
--%>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <table>
            <tr>
                <td style="height: 90%; vertical-align: top; background-color: white; width: 100%;">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Select Export Type :" Font-Names="verdana"
                                    Font-Size="8pt"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drTpye" Font-Names="verdana" Font-Size="8pt" runat="server">
                                    <asp:ListItem Value="PD">PDF File Format</asp:ListItem>
                                    <asp:ListItem Value="WF">Word Format</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="BtnExport" runat="server" Font-Names="verdana" Font-Size="8pt" Text="Export File"
                                    OnClick="BtnExport_Click" />
                            </td>
                        </tr>
                    </table>
                    <%--<CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                        <Report FileName="Billing\Reports\CrystalReportSTAX.rpt">
                        </Report>
                    </CR:CrystalReportSource>
                    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />--%>
                    
                </td>
            </tr>
        </table>
    </div>
       <div>
     
        <rsweb:reportviewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
            InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
            Width="884px" Height="581px" onprerender="ReportViewer1_PreRender">
            <LocalReport ReportPath="ImpInvoice.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="DataSet2" />
                </DataSources>
            </LocalReport>
        </rsweb:reportviewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
            TypeName="ImpexCube.ImpInvoiceTableAdapters.View_InvoiceTableAdapter" OldValuesParameterFormatString="original_{0}">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="ImpexCube.ImpInvoiceTableAdapters.View_appdetailsTableAdapter">
        </asp:ObjectDataSource>
    </div>
</asp:Content>

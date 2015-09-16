<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmImpInvoiceReport.aspx.cs"
    MasterPageFile="~/Billing/MasterPage.master" Inherits="ImpexCube.frmImpInvoiceReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
            InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
            Width="884px" Height="581px" OnPreRender="ReportViewer1_PreRender">
            <LocalReport ReportPath="ImpInvoice.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="DataSet2" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
            TypeName="ImpexCube.ImpInvoiceTableAdapters.View_InvoiceTableAdapter" OldValuesParameterFormatString="original_{0}">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="ImpexCube.ImpInvoiceTableAdapters.View_appdetailsTableAdapter">
        </asp:ObjectDataSource>
    </div>
</asp:Content>

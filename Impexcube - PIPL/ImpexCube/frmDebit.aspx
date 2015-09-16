﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmDebit.aspx.cs" Inherits="ImpexCube.frmDebit" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
            Height="581px" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
            WaitMessageFont-Size="14pt" Width="884px" 
            onprerender="ReportViewer1_PreRender">
            <LocalReport ReportPath="ImpDebit.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet2" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="DataSet1" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="ImpexCube.ImpInvoiceTableAdapters.View_appdetailsTableAdapter">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="ImpexCube.ImpInvoiceTableAdapters.View_DebitNoteTableAdapter">
        </asp:ObjectDataSource>
        <br />
    </div>
    </form>
</body>
</html>

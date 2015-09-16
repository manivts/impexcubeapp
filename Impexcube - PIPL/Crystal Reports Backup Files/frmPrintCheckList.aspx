<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="frmPrintCheckList.aspx.cs" Inherits="ImpexCube.frmPrintCheckList" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

        .style5
        {
            font-size: 8pt;
            font-family: Arial;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <table style="width:100%;">
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: center">
                <asp:Button ID="btnBack1" runat="server" onclick="btnBack1_Click" Text="Back" 
                    Width="100px" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: right">
                <asp:Label ID="Label1" runat="server" style="font-size: 10pt" Text="Job No  : "></asp:Label>
&nbsp;
                                    <asp:DropDownList ID="ddlJobNo" runat="server" 
                    AppendDataBoundItems="True" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged" 
                    CssClass="style5" Height="20px"
                                        Width="250px">
                                        <asp:ListItem>~Select~</asp:ListItem>
                                    </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: center">
<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
    Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
    WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1011px" 
                    style="text-align: left" Height="330px">
    <LocalReport ReportPath="Print_CheckList.rdlc">
        <DataSources>
            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                Name="DataSetCheckList" />
                 <rsweb:ReportDataSource DataSourceId="ObjectDataSource2"
                    Name="DataSet1" />
                   <%-- <rsweb:ReportDataSource DataSourceId="ObjectDataSource3"
                    Name="DataSetContainerDetails" />--%>
                  <rsweb:ReportDataSource DataSourceId="ObjectDataSource4" 
                    Name="DataSetImpShipment" />
        </DataSources>
    </LocalReport>
</rsweb:ReportViewer>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: center">
                <asp:Button ID="btnBack2" runat="server" onclick="btnBack2_Click" Text="Back" 
                    Width="100px" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" 
        SelectMethod="GetData" 
        
        TypeName="ImpexCube.ImpexCubeDSTableAdapters.View_ImportShipmentTableAdapter" 
        OldValuesParameterFormatString="original_{0}">
    </asp:ObjectDataSource>
<%--<asp:ObjectDataSource ID="ObjectDataSource3" runat="server" 
    SelectMethod="GetData" 
    TypeName="ImpexCube.ImpexCubeDSTableAdapters.T_ShipmentContainerInfoTableAdapter">
</asp:ObjectDataSource>--%>
<asp:ObjectDataSource ID="ObjectDataSource2" runat="server"
        SelectMethod="GetData"
        TypeName="ImpexCube.ImpexCubeDSTableAdapters.View_ProductTableAdapter">
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
    TypeName="ImpexCube.ImpexCubeDSTableAdapters.View_ImportInvoiceTableAdapter">
</asp:ObjectDataSource>

<br />
</asp:Content>

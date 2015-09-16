<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="efrmPrintCheckList.aspx.cs" Inherits="ImpexCube.efrmPrintCheckList" %>
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
    <table width="100%">
<tr>
<td>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

</td>
</tr>
<tr>
<td style="text-align: right">

                <asp:Label ID="Label1" runat="server" style="font-size: 10pt" Text="Job No  : "></asp:Label>
                                    <asp:DropDownList ID="ddlJobNo" runat="server" 
                    AppendDataBoundItems="True" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged" 
                    CssClass="style5" Height="20px"
                                        Width="250px">
                                        <asp:ListItem>~Select~</asp:ListItem>
                                    </asp:DropDownList>

</td>
</tr>
<tr>
<td>

    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="790px">
        <LocalReport ReportPath="Exp_Print_CheckList.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                    Name="DataSetExpCheckList" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" 
                    Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
        TypeName="ImpexCube.ExpCheckListTableAdapters.View_ExpCheckListTableAdapter">
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
        TypeName="ImpexCube.ExpCheckListTableAdapters.View_ExpProductTableAdapter">
    </asp:ObjectDataSource>

    <br />
    <br />
    <br />
    <br />

</td>
</tr>
</table>
</asp:Content>

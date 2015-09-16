<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmFundPrint.aspx.cs" Inherits="ImpexCube.frmFundPrint" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1000px">
                <LocalReport ReportPath="FundRequest.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="FundDataset" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                DeleteMethod="Delete" InsertMethod="Insert" 
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
                TypeName="ImpexCube.FundDSTableAdapters.T_FundRequestTableAdapter" 
                UpdateMethod="Update">
                <DeleteParameters>
                    <asp:Parameter Name="Original_FundRequestId" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="FundRequestNo" Type="String" />
                    <asp:Parameter Name="FundRequestDate" Type="DateTime" />
                    <asp:Parameter Name="JobNo" Type="String" />
                    <asp:Parameter Name="ImporterName" Type="String" />
                    <asp:Parameter Name="RequestAmt" Type="Decimal" />
                    <asp:Parameter Name="RequestDate" Type="DateTime" />
                    <asp:Parameter Name="MOP" Type="String" />
                    <asp:Parameter Name="RequestBy" Type="String" />
                    <asp:Parameter Name="Purpose" Type="String" />
                    <asp:Parameter Name="UserRemarks" Type="String" />
                    <asp:Parameter Name="ApprovedAmt" Type="Decimal" />
                    <asp:Parameter Name="ApprovalDate" Type="DateTime" />
                    <asp:Parameter Name="Approved" Type="Boolean" />
                    <asp:Parameter Name="ApprovalAmtFrom" Type="String" />
                    <asp:Parameter Name="ApprovalRemarks" Type="String" />
                    <asp:Parameter Name="PaymentAmt" Type="Decimal" />
                    <asp:Parameter Name="PaymentMode" Type="String" />
                    <asp:Parameter Name="PaymentStatus" Type="String" />
                    <asp:Parameter Name="PayBalance" Type="Decimal" />
                    <asp:Parameter Name="ChequeDDNo" Type="String" />
                    <asp:Parameter Name="DDChequeDate" Type="String" />
                    <asp:Parameter Name="BankName" Type="String" />
                    <asp:Parameter Name="DrewBank" Type="String" />
                    <asp:Parameter Name="PayRemarks" Type="String" />
                    <asp:Parameter Name="Completed" Type="Boolean" />
                    <asp:Parameter Name="CreatedBy" Type="Int32" />
                    <asp:Parameter Name="CreatedDate" Type="DateTime" />
                    <asp:Parameter Name="ModifiedBy" Type="Int32" />
                    <asp:Parameter Name="ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="CfsName" Type="String" />
                    <asp:Parameter Name="ShippingName" Type="String" />
                    <asp:Parameter Name="BenificiaryName" Type="String" />
                    <asp:Parameter Name="Payable" Type="String" />
                    <asp:Parameter Name="Active" Type="Boolean" />
                    <asp:Parameter Name="AccountsPaymentEntry" Type="Boolean" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="FundRequestNo" Type="String" />
                    <asp:Parameter Name="FundRequestDate" Type="DateTime" />
                    <asp:Parameter Name="JobNo" Type="String" />
                    <asp:Parameter Name="ImporterName" Type="String" />
                    <asp:Parameter Name="RequestAmt" Type="Decimal" />
                    <asp:Parameter Name="RequestDate" Type="DateTime" />
                    <asp:Parameter Name="MOP" Type="String" />
                    <asp:Parameter Name="RequestBy" Type="String" />
                    <asp:Parameter Name="Purpose" Type="String" />
                    <asp:Parameter Name="UserRemarks" Type="String" />
                    <asp:Parameter Name="ApprovedAmt" Type="Decimal" />
                    <asp:Parameter Name="ApprovalDate" Type="DateTime" />
                    <asp:Parameter Name="Approved" Type="Boolean" />
                    <asp:Parameter Name="ApprovalAmtFrom" Type="String" />
                    <asp:Parameter Name="ApprovalRemarks" Type="String" />
                    <asp:Parameter Name="PaymentAmt" Type="Decimal" />
                    <asp:Parameter Name="PaymentMode" Type="String" />
                    <asp:Parameter Name="PaymentStatus" Type="String" />
                    <asp:Parameter Name="PayBalance" Type="Decimal" />
                    <asp:Parameter Name="ChequeDDNo" Type="String" />
                    <asp:Parameter Name="DDChequeDate" Type="String" />
                    <asp:Parameter Name="BankName" Type="String" />
                    <asp:Parameter Name="DrewBank" Type="String" />
                    <asp:Parameter Name="PayRemarks" Type="String" />
                    <asp:Parameter Name="Completed" Type="Boolean" />
                    <asp:Parameter Name="CreatedBy" Type="Int32" />
                    <asp:Parameter Name="CreatedDate" Type="DateTime" />
                    <asp:Parameter Name="ModifiedBy" Type="Int32" />
                    <asp:Parameter Name="ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="CfsName" Type="String" />
                    <asp:Parameter Name="ShippingName" Type="String" />
                    <asp:Parameter Name="BenificiaryName" Type="String" />
                    <asp:Parameter Name="Payable" Type="String" />
                    <asp:Parameter Name="Active" Type="Boolean" />
                    <asp:Parameter Name="AccountsPaymentEntry" Type="Boolean" />
                    <asp:Parameter Name="Original_FundRequestId" Type="Int32" />
                </UpdateParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

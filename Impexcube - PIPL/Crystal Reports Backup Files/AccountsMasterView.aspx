<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="AccountsMasterView.aspx.cs" Inherits="ImpexCube.Accounts.AccountsMasterView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:SqlDataSource ID="SqlDataSourceLedger" runat="server" 
    ConnectionString="<%$ ConnectionStrings:Constr %>" 
    SelectCommand="SELECT * FROM [AccountMaster]"></asp:SqlDataSource>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" 
    AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>
        <asp:Image ID="Image1" runat="server" 
    ImageUrl="~/Content/Images/progress.gif" />
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table style="width:100%;">
            <tr>
                <td colspan="3" style="text-align: center">
                    <asp:TextBox ID="TextBoxSearch" runat="server" 
                        ontextchanged="TextBoxSearch_TextChanged" 
                        AutoPostBack="True" CssClass="textbox140"></asp:TextBox>
                    <asp:Button ID="ButtonSearch" runat="server" Text="Search" 
                        onclick="ButtonSearch_Click" CssClass="masterbutton" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                        DataKeyNames="TransId" DataSourceID="SqlDataSourceLedger" 
                        onselectedindexchanged="GridView1_SelectedIndexChanged" style="font-size: 10pt" 
                        Width="1076px">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:BoundField DataField="AccountCode" HeaderText="Ledger Code" 
                                SortExpression="AccountCode" />
                            <asp:BoundField DataField="ShortName" HeaderText="ShortName" 
                                SortExpression="ShortName" />
                            <asp:BoundField DataField="AccountName" HeaderText="Ledger Name" 
                                SortExpression="AccountName" />
                            <asp:BoundField DataField="Acc_Group" HeaderText="Group" 
                                SortExpression="Acc_Group" />
                            <asp:BoundField DataField="Address1" HeaderText="Address1" 
                                SortExpression="Address1" />
                            <asp:BoundField DataField="PhoneNo" HeaderText="PhoneNo" 
                                SortExpression="PhoneNo" />
                            <asp:BoundField DataField="Mobile" HeaderText="Mobile" 
                                SortExpression="Mobile" />
                            <asp:BoundField DataField="EmailID" HeaderText="EmailID" 
                                SortExpression="EmailID" />
                            <asp:BoundField DataField="IECode" HeaderText="IECode" 
                                SortExpression="IECode" />
                            <asp:BoundField DataField="ContactPerson" HeaderText="ContactPerson" 
                                SortExpression="ContactPerson" />
                            <asp:BoundField DataField="OpeningBalance" HeaderText="O/P Balance" 
                                SortExpression="OpeningBalance" />
                            <asp:BoundField DataField="DRCR" HeaderText="DRCR" SortExpression="DRCR" />
                            <asp:BoundField DataField="CreditLimit" HeaderText="CreditLimit" 
                                SortExpression="CreditLimit" />
                            <asp:CheckBoxField DataField="CostCenter" HeaderText="CostCenter" 
                                SortExpression="CostCenter" />
                            <asp:BoundField DataField="Approved" HeaderText="Approved" 
                                SortExpression="Approved" />
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <br />
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

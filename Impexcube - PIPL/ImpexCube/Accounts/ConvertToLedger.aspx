<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="ConvertToLedger.aspx.cs" Inherits="AccountsManagement.ConvertToLedger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #savebtn
        {
            width:70px;
            margin-left: 400px;
            float: left;
        }
        #MainPage
        {
            height:250px;
            width:1000px;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div id="MainPage">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
              AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <asp:Image ID="Image2" runat="server" 
                    ImageUrl="~/Accounts/AccImages/progress.gif" />
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridLedger" runat="server" AutoGenerateColumns="false" BorderColor="Black"
                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="10pt" ForeColor="Black"
                    ShowFooter="false" ShowHeader="true" Style="text-align: left; font-size: 8pt;"
                    Width="904px">
                    <FooterStyle BackColor="#C6C3C6" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                        ForeColor="Black" />
                    <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                    <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="#E7E7FF" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="CompName" HeaderText="Company Name" HtmlEncode="false"/>
                        <asp:BoundField DataField="address1" HeaderText="Address" HtmlEncode="false"/>
                        <asp:BoundField DataField="BranchId" HeaderText="Branch Id"  HtmlEncode="false"/>
                        <asp:TemplateField HeaderText="Tally Account Name">
                            <ItemTemplate>
                                <asp:TextBox ID="txtledger" runat="server" Text='<%#Bind("AccountName") %>' Width="350px"
                                    Style="font-size: 8pt" >
                                </asp:TextBox>
                            </ItemTemplate >
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Short Name">
                            <ItemTemplate>
                                <asp:TextBox ID="txtshort" runat="server" Text='<%#Bind("ShortName") %>' Width="150px" Style="font-size: 8pt">
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div id="savebtn">
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" OnClientClick="return confirm('Do you want to Save?');"
                        Text="Save" Width="70px"  />
                </div>
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
          <asp:Label ID="txtNote" runat="server" Font-Size="Larger"></asp:Label>
    </div>
</asp:Content>

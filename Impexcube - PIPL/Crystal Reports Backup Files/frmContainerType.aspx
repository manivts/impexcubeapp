<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmContainerType.aspx.cs" Inherits="ImpexCube.frmContainerType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function validate() {
        var containertype = document.getElementById("<%= txtContainerType.ClientID %>").value;
        if (containertype == "") {
            alert('Please Enter ContainerType');
            document.getElementById("<%= txtContainerType.ClientID %>").focus();
            return false;
        }
        return true;
    }
</script>
    <style type="text/css">
        .style2
        {
            width: 441px;
        }
        .style5
        {
            width: 958px;
        }
        .style10
        {
            width: 280px;
        }
        .style12
        {
            height: 253px;
            width: 949px;
        }
        .style14
        {
            width: 183px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                    <tr>
                        <td colspan="2" align="center" style="color: #008080; font-style: italic; font-weight: bold;
                            font-size: large">
                            &nbsp;ContainerType Master
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" style="color: #008080; font-style: italic; font-weight: bold;
                            font-size: large">
                           </td>
                    </tr>
                    <tr>
                       <td style="width: 100px;">
                            <asp:Label ID="lblContainerType" runat="server" Text="Container Type" 
                                CssClass="fontsize"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtContainerType" runat="server" CssClass="textbox150"></asp:TextBox><font color=red>*</font>
                        </td>
                    </tr>
                    <tr>
                       
                        <td style="width: 100px;">
                            <asp:Label ID="lblDescription" runat="server" Text="Description" 
                                CssClass="fontsize"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtContainerDesc" runat="server" CssClass="textboxHeight29" 
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                      
                    </tr>
                   
            <tr>
                        <td align="center" colspan="2" >
                            <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CssClass="masterbutton" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="masterbutton" OnClientClick="javascript:return validate();" />
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                                CssClass="masterbutton" />
                            <asp:Button ID="btnDiscard" runat="server" Text="Exit" OnClick="btnDiscard_Click"
                                CssClass="masterbutton" />
                        </td>
                    </tr>
                 
                        <tr>
                            <td colspan="2">
                            </td>
                       </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="grid_scroll-2">
                                            <asp:GridView ID="gvContainerType" runat="server" AutoGenerateColumns="False" 
                                                AutoGenerateSelectButton="True" CssClass="table-wrapper" 
                                                OnPageIndexChanging="gvContainerType_PageIndexChanging" 
                                                OnSelectedIndexChanged="gvPackage_SelectedIndexChanged1" Width="600px">
                                                <Columns>
                                                    <asp:BoundField DataField="ContainerType" HeaderStyle-HorizontalAlign="Center" 
                                                        HeaderText="ContainerType" InsertVisible="False" ReadOnly="True" 
                                                        SortExpression="ContainerType" />
                                                    <asp:BoundField DataField="Description" HeaderStyle-HorizontalAlign="Center" 
                                                        HeaderText="Description" SortExpression="Description" />
                                                </Columns>
                                                <RowStyle CssClass="table-header light" />
                                                <HeaderStyle BackColor="#E7E7FF" CssClass="table-row" />
                                                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                <AlternatingRowStyle BackColor="#E7E7FF" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                        
                    </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

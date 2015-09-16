<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmStatusMaster.aspx.cs" Inherits="ImpexCube.frmStatusMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function validate() {
        var statusid = document.getElementById("<%= txtStatusId.ClientID %>").value;
        if (statusid == "") {
            alert('Please Enter StatusId');
            document.getElementById("<%= txtStatusId.ClientID %>").focus();
            return false;
        }
        return true;
    }
</script>
    <style type="text/css">
        .style5
        {
            width: 1117px;
        }
        .style13
        {
            width: 397px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>                     
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">  
    <ContentTemplate>
    <table>
        <table>
                <tr>
                    <td colspan="2" align="center" style="color: #008080; font-style: italic; font-weight: bold;
                        font-size: large">
                        Status Master
                    </td>                                                                                                                                                   
                         </tr>
                <tr>
                    <td align="center" colspan="2" style="color: #008080; font-style: italic; font-weight: bold;
                        font-size: large">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="fontsize">
                        Status Id
                    </td>
                                        
                                    <td>
                                        <asp:TextBox ID="txtStatusId" runat="server" CssClass="textbox150" 
                                            ReadOnly="true"></asp:TextBox>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td class="fontsize" >
                                        Stage
                                   </td>
                                   <td>
                                        <asp:DropDownList ID="ddlStage" runat="server" CssClass="ddl156">
                                        </asp:DropDownList>
                                        </td>
                                   </tr>
                                   <tr>
                                   <td class="fontsize" >
                                        Status
                                    
                                   </td>
                                   <td>
                                        <asp:TextBox ID="txtStatusName" runat="server" CssClass="textbox150"></asp:TextBox>
                                    </td>
                           </tr>
                           <tr>
                           <td class="fontsize" >
                                        Type of Communication
                                    </td>
                                    <td class="fontsize">
                                        <asp:CheckBox ID="chkInternal" runat="server" CssClass="radio-area1" 
                                            Text="Internal" Height="20px" Width="71px" />&nbsp;
                                        <asp:CheckBox ID="chkExternal" runat="server" CssClass="radio-area1" 
                                            Text="External" Height="18px" Width="83px" />&nbsp;
                                        <asp:CheckBox ID="chkCustomer" runat="server" CssClass="radio-area1" 
                                            Text="Customer" Height="16px" Width="81px" />
                                        </td>
                                </tr>
                                <tr>
                                <td class="fontsize">
                                        Subject
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSubject" runat="server" CssClass="textd" 
                                            TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                   </tr>
                                   <tr>
                                       
                                       <td class="fontsize"> 
                                       Comment
                                   </td>
                                   <td>
                                        <asp:TextBox ID="txtComment" runat="server" CssClass="textd" 
                                            TextMode="MultiLine"></asp:TextBox>
                                   </td>
                            </tr>
                                <tr>
                                <td  align="right" colspan="2" >
                                    <asp:Button ID="btnNew" runat="server" CssClass="masterbutton" 
                                        OnClick="btnNew_Click" Text="New">
                                    </asp:Button>
                                  
                                    <asp:Button ID="btnSave" runat="server" CssClass="masterbutton" 
                                        OnClick="btnSave_Click" OnClientClick="javascript:return validate();" 
                                        Text="Save" />
                                  
                                    <asp:Button ID="btnCancel" runat="server" Text="Exit" CssClass="masterbutton" 
                                        onclick="btnCancel_Click" ></asp:Button>
                                    </td>
                                </tr>
                
                    <tr>
                        <td colspan="2">
                            &nbsp;</td>
                       
                </tr>
                
                    <tr>
                    <td colspan="2">
                      <div class="grid_scroll-2">
                        <asp:GridView ID="gvStatusDetails" runat="server" CellPadding="4" GridLines="None"
                            CssClass="table-wrapper" AutoGenerateColumns="false" Font-Size="10pt" 
                              OnSelectedIndexChanged="gvStatusDetails_SelectedIndexChanged" 
                              Width="600px" Height="120px">
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" />
                                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="StageId" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Stage" HeaderText="Stage" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Communication" HeaderText="Communication Type" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Subject" HeaderText="Subject" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Comment" HeaderText="Comment" ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                            <RowStyle CssClass="table-header light" />
                            <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" />
                            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                            <AlternatingRowStyle BackColor="#E7E7FF" />
                        </asp:GridView>  
                       
                         </div>
                    </td>
                </tr>
              </table>  
              </table>   
              </ContentTemplate>                 
    </asp:UpdatePanel>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="frmVesselMaster.aspx.cs" Inherits="ImpexCube.frmVesselMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function validate() {
        var vessel = document.getElementById("<%= txtVesselcode.ClientID %>").value;
        if (vessel == "") {
            alert('Please Enter Vessel Code');
            document.getElementById("<%= txtVesselcode.ClientID %>").focus();
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
        .style12
        {
            height: 253px;
            width: 949px;
        }
        .style15
        {
            font-family: Verdana;
            font-size: 8pt;
            height: 19px;
        }
        .style16
        {
            width: 441px;
            height: 19px;
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
                        &nbsp;Vessel Master
                    </td>
                </tr>                
                 <tr>
                     <td colspan="2" ></td>
                </tr>
                 <tr>
                    <td align="center" class="fontsize">
                        Vessel Code:
                    </td>
                    <td >
                        <asp:TextBox ID="txtVesselcode" runat="server" CssClass="textbox150"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center" class="fontsize">
                        Vessel Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtVesselName" runat="server" 
                            CssClass="textbox150" ></asp:TextBox>
                    </td>
                </tr>
          
               
                    <tr>
                <td style="text-align: center" colspan="2">
                    <asp:Button ID="btnNew" runat="server" Text="New" 
                        onclick="btnNew_Click" CssClass="masterbutton" />        
                    <asp:Button ID="btnSave" runat="server" Text="Save" 
                        onclick="btnSave_Click" CssClass="masterbutton" OnClientClick="javascript:return validate();" />                                               
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" 
                        onclick="btnUpdate_Click" CssClass="masterbutton" />                        
                    <asp:Button ID="btnDiscard" runat="server" Text="Exit" 
                        onclick="btnDiscard_Click1" CssClass="masterbutton" />
                        </td>                   
                
            </tr>
               
                
                <tr>
                    <td colspan="2" style="text-align: center">
                        &nbsp;</td>
                </tr>
               
                
                <tr>
                    <td colspan="2">
                      <div class="grid_scroll-2">
                        <asp:GridView ID="gvVesselMaster" runat="server" CssClass="table-wrapper" 
                              AutoGenerateSelectButton="True" 
                              onselectedindexchanged="gvVesselMaster_SelectedIndexChanged"  
                              AutoGenerateColumns="False" Width="600px" Height="120px">
                              
                              <Columns>
                                <asp:BoundField DataField="TransId" HeaderText="TransId" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" SortExpression="VesselCode" HeaderStyle-HorizontalAlign="Center"/>
                              <asp:BoundField DataField="VesselCode" HeaderText="VesselCode" InsertVisible="False" 
                                    ReadOnly="True" SortExpression="VesselCode" HeaderStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="VesselName" HeaderText="VesselName" 
                                    SortExpression="VesselName" HeaderStyle-HorizontalAlign="Center" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

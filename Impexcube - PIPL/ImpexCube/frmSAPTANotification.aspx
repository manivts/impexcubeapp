<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="frmSaptaNotification.aspx.cs" Inherits="ImpexCube.frmSaptaNotification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            font-family: Verdana;
            font-size: small;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="Panel1" runat="server" Width="747px">
        <table width="950px" style="height: 305px">
        <tr>
        <td align="center" colspan="2">
            <strong>
            <asp:Label ID="Label4" runat="server" CssClass="style1" 
                Text="SAPTA Notification"></asp:Label>
            </strong>
        </td>
        </tr>
            
            <tr>
                <td align="center" colspan="2">
                    &nbsp;</td>
            </tr>
            
            <tr>
                <td class="style2" >
                    <asp:Label ID="Label2" runat="server" Text="SAPTA Description" CssClass="fontsize"></asp:Label>
                </td>               
                <td >                    
                    <asp:TextBox ID="txtSaptaDesc" runat="server" CssClass="textbox300" 
                        TextMode="MultiLine"></asp:TextBox>
                                 

                </td>
                </tr>
                <tr>
                <td class="style2" >
                    <asp:Label ID="Label5" runat="server" Text="Notification" 
                        CssClass="fontsize"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="txtNotification" runat="server" CssClass="textbox150"></asp:TextBox>                     
                </td>               
            </tr>
            <tr>
                 <td class="style2" >
                     <asp:Label ID="Label3" runat="server" Text="Serial No" CssClass="fontsize"></asp:Label>
                </td>          
                <td >
                    <asp:TextBox ID="txtserialno" runat="server" CssClass="textbox150"></asp:TextBox>                                        
                </td>
                                             
            </tr>
            <tr>
                <td >
                     <asp:Label ID="Label1" runat="server" Text="Duty Rate" CssClass="fontsize"></asp:Label></td>
                     <td><asp:TextBox ID="txtdutyrate" runat="server" CssClass="textbox150"></asp:TextBox></td>
            </tr>
            <tr>
            <td align="center" colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    <asp:Button ID="btnsave" runat="server" CssClass="stylebutton" Text="Save" 
                        onclick="btnsave_Click" />
                    <asp:Button ID="btnupdate" runat="server" CssClass="stylebutton" 
                        onclick="btnupdate_Click" Text="Update" />
                </td>
            </tr>
            <tr>
            <td colspan="2">
           
                <asp:GridView ID="gvsaptaNotification" runat="server" AutoGenerateColumns="false" 
                    CssClass="table-wrapperInv"
                                             BorderColor="Black" 
                    BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="calibri" Font-Size="10pt" 
                    ForeColor="Black" GridLines="Vertical"
                                            ShowFooter="false" ShowHeader="true" 
                    Style="text-align: center; font-size: 9pt;" Width="652px">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                         <asp:BoundField DataField="TransId" HeaderText="TransID" 
                            ItemStyle-HorizontalAlign="Center" SortExpression="TransID" />
                        <asp:BoundField DataField="Description" HeaderText="Sapta Description" 
                            ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="Notification" HeaderText="Notification" 
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SerialNo" HeaderText="Serial no" 
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="DutyRate" HeaderText="Duty Rate" 
                            ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <RowStyle CssClass="table-header light" />
                    <HeaderStyle BackColor="#E7E7FF" CssClass="table-row" />
                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                    <AlternatingRowStyle BackColor="#E7E7FF" />
                </asp:GridView>

            </td>
            </tr>
            </table>
            </asp:Panel>
</asp:Content>

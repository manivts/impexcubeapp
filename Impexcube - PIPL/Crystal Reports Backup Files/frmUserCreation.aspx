<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="frmUserCreation.aspx.cs" Inherits="ImpexCube.frmUserCreation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
    .check {
      border:none 1px #444444;
      }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
        
    <table style=" width:100%; height: 595px;  margin-top: -13px; " >
  
    <tr>
    
    <td style=" width: 953px; vertical-align: top;">
    <table style="width: 100%;">
    <tr>
    <td style=" vertical-align: top; width: 20%; background-color: #EFF3FB;" >
    <table style="width: 100%;">
   
     <tr>
    <td>
    <asp:LinkButton ID="lkImporter" runat="server" BorderColor="White" BorderStyle="Solid"
            BorderWidth="1px" CausesValidation="False" Font-Bold="True" Font-Names="Corbel"
            Font-Overline="False" Font-Size="9pt" ForeColor="DodgerBlue" Height="25px"
            Width="150px" onclick="lkImporter_Click">Importer Register</asp:LinkButton>
    </td>
    </tr>
    <tr>
    <td>
     <asp:LinkButton ID="lkNewUser" runat="server" BorderColor="White" BorderStyle="Solid"
            BorderWidth="1px" CausesValidation="False" Font-Bold="True" Font-Names="Corbel"
            Font-Overline="False" Font-Size="9pt" ForeColor="DodgerBlue" Height="25px" OnClick="lkNewUser_Click"
            Width="150px">New User</asp:LinkButton>
    </td>
    </tr>
    
    <tr>
    <td>
     <asp:LinkButton ID="lkUser" runat="server" BorderColor="White" BorderStyle="Solid"
            BorderWidth="1px" CausesValidation="False" Font-Bold="True" Font-Names="Corbel"
            Font-Overline="False" Font-Size="9pt" ForeColor="DodgerBlue" Height="25px" OnClick="lkUser_Click"
            Width="150px">User View</asp:LinkButton>
    </td>
    </tr>
     <tr>
    <td>
     <asp:LinkButton ID="lkExit" runat="server" BorderColor="White" BorderStyle="Solid"
            BorderWidth="1px" CausesValidation="False" Font-Bold="True" Font-Names="Corbel"
            Font-Overline="False" Font-Size="9pt" ForeColor="DodgerBlue" Height="25px"
            Width="150px" onclick="lkExit_Click">Exit</asp:LinkButton>
    </td>
    </tr>
    </table>
       
       </td>
    <td align="center" style="vertical-align: top; width: 80%;" >
    <table style="background-color: White;">
    <tr>
    <td align="center"  style="vertical-align: top;">
    <asp:Label id="Label2" runat="server" Font-Size="12pt" Font-Names="Arial" Text="User Authentication - Panel" ForeColor="DodgerBlue"></asp:Label>
    </td>
    </tr>
    <tr>
    <td style="vertical-align: top;">
    <asp:GridView ID="GrdUser" runat="server" BorderColor="#C0C0FF" BorderStyle="Solid" 
            BorderWidth="0px" CellPadding="3" Font-Names="Arial" Font-Size="8pt" 
            AutoGenerateColumns="False" Width="631px" DataKeyNames="uid" 
            OnSelectedIndexChanged="GrdUser_SelectedIndexChanged" AllowPaging="True" 
            OnPageIndexChanging="GrdUser_PageIndexChanging" BackColor="#C0C0FF" 
            CellSpacing="2" Font-Underline="False" PageSize="15" 
            OnRowDeleting="GrdUser_RowDeleting" onrowupdating="GrdUser_RowUpdating">
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <RowStyle BackColor="#FFF7E7" ForeColor="#404040" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#C0C0FF" Font-Bold="True" ForeColor="White" />
            <Columns>
            <asp:CommandField ButtonType="Image" HeaderText="Select" SelectImageUrl="~/image/userLogo.JPG"   ShowSelectButton="True"  >
                    <HeaderStyle Height="5px" />
                <ControlStyle Height="15px" Width="15px" />
           </asp:CommandField>
           
            <asp:BoundField DataField="employeeName" HeaderText="EmpName" SortExpression="EmpName" >
                <ItemStyle Wrap="False" Width="120px" HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Left"  />
                </asp:BoundField>
                <asp:BoundField DataField="EmpName" HeaderText="User ID" SortExpression="EmpName" >
                <ItemStyle Wrap="False" Width="220px" HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Left"  />
                </asp:BoundField>
                <asp:TemplateField  HeaderText="Reset">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnResetPass"  Font-Names="arial" Font-Size="8pt" runat="server" CommandName="update" OnClientClick="return confirm ('Do you want to Reset Password?');">Reset Password</asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle BorderColor="White" BorderStyle="None" />
                </asp:TemplateField>
                <asp:BoundField DataField="empid" Visible="false"  HeaderText="Password" SortExpression="empid" >
                    <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Grade" HeaderText="Grade" SortExpression="grade" >
                    <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Zone" HeaderText="Branch" SortExpression="zone" >
                    <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Mail" HeaderText="E-Mail" SortExpression="mail" >
                    <ItemStyle Wrap="False" HorizontalAlign="Left"  />
                </asp:BoundField>
                <asp:TemplateField  >
                    <ItemTemplate>
                    <asp:LinkButton ID="BTC" ForeColor="white" CssClass="check" BorderStyle="none" BorderColor="white"   runat="server" CommandName="Delete"  OnClientClick="return confirm ('Do you want to Delete this User ?');">
                        <asp:Image ID="Image2" ImageUrl="~/Content/Images/delete.png" Width="18px" Height="18px" runat="server" /></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle BorderColor="White" BorderStyle="None" />
                    
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </td>
    </tr>
    <tr>
    <td align="left" style="background-color: #EFF3FB; vertical-align: top;" >
    <asp:Panel ID="plUser" runat="server" Height="580px" Width="702px" 
            HorizontalAlign="Left">
        <table>
        <tr>
        <td style="height: 21px;">
        <asp:Label ID="Label4" runat="server" Text="Employee Name :" Width="103px" Font-Names="Arial" Font-Size="9pt"></asp:Label>
        <asp:Label id="lblEName" runat="server"  Font-Names="Arial" Font-Size="9pt" Width="220px"></asp:Label></td>
        <td style="height: 21px">
            &nbsp;</td>
        <td style="height: 21px">
            <asp:Label ID="Label6" runat="server" Text="Branch :" Font-Names="Arial" Font-Size="9pt"></asp:Label>
            <asp:Label ID="lblZone" runat="server" Font-Names="Arial" Font-Size="9pt" Width="96px"></asp:Label></td>
        <td style="height: 21px">
            &nbsp;</td>
        </tr>
        <tr>
        <td colspan="4" style="height: 21px">
            <asp:Label ID="Label5" runat="server" Text="List of Form details :-" Font-Names="Arial" Font-Size="9pt" Font-Underline="True" ForeColor="CornflowerBlue"></asp:Label>
        </td>
        </tr>
       <tr>
        <td colspan="4" style="height: 190px; vertical-align: top;">
           
        <asp:GridView ID="GrdForms" runat="server" BorderColor="#C0C0FF" 
                BorderStyle="Dotted" BorderWidth="0px" CellPadding="3" Font-Names="Arial" 
                Font-Size="8pt" AutoGenerateColumns="False" Width="626px" 
                OnSelectedIndexChanged="GrdUser_SelectedIndexChanged" BackColor="#C0C0FF" 
                DataKeyNames="formNAME" CellSpacing="2" onrowdatabound="GrdForms_RowDataBound">
                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                <RowStyle BackColor="#FFF7E7" ForeColor="#404040" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#C0C0FF" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Content/Images/alert_note.gif" Width="8px" Height="10px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="formID" HeaderText="FNAME" SortExpression="formid" >
                        <ItemStyle Wrap="False" HorizontalAlign="Left"  />
                        <HeaderStyle HorizontalAlign="Left"  />
                    </asp:BoundField>
                    <asp:BoundField DataField="formNAme" HeaderText="Forms Description" SortExpression="formNAme" >
                        <ItemStyle Wrap="False" Width="400px" HorizontalAlign="Left"  />
                        <HeaderStyle HorizontalAlign="Left"  />
                    </asp:BoundField>
                    
                     <asp:TemplateField HeaderText="Enable">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkDisable" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Read Only">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkRead" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                </Columns>
            </asp:GridView>
           
        </td>
        </tr>
        <tr>
        <td align="center"  colspan="4">
            <asp:Button ID="BtnSubmit" runat="server" Height="32px" Text="Confirm Authentication" Width="140px" OnClick="BtnSubmit_Click" BackColor="#FF8080" BorderColor="#FF8080" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="9pt" ForeColor="DarkSlateGray" OnClientClick="return confirm ('Do you want to give the above permission to this user?');" />
            <asp:Button ID="BtnSUB_Exit" runat="server" Height="32px" Text="Close" Width="99px" BackColor="#FF8080" BorderColor="#FF8080" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="9pt" ForeColor="DarkSlateGray" OnClick="BtnSUB_Exit_Click" /></td>
        </tr>
        </table>
            
            </asp:Panel>
    </td>
    </tr>
    <tr>
    <td align="center"  style="background-color: #EFF3FB;">
        <asp:Panel ID="PLNewUser" runat="server" Height="350px" Width="700px">
        <table style="background-color: #EFF3FB; border-bottom: 2px; border-bottom-color: #2461bf; border-bottom-style: solid; border-left: 1px; border-left-color: #2461bf; border-left-style: solid; border-right: 1px; border-right-color: #2461bf; border-right-style: solid; border-top: 1px; border-top-color: #2461bf; border-top-style: solid; width: 310px;">
        <tr style="background-color: #2461BF;">
        <td style="background-color: #2461BF;" colspan="4">
            <asp:Label ID="Label15" runat="server" Text="NEW USER CREATION" Width="202px" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="White"></asp:Label></td>
        </tr>
        <tr>
        <td align="right" style="width: 87px" >
            <asp:Label ID="Label1" runat="server" Text="Employee Name" Font-Names="Arial" 
                Font-Size="9pt" Width="95px"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label3" runat="server" Text=":" Font-Names="Arial" Font-Size="9pt"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtEmpname" runat="server" Font-Names="Arial" Font-Size="9pt" Width="167px"></asp:TextBox>
        </td>
        <td style="width: 11px">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtEmpname"></asp:RequiredFieldValidator>
        </td>
        </tr>
        <tr>
        <td align="right" style="width: 87px" >
            <asp:Label ID="Label7" runat="server" Text="User Name" Font-Names="Arial" Font-Size="9pt" Width="69px"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label8" runat="server" Text=":" Font-Names="Arial" Font-Size="9pt"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtName" runat="server" Font-Names="Arial" Font-Size="9pt" Width="167px"></asp:TextBox>
        </td>
        <td style="width: 11px">
            <asp:RequiredFieldValidator ID="RFV1" runat="server" ErrorMessage="*" ControlToValidate="txtName"></asp:RequiredFieldValidator>
        </td>
        </tr>
        <tr>
        <td align="right"  style="height: 24px; width: 87px;">
            <asp:Label ID="Label9" runat="server" Text="Branch" Font-Names="Arial" Font-Size="9pt" Width="40px"></asp:Label>
        </td>
        <td style="height: 24px">
            <asp:Label ID="Label10" runat="server" Text=":" Font-Names="Arial" Font-Size="9pt"></asp:Label>
        </td>
        <td style="height: 24px">
            <asp:DropDownList ID="drBranch" runat="server" Width="170px">
            </asp:DropDownList></td>
        </tr>
        <tr>
        <td align="right" style="width: 87px" >
            <asp:Label ID="Label11" runat="server" Text="Grade" Font-Names="Arial" Font-Size="9pt" Width="32px"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label12" runat="server" Text=":" Font-Names="Arial" Font-Size="9pt"></asp:Label>
        </td>
        <td><asp:DropDownList ID="drGrade" runat="server" Width="170px">
        </asp:DropDownList></td>
        <td style="width: 11px">
            
        </td>
        </tr>
        <tr>
        <td align="right"  style="width: 87px"><asp:Label ID="Label21" runat="server" Text="Department" Font-Names="Arial" Font-Size="9pt" Width="80px"></asp:Label></td>
        <td><asp:Label ID="Label23" runat="server" Text=":" Font-Names="Arial" Font-Size="9pt"></asp:Label></td>
        <td><asp:DropDownList ID="drDept" runat="server" Width="170px">
                <asp:ListItem Value="0">select</asp:ListItem>
                <asp:ListItem>General</asp:ListItem>
                <asp:ListItem>Accounts</asp:ListItem>
                <asp:ListItem>Operation</asp:ListItem>
            </asp:DropDownList></td>
        </tr>
        <tr>
        <td align="right" style="width: 87px" >
            <asp:Label ID="Label13" runat="server" Text="E-Mail" Font-Names="Arial" Font-Size="9pt" Width="38px"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label14" runat="server" Text=":" Font-Names="Arial" Font-Size="9pt"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtmail" runat="server" Font-Names="Arial" Font-Size="9pt" Width="167px"></asp:TextBox>
        </td>
        </tr>
        <tr>
        <td style="width: 87px"></td><td></td>
        <td align="left">
        <asp:Button ID="BtnNew" runat="server" Height="24px" Text="Create User" Width="78px" OnClick="BtnNew_Click" BackColor="#FF8080" BorderColor="#FF8080" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="9pt" ForeColor="DarkSlateGray" OnClientClick="return confirm ('Do you want to Create New User ?');" />
            <asp:Button ID="BtnNew_Exit" runat="server" Height="23px" Text="Close" Width="74px" BackColor="#FF8080" BorderColor="#FF8080" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="9pt" ForeColor="DarkSlateGray" CausesValidation="False" OnClick="BtnNew_Exit_Click" /></td>
        </tr>
        </table>
           
            <table>
            <tr>
            <td align="left"  colspan="2">
                <asp:Label ID="Label16" runat="server" Text="Grade Note :-" Font-Names="Arial" Font-Size="10pt"></asp:Label>
            </td>
           </tr>
            <tr>
            <td style="width: 34px">
            
            </td>
            <td align="left" >
                <asp:Label ID="Label17" runat="server" Text="A - Grade A has refer to Administrator Authority.." Font-Names="Arial" Font-Size="10pt"></asp:Label><br />
                <asp:Label ID="Label18" runat="server" Text="B - It has allow to the all data for the particular Branch" Font-Names="Arial" Font-Size="10pt"></asp:Label><br />
                <asp:Label ID="Label19" runat="server" Text="C - Allow to the user only" 
                    Font-Names="Arial" Font-Size="10pt"></asp:Label>
                <br />
                <asp:Label ID="Label20" runat="server" Font-Names="Arial" Font-Size="10pt" Text="D- Dashboard User Only"></asp:Label></td>
            </tr>
            </table>
        
        </asp:Panel>
    </td>
    </tr>
    </table>
    
    
        
    </td>
    </tr>
    </table>
        
    </td>
    </tr>
    </table>
    
    </div>
</asp:Content>

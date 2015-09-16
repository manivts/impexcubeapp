<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmChangePassword.aspx.cs" Inherits="ImpexCube.frmChangePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <cc1:RoundedCornersExtender ID="RoundedCornersExtender1" TargetControlID="plMessage" Radius="10" Corners="all" BorderColor="red" runat="server">
        </cc1:RoundedCornersExtender>
        &nbsp; &nbsp;&nbsp;
        <asp:Panel ID="plPass" runat="server" Height="225px" Width="400px" Font-Names="Arial" Font-Size="9pt" GroupingText="PIMPEX Panel">
        
    <table style="background-color:#ccccff;">
    <tr>
    <td rowspan="7" style="background-color: Lavender; width: 7px;" >
        <asp:Image ID="Image1" runat="server" Height="178px" ImageUrl="~/images/Bluthner_Piano.jpg" Width="27px" />
    </td>
    <td align="center" style="background-color: Lavender;"  colspan="4">
        <asp:Label ID="Label1" runat="server" Text="Change Your Password" Font-Names="Tahoma" Font-Size="13pt" BackColor="Lavender"></asp:Label>
    </td>
    <td rowspan="8" style="background-color: Lavender; width: 3px;"><asp:Image ID="Image2" runat="server" Height="178px" ImageUrl="~/images/Bluthner_Piano.jpg" Width="27px" /></td>
    </tr>
    <tr>
    <td align="right">
        <asp:Label ID="Label2" runat="server" Text="User Name" Font-Names="Arial" Font-Size="9pt" Width="81px"></asp:Label>
    </td>
    <td>
        <asp:Label ID="Label3" runat="server" Text=":"></asp:Label>
    </td>
    <td align="left">
        <asp:TextBox ID="txtUser" runat="server" Width="207px"></asp:TextBox>
    </td>
    <td>
        <asp:RequiredFieldValidator ID="RFV1" runat="server" ErrorMessage="*" ControlToValidate="txtUser"></asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr>
    <td align="right">
        <asp:Label ID="Label4" runat="server" Text="Password" Font-Names="Arial" Font-Size="9pt"></asp:Label>
    </td>
    <td>
        <asp:Label ID="Label5" Text=":" runat="server"></asp:Label>
    </td>
    <td align="left" >
        <asp:TextBox ID="txtpwd" runat="server" TextMode="Password" Width="207px"></asp:TextBox>
    </td>
    <td>
        <asp:RequiredFieldValidator ID="RFV2" runat="server" ControlToValidate="txtpwd"
            ErrorMessage="*"></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
    <td align="right">
        <asp:Label ID="Label6" runat="server" Text="New Password" Font-Names="Arial" Font-Size="9pt" Width="87px"></asp:Label>
    </td>
    <td>
        <asp:Label ID="Label7" runat="server" Text=":"></asp:Label>
    </td>
    <td align="left">
        <asp:TextBox ID="txtNewpwd" runat="server" TextMode="Password" Width="207px"></asp:TextBox>
    </td>
    <td>
        <asp:RequiredFieldValidator ID="RFV3" runat="server" ControlToValidate="txtNewpwd"
            ErrorMessage="*"></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
    <td align="right" >
        <asp:Label ID="Label8" runat="server" Text="Confirm New Password" Width="135px" Font-Names="Arial" Font-Size="9pt"></asp:Label>
    </td>
    <td>
        <asp:Label ID="Label9" runat="server" Text=":"></asp:Label>
    </td>
    <td align="left">
        <asp:TextBox ID="txtCNpwd" runat="server" TextMode="Password" Width="207px"></asp:TextBox>
    </td>
    <td>
        <asp:CompareValidator ID="CV1" runat="server" ControlToCompare="txtNewpwd"
            ControlToValidate="txtCNpwd" ErrorMessage="*" Width="12px"></asp:CompareValidator></td>
    </tr>
    <tr>
    <td colspan="4">
        <asp:Label ID="lblError" runat="server" Width="398px" ForeColor="Red" ></asp:Label>
    </td>
    </tr>
    <tr>
    <td style="background-color: Lavender; height: 24px;" align="center" colspan="4" >
        <asp:Button ID="BtnChangepwd" runat="server" Text="Change Password" OnClick="BtnChangepwd_Click" Width="131px" BackColor="White" BorderColor="#8080FF" BorderStyle="Solid" BorderWidth="1px" />
        <asp:Button ID="BtnCancel" runat="server" Text="Cancel" CausesValidation="False" OnClick="BtnCancel_Click" BackColor="White" BorderColor="#8080FF" BorderStyle="Solid" BorderWidth="1px" />
    </td>
    </tr>
    </table>
        </asp:Panel>
        &nbsp;&nbsp;
        <br />
        
        <br />
        
        <table>
        <tr>
        <td style="width: 40px"></td>
        <td align="center" >
        <asp:Panel ID="plMessage" runat="server" Height="50px" Width="450px" BackColor="LemonChiffon">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="10pt" ForeColor="#2461BF"
                            Text="Your Password has Changed Successfully....." Width="369px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" >
                        &nbsp;<asp:LinkButton ID="BtnCl" runat="server" OnClick="BtnCl_Click" Font-Names="Arial" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="White" Width="66px" BackColor="LightPink" BorderColor="Crimson" BorderStyle="Dotted" BorderWidth="1px" Font-Bold="True">Close</asp:LinkButton></td>
                </tr>
    </table>
    </asp:Panel>
        </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="ImpexCube.frmLogin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="Content/Styles/StandardTool.css" rel="stylesheet" type="text/css" />
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>::Login</title>
    <script language="javascript" type="text/javascript">
        window.history.forward(1);  
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <cc1:RoundedCornersExtender ID="RCE1" runat="server" Corners="All" Radius="10" TargetControlID="Panel1"
            BorderColor="#0099ff">
        </cc1:RoundedCornersExtender>
        <cc1:RoundedCornersExtender ID="RCE2" runat="server" Corners="All" Radius="10" TargetControlID="Panel2"
            BorderColor="#0099ff">
        </cc1:RoundedCornersExtender>
        <cc1:RoundedCornersExtender ID="RCE3" runat="server" Corners="All" Radius="10" TargetControlID="Panel3"
            BorderColor="#0099ff">
        </cc1:RoundedCornersExtender>
        <table style="width: 100%;">
            <tr>
                <td align="right" style="width: 99%">
                    <asp:Panel ID="Panel3" BackColor="#0099ff" runat="server" Height="20px" Width="100%">
                        <asp:Label ID="lblShortName1" runat="server" Font-Bold="True" Font-Names="Castellar"
                            Font-Overline="False" Font-Size="13pt" ForeColor="White" Style="margin-right: 0px"
                            Text="" Width="97%"></asp:Label>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="Panel1" runat="server" Height="500px" Width="99%">
                        <table style="width: 100%; height: 100%; vertical-align: middle;">
                            <tr>
                                <td style="width: 45%; height: 97%; vertical-align: top;">
                                    <br />
                                    <br />
                                    <asp:Image ID="Image3" runat="server" Height="373px" ImageUrl="~/Content/Images/homepagegraphic.jpg"
                                        Width="362px" />
                                </td>
                                <td style="height: 97%; vertical-align: middle; width: 55%;">
                                    <table>
                                        <tr>
                                            <td align="center" style="width: 30%; height: 333px;">
                                                <asp:Label ID="lblShortName" runat="server" Text="" Width="195px" Font-Names="Verdana"
                                                    Font-Size="9pt" Font-Bold="True" ForeColor="#2461BF"></asp:Label>
                                            </td>
                                            <td style="height: 333px; width: 70%">
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <asp:Panel ID="Panel2" runat="server" Height="220px" Width="368px">
                                                    <table style="width: 368px;">
                                                        <tr>
                                                            <td align="center" colspan="2">
                                                                <asp:Label ID="lblshortname2" runat="server" Font-Bold="True" Font-Names="Verdana"
                                                                    Font-Size="9pt" ForeColor="OrangeRed"></asp:Label>
                                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="9pt"
                                                                    Text="Account" ForeColor="OrangeRed"></asp:Label>
                                                                <br />
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label7" runat="server" Text="User Name :" Width="79px" Font-Names="Verdana"
                                                                    Font-Size="9pt" ForeColor="#2461BF"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtUser" runat="server" Width="144px" Font-Names="Arial" Font-Size="9pt">admin</asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label8" runat="server" Text="Password :" Font-Names="Verdana" Font-Size="9pt"
                                                                    ForeColor="#2461BF"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPassword" runat="server" Width="143px" TextMode="Password" Font-Names="Arial"
                                                                    Font-Size="9pt">solutions</asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label20" runat="server" Text="Branch :" Font-Names="Verdana" Font-Size="9pt"
                                                                    ForeColor="#2461BF"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="drBranch" runat="server" Width="150px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" style="vertical-align: top;">
                                                                <asp:Label ID="Label13" runat="server" Text="Financial Year :" Font-Names="Verdana"
                                                                    Font-Size="9pt" ForeColor="#2461BF" Width="103px"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="drFinancial" runat="server" Width="150px">
                                                                </asp:DropDownList>
                                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="drFinancial"
                                                                    ErrorMessage="* Select Financial Year" Font-Names="Arial" Font-Size="8pt" Operator="GreaterThan"
                                                                    ValueToCompare="0"></asp:CompareValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td align="left">
                                                                <asp:Button ID="BtnSubmit" OnClick="BtnSubmit_Click" runat="server" Font-Size="8pt"
                                                                    Font-Names="Arial" Text="Sign in" Width="81px" Height="28px"></asp:Button>&nbsp;
                                                                <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="8pt" Font-Names="Arial"
                                                                    Width="72px" OnClick="BtnExit_Click" Height="28px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td align="left" style="text-align: right">
                                                                <asp:LinkButton ID="lbChangePassword" runat="server" OnClick="lbChangePassword_Click"
                                                                    Style="font-size: small; font-family: Verdana">Change Password</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <asp:Panel ID="Panel5" runat="server" Visible="false">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblLoginDetails" runat="server" Font-Bold="True" 
                                                                    Font-Italic="False" Font-Names="Cambria" Font-Overline="False" 
                                                                    ForeColor="#660066"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                        <td>
                                                            <asp:Button ID="btnrelogin" runat="server" Text="Re login" 
                                                                onclick="btnrelogin_Click" CssClass="stylebutton" />
                                                            <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
                                                                Text="Cancel" CssClass="stylebutton" />
                                                        </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
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

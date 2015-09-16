<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="ImpexCube.ChangePassword" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:RoundedCornersExtender ID="RoundedCornersExtender1" TargetControlID="plMessage"
        Radius="10" Corners="All" BorderColor="Red" runat="server">
    </cc1:RoundedCornersExtender>
    &nbsp; &nbsp;&nbsp;
    <asp:Panel ID="plPass" runat="server" Height="225px" Width="475px" Font-Names="Arial"
        Font-Size="9pt" GroupingText="User Panel">
        <table style="background-color: #ccccff;">
            <tr>
                <td rowspan="7" style="background-color: Silver; width: 7px;">
                    <asp:Image ID="Image1" runat="server" Height="178px" ImageUrl="~/Image/Bluthner_Piano.jpg"
                        Width="27px" />
                </td>
                <td align="center" style="background-color: Silver;" colspan="4">
                    <asp:Label ID="Label1" runat="server" Text="Change Your Password" Font-Names="Tahoma"
                        Font-Size="13pt" BackColor="Silver"></asp:Label>
                </td>
                <td rowspan="8" style="background-color: Silver; width: 3px;">
                    <asp:Image ID="Image2" runat="server" Height="178px" ImageUrl="~/Image/Bluthner_Piano.jpg"
                        Width="27px" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label2" runat="server" Text="Old Password" Font-Names="Arial" Font-Size="9pt"
                        Width="81px"></asp:Label>
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
                    <asp:Label ID="Label6" runat="server" Text="New Password" Font-Names="Arial" Font-Size="9pt"
                        Width="87px"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label7" runat="server" Text=":"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtNewpwd" runat="server" TextMode="Password" Width="207px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RFV3" runat="server" ControlToValidate="txtNewpwd"
                        ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label8" runat="server" Text="Confirm New Password" Width="135px" Font-Names="Arial"
                        Font-Size="9pt"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label9" runat="server" Text=":"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtCNpwd" runat="server" TextMode="Password" Width="207px"></asp:TextBox>
                </td>
                <td>
                    <asp:CompareValidator ID="CV1" runat="server" ControlToCompare="txtNewpwd" ControlToValidate="txtCNpwd"
                        ErrorMessage="*" Width="12px"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblError" runat="server" Width="398px" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="background-color: Silver; height: 24px;" align="center" colspan="4">
                    <asp:Button ID="BtnChangepwd" runat="server" Text="Change Password" Width="131px"
                        BackColor="White" BorderColor="#8080FF" BorderStyle="Solid" BorderWidth="1px"
                        OnClick="BtnChangepwd_Click" />
                    <asp:Button ID="BtnCancel" runat="server" Text="Cancel" CausesValidation="False"
                        BackColor="White" BorderColor="#8080FF" BorderStyle="Solid" BorderWidth="1px"
                        OnClick="BtnCancel_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    &nbsp;&nbsp;
    <br />
    <br />
    <table>
        <tr>
            <td style="width: 40px">
            </td>
            <td align="center">
                <asp:Panel ID="plMessage" runat="server" Height="50px" Width="450px" BackColor="LemonChiffon">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="10pt" ForeColor="#2461BF"
                                    Text="Your Password has Changed Successfully....." Width="369px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                &nbsp;<asp:LinkButton ID="BtnCl" runat="server" Font-Names="Arial" Font-Size="9pt"
                                    Font-Strikeout="False" Font-Underline="False" ForeColor="White" Width="66px"
                                    BackColor="LightPink" BorderColor="Crimson" BorderStyle="Dotted" BorderWidth="1px"
                                    Font-Bold="False">Close</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>

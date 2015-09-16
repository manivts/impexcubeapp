<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="CSVLoad.aspx.cs" Inherits="ImpexCube.CSVLoad" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .modalPopup
        {
            background-color: #696969;
            filter: alpha(opacity=40);
            opacity: 0.8;
            xindex: -1;
        }
    </style>
    <script type="text/javascript">
        function pageLoad(sender, args) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (!prm.get_isInAsyncPostBack()) {
                prm.add_beginRequest(BeginRequestHandler);
                prm.add_endRequest(EndRequestHandler);
            }
        }
        function BeginRequestHandler(sender, args) {
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.show();
            }
        }
        function EndRequestHandler(sender, args) {
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.hide();
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress1"
        PopupControlID="UpdateProgress1" BackgroundCssClass="modalPopup" />
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <img alt="Please Wait...." src="Content/Images/wait.gif" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div>
        <table style="width: 68%;" align="center">
            <tr>
                <td class="fontsize">
                    Job No
                </td>
                <td>
                    <asp:DropDownList ID="ddlJobNo" runat="server" Width="160px" CssClass="ddl100">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="fontsize">
                    File Name
                </td>
                <td>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="tx4" />
                            <asp:Button ID="btnRead" runat="server" OnClick="btnRead_Click" Text="Read" 
                                CssClass="stylebutton" /><asp:Label
                                ID="lblMsg" runat="server" Text=""></asp:Label>
                            <asp:DropDownList ID="drfile" runat="server" AutoPostBack="True" 
                                Visible="False" CssClass="ddl100">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnRead" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: center">
                 <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                        <ContentTemplate>
                    <asp:Button ID="btnLoad" runat="server" Text="Load File" Width="70px" 
                                OnClick="btnLoad_Click" CssClass="stylebutton" />
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

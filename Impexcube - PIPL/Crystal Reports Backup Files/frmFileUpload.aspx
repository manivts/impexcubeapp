<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmFileUpload.aspx.cs" Inherits="ImpexCube.frmFileUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function preventBackspace(e) {
            var evt = e || window.event;
            if (evt) {
                var keyCode = evt.charCode || evt.keyCode;
                if (keyCode === 8) {
                    if (evt.preventDefault) {
                        evt.preventDefault();
                    } else {
                        evt.returnValue = false;
                    }
                }
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        function NewJobCreationDetails() {
            window.open('ffDocumentnew.aspx', '_blank', 'width=800,height=280,titlebar=no, menubar=no, scrollbars=yes,header=no toolbar=no, location=no, resizable=no, left=180, top=200');
            false;
        }

        function ViewJobCreationDetails() {
            window.open('ffDocumentview.aspx', '_blank', 'width=800,height=280,titlebar=no, menubar=no, scrollbars=yes,header=no toolbar=no, location=no, resizable=no, left=180, top=200');
            false;
        }
    </script>
    <style type="text/css">
        .completionList
        {
            border: solid 1px #444444;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
            font-family: verdana;
            font-size: 10px;
            background-color: white;
        }
        .listItem
        {
            color: #666666;
            background-color: white;
            font-family: verdana;
            font-size: 10px;
        }
        .itemHighlighted
        {
            background-color: #ffc0c0;
        }
        .styleLabel
        {
            font-size: 8pt;
        }
        .cpHeader
        {
            cursor: pointer;
        }
        .cpBody
        {
            background-color: #DCE4F9;
            font: normal 11px auto Verdana, Arial;
            border: 1px gray;
            width: 450px;
            padding: 4px;
            padding-top: 7px;
        }
        .FontSize
        {
            font-family: Arial;
            font-size: 8pt;
        }
    </style>
    <style type="text/css">
        .BackColorTab
        {
            font-family: Verdana, Arial, Courier New;
            font-size: 10px;
            color: Gray;
            background-color: Silver;
        }
        .button
        {
            background: #073088;
            font-family: Trebuchet MS;
            font-size: 12px;
            font-weight: bold;
            color: #FFFFFF;
        }
        .button:hover
        {
            background: #deefef;
            border: solid 1px grey;
            font-family: Arial, sans-serif;
            font-size: 12px;
            font-weight: bold;
            color: Red;
            height: 25px;
        }
        .button1
        {
            background: #073088;
            border: solid 1px grey;
            font-family: Trebuchet MS;
            font-size: 12px;
            font-weight: bold;
            color: Red;
            height: 25px;
        }
        .button1:hover
        {
            background: #deefef;
            border: solid 1px grey;
            font-family: Arial, sans-serif;
            font-size: 12px;
            font-weight: bold;
            color: Red;
            height: 25px;
        }
        .buttonsmall
        {
            background: #073088;
            border: solid 1px grey;
            font-family: Trebuchet MS;
            font-size: 12px;
            font-weight: bold;
            color: #FFFFFF;
            height: 25px;
        }
        .buttonsmall:hover
        {
            background: white;
            border: solid 1px grey;
            font-family: Arial, sans-serif;
            font-size: 12px;
            font-weight: bold;
            color: Red;
            height: 25px;
        }
        .grid_scrollg
        {
            overflow: auto;
            border: solid 1px white;
            height: 300px;
            width: 450px;
        }
        .buttonclick
        {
            background: blue;
            border: solid 1px grey;
            font-family: Arial, sans-serif;
            font-size: 12px;
            font-weight: bold;
            color: Red;
            height: 25px;
        }
        #doid
        {
            height: 446px;
        }
        #frame1
        {
            margin-left: 47px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="float: left; width: 500px">
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Job No"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlJobNo" runat="server" Width="220px" AppendDataBoundItems="True"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
        
        </table>
    </div>
</asp:Content>

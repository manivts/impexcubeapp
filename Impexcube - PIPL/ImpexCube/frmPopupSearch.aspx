<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPopupSearch.aspx.cs"
    Inherits="ImpexCube.frmPopupSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/Styles/StandardTool.css" rel="stylesheet" type="text/css" />
    <%--  <script type="text/javascript">
//        window.onunload = function () {
//            window.opener.location.reload(true);
//        };
//        function popupClosing() {
//            alert('About to refresh');
//            window.location.href = window.location.href;
//            alert(window.location.href);
//        }

//        var w = window.open("frmPrintCheckList.aspx", "_blank", "toolbar=yes, location=yes, directories=no, status=no, menubar=yes, scrollbars=yes, resizable=no, copyhistory=yes, width=400, height=400");
//        w.onunload = function () {
//            window.parent.popupClosing()
//        };
//        function RefreshParent() {
//            alert('About to refresh');
//            window.location.href = window.location.href;

////            if (window.opener != null && !window.opener.closed) {
////                window.onbeforeunload = RefreshParent;
////                window.opener.location.reload();
////                
////                window.close();
////                
////                return true;
////            }
//        }
//        
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 30%; text-align: center;">
            <tr>
                <td colspan="3">
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    Search
                </td>
                <td class="tdcolumn150">
                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: center">
                    <asp:GridView ID="gvSearch" runat="server" CssClass="gridview" BorderColor="#3366CC"
                        CellPadding="4" Font-Names="Arial" Font-Size="8pt" AutoGenerateSelectButton="True"
                        OnSelectedIndexChanged="gvSearch_SelectedIndexChanged">
                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <RowStyle BackColor="White" ForeColor="#003399" />
                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#073088" Font-Bold="True" ForeColor="#CCCCFF" Font-Size="8pt" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

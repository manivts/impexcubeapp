<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmProductPopup.aspx.cs" Inherits="ImpexCube.frmProductPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Details</title>
    <script type="text/javascript">
        function GetProduct(productName) {
            window.opener.document.forms[0].ContentPlaceHolder1_txtpro.value = productName;
//            alert("Test 1");
//            window.opener.document.forms[0].ContentPlaceHolder1_txtRITC.value = RITCCode;
//            alert("Test 2");
//            window.opener.document.forms[0].ContentPlaceHolder1_txtCTH.value = CTHNo;
            window.close();
        }
        function GetProductExp(productName) {
            window.opener.document.forms[0].ContentPlaceHolder1_txtDesc.value = productName;

            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table width="500">
            <tr>
                <td style="text-align: center; font-weight: 700;" colspan="3">
                    <asp:Label ID="Label2" runat="server" Text="Product Details"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="130">
                    <asp:Label ID="Label1" runat="server" Text="Product Name"></asp:Label>
                </td>
                <td width="300">
                    <asp:TextBox ID="txtSearch" runat="server" Width="300px"></asp:TextBox>
                </td>
                <td width="70">
                    <asp:Button ID="btnSearch" runat="server" onclick="btnSearch_Click" 
                        Text="Search" Width="70px" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:GridView ID="GridView1"  runat="server" Width="500px" 
                        AutoGenerateSelectButton="true" AllowPaging="True" 
                        onselectedindexchanged="GridView1_SelectedIndexChanged" 
                        style="font-size: 8pt" onpageindexchanging="GridView1_PageIndexChanging">
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

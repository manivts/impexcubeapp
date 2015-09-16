<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmJobSync.aspx.cs" Inherits="ImpexCube.frmJobSync" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <title>Job </title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link type="text/css" href="css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="js/New Folder/jquery-1.5.2.min.js"></script>
    <script type="text/javascript" src="js/New Folder/scriptbreaker-multiple-accordion-1.js"></script>
    <script language="JavaScript">

        $(document).ready(function () {
            $(".topnav").accordion({
                accordion: false,
                speed: 500,
                closedSign: '',
                openedSign: ''
            });
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <!--top-area-->
    <div id="top">
        <div class="top-area">
            <div class="top-area-left">
                Impex Cube</div>
            <div class="top-area-right">
                <a href="HomePage.aspx" style="color: #666666; text-decoration: none;">HOME</a>
            </div>
        </div>
    </div>
    <!--topareaEnd-->
    <!--continerarea-->
    <div id="container">
        <div class="container-area">
            <div id="col-1">
                <div style="width: 120px; margin: 0; background: #FFFFFF; padding: 0px;">
                    <ul class="topnav">
                                         
                        <li><a href="frmJobSync.aspx">Settings</a></li>
                    </ul>
                </div>
            </div>
            <div id="col-2">
                <div class="menu-list">
                    <div class="col-area">
                        <div class="c-abutton">
                            <div style="float:none; text-align: center;">
                                <asp:Button ID="btnJobSync" runat="server" Text="Job Sync" CssClass="orange" 
                                    onclick="btnJobSync_Click">
                                </asp:Button>                                
                            </div>                            
                        </div>
                    </div>
                </div>
            </div>
            <div id="col-3">
            </div>
        </div>
    </div>
    <!--containerareaend-->
    <div style="clear: both;">
    </div>
    <div id="footer">
        <div class="footer-area">
            <div class="footer-copy">
                Copy Rights.All Rightd reserved
            </div>
            <div class="footer-copy1">
                <img src="image/spade.png" width="24" height="24" alt="s"></div>
            <div class="footer-copy2">
                <img src="image/heart.png" width="24" height="24" alt="h"></div>
            <div class="footer-copy3">
                <img src="image/1367349924_Diamonds_card.png" width="24" height="24" alt="d"></div>
            <div class="footer-copy4">
                <img src="image/1367349970_clover.png" width="24" height="24" alt="b"></div>
        </div>
    </div>
    </form>
</body>
</html>

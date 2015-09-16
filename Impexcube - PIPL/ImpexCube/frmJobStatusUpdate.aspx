<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmJobStatusUpdate.aspx.cs" Inherits="ImpexCube.frmJobStatusUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
  <link href="Content/Styles/StandardTool.css" rel="stylesheet" type="text/css" />
    <link href="Content/Styles/MasterStyle.css" rel="stylesheet" type="text/css" />
    <link href="Content/Styles/MenuStyle.css" rel="stylesheet" type="text/css" />
    <script src="Content/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Content/Scripts/MenuScript.js" type="text/javascript"></script>
  <script type="text/javascript">
      function ValidateText() {
          if (document.getElementById('<%= txtStatusDate.ClientID %>').value == "") {
              alert('Please fill the status date');
              return false;
          }
          //if (document.getElementById('<%= txtRemarks.ClientID %>').value == "") {
          //alert('Please fill the remarks');
          //return false;
          //}
      }
      function SendAttach() {
          var theApp   //Reference to Outlook.Application
          var theMailItem      //Outlook.mailItem
          //Attach Files to the email
          var attach1 = document.getElementById("<%=myfile.ClientID %>").value;
          //Construct the Email including To(address),subject,body
          //var recipient
          var subject = document.getElementById("<%=hdnSubject.ClientID %>").value;
          var msg = document.getElementById("<%=hdnComment.ClientID %>").value;
          var to = document.getElementById('<%=hdnTo.ClientID %>').value;
          var CC = document.getElementById('<%=hdnCC.ClientID %>').value;

          //Create a object of Outlook.Application
          try {
              var theApp = new ActiveXObject('Outlook.Application')
              var theMailItem = theApp.CreateItem(0) // value 0 = MailItem
              //Bind the variables with the email
              theMailItem.to = to;
              theMailItem.cc = CC;
              theMailItem.Subject = (subject);
              theMailItem.Body = (msg);
              theMailItem.Attachments.Add(attach1)
              theMailItem.send()
              //Show the mail before sending for review purpose
              //You can directly use the theMailItem.send() function
              //if you do not want to show the message.
              //theMailItem.display()
          }
          catch (err) {
              alert(err);
          }
      }
    </script>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5″&gt" />


    <form id="form1" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="container">
        <div class="container-area">
            <div id="col-2">
                <div class="d">
                    <div class="c-al">
                        <div class="Col-title">
                            Job Stage Update</div>
                    </div>
                    <div class="c-a">
                        <div class="content-work-increase">
                            <div class="c-s-b1">
                                Job No :</div>
                            <div class="c-s-b2fixeda">
                                <asp:Label ID="txtJobNo" runat="server" CssClass="arealaber"></asp:Label>
                            </div>
                        </div>
                        <div class="content-work-box1we">
                            <div class="c-s-b1">
                                &nbsp;Date :</div>
                            <div class="c-s-b2we">
                                <asp:Label ID="txtJobDate" runat="server" TabIndex="2" CssClass="arealaber" ReadOnly="true"></asp:Label>
                                <%-- <cc1:CalendarExtender ID="ceTo" runat="server" Format="dd/MM/yyyy" TargetControlID="txtJobDate">
                                </cc1:CalendarExtender>--%>
                            </div>
                        </div>
                    </div>
                    <div class="c-a">
                        <div class="content-work-increase">
                            <div class="c-s-b1">
                                Party Name :</div>
                            <div class="c-s-b2fixeda">
                                <asp:Label ID="txtImporter" runat="server" CssClass="arealaber1" TabIndex="3" ReadOnly="true"></asp:Label>
                            </div>
                        </div>
                        <div class="content-work-box1we">
                            <div class="c-s-b1">
                                Shipment Type :</div>
                            <div class="c-s-b2we">
                                <asp:Label ID="txtShipmentType" runat="server" CssClass="arealaber" TabIndex="4"
                                    ReadOnly="true"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <%--<div class="c-a">
                        <div class="content-work-increase">
                            <div class="c-s-b1">
                                &nbsp;Inv Details :</div>
                            <div class="c-s-b2fixeda">
                                <asp:Label ID="txtInvoice" runat="server" CssClass="arealaber" TabIndex="5" ReadOnly="true"></asp:Label>
                            </div>
                        </div>
                        <div class="content-work-box1we">
                            <div class="c-s-b1">
                                BE Type :</div>
                            <div class="c-s-b2we">
                                <asp:Label ID="txtBEType" runat="server" CssClass="arealaber" TabIndex="6" ReadOnly="true"></asp:Label>
                            </div>
                        </div>
                    </div>--%>
                    <div class="c-a">
                        <div class="content-work-increase">
                            <div class="c-s-b1">
                                &nbsp;BE No :</div>
                            <div class="c-s-b2fixeda">
                                <asp:TextBox ID="txtBENo" CssClass="textbox150" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="content-work-box1we">
                            <div class="c-s-b1">
                                BE Date :</div>
                            <div class="c-s-b2we">
                                <asp:TextBox ID="txtBEDate" CssClass="textbox150" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtBEDate">
                                </cc1:CalendarExtender>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="menu-list">
                    <div class="c-a1">
                        <div class="m-l1">
                            Job Stage</div>
                        <div class="m-l3">
                            Status</div>
                        <div class="m-l2">
                            Remarks</div>
                        <div class="m-l1">
                            Status Date</div>
                        <div class="m-l31">
                            Mail</div>
                        <div class="m-l312">
                            Add</div>
                    </div>
                </div>
                <div class="menu-list">
                    <div class="c-as">
                        <div class="m-l1a">
                            <asp:DropDownList ID="ddlJobStages" runat="server" CssClass="span3v required" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlJobStages_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="m-l3a">
                            <asp:DropDownList ID="ddlJobStageStatus" runat="server" CssClass="span3v required">
                            </asp:DropDownList>
                        </div>
                        <div class="m-l2a">
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="postmsgg234 required" 
                                Height="40px"></asp:TextBox>
                        </div>
                        <div class="m-l1a">
                            <asp:TextBox ID="txtStatusDate" runat="server" CssClass="postmsgg236 required" OnKeyPress="javascript:return false;"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtStatusDate">
                            </cc1:CalendarExtender>
                        </div>
                        <div class="m-l3a1">
                            <asp:ImageButton ID="btnMail" runat="server" 
                                ImageUrl="~/Content/Images/sendmail.png" OnClick="btnMail_Click" />
                        </div>
                        <div class="m-l4a1">
                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Content/Images/Add.jpg" OnClick="btnAdd_Click"
                                Style="height: 24px" OnClientClick="javascript:return ValidateText();" />
                            <asp:ImageButton ID="btnUpdate" runat="server" 
                                ImageUrl="~/Content/Images/Add.jpg" Style="height: 24px"
                                OnClick="btnUpdate_Click" 
                                OnClientClick="javascript:return ValidateText();" />
                        </div>
                    </div>
                    <div class="d">
                        <asp:GridView ID="gvJobStageStatus" runat="server" Style="text-align: center; margin-top:10px" GridLines="Vertical"
                            CssClass="table-wrapper" AutoGenerateColumns="False" OnSelectedIndexChanged="gvJobStageStatus_SelectedIndexChanged"
                            Width="900px">
                            <RowStyle CssClass="table-header light" />
                            <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" ForeColor="#EE2521" />
                            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                            <AlternatingRowStyle BackColor="#E7E7FF" />
                            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" HeaderText="Select" />
                                <asp:BoundField DataField="Id" HeaderText="Sl.No" ItemStyle-CssClass="hiddencol"
                                    HeaderStyle-CssClass="hiddencol" />
                                <asp:BoundField DataField="Stage" HeaderText="Job Stage" />
                                <asp:BoundField DataField="Status" HeaderText="Job Status" />
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                <asp:BoundField DataField="Status Date" HeaderText="Status Date" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <br />
                    <div class="col-area">
                        <div style="clear: both;">
                        </div>
                        <div class="c-abutton">
                            <div style="float: right;">
                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="orange" OnClick="btnClose_Click" />
                            </div>
                        </div>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div class="col-area">
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div class="col-area">
                    </div>
                </div>
            </div>
            <div id="col-3">
                <div class="c-alr">
                    <div class="Col-title">
                        Job Status Mail
                    </div>
                </div>
                <div class="col3-row">
                    <div class="col3-Contentlabel">
                        From
                    </div>
                    <div class="col3-ContentText">
                        <asp:TextBox ID="txtFrom" runat="server" CssClass="col3-text required"></asp:TextBox>
                    </div>
                </div>
                <div class="col3-row">
                    <div class="col3-Contentlabel">
                        To
                    </div>
                    <div class="col3-ContentText">
                        <asp:TextBox ID="txtTo" runat="server" CssClass="col3-text required"></asp:TextBox>
                    </div>
                </div>
                <div class="col3-row">
                    <div class="col3-Contentlabel">
                        CC
                    </div>
                    <div class="col3-ContentText">
                        <asp:TextBox ID="txtCC" runat="server" CssClass="col3-text required"></asp:TextBox>
                    </div>
                </div>
                <div class="col3-row">
                    <div class="col3-Contentlabel">
                        Subject
                    </div>
                    <div class="col3-ContentText">
                        <asp:TextBox ID="txtSubject" runat="server" CssClass="col3-text required"></asp:TextBox>
                    </div>
                </div>
                <div class="col3-row">
                    <div class="col3-ContentText">
                        <asp:FileUpload ID="fuAttach" runat="server" />
                    </div>
                </div>
                <div class="col3-rowa">
                    <div class="co3-button1a">
                        <asp:Button ID="btnAttach" CssClass="orange-g" runat="server" Text="Attach" OnClick="btnAttach_Click" />
                    </div>
                </div>
                <div class="col3-row">
                    <div class="col3-Contentlabel">
                        Attach
                    </div>
                    <div class="col3-ContentText">
                        <asp:TextBox ID="txtAttach" runat="server" CssClass="col3-text required" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="col3-rowex">
                    <div class="col3-Contentlabel">
                        Comment
                    </div>
                    <div class="col3-ContentTextex">
                        <asp:TextBox ID="txtComment" runat="server" CssClass="col3-textcomment required"
                            TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                <div class="col3-row">
                    <div class="co3-button1">
                        <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="blue" OnClick="btnSend_Click"
                            Enabled="false"></asp:Button>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="blue" OnClick="btnCancel_Click">
                        </asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="myfile" runat="server" />
    <input type="hidden" id="hdnSubject" runat="server" />
    <input type="hidden" id="hdnComment" runat="server" />
    <input type="hidden" id="hdnTo" runat="server" />
    <input type="hidden" id="hdnCC" runat="server" />

    </form>
</body>
</html>

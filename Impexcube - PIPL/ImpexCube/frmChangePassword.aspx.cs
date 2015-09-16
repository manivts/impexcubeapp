using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using VTS.ImpexCube.Business;
namespace ImpexCube
{
    public partial class frmChangePassword : System.Web.UI.Page
    {

        VTS.ImpexCube.Business.pChangePassBL ba = new VTS.ImpexCube.Business.pChangePassBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                plMessage.Visible = false;
            }

        }
        protected void BtnChangepwd_Click(object sender, EventArgs e)
        {
            string userName = txtUser.Text;
            string pwd = txtpwd.Text;
            string NewPwd = txtCNpwd.Text;
            string dPass = "";

            DataSet dsData = new DataSet();
            dsData = ba.GetData(userName);

            DataTable dt = dsData.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                dPass = row["empid"].ToString();
                Session["oldPass"] = dPass;
            }
            string val = dPass;
            //if (val.Length > 7)
            //{
            //    val = val.Remove(0, 7);
            //    dPass = EcnryptionTest.base64Decode(val);
            //}
            if (pwd != dPass)
            {
                //lblError.Text = "* InCorrect User Name & Password";
                Response.Write("<script>alert('Invalid UserName And Password');window.open('frmChangePassword.aspx', '_blank','width=510,height=300, menubar=no, scrollbars=no, toolbar=no, location=no, resizable=no, left=400, top=280');</script>");
            }
            else
            {

                try
                {
                    pwd = (string)Session["oldPass"];
                    //Ecnryption password to store database
                    //string Eval = NewPwd;
                    //string pass = EcnryptionTest.base64Encode(Eval);
                    //Int32 i = 7;
                    //string pss = RandomPassword.CreateRandomPassword(i);
                    //NewPwd = pss + pass;

                    int res = ba.updataPassword(NewPwd, userName, pwd);

                    plMessage.Visible = true;
                    plPass.Visible = false;
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "')</script>");
                }

            }
        }
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
           // Response.Write("<script>window.close();</script>");
            Response.Redirect("frmLogin.aspx");
        }
        protected void BtnCl_Click(object sender, EventArgs e)
        {
            //Response.Write("<script>window.close();</script>");
            Response.Redirect("frmLogin.aspx");
        }
    }
}
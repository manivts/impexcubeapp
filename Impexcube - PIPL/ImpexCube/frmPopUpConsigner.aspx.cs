using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using VTS.ImpexCube.Business;
namespace ImpexCube.Popup
{
    public partial class frmPopUpConsigner : System.Web.UI.Page
    {
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        VTS.ImpexCube.Business.CommonBL obj1 = new VTS.ImpexCube.Business.CommonBL();
 
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        //protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        //{
        //    string mode = Request.QueryString["mode"];
        //    DataSet ds = obj1.GetConsignorPopup(txtSearch.Text, mode);
        //    GridView1.DataSource = ds;
        //    GridView1.DataBind();
        //}

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mode = Request.QueryString["mode"];
            if (mode == "agent")
            {
                string ConsName = GridView1.SelectedRow.Cells[2].Text;
                string ConsAdd = GridView1.SelectedRow.Cells[3].Text;
                string ConCountry = GridView1.SelectedRow.Cells[4].Text;

                ConsName = ConsName.Replace("&nbsp;", "");
                ConsAdd = ConsAdd.Replace("&nbsp;", "");
                ConCountry = ConCountry.Replace("&nbsp;", "");

                ClientScript.RegisterStartupScript(typeof(GridView), "JavaScript", "GetProduct('" + ConsName + "','" + ConsAdd + "','" + ConCountry + "','"+ mode +"');", true);
            }
            else if (mode == "seller")
            {
                string ConsName = GridView1.SelectedRow.Cells[2].Text;
                string ConsAdd = GridView1.SelectedRow.Cells[3].Text;
                string ConCountry = GridView1.SelectedRow.Cells[4].Text;

                ConsName = ConsName.Replace("&nbsp;", "");
                ConsAdd = ConsAdd.Replace("&nbsp;", "");
                ConCountry = ConCountry.Replace("&nbsp;", "");

                ClientScript.RegisterStartupScript(typeof(GridView), "JavaScript", "GetProduct('" + ConsName + "','" + ConsAdd + "','" + ConCountry + "','" + mode + "');", true);
            }
            else if (mode == "cnsr")
            {
                string ConsName = GridView1.SelectedRow.Cells[2].Text;
                string ConsAdd = GridView1.SelectedRow.Cells[3].Text;
                string ConCountry = GridView1.SelectedRow.Cells[4].Text;

                ConsName = ConsName.Replace("&nbsp;", "");
                ConsAdd = ConsAdd.Replace("&nbsp;", "");
                ConCountry = ConCountry.Replace("&nbsp;", "");

                ClientScript.RegisterStartupScript(typeof(GridView), "JavaScript", "GetProduct('" + ConsName + "','" + ConsAdd + "','" + ConCountry + "','" + mode + "');", true);
            }
            else if (mode == "imp")
            {
                // PartyCode, PartyName, IeCodeNo,  BranchSno, Address,City,State, ZipCode,CommericalTaxState, CommericalTaxType, CommericalTaxRegnNo
                string PartyName =GridView1.SelectedRow.Cells[2].Text;
                string IeCodeNo = GridView1.SelectedRow.Cells[3].Text;
                string BranchSno = GridView1.SelectedRow.Cells[4].Text;
                string Address = GridView1.SelectedRow.Cells[5].Text;
                string City = GridView1.SelectedRow.Cells[6].Text;
                string State = GridView1.SelectedRow.Cells[7].Text;
                string ZipCode = GridView1.SelectedRow.Cells[8].Text;
                string CommericalTaxState = GridView1.SelectedRow.Cells[9].Text;
                string CommericalTaxType = GridView1.SelectedRow.Cells[10].Text;
                string CommericalTaxRegnNo = GridView1.SelectedRow.Cells[11].Text;

                PartyName = PartyName.Replace("&nbsp;", "");
                IeCodeNo = IeCodeNo.Replace("&nbsp;", "");
                BranchSno = BranchSno.Replace("&nbsp;", "");
                Address = Address.Replace("&nbsp;", "");
                City = City.Replace("&nbsp;", "");
                State = State.Replace("&nbsp;", "");
                ZipCode = ZipCode.Replace("&nbsp;", "");

                CommericalTaxState = CommericalTaxState.Replace("&nbsp;", "");
                CommericalTaxType = CommericalTaxType.Replace("&nbsp;", "");
                CommericalTaxRegnNo = CommericalTaxRegnNo.Replace("&nbsp;", "");
                ClientScript.RegisterStartupScript(typeof(GridView), "JavaScript", "GetImporter('" + PartyName + "','" + IeCodeNo + "','" + BranchSno + "','" + Address + "','" + City + "','" + State + "','" + ZipCode + "','" + CommericalTaxState + "','" + CommericalTaxType + "','" + CommericalTaxRegnNo + "','" + mode + "');", true);
            }
            else if (mode == "high")
            {
                // PartyCode, PartyName, IeCodeNo,  BranchSno, Address,City,State, ZipCode,CommericalTaxState, CommericalTaxType, CommericalTaxRegnNo
                string PartyName =GridView1.SelectedRow.Cells[2].Text;
                string IeCodeNo = GridView1.SelectedRow.Cells[3].Text;
                string BranchSno = GridView1.SelectedRow.Cells[4].Text;
                string Address = GridView1.SelectedRow.Cells[5].Text;
                string City = GridView1.SelectedRow.Cells[6].Text;
                string State = GridView1.SelectedRow.Cells[7].Text;
                string ZipCode = GridView1.SelectedRow.Cells[8].Text;
                string CommericalTaxState = GridView1.SelectedRow.Cells[9].Text;
                string CommericalTaxType = GridView1.SelectedRow.Cells[10].Text;
                string CommericalTaxRegnNo = GridView1.SelectedRow.Cells[11].Text;

                PartyName = PartyName.Replace("&nbsp;", "");
                IeCodeNo = IeCodeNo.Replace("&nbsp;", "");
                BranchSno = BranchSno.Replace("&nbsp;", "");
                Address = Address.Replace("&nbsp;", "");
                City = City.Replace("&nbsp;", "");
                State = State.Replace("&nbsp;", "");
                ZipCode = ZipCode.Replace("&nbsp;", "");

                //CommericalTaxState = CommericalTaxState.Replace("&nbsp;", "");
                //CommericalTaxType = CommericalTaxType.Replace("&nbsp;", "");
                //CommericalTaxRegnNo = CommericalTaxRegnNo.Replace("&nbsp;", "");
                ClientScript.RegisterStartupScript(typeof(GridView), "JavaScript", "GetImporter('" + PartyName + "','" + IeCodeNo + "','" + BranchSno + "','" + Address + "','" + City + "','" + State + "','" + ZipCode + "','" + CommericalTaxState + "','" + CommericalTaxType + "','" + CommericalTaxRegnNo + "','" + mode + "');", true);
            }
            else if (mode == "Exp")
            {

                string PartyName = GridView1.SelectedRow.Cells[2].Text;
                string IeCodeNo = GridView1.SelectedRow.Cells[3].Text;
                string BranchSno = GridView1.SelectedRow.Cells[4].Text;
                string Address = GridView1.SelectedRow.Cells[5].Text;

                string State = GridView1.SelectedRow.Cells[6].Text;

                PartyName = PartyName.Replace("&nbsp;", "");
                IeCodeNo = IeCodeNo.Replace("&nbsp;", "");
                BranchSno = BranchSno.Replace("&nbsp;", "");
                Address = Address.Replace("&nbsp;", "");

                State = State.Replace("&nbsp;", "");

                ClientScript.RegisterStartupScript(typeof(GridView), "JavaScript", "GetExporter('" + PartyName + "','" + IeCodeNo + "','" + BranchSno + "','" + Address + "','" + State + "','" + mode + "');", true);
            }
            else if (mode == "ExpCnsr")
            {
                string ConsName = GridView1.SelectedRow.Cells[2].Text;
                string ConsAdd = GridView1.SelectedRow.Cells[3].Text;
                string ConCountry = GridView1.SelectedRow.Cells[4].Text;

                ConsName = ConsName.Replace("&nbsp;", "");
                ConsAdd = ConsAdd.Replace("&nbsp;", "");
                ConCountry = ConCountry.Replace("&nbsp;", "");

                ClientScript.RegisterStartupScript(typeof(GridView), "JavaScript", "GetProduct('" + ConsName + "','" + ConsAdd + "','" + ConCountry + "','" + mode + "');", true);
            }
            else if (mode == "ExpBuyer")
            {
                string ConsName = GridView1.SelectedRow.Cells[2].Text;
                string ConsAdd = GridView1.SelectedRow.Cells[3].Text;
                string ConCountry = GridView1.SelectedRow.Cells[4].Text;

                ConsName = ConsName.Replace("&nbsp;", "");
                ConsAdd = ConsAdd.Replace("&nbsp;", "");
                ConCountry = ConCountry.Replace("&nbsp;", "");

                ClientScript.RegisterStartupScript(typeof(GridView), "JavaScript", "GetProduct('" + ConsName + "','" + ConsAdd + "','" + ConCountry + "','" + mode + "');", true);
            }
            else if (mode == "Notify")
            {

                string PartyName = GridView1.SelectedRow.Cells[2].Text;
                string IeCodeNo = GridView1.SelectedRow.Cells[3].Text;
                string BranchSno = GridView1.SelectedRow.Cells[4].Text;
                string Address = GridView1.SelectedRow.Cells[5].Text;

                string State = GridView1.SelectedRow.Cells[6].Text;

                PartyName = PartyName.Replace("&nbsp;", "");
                IeCodeNo = IeCodeNo.Replace("&nbsp;", "");
                BranchSno = BranchSno.Replace("&nbsp;", "");
                Address = Address.Replace("&nbsp;", "");

                State = State.Replace("&nbsp;", "");

                ClientScript.RegisterStartupScript(typeof(GridView), "JavaScript", "GetExporter('" + PartyName + "','" + IeCodeNo + "','" + BranchSno + "','" + Address + "','" + State + "','" + mode + "');", true);
            }

            else if (mode == "Product")
            {

                string PartyName = GridView1.SelectedRow.Cells[2].Text;
                string IeCodeNo = GridView1.SelectedRow.Cells[3].Text;
                string BranchSno = GridView1.SelectedRow.Cells[4].Text;
                string Address = GridView1.SelectedRow.Cells[5].Text;

                string State = GridView1.SelectedRow.Cells[6].Text;

                PartyName = PartyName.Replace("&nbsp;", "");
                IeCodeNo = IeCodeNo.Replace("&nbsp;", "");
                BranchSno = BranchSno.Replace("&nbsp;", "");
                Address = Address.Replace("&nbsp;", "");

                State = State.Replace("&nbsp;", "");

                ClientScript.RegisterStartupScript(typeof(GridView), "JavaScript", "GetExporter('" + PartyName + "','" + IeCodeNo + "','" + BranchSno + "','" + Address + "','" + State + "','" + mode + "');", true);
            }
            
            
            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //string mode = Request.QueryString["mode"];
            //DataSet ds = obj1.GetConsignorPopup(txtSearch.Text, mode);
            //GridView1.DataSource = ds;
            //GridView1.DataBind();
        }

      

    }
}
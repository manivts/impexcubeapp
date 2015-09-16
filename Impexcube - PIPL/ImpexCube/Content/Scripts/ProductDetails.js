//function GetProductMain(ITCLocation, ITCCHSCode, PolicyPara, PolicyYr) 
//{
//    document.getElementById('ContentPlaceHolder1_txtitcloc').value = ITCLocation;
//    document.getElementById('ContentPlaceHolder1_txtitchscode').value = ITCCHSCode;
//    document.getElementById('ContentPlaceHolder1_txtpolicy').value = PolicyPara;
//    document.getElementById('ContentPlaceHolder1_txtpyear').value = PolicyYr;
//}
function ProductValueINR(Exrate,ProValue,INRValue) {
    var exrate = 0.00;
    var proval = 0.00;
    var proinrval = 0.00;
    exrate = document.getElementById(Exrate).value;
    proval = document.getElementById(ProValue).value;
    proinrval = exrate * proval;
    document.getElementById(INRValue).value = parseFloat(proinrval);
}


function TotalFOBMisRate(txtTotInv, txtTotalAmount, txtTotalRate, txtINRAmount, txtAmount, txtRate, txtExchange,txtInvExchange,txtProductValues) {
    //Input

    var txtTotInv1=0.00;
    var txtTotalAmount1 = 0.00;
    var txtExchange1 = 0.00;
    var txtProductValues1 = 0.00;
    var txtInvExchange1 = 0.00;

    if (document.getElementById(txtExchange).value != '0') {
        txtTotInv1 = document.getElementById(txtTotInv).value;
        txtTotalAmount1 = document.getElementById(txtTotalAmount).value;
        txtExchange1 = document.getElementById(txtExchange).value;
        txtInvExchange1 = document.getElementById(txtInvExchange).value;
        txtProductValues1 = document.getElementById(txtProductValues).value;

        //Calcualtion
        var TotalInvINR = 0.00;
        var InvINR = 0.00;
        var FOBINR = 0.00;
        // var TotalRateINR = 0.00;
        var txtINRAmount1 = 0.00;
        var txtAmount1 = 0.00;
        // var  Rate1 = 0.00;

        var txtTotalRate1 = 0.00;
        var txtRate1 = 0.00;

        TotalInvINR = txtTotInv1 * txtInvExchange1;
        InvINR = txtProductValues1 * txtInvExchange1;
        FOBINR = txtTotalAmount1 * txtExchange1;

        txtTotalRate1 = (FOBINR / TotalInvINR) * 100;
        txtINRAmount1 = (FOBINR / TotalInvINR) * InvINR;
        txtAmount1 = txtINRAmount1 / txtExchange1;

        txtRate1 = (txtINRAmount1 / InvINR) * 100;

        //Output

        //txtTotalRate1 = TotalRateINR;//TotalRateINR / txtExchange1;
        document.getElementById(txtTotalRate).value = parseFloat(txtTotalRate1).toFixed(4);
        document.getElementById(txtINRAmount).value = parseFloat(txtINRAmount1).toFixed(2);
        document.getElementById(txtAmount).value = parseFloat(txtAmount1).toFixed(4);
        //txtRate1 = Rate1;//Rate1 / txtExchange1;
        document.getElementById(txtRate).value = parseFloat(txtRate1).toFixed(4);
    }
    else {
        alert('Please Select the Currency');
    }

}
function TotalFOBMisAmount(txtTotInv, txtTotalAmount, txtTotalRate, txtINRAmount, txtAmount, txtRate, txtExchange, txtInvExchange, txtProductValues) {

    //Input
    var txtTotInv1 = 0.00;
    var txtTotalRate1 = 0.00;
    var txtExchange1 = 0.00;
    var txtProductValues1 = 0.00;
    var txtInvExchange1 = 0.00;
    if (document.getElementById(txtExchange).value != '0') {
        txtTotInv1 = document.getElementById(txtTotInv).value;
        txtTotalRate1 = document.getElementById(txtTotalRate).value;
        txtExchange1 = document.getElementById(txtExchange).value;
        txtInvExchange1 = document.getElementById(txtInvExchange).value;
        txtProductValues1 = document.getElementById(txtProductValues).value;

        //Calcualtion
        var TotalInvINR = 0.00;
        var InvINR = 0.00;
        var FOBINR = 0.00;
        var txtINRAmount1 = 0.00;
        var txtAmount1 = 0.00;
        var txtTotalAmount1 = 0.00;
        var txtRate1 = 0.00;

        TotalInvINR = txtTotInv1 * txtInvExchange1;
        InvINR = txtProductValues1 * txtInvExchange1;


        //txtTotalRate1 = (FOBINR / TotalInvINR) * 100;
        txtTotalAmount1 = ((TotalInvINR * txtTotalRate1) / 100) / txtExchange1;
        FOBINR = txtTotalAmount1 * txtExchange1;
        txtINRAmount1 = (FOBINR / TotalInvINR) * InvINR;
        txtAmount1 = txtINRAmount1 / txtExchange1;
        txtRate1 = (txtINRAmount1 / InvINR) * 100;

        //Output
        document.getElementById(txtTotalAmount).value = parseFloat(txtTotalAmount1).toFixed(4);
        document.getElementById(txtINRAmount).value = parseFloat(txtINRAmount1).toFixed(2);
        document.getElementById(txtAmount).value = parseFloat(txtAmount1).toFixed(4);
        document.getElementById(txtRate).value = parseFloat(txtRate1).toFixed(4);
    }
    else {
        alert('Please Select the Currency');
    }

}
function TotalFOBRate(txtTotInv, txtTotalAmount, txtTotalRate, txtINRAmount, txtAmount, txtRate, txtExchange, txtInvExchange, txtProductValues) {
    //Input

    var txtTotInv1 = 0.00;
    var txtTotalAmount1 = 0.00;
    var txtExchange1 = 0.00;
    var txtProductValues1 = 0.00;
    var txtInvExchange1 = 0.00;
    
    if (document.getElementById(txtExchange).value != '0') {
        txtTotInv1 = document.getElementById(txtTotInv).value;        
        txtTotalAmount1 = document.getElementById(txtTotalAmount).value;
        txtExchange1 = document.getElementById(txtExchange).value;
        txtInvExchange1 = document.getElementById(txtInvExchange).value;
        txtProductValues1 = document.getElementById(txtProductValues).value;

       
        //Calcualtion
        var TotalInvINR = 0.00;
        var InvINR = 0.00;
        var FOBINR = 0.00;
        // var TotalRateINR = 0.00;
        var txtINRAmount1 = 0.00;  
        var txtAmount1 = 0.00;
        // var  Rate1 = 0.00;

        var txtTotalRate1 = 0.00;
        var txtRate1 = 0.00;

        TotalInvINR = txtTotInv1 * txtInvExchange1;
        InvINR = txtProductValues1 * txtInvExchange1;
        FOBINR = txtTotalAmount1 * txtExchange1;

        txtTotalRate1 = (FOBINR / TotalInvINR) * 100;
        txtINRAmount1 = (FOBINR / TotalInvINR) * InvINR;
        txtAmount1 = txtINRAmount1 / txtExchange1;
        txtRate1 = (txtINRAmount1 / InvINR) * 100;

        //Output

        document.getElementById(txtTotalRate).value = parseFloat(txtTotalRate1).toFixed(4);
        document.getElementById(txtINRAmount).value = parseFloat(txtINRAmount1).toFixed(2);
        document.getElementById(txtAmount).value = parseFloat(txtAmount1).toFixed(4);
        document.getElementById(txtRate).value = parseFloat(txtRate1).toFixed(4);
    }
    else {
        alert('Please Select the Currency');
    }

}
function TotalFOBAmount(txtTotInv, txtTotalAmount, txtTotalRate, txtINRAmount, txtAmount, txtRate, txtExchange, txtInvExchange, txtProductValues, txtMiscellameousExchange,
 txtMiscelTotalAmount) {debugger

    //Input
    var txtTotInv1 = 0.00;
    var txtTotalRate1 = 0.00;
    var txtExchange1 = 0.00;
    var txtProductValues1 = 0.00;
    var txtInvExchange1 = 0.00;

    var txtMiscellameousExchange1 = 0.00;
    var txtMiscelTotalAmount1 = 0.00;
    var MisAmount = 0.00;

    if (document.getElementById(txtExchange).value != '0') {
        txtTotInv1 = document.getElementById(txtTotInv).value;
        txtTotalRate1 = document.getElementById(txtTotalRate).value;
        txtExchange1 = document.getElementById(txtExchange).value;
        txtInvExchange1 = document.getElementById(txtInvExchange).value;
        txtProductValues1 = document.getElementById(txtProductValues).value;

        if (document.getElementById(txtMiscelTotalAmount).value != '0') {
            txtMiscellameousExchange1 = document.getElementById(txtMiscellameousExchange).value;
            txtMiscelTotalAmount1 = document.getElementById(txtMiscelTotalAmount).value;
            MisAmount = txtMiscellameousExchange1 * txtMiscelTotalAmount1;
        }

        //Calcualtion
        var TotalInvINR = 0.00;
        var InvINR = 0.00;
        var FOBINR = 0.00;
        var txtINRAmount1 = 0.00;
        var txtAmount1 = 0.00;
        var txtTotalAmount1 = 0.00;
        var txtRate1 = 0.00;
        var MisInvAmt = 0.00;


        TotalInvINR = txtTotInv1 * txtInvExchange1;
        InvINR = txtProductValues1 * txtInvExchange1;
        MisInvAmt = (parseFloat(MisAmount) * parseFloat(InvINR)) / parseFloat(TotalInvINR);
        TotalInvINR = parseFloat(TotalInvINR) + parseFloat(MisAmount);
        InvINR = parseFloat(InvINR) + parseFloat(MisInvAmt);
        //txtTotalRate1 = (FOBINR / TotalInvINR) * 100;
        txtTotalAmount1 = ((TotalInvINR * txtTotalRate1) / 100) / txtExchange1;
        FOBINR = txtTotalAmount1 * txtExchange1;
        txtINRAmount1 = (FOBINR / TotalInvINR) * InvINR;
        txtAmount1 = txtINRAmount1 / txtExchange1;
        txtRate1 = (txtINRAmount1 / InvINR) * 100;

        //Output
        document.getElementById(txtTotalAmount).value = parseFloat(txtTotalAmount1).toFixed(4);
        document.getElementById(txtINRAmount).value = parseFloat(txtINRAmount1).toFixed(2);
        document.getElementById(txtAmount).value = parseFloat(txtAmount1).toFixed(4);
        document.getElementById(txtRate).value = parseFloat(txtRate1).toFixed(4);
    }
    else {
        alert('Please Select the Currency');
    }
}

//function FreightValueINR() {
//    var exrate = 0.00;
//    var proval = 0.00;
//    var proinrval = 0.00;
//    exrate = document.getElementById('ContentPlaceHolder1_txtFreightTypeEx').value
//    proval = document.getElementById('ContentPlaceHolder1_txtFreightTypeAmount').value
//    proinrval = exrate * proval;
//    document.getElementById('ContentPlaceHolder1_txtFreightTypeAmountINR').value = proinrval;
//}

//function InsuranceValueINR() {
//    var exrate = 0.00;
//    var proval = 0.00;
//    var proinrval = 0.00;
//    exrate = document.getElementById('ContentPlaceHolder1_txtInsuranceTypeEx').value
//    proval = document.getElementById('ContentPlaceHolder1_txtInsuranceTypeAmount').value
//    proinrval = exrate * proval;
//    document.getElementById('ContentPlaceHolder1_txtInsuranceTypeAmountINR').value = proinrval;
//}


//function MiscellaneousValueINR() {
//    var exrate = 0.00;
//    var proval = 0.00;
//    var proinrval = 0.00;
//    exrate = document.getElementById('ContentPlaceHolder1_txtMiscelROE').value
//    proval = document.getElementById('ContentPlaceHolder1_txtMiscelAmount').value
//    proinrval = exrate * proval;
//    document.getElementById('ContentPlaceHolder1_txtMiscelAmountINR').value = proinrval;
//}

//CIFAmtCalculation('ContentPlaceHolder1_txtFreightExchange', 'ContentPlaceHolder1_txtFreightRate', 'ContentPlaceHolder1_txtProductINRValues', 'ContentPlaceHolder1_txtFreightAmount', 'ContentPlaceHolder1_txtFreightINRAmount')
function CIFAmtCalculation(ExRate, Rate, PrVal, Amt, INRAmt) 
{
    try {

        var ExchRate = 0.00;
        var RateP = 0.00;
        var ProdValue = 0.00;
        var ProAmt = 0.00;
        var Amount = 0.00;

        ExchRate = document.getElementById(ExRate).value;
        RateP = document.getElementById(Rate).value;
        ProAmt = document.getElementById(PrVal).value;
        ProdValue = (RateP * ProAmt) / 100;
        Amount = ProdValue / ExchRate;
        document.getElementById(Amt).value = parseFloat(Amount).toFixed(4);
        document.getElementById(INRAmt).value = parseFloat(ProdValue).toFixed(2);
    }
    catch (err) 
    {
        document.getElementById(Amt).value = 0;
        document.getElementById(INRAmt).value = 0;
    }
}

//InsutanceAmount('ContentPlaceHolder1_txtFreightExchange', 'ContentPlaceHolder1_txtFreightRate', 'ContentPlaceHolder1_txtProductINRValues', 'ContentPlaceHolder1_txtFreightAmount', 'ContentPlaceHolder1_txtFreightINRAmount')
function InsuranceAmount(ExRate, Rate, PrVal, Amt, INRAmt,Mis) {
    try {

        var ExchRate = 0.00;
        var RateP = 0.00;
        var ProdValue = 0.00;
        var ProAmt = 0.00;
        var Amount = 0.00;
        var MisAmt = 0.00;

        ExchRate = document.getElementById(ExRate).value;
        RateP = document.getElementById(Rate).value;
        ProAmt = document.getElementById(PrVal).value;
        MisAmt = document.getElementById(Mis).value;

        ProdValue = (RateP * (parseFloat(ProAmt) + parseFloat(MisAmt))) / 100;
        Amount = ProdValue / ExchRate;
        document.getElementById(Amt).value = Amount.toFixed(2);
        document.getElementById(INRAmt).value = ProdValue.toFixed(2);
    }
    catch (err) {
        document.getElementById(Amt).value = 0;
        document.getElementById(INRAmt).value = 0;
    }
}



// double frghtexchgRate = Convert.ToDouble(txtFreightExchange.Text);
//            double frghtrate1 = Convert.ToDouble(txtFreightAmount.Text);
//            double frghtrate = (frghtrate1 * 100) / (Convert.ToDouble(txtProductValues.Text) * frghtexchgRate);
//            double frtamt = Math.Round(frghtrate, 4);
//            txtFreightRate.Text = frtamt.ToString();
function CIFRateCalculation(ExRate, Amt, PrVal, Rate, INRAmt) 
{
    try {
        var ExchRate = 0.00;
        var RateP = 0.00;
        var ProdValue = 0.00;
        var ProAmt = 0.00;
        var Amount = 0.00;
        ExchRate = document.getElementById(ExRate).value;

        Amount = document.getElementById(Amt).value;

        ProAmt = document.getElementById(PrVal).value;

        ProdValue = (Amount * ExchRate);

        RateP = (ProdValue / ProAmt) * 100;

        document.getElementById(Rate).value = RateP.toFixed(4);
        document.getElementById(INRAmt).value = ProdValue.toFixed(2);
    }
    catch (err) 
    {
        document.getElementById(Rate).value = 0;
        document.getElementById(INRAmt).value = 0;
    }

}

function InsuranceRate(ExRate, Amt, PrVal, Rate, INRAmt,Mis) {
    try {
        debugger
       
        var ExchRate = 0.00;
        var RateP = 0.00;
        var ProdValue = 0.00;
        var ProAmt = 0.00;
        var Amount = 0.00;
        var MisAmt = 0.00;
        ExchRate = document.getElementById(ExRate).value;

        Amount = document.getElementById(Amt).value;

        ProAmt = document.getElementById(PrVal).value;
        MisAmt = document.getElementById(Mis).value;

        ProdValue = (Amount * ExchRate);

        RateP = (ProdValue / (parseFloat(ProAmt) + parseFloat(MisAmt))) * 100;

        document.getElementById(Rate).value = RateP.toFixed(4);
        document.getElementById(INRAmt).value = ProdValue.toFixed(2);
    }
    catch (err) {
        document.getElementById(Rate).value = 0;
        document.getElementById(INRAmt).value = 0;
    }

}


function txtboxdisable() {

    return false;
}

function popupwindow(url) {
    if (url == 'frmPopUpConsigner.aspx?mode=high') {
        window.open(url, '_blank', 'width=800,height=400, menubar=no, scrollbars=no, toolbar=no, location=no, resizable=no, left=350, top=200, Right=200=, bottom=200');


    }
    else {
        var win = window.open(url, '_blank', 'width=800,height=400, menubar=no, scrollbars=no, toolbar=no, location=no, resizable=no, left=350, top=200, Right=200=, bottom=200');
        win.focus();
    }
    /*ScriptManager.RegisterStartupScript(this, this.GetType(), "Con", "window.open('frmPopUpConsigner.aspx?mode=high','_blank','width=800,height=400, menubar=no, scrollbars=no, toolbar=no, location=no, resizable=no, left=350, top=200, Right=200=, bottom=200');", true);*/
}

 function isFloat(evt, val) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode == 46 && val.indexOf('.') == -1) {
        return true;
    }

    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        //alert('Enter Only Numbers');
        return false;
    }

  return true;
}
function txtdisable(chkobj, txtobj) {
    var chk1 = document.getElementById(chkobj).checked;
    if (chk1 == true) {
        document.getElementById(txtobj).disabled = false;
    }
    else {
        document.getElementById(txtobj).disabled = true;
    }
}

function CheckCurrency(evt, Details, Exrate, msg) {
    if (isNewFloat(evt)) {
        if (document.getElementById(Details).selectedIndex == 0) {
            alert(msg);
            document.getElementById(Exrate).value = '0';
            document.getElementById(Details).focus();
            return false;
        }
    }
    else {
        return false;
    }
}

function CheckExchange(id) {
    if (document.getElementById(id).value == '') {
        document.getElementById(id).value = '0';
    }
}

function CheckFreightDet(ID, ExchRate, Rate, Amount, AmountINR) {
    if (document.getElementById(ID).selectedIndex == 0) {
        document.getElementById(ExchRate).value = '0';
        document.getElementById(Rate).value = '0';
        document.getElementById(Amount).value = '0';
        document.getElementById(AmountINR).value = '0';
    }
}
function ExpProductValueINR() {
    var exrate = 0.00;
    var proval = 0.00;
    var proinrval = 0.00;
    exrate = document.getElementById('ContentPlaceHolder1_txtexcrate').value
    proval = document.getElementById('ContentPlaceHolder1_txtinvval').value
    proinrval = exrate * proval;
    document.getElementById('ContentPlaceHolder1_txtAmountinINR').value = proinrval;
}
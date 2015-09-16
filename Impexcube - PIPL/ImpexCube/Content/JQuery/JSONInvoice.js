function BindExchgeRate(CurrencyId, exrate) {
    myArguments = arguments;
    $.ajax({
        type: "POST",
        //url: "/WebForm2.aspx/BindDatatoDropdownemail",
        url: "/JSONServices/frmCommonService.aspx/BindDatatoDropdownemail",
       // url: "http://vts-sdu-4/impexcube/JSONServices/frmCommonService.aspx/BindDatatoDropdownemail",
        //url: "http://pigroup.in/impexcube/JSONServices/frmCommonService.aspx/BindDatatoDropdownemail",
        data: "{'CurrencyShortName':'" + CurrencyId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var ExchangeRate = msg.d;
            $.each(ExchangeRate, function (key, value) {
                exrate.val(value.IMPCurrencyRate).html(value.IMPCurrencyRate);
            });
        },
        error: function (jqXhr) {
            errorCaller(jqXhr);
        }
    });
}

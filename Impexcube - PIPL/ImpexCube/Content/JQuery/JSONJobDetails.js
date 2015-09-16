function BindJobDetails(JobNo, JobReceivedDate, Mode, Custom, BEType, DocFillingStatus, BENo, BEDate) {

    myArguments = arguments;
    $.ajax({
        type: "POST",
        //url: "/WebForm2.aspx/BindDatatoDropdownemail",
        url: "/JSONServices/frmCommonService.aspx/BindJobDetails",

        //url: "http://vts-sdu-4/impexcube/JSONServices/frmCommonService.aspx/BindJobDetails",
        //url: "http://pigroup.in/impexcube/JSONServices/frmCommonService.aspx/BindJobDetails",
        data: "{'JobNo':'" + JobNo + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var ReadJob = msg.d;
            $.each(ReadJob, function (key, value) {
                JobReceivedDate.val(value.JobReceivedDate).html(value.JobReceivedDate);
                Mode.val(value.Mode).html(value.Mode);
                Custom.val(value.Custom).html(value.Custom);
                BEType.val(value.BEType).html(value.BEType);
                DocFillingStatus.val(value.DocFillingStatus).html(value.DocFillingStatus);
                BENo.val(value.BENo).html(value.BENo);
                BEDate.val(value.BEDate).html(value.BEDate);
            });
        },
        error: function (jqXhr) {
            errorCaller(jqXhr);
        }
    });
}

//BindAccountMaster

function BindAccountMaster(ImporterSearch, Importer, IECodeNo, BranchSno, Address, City, State, ZipCode, ShortName) {
    myArguments = arguments;
    try {
        $.ajax({
            type: "POST",
             url: "/JSONServices/frmCommonService.aspx/BindAccountMaster",
           // url: "/JSONServices/ImpexCubeServices.cs/BindAccountMaster",
             //url: "/JSONServices/frmCommonService.aspx/BindAccountMaster",
            // url: "http://vts-sdu-3/impexcube/JSONServices/frmCommonService.aspx/BindAccountMaster",
            //url: "http://pigroup.in/impexcube/JSONServices/frmCommonService.aspx/BindAccountMaster",
            data: "{'SearchAccName':'" + ImporterSearch + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                var ReadImp = msg.d;
                $.each(ReadImp, function (key, value) {
                    Importer.val(value.Importer).html(value.Importer);
                    IECodeNo.val(value.IECodeNo).html(value.IECodeNo);
                    BranchSno.val(value.BranchSno).html(value.BranchSno);
                    Address.val(value.Address).html(value.Address);
                    City.val(value.City).html(value.City);
                    State.val(value.State).html(value.State);
                    ZipCode.val(value.ZipCode).html(value.ZipCode);
                    ShortName.val(value.ShortName).html(value.ShortName);

                });
            },
            error: function (jqXhr) {
                errorCaller(jqXhr);
            }
        });

    }
    catch (e) {
    }
}


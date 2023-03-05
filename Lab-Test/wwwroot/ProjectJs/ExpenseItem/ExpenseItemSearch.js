var $ = jQuery.noConflict(true);
$(document).ready(function () {
    //search();
    getSearchResult();
});

function getSearchObject() {

    var searchVm = {
        //"ServiceCenterId": $("#ServiceCenterId").val(),
        //"DesignationId": $("#DesignationId").val(),
        //"DepartmentId": $("#DepartmentId").val(),
        //"ShiftId": $("#ShiftId").val(),
        //"SupervisorId": $("#SupervisorId").val(),
        //"EmploymentTypeId": $("#EmploymentTypeId").val(),
        //"Email": $("#Email").val(),
        //"CodeWithPrefix": $("#CodeWithPrefix").val(),
        //"NidNo": $("#NidNo").val(),
        //"LastName": $("#LastName").val()

    }
    return searchVm;
}


function search() {
    var searchVm = getSearchObject();

    if ($.fn.DataTable.isDataTable("#ExpenseItemSearchTable")) {
        var table = $("#ExpenseItemSearchTable").DataTable();
        table.destroy();
    }

    var params = "";
    if (searchVm != undefined && searchVm != "") {
        params = { searchVm: searchVm }
    }

    var oTable = $("#ExpenseItemSearchTable").DataTable({
        "aLengthMenu": [[50, 75, 100, 125, 150, 175, 200], [50, 75, 100, 125, 150, 175, 200]],
        "iDisplayLength": 50,
        "processing": true,
        "serverSide": true,

        "ajax": {
            url: "/ExpenseItem/Search",
            type: "POST",
            data: params
        },

        "columns": [

            { "data": "SerialNo" },
            { "data": "Name" },
            { "data": "UnitPrice" },
            { "data": "Description" },
            {

                "render": function (data, type, item) {
                    var editBtn = "";
                    var deleteBtn = "";
                    var viewBtn = "";

                    if (item.CanView) {
                        viewBtn = "<button type='button' data-url='" + subDirectory + "Employees/GetEmployeeDetailsPartial/" + item.Id + "' title='View' class='btn btn-success btn-xs glyphicon glyphicon-eye-open base-view-btn'></button>";;
                    }

                    if (item.CanUpdate) {
                        editBtn = "<a href='" + subDirectory + "Employees/edit/" + item.Id + "' target='_blank' title='Edit' class='btn btn-warning btn-xs glyphicon glyphicon-pencil'></a>";
                    }
                    if (!item.IsConfirm && item.CanDelete) {
                        deleteBtn = "<a href='" + subDirectory + "Employees/Delete/" + item.Id + "' target='_blank' title='Delete' class='btn btn-danger btn-xs glyphicon glyphicon-trash'></a>";
                    }

                    return viewBtn + editBtn + deleteBtn;


                }
            }

        ]
    });

    // scrollDiv("#EmployeeSearchTable");

}

function getSearchResult() {
    var data = [];
    $.ajax({
        type: "POST",
        url: "/ExpenseItem/Search",
        async: false,
        success: function (result) {
            $.each(result.dataList, function (key, value) {

                var editBtn = "<a href='Edit/" + value.id + "'><i class='fa fa-pencil text-warning m-1' title='Edit' aria-hidden='true'></i></a>";
                var detailsBtn = "<a href='Details/" + value.id + "'><i class='fa fa-eye text-info m-1' title='Details' aria-hidden='true'></i></a>";
                var deleteBtn = "<a href='Delete/" + value.id + "'><i class='fa fa-trash-o text-danger m-1' title='Delete' aria-hidden='true'></i></a>";
                var actionBtn = editBtn + detailsBtn + deleteBtn;
                data.push([
                    value.serialNo, value.name, value.unitPrice, value.description, actionBtn
                ]);
            });
        },
        failure: function (err) {

        }
    });
    $("#ExpenseItemSearchTable").DataTable({
        data: data
    });
}
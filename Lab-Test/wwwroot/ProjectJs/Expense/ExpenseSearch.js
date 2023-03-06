var $ = jQuery.noConflict(true);
$(document).ready(function () {
    search();
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

    if ($.fn.DataTable.isDataTable("#ExpenseSearchTable")) {
        var table = $("#ExpenseSearchTable").DataTable();
        table.destroy();
    }

    var params = "";
    if (searchVm != undefined && searchVm !== "") {
        params = { searchVm: searchVm }
    }

    var oTable = $("#ExpenseSearchTable").DataTable({
        "aLengthMenu": [[25, 50, 100, 200], [25, 50, 100, 200]],
        "iDisplayLength": 25,
        "processing": true,
        "serverSide": true,
        "bSort": false,
        "ajax": {
            url: "/Expense/Search",
            type: "POST",
            data: params
        },

        "columns": [
            {
                data: "SerialNo",
                render: function (data, type, row, meta) {
                    return row.serialNo;
                }
            },
            {
                data: "Title",
                render: function (data, type, row, meta) {
                    return row.title;
                }
            },
            {
                data: "Date",
                render: function (data, type, row, meta) {
                    return row.date;
                }
            },
            {
                data: "Description",
                render: function (data, type, row, meta) {
                    return row.description;
                }
            },
            {
                data: "Action",
                render: function (data, type, row, meta) {
                    var editBtn = "<a href='Edit/" + row.id + "'><i class='fa fa-pencil text-warning m-1' title='Edit' aria-hidden='true'></i></a>";
                    var detailsBtn = "<a href='Details/" + row.id + "'><i class='fa fa-eye text-info m-1' title='Details' aria-hidden='true'></i></a>";
                    var deleteBtn = "<a href='Delete/" + row.id + "'><i class='fa fa-trash-o text-danger m-1' title='Delete' aria-hidden='true'></i></a>";
                    var actionBtn = editBtn + detailsBtn + deleteBtn;
                    return actionBtn;
                }
            }
        ]
    });
}

var $ = jQuery.noConflict(true);
$(document).ready(function () {
    search();
   // getSearchResult();
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
        "aLengthMenu": [[25, 50, 75, 100], [25, 50, 75, 100]],
        "iDisplayLength": 25,
        "processing": true,
        "serverSide": true,

        "ajax": {
            url: "/ExpenseItem/Search",
            type: "POST",
            data: params
        },

        "columns": [
            {
                data: 'SerialNo',
                render: function (data, type, row, meta) {
                    return row.serialNo;
                }
            },
            {
                data: 'Name',
                render: function (data, type, row, meta) {
                    return row.name;
                }
            },
            {
                data: 'UnitPrice',
                render: function (data, type, row, meta) {
                    return row.unitPrice;
                }
            },
            {
                data: 'Description',
                render: function (data, type, row, meta) {
                    return row.description;
                }
            },
            {
                data: 'Action',
                render: function (data, type, row, meta) {
                    var editBtn = "<a href='Edit/" + row.id + "'><i class='fa fa-pencil text-warning m-1' title='Edit' aria-hidden='true'></i></a>";
                    var detailsBtn = "<a href='Details/" + row.id + "'><i class='fa fa-eye text-info m-1' title='Details' aria-hidden='true'></i></a>";
                    var deleteBtn = "<a href='Delete/" + row.id + "'><i class='fa fa-trash-o text-danger m-1' title='Delete' aria-hidden='true'></i></a>";
                    var actionBtn = editBtn + detailsBtn + deleteBtn;
                    return actionBtn;
                }
            }
//            { "data": "SerialNo" },
//            { "data": "Name" },
//            { "data": "UnitPrice" },
//            { "data": "Description" },
//            {
//
//                "render": function (data, type, item) {
//                    var editBtn = "";
//                    var deleteBtn = "";
//                    var viewBtn = "";
//
//                    if (item.CanView) {
//                        viewBtn = "<button type='button' data-url='" + subDirectory + "Employees/GetEmployeeDetailsPartial/" + item.Id + "' title='View' class='btn btn-success btn-xs glyphicon glyphicon-eye-open base-view-btn'></button>";;
//                    }
//
//                    if (item.CanUpdate) {
//                        editBtn = "<a href='" + subDirectory + "Employees/edit/" + item.Id + "' target='_blank' title='Edit' class='btn btn-warning btn-xs glyphicon glyphicon-pencil'></a>";
//                    }
//                    if (!item.IsConfirm && item.CanDelete) {
//                        deleteBtn = "<a href='" + subDirectory + "Employees/Delete/" + item.Id + "' target='_blank' title='Delete' class='btn btn-danger btn-xs glyphicon glyphicon-trash'></a>";
//                    }
//
//                    return viewBtn + editBtn + deleteBtn;
//
//
//                }
//            }

        ]
    });

    // scrollDiv("#EmployeeSearchTable");

}

function getSearchResult() {
    $.ajax({
        type: "POST",
        url: "/ExpenseItem/Search",
        async: false,
        success: onSuccess,
        failure: function (err) {

        }
    });

    function onSuccess(response) {
        $("#ExpenseItemSearchTable").DataTable({
            bProcessing: true,
            bLengthChange: true,
            lengthMenu: [[25, 50, 75, 100], [25, 50, 75, 100]],
            bFilter: true,
            bSort: true,
            bPaginate: true,
            serverSide: true,
            data: response.dataList,
            columns: [
                {
                    data: 'SerialNo',
                    render: function (data, type, row, meta) {
                        return row.serialNo;
                    }
                },
                {
                    data: 'Name',
                    render: function (data, type, row, meta) {
                        return row.name;
                    }
                },
                {
                    data: 'UnitPrice',
                    render: function (data, type, row, meta) {
                        return row.unitPrice;
                    }
                },
                {
                    data: 'Description',
                    render: function (data, type, row, meta) {
                        return row.description;
                    }
                },
                {
                    data: 'Action',
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
}
$(document).ready(function () {
    $('#ExpenseItemSearchTable').DataTable();
   // search();
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

    var oTable = $("#EmployeeSearchTable").DataTable({
        "aLengthMenu": [[50, 75, 100, 125, 150, 175, 200], [50, 75, 100, 125, 150, 175, 200]],
        "iDisplayLength": 50,
        "processing": true,
        "serverSide": true,

        "ajax": {
            url: subDirectory + "Employees/GetDataTableJsonData",
            type: "POST",
            data: params
        },

        "columns": [

            { "data": "SerialNo" },
            {
                "data": "PhotoString",

                "render": function (data) {
                    if (data != null) {

                        return '<img src="' + data + '"  style="height:50px;width:50px;"/>';

                    } else {

                        return '<img src="../Images/Nopreview-Available.jpg" alt=" Image"  style="height:50px;width:50px;"/>';
                    }

                }
            },

            { "data": "CodeWithPrefix" },
            { "data": "ServiceCenterName" },
            { "data": "DepartmentName" },
            { "data": "DesignationName" },


            //{
            //    "data": "FromDate",
            //    "render": function (data) {
            //        if (data != null && data != undefined) {
            //            return convertJsonDateForView(data);
            //        }
            //        return "N/ A";
            //    }
            //},

            //{
            //    "data": "ToDate",
            //    "render": function (data) {
            //        if (data != null && data != undefined) {
            //            return convertJsonDateForView(data);
            //        }
            //        return "N/ A";
            //    }
            //},

            { "data": "FullName" },
            { "data": "ContactNo" },
            { "data": "Email" },
            { "data": "NidNo" },

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

    scrollDiv("#EmployeeSearchTable");

}
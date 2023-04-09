
var $ = jQuery.noConflict(true);

$(document).ready(function () {
    var expenseId = $("#ExpenseIdHidden").attr("data-id");
    if (expenseId != "" && expenseId != undefined) {
        getExpenseDetailsByExpenseId(expenseId);
    }

});

function getExpenseDetailsByExpenseId(expenseId) {
    var json = { expenseId: expenseId };
    $.ajax({
        type: "GET",
        url: subDirectory + 'Loader/GetExpenseDetailsByExpenseId',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(json),
        success: function (data) {
            if (data != null) {
                $.each(data, function (key, value) {
                    createNewRowForExpense(index, value);
                    itemList.push(value.ExpenseItemId);
                });

            }

        }
    });
}


$(document.body).on("change", "#expItemIdVal", function () {
    var value = $(this).val();
    $('#expItemIdValidation').text(null);
    if (value <= 0) {
        var msg = "Sorry, Expense Item is required!";
        $('#expItemIdValidation').text(msg);
        return;
    }
});

$(document.body).on("change", "#expItemQtyVal", function () {
    var value = $(this).val();
    $('#expItemQtyValValidation').text(null);
    if (value <= 0) {
        var msg = "Sorry, Expense Item Qty is not valid!";
        $('#expItemQtyValValidation').text(msg);
        return;
    }
});

$(document.body).on("change", "#expItemUnitPriceVal", function () {
    var value = $(this).val();
    $('#expItemUnitPriceValValidation').text(null);
    if (value <= 0) {
        var msg = "Sorry, Expense Item unit price is not valid!";
        $('#expItemUnitPriceValValidation').text(msg);
        return;
    }
});

$(document.body).on("change", "#expItemUnitDiscountVal", function () {
    var value = $(this).val();
    $('#expItemUnitDiscountValValidation').text(null);
    if (value < 0) {
        var msg = "Sorry, Expense Item unit price Discount is not valid!";
        $('#expItemUnitDiscountValValidation').text(msg);
        return;
    }
});

$("#expItemAddButton").click(function () {
    createExpItemDetailRow();
});


function createExpItemDetailRow() {
    var selectedItem = getSelectedItem();
    var index = $("#ExpenseDetailTable").children("tr").length;
    var sl = index;

    var indexCell = "<td style='display:none'><input type='hidden' id='Index" + index + "' name='Details.Index' value='" + index + "'/> </td>";
    var serialCell = "<td>" + (++sl) + "</td>";

    var expItemNameCell = "<td><input type='hidden' id='expItemId" + index + "' name='Details[" + index + "].ExpenseItemId' value='" + selectedItem.ExpenseItemId + "'/>" + selectedItem.ExpenseItemName + "</td>";
    var expItemQtyCell = "<td><input type='hidden' id='ExpItemQty" + index + "' name='Details[" + index + "].Qty' value='" + selectedItem.ExpItemQty + "'/>" + selectedItem.ExpItemQty + "</td>";
    var expItemUnitPriceCell = "<td><input type='hidden' id='ExpItemUnitPrice" + index + "' name='Details[" + index + "].UnitPrice' value='" + selectedItem.ExpItemUnitPrice + "'/>" + selectedItem.ExpItemUnitPrice + "</td>";
    var expItemUnitDiscountCell = "<td><input type='hidden' id='ExpItemUnitDiscount" + index + "' name='Details[" + index + "].Discount' value='" + selectedItem.ExpItemUnitDiscount + "'/>" + selectedItem.ExpItemUnitDiscount + "</td>";
    var totalPrice = ((selectedItem.ExpItemUnitPrice - selectedItem.ExpItemUnitDiscount) * selectedItem.ExpItemQty);
    var expItemPrice = "<td> <input type='hidden' id ='TotalPrice" + index + "' name = 'Details[" + index + "].Price' value = '" + totalPrice + "' /> " + totalPrice + "</td>";
    var expItemNoteCell = "<td><input type='hidden' id='ExpItemNote" + index + "' name='Details[" + index + "].Note' value='" + selectedItem.ExpItemNote + "'/>" + selectedItem.ExpItemNote + "</td>";
    var actionCell = "<td></td>";

    var createNewRow = "<tr>" + indexCell + serialCell + expItemNameCell + expItemQtyCell + expItemUnitPriceCell + expItemUnitDiscountCell + expItemPrice + expItemNoteCell + actionCell + " </tr>";
    $("#ExpenseDetailTable").append(createNewRow);

    clearSelectedItem();
};

function getSelectedItem() {
    //You can validate here
    var expItemId = $("#expItemIdVal").val();
    var expItemName = $('#expItemIdVal :selected').text();
    var expItemQty = $("#expItemQtyVal").val();
    var expItemUnitPrice = $("#expItemUnitPriceVal").val();
    var expItemUnitDiscount = $("#expItemUnitDiscountVal").val();
    var expItemNote = $("#expItemNoteVal").val();

    var item = {
        "ExpenseItemId": expItemId,
        "ExpenseItemName": expItemName,
        "ExpItemQty": expItemQty,
        "ExpItemUnitPrice": expItemUnitPrice,
        "ExpItemUnitDiscount": expItemUnitDiscount,
        "ExpItemNote": expItemNote
    }
    return item;
};

function clearSelectedItem() {
    $("#expItemIdVal").val(null);
    $("#expItemQtyVal").val(null);
    $("#expItemUnitPriceVal").val(null);
    $("#expItemUnitDiscountVal").val(null);
    $("#expItemNoteVal").val("");
};

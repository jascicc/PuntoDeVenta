var orders = [];
var table = -1;
function addToCart(id) {
    var order = {};
    order.ProductID = id;
    order.ProductName = document.getElementById("name " + id).innerText;
    order.Quantity = 1;
    order.TableId = table;
    if (orders.filter(a => a.ProductID == id).length == 0) {

        orders.push(order);
        // Find a <table> element with id="myTable":
        var table = document.getElementById("myTable");

        // Create an empty <tr> element and add it to the 1st position of the table:
        var row = table.insertRow(table.rows.length);
        row.id = "row" + id;

        // Insert new cells (<td> elements) at the 1st and 2nd position of the "new" <tr> element:
        var cell1 = row.insertCell(0);
        var cell2 = row.insertCell(1);
        var cell3 = row.insertCell(2);

        // Add some text to the new cells:
        cell1.innerHTML = order.ProductName;
        cell2.innerHTML = order.Quantity;

        cell3.innerHTML = '<a class="close dngr" onclick=rmv(' + id + ') >&times;</a>';

    } else {
        var ind = orders.findIndex(o => o.ProductID == id);
        orders[ind].Quantity = orders[ind].Quantity + 1;
        var table = document.getElementById("myTable");
        table.rows[ind + 1].cells[1].innerHTML = orders[ind].Quantity;
    }
    total();

}

function total() {
    let total = 0;
    for (const key in orders) {
        total = total + orders[key].Quantity;
    }
    document.getElementById("bdgQuantity").innerHTML = total;
}

function rmv(id) {
    var elem = document.getElementById("row" + id);
    elem.parentNode.removeChild(elem);
    console.log(orders);
    orders = orders.filter(a => a.ProductID != id);
    console.log(orders);
    total();
}

function selectTable(id, number) {
    table = id;
    for (const key in orders) {
        orders[key].TableId = id;
    }
    console.log(orders);
    table = true;
    alertify.set('notifier', 'position', 'top-center');
    alertify.notify('Se ha seleccionado la mesa No. ' + number, 'success', 3, function () { }).dismissOthers();
}

$(document).ready(function () {
    $("#btnSubmit").click(function () {
        if (orders.length < 1) {
            alertify.set('notifier', 'position', 'top-center');
            alertify.notify('No se ha seleccionado ningun producto', 'error', 3, function () { }).dismissOthers();
        } else if (!table) {
            alertify.set('notifier', 'position', 'top-center');
            alertify.notify('No se ha seleccionado ninguna mesa', 'error', 3, function () { }).dismissOthers();
        } else {
            orders = JSON.stringify({ 'orders': orders });
            console.log(orders);
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                type: "POST",
                url: "/Orders/OrdersPost",
                data: orders,
                success: function () {
                    $("#myModal").modal("hide");
                }
            })
        }
    })
})

function display(category) {
    var all = document.getElementsByClassName('card');
    for (var i = 0; i < all.length; i++) {
        all[i].style.display = "none";
    }
    var coll = document.getElementsByName(category);
    for (var i = 0; i < coll.length; i++) {
        coll[i].style.display = "inline-grid";
    }
}

function add(id) {
    var input = document.getElementById(id);
    var val = parseInt(input.value) + 1;
    input.value = val;
    //hace que se mueva el card en edge
    if (val > 1) {
        var badge = document.getElementById("mybadge " + id);
        badge.style.display = "flex";
    }
    document.getElementById("lbl " + id).innerHTML = val;
}

function removeItem(id) {
    var input = document.getElementById(id);
    var val = parseInt(input.value);
    if (val > 1) {
        input.value = val - 1;
        document.getElementById("lbl " + id).innerHTML = val - 1;
        if (val == 2) {
            var badge = document.getElementById("mybadge " + id);
            badge.style.display = "none";
        }
    }

}
var orders = [];
var table = false;
var tableid = 0;
var tabletype = 0;

window.onload = function () {
    display('3');
}

function addToCart(id) {
    var order = {};
    order.ProductID = id;
    order.ProductName = document.getElementById("name " + id).innerText;
    order.Quantity = 1;
    order.TableId = tableid;

    if (orders.filter(a => a.ProductID == id).length == 0) {
        orders.push(order);
        var table = document.getElementById("myTable");

        var row = table.insertRow(table.rows.length);
        row.id = "row" + id;

        var cell1 = row.insertCell(0);
        var cell2 = row.insertCell(1);
        var cell3 = row.insertCell(2);
        var cell4 = row.insertCell(3);

        cell1.innerHTML = order.ProductName;
        cell2.innerHTML = order.Quantity;
        cell3.innerHTML = '<button class="rmvItem" onclick=rmvItem(' + id + ') >&minus;</button> ' + '<button class="addItem" onclick=addItem(' + id + ') >&plus;</button>';

        cell4.innerHTML = '<a class="close dngr" onclick=rmv(' + id + ') >&times;</a>';

        document.getElementById("mybadge " + id).style.display = "flex";
        document.getElementById("lbl " + id).innerHTML = order.Quantity;

    } else {
        var ind = orders.findIndex(o => o.ProductID == id);
        orders[ind].Quantity = orders[ind].Quantity + 1;
        var table = document.getElementById("myTable");
        table.rows[ind + 1].cells[1].innerHTML = orders[ind].Quantity;
        document.getElementById("lbl " + id).innerHTML = orders[ind].Quantity;

    }
    total();
}

function addItem(id) {
    var ind = orders.findIndex(o => o.ProductID == id);
    orders[ind].Quantity = orders[ind].Quantity + 1;
    var table = document.getElementById("myTable");
    table.rows[ind + 1].cells[1].innerHTML = orders[ind].Quantity;
    document.getElementById("lbl " + id).innerHTML = orders[ind].Quantity;

}

function rmvItem(id) {
    var ind = orders.findIndex(o => o.ProductID == id);
    if (orders[ind].Quantity > 0) {
        orders[ind].Quantity = orders[ind].Quantity - 1;
        var table = document.getElementById("myTable");
        table.rows[ind + 1].cells[1].innerHTML = orders[ind].Quantity;
        document.getElementById("lbl " + id).innerHTML = orders[ind].Quantity;
    }
    if (orders[ind].Quantity === 0) {
        rmv(id);
    }
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
    orders = orders.filter(a => a.ProductID != id);
    document.getElementById("mybadge " + id).style.display = "none";
    document.getElementById("lbl " + id).innerHTML = 0;
    total();
}

function selectTable(id, number) {
    if (tableid !== 0) {

        if (tabletype == 1) {
            document.getElementById("table " + tableid).className = "taable";
        } else {
            document.getElementById("table " + tableid).className = "taable busy";
        }
    }
    var classname = document.getElementById("table " + id).className;
    tableid = id;
    if (classname == "taable busy") {
        tabletype = 2;
    } else {
        tabletype = 1;
    }
    for (const key in orders) {
        orders[key].TableId = id;
    }

    document.getElementById("table " + id).className = "taable actual";

    alertify.set('notifier', 'position', 'top-center');
    alertify.notify('Se ha seleccionado la mesa No. ' + number, 'success', 3, function () { }).dismissOthers();
    table = true;
}

$(document).ready(function () {
    $("#btnSubmit").click(function () {
        alertify.set('notifier', 'position', 'top-center');
        if (orders.length < 1 || !table) {
            if (orders.length < 1) {
                alertify.notify('No se ha seleccionado ningun producto', 'error', 3, function () { }).dismissOthers();
            } else {
                alertify.notify('No se ha seleccionado ninguna mesa', 'error', 3, function () { }).dismissOthers();
            }
        } else {
            orders = JSON.stringify({ 'orders': orders });
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                type: "POST",
                url: "/Orders/OrdersPost",
                data: orders,
                success: function (returndata) {
                    if (returndata.ok) {
                        window.location = returndata.newurl;
                    } else {
                        alertify.notify(returndata.message, 'error', 10, function () { }).dismissOthers();
                    }

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
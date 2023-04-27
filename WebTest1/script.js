
// todo Zavolanie webovej methódy (addCarToXml) -> Ktorá pridá parametre auta do XML súboru. 






//---------------------------------------//------------------------------------------------------------//

// Alert o úspešnom pridaní automobilu (addCarToXml)
//const form = document.querySelector('#AddCar');
//const alertContainer = document.querySelector('.alert-container'); // ziskanie elementu pre alert

//form.addEventListener('submit', function (event) {
//    event.preventDefault();
//    const alertSuccess = document.createElement('div');
//    alertSuccess.classList.add('alert', 'alert-success');
//    alertSuccess.textContent = 'Formular bol uspesne odoslany.';
//    alertContainer.insertBefore(alertSuccess, alertContainer.firstChild); // vlozenie alertu pred prvy element v alertContainer
//    form.reset();
//    setTimeout(function () {
//        alertSuccess.remove();
//    }, 5000);

//});

//todo---------------------------------------Zavolanie WebMethody (UpdateCarSale)------------------------------------------------------------//

$(document).ready(function () {
    $('#updateCarBtn').on('click', function () {
        var vinNumber = $("#vinNumber").val(); 
        var dateOfSale = $("#dateOfSale").val();
        var salePrice = $("#salePrice").val(); 

        updateCarSale(vinNumber, dateOfSale, salePrice);
    });
});

function updateCarSale(vinNumber, dateOfSale, salePrice) {
    $.ajax({
        type: "POST",
        url: "https://localhost:44379/WebService.asmx/UpdateCarSale",
        data: JSON.stringify({ vinNumber: vinNumber, dateOfSale: dateOfSale, salePrice: salePrice }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            // handle success response
            alert("Automobil uspesne upraveny.");
        },
        error: function (xhr, ajaxOptions, thrownError, error) {
            // handle error response
            alert("Error deleting car: " + error);
        }
    });
}

//todo ---------------------------------------Zavolanie WebMethody (deleteCarByVin)------------------------------------------------------------//

$(document).ready(function () {
    $("#deleteCarBtn").click(function () {
        var vinNumber = $("#vinNumberInput").val();
        $.ajax({
            type: "POST",
            url: "https://localhost:44379/WebService.asmx/DeleteCarByVin",
            data: "{'VINNumber': '" + vinNumber + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                alert("Automobil uspesne odstraneny.");
            },
            error: function (xhr, status, error) {
                alert("Error deleting car: " + error);
            }
        });
    });
});

//todo---------------------------------------Zavolanie WebMethody (CalculationOfProfit)------------------------------------------------------------//

$(document).ready(function () {
    // Call the CalculationOfProfit web method
    $.ajax({
        type: "POST",
        url: "https://localhost:44379/WebService.asmx/CalculationOfProfit",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            //Do something with the response
            var profit = response.d;
          // var color = profit >= 0 ? "green" : "red"; // if profit is positive, use green color, otherwise use red color
            $("#profit").html(profit + "Eur");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status + ": " + thrownError);
        }
    });
});

//---------------------------------------Zavolanie WebMethody (GetSoldAndUnsoldCars)------------------------------------------------------------//
//$(document).ready(function () {
//    // Volanie GetSoldAndUnsoldCars web metódy
//    $.ajax({
//        type: "POST",
//        url: "https://localhost:44379/WebService.asmx/GetSoldAndUnsoldCars",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (response1) {
//            var soldCount = response1.d[0];
//            var unsoldCount = response1.d[1];
//            var totalCount = response1.d[2];

//            $("#soldCount").html("Number of cars sold: " + soldCount );
//            $("#unsoldCount").html("Number of cars unsold: " + unsoldCount );
//            $("#totalCount").html("Total number of cars: " + totalCount );
//        },
//        error: function (xhr, ajaxOptions, thrownError) {
//            alert(xhr.status + ": " + thrownError);
//        }
//   });
//});







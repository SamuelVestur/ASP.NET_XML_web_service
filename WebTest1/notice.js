function ConfirmDelete(VINNumber) {
    var result = confirm("Ste si istı, e chcete odstráni auto s VIN èíslom " + VINNumber + "?");
    if (result) {
        //volanie WebMethodu na odstránenie auta
        PageMethods.deleteCarByVin(VINNumber, OnSuccess, OnError);
    }
}

function OnSuccess() {
    //akciou po úspešnom odstránení auta
    alert("Auto bolo úspešne odstránené.");
}

function OnError(error) {
    //akciou po chybe pri odstránení auta
    alert("Došlo k chybe pri odstraòovaní auta.");
}
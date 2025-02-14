//$(document).ready(function () {
//    $('#b1').click(function () {
//        var searchitem = $('#searchitem').val();
//        $.get('/home/Search', { searchTerm: searchitem }, function (result) {
//            $('#searchResults').html(result);
//        });
//    });
//});



function AddtoCart() {
    var data = $("#addtocart").serialize();
    $.ajax({
        type: 'POST',
        url: '/Order/addtoCart',
        data: data,
        success: function (result) {
            let html = '<span class="cartcount position-absolute badge bg-danger" style="font-size: 15px; padding: 0 2px;">';
            html += result;
            html += '</span>';
            $("#carticon").html(html);
        },
        error: function (xhr, status, error) {
            console.error("Error adding to cart:", error);
        }
    });
}
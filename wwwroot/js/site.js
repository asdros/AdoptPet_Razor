// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// Get the modal
var modal = document.getElementById('myModal');
// Get the image and insert it inside the modal - use its "alt" text as a caption
var img = document.getElementById('myImg');
var modalImg = document.getElementById("img01");
var captionText = document.getElementById("caption");
img.onclick = function () {
    modal.style.display = "block";
    modalImg.src = this.src;
    modalImg.alt = this.alt;
    //    captionText.innerHTML = this.alt;
}

// When the user clicks on <span> (x), close the modal
modal.onclick = function () {
    img01.className += " out";
    setTimeout(function () {
        modal.style.display = "none";
        img01.className = "modal-content";
    }, 400);

}

$(document).ready(function () {
    var auto_array = {};
    var label = '';
    $('#placeName').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Ads/AutoCompletePlaces',
                dataType: 'json',
                data: request,
                success: function (data) {
                    response(data.map(function (value) {
                        label = value.fullPlaceName;
                        auto_array[label] = value.placeId;
                        return label;
                    }));
                }
            });
        },
        minLength: 3,
        position: { my: "left center", at: "right top" },

        select: function (event, ui) {
            console.log(auto_array);
            $('#hiddenPlaceId').val(auto_array[ui.item.value]);
        }
    });
});
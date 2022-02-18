// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function imagePreview() {
    $("#imageUpload").on('change', function () {
        var countFiles = $(this)[0].files.length;
        var imgPath = $(this)[0].value;
        var image_holder = $("#image-holder");
        image_holder.empty();
        if (typeof (FileReader) != "undefined") {
            for (var i = 0; i < countFiles; i++) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $("<img />", {
                        "src": e.target.result,
                        "class": "thumb-image"
                    }).appendTo(image_holder);
                }
                image_holder.show();
                reader.readAsDataURL($(this)[0].files[i]);
            }
        } else {
            alert("this support does not support FileReader.");
        }
    });
};

function refreshAfterValidation() {
    window.location.reload();
}

function validateImages() {
    var inp = document.getElementById('imageUpload');
    if (inp.files.length == 0) {
        alert("Zdjęcia są obowiązkowe");
        inp.focus();
        return false;
    }
}

function breedsDropDownList() {
    $("#breedId").prop("disabled", true);
    $("#animalId").change(function () {
        var id = $("#animalId").val();
        $.ajax({
            cache: false,
            type: "GET",
            url: '/Ads/GetBreeds',
            contentType: 'application/json; charset=utf-8',
            data: { "animalIdVal": id },
            success: function (json, textStatus) {
                $("#breedId").empty();
                json = json || {};
                for (var i = 0; i < json.length; i++) {
                        $("#breedId").append('<option id="breedAd" value="' + json[i].id + '">' + json[i].name + '</option>');
                }
                $("#breedId").prop("disabled", false);
            },
            error: function () {
                alert("Data not found");
            }
        });
    });
};

function autocompletePlaceInput() {
    var auto_array = {};
    var label = '';
    $('#placeName').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Ads/AutoCompletePlaces',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
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
};

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

//$(document).ready(function () {
//    var auto_array = {};
//    var label = '';
//    $('#placeName').autocomplete({
//        source: function (request, response) {
//            $.ajax({
//                url: '/Ads/AutoCompletePlaces',
//                dataType: 'json',
//                data: request,
//                success: function (data) {
//                    response(data.map(function (value) {
//                        label = value.fullPlaceName;
//                        auto_array[label] = value.placeId;
//                        return label;
//                    }));
//                }
//            });
//        },
//        minLength: 3,
//        position: { my: "left center", at: "right top" },

//        select: function (event, ui) {
//            console.log(auto_array);
//            $('#hiddenPlaceId').val(auto_array[ui.item.value]);
//        }
//    });
//});
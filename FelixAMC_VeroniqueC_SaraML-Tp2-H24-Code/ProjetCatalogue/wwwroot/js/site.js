
$(document).ready(function () {

    //var imageSelector = ".imgHover";
    //var videoSelector = ".videoHover";
    //var divSelector = ".divHover";

    //$(imageSelector).on("load", function () {
    //    var imageWidth = $(this).width();
    //    var imageHeight = $(this).height();
    //    $(videoSelector).css({ width: imageWidth, height: imageHeight });
    //});

    //$(videoSelector).css({ position: "absolute", top: 0, left: 0 });

    //$(videoSelector).hide();

    $(".divHover").hover(
        function () {
            $(this).children(".videoHover").get(0).play();
         }, function () {
            $(this).children(".videoHover").get(0).pause();
            //$(this).children(".videoHover").get(0).$("poster").show();
    }); 

});
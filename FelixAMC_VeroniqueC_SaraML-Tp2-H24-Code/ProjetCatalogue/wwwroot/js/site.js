
$(document).ready(function () {

    $('.confirmation').on('click', function () {
        return confirm('Êtes vous sûr de supprimer cet utilisateur? Cette action est irréversible!');
    });

    $(".divHover").hover(
        function () {
            $(this).children(".videoHover").get(0).play();
         }, function () {
            $(this).children(".videoHover").get(0).load();
    }); 



});
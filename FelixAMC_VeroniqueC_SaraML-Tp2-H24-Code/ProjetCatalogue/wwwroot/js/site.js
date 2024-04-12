
$(document).ready(function () {

    $('.confirmation').on('click', function () {
        return confirm('Êtes vous sûr de supprimer cet utilisateur? Cette action est irréversible!');
    });

    //let ancreConfirmation = document.getElementsByClassName('confirmation');
    //var confirmationSupression = function (e) {
    //    if (!confirm("Êtes vous sûr de supprimer " + pseudo + "? Cette action est irréversible!")){
    //        e.preventDefault();
    //    }
    //};
    //for (let i = 0, l = ancreConfirmation.length; i < l; i++) {
    //    ancreConfirmation[i].addEventListener('click', confirmationSupression, false);
    //}

    $(".divHover").hover(
        function () {
            $(this).children(".videoHover").get(0).play();
         }, function () {
            $(this).children(".videoHover").get(0).load();
    }); 



});
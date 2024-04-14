"use strict"

function validerConnection() {
    let isOk = true;
    if ($('#PseudoConnection').val().length === 0) {
        isOk = false;
    }
    if ($('#MotDePasseConnection').val().length === 0) {
        isOk = false;
    }
    return isOk;
}

function validerInscription() {
    let message = "";
    let regExNom = /^[\-/A-Za-z\u00C0-\u017F]+$/;
    let regExMdp = /^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@.#$!%*?&^])[A-Za-z\d@.#$!%*?&]{8,60}$/;
    let regExPseudo = /^\w+$/;

    let pseudo = $('#PseudoInscription').val();
    if (pseudo.length < 5 || pseudo.length > 20 || regExPseudo.exec(pseudo) === null) {
        message += "Le pseudo doit être entre 5 et 20 caractères inclusivement et ne doit pas contenir de caractère spécial. ";
    }

    let motDePasse = $('#MotDePasseInscription').val();
    if (regExMdp.exec(motDePasse) === null) {
        message += "Le mot de passe doit avoir une longueur de 8 à 60 caractères inclusivement, contenir un chiffre et un caractère spécial (@.#$!%*?&^). ";
    }

    let nom = $('#NomInscription').val();
    if (nom.length > 50 || regExNom.test(nom) === null) {
        message += "Le nom doit avoir 50 caractère ou moins. Il doit être composer de caractère alphanumérique ou '-'. ";
    }
    let prenom = $('#PrenomInscription').val();
    if (prenom.length > 50 || regExNom.test(nom) === null) {
        message += "Le prénom doit avoir 50 caractère ou moins. Il doit être composer de caractère alphanumérique ou '-'. ";
    }
    return message;
}

$(document).ready(function () {

    $('.confirmation').on('click', function () {
        return confirm('Êtes vous sûr de supprimer cet utilisateur? Cette action est irréversible!');
    });

    $('#connection-btn').on('click', function (e) {
        
        if (!validerConnection()) {
            if ($('#message-connection').length)
            {
            }
            else {
                $('#message-erreur-connection').prepend($('<div>').prop('class', 'alert alert-danger').prop('id', 'message-connection')
                    .prop('role', 'alert'));
            }
            $('#message-connection').text('Le pseudo ou mot de passe est invalide');
            e.preventDefault();
        }
    });
    $('#inscription-btn').on('click', function (e) {
        let messageErreur = validerInscription();
        if (messageErreur.length !== 0) {
            if ($('#message-inscription').length) {
            }
            else {
                $('#message-erreur-inscription').prepend($('<div>').prop('class', 'alert alert-danger').prop('id', 'message-inscription')
                    .prop('role', 'alert'));
            }
            $('#message-inscription').text(messageErreur);
            e.preventDefault();
        }
    });

    $(".divHover").hover(
        function () {
            $(this).children(".videoHover").get(0).play();
         }, function () {
            $(this).children(".videoHover").get(0).load();
    }); 



});


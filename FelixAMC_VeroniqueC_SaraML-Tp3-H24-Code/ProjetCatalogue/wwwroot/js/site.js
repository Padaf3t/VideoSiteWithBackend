"use strict"

/**
 * Methode validant la connection de l'utilisateur côté client
 * Vérifie si les chaines de caractères entrées ne sont pas vide 
 * @returns estOk (boolean)
 */
function validerConnection() {
    let estOk = true;
    if ($('#PseudoConnection').val().length === 0) {
        estOk = false;
    }
    if ($('#MotDePasseConnection').val().length === 0) {
        estOk = false;
    }
    return estOk;
}

/**
 * Methode validant l'inscription de l'utilisateur côté client
 * Vérifie si les chaines de caractères entrées respectes les conditions établis
 * @returns message (string) : contient le message d'erreur combinant les erreurs de tous les champs
 */
function validerInscription() {
    let message = [];
    let regExNom = /^[\-/A-Za-z\u00C0-\u017F]+$/;
    let regExMdp = /^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@.#$!%*?&^])[A-Za-z\d@.#$!%*?&]{8,60}$/;
    let regExPseudo = /^\w+$/;

    let pseudo = $('#PseudoInscription').val();
    if (pseudo.length < 5 || pseudo.length > 20 || regExPseudo.exec(pseudo) === null) {
        message.push("Le pseudo doit être entre 5 et 20 caractères inclusivement et ne doit pas contenir de caractère spécial.");
    }

    let motDePasse = $('#MotDePasseInscription').val();
    if (regExMdp.exec(motDePasse) === null) {
        message.push("Le mot de passe doit avoir une longueur de 8 à 60 caractères inclusivement, contenir un chiffre et un caractère spécial (@.#$!%*?&^).");
    }

    let nom = $('#NomInscription').val();
    if (nom.length > 50 || regExNom.test(nom) === null) {
        message.push("Le nom doit avoir 50 caractère ou moins. Il doit être composer de caractère alphanumérique ou '-'.");
    }
    let prenom = $('#PrenomInscription').val();
    if (prenom.length > 50 || regExNom.test(prenom) === null) {
        message .push("Le prénom doit avoir 50 caractère ou moins. Il doit être composer de caractère alphanumérique ou '-'.");
    }
    return message;
}

/**
 * Methode principale appellé à la création du document
 */
$(document).ready(function () {
    //Évènement des bouton .confirmation
    $('.confirmation').on('click', function () {
        return confirm('Êtes vous sûr de supprimer cet utilisateur? Cette action est irréversible!');
    });

    //Évènement pour le bouton #connection-btn
    $('#connection-btn').on('click', function (e) {
        if (!validerConnection()) {
            if ($('#message-connection').length) {
            }
            else {
                $('#message-erreur-connection').prepend($('<div>').prop('class', 'alert alert-danger').prop('id', 'message-connection')
                    .prop('role', 'alert'));
                }
            $('#message-connection').text('Le pseudo ou mot de passe est invalide');
            e.preventDefault();
        }
    });

    //Évènement pour le bouton #inscription-btn
    $('#inscription-btn').on('click', function (e) {
        $('#message-inscription').html('');
        let messageErreur = validerInscription();
        if (messageErreur.length !== 0) {

            if ($('#message-inscription').length) {
                
            }
            else {
                $('#message-erreur-inscription').prepend($('<div>').prop('class', 'alert alert-danger').prop('id', 'message-inscription')
                        .prop('role', 'alert'));
            }
            for (let i = 0; i < messageErreur.length; i++) {
                $('#message-inscription').append($('<p>').text(messageErreur[i]));
            }
            e.preventDefault();
        }
    });

    //Évènement pour les .divhover
    $(".divHover").hover(
        function () {
            $(this).children(".videoHover").get(0).play();
         }, function () {
            $(this).children(".videoHover").get(0).load();
    }); 



});


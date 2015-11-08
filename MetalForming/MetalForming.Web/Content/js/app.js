var app = function() {
    var loadAjax = function () {
        var $body = $("body");
        $(document).on({
            ajaxStart: function () {
                $body.addClass("loadingAjax");
            },
            ajaxStop: function () {
                $body.removeClass("loadingAjax");
            }
        });
    };

    var createMessageDialog = function () {
        var dialogMessage = "<div id='popupMensaje' tabindex='-1' role='dialog' aria-hidden='true' class='modal fade' data-backdrop='static' style='z-index:100000;'>";
        dialogMessage += "<div class='modal-dialog'>";
        dialogMessage += "<div class='modal-content'>";
        dialogMessage += "<div class='modal-header'>";
        dialogMessage += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>';
        dialogMessage += "<h4 class='modal-title text-primary'>Informacion!</h4>";
        dialogMessage += "</div>";
        dialogMessage += "<div class='modal-body'></div>";
        dialogMessage += "<div class='modal-footer' style='margin-top: 0px; margin-bottom: 0px;'>";
        dialogMessage += "<button class='btn btn-primary btn-aceptar' data-dismiss='modal'><i class='fa fa-thumbs-o-up'></i> Aceptar</button>";
        dialogMessage += "</div>";
        dialogMessage += "</div>";
        dialogMessage += "</div>";

        $("body").append(dialogMessage);
    };

    var createConfirmDialog = function () {
        var dialogConfirm = "<div id='popupConfirmacion' tabindex='-1' role='dialog' aria-hidden='true' class='modal fade' data-backdrop='static' style='z-index:100000;'>";
        dialogConfirm += "<div class='modal-dialog'>";
        dialogConfirm += "<div class='modal-content'>";
        dialogConfirm += "<div class='modal-header'><h4 class='modal-title text-primary'>Confirmar!</h4></div>";
        dialogConfirm += "<div class='modal-body'><p></p></div>";
        dialogConfirm += "<div class='modal-footer' style='margin-top: 0px; margin-bottom: 0px;'>";
        dialogConfirm += "<button class='btn btn-primary' data-dismiss='modal'><i class='fa fa-thumbs-o-up'></i> Aceptar</button>";
        dialogConfirm += "<button class='btn btn-danger' data-dismiss='modal'><i class='fa fa-remove'></i> Cancelar</button> ";
        dialogConfirm += "</div>";
        dialogConfirm += "</div>";
        dialogConfirm += "</div>";

        $("body").append(dialogConfirm);
    };

    return {
        init: function () {
            loadAjax();
            createMessageDialog();
            createConfirmDialog();
        },
        showMessageDialog: function (message, fnAceptar, fnCerrar) {
            $('#popupMensaje .modal-body').html(message);
            $('#popupMensaje').modal('show');

            if ($.isFunction(fnAceptar)) {
                $('#popupMensaje .btn-aceptar').off('click');
                $('#popupMensaje .btn-aceptar').on('click', fnAceptar);
            }

            if ($.isFunction(fnCerrar)) {
                $('#popupMensaje .close').off('click');
                $('#popupMensaje .close').on('click', fnCerrar);
            }
        },
        showConfirmDialog: function (message, fnSuccess, fnCancel) {
            var popup = $('#popupConfirmacion');

            var btnSuccess = $(popup).find('.btn-primary');
            var btnCancel = $(popup).find('.btn-danger');

            btnSuccess.off('click');
            if ($.isFunction(fnSuccess)) {
                btnSuccess.on('click', function() { fnSuccess(); });
            }

            btnCancel.off('click');
            if ($.isFunction(fnCancel)) {
                btnCancel.on('click', function() { fnCancel(); });
            }

            if (message != null && message != '') {
                $('#popupConfirmacion .modal-body p').text(message);
            } else {
                $('#popupConfirmacion .modal-body p').text("Esta seguro de realizar esta acción?");
            }

            popup.modal('show');
        },
        disableAllFormElements: function (formId) {
            $('#' + formId).find('input, textarea, button, select').attr('disabled', 'disabled');
        }
    };
}();
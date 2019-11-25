$(document).ready(function () {
    $('.number').keypress(function (event) {
        var keycode = event.which;
        if (!(event.shiftKey == false && (keycode == 46 || keycode == 8 || keycode == 37 || keycode == 39 || (keycode >= 48 && keycode <= 57)))) {
            event.preventDefault();
        }
    });

    $('.alphaonly').keypress(function (event) {
        if (event.shiftKey || event.altKey) {
            event.preventDefault();
        } else {
            var keycode = event.which;
            if ((event.shiftKey == false && (keycode == 46 || keycode == 8 || keycode == 37 || keycode == 39 || (keycode >= 48 && keycode <= 57)))) {
                event.preventDefault();
            }
        }
    });

    $('.alphaWithSpecialChar').keypress(function (event) {
        var keycode = event.which;
        if ((keycode == 46 || keycode == 8 || keycode == 37 || keycode == 39 || (keycode >= 48 && keycode <= 57))) {
            event.preventDefault();
        }
    });

    $('.price').keypress(function (event) {
        var keycode = event.which;
        if (this.value.length == 0 && keycode == 48) {
            event.preventDefault();
        }
        if (!(event.shiftKey == false && (keycode == 46 || keycode == 8 || keycode == 37 || keycode == 39 || (keycode >= 48 && keycode <= 57)))) {
            event.preventDefault();
        }
    });

    $('.alphaNumber').keydown(function (e) {
        if (e.shiftKey || e.altKey) {
            e.preventDefault();
        } else {
            var key = e.keyCode;
            if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
                e.preventDefault();
            }
        }
    });

    $('.StartAlphaOnly').keypress(function (event) {
        var keycode = event.which;
        if (this.value.length < 2 && (keycode == 46 || keycode == 8 || keycode == 37 || keycode == 39 || (keycode >= 48 && keycode <= 57))) {
            event.preventDefault();
        }
    });


    $('.AlphaNumeric').keypress(function () {
        if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
            this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
        }
    });

    $('.amount').keypress(function (event) {
        var keycode = event.which;
        if (this.value.length > 10) {
            event.preventDefault();
        }
        if (!(event.shiftKey == false && (keycode == 46 || keycode == 8 || keycode == 37 || keycode == 39 || (keycode >= 48 && keycode <= 57)))) {
            event.preventDefault();
        }
    });

    $('.no-copy-paste').bind('cut copy paste', function (e) {
        e.preventDefault();
    });

    //$('.capitalize').keyup(function (event) {
    //    var textBox = event.target;
    //    var start = textBox.selectionStart;
    //    var end = textBox.selectionEnd;
    //    textBox.value = textBox.value.charAt(0).toUpperCase() + textBox.value.slice(1);
    //    textBox.setSelectionRange(start, end);

    //});





    $.fn.capitalize = function () {
        $.each(this, function () {
            this.value = this.value.replace(/\b[a-z]/gi, function ($0) {
                return $0.toUpperCase();
            });
            this.value = this.value.replace(/@([a-z])([^.]*\.[a-z])/gi, function ($0, $1) {
                console.info(arguments);
                return '@' + $0.toUpperCase() + $1.toLowerCase();
            });
        });
    }

    $(".capitalize").keyup(function () {
        $(this).capitalize();
    }).capitalize();

});
$(document).ready(function () {

    $('#nav-left').on('affix.bs.affix', function () {
        $(this).width($(this).width() - 1);
        $('#main').addClass('col-md-offset-2');

    }).on('affix-top.bs.affix', function () {
        $(this).width($(this).width() - 1);
        $('#main').addClass('col-md-offset-2');
    });
});
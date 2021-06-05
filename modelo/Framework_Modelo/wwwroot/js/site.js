$(document).ready(function () {

    $(".cpfcnpj").inputmask({ mask: ['999.999.999-99', '99.999.999/9999-99'], keepStatic: true });
    $(".cpf").inputmask({ mask: ['999.999.999-99'], keepStatic: true });
    $(".cnpj").inputmask({ mask: ['99.999.999/9999-99'], keepStatic: true });
    Inputmask('datetime', { 'inputFormat': 'HH:MM' }).mask($(".hora"));
    Inputmask('datetime', { 'inputFormat': 'dd/mm/yyyy' }).mask($(".data"));

});
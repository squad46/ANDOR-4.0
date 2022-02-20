
$("#feed-trabalhos").click(function ()
{
    $("#trabalhos").show();
    $("#moradias").hide();
    $("#informacoes").hide();
    $("#ongs").hide();
});

$("#feed-moradias").click(function ()
{
    $("#trabalhos").hide();
    $("#moradias").show();
    $("#informacoes").hide();
    $("#ongs").hide();
});

$("#btn-perfil").click(function ()
{
    $("#perfil").show();
    $("#formacao").hide();
    $("#experiencia").hide();
    $("#trabalho").hide();
    $("#moradia").hide();
});

$("#btn-formacao").click(function () {
    $("#perfil").hide();
    $("#formacao").show();
    $("#experiencia").hide();
    $("#trabalho").hide();
    $("#moradia").hide();
});

$("#btn-experiencia").click(function () {
    $("#perfil").hide();
    $("#formacao").hide();
    $("#experiencia").show();
    $("#trabalho").hide();
    $("#moradia").hide();
});

$("#btn-trabalho").click(function () {
    $("#perfil").hide();
    $("#formacao").hide();
    $("#experiencia").hide();
    $("#trabalho").show();
    $("#moradia").hide();
});

$("#btn-moradia").click(function () {
    $("#perfil").hide();
    $("#formacao").hide();
    $("#experiencia").hide();
    $("#trabalho").hide();
    $("#moradia").show();
});




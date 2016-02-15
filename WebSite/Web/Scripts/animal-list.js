function AnimalListModel() {
    
}
ko.applyBindings(new AnimalListModel());



function onAccordionClick() {
    $(this).parent().find(".js-item").slideToggle();
    $(this).toggleClass("active");
}

$(".js-menu-database").addClass("active");
$(".js-accordion").click(onAccordionClick)

//debug
$(".breed-type .js-accordion").trigger("click");
$(".found-time .js-accordion").trigger("click");
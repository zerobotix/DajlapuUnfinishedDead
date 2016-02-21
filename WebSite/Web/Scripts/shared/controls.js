// js-accordion
function onAccordionClick() {
    $(this).closest(".js-accordion").find(".js-body").slideToggle();
    $(this).toggleClass("js-active");
}
$(".js-accordion").find(".js-header").click(onAccordionClick);

// "active" knockout binding = add class "js-active" on checked / radio-selected elements
ko.bindingHandlers.active = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        console.log('init', element, valueAccessor())

        $(element).click(function () {
            var accessor = valueAccessor();
            console.log('click', element, accessor)

            var isArray = $.isArray(accessor.state());

            if (isArray) { // checkbox logic
                var containsValue = accessor.state.indexOf(accessor.value) > -1;
                if (containsValue) {
                    accessor.state.remove(accessor.value);
                } else {
                    accessor.state.push(accessor.value);
                }
            } else { // radio logic
                if (accessor.state() != accessor.value) {
                    accessor.state(accessor.value);
                }
            }
        });
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var accessor = valueAccessor();
        console.log('update', element, accessor)

        var shouldHaveClass;
        var isArray = $.isArray(accessor.state());

        if (isArray) {
            shouldHaveClass = accessor.state.indexOf(accessor.value) > -1;
        } else {
            shouldHaveClass = valueAccessor().state() == valueAccessor().value;
        }

        ko.utils.toggleDomNodeCssClass(element, "js-active", shouldHaveClass);
    }
};
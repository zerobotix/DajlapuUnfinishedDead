function AnimalAddModel(model, enums) {
    var self = this;

    this.input = {
        advertType: ko.observable(model.AdvertType),
        animal: ko.observable(enums.AnimalTypes.Dog),
        sex: ko.observable(""),
        size: ko.observable(""),
        colors: ko.observableArray([]),
        breed: ko.observable(""),
        status: ko.observable(""),

        foundTime: ko.observable(""),
        contactPhone: ko.observable(""),
        contactEmail: ko.observable(""),
        description: ko.observable(""),
        photos: ko.observableArray([])
    }

    this.available = {
        colors: ko.computed(function() {
            if (self.input.animal() === enums.AnimalTypes.Dog) {
                return model.DogColors;
            } else {
                return model.CatColors;
            }
        }),
        breeds: ko.computed(function() {
            if (self.input.animal() === enums.AnimalTypes.Dog) {
                return model.DogBreeds;
            } else {
                return model.CatBreeds;
            }
        })
    }

    this.formColor = ko.computed(function () {
        switch (self.input.advertType()) {
            case enums.AdvertTypes.Found:
                return "js-green";
            case enums.AdvertTypes.Lost:
                return "js-blue";
            default:
                return "js-black";
        }
    });

    this.addButtonClick = function() {
        var input = self.input;

        var colorIds = [];
        for (var i = 0; i < input.colors().length; i++) {
            colorIds.push(input.colors()[i].Id);
        }
        //map to ids;

        $.ajax({
            url: "/Animal/Add",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({
                AdvertType: input.advertType(),
                Animal: input.animal(),
                Sex: input.sex(),
                Size: input.size(),
                ColorIds: colorIds,
                BreedId: input.breed().Id,
                Status: input.status(),

                FoundTime: input.foundTime(),
                Phone: input.contactPhone(),
                Email: input.contactEmail(),
                Description: input.description(),

                Photos: input.photos()

                //LocationXY: '',
            }),
            success: function (response) {
                if (response.Success) {
                    alert(JSON.stringify(response));
                } else {
                    throw "server-side error";
                }
            },
            error: function() {
                throw "ajax-error";
            }
        });
    }

    // todo: переделать костыли в кастомный элемент
    // почему тут костыль:
    // при изменении животного, нокаут обновляет <option> внутри <select>. после того, как селект обновился, нужно триггернуть chosen:updated
    // 1) если colors.subscribe без таймаута, то селект к этому времени ещё не обновлён
    // 2) если js-select.onchange - срабатывает только при изменении выделенного пункта
    // 3) если юзать биндинг optionsAfterRender - срабатывает при добавлении каждого <option>,
    // старые элементы удаляются после добавления и этот биндинг не срабатывает, поэтому в дропдауне получаются варианты и для собак и для котов
    // можно попробовать заюзать foreach и его afterRender, но это всё равно како-то говно, поэтому надо делать компонент.
    self.available.colors.subscribe(function () {
        setTimeout(function() {
            $("#js-color-select").trigger("chosen:updated");
        }, 100);
    });
    self.available.breeds.subscribe(function () {
        setTimeout(function () {
            $("#js-breed-select").trigger("chosen:updated");
        }, 100);
    });
    
}

try {
    ko.applyBindings(new AnimalAddModel(ServerModel, Enums));
} catch (e) {
    alert(e.name + ":" + e.message + "\n" + e.stack);
}

if (ServerModel.AdvertType === Enums.AdvertTypes.Found) {
    $(".js-menu-found").addClass("js-active");
} else {
    $(".js-menu-lost").addClass("js-active");
}

$.datetimepicker.setLocale("ru");

var defaultDatetimepickerOptions = {
    //inline: true,
    timepicker: true,
    dayOfWeekStart: 1,
    scrollMonth: false,
    yearStart: 2015,
    yearEnd: 2017,
    formatDate: "d.m.Y",
    minDate: "01.01.2016",
    maxDate: 0
}

$("#js-found-date").datetimepicker(defaultDatetimepickerOptions);

(function createDropdowns() {
    $("#js-color-select").chosen({
        disable_search: true,
        inherit_select_classes: true,
        width: "100%",
        placeholder_text_multiple: "окрас"
    });
    $("#js-breed-select").chosen({
        allow_single_deselect: true,
        inherit_select_classes: true,
        placeholder_text_single: "порода",
        no_results_text: "выберите породу \"другая\", потому что у нас нет породы",
        width: "100%",
    });
})();
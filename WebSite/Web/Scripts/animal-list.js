function AnimalListModel(model, enums) {
    var self = this;

    this.input = {
        advertType: ko.observable(enums.AdvertTypes.Found),
        animal: ko.observable(enums.AnimalTypes.Dog),
        sexes: ko.observableArray([]),
        sizes: ko.observableArray([]),
        colors: ko.observableArray([]),
        breeds: ko.observableArray([]),
        statuses: ko.observableArray([]),
        startDate: ko.observable(""),
        finishDate: ko.observable(""),
    }

    this.available = {
        colors: model.Colors,
        breeds: model.Breeds
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

    this.searchResults = ko.observableArray([]);
    this.totalResultsCount = ko.observable();

    this.isResultsLoading = ko.observable(false);

    function loadResults() {
        self.isResultsLoading(true);

        var input = self.input;

        // todo: $.map to ids;
        var colorIds = [];
        for (var i = 0; i < input.colors().length; i++) {
            colorIds.push(input.colors()[i].Id);
        }

        var breedIds = [];
        for (var i = 0; i < input.breeds().length; i++) {
            breedIds.push(input.breeds()[i].Id);
        }

        $.ajax({
            url: "/Animal/List",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({
                //locationTopLeft: '',
                //locationBottomRight: '',
                AdvertType: input.advertType(),
                Animal: input.animal(),
                Sexes: input.sexes(),
                Sizes: input.sizes(),
                ColorIds: colorIds,
                BreedIds: breedIds,
                StartDate: input.startDate(), //todo: передаём строку, но в контроллер приходит DateTime - может вылезти проблема с локализацией
                FinishDate: input.finishDate(), //todo: будет ли правильнее передавать new Date() из джаваскрипта?
                Statuses: input.statuses(),
                //withMapPoints? // в первый раз мы загружаем часть shortInfo и все точки на карту
                                 // в последующие разы мы загружаем последующие части shortInfo БЕЗ точек на карту
            }),
            success: function (response) {
                self.totalResultsCount(response.TotalResultsCount);

                var searchResults = [];
                $.each(response.ShortInfoList, function (index, item) {
                    var status = item.AnimalStatus === "empty" ? "" : item.AnimalStatus;
                    //todo: write getStatusStyle(enumStatus) = ".js-status" or extend enum object to have a function
                    var shortInfo = {
                        status: item.AnimalStatus,
                        imgUrl: item.MediumPhotoUrl,
                        id: item.AnimalId,
                        text: status,
                        style: "js-" + status,
                    };
                    searchResults.push(shortInfo);
                });

                self.isResultsLoading(false);
                self.searchResults(searchResults);
            }
        });
    }

    self.input.advertType.subscribe(loadResults);
    self.input.animal.subscribe(loadResults);
    self.input.sexes.subscribe(loadResults);
    self.input.sizes.subscribe(loadResults);
    self.input.colors.subscribe(loadResults);
    self.input.breeds.subscribe(loadResults);
    self.input.statuses.subscribe(loadResults);
    self.input.startDate.subscribe(function() {
        console.log(self.input.startDate());
    });

    loadResults();
}

try {
    ko.applyBindings(new AnimalListModel(ServerModel, Enums));
} catch (e) {
    alertException(e);
}

$(".js-menu-database").addClass("js-active");

$.datetimepicker.setLocale("ru");

var defaultDatetimepickerOptions = {
    inline: true,
    timepicker: false,
    dayOfWeekStart: 1,
    scrollMonth: false,
    yearStart: 2015,
    yearEnd: 2017,
    formatDate: "d.m.Y",
    minDate: "05.03.2016",
    maxDate: 0,
}

$("#js-start-date").datetimepicker(defaultDatetimepickerOptions);
$("#js-finish-date").datetimepicker(defaultDatetimepickerOptions);

function setMinFinishDate(ct) {
    $("#js-finish-date").datetimepicker("setOptions", { minDate: ct });
}

function setMaxStartDate(ct) {
    $("#js-start-date").datetimepicker("setOptions", { maxDate: ct });
}

$("#js-start-date").datetimepicker("setOptions", { onSelectDate: setMinFinishDate });
$("#js-finish-date").datetimepicker("setOptions", { onSelectDate: setMaxStartDate });

//debug
//$(".breed-type .js-header").trigger("click");
$(".found-time .js-header").trigger("click");
//$(".sex-type .js-header").trigger("click");
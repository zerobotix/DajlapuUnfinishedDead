function AnimalListModel(model, enums) {
    var self = this;

    this.input = {
        advertType: ko.observable(enums.AdvertTypes.Found),
        animal: ko.observable(enums.AnimalTypes.Dog),
        sexes: ko.observableArray([]),
        sizes: ko.observableArray([]),
        colors: ko.observableArray([]),
        breeds: ko.observableArray([]),
        statuses: ko.observableArray([])
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
                Colors: input.colors(),
                Breeds: input.breeds(),
                //startDate: '',
                //finishDate: '',
                Statuses: input.statuses(),
                //withMapPoints? // в первый раз мы загружаем часть shortInfo и все точки на карту
                                 // в последующие разы мы загружаем последующие части shortInfo БЕЗ точек на карту
            }),
            success: function (response) {
                self.totalResultsCount(response.TotalResultsCount);

                var searchResults = [];
                $.each(response.ShortInfoList, function (index, item) {
                    var status = item.AnimalStatus === "empty" ? "" : item.AnimalStatus; //todo: write getStatusStyle(enumStatus) = ".js-status"
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

    loadResults();
}

try {
    ko.applyBindings(new AnimalListModel(ServerModel, Enums));
} catch (e) {
    alertException(e);
}

$(".js-menu-database").addClass("js-active");

//debug
//$(".breed-type.js-accordion").trigger("click");
//$(".found-time.js-accordion").trigger("click");
//$(".sex-type .js-header").trigger("click");


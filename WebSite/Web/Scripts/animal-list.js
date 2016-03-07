try {

var enums = { // todo: share enums from C# to JS
    SearchTypes: {
    }
}

function AnimalListModel() {
    var self = this;

    this.searchType = ko.observable("found");
    this.animalType = ko.observable("dog");
    this.sexTypes = ko.observableArray([]);
    this.sizeTypes = ko.observableArray([]);
    this.colorTypes = ko.observableArray([]);
    this.breedTypes = ko.observableArray([]);
    this.animalStatuses = ko.observableArray([]);

    this.availableColors = [
        { name: "красный", id: 0 },
        { name: "синий", id: 1 },
        { name: "желтый", id: 2 },
    ];
    
    this.availableBreeds = [
        { name: "порода 1", id: 0 },
        { name: "порода 2", id: 1 },
        { name: "порода 3", id: 2 },
    ];

    this.formColor = ko.computed(function () {
        switch (self.searchType()) {
            case "found":
                return "js-green";
            case "lost":
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

        $.ajax({
            url: "/Animal/List",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({
                //locationTopLeft: '',
                //locationBottomRight: '',
                SearchType: self.searchType(),
                AnimalType: self.animalType(),
                SizeTypes: self.sizeTypes(),
                ColorTypes: self.colorTypes(),
                BreedTypes: self.breedTypes(),
                //startDate: '',
                //finishDate: '',
                AnimalStatuses: self.animalStatuses(),
                //withMapPoints? // в первый раз мы загружаем часть shortInfo и все точки на карту
                                 // в последующие разы мы загружаем последующие части shortInfo БЕЗ точек на карту
            }),
            success: function (response) {
                self.totalResultsCount(response.TotalResultsCount);

                var searchResults = [];
                $.each(response.ShortInfoList, function (index, item) {
                    var status = item.AnimalStatus === "empty" ? "" : item.AnimalStatus;
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

    self.searchType.subscribe(loadResults);
    self.animalType.subscribe(loadResults);
    self.sexTypes.subscribe(loadResults);
    self.sizeTypes.subscribe(loadResults);
    self.colorTypes.subscribe(loadResults);
    self.breedTypes.subscribe(loadResults);
    self.animalStatuses.subscribe(loadResults);

    loadResults();
}
ko.applyBindings(new AnimalListModel());

$(".js-menu-database").addClass("js-active");

} catch(e) {
    alert(e.name + ":" + e.message + "\n" + e.stack);
}

//debug
//$(".breed-type.js-accordion").trigger("click");
//$(".found-time.js-accordion").trigger("click");
//$(".sex-type .js-header").trigger("click");


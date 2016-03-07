try {

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
}
ko.applyBindings(new AnimalAddModel(ServerModel, Enums));

if (ServerModel.AdvertType === Enums.AdvertTypes.Found) {
    $(".js-menu-found").addClass("js-active");
} else {
    $(".js-menu-lost").addClass("js-active");
}

} catch (e) {
    alert(e.name + ":" + e.message + "\n" + e.stack);
}
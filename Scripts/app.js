var ViewModel = function () {
    var self = this;
    self.pumaTypes = ko.observableArray();
    self.error = ko.observable();

    var booksUri = '/api/puma_types/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllPumaTypes() {
        ajaxHelper(pumaTypesUri, 'GET').done(function (data) {
            self.pumaTypes(data);
        });
    }

    // Fetch the initial data.
    getAllPumaTypes();
};

ko.applyBindings(new ViewModel());
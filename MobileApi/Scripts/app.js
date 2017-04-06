var ViewModel = function () {
    var self = this;
    self.pumas = ko.observableArray();
    self.error = ko.observable();

    var booksUri = '/api/pumas/';

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

    function getAllPumas() {
        ajaxHelper(pumasUri, 'GET').done(function (data) {
            self.pumas(data);
        });
    }

    // Fetch the initial data.
    getAllPumas();
};

ko.applyBindings(new ViewModel());
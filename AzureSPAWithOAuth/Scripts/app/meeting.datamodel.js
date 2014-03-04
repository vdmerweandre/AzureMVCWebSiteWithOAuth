ko.observable.fn.store = function () {
    var self = this;
    var oldValue = self();
    var observable = ko.computed({
        read: function () {
            return self();
        },
        write: function (value) {
            oldValue = self();
            self(value);
        }
    });
    this.revert = function () {
        self(oldValue);
    }
    this.commit = function () {
        oldValue = self();
    }
    return this;
}

// Model
function Meeting(data) {
    var self = this;
    data = data || {};

    //Persisted properties
    self.Id = data.Id || '';
    self.VenueName = ko.observable(data.VenueName).store();
    self.RGB = ko.observable(data.RGB).store();
    self.NumberOfRaces = ko.observable(data.NumberOfRaces).store();
    self.Date = ko.observable(data.Date).store();
    self.TabcorpReqCode = ko.observable(data.TabcorpReqCode).store();
    self.TabcorpDispCode = ko.observable(data.TabcorpDispCode).store();
    self.TabLtdReqCode = ko.observable(data.TabLtdReqCode).store();
    self.TabLtdReqDispCode = ko.observable(data.TabLtdReqDispCode).store();
    self.UniTABReqCode = ko.observable(data.UniTABReqCode).store();
    self.UniTABReqDispCode = ko.observable(data.UniTABReqDispCode).store();
    self.RaceStarts = ko.observable(data.RaceStarts).store();
    self.Coverages = ko.observable(data.Coverages).store();

    self.editing = ko.observable(false || self.Id == "");
    self.new = ko.observable(self.Id == "");

    self.save = function (item) {
        commitChanges(item);
        dataContext.updateMeeting(item);
        self.editing(false);
    };

    self.cancel = function (item) {
        revertChanges(item);
        self.editing(false);
    };

    self.edit = function (item) {
        self.editing(true);
    }

    function commitChanges(item) {
        for (var prop in item) {
            if (item.hasOwnProperty(prop) && item[prop].commit) {
                item[prop].commit();
            }
        }
    }

    function revertChanges(item) {
        for (var prop in item) {
            if (item.hasOwnProperty(prop) && item[prop].revert) {
                item[prop].revert();
            }
        }
    }
}

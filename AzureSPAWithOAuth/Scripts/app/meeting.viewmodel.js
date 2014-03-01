// ViewModel
function MeetingViewModel(app, dataModel) {
    var self = this;
    
    self.error = ko.observable();
    self.meetings = ko.observableArray();
    self.adding = ko.observable(false);

    self.add = function () {
        //push 'empty' meeting to allow for new meeting creation
        self.meetings.push(new Meeting(''));
        self.adding(true);
    }

    self.create = function (meeting) {
        // If valid, post the serialized form data to the web api
        //$(formElement).validate();
        //if ($(meeting).valid()) {
        //formElement.elements["VenueName"].value
        //formElement
        dataContext.createMeeting(meeting, function (response) {
            meeting.new(false);
            meeting.editing(false);
            meeting.Id = response.Id;
            self.adding(false);
        });
    }

    self.cancel = function (meeting) {
        self.meetings.pop();
        self.adding(false);
    }

    self.remove = function (meeting) {
        if (confirm('Do you really want to delete this meeting?')) {
            // First remove from the server, then from the UI
            dataContext.deleteMeeting(meeting, function () {
                self.meetings.remove(meeting);
            });
        }
    }

    self.get = function (uri) {
        dataContext.get(uri, self.meetings, self.error);
    }

    self.get("Meetings");
    //dataContext.get("Meetings", self.meetings, self.error);

    self.AllMeetingUrl = "Meetings";
    self.AllMeetingSelectIDVenueNameUrl = "Meetings/?$select=Id,VenueName";
}

app.addViewModel({
    name: "Meeting",
    bindingMemberName: "meetings",
    factory: MeetingViewModel,
    navigatorFactory: function (app) {
        return function () {
            app.errors.removeAll();
            app.view(app.Views.Meeting);
        };
    }
});

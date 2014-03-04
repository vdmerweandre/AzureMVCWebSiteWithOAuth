// DataContext
var meetingApiUrl = 'http://azureracedatawebapi.azurewebsites.net/odata';
var dataContext = {

    get: function (uri, meetingsObservable, errorObservable) {

        var jsonHandler = {
            read: function (response, context) {
                var contentType = response.headers["Content-Type"];
                if (contentType && contentType.indexOf("application/json") === 0) {
                    response.data = response.body;
                } else {
                    OData.defaultHandler.read(response, context);
                }
            },
            write: function (request, context) { }
        }

        OData.defaultHandler = jsonHandler;

        OData.read({
            requestUri: meetingApiUrl+'/'+uri,
            headers: getHeaders("GET")
        },
        onSuccess,
        onError
        );

        function onSuccess(data, request) {
            var js = JSON.parse(data);
            var meetingList = new Array();
            for (var i = 0, tot = js.value.length; i < tot; i++) {
                var meeting = new Meeting(js.value[i]);
                meetingList.push(meeting);
            };
            meetingsObservable(meetingList);
        };
        function onError(data) {
            alert("Error occurred " + JSON.stringify(data));
        };
      
    },

    deleteMeeting: function (meeting, successCallBack) {

        var uri = meetingApiUrl + "(" + meeting.Id + ")";
        var type = "DELETE";

        OData.request(
            {
                requestUri: uri,
                method: type,
                headers: getHeaders(type),
                body: meetingToData(meeting, type)
            },
            onSuccess,
            onError
            );

        function onSuccess(data, response) {
            //alert("Deleted");
            if (successCallBack && typeof (successCallBack) === "function") {
                successCallBack();
            }
        }

        function onError(data) {
            //console.log(JSON.stringify(data));
            alert("Error occured " + JSON.stringify(data.response));
        }
    },

    updateMeeting: function (meeting, successCallBack) {

        var uri = meetingApiUrl + "(" + meeting.Id + ")";;
        var type = "PUT";

        OData.request(
            {
                requestUri: uri,
                method: type,
                headers: getHeaders(type),
                body: meetingToData(meeting, type)
            },
            onSuccess,
            onError
            );

        function onSuccess(data, response) {
            //alert("Saved");

            if (successCallBack && typeof (successCallBack) === "function") {
                successCallBack(meeting);
            }
        }

        function onError(data) {
            //console.log(JSON.stringify(data));
            alert("Error occured " + JSON.stringify(data.response));
        }
    },

    createMeeting: function (meeting, successCallBack) {

        var uri = meetingApiUrl;
        var type = "POST";

        OData.request(
            {
                requestUri: uri,
                method: type,
                headers: getHeaders(type),
                body: meetingToData(meeting, type)
            },
            onSuccess,
            onError
            );

        function onSuccess(data, response) {
            //alert("Saved");
            if (data)
            {
                var js = JSON.parse(data);
                meeting.Id = js.Id;
            }

            if (successCallBack && typeof (successCallBack) === "function") {
                successCallBack(meeting);
            }
        }

        function onError(data) {
            //console.log(JSON.stringify(data));
            alert("Error occured " + JSON.stringify(data.response));
        }
    }
}

function meetingToData(meeting, type) {

    var mapping = {};
    if (type == "POST")
    {
        mapping = {
            'ignore': ["editing", "Id", "new"]
        }
    }
    else {
        mapping = {
            'ignore': ["editing", "new"]
        }
    }

    return ko.mapping.toJSON(meeting, mapping);
}

function getHeaders(type) {

    var oHeaders = {};
    if (type == "GET")
    {
        oHeaders = {
            "Accept": "application/json",
            "dataserviceversion": "3.0",
            "maxdataserviceversion": "3.0"
        };
    }
    else
    {
        oHeaders = {
            "Accept": "application/json",
            "MaxDataServiceVersion": "3.0",
            "DataServiceVersion": "3.0",
            "Content-Type": "application/json; odata=minimalmetadata; charset=utf-8"
        };
    }

    return oHeaders;
}

function ajaxRequest(type, url, data, dataType) { // Ajax helper
    var options = {
        dataType: dataType || "json",
        contentType: "application/json",
        cache: false,
        type: type,
        data: data ? data.toJson() : null
    };

    var antiForgeryToken = $("#antiForgeryToken").val();
    if (antiForgeryToken) {
        options.headers = {
            'RequestVerificationToken': antiForgeryToken
        }
    }
    return $.ajax(url, options);
}

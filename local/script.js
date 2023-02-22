/*
 * Tripscribe API
 * An ASP.NET Core Web API for the Tripscribe backend
 *
 * OpenAPI spec version: v1
 *
 * NOTE: This class is auto generated by OpenAPI Generator.
 * https://github.com/OpenAPITools/openapi-generator
 *
 * OpenAPI generator version: 6.5.0-SNAPSHOT
 */


import http from "k6/http";
import { group, check, sleep } from "k6";

const BASE_URL = "http://localhost:5126";
// Sleep duration between successive requests.
// You might want to edit the value of this variable or remove calls to the sleep function on the script.
const SLEEP_DURATION = 0.1;
// Global variables should be initialized.

export default function() {
    // group("/api/Reviews", () => {

    //     // Request No. 1
    //     {
    //         let url = BASE_URL + `/api/Reviews`;
    //         // TODO: edit the parameters of the request body.
    //         let body = {"reviewText": "string", "score": "integer", "timestamp": "date"};
    //         let params = {headers: {"Content-Type": "application/json", "Accept": "application/json"}};
    //         let request = http.post(url, JSON.stringify(body), params);

    //         check(request, {
    //             "Success": (r) => r.status === 201
    //         });
    //     }
    // });

    group("/api/Accounts/{id}/trips", () => {
        let id = '1'; // specify value as there is no example value for this parameter in OpenAPI spec

        // Request No. 1
        {
            let url = BASE_URL + `/api/Accounts/${id}/trips`;
            let request = http.get(url);

            check(request, {
                "Success": (r) => r.status === 200
            });
        }
    });

    // group("/api/Locations/{id}/reviews", () => {
    //     let id = 'TODO_EDIT_THE_ID'; // specify value as there is no example value for this parameter in OpenAPI spec

    //     // Request No. 1
    //     {
    //         let url = BASE_URL + `/api/Locations/${id}/reviews`;
    //         let request = http.get(url);

    //         check(request, {
    //             "Success": (r) => r.status === 200
    //         });
    //     }
    // });

    group("/api/Locations/{id}", () => {
        let id = '1'; // specify value as there is no example value for this parameter in OpenAPI spec

        // Request No. 1
        {
            let url = BASE_URL + `/api/Locations/${id}`;
            let request = http.get(url);

            check(request, {
                "Success": (r) => r.status === 200
            });

            sleep(SLEEP_DURATION);
        }

        // Request No. 2
        {
            let url = BASE_URL + `/api/Locations/${id}`;
            // TODO: edit the parameters of the request body.
            let body = {"name": "string", "dateVisited": "date", "latitude": "double", "longitude": "double", "locationType": "string"};
            let params = {headers: {"Content-Type": "application/json", "Accept": "application/json"}};
            let request = http.patch(url, JSON.stringify(body), params);

            check(request, {
                "Success": (r) => r.status === 204
            });
        }
    });

    group("/api/Trips", () => {
        let startTime = 'TODO_EDIT_THE_STARTTIME'; // specify value as there is no example value for this parameter in OpenAPI spec
        let endTime = 'TODO_EDIT_THE_ENDTIME'; // specify value as there is no example value for this parameter in OpenAPI spec
        let title = 'Eiffel'; // specify value as there is no example value for this parameter in OpenAPI spec

        // Request No. 1
        {
            let url = BASE_URL + `/api/Trips?title=${title}&startTime=${startTime}&endTime=${endTime}`;
            let request = http.get(url);

            check(request, {
                "Success": (r) => r.status === 200
            });

            sleep(SLEEP_DURATION);
        }

        // Request No. 2
        {
            let url = BASE_URL + `/api/Trips`;
            // TODO: edit the parameters of the request body.
            let body = {"title": "string", "description": "string", "publicView": "boolean", "accounts": "list"};
            let params = {headers: {"Content-Type": "application/json", "Accept": "application/json"}};
            let request = http.post(url, JSON.stringify(body), params);

            check(request, {
                "Success": (r) => r.status === 201
            });
        }
    });

    group("/api/Locations", () => {
        let endDate = 'TODO_EDIT_THE_ENDDATE'; // specify value as there is no example value for this parameter in OpenAPI spec
        let latitude = 'TODO_EDIT_THE_LATITUDE'; // specify value as there is no example value for this parameter in OpenAPI spec
        let name = 'TODO_EDIT_THE_NAME'; // specify value as there is no example value for this parameter in OpenAPI spec
        let locationType = 'TODO_EDIT_THE_LOCATIONTYPE'; // specify value as there is no example value for this parameter in OpenAPI spec
        let stopId = 'TODO_EDIT_THE_STOPID'; // specify value as there is no example value for this parameter in OpenAPI spec
        let startDate = 'TODO_EDIT_THE_STARTDATE'; // specify value as there is no example value for this parameter in OpenAPI spec
        let longitude = 'TODO_EDIT_THE_LONGITUDE'; // specify value as there is no example value for this parameter in OpenAPI spec

        // Request No. 1
        {
            let url = BASE_URL + `/api/Locations?name=${name}&locationType=${locationType}&startDate=${startDate}&endDate=${endDate}&latitude=${latitude}&longitude=${longitude}&stopId=${stopId}`;
            let request = http.get(url);

            check(request, {
                "Success": (r) => r.status === 200
            });

            sleep(SLEEP_DURATION);
        }

        // Request No. 2
        {
            let url = BASE_URL + `/api/Locations`;
            // TODO: edit the parameters of the request body.
            let body = {"name": "string", "dateVisited": "date", "locationType": "string", "latitude": "double", "longitude": "double", "stopId": "integer"};
            let params = {headers: {"Content-Type": "application/json", "Accept": "application/json"}};
            let request = http.post(url, JSON.stringify(body), params);

            check(request, {
                "Success": (r) => r.status === 201
            });
        }
    });

    // group("/api/Reviews/{id}", () => {
    //     let id = 'TODO_EDIT_THE_ID'; // specify value as there is no example value for this parameter in OpenAPI spec

    //     // Request No. 1
    //     {
    //         let url = BASE_URL + `/api/Reviews/${id}`;
    //         // TODO: edit the parameters of the request body.
    //         let body = {"reviewText": "string", "score": "integer"};
    //         let params = {headers: {"Content-Type": "application/json", "Accept": "application/json"}};
    //         let request = http.put(url, JSON.stringify(body), params);

    //         check(request, {
    //             "Success": (r) => r.status === 204
    //         });

    //         sleep(SLEEP_DURATION);
    //     }

    //     // Request No. 2
    //     {
    //         let url = BASE_URL + `/api/Reviews/${id}`;
    //         let request = http.del(url);

    //         check(request, {
    //             "Success": (r) => r.status === 204
    //         });
    //     }
    // });

    group("/api/Stops/{id}", () => {
        let id = '1'; // specify value as there is no example value for this parameter in OpenAPI spec

        // Request No. 1
        {
            let url = BASE_URL + `/api/Stops/${id}`;
            let request = http.get(url);

            check(request, {
                "Success": (r) => r.status === 200
            });

            sleep(SLEEP_DURATION);
        }

        // Request No. 2
        {
            let url = BASE_URL + `/api/Stops/${id}`;
            // TODO: edit the parameters of the request body.
            let body = {"id": "integer", "name": "string", "dateArrived": "date", "dateDeparted": "date"};
            let params = {headers: {"Content-Type": "application/json", "Accept": "application/json"}};
            let request = http.patch(url, JSON.stringify(body), params);

            check(request, {
                "Success": (r) => r.status === 204
            });
        }
    });

    // group("/api/Trips/{id}/reviews", () => {
    //     let id = 'TODO_EDIT_THE_ID'; // specify value as there is no example value for this parameter in OpenAPI spec

    //     // Request No. 1
    //     {
    //         let url = BASE_URL + `/api/Trips/${id}/reviews`;
    //         let request = http.get(url);

    //         check(request, {
    //             "Success": (r) => r.status === 200
    //         });
    //     }
    // });

    // group("/api/Accounts/{id}/reviews", () => {
    //     let id = 'TODO_EDIT_THE_ID'; // specify value as there is no example value for this parameter in OpenAPI spec

    //     // Request No. 1
    //     {
    //         let url = BASE_URL + `/api/Accounts/${id}/reviews`;
    //         let request = http.get(url);

    //         check(request, {
    //             "Success": (r) => r.status === 200
    //         });
    //     }
    // });

    // group("/api/Stops/{Id}/reviews", () => {
    //     let id = 'TODO_EDIT_THE_ID'; // specify value as there is no example value for this parameter in OpenAPI spec

    //     // Request No. 1
    //     {
    //         let url = BASE_URL + `/api/Stops/${Id}/reviews`;
    //         let request = http.get(url);

    //         check(request, {
    //             "Success": (r) => r.status === 200
    //         });
    //     }
    // });

    group("/api/Authentication", () => {

        // Request No. 1
        {
            let url = BASE_URL + `/api/Authentication`;
            let request = http.get(url);

            check(request, {
                "Success": (r) => r.status === 200
            });

            sleep(SLEEP_DURATION);
        }

        // Request No. 2
        {
            let url = BASE_URL + `/api/Authentication`;
            let body = {"email": "jdoe2002@example.com", "password": "p445word"};
            let params = {headers: {"Content-Type": "application/json", "Accept": "application/json"}};
            let request = http.post(url, JSON.stringify(body), params);

            check(request, {
                "Success": (r) => r.status === 200
            });
        }
    });

    group("/api/Trips/{id}/stops", () => {
        let id = '1'; // specify value as there is no example value for this parameter in OpenAPI spec

        // Request No. 1
        {
            let url = BASE_URL + `/api/Trips/${id}/stops`;
            let request = http.get(url);

            check(request, {
                "Success": (r) => r.status === 200
            });
        }
    });

    group("/api/Trips/{id}/accounts", () => {
        let id = '1'; // specify value as there is no example value for this parameter in OpenAPI spec

        // Request No. 1
        {
            let url = BASE_URL + `/api/Trips/${id}/accounts`;
            let request = http.get(url);

            check(request, {
                "Success": (r) => r.status === 200
            });
        }
    });

    group("/api/Accounts", () => {
        let firstName = 'John'; // specify value as there is no example value for this parameter in OpenAPI spec
        let lastName = 'Doe'; // specify value as there is no example value for this parameter in OpenAPI spec
        let email = 'jdoe'; // specify value as there is no example value for this parameter in OpenAPI spec

        // Request No. 1
        {
            let url = BASE_URL + `/api/Accounts?email=${email}&firstName=${firstName}&lastName=${lastName}`;
            let request = http.get(url);

            check(request, {
                "Success": (r) => r.status === 200
            });

            sleep(SLEEP_DURATION);
        }

        // Request No. 2
        {
            let url = BASE_URL + `/api/Accounts`;
            // TODO: edit the parameters of the request body.
            let body = {"firstName": "Gordon", "lastName": "Freeman", "email": "mrfreeman@blackmesa.com", "password": "h3adcrab"};
            let params = {headers: {"Content-Type": "application/json", "Accept": "application/json"}};
            let request = http.post(url, JSON.stringify(body), params);

            check(request, {
                "Success": (r) => r.status === 201
            });
        }
    });

    group("/api/Accounts/{id}", () => {
        let id = '1'; // specify value as there is no example value for this parameter in OpenAPI spec

        // Request No. 1
        {
            let url = BASE_URL + `/api/Accounts/${id}`;
            let request = http.get(url);

            check(request, {
                "Success": (r) => r.status === 200
            });

            sleep(SLEEP_DURATION);
        }

        // Request No. 2
        {
            let url = BASE_URL + `/api/Accounts/${id}`;
            // TODO: edit the parameters of the request body.
            let body = {"firstName": "string", "lastName": "string", "password": "string"};
            let params = {headers: {"Content-Type": "application/json", "Accept": "application/json"}};
            let request = http.patch(url, JSON.stringify(body), params);

            check(request, {
                "Success": (r) => r.status === 204
            });
        }
    });

    group("/api/Stops", () => {
        let arrivedEndDate = 'TODO_EDIT_THE_ARRIVEDENDDATE'; // specify value as there is no example value for this parameter in OpenAPI spec
        let departedEndDate = 'TODO_EDIT_THE_DEPARTEDENDDATE'; // specify value as there is no example value for this parameter in OpenAPI spec
        let name = 'TODO_EDIT_THE_NAME'; // specify value as there is no example value for this parameter in OpenAPI spec
        let arrivedStartDate = 'TODO_EDIT_THE_ARRIVEDSTARTDATE'; // specify value as there is no example value for this parameter in OpenAPI spec
        let departedStartDate = 'TODO_EDIT_THE_DEPARTEDSTARTDATE'; // specify value as there is no example value for this parameter in OpenAPI spec
        let journeyId = 'TODO_EDIT_THE_JOURNEYID'; // specify value as there is no example value for this parameter in OpenAPI spec

        // Request No. 1
        {
            let url = BASE_URL + `/api/Stops?name=${name}&arrivedStartDate=${arrivedStartDate}&arrivedEndDate=${arrivedEndDate}&departedStartDate=${departedStartDate}&departedEndDate=${departedEndDate}&journeyId=${journeyId}`;
            let request = http.get(url);

            check(request, {
                "Success": (r) => r.status === 200
            });

            sleep(SLEEP_DURATION);
        }

        // Request No. 2
        {
            let url = BASE_URL + `/api/Stops`;
            // TODO: edit the parameters of the request body.
            let body = {"name": "string", "dateArrived": "date", "dateDeparted": "date", "tripId": "integer"};
            let params = {headers: {"Content-Type": "application/json", "Accept": "application/json"}};
            let request = http.post(url, JSON.stringify(body), params);

            check(request, {
                "Success": (r) => r.status === 201
            });
        }
    });

    group("/api/Trips/{id}", () => {
        let id = '1'; // specify value as there is no example value for this parameter in OpenAPI spec

        // Request No. 1
        {
            let url = BASE_URL + `/api/Trips/${id}`;
            let request = http.get(url);

            check(request, {
                "Success": (r) => r.status === 200
            });

            sleep(SLEEP_DURATION);
        }

        // Request No. 2
        {
            let url = BASE_URL + `/api/Trips/${id}`;
            // TODO: edit the parameters of the request body.
            let body = {"id": "integer", "title": "string", "description": "string", "publicView": "boolean", "accounts": {"id": "integer", "firstName": "string", "lastName": "string", "email": "string", "trips": "list"}};
            let params = {headers: {"Content-Type": "application/json", "Accept": "application/json"}};
            let request = http.patch(url, JSON.stringify(body), params);

            check(request, {
                "Success": (r) => r.status === 204
            });
        }
    });

}

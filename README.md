# tripscribe
Repository for the travel app Tripscribe

## Introduction
TripScribe is a website designed to allow users to create, document and share journeys they have been on in their life. Users are able to create "Trips" in their profile, attach dates to the cities they visit, add in places they visited while there, then attach photos and reviews. These trips can then be shared with friends, who can see these journeys. 

## Who is this for?
The goal of this website is for people who are interested in traveling and documenting where they have been, both to share their experiences and keep a journal about their journeys. This idea came about originally as a replacement for a physical travel log - something I had used during a trip across America.

## How will this be used?
The user will create an account on the website and gain access to a dashboard from which they can create new trips and edit already created trips. Within a trip they can detail the major stops they visited and attractions/restaurants they went to while there, creating a review for each as well as attaching pictures. Trips will be collected in a dashboard for easy viewing/editing. Account creation should only require basic information about the user.

### Create Trip
- User clicks "Create Trip" in the dashboard
- User enteres a name for the trip
- User redirected to the Trip edit page

### View/Edit Trip
- From dashboard, user selects one of their created trips to be redirected to it
- User is shown information attached to that trip (Stops, Places Visited, Photos, etc.)
- User can click an "Edit Trip" button which allows them to add new details or edit existing one (When first redirected here, editing will be enabled)

### Add Stop 
- Once a trip is in edit mode, the user can add stops that they have visited, with a stop being a city, national park, event, etc.
- User enters the name of the stop, the date arrived, and the date of departure

### Add Location
- With a stop created, the user can add a location they visited within that stop, including name and brief description (Restaurant, Museum, etc.) 

### Add Photographs and Reviews
- Both the stop in general as well as individual locations can be given a review or attached photographs
- For a review, the user writes what they want posted and when complete it will appear in that trip along the attached stop
- For photographs, the other uploads photographs from their local file storage/drag and drop function 

These are the basic functions that the user should be able to accomplish. Once a trip has been created, the details of it should be able to be seen in the dashboard - for example showing the number of visited stops and range of dates that the trip took place on. When the user goes from the dashboard into a trip, they will find the stops listed in chronological order with attached reviews and photos in a "gallery". 

Furthermore once a user has created a trip, they should be able to share it using a link to their friends. With this link, the user should be able to see the same page but without the ability to edit the page.

```mermaid
   erDiagram
          USER ||--|| TRIP : creates
          TRIP ||--|| STOP : contains
          STOP ||--|| LOCATION : contains
          REVIEW ||--|| STOP : "attached to"
          REVIEW ||--|| LOCATION : "attached to"
          GALLERY ||--|| STOP : "has photos of"
          GALLERY ||--|| LOCATION : "has photos of"
```

## Dictionary 
- **User**, a person who is interacting with the website, they have to own an account on the website to access the features
- **Trip**, a trip is created from a user's dashboard and represents a vacation of any length, from one major city to a tour of a few difference places
- **Stop**, a stop represents a city or a major destination within the trip and has the dates of when the user arrived and departed
- **Location**, a location is used to break up the major stop into smaller pieces, for examples restaurants and museums within a city or viewpoints in a national park
- **Review**, a text review of the user's thoughts which can be attached to both a stop or location
- **Gallery**, a collection of photos that are attached to a stop or location

***

## MVP
- User account Creation
- Trip Creation
- Adding City to the Trip
- Adding Locations to City
- Attach review to city and location
- User can share a trip with someone else (Being a user not required maybe)

## Stretch Goals 1 - Basic Social Media
- Social media aspect allowing users to add others to a friend's list from where they can see all their trips
- Privacy settings for journeys (private, friend's only, public)
- Allow users to search and find public journeys from other user's using the name of a stop or location

## Stretch Goals 2 - Planner Feature
- Allow user's to create journeys in advance instead of just in the past
- Invite other user's who will be joining them 
- They can organise a timetable for their travel plans and use public reviews to decide to add certain places to their plans

## Stretch Goals 3 - Mapper Feature
- Add a world map to the user's dashboard
- Populate the map with a number of 'pins' that represent places from their journeys
- Clicking a pin will select that journey as if they selected it from the list

***

## API Specification

#### ACCOUNTS

`GET /accounts` Return a list of accounts

Response

```json
[
   {
      "id": 1,
      "first_name": "John",
      "last_name": "Doe",
      "email": "john.doe@tester.com",
      "password": "passc0de"
   },
   {
      "id": 2,
      "first_name": "Jane",
      "last_name": "Smith",
      "email": "jane_smith@tester.com",
      "password": "speakfriend"
   }
]
```

`GET /accounts/{accountId}` Return the account matching the id

Response

```json
{
   "id": 1,
   "first_name": "John",
   "last_name": "Doe",
   "email": "john.doe@tester.com",
   "password": "passc0de"
}

```

`GET /accounts/{accountId}/trips` Return a list of trips tied to the given account

Response

```json
[
   {
      "id": 1,
      "title": "Canadian Adventure",
      "description": "Trekking Canada",
      "timestamp": "2021-07-10 17:30:00"
   },
   {
      "id": 2,
      "title": "My French Trip",
      "description": "Time Spent in Paris and Lyon",
      "timestamp": "2022-01-05 08:00:00"
   }
]
```

`GET /accounts/{accountId}/reviews` Return a list of reviews tied to the given account

Response

```json
[
   {
      "id": 1,
      "review_text": "Great views of Toronto",
      "score": 5,
      "timestamp": "2022-01-06 12:30:00"
   },
   {
      "id": 2,
      "review_text": "Very romantic, very crowded",
      "score": 4,
      "timestamp": "2021-07-11 10:30:00"
   }
]
```

`POST /accounts` Create an account

Request

```json
{
   "first_name": "new_f_name",
   "last_name": "new_l_name",
   "email": "new_email",
   "password": "new_password"
}
```

Response - `201 Created`

`PUT /accounts/{accoundId}` Update an account using a given id

Request

```json
{
   "first_name": "update_f_name",
   "last_name": "update_l_name",
   "email": "update_email",
   "password": "update_password"
}
```

Response - `204 Updated`

#### TRIPS

`GET /trips` Return a list of trips

Response

```json
[
   {
      "id": 1,
      "title": "Canadian Adventure",
      "description": "Trekking Canada",
      "timestamp": "2021-07-10 17:30:00"
   },
   {
      "id": 2,
      "title": "My French Trip",
      "description": "Time Spent in Paris and Lyon",
      "timestamp": "2022-01-05 08:00:00"
   }
]
```

`GET /trips/{tripId}` Return the trip matching the id

Response

```json
{
   "id": 1,
   "title": "Canadian Adventure",
   "description": "Trekking Canada",
   "timestamp": "2021-07-10 17:30:00"
}
```

`GET /trips/{tripId}/accounts` Return a list of accounts tied to the given trip

Response

```json
[
   {
      "id": 1,
      "first_name": "John",
      "last_name": "Doe",
      "email": "john.doe@tester.com",
      "password": "passc0de"
   },
   {
      "id": 2,
      "first_name": "Jane",
      "last_name": "Smith",
      "email": "jane_smith@tester.com",
      "password": "speakfriend"
   }
]
```

`GET /trips/{tripId}/reviews` Return a list of reviews tied to the given trip

Response

```json
[
   {
      "id": 1,
      "review_text": "Great views of Toronto",
      "score": 5,
      "timestamp": "2022-01-06 12:30:00"
   },
   {
      "id": 2,
      "review_text": "Very romantic, very crowded",
      "score": 4,
      "timestamp": "2021-07-11 10:30:00"
   }
]
```

`POST /trips` Create a new trip

Request

```json
{
   "title": "new_journey",
   "description": "new_description"
}
```

Response - `201 Created`

`PUT /trips/{tripId}` Update a trip based on a given id

Request

```json
{
   "title": "update_journey",
   "description": "update_desc"
}
```

Response - `204 Updated`

#### STOPS

`GET /stops/{stopId}` Return a single stop matching the id

Response

```json
{
   "id": 1,
   "name": "Toronto",
   "date_arrived": "2021-06-15 06:00:00",
   "date_departed": "2021-06-22 18:30:00",
   "trip_id": 1
}
```

`GET /stops/{tripId}` Return a list of stops matching the given trip

Response

```json
[
   {
      "id": 1,
      "name": "Toronto",
      "date_arrived": "2021-06-15 06:00:00",
      "date_departed": "2021-06-22 18:30:00",
      "trip_id": 1
   },
   {
      "id": 3,
      "name": "Montreal",
      "date_arrived": "2021-06-22 22:00:00",
      "date_departed": "2021-06-26 10:00:00",
      "trip_id": 1
   }
]
```

`GET /stops/{stopId}/reviews` Return a list of reviews tied to the given stop

Response

```json
[
   {
      "id": 1,
      "review_text": "Great views of Toronto",
      "score": 5,
      "timestamp": "2022-01-06 12:30:00"
   },
   {
      "id": 2,
      "review_text": "Very romantic, very crowded",
      "score": 4,
      "timestamp": "2021-07-11 10:30:00"
   }
]
```

#### LOCATIONS

`GET /locations/{locationId}` Return a single location matching the id

Response

```json
{
   "id": 1,
   "name": "CN Tower",
   "date_arrived": "2022-01-02 15:00:00",
   "location_type": "Attraction",
   "stop_id": 1
}
```

`GET /locations/{stopId}` Return a list of locations matching the given stop

Response

```json
[
   {
      "id": 1,
      "name": "CN Tower",
      "date_arrived": "2022-01-02 15:00:00",
      "location_type": "Attraction",
      "stop_id": 1
   },
   {
      "id": 3,
      "name": "Jack Astor's",
      "date_arrived": "2022-01-04 20:00:00",
      "location_type": "Restaurant",
      "stop_id": 1
   }
]
```

`GET /locations/{locationId}/reviews` Return a list of reviews tied to the given location

Response

```json
[
   {
      "id": 1,
      "review_text": "Great views of Toronto",
      "score": 5,
      "timestamp": "2022-01-06 12:30:00"
   },
   {
      "id": 2,
      "review_text": "Very romantic, very crowded",
      "score": 4,
      "timestamp": "2021-07-11 10:30:00"
   }
]
```

#### REVIEWS 

`GET /reviews/{reviewId}` Return a single review matching the id

Response

```json
{
   "id": 1,
   "review_text": "Great views of Toronto",
   "score": 5,
   "timestamp": "2022-01-06 12:30:00"
}
```

### Misc. Links
- Trello - https://trello.com/b/G33esPU4/tripscribe-kanban
- Lucidchart Database ERD - https://lucid.app/lucidchart/836838f8-d65a-44f7-8172-ce222fe9e87c/edit?viewport_loc=-1376%2C-334%2C3491%2C1760%2CLaP3wGvsL-Vc&invitationId=inv_b329b385-764e-4ddc-9f05-c24404f51496

### User Password for Testing
p455word
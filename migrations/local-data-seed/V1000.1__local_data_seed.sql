-- User Test

INSERT INTO accounts(first_name, last_name, email, password) 
    VALUES('John', 'Doe', 'jdoe2002@example.com', '$2a$11$aI7b3zWnfwqoOHKl6xU8i.L0X7hxcqZYaGeJlWIz1xkjSdWoEmUYa');
INSERT INTO accounts(first_name, last_name, email, password) 
    VALUES('Dariusz', 'Halasa', 'darek_halasa@example.com', '$2a$11$aI7b3zWnfwqoOHKl6xU8i.L0X7hxcqZYaGeJlWIz1xkjSdWoEmUYa');
INSERT INTO accounts(first_name, last_name, email, password)
    VALUES('Emma', 'Howard', 'emhowradami@example.com', '$2a$11$aI7b3zWnfwqoOHKl6xU8i.L0X7hxcqZYaGeJlWIz1xkjSdWoEmUYa');
INSERT INTO accounts(first_name, last_name, email, password)
    VALUES('Kodie', 'Tessler', 'tesslermachine@example.com', '$2a$11$aI7b3zWnfwqoOHKl6xU8i.L0X7hxcqZYaGeJlWIz1xkjSdWoEmUYa');
INSERT INTO accounts(first_name, last_name, email, password)
    VALUES('Ritchard', 'Waterson', 'richhawk@example.com', '$2a$11$aI7b3zWnfwqoOHKl6xU8i.L0X7hxcqZYaGeJlWIz1xkjSdWoEmUYa');

INSERT INTO trips(title, description, timestamp, public_view)
    VALUES('My French Trip', 'Days in Paris', '2022-01-05 08:00:00', TRUE);
INSERT INTO trips(title, description, timestamp, public_view)
    VALUES('Canadian Adventure', 'Trekking Canada!', '2021-07-10 17:30:00', TRUE);
INSERT INTO trips(title, description, timestamp, public_view)
    VALUES('Sleepless in Seattle', 'Love the rainy city:)', '2020-04-22 20:00:00', TRUE);
INSERT INTO trips(title, description, timestamp, public_view)
    VALUES('Interrailing', 'Sweatshirt Pillows', '2020-06-16 00:45:00', TRUE);
INSERT INTO trips(title, description, timestamp, public_view)
    VALUES('Japan Jaunt', 'Hokkaido and Honshu', '2022-05-29 13:15:00', TRUE);

INSERT INTO account_trips(account_id, trip_id)
    VALUES(1, 4);
INSERT INTO account_trips(account_id, trip_id)
    VALUES(2, 5);
INSERT INTO account_trips(account_id, trip_id)
    VALUES(3, 1);
INSERT INTO account_trips(account_id, trip_id)
    VALUES(4, 2);
INSERT INTO account_trips(account_id, trip_id)
    VALUES(5, 3);

INSERT INTO stops(name, date_arrived, date_departed, trip_id)
    VALUES('Paris', '2022-01-02 09:00:00', '2022-01-04 22:00:00', 1);
INSERT INTO stops(name, date_arrived, date_departed, trip_id)
    VALUES('Toronto', '2021-06-15 06:00:00', '2021-06-22 18:30:00', 2);
INSERT INTO stops(name, date_arrived, date_departed, trip_id)
    VALUES('Seattle', '2020-04-10 12:00:00', '2020-04-24 10:20:00', 3);
INSERT INTO stops(name, date_arrived, date_departed, trip_id)
    VALUES('Munich', '2020-05-22 00:00:00', '2020-05-29 15:00:00', 4);
INSERT INTO stops(name, date_arrived, date_departed, trip_id)
    VALUES('Tokyo', '2022-05-05 09:00:00', '2022-05-26 12:00:00', 5);

INSERT INTO locations(name, date_visited, location_type, geo_location, stop_id)
    VALUES('Eiffel Tower', '2022-01-02 15:00:00', 'Attraction', '48.8584, 2.2945', 1);
INSERT INTO locations(name, date_visited, location_type, geo_location, stop_id)
    VALUES('CN Tower', '2021-06-19 08:00:00', 'Attraction', '43.6426, -79.3870', 2);
INSERT INTO locations(name, date_visited, location_type, geo_location, stop_id)
    VALUES('Einstein Bros', '2020-04-15 19:00:00', 'Restaurant', '47.6062, -122.3321', 3);
INSERT INTO locations(name, date_visited, location_type, geo_location, stop_id)
    VALUES('Munich Beer Hall', '2020-05-26 10:00:00', 'Other', '48.1351, 11.5820', 4);
INSERT INTO locations(name, date_visited, location_type, geo_location, stop_id)
    VALUES('Tokyo Temple', '2022-05-22 18:00:00', 'Scenic Spot', '35.7148, 139.7967',1);

INSERT INTO reviews(review_text, score, timestamp)
    VALUES('Very romantic, very crowded', 4, '2022-01-05 11:00:00');
INSERT INTO reviews(review_text, score, timestamp)
    VALUES('Great views of Toronto', 5, '2021-07-11 12:30:00');
INSERT INTO reviews(review_text, score, timestamp)
    VALUES('Best reuben sandwich, very expensive', 4, '2020-04-23 13:00:00');
INSERT INTO reviews(review_text, score, timestamp)
    VALUES('Nothing Special', 2, '2020-06-16 16:45:00');
INSERT INTO reviews(review_text, score, timestamp)
    VALUES('Lot of steps to reach it!', 3, '2022-05-29 14:00:00');

INSERT INTO trip_reviews(trip_id, review_id, account_id)
    VALUES(1, 1, 1);
INSERT INTO trip_reviews(trip_id, review_id, account_id)
    VALUES(2, 2, 2);
INSERT INTO trip_reviews(trip_id, review_id, account_id)
    VALUES(3, 3, 3);
INSERT INTO trip_reviews(trip_id, review_id, account_id)
    VALUES(4, 4, 4);
INSERT INTO trip_reviews(trip_id, review_id, account_id)
    VALUES(5, 5, 5);

INSERT INTO stop_reviews(stop_id, review_id, account_id)
    VALUES(1, 1, 1);
INSERT INTO stop_reviews(stop_id, review_id, account_id)
    VALUES(2, 2, 2);
INSERT INTO stop_reviews(stop_id, review_id, account_id)
    VALUES(3, 3, 3);
INSERT INTO stop_reviews(stop_id, review_id, account_id)
    VALUES(4, 4, 4);
INSERT INTO stop_reviews(stop_id, review_id, account_id)
    VALUES(5, 5, 5);

INSERT INTO location_reviews(location_id, review_id, account_id)
    VALUES(1, 1, 1);
INSERT INTO location_reviews(location_id, review_id, account_id)
    VALUES(2, 2, 2);
INSERT INTO location_reviews(location_id, review_id, account_id)
    VALUES(3, 3, 3);
INSERT INTO location_reviews(location_id, review_id, account_id)
    VALUES(4, 4, 4);
INSERT INTO location_reviews(location_id, review_id, account_id)
    VALUES(5, 5, 5);
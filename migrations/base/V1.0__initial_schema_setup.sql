CREATE TABLE accounts (
	id serial PRIMARY KEY,
	first_name VARCHAR (255) NOT NULL,
	last_name VARCHAR (255) NOT NULL,
	email VARCHAR (100) NOT NULL UNIQUE,
	password VARCHAR (100) NOT NULL
);

CREATE TABLE journeys (
	id serial PRIMARY KEY,
	title VARCHAR (100) NOT NULL,
	description TEXT NOT NULL,
	timestamp TIMESTAMP NOT NULL
);

CREATE TABLE account_journeys (
	account_id INTEGER NOT NULL, 
	journey_id INTEGER NOT NULL,
	PRIMARY KEY (account_id, journey_id),
	CONSTRAINT fk_account FOREIGN KEY (account_id)
		REFERENCES accounts (id),
	CONSTRAINT fk_journey FOREIGN KEY (journey_id)
		REFERENCES journeys (id) 
);

CREATE TABLE stops (
	id serial PRIMARY KEY,
	name VARCHAR (100) NOT NULL,
	date_arrived TIMESTAMP NOT NULL,
	date_departed TIMESTAMP NOT NULL,
	journey_id INTEGER NOT NULL,
	CONSTRAINT fk_journey FOREIGN KEY (journey_id)
		REFERENCES journeys (id) 
);

CREATE TABLE locations (
	id serial PRIMARY KEY,
	name VARCHAR (100) NOT NULL,
	date_visited TIMESTAMP NOT NULL,
	location_type VARCHAR (30) NOT NULL,
	stop_id INTEGER NOT NULL,
	CONSTRAINT fk_stop FOREIGN KEY (stop_id)
		REFERENCES stops (id)
);

CREATE TABLE reviews (
	id serial PRIMARY KEY,
	review_text TEXT NOT NULL,
	score INTEGER NOT NULL,
	timestamp TIMESTAMP NOT NULL
);

CREATE TABLE journey_reviews (
	journey_id INTEGER NOT NULL,
	review_id INTEGER NOT NULL,
	account_id INTEGER NOT NULL,
	CONSTRAINT fk_journey FOREIGN KEY (journey_id)
		REFERENCES journeys (id),
	CONSTRAINT fk_review FOREIGN KEY (review_id)
		REFERENCES reviews (id),
	CONSTRAINT fk_account FOREIGN KEY (account_id)
		REFERENCES accounts (id)
);

CREATE TABLE stop_reviews (
	stop_id INTEGER NOT NULL,
	review_id INTEGER NOT NULL,
	account_id INTEGER NOT NULL,
	CONSTRAINT fk_stop FOREIGN KEY (stop_id)
		REFERENCES stops (id),
	CONSTRAINT fk_review FOREIGN KEY (review_id)
		REFERENCES reviews (id),
	CONSTRAINT fk_account FOREIGN KEY (account_id)
		REFERENCES accounts (id)
);

CREATE TABLE location_reviews (
	location_id INTEGER NOT NULL,
	review_id INTEGER NOT NULL,
	account_id INTEGER NOT NULL,
	CONSTRAINT fk_location FOREIGN KEY (location_id)
		REFERENCES locations (id),
	CONSTRAINT fk_review FOREIGN KEY (review_id)
		REFERENCES reviews (id),
	CONSTRAINT fk_account FOREIGN KEY (account_id)
		REFERENCES accounts (id)
);
CREATE TABLE account (
	id serial PRIMARY KEY,
	first_name VARCHAR (255) NOT NULL,
	last_name VARCHAR (255) NOT NULL,
	email VARCHAR (100) NOT NULL UNIQUE,
	password VARCHAR (100) NOT NULL
);

CREATE TABLE journey (
	id serial PRIMARY KEY,
	title VARCHAR (100) NOT NULL,
	description TEXT NOT NULL,
	timestamp TIMESTAMP NOT NULL
);

CREATE TABLE account_journey (
	account_id INTEGER NOT NULL, 
	journey_id INTEGER NOT NULL,
	PRIMARY KEY (account_id, journey_id),
	CONSTRAINT fk_account FOREIGN KEY (account_id)
		REFERENCES account (id),
	CONSTRAINT fk_journey FOREIGN KEY (journey_id)
		REFERENCES journey (id) 
);

CREATE TABLE stop (
	id serial PRIMARY KEY,
	name VARCHAR (100) NOT NULL,
	date_arrived TIMESTAMP NOT NULL,
	date_departed TIMESTAMP NOT NULL,
	journey_id INTEGER NOT NULL,
	CONSTRAINT fk_journey FOREIGN KEY (journey_id)
		REFERENCES journey (id) 
);

CREATE TABLE location (
	id serial PRIMARY KEY,
	name VARCHAR (100) NOT NULL,
	date_visited TIMESTAMP NOT NULL,
	location_type VARCHAR (30) NOT NULL,
	stop_id INTEGER NOT NULL,
	CONSTRAINT fk_stop FOREIGN KEY (stop_id)
		REFERENCES stop (id)
);

CREATE TABLE review (
	id serial PRIMARY KEY,
	review_text TEXT NOT NULL,
	score INTEGER NOT NULL,
	timestamp TIMESTAMP NOT NULL
);

CREATE TABLE journey_review (
	journey_id INTEGER NOT NULL,
	review_id INTEGER NOT NULL,
	account_id INTEGER NOT NULL,
	CONSTRAINT fk_journey FOREIGN KEY (journey_id)
		REFERENCES journey (id),
	CONSTRAINT fk_review FOREIGN KEY (review_id)
		REFERENCES review (id),
	CONSTRAINT fk_account FOREIGN KEY (account_id)
		REFERENCES account (id)
);

CREATE TABLE stop_review (
	stop_id INTEGER NOT NULL,
	review_id INTEGER NOT NULL,
	account_id INTEGER NOT NULL,
	CONSTRAINT fk_stop FOREIGN KEY (stop_id)
		REFERENCES stop (id),
	CONSTRAINT fk_review FOREIGN KEY (review_id)
		REFERENCES review (id),
	CONSTRAINT fk_account FOREIGN KEY (account_id)
		REFERENCES account (id)
);

CREATE TABLE location_review (
	location_id INTEGER NOT NULL,
	review_id INTEGER NOT NULL,
	account_id INTEGER NOT NULL,
	CONSTRAINT fk_location FOREIGN KEY (location_id)
		REFERENCES location (id),
	CONSTRAINT fk_review FOREIGN KEY (review_id)
		REFERENCES review (id),
	CONSTRAINT fk_account FOREIGN KEY (account_id)
		REFERENCES account (id)
);
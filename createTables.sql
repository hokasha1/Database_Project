create table event_ (
	event_ID integer PRIMARY KEY AUTOINCREMENT,
	venue_ID integer,
	event_Title text,
	title_ID integer,
	FOREIGN Key(title_ID) REFERENCES papers,
	FOREIGN KEY(venue_ID) REFERENCES venue
);

create table venue(
	venue_ID integer PRIMARY KEY AUTOINCREMENT,
	venue_Name text,
	Location text,
	chair_ID integer,
	event_ID integer
	FOREIGN KEY(chair_ID) REFERENCES chairs,
	FOREIGN KEY(event_ID) REFERENCES event_
);

create table authors(
	author_ID integer PRIMARY KEY AUTOINCREMENT,
	a_name text,
	title_ID integer,
	FOREIGN KEY(title_ID) REFERENCES papers
);

create table papers(
	title_ID integer PRIMARY KEY AUTOINCREMENT,
	paper_Title text,
	author_ID integer, 
	event_ID integer,
	FOREIGN Key(event_ID) REFERENCES event_,
	FOREIGN Key(author_ID) REFERENCES author
);

create table reviewers(
	reviewer_ID integer PRIMARY KEY AUTOINCREMENT,
	title_ID integer,
	chair_ID integer,
	r_Name text,
	FOREIGN KEY(title_ID) REFERENCES papers,
	FOREIGN Key(chair_ID) REFERENCES chairs
);

create table chairs(
	chair_ID integer PRIMARY KEY AUTOINCREMENT,
	chair_Name text,
	venue_ID integer,
	FOREIGN Key(venue_ID) REFERENCES venue
);
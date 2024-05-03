CREATE TABLE NOTE (
NoteId Int IDENTITY (1,1) PRIMARY KEY,
Title NvarChar(50),
Date DateTime2,
MentorName NvarChar(50),
NoteDescription NvarChar(4000),
);
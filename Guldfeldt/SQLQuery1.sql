CREATE TABLE LOCATION (
    LocationId Int PRIMARY KEY IDENTITY(1,1),
    Name NVarChar(50),
    Address NVarChar(500),
    IsConstructionSite Bit,
    IsSchool Bit
);

CREATE TABLE TIMEPERIOD (
    TimeperiodId Int IDENTITY(1,1) PRIMARY KEY,
    Period DateTime2
);

CREATE TABLE NOTE (
    NoteId Int IDENTITY(1,1) PRIMARY KEY,
    Title NvarChar(50),
    Date DateTime2,
    MentorName NvarChar(50),
    NoteDescription NvarChar(4000)
);

CREATE TABLE EMPLOYEE (
    SalaryNumber Int PRIMARY KEY,
    FullName NVarChar(50),
    PhoneNumber Int,
    Email NVarChar(50),
    CurrentWorkplace NVarChar(100),
    SocialSecurityNumber NVarChar(11),
    IsApprentice Bit,
    IsJourneyman Bit,
    IsMentor Bit
);
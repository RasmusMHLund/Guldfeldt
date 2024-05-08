CREATE TABLE LOCATION (
LocationId Int PRIMARY KEY IDENTITY(1,1),
Name NVarChar(50),
Address NVarChar(500),
IsConstructionSite Bit,
IsSchool Bit
);
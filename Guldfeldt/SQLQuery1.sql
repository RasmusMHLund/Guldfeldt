CREATE TABLE APPRENTICE (
SalaryNumber Int PRIMARY KEY,
Name NVarChar(50),
DateOfBirth DateTime2, 
SocialSecurityNumber NVarChar(11),
Email NVarChar(50),
PhoneNumber Int
);

CREATE TABLE JOURNEYMAN (
SalaryNumber Int PRIMARY KEY,
Name NVarChar(50),
DateOfBirth DateTime2, 
SocialSecurityNumber NVarChar(11),
Email NVarChar(50),
PhoneNumber Int,
MentorStatus Bit
);

CREATE TABLE WORKPLACE (
WorkplaceId Int IDENTITY(1,1) PRIMARY KEY,
Name NVarChar(50),
Address NVarChar(200)
);
--create database photoExpressDB
use photoExpressDB
CREATE TABLE HigherEducationInstitution (
    InstitutionID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    InstitutionName NVARCHAR(255) NOT NULL,
    InstitutionAddress NVARCHAR(255) NOT NULL
);


CREATE TABLE Event (
    EventID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    InstitutionID UNIQUEIDENTIFIER,
    NumberOfStudents INT NOT NULL,
    StartTime DATETIME NOT NULL,
    ServiceCost DECIMAL(10, 2) NOT NULL,
	capAndGown bit,
    FOREIGN KEY (InstitutionID) REFERENCES HigherEducationInstitution(InstitutionID)
);


CREATE TABLE EventModificationLog (
    ModificationID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    EventID UNIQUEIDENTIFIER,
    ModificationDate DATETIME NOT NULL,
	EventBefore NVARCHAR(MAX),
    EventAfter NVARCHAR(MAX),
    FOREIGN KEY (EventID) REFERENCES Event(EventID)
);

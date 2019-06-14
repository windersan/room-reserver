CREATE TABLE Employee (
    ID int NOT NULL PRIMARY KEY,
    LastName varchar(50),
    FirstName varchar(50),
    Email varchar(50)
);

CREATE TABLE Reservation (
    ID int IDENTITY(1,1) PRIMARY KEY,
    [Start] DATETIME,
    Finish DATETIME,
    RoomID int,
	EmployeeID int FOREIGN KEY REFERENCES Employee(ID),
	Comment varchar(200)
);

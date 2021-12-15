
create Table Users
(
	 Id nvarchar(255)   PRIMARY KEY,
	 FirstName VARCHAR(20) NOT NULL, 
	 LastName VARCHAR(20) NOT NULL,
	 Email VARCHAR(100) NOT NULL UNIQUE,
	 PhoneNumber varchar(14)  NOT NULL UNIQUE,
	 PasswordHash VARCHAR(50) NOT NULL,
	 PasswordSalt VARCHAR(50) NOT NULL,
	 DateCreated DATE NOT NULL DEFAULT GETDATE(),
	 DateUpdated DATE NOT NULL DEFAULT GETDATE(),
	 IsActive BIT NOT NULL DEFAULT 1
)
create Table JobIndustry
(
	Id  nvarchar(255) PRIMARY KEY,
	[Name] VARCHAR(100) NOT NULL,
	DateCreated DATE NOT NULL DEFAULT GETDATE(),
	 DateUpdated DATE NOT NULL DEFAULT GETDATE(),
)create Table Roles
(
	Id nvarchar(255) PRIMARY KEY,
	[Name] varchar(50) NOT NULL,

)
create Table JobCategory
(
	Id nvarchar(255) PRIMARY KEY,
	[Name] VARCHAR(100) NOT NULL,
	DateCreated DATE NOT NULL DEFAULT GETDATE(),
	 DateUpdated DATE NOT NULL DEFAULT GETDATE(),
)
create Table Jobs
(
	Id nvarchar(255) PRIMARY KEY,
	JobTitle VARCHAR(100) NOT NULL,
	Company VARCHAR(100) NOT NULL,
	[Location] VARCHAR(20) NOT NULL,
	JobNature VARCHAR(20) NOT NULL,
	JobDescription TEXT NOT NULL,
	MinimumSalary INT NOT NULL,
	MaximumSalary INT NOT NULL,
	JobIndustryId nvarchar(255) NOT NULL FOREIGN KEY REFERENCES JobIndustry(Id),
	JobCategoryId nvarchar(255) NOT NULL  FOREIGN KEY REFERENCES JobCategory(Id),
	DateCreated DATE NOT NULL DEFAULT GETDATE(),
	 DateUpdated DATE NOT NULL DEFAULT GETDATE(),
	 DeadLine DATE NOT NULL 
)
create Table UserRole
(
	RoleId nvarchar(255) NOT NULL FOREIGN KEY REFERENCES Roles(Id),
	RoleName VARCHAR(100) NOT NULL,
	UserId nvarchar(255) NOT NULL FOREIGN KEY REFERENCES Users(Id),
)
create Table Applicant
(
	UserId nvarchar(255) NOT NULL FOREIGN KEY REFERENCES Users(Id),
	Location varchar(20) NOT NULL,
)
SELECT * FROM Users
CREATE TABLE Team (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    City NVARCHAR(50) NOT NULL,
    FoundationYear INT NOT NULL,
    Created_At DATETIME NOT NULL,
    Updated_At DATETIME
);

CREATE TABLE Player (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Position CHAR(2) NOT NULL,
    Age INT NOT NULL,
    TeamId INT NOT NULL,
    Created_At DATETIME NOT NULL,
    Updated_At DATETIME,
    CONSTRAINT FK_Player_Team FOREIGN KEY (TeamId) REFERENCES Team(Id)
);

CREATE TABLE Match (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Goals_Home_Team INT NOT NULL,
    Goals_Away_Team INT NOT NULL,
    Date DATETIME NOT NULL,
    HomeTeamId INT NOT NULL,
    AwayTeamId INT NOT NULL,
    Created_At DATETIME NOT NULL,
    Updated_At DATETIME,
    CONSTRAINT FK_Match_HomeTeam FOREIGN KEY (HomeTeamId) REFERENCES Team(Id),
    CONSTRAINT FK_Match_AwayTeam FOREIGN KEY (AwayTeamId) REFERENCES Team(Id)
);

CREATE TABLE PlayerMatch (
    PlayerId INT NOT NULL,
    MatchId INT NOT NULL,
    CONSTRAINT PK_PlayerMatch PRIMARY KEY (PlayerId, MatchId),
    CONSTRAINT FK_PlayerMatch_Player FOREIGN KEY (PlayerId) REFERENCES Player(Id),
    CONSTRAINT FK_PlayerMatch_Match FOREIGN KEY (MatchId) REFERENCES Match(Id)
);
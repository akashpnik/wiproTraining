CREATE DATABASE ProfileBookDB;
USE ProfileBookDB;


-- Users Table
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) UNIQUE NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Role NVARCHAR(20) DEFAULT 'User' CHECK (Role IN ('User', 'Admin')),
    ProfileImage NVARCHAR(255) NULL,
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1
);

-- Posts Table (CORRECTED - Added PostImage column)
CREATE TABLE Posts (
    PostId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    PostImage NVARCHAR(255) NULL,  -- This column for post images
    Status NVARCHAR(20) DEFAULT 'Pending' CHECK (Status IN ('Pending', 'Approved', 'Rejected')),
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    ApprovedAt DATETIME2 NULL,
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

-- Messages Table
CREATE TABLE Messages (
    MessageId INT IDENTITY(1,1) PRIMARY KEY,
    SenderId INT NOT NULL,
    ReceiverId INT NOT NULL,
    MessageContent NVARCHAR(MAX) NOT NULL,
    TimeStamp DATETIME2 DEFAULT GETDATE(),
    IsRead BIT DEFAULT 0,
    FOREIGN KEY (SenderId) REFERENCES Users(UserId),
    FOREIGN KEY (ReceiverId) REFERENCES Users(UserId)
);

-- Reports Table
CREATE TABLE Reports (
    ReportId INT IDENTITY(1,1) PRIMARY KEY,
    ReportedUserId INT NOT NULL,
    ReportingUserId INT NOT NULL,
    Reason NVARCHAR(500) NOT NULL,
    TimeStamp DATETIME2 DEFAULT GETDATE(),
    Status NVARCHAR(20) DEFAULT 'Pending' CHECK (Status IN ('Pending', 'Reviewed', 'Resolved')),
    FOREIGN KEY (ReportedUserId) REFERENCES Users(UserId),
    FOREIGN KEY (ReportingUserId) REFERENCES Users(UserId)
);

-- Groups Table
CREATE TABLE Groups (
    GroupId INT IDENTITY(1,1) PRIMARY KEY,
    GroupName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500) NULL,
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    CreatedBy INT NOT NULL,
    FOREIGN KEY (CreatedBy) REFERENCES Users(UserId)
);

-- GroupMembers Table (Many-to-Many relationship)
CREATE TABLE GroupMembers (
    GroupMemberId INT IDENTITY(1,1) PRIMARY KEY,
    GroupId INT NOT NULL,
    UserId INT NOT NULL,
    JoinedAt DATETIME2 DEFAULT GETDATE(),
    FOREIGN KEY (GroupId) REFERENCES Groups(GroupId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    UNIQUE(GroupId, UserId)
);

-- PostLikes Table
CREATE TABLE PostLikes (
    LikeId INT IDENTITY(1,1) PRIMARY KEY,
    PostId INT NOT NULL,
    UserId INT NOT NULL,
    LikedAt DATETIME2 DEFAULT GETDATE(),
    FOREIGN KEY (PostId) REFERENCES Posts(PostId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    UNIQUE(PostId, UserId)
);

-- PostComments Table
CREATE TABLE PostComments (
    CommentId INT IDENTITY(1,1) PRIMARY KEY,
    PostId INT NOT NULL,
    UserId INT NOT NULL,
    CommentText NVARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    FOREIGN KEY (PostId) REFERENCES Posts(PostId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

-- Insert a default admin user
INSERT INTO Users (Username, Email, Password, Role) 
VALUES ('admin', 'admin@profilebook.com', 'Admin@123', 'Admin');

-- Insert a sample regular user
INSERT INTO Users (Username, Email, Password, Role) 
VALUES ('testuser', 'user@profilebook.com', 'User@123', 'User');




use ProfileBookDB

SELECT PostId, Content, Status, CreatedAt, UserId 
FROM Posts 

WHERE Status = 'pending'

SELECT UserId, Username, Email, Role
FROM Users
WHERE IsActive = 1;


SELECT UserId, Username, Password FROM Users;
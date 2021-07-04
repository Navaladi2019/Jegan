use GRR;

create table DbUser (
Id int identity(1,1) not null,
Name nvarchar(100),
Age int,
DOB DATETIME,
CreatedBy int,
CreatedOn datetime,
ModifiedBy int,
ModifiedOn datetime,
Status bit,
IsDeleted bit,

CONSTRAINT PK_User_Id PRIMARY KEY (Id)
)

Drop table UserRole,UserAdditionalDetail

create table UserAdditionalDetail(


    Id int  identity(1,1),
    Groups nvarchar(100),
    UserId int ,
    CreatedBy int,
CreatedOn datetime,
ModifiedBy int,
ModifiedOn datetime,
Status bit,
IsDeleted bit,
    CONSTRAINT PK_UserAdditionalDetails_Id PRIMARY KEY (Id),
    CONSTRAINT FK_UserAdditionalDetails_DbUser_UserId FOREIGN KEY (UserId)
    REFERENCES DbUser(Id)
  
);

create   UNIQUE INDEX IX_UserId on UserAdditionalDetail(UserId)  where UserId is not null


create table UserRole(


    Id int  identity(1,1),
    Roles nvarchar(100),
    UserAdditionalDetailId int,
    CreatedBy int,
CreatedOn datetime,
ModifiedBy int,
ModifiedOn datetime,
Status bit,
IsDeleted bit,
    CONSTRAINT PK_UserRole_Id PRIMARY KEY (Id),
    CONSTRAINT FK_UserRole_UserAdditionalDetail_UserId FOREIGN KEY (UserAdditionalDetailId)
    REFERENCES UserAdditionalDetail(Id)
)



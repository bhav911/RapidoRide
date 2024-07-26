use BhavyaModhiya_490

Create table Users(
 UserID int identity(1,1),
 FirstName varchar(30) not null,
 MiddleName varchar(30),
 Lastname varchar(30) not null,
 Email varchar(60) not null,
 PhoneNumber char(10) not null,
 BirthDate Date,
 Address varchar(max) not null,
 CountryID int not null,
 StateID int not null,
 CityID int not null,
 ZipCode int not null,
 ProfilePhoto varchar(max)
 CONSTRAINT pk_user Primary key(userID)
 CONSTRAINT fk_country Foreign key (countryID) references Country(countryId),
 CONSTRAINT fk_state Foreign key (StateID) references State(stateID),
 CONSTRAINT fk_city Foreign key (CityID) references City(cityid),
)

alter table Users  add Password char(6) not null

delete from users


create table Country(
	CountryID int identity(1,1),
	Name varchar(100)
	 CONSTRAINT pk_country Primary key(CountryID)
)


create table State(
	StateID int identity(1,1),
	Name varchar(100),
	CountryId int
	 CONSTRAINT pk_state Primary key(StateID)
	  CONSTRAINT fk_state_country Foreign key (CountryId) references Country(CountryId),

)

create table City(
	CityID int identity(1,1),
	Name varchar(100),
	StateID int
	 CONSTRAINT pk_City Primary key(CityID)
	 CONSTRAINT fk_city_state Foreign key (StateID) references State(StateID)
)

create table Seller(
	SellerID int identity(1,1),
	FirstName varchar(40) not null,
	LastName varchar(40) not null,
	Email varchar(100) not null,
	PhoneNumber char(10) not null
	Constraint pk_seller primary key(sellerID)
)

create table Bike(
	BikeID int identity(1,1),
	SellerID int not null,
	Brand varchar(50) not null,
	Models varchar(30) not null,
	Years tinyint not null,
	kilometresDriven int not null,
	Price decimal(9,2) not null,
	Description varchar(max),
	Image varchar(1000)
	constraint pk_bike primary key(bikeid)
	constraint fk_bike_seller foreign key (sellerid) references seller(sellerID)
)

CREATE TABLE FAVORITES(
	FavID int identity(1,1),
	UserID int not null,
	BikeIDs varchar(max) not null
	CONSTRAINT pk_favorite PRIMARY KEY (FavID)
	CONSTRAINT fk_favorite_bike FOREIGN KEY (UserID) REFERENCES Users(UserID)
)

insert into FAVORITES values(3,',')
select * from FAVORITES
select * from Bike

CREATE OR ALTER PROC ToggleFavorite
	@UserID int,
	@BikeID int
AS
BEGIN
	Declare @Add bit = 0;
	Declare @BikeIDs varchar(max);

	Select @BikeIDs = BikeIDs
	from FAVORITES
	where USERID = @UserID
	IF CHARINDEX(concat(',',@BikeID,','), @BikeIDs) = 0
	begin
		set @Add = 1
	end

	IF @Add = 1
	BEGIN
		Update FAVORITES 
		set BikeIDs = Concat(BikeIDs,@BikeID,',')
		where UserID = @UserID;
		return;
	END			

	Update FAVORITES 
	set BikeIDs = REPLACE(BikeIDs,concat(',',@BikeID,','),',')
	where UserID = @UserID 
END


ALTER PROC GetFilteredBikes
	@DynamicConditions varchar(300),
	@page smallint,
	@max tinyint = 5
as
begin
	CREATE TABLE #TempFilteredBikes (
        BikeID INT,
        SellerID int,
		Brand varchar(50),
		Models varchar(30),
		Years tinyint,
		kilometresDriven int,
		Price decimal(9,2),
		FirstName varchar(40),
		LastName varchar(40),
		Email varchar(100),
		PhoneNumber char(10),
		Description varchar(max),
		Image varchar(1000)
    );
	DECLARE @SQL NVARCHAR(MAX)
	SET @SQL = 'INSERT INTO #TempFilteredBikes (
        BikeID, SellerID, Brand, Models, Years, KilometresDriven, Price, 
        FirstName, LastName, Email, PhoneNumber, Description, Image
    )
    SELECT 
        b.BikeID, b.SellerID, b.Brand, b.Models, b.Years, b.KilometresDriven, b.Price,
        s.FirstName, s.LastName, s.Email, s.PhoneNumber, b.Description, b.Image
    FROM Bike AS b
    INNER JOIN Seller AS s ON s.SellerID = b.SellerID
    WHERE ' + TRIM('AND' FROM Trim(@DynamicConditions))
	EXEC sp_executesql @SQL

	SELECT * FROM #TempFilteredBikes
	ORDER BY BikeID
    OFFSET (@page - 1) * @max ROWS
    FETCH NEXT @max ROWS ONLY
end

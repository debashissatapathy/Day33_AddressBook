select * from AddressBook


Create table Address_Book(AddressBookId Int Identity(1,1) Primary Key,
						  AddressBookName varchar(100));

Create table PersonDetail(   PersonId Int Identity(1,1) Primary Key,
							 AddressBookId Int Foreign Key References Address_Book(AddressBookId),
							 FirstName varchar(50),
							 LastName varchar(50),
							 Address varchar(100),
							 City varchar(50),
							 State varchar(50),
							 Zip int,
							 PhoneNumber bigint,
							 Email_ID varchar(50)    );

CREATE table PersonTypes(	 PersonTypeId Int Identity(1,1) Primary Key,
							 PersonType varchar(50), );


CREATE table PersonsDetail_Type(PersonId Int Foreign Key References PersonDetail(PersonId),
								PersonTypeId Int Foreign Key References PersonTypes(PersonTypeId),  );


CREATE table Employee_Department(PersonId Int Foreign Key References PersonDetail(PersonId),
								EmployeeID Int  ,
								DepartmentID int,);

select *from Address_Book;
select *from PersonDetail;
select *from PersonTypes;
select *from PersonsDetail_Type;
select *from Employee_Department;

---------------------------------------------
INSERT INTO Address_Book(AddressBookName) Values('Home'),('school'),('college'),(' office');
---------------------------------------------
Insert INTO PersonDetail VALUES(1,'Ram','Krishna','Poranki','Gudivada','Andhra Pradesh',520001,1234567890,'Ram@gmail.com'),
								(2,'sai','Gupta','AyyappaNagar','Vijayawada','Karnataka',520002,1234567891,'sai@gmail.com'),
								(3,'Shravanthi','Pabboji','ABC Colony','Alwal','Telangana',520003,1234567892,'Shravanthi@gamil.com'),
								(4,'Vishnu','vardhan','Kanuru','Vijayawada','Andhra Pradeshtra',520007,1234567893,'Vishnu@gmail.com');
---------Inserting values into persontype1 table--------------
INSERT INTO PersonTypes(PersonType) VALUES('Family'),('SchoolFriend'),('Friend'),('Profession');
-----------Insert values in PersonsDetail_Type1 table-------------
INSERT INTO PersonsDetail_Type(PersonId,PersonTypeId) VALUES(1,4),(2,3),(3,1),(4,2);

-----------Insert values in Employee_Department1 table-------------
INSERT INTO Employee_Department VALUES(1,123,818),(2,456,19112),(3,789,4512),(4,244,161815)

CREATE DATABASE STUDENTREGISTRATION

Create table logins (
username varchar(50) Primary key,
password varchar (50)
);

Create table registration (
regNo INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
firstName varchar(50) NOT NULL,
lastName varchar(50) NOT NULL,
dateOfBirth date NOT NULL,
gender varchar (50) NOT NULL,
address varchar (50) NOT NULL,
email varchar (50) NOT NULL,
mobilePhone Int,
homePhone Int,
parentName varchar (50) NOT NULL,
nic varchar (50),
contactNo Int
);

INSERT INTO logins (username, password)
VALUES ('Admin','Skills@123');

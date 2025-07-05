USE [Task 3 ]
------------#1-->Create the EMPLOYEE table---------
CREATE TABLE Employee (
SSN int primary key , 
first_Name varchar(20) , 
last_Name varchar(20) , 
Gender varchar(10) , 
birth_Date date , 
Super_SSN int ,FOREIGN KEY (Super_SSN) REFERENCES Employee(SSN) 
)
------------#2-->Create the DEPARTMENT table---------
CREATE TABLE Department (
Dnum int primary key , 
Dname varchar(20) , 
Dlocation varchar(20) , 
SSN int ,FOREIGN KEY (SSN) REFERENCES Employee(SSN)
)
----------هنعدل على اول جدول ونضيف فيه(Dnum As FK)---------
Alter table Employee
Add Dnum int,
CONSTRAINT Dept_num FOREIGN KEY (Dnum) REFERENCES Department(Dnum) 
go

---------#3-->Create the PROJECT table--------------------
CREATE TABLE Projects (
Pnum int PRIMARY KEY ,
Pname varchar(20) , 
location_city varchar(20) , 
Dnum int ,FOREIGN KEY (Dnum) REFERENCES Department(Dnum) 
)
----------#5-->Insert sample data into EMPLOYEE --------
INSERT INTO Employee(SSN,first_Name,last_Name,Gender,birth_Date,Super_SSN)
VALUES (1 ,'Ahmed' , 'Hossam' , 'Male' ,'2004-11-15', NULL )

INSERT INTO Employee (SSN, first_Name, last_Name, Gender, birth_Date, Super_SSN)
VALUES (2, 'Mona', 'Ibrahim', 'Female', '1999-06-21', NULL);

INSERT INTO Employee (SSN, first_Name, last_Name, Gender, birth_Date, Super_SSN)
VALUES (3, 'Youssef', 'Ali', 'Male', '2000-03-10', 1);

INSERT INTO Employee (SSN, first_Name, last_Name, Gender, birth_Date, Super_SSN)
VALUES (4, 'Sara', 'Mahmoud', 'Female', '2002-12-01', 2);

INSERT INTO Employee (SSN, first_Name, last_Name, Gender, birth_Date, Super_SSN)
VALUES (5, 'Omar', 'Nasser', 'Male', '1998-08-05', 3);

INSERT INTO Employee (SSN, first_Name, last_Name, Gender, birth_Date, Super_SSN)
VALUES (6, 'Laila', 'Samir', 'Female', '2001-04-17', 2);
----------#6-->Insert sample data into DEPARTMENT-------
INSERT INTO Department (SSN, Dname, Dlocation, Dnum)
VALUES (1, 'IT', 'Cairo', 2);

INSERT INTO Department (SSN, Dname, Dlocation, Dnum)
VALUES (2, 'HR', 'Alexandria', 3);

INSERT INTO Department (SSN, Dname, Dlocation, Dnum)
VALUES (3, 'Finance', 'Giza', 4);
------------UPDATE Dnum from NULL to num of Dept----------------
UPDATE  Employee SET Dnum = 3 WHERE SSN = 1

UPDATE Employee SET Dnum = 2 WHERE SSN = 2

UPDATE Employee SET Dnum = 3 WHERE SSN = 3

UPDATE Employee SET Dnum = 2 WHERE SSN = 4

UPDATE Employee SET Dnum = 2 WHERE SSN = 5

UPDATE Employee SET Dnum = 3 WHERE SSN = 6
-------------#7-->Update an employee's department-------------------
UPDATE Employee 
SET Dnum=2 
WHERE SSN=1 


UPDATE Employee 
SET Dnum=3 
WHERE SSN=2 
---------------#8-->Delete a dependent record--------
CREATE TABLE Dependent (
Name varchar(20) primary key,
Gender varchar(20),
Birth_Date date,
SSN int,
 FOREIGN KEY (SSN) REFERENCES Employee(SSN) on delete cascade
)

INSERT INTO Dependent(Name,Gender,Birth_Date,SSN)
VALUES('Wael','Male','3/5/2010',3)

INSERT INTO Dependent(Name,Gender,Birth_Date,SSN)
VALUES('Ahmed','Male','6/5/2010',2)

INSERT INTO Dependent(Name,Gender,Birth_Date,SSN)
VALUES('mohamed','Male','3/6/2010',4)

DELETE FROM Dependent
WHERE SSN=2 

DELETE FROM Dependent
WHERE SSN=4

-------------#9-->Retrieve all employees-----------
SELECT *
FROM Employee 
WHERE DNum = 2

SELECT *
FROM Employee 
WHERE first_Name='Ahmed'

SELECT *
FROM Employee 
WHERE Gender='Male'

---------------#10-->----------------------------------
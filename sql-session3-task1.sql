USE Session3task1

CREATE TABLE Employee (
SSN INT Primary key Identity(1,1) , 
firstName NVarchar(30) NOT NULL , 
LasstName NVarchar(30) NOT NULL , 
Gender Nvarchar(30) NOT NULL , 
Birth_Date Date UNIQUE , 
Super_SSN INT,FOREIGN KEY (Super_SSN) REFERENCES Employee(SSN)
 ON DELETE NO ACTION
 ON UPDATE NO ACTION
)
-------------------------
CREATE TABLE Department (
Dnum INT primary key identity(1,1) , 
Dname Nvarchar(50) NOT NULL , 
Locations Nvarchar(50) UNIQUE , 
SSN INt , 
Foreign key (SSN) References Employee (SSN)
 ON DELETE NO ACTION
 ON UPDATE NO ACTION
)
-------------------------
ALTER TABLE Employee 
ADD Dnum int , 
Foreign key (Dnum) References Department (Dnum)
 ON DELETE NO ACTION
 ON UPDATE NO ACTION
go
-------------------------
CREATE TABLE Projects (
Pnum INt primary key identity(1,1) , 
Pname Nvarchar(50) NOt NULL , 
locationcity Nvarchar(50) UNIQUE , 
Dnum int , 
Foreign key (Dnum) References Department (Dnum)
 ON DELETE NO ACTION
 ON UPDATE NO ACTION
)
---------------------------
CREATE TABLE Emp_Project (
    SSN INT,
    Pnum INT,
    Working_Hours INT CHECK (Working_Hours > 9),
    PRIMARY KEY (SSN, Pnum),
    FOREIGN KEY (SSN) REFERENCES Employee(SSN),
    FOREIGN KEY (Pnum) REFERENCES Projects(Pnum)
)
----------------------------
ALTER TABLE Employee 
ADD Salary int check(salary>10000)

ALTER TABLE Department
alter COLUMN Dname Nvarchar(255) 

ALTER TABLE Department
alter COLUMN lOCATIONS Nvarchar(255) 

alter table Employee
Drop column Gender

-----------------------------
INSERT INTO Employee (firstName, LasstName, Birth_Date, Super_SSN)
VALUES ('Ahmed', 'Ali', '1980-01-01', NULL);

INSERT INTO Employee (firstName, LasstName, Birth_Date, Super_SSN)
VALUES 
('Mona', 'Hassan', '1990-02-01', 1),
('Youssef', 'Ibrahim', '1992-03-15', 1),
('Sara', 'Mahmoud', '1995-04-20', 1),
('Omar', 'Tarek', '1993-05-10', 1),
('Laila', 'Nabil', '1991-06-12', 1),
('Khaled', 'Mostafa', '1989-07-25', 1),
('Nour', 'Hany', '1994-08-05', 1),
('Walid', 'Yasser', '1996-09-14', 1),
('Salma', 'Amr', '1997-10-18', 1);
---------------------------
UPDATE Employee SET Dnum = 3, Salary = 50000 WHERE SSN = 1;
UPDATE Employee SET Dnum = 2, Salary = 40000 WHERE SSN = 2;
UPDATE Employee SET Dnum = 3, Salary = 30000 WHERE SSN = 3;
UPDATE Employee SET Dnum = 2, Salary = 20000 WHERE SSN = 4;
UPDATE Employee SET Dnum = 2, Salary = 70000 WHERE SSN = 5;
UPDATE Employee SET Dnum = 3, Salary = 80000 WHERE SSN = 6;
UPDATE Employee SET Dnum = 2, Salary = 22000 WHERE SSN = 7;
UPDATE Employee SET Dnum = 3, Salary = 60000 WHERE SSN = 8;
UPDATE Employee SET Dnum = 2, Salary = 90000 WHERE SSN = 9;
UPDATE Employee SET Dnum = 3, Salary = 100000 WHERE SSN = 10;

-----------------------------

INSERT INTO Department (Dname, Locations, SSN)
VALUES 
('HR', 'Cairo', 1),
('IT', 'Alexandria', 2),
('Finance', 'Giza', 3),
('Marketing', 'Tanta', 4),
('Sales', 'Mansoura', 5),
('Logistics', 'Suez', 6),
('Support', 'Aswan', 7),
('Legal', 'Ismailia', 8),
('R&D', 'Hurghada', 9),
('Operations', 'Minya', 10);

--------------------------

INSERT INTO Projects (Pname, locationcity, Dnum)
VALUES
('HR System', 'Cairo', 1),
('Website Revamp', 'Alexandria', 2),
('Budget Tracker', 'Giza', 3),
('Social Media Campaign', 'Tanta', 4),
('Sales Dashboard', 'Mansoura', 5),
('Inventory System', 'Suez', 6),
('Customer Support Portal', 'Aswan', 7),
('Compliance Checker', 'Ismailia', 8),
('AI Research', 'Hurghada', 9),
('Workflow Automation', 'Minya', 10);

------------------------

INSERT INTO Emp_Project (SSN, Pnum, Working_Hours)
VALUES
(1, 1, 10),
(2, 2, 12),
(3, 3, 15),
(4, 4, 11),
(5, 5, 14),
(6, 6, 13),
(7, 7, 10),
(8, 8, 16),
(9, 9, 12),
(10, 10, 18);

-----------------------

SELECT *
FROM Employee 
WHERE DNum = 2

SELECT *
FROM Employee 
WHERE Salary>50000

--------------------


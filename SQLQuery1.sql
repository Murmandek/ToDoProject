Create database tobodb

ALTER TABLE EmployeeTask
DROP CONSTRAINT FK_EmployeeTask_Employee,
	 CONSTRAINT FK_EmployeeTask_Tasks;
	 
ALTER TABLE Image
	DROP CONSTRAINT FK_Image_Employee;

DROP TABLE Employee
GO
CREATE TABLE Employee
(Id int identity (1,1),
 Name varchar(100) NOT NULL,
 Age int NOT NULL,
 Address varchar(100) NOT NULL,
 Position varchar(200) NOT NULL,
 CONSTRAINT PK_Employee PRIMARY KEY CLUSTERED (Id)
)
GO

DROP TABLE Tasks
GO
CREATE TABLE Tasks
(Id int identity (1,1) NOT NULL,
 Name varchar(100) NOT NULL unique,
 Description varchar(100) NOT NULL,
 CONSTRAINT PK_Tasks PRIMARY KEY CLUSTERED (Id)
)
GO

DROP TABLE EmployeeTask
GO
CREATE TABLE EmployeeTask
(EmployeeId int NOT NULL,
 TaskId int NOT NULL,
 AppointmentDate datetime NOT NULL,
 Estemate int NOT NULL
)
GO

DROP TABLE Image
GO
CREATE TABLE Image
(Id int identity (1,1) NOT NULL,
 Name varchar(100) NOT NULL,
 Avatar varbinary(max),
 EmployeeId int NOT NULL,
 CONSTRAINT PK_Image PRIMARY KEY CLUSTERED (Id)
)
GO


ALTER TABLE EmployeeTask
	ADD CONSTRAINT FK_EmployeeTask_Employee FOREIGN KEY (EmployeeId) REFERENCES Employee ON DELETE CASCADE,
		CONSTRAINT FK_EmployeeTask_Tasks FOREIGN KEY (TaskId) REFERENCES Tasks ON DELETE CASCADE;

GO

ALTER TABLE Image
	ADD CONSTRAINT FK_Image_Employee FOREIGN KEY (EmployeeId) REFERENCES Employee ON DELETE CASCADE;

GO

DROP TABLE Person
GO
CREATE TABLE Person
(Id int identity primary key,
 Login varchar(50) NOT NULL unique,
 Password varchar(50) NOT NULL
)
GO

INSERT INTO Employee
	VALUES('Иванов Иван Иванович',22,'ул. Пушкина 20','Программист'), 
	('Азявчиков Сегрей Сергеевич',31,'ул. Минская 15','Уборщик'), 
	('Кириллов Кирилл Сергеевич',18,'ул. Бобруйская 6','Работник'), 
	('Петров Александр Сергеевич',21,'ул. Советская 10','Worker');
GO

INSERT INTO Tasks
	VALUES('Работать','Зарабатывать баблишко'), 
	('Работать2','Зарабатывать баблишко2');
GO

INSERT INTO EmployeeTask
	VALUES(1,1,'2014-06-06',1), 
	(1,2,'2020-10-09',3), 
	(3,1,'2013-06-06',5);
GO

INSERT INTO Person
	VALUES('admin@gmail.com','12345'), 
	('qwerty@gmail.com','55555'), 
	('qwqw@mail.ru','111');
GO

INSERT INTO Image
	VALUES('имя1','',1), 
	('имя2','',2), 
	('имя3','',3), 
	('имя4','',4);
GO
SELECT * from Tasks
GO
SELECT * from Employee
GO
SELECT * from Image
GO
SELECT * from EmployeeTask
GO

/*SELECT * from EmployeeTask
GO
Select emp.Id, ts.Id, emp.Name, ts.Name, ts.Description, AppointmentDate, Estemate
FROM EmployeeTask as empTask INNER JOIN Employee as emp ON emp.Id = empTask.EmployeeId
INNER JOIN Tasks as ts ON ts.Id = empTask.TaskId

GO

Select emp.Id, emp.Name FROM Employee as emp
INNER JOIN EmployeeTask as empTasks ON emp.Id = empTasks.EmployeeId
WHERE TaskId IN (Select Id from Tasks)
GO

Select ts.Name, ts.Description FROM Tasks as ts
INNER JOIN EmployeeTask as empTasks ON ts.Id = empTasks.TaskId
WHERE EmployeeId IN (Select Id from Employee)
GO
Select * from Image

Select emp.Id, emp.Name FROM Employee as emp INNER JOIN EmployeeTask 
as empTask ON emp.id = empTask.EmployeeId 
WHERE empTask.TaskId = 2

Select emp.Name, ts.Name, ts.Description FROM Employee as emp, Task as ts
WHERE emp.Id IN (SELECT EmployeeId FROM EmployeeTask) AND ts.Id IN (SELECT TaskId FROM EmployeeTask)*/
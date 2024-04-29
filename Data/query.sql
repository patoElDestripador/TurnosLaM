-- SQLBook: Code
-- Active: 1713899042493@@bnaeksshtlfrlbvcc3k7-mysql.services.clever-cloud.com@3306@bnaeksshtlfrlbvcc3k7

CREATE TABLE Employees (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Email VARCHAR(100),
    PhoneNumber VARCHAR(20),
    Status ENUM('Active', 'Disabled', 'Vacation')
);
--MTO= Intente cambiar la base de datos agregando los campos necesarios. 
ALTER TABLE Employees 
ADD Skill1 BOOLEAN DEFAULT FALSE,
ADD Skill2 BOOLEAN DEFAULT FALSE,
ADD Skill3 BOOLEAN DEFAULT FALSE,
ADD Skill4 BOOLEAN DEFAULT FALSE;
--Me dan problemas a la hora de actualizar la tabla para guardar las habilidades.

CREATE TABLE Users (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    EmployeesId INT NOT NULL,
    UserName VARCHAR(50),
    Password VARCHAR(120),
    Role ENUM('Asesor', 'Mision Controller'),
    Module VARCHAR(30),
    Status ENUM('LogIn', 'LogOut', 'Break', 'Baño'),
    Skills VARCHAR(120),
    FOREIGN KEY (EmployeesId) REFERENCES Employees(Id) ON DELETE CASCADE
);

CREATE TABLE Patients (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Document VARCHAR(50) UNIQUE,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Gender ENUM('Hombre', 'Mujer', 'Otro'),
    Address VARCHAR(255),
    PhoneNumber VARCHAR(20),
    Eps VARCHAR(20)
);

CREATE TABLE Services (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    ServiceName VARCHAR(45) NOT NULL,
    Status ENUM('Enabled', 'Disabled')
);

CREATE TABLE Shifts (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    PatientId INT NOT NULL,
    ServiceId INT NOT NULL,
    CreationDate DATETIME,
    FOREIGN KEY (PatientId) REFERENCES Patients(Id) ON DELETE CASCADE,
    FOREIGN KEY (ServiceId) REFERENCES Services(Id) ON DELETE CASCADE
);

CREATE TABLE Queues (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    UserId INT ,
    ShiftId INT NOT NULL,
    Status ENUM('Atendida', 'En espera', 'En progreso','Ausente','Por reasinar'),
    AssignedShift VARCHAR(45),
    CreationDate DATETIME,
    AssignmentTime DATETIME,
    ClosingTime DATETIME,
    Calls INT,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    FOREIGN KEY (ShiftId) REFERENCES Shifts(Id) ON DELETE CASCADE
);

CREATE TABLE DailyCounters   (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Day DATETIME NOT NULL,
    ServiceName Varchar(4) NOT NULL,
    Counter INT NOT NULL
);


INSERT INTO Employees (FirstName, LastName, Email, PhoneNumber, Status) VALUES
('John', 'Doe', 'john.doe@example.com', '123-456-7890', 'Active'),
('Jane', 'Smith', 'jane.smith@example.com', '987-654-3210', 'Active'),
('Alice', 'Johnson', 'alice.johnson@example.com', '555-123-4567', 'Active'),
('Bob', 'Brown', 'bob.brown@example.com', '444-789-0123', 'Active'),
('Sarah', 'Miller', 'sarah.miller@example.com', '111-222-3333', 'Active'),
('Michael', 'Brown', 'michael.brown@example.com', '444-555-6666', 'Active'),
('Emma', 'Garcia', 'emma.garcia@example.com', '777-888-9999', 'Active'),
('William', 'Taylor', 'william.taylor@example.com', '123-456-7890', 'Active');

INSERT INTO Users (EmployeesId, UserName, Password, Role,Module, Status, Skills) VALUES
(1, '1', '1', 'Asesor', 'A-0' ,'LogIn', 'Autorización de medicamentos,Pago de facturas,Información general'),
(2, 'janesmith', 'password456', 'Mision Controller','A-03', 'LogIn', 'Autorización de medicamentos,Pago de facturas,Información general'),
(3, 'alicejohnson', 'password789', 'Asesor','A-04', 'LogIn', 'Autorización de medicamentos,Pago de facturas,Información general'),
(4, 'bobbrown', 'password000', 'Asesor', 'A-05','LogOut', 'Pago de facturas,Información general'),
(5, 'sarahmiller', 'password123', 'Asesor', 'A-06','LogIn', 'Información general'),
(6, 'michaelbrown', 'password456', 'Mision Controller','A-07', 'LogOut', 'Autorización de medicamentos,Pago de facturas,Información general'),
(7, 'emmagarcia', 'password789', 'Asesor', 'A-08','LogIn', 'Pago de facturas,Información general'),
(8, 'williamtaylor', 'password000', 'Asesor', 'A-09','LogOut', 'Solicitud de medicamentos,Pago de facturas,Autorización de medicamentos');


INSERT INTO Patients (Document,FirstName, LastName, Gender, Address,PhoneNumber,Eps) VALUES
('100000001','Michael', 'Johnson', 'Hombre', '123 Main St', '333-333-585','Misery'),
('100000002','Emily', 'Davis', 'Mujer', '456 Elm St', '333-333-585','Misery'),
('100000003','Alex', 'Martinez', 'Hombre', '789 Oak St', '333-333-585','Misery'),
('100000004','Olivia', 'Wilson', 'Mujer', '321 Pine St', '333-333-585','Misery'),
('100000005','James', 'Lee', 'Hombre', '654 Maple St', '333-333-585','Misery'),
('100000006','Sophia', 'Rodriguez', 'Mujer', '987 Cedar St', '333-333-585','Misery');


INSERT INTO Services (ServiceName, Status) VALUES
('Autorización de medicamentos', 'Enabled'),
('Pago de facturas', 'Enabled'),
('Solicitud de medicamentos', 'Enabled'),
('Información general', 'Enabled'),
('X-ray', 'Disabled'),
('Test de sangre', 'Disabled'),
('Ultrasonido', 'Disabled');

INSERT INTO Shifts (PatientId, ServiceId, CreationDate, Shift) VALUES
(1, 1, '2024-04-23 08:00:00', 1),
(2, 2, '2024-04-23 09:00:00', 2),
(3, 1, '2024-04-23 10:00:00', 3),
(4, 3, '2024-04-24 08:00:00', 1),
(5, 1, '2024-04-24 09:00:00', 2),
(6, 2, '2024-04-24 10:00:00', 3);

INSERT INTO Queues (UserId, ShiftId, Status, AssignedShift, CreationDate, AssignmentTime, ClosingTime, Calls) VALUES
(1, 1, 'Atendida', 'Morning', '2024-04-23 07:55:00', '2024-04-23 08:05:00', '2024-04-23 08:15:00', 1),
(2, 2, 'En espera', 'Afternoon', '2024-04-23 08:55:00', '2024-04-23 09:05:00', NULL, 2),
(3, 3, 'Ausente', 'Evening', '2024-04-23 09:55:00', '2024-04-23 10:05:00', '2024-04-23 10:20:00', 0),
(5, 4, 'Atendida', 'Morning', '2024-04-24 07:55:00', '2024-04-24 08:05:00', '2024-04-24 08:15:00', 2),
(6, 5, 'En espera', 'Afternoon', '2024-04-24 08:55:00', '2024-04-24 09:05:00', NULL, 1),
(7, 6, 'Ausente', 'Evening', '2024-04-24 09:55:00', '2024-04-24 10:05:00', '2024-04-24 10:20:00', 0);

CREATE VIEW ViewQueuesStatus AS
SELECT 
    q.Id AS QueueId,
    q.UserId,
    e.Id AS EmployeeId,
    e.FirstName,
    e.LastName,
    COUNT(*) AS PendingShifts,
    u.Status AS UserStatus,
    u.Skills
FROM
    Queues q
    INNER JOIN Users u ON q.UserId = u.Id
    INNER JOIN Employees e ON u.EmployeesId = e.Id
WHERE
    q.Status = 'En proceso'
GROUP BY
    q.UserId
HAVING
    PendingShifts > 5;



CREATE VIEW ViewServiceDemanded AS
SELECT
    s.Id AS ServiceId,
    s.ServiceType,
    COUNT(*) AS ShiftDemand
FROM
    Shifts sh
    INNER JOIN Services s ON sh.ServiceId = s.Id
    INNER JOIN Queues q ON sh.Id = q.ShiftId
WHERE
    DATE(sh.CreationDate) = CURDATE()
    AND (q.Status = 'in process' OR q.Status = 'Slope')
GROUP BY


    sh.ServiceId
ORDER BY
    ShiftDemand DESC
LIMIT 1;


CREATE VIEW ViewQueueToReassign AS
SELECT 
q.Id,
q.UserId,
q.Status, 
s.ServiceName AS AssociatedService
FROM Queues q
LEFT JOIN Shifts sh ON q.ShiftId = sh.Id
LEFT JOIN Services s ON sh.ServiceId = s.Id
WHERE q.Status = 'Por reasignar' AND q.UserId IS NULL
ORDER BY q.CreationDate ASC;



--DROP TABLES Employees, Users, Patients, Services, Shifts, Queues;

-- DROP VIEW ViewQueuesStatus,ViewServiceDemanded;



SELECT * FROM ViewQueueToReassign;

Select * FROM ViewQueuesStatus;

Select * FROM ViewServiceDemanded;



CREATE VIEW ViewNextShift AS;

SELECT * FROM Queues q
WHERE
    q.Status = 'En espera'
GROUP BY
    q.UserId
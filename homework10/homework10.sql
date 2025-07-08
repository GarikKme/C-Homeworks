# Дмитрий приветствую, использую MySQL, вместо nvarchar(max) - varchar(255) могу конечно TEXT но ты не одобрил) и вместо MONEY - DECIMAL(), вместо IDENTITY - AUTO_INCREMENT,
# EndTime   TIME         NOT NULL CHECK (EndTime > StartTime) - не могу такое выражение в MySQL сделать, буду использовать без CHECK
# EndTime TIME NOT NULL

CREATE DATABASE hospital CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci; # створення бд

USE hospital;
# використовуємо її

# створення таблиць
CREATE TABLE Departments
(
    Id        INT AUTO_INCREMENT PRIMARY KEY,
    Building  INT            NOT NULL CHECK ( Building BETWEEN 1 AND 5),
    Financing DECIMAL(12, 2) NOT NULL DEFAULT 0 CHECK ( Financing >= 0 ),
    Name      VARCHAR(100)   NOT NULL UNIQUE CHECK ( Name != '' )
);

CREATE TABLE Diseases
(
    Id       INT AUTO_INCREMENT PRIMARY KEY,
    Name     VARCHAR(100) NOT NULL UNIQUE CHECK ( Name != '' ),
    Severity INT          NOT NULL DEFAULT 1 CHECK ( Severity >= 1 )
);

CREATE TABLE Doctors
(
    Id      INT AUTO_INCREMENT PRIMARY KEY,
    Name    VARCHAR(255)   NOT NULL CHECK ( Name != '' ),
    Surname VARCHAR(255)   NOT NULL CHECK ( Surname != '' ),
    Phone   CHAR(10),
    Salary  DECIMAL(10, 2) NOT NULL CHECK ( Salary > 0 )
);

CREATE TABLE Examinations
(
    Id        INT AUTO_INCREMENT PRIMARY KEY,
    DAyOfWeek INT          NOT NULL CHECK ( DAyOfWeek BETWEEN 1 AND 7 ),
    Name      VARCHAR(100) NOT NULL UNIQUE CHECK ( Name != '' ),
    StartTime TIME         NOT NULL CHECK ( StartTime >= '08:00:00' AND StartTime <= '18:00:00' ),
    #EndTime   TIME         NOT NULL CHECK (EndTime > StartTime)
    EndTime   TIME         NOT NULL
);

CREATE TABLE Wards
(
    Id       INT AUTO_INCREMENT PRIMARY KEY,
    Building INT         NOT NULL CHECK ( Building BETWEEN 1 AND 5 ),
    Floor    INT         NOT NULL CHECK ( Floor >= 1 ),
    Name     VARCHAR(20) NOT NULL UNIQUE CHECK ( Name != '' )
);

# Заповнюємо таблиці даними
INSERT INTO Departments (Building, Financing, Name)
VALUES (1, 250000.00, 'Cardiology'),
       (2, 180000.00, 'Neurology'),
       (3, 300000.00, 'Oncology');

INSERT INTO Diseases (Name, Severity)
VALUES ('Flu', 2),
       ('COVID-19', 5),
       ('Migraine', 1),
       ('Pneumonia', 3);

INSERT INTO Doctors (Name, Surname, Phone, Salary)
VALUES ('Ivan', 'Shevchenko', '0671234567', 22000.00),
       ('Olena', 'Petrenko', '0509876543', 27500.00),
       ('Dmytro', 'Kovalchuk', NULL, 25000.00);

INSERT INTO Examinations (DAyOfWeek, Name, StartTime, EndTime)
VALUES (1, 'Blood Test', '09:00:00', '09:30:00'),
       (2, 'X-Ray', '10:00:00', '10:45:00'),
       (3, 'MRI', '14:00:00', '15:30:00');

INSERT INTO Wards (Building, Floor, Name)
VALUES (1, 1, 'Alpha'),
       (1, 2, 'Beta'),
       (2, 3, 'Gamma');

# Для бази даних «Лікарня» створіть такі запити:

# Вивести вміст таблиці палат.
SELECT *
FROM Wards;

# Вивести прізвища та телефони усіх лікарів.
SELECT Surname, Phone
FROM Doctors;

# Вивести усі поверхи без повторень, де розміщуються палати.
SELECT DISTINCT Floor
FROM Wards;

# Вивести назви захворювань під назвою «Name of Disease» та ступінь їхньої тяжкості під назвою «Severity of Disease».
SELECT Name AS 'Name of Disease', Severity AS 'Severity of Disease'
FROM Diseases;

# Застосувати вираз FROM для будь-яких трьох таблиць бази даних, використовуючи псевдоніми.
SELECT d.Name   AS 'Doctor Name',
       dep.Name AS 'Department',
       dis.Name AS 'Disease'
FROM Doctors AS d,
     Departments AS dep,
     Diseases AS dis
LIMIT 5;

# Вивести назви відділень, які знаходяться у корпусі 5 з фондом фінансування меншим, ніж 30000.
SELECT Name
FROM Departments
WHERE Building = 5
  AND Financing < 30000;

# Вивести назви відділень, які знаходяться у корпусі 3 з фондом фінансування у діапазоні від 12000 до 15000.
SELECT Name
FROM Departments
WHERE Building = 3
  AND Financing BETWEEN 12000 AND 15000;

# Вивести назви палат, які знаходяться у корпусах 4 та 5 на 1-му поверсі.
SELECT Name
FROM Wards
WHERE (Building = 4 OR Building = 5)
  AND Floor = 1;
SELECT Name
FROM Wards
WHERE Building IN (4, 5)
  AND Floor = 1;

# Вивести назви, корпуси та фонди фінансування відділень, які знаходяться у корпусах 3 або 6 та мають фонд фінансування менший, ніж 11000 або більший за 25000.
SELECT Name, Building, Financing
FROM Departments
WHERE Building IN (3, 6)
  AND (Financing < 11000 OR Financing > 25000);


# Вивести прізвища лікарів, зарплата (сума ставки та надбавки) яких перевищує 1500.
# Спочатку в нас в ТЗ немає бонусів додаємл їх  і оновлюємо дані.
ALTER TABLE Doctors
    ADD Bonus DECIMAL(10, 2) DEFAULT 0 CHECK ( Bonus >= 0 );
UPDATE Doctors
SET Bonus = 500
WHERE Surname = 'Shevchenko';

UPDATE Doctors
SET Bonus = 800
WHERE Surname = 'Petrenko';

UPDATE Doctors
SET Bonus = 0
WHERE Surname = 'Kovalchuk';
SELECT Surname, Salary, Bonus, (Salary + Bonus) AS TotalSalary
FROM Doctors
WHERE (Salary + Bonus) > 1500;


# Вивести прізвища лікарів, у яких половина зарплати перевищує триразову надбавку
SELECT Surname, Salary, Bonus
FROM Doctors
WHERE (Salary / 2) > (Bonus * 3);


# Вивести назви обстежень без повторень, які проводяться у перші три дні тижня з 12:00 до 15:00.
SELECT DISTINCT Name
FROM Examinations
WHERE (DAyOfWeek IN (1, 2, 3))
  AND StartTime BETWEEN '12:00:00' AND '15:00:00';


# Вивести назви та номери корпусів відділень, які знаходяться у корпусах 1, 3, 8 або 10.
SELECT Name, Building
FROM Departments
WHERE Building IN (1, 3, 8, 10);


# Вивести назви захворювань усіх ступенів тяжкості, крім 1-го та 2-го.
SELECT Name
FROM Diseases
WHERE Severity != 1
  AND Severity != 2;
#second variant
SELECT Name
FROM Diseases
WHERE Severity NOT IN (1, 2);


# Вивести назви відділень, які не знаходяться у 1-му або 3-му корпусі.
SELECT Name
FROM Departments
WHERE Building NOT IN (1, 3);


# Вивести назви відділень, які знаходяться у 1-му або 3-му корпусі.
SELECT Name
FROM Departments
WHERE Building IN (1, 3);


# Вивести прізвища лікарів, що починаються з літери «N».
SELECT Surname
FROM Doctors
WHERE Surname LIKE 'N%'; 
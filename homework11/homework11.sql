CREATE DATABASE barbershop CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci; # створення бд

USE barbershop;
# використовуємо її

# створення таблиць
# Clients 1—* Appointments *—1 Barbers
# Appointments *—* Services (через AppointmentServices)
# Barbers *—* Services (через BarberServices)
# Appointments 1—1 Feedback

CREATE TABLE Barbers
(
    Id        INT AUTO_INCREMENT PRIMARY KEY,
    FullName  VARCHAR(100)                       NOT NUll,
    Gender    ENUM ('Male', 'Female', 'Other')   NOT NULL,
    Phone     CHAR(10)                           NOT NULL,
    Email     VARCHAR(100) UNIQUE                NOT NULL,
    BirthDate DATE                               NOT NULL,
    HireDate  DATE                               NOT NULL,
    Position  ENUM ('Chief', 'Senior', 'Junior') NOT NULL
);

CREATE TABLE Clients
(
    Id       INT AUTO_INCREMENT PRIMARY KEY,
    FullName VARCHAR(100) NOT NULL,
    Phone    CHAR(10)     NOT NULL,
    Email    VARCHAR(100) UNIQUE
);

CREATE TABLE Services
(
    Id       INT AUTO_INCREMENT PRIMARY KEY,
    Name     VARCHAR(100)  NOT NULL,
    Price    DECIMAL(8, 2) NOT NULL,
    Duration TIME          NOT NULL
);


CREATE TABLE BarberServices
(
    BarberId  INT,
    ServiceId INT,
    PRIMARY KEY (BarberId, ServiceId),
    FOREIGN KEY (BarberId) REFERENCES Barbers (Id) ON DELETE CASCADE,
    FOREIGN KEY (ServiceId) REFERENCES Services (Id) ON DELETE CASCADE
);

CREATE TABLE Schedule
(
    Id            INT AUTO_INCREMENT PRIMARY KEY,
    BarberId      INT  NOT NULL,
    AvailableDate DATE NOT NULL,
    StartTime     TIME NOT NULL,
    EndTime       TIME NOT NULL,
    FOREIGN KEY (BarberId) REFERENCES Barbers (Id) ON DELETE CASCADE
);

CREATE TABLE Appointments
(
    Id              INT AUTO_INCREMENT PRIMARY KEY,
    ClientId        INT           NOT NULL,
    BarberId        INT           NOT NULL,
    AppointmentDate DATE          NOT NULL,
    StartTime       TIME          NOT NULL,
    TotalPrice      DECIMAL(8, 2) NOT NULL,
    FOREIGN KEY (ClientId) REFERENCES Clients (Id),
    FOREIGN KEY (BarberId) REFERENCES Barbers (Id)
);

CREATE TABLE AppointmentServices
(
    AppointmentId INT,
    ServiceId     INT,
    PRIMARY KEY (AppointmentId, ServiceId),
    FOREIGN KEY (AppointmentId) REFERENCES Appointments (Id) ON DELETE CASCADE,
    FOREIGN KEY (ServiceId) REFERENCES Services (Id) ON DELETE CASCADE
);

CREATE TABLE Feedback
(
    Id            INT AUTO_INCREMENT PRIMARY KEY,
    AppointmentId INT                                                     NOT NULL,
    Rating        ENUM ('Very Bad', 'Bad', 'Normal', 'Good', 'Excellent') NOT NULL,
    Comment       TEXT,
    FOREIGN KEY (AppointmentId) REFERENCES Appointments (Id) ON DELETE CASCADE
);

# Заповнюємо таблиці
INSERT INTO Barbers (FullName, Gender, Phone, Email, BirthDate, HireDate, Position)
VALUES ('Ivan Shevchenko', 'Male', '0671112233', 'ivan@barbershop.com', '1990-03-15', '2020-06-01', 'Chief'),
       ('Oleh Bondarenko', 'Male', '0505556677', 'oleh@barbershop.com', '1988-07-22', '2021-01-10', 'Senior'),
       ('Andrii Kovalenko', 'Male', '0939876543', 'andrii@barbershop.com', '1995-11-05', '2023-03-20', 'Junior');

INSERT INTO Clients (FullName, Phone, Email)
VALUES ('Dmytro Sydorenko', '0971234567', 'dmytro@gmail.com'),
       ('Oksana Datsenko', '0735554411', 'oksana@gmail.com'),
       ('Mykhailo Kravets', '0667891122', 'mykhailo@gmail.com');

INSERT INTO Services (Name, Price, Duration)
VALUES ('Haircut', 300.00, '00:40:00'),
       ('Beard Trim', 200.00, '00:30:00'),
       ('Shaving', 150.00, '00:20:00'),
       ('Hair & Beard Combo', 450.00, '01:00:00');

INSERT INTO BarberServices (BarberId, ServiceId)
VALUES (1, 1),
       (1, 2),
       (1, 4), -- Chief делает всё
       (2, 1),
       (2, 2),
       (2, 3), -- Senior — 3 услуги
       (3, 1),
       (3, 3); -- Junior — только базовые

INSERT INTO Schedule (BarberId, AvailableDate, StartTime, EndTime)
VALUES (1, '2025-07-13', '10:00:00', '18:00:00'),
       (2, '2025-07-13', '12:00:00', '20:00:00'),
       (3, '2025-07-13', '09:00:00', '15:00:00');

INSERT INTO Appointments (ClientId, BarberId, AppointmentDate, StartTime, TotalPrice)
VALUES (1, 1, '2025-07-13', '10:30:00', 450.00),
       (2, 2, '2025-07-13', '13:00:00', 300.00),
       (3, 3, '2025-07-13', '09:30:00', 150.00);

INSERT INTO AppointmentServices (AppointmentId, ServiceId)
VALUES (1, 1),
       (1, 2), -- Haircut + Beard Trim (combo)
       (2, 1), -- Haircut
       (3, 3); -- Shaving

INSERT INTO Feedback (AppointmentId, Rating, Comment)
VALUES (1, 'Excellent', 'Great experience, very professional!'),
       (2, 'Good', 'Nice haircut, will come again.'),
       (3, 'Normal', 'Decent but could be faster.');

# Використовуючи тригери, функції користувача, збережені процедури реалізуйте наступну функціональність: 
# Повернути ПІБ всіх барберів салону. 
DELIMITER //

CREATE PROCEDURE GetAllBarbers()
BEGIN
    SELECT FullName FROM Barbers;
END //

DELIMITER ;
CALL GetAllBarbers();

# Повернути інформацію про всіх синьйор-барберів. 

DELIMITER //

CREATE PROCEDURE GetAllSeniorBarbers()
BEGIN
    SELECT * FROM Barbers WHERE Position = 'Senior';
END //

DELIMITER ;
CALL GetAllSeniorBarbers();

# Повернути інформацію про всіх барберів, які можуть надати послугу традиційного гоління бороди. 

DELIMITER //

CREATE PROCEDURE GetBarbersForTraditionalShave()
BEGIN
    SELECT b.*
    FROM Barbers b
             JOIN BarberServices bs ON b.Id = bs.BarberId
             JOIN Services s ON s.Id = bs.ServiceId
    WHERE s.Name = 'Traditional Beard Shave';
END //

DELIMITER ;

CALL GetBarbersForTraditionalShave();

# Повернути інформацію про всіх барберів, які можуть надати конкретну послугу. Інформація про потрібну послугу надається як параметр.

DELIMITER //

CREATE PROCEDURE GetBarbersByService(IN serviceName VARCHAR(100))
BEGIN
    SELECT b.*
    FROM Barbers AS b
             JOIN BarberServices AS bs ON b.Id = bs.BarberId
             JOIN Services AS s ON bs.ServiceId = s.Id
    WHERE s.Name = serviceName;
END //

DELIMITER ;
CALL GetBarbersByService('Haircut');

# Повернути інформацію про всіх барберів, які працюють понад зазначену кількість років. Кількість років передається як параметр. 

DELIMITER //

CREATE PROCEDURE GetBarbersWithExperience(IN minYears INT)
BEGIN
    SELECT *
    FROM Barbers
    WHERE TIMESTAMPDIFF(YEAR, HireDate, CURDATE()) > minYears;
END //

DELIMITER ;

CALL GetBarbersWithExperience(2);

# Повернути кількість синьйор-барберів та кількість джуніор-барберів. 

DELIMITER //

CREATE PROCEDURE CountSeniorAndJuniorBarbers()
BEGIN
    SELECT Position,
           COUNT(*) AS Count
    FROM Barbers
    WHERE Position IN ('Senior', 'Junior')
    GROUP BY Position;
END //

DELIMITER ;

CALL CountSeniorAndJuniorBarbers();

# Повернути інформацію про постійних клієнтів. Критерій постійного клієнта: був у салоні задану кількість разів. Кількість передається як параметр. 

DELIMITER //

CREATE PROCEDURE GetLoyalClients(IN visitCount INT)
BEGIN
    SELECT c.Id,
           c.FullName,
           c.Phone,
           c.Email,
           COUNT(a.Id) AS TotalVisits
    FROM Clients c
             JOIN Appointments a ON c.Id = a.ClientId
    GROUP BY c.Id, c.FullName, c.Phone, c.Email
    HAVING COUNT(a.Id) >= visitCount;
END //

DELIMITER ;

CALL GetLoyalClients(2);

# Заборонити можливість видалення інформації про чиф-барбер, якщо не додано другий чиф-барбер.

DELIMITER //

CREATE TRIGGER prevent_single_chief_delete
    BEFORE DELETE
    ON Barbers
    FOR EACH ROW
BEGIN
    DECLARE chief_count INT;

    -- Считаем количество Chief-барберов, исключая того, кого удаляют
    SELECT COUNT(*)
    INTO chief_count
    FROM Barbers
    WHERE Position = 'Chief'
      AND Id != OLD.Id;

    -- Если других нет — выбрасываем ошибку
    IF chief_count = 0 THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Неможливо видалити останнього чиф-барбера.';
    END IF;
END //

DELIMITER ;


# Заборонити додавати барберів молодше 21 року
DELIMITER //

CREATE TRIGGER prevent_young_barber_update
    BEFORE UPDATE
    ON Barbers
    FOR EACH ROW
BEGIN
    DECLARE age INT;

    SET age = TIMESTAMPDIFF(YEAR, NEW.BirthDate, CURDATE());

    IF age < 21 THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Неможливо оновити: барберу має бути щонайменше 21 рік.';
    END IF;
END //

DELIMITER ;

# Функції користувача
# Створіть наступні функції користувача: 
# Функція користувача повертає вітання в стилі «Hello, ІМ'Я!» Де ІМ'Я передається як параметр. Наприклад, якщо передали Nick, то буде Hello, Nick!

DELIMITER //
CREATE FUNCTION SayHello(name VARCHAR(100))
    RETURNS VARCHAR(200)
    DETERMINISTIC
BEGIN
    RETURN CONCAT('Hello, ', name, '!');
END //
DELIMITER ;
SELECT SayHello('Nick');

# Функція користувача повертає інформацію про поточну кількість хвилин; 

DELIMITER //
CREATE FUNCTION GetCurrentMinutes()
    RETURNS INT
    DETERMINISTIC
BEGIN
    RETURN MINUTE(NOW());
END //
DELIMITER ;

SELECT GetCurrentMinutes();

# ■ Функція користувача повертає інформацію про поточний рік; 

DELIMITER //
CREATE FUNCTION GetCurrentYear()
    RETURNS INT
    DETERMINISTIC
BEGIN
    RETURN YEAR(NOW());
END //
DELIMITER ;

SELECT GetCurrentYear();

# ■ Функція користувача повертає інформацію про те: парний або непарний рік; 

DELIMITER //
CREATE FUNCTION GetOddOrEvenYear()
    RETURNS VARCHAR(10)
    DETERMINISTIC
BEGIN
    DECLARE result VARCHAR(10);
    IF MOD(YEAR(NOW()), 2) = 0 THEN
        SET result = 'Even';
    ELSE
        SET result = 'Odd';
    END IF;
    RETURN result;
END //
DELIMITER ;

SELECT GetOddOrEvenYear();

# ■ Функція користувача приймає число і повертає yes, якщо число просте і no, якщо число не просте; 


DELIMITER //

CREATE FUNCTION IsPrimeNumber(n INT)
    RETURNS VARCHAR(3)
    DETERMINISTIC
BEGIN
    DECLARE i INT DEFAULT 2;

    IF n < 2 THEN
        RETURN 'no';
    END IF;

    WHILE i <= SQRT(n)
        DO
            IF n % i = 0 THEN
                RETURN 'no';
            END IF;
            SET i = i + 1;
        END WHILE;

    RETURN 'yes';
END //

DELIMITER ;

SELECT IsPrimeNumber(4);

# Функція користувача приймає як параметри п'ять чисел. Повертає суму мінімального та максимального значення з переданих п'яти параметрів;

DELIMITER //

CREATE FUNCTION GetMinAndMaxSum(num1 INT, num2 INT, num3 INT, num4 INT, num5 INT)
    RETURNS INT
    DETERMINISTIC
BEGIN
    DECLARE min_val INT;
    DECLARE max_val INT;

    SET min_val = LEAST(num1, num2, num3, num4, num5);
    SET max_val = GREATEST(num1, num2, num3, num4, num5);

    RETURN min_val + max_val;
END //
DELIMITER ;

SELECT GetMinAndMaxSum(10, 3, 22, 26, 14);

# Функція користувача показує всі парні або непарні числа в переданому діапазоні. Функція приймає три параметри: початок діапазону, кінець діапазону, парне чи непарне показувати.

DELIMITER //

CREATE FUNCTION GetEvenOrOddRange(start_num INT, end_num INT, type VARCHAR(10))
    RETURNS TEXT
    DETERMINISTIC
BEGIN
    DECLARE result TEXT DEFAULT '';
    DECLARE current INT;

    SET current = start_num;

    WHILE current <= end_num
        DO
            IF (type = 'even' AND MOD(current, 2) = 0) OR
               (type = 'odd' AND MOD(current, 2) != 0) THEN
                SET result = CONCAT(result, current, ', ');
            END IF;
            SET current = current + 1;
        END WHILE;

    -- Удалим последнюю запятую и пробел
    IF LENGTH(result) > 0 THEN
        SET result = LEFT(result, LENGTH(result) - 2);
    END IF;

    RETURN result;
END //

DELIMITER ;

SELECT GetEvenOrOddRange(1, 10, 'even');
-- Результат: '2, 4, 6, 8, 10'

SELECT GetEvenOrOddRange(1, 10, 'odd');
-- Результат: '1, 3, 5, 7, 9'

# Збережені процедури
# Створіть наступні збережені процедури:
# Збережена процедура виводить «Hello, world!»; 

DELIMITER //
CREATE PROCEDURE SayHelloWOrld()
BEGIN
    SELECT 'Hello, world!';
END //
DELIMITER ;

CALL SayHelloWOrld();

# Збережена процедура повертає інформацію про поточний час; 

DELIMITER //
CREATE PROCEDURE GetTime()
BEGIN
    SELECT TIME(NOW());
END //
DELIMITER ;

CALL GetTime();

# Збережена процедура повертає інформацію про поточну дату; 

DELIMITER //
CREATE PROCEDURE GetCurDate()
BEGIN
    SELECT CURDATE();
END //
DELIMITER ;

CALL GetCurDate();

# Збережена процедура приймає три числа і повертає їхню суму; 

DELIMITER //
CREATE PROCEDURE GetSumFromNumbers(num1 DECIMAL(10, 2), num2 DECIMAL(10, 2), num3 DECIMAL(10, 2))
BEGIN
    DECLARE result DECIMAL(10, 2);
    SET result = num1 + num2 + num3;
    SELECT result AS SumOfTheNumbers;
END //
DELIMITER ;

CALL GetSumFromNumbers(54.3, 12.5, 344);

# Збережена процедура приймає три числа і повертає середньоарифметичне трьох чисел; 

DELIMITER //
CREATE PROCEDURE GetAverageNumbers(num1 DECIMAL(10, 2), num2 DECIMAL(10, 2), num3 DECIMAL(10, 2))
BEGIN
    DECLARE result DECIMAL(10, 2);
    SET result = (num1 + num2 + num3) / 3;
    SELECT result AS AverageOfTheNumbers;
END //
DELIMITER ;

CALL GetAverageNumbers(54.3, 12.5, 344);

# Збережена процедура приймає три числа і повертає максимальне значення; 

DELIMITER //
CREATE PROCEDURE GetMaxNumber(num1 DECIMAL(10, 2), num2 DECIMAL(10, 2), num3 DECIMAL(10, 2))
BEGIN
    DECLARE result DECIMAL(10, 2);
    SET result = GREATEST(num1, num2, num3);
    SELECT result AS MaxNumber;
END //
DELIMITER ;

CALL GetMaxNumber(54.3, 12.5, 344);

# Збережена процедура приймає три числа і повертає мінімальне значення; 

DELIMITER //
CREATE PROCEDURE GetMinNumber(num1 DECIMAL(10, 2), num2 DECIMAL(10, 2), num3 DECIMAL(10, 2))
BEGIN
    DECLARE result DECIMAL(10, 2);
    SET result = LEAST(num1, num2, num3);
    SELECT result AS MinNumber;
END //
DELIMITER ;

CALL GetMinNumber(54.3, 12.5, 344);

#Збережена процедура приймає число та символ. 

#В результаті роботи збереженої процедури відображається  лінія довжиною, що дорівнює числу. Лінія побудована із символу, вказаного у другому параметрі. 

#Наприклад, якщо було передано 5 та #, ми отримаємо лінію такого виду #####; 

DELIMITER //
CREATE PROCEDURE ShowSymbols(num1 INT, symbol VARCHAR(1))
BEGIN
    DECLARE output_str VARCHAR(255) DEFAULT '';
    DECLARE counter INT DEFAULT 0;
    WHILE counter < num1
        DO
            SET output_str = CONCAT(output_str, symbol);
            SET counter = counter + 1;
        END WHILE;
    SELECT output_str AS Line;
END //
DELIMITER ;

CALL ShowSymbols(15, '#');

# Збережена процедура приймає як параметр число і повертає його факторіал. 
# Формула розрахунку факторіалу: n! = 1 * 2 * ... n. Наприклад, 3! = 1 * 2 * 3 = 6;

DELIMITER //

CREATE PROCEDURE GetFactorial(IN num INT)
BEGIN
    DECLARE result BIGINT DEFAULT 1;
    DECLARE counter INT DEFAULT 1;

    IF num < 0 THEN
        SELECT 'Factorial is not defined for negative numbers.' AS ErrorMessage;
    ELSE
        WHILE counter <= num
            DO
                SET result = result * counter;
                SET counter = counter + 1;
            END WHILE;
        SELECT result AS Factorial;
    END IF;
END //

DELIMITER ;

CALL GetFactorial(7);

# Збережена процедура приймає два числові параметри. Перший параметр – це число. 
# Другий параметр – це ступінь. Процедура повертає число, зведене до ступеня. 
# Наприклад, якщо параметри дорівнюють 2 і 3, тоді повернеться 2 у третьому ступені, тобто 8.

DELIMITER //
CREATE PROCEDURE GetNumberToPower(num1 INT, num2 INT)
BEGIN
    DECLARE result BIGINT;
    SET result = POW(num1, num2);
    SELECT result AS PowerResult;
END //
DELIMITER ;

CALL GetNumberToPower(3, 4);



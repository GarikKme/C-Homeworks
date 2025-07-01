-- Homework 9 SQL

SELECT *
FROM StudentGrades; -- відображення всієї інформації

SELECT full_name
FROM StudentGrades; -- відображення ПІБ студентів

SELECT average_point
FROM StudentGrades; -- відображення усіх середні оцінки

SELECT full_name FROM StudentGrades WHERE average_point > 8.0; -- Показати ПІБ усіх студентів з мінімальною оцінкою, більшою, ніж зазначена.

SELECT DISTINCT country FROM StudentGrades; -- Показати країни студентів. Назви країн мають бути унікальними.

SELECT DISTINCT city FROM StudentGrades; -- Показати міста студентів. Назви міст мають бути унікальними

SELECT DISTINCT group_name FROM StudentGrades; -- Показати міста студентів. Назви міст мають бути унікальними

SELECT DISTINCT  weakest_subject AS subject FROM StudentGrades
UNION
SELECT DISTINCT  strongest_subject AS subject FROM StudentGrades; -- Назви предметів мають бути унікальними.


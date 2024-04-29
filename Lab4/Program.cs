using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<Student> students = new List<Student>();

        try
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Введіть дані для студента {i + 1}:");
                Student student = new Student();

                bool validInput = false;
                while (!validInput)
                {
                    Console.Write("Прізвище (без пробілів і цифр): ");
                    string lastName = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(lastName) || lastName.Any(char.IsDigit) || lastName.Contains(" "))
                    {
                        Console.WriteLine("Прізвище повинно бути текстовим значенням і не містити пробілів чи цифр.");
                    }
                    else
                    {
                        student.LastName = lastName;
                        validInput = true;
                    }
                }

                validInput = false;
                while (!validInput)
                {
                    Console.Write("Ім'я (без пробілів і цифр): ");
                    string firstName = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(firstName) || firstName.Any(char.IsDigit) || firstName.Contains(" "))
                    {
                        Console.WriteLine("Ім'я повинно бути текстовим значенням і не містити пробілів чи цифр.");
                    }
                    else
                    {
                        student.FirstName = firstName;
                        validInput = true;
                    }
                }

                validInput = false;
                while (!validInput)
                {
                    Console.Write("Номер залікової книжки (тільки цифри): ");
                    string studentId = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(studentId) && studentId.All(char.IsDigit))
                    {
                        student.StudentId = studentId;
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Номер залікової книжки повинен містити тільки цифри.");
                    }
                }

                validInput = false;
                while (!validInput)
                {
                    Console.WriteLine("Оцінки за 5 предметів (введіть через пробіл): ");
                    string[] gradesInput = Console.ReadLine()?.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (gradesInput == null || gradesInput.Length != 5 || !gradesInput.All(grade => int.TryParse(grade, out _)))
                    {
                        Console.WriteLine("Некоректний ввід оцінок. Повторіть спробу.");
                    }
                    else
                    {
                        student.Grades = gradesInput.Select(int.Parse).ToArray();

                        if (student.Grades.Any(grade => grade < 0 || grade > 100))
                        {
                            Console.WriteLine("Оцінки повинні бути в діапазоні від 0 до 100. Повторіть спробу.");
                        }
                        else
                        {
                            validInput = true;
                        }
                    }
                }

                students.Add(student);
            }

            var sortedStudents = students.OrderBy(s => s.Grades.Average()).ToList();

            Console.WriteLine("Прізвище\t\tІм'я\tНомер залікової книжки\tСередній бал");
            foreach (var student in sortedStudents)
            {
                Console.WriteLine($"{student.LastName,-15}\t{student.FirstName,-10}\t{student.StudentId,-20}\t{student.Grades.Average(),-10:F2}");
            }

            double unsatisfactoryCount = students.Count(s => s.Grades.Any(grade => grade < 60));
            double unsatisfactoryPercentage = (unsatisfactoryCount / students.Count) * 100;
            Console.WriteLine($"\nВідсоток студентів з незадовільними оцінками: {unsatisfactoryPercentage:F2}%");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }
}

class Student
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string StudentId { get; set; }
    public int[] Grades { get; set; }
}

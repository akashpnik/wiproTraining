using System;

class Student
{
    public int Id;
    public string Name;
    public int Age;
}

class Program
{
    static void Main(string[] args)
    {
        Student student = new Student(); // Only one student at a time
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n--- Student Menu ---");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. View Student");
            Console.WriteLine("3. Update Student");
            Console.WriteLine("4. Delete Student");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter ID: ");
                    student.Id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Name: ");
                    student.Name = Console.ReadLine();
                    Console.Write("Enter Age: ");
                    student.Age = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Student added.");
                    break;

                case 2:
                    Console.WriteLine("Student Details:");
                    Console.WriteLine("ID: " + student.Id);
                    Console.WriteLine("Name: " + student.Name);
                    Console.WriteLine("Age: " + student.Age);
                    break;

                case 3:
                    Console.WriteLine("Update Student:");
                    Console.Write("Enter new Name: ");
                    student.Name = Console.ReadLine();
                    Console.Write("Enter new Age: ");
                    student.Age = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Student updated.");
                    break;

                case 4:
                    student = new Student(); // Reset student
                    Console.WriteLine("Student deleted.");
                    break;

                case 5:
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }
}

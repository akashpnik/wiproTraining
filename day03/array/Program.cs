using System;

namespace JaggedArrayExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] studentNames = new string[5];
            string[][] subjects = new string[5][]; // Jagged array

            // Input student names and subjects
            for (int i = 0; i < 5; i++)
            {
                Console.Write($"Enter name of student {i + 1}: ");
                studentNames[i] = Console.ReadLine();

                Console.Write($"How many subjects for {studentNames[i]}? ");
                int subjectCount = Convert.ToInt32(Console.ReadLine());

                subjects[i] = new string[subjectCount];

                for (int j = 0; j < subjectCount; j++)
                {
                    Console.Write($"Enter subject {j + 1} for {studentNames[i]}: ");
                    subjects[i][j] = Console.ReadLine();
                }
            }

            // Display student names and their subjects
            Console.WriteLine("\n--- Student Subjects List ---");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"\nStudent {i + 1}: {studentNames[i]}");
                Console.WriteLine("Subjects:");
                for (int j = 0; j < subjects[i].Length; j++)
                {
                    Console.WriteLine($"- {subjects[i][j]}");
                }
            }

            Console.ReadKey();
        }
    }
}

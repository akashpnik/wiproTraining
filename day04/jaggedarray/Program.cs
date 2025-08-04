using System;

class JaggedArrayExample
{
    static void Main()
    {
        string[] studentNames = new string[5];
        string[][] subjects = new string[5][];

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

            Console.WriteLine();
        }

        // Displaying the data
        Console.WriteLine("\n--- Student and Subjects ---");
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"Student: {studentNames[i]}");
            Console.WriteLine("Subjects:");
            foreach (string subject in subjects[i])
            {
                Console.WriteLine($"- {subject}");
            }
            Console.WriteLine();
        }
    }
}

//
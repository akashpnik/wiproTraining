class Employee
{
    public void Main()
    {
        Console.WriteLine("Enter the first no.:  );
        int a = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter the second no: ");
        int b= Convert.ToInt32(Console.ReadLine());
        

    }
    public void Display()
    {
        Console.WriteLine("Entry Point of Program");
        Employee e = new Employee();
        e.Input();
        e.Display();
    }
}
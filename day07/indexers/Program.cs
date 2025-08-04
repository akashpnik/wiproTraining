 // Indexers -- It allows instances of a class to be indexed just like an array

 class Employee
 {
     public int id { get; set; }
     public string name { get; set; }
     Employee(int id, string name)
     {
         this.id = id;
         this.name = name;
     }


     // modifier type  this[int index or string name]
     // Indexer for a Employee class to work as an array while fetching the values 
     public Object this[int index]
     {
         get
         {

             if (index == 0)
                 return id;

             else if (index == 1)
                 return name;
             else
                 return null;
         }
         set
         {

             if (index == 0)
                 id = Convert.ToInt32(value);

             else if (index == 1)
                 name = value.ToString();

         }




     }

     class MainProgram
     {
         public static void Main()
         {
             Employee e = new Employee(101, "Niti");
             Console.WriteLine(e[1]);


               // Console.WriteLine("Enter your age");
                if (int.TryParse("12" , out int value))
                    Console.WriteLine("Value is " + value);

                //At the time of taking input in front end itself you can cofirm the value 
             //   Console.WriteLine("Even after converting the value is in string format"); 

         }
     }

 }







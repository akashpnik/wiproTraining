// See https://aka.ms/new-console-template for more information
Dictionary<int, string> data = new Dictionary<int, string>();
       data.Add(101, "Niti");
       data.Add(102, "Preeti");

       Console.WriteLine(data[101]);

       foreach (KeyValuePair<int, string> kv in data)
       {
           Console.WriteLine(kv.Key + " " + kv.Value);
       }
s1.Add("Subjects", new Dictionary<string, int>
        {
            { "Math", 85 },
            { "Science", 90 }
        });
can i do like this for it ??
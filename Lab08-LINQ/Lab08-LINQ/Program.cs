using System;
using Lab08_LINQ.classes;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.InteropServices.ComTypes;
using System.IO;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace Lab08_LINQ
{
    delegate void MyDelegate();
    delegate bool BoolDelegate();

    public class Program
    {
        public void Data() { Data data = new Data(); }
        static void Main(string[] args)
        {
            string path = "../../../../jsonData.json";
            Console.WriteLine("THIS METHOD DOES ALL 3 STEPS IN 1 METHOD");
            ReadsJson(path);
            Console.WriteLine("single query");
            SingleQuery(path);
            Console.WriteLine("METHOD");
            MethodWay(path);
            

        Console.WriteLine("Hello World!");
        
        }
        public static void ReadsJson(string path)
        {

            var Data = "";
            using (StreamReader sr = File.OpenText(path)) 
            {
                Data = sr.ReadToEnd();
            }

            RootObject root = JsonConvert.DeserializeObject<RootObject>(Data);

            var query = from blah in root.features
                        select blah.properties.neighborhood;
            foreach (string value in query)
            {
                Console.WriteLine("===========RAW================");
                Console.WriteLine(value);
                

            }

            var query2 = query.Distinct();
            foreach (var value in query2)
            {
                Console.WriteLine("==========NO DUPES===========");
                Console.WriteLine(value);
            }

            var query3 = from q in query2
                         where (q != "")
                         select q;
            foreach (var val in query3)
            {
                Console.WriteLine("==========NO BLANKS===========");
                Console.WriteLine(val);
            }
            
            foreach (var value in query3)
            {
                Console.WriteLine("===========NO DUPES AND BLANKS==========");
                Console.WriteLine(value);
            }
           

        }
        public static void SingleQuery(string path)
        {
            var Data = "";
            using (StreamReader sr = File.OpenText(path))
            {
                Data = sr.ReadToEnd();
            }

            RootObject root = JsonConvert.DeserializeObject<RootObject>(Data);
            
            var query = from blah in root.features
                        where (string)blah.properties.neighborhood != ""
                        group blah by blah.properties.neighborhood
                        into neighborhoods
                        select neighborhoods;

     

                        
                        
        }
        public static void MethodWay(string path)
        {
            var Data = "";
            using (StreamReader sr = File.OpenText(path))
            {
                Data = sr.ReadToEnd();
            }

            RootObject root = JsonConvert.DeserializeObject<RootObject>(Data);
            var methodWay = root.features.Where(n => n.properties.neighborhood != "");
                                       // .Distinct();                

            foreach (var value in methodWay)
            {
                Console.WriteLine("=================");
                Console.WriteLine(value.properties.neighborhood);
            }

        }

    }
  
}

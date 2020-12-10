using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final
{
    public class Manager
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public string City { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }

        public Manager()
        {

        }
        public Manager(int id, string name, int phone, string city, int age, string gender)
        {
            ID = id;
            Name = name;
            Phone = phone;
            City = city;
            Age = age;
            Gender = gender;
        }

        public Manager(string name, int phone, string city, int age, string gender)
        {
            Name = name;
            Phone = phone;
            City = city;
            Age = age;
            Gender = gender;
        }

        public override string ToString()
        {
            return String.Format("ID: {0} \tName: {1}\tPhone Number: {2}\tCity: {3}\tAge: {4}\t Gender: {5}"
                ,ID,  Name, Phone, City, Age, Gender);
        }



    }
}

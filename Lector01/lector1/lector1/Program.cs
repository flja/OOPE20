using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace lector1
{
    class Program
    {
        static void Main(string[] args)
        {
           
            //Task 1-3
            Person p = new Person();
            p.fornavn = "Frederik";
            p.efternavn = "Jakobsen";
            p.far = new Person();  //poor implementation but is done to prove the point of null check in the assignment
            p.far.fornavn = "Peter";
            p.far.efternavn = "Jakobsen";
            p.mor = new Person(); //poor implementation but is done to prove the point of null check in the assignment
            p.mor.fornavn = "Tove";
            p.mor.efternavn = "Jakobsen";
            PersonPrinter.PrintPerson(p);

            //Another way to show have to init person by constructors
            p = new Person
            {
                fornavn = "Frederik",
                efternavn = "Jakobsen",
                mor = new Person
                {
                    fornavn = "Tove",
                    efternavn = "Jakobsen"
                },
                far = new Person
                {
                    fornavn = "Peter",
                    efternavn = "Jakobsen"
                }
            };

            PersonPrinter.PrintPerson(p);
            Console.ReadKey();//wait to end program untill key is pressed

            //task 4 
            // a)
            string[] files = Directory.GetFiles("c:\\");
            foreach (string s in files)
            {
                FileInfo fileInfo = new FileInfo(s);
                Console.WriteLine($"Name: {fileInfo.Name}, size: {fileInfo.Length / 1000000}MB");
            }

            // b)

            Console.ReadKey();//wait to end program untill key is pressed
        
        }
    }

    class PersonPrinter
    {
        public static void PrintPerson(Person person)
        {
            string s = "Unkown person!";
            if(person != null)
            {
                s = $"{person.personId}. Navn: {person.fornavn} {person.efternavn}, Forældre: ";

                if (person.far != null)
                {
                    s += $"{person.far.personId}. {person.far.fornavn} {person.far.efternavn} & ";
                }

                if (person.mor != null)
                {
                    s += $"{person.mor.personId}. {person.mor.fornavn} {person.mor.efternavn}";
                }
            }

            Console.WriteLine(s);
        }
    }

    class Person
    {
        static int personIdCount;
        string _fornavn;
        string _efternavn;
        int _personId;
        int _alder;
        Person _far;
        Person _mor;

        public Person()
        {
            _personId = personIdCount++; 
        }

        public Person(string fornavn, string efternavn) : this()
        {
            _fornavn = fornavn;
            _efternavn = efternavn;
        }

        public Person(Person far, Person mor) : this() //Task 2
        {
            _far = far;
            _mor = mor;
        }

        public Person(string fornavn, string efternavn, Person far, Person mor) : this()
        {
            _fornavn = fornavn;
            _efternavn = efternavn;
            _far = far;
            _mor = mor;
        }

        public string fornavn
        {
            get
            {
                return _fornavn;
            }
            set
            {
                if(value != null)
                {
                    _fornavn = value;
                }
            }
        }

        public string efternavn
        {
            get
            {
                return _efternavn;
            }
            set
            {
                if (value != null)
                {
                    _efternavn = value;
                }
            }
        }

        public int alder
        {
            get { return _alder; }
            set
            {
                if(value >=0 && value <=130)
                {
                    _alder = value;
                }
            }
        }

        public int personId
        {
            get { return _personId; }
            set
            {
                if (value >= 0 && value <= 130)
                {
                    _personId = value;
                }
            }
        }

        public Person far
        {
            get { return _far; }
            set
            {
                if(value != null)
                {
                    _far = value;
                }
            }
        }

        public Person mor
        {
            get { return _mor; }
            set
            {
                if (value != null)
                {
                    _mor = value;
                }
            }
        }

    }

}

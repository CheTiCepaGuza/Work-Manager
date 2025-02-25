using System.Collections.Generic;
using System;
using System.Linq;
using System.Configuration;
using System.Xml.Linq;

namespace WorkManager

{

    public class Worker
    {

        static Random rnd = new Random();

        public string Name;

        public string Position;

        public double Salary;

        public int ProductionRate;

        public Department Department = null;


        public Worker(string Name, string Position, double Salary, Department dept)
        {

            this.Name = Name;
            this.Position = Position;
            this.Salary = Salary;
            Department = dept;
            this.ProductionRate = rnd.Next(1, 6);

        }

        public override string ToString()
        {

            return $"|{Name}| Position: {Position}, Salary {Salary}";
        }

    }

    public class Department
    {

        public string DName;

        public int ProductionPerMonth;

        public List<Worker> Workers = new List<Worker>();

        public Department(string DName)
        {

            this.DName = DName;

        }

        public Department(string DName, List<Worker> Workers)
        {

            this.DName = DName;
            this.Workers = Workers;

        }

        public void ProductionUpdate()
        {


            foreach (Worker worker in Workers)
            {

                ProductionPerMonth += worker.ProductionRate;

            }

            ProductionPerMonth *= 100;

        }

        public void AddWorker(Department dept)
        {

            Console.WriteLine("!--Add--Worker--!");
            Console.WriteLine();
            Console.Write("Name:");
            string name = Console.ReadLine();

            Console.Write("Position:");
            string position = Console.ReadLine();

            Console.Write("Salary:");
            double salary = double.Parse(Console.ReadLine());

            Worker pesho = new Worker(name, position, salary, dept);

            Workers.Add(pesho);

            ProductionUpdate();

        }
        public void RemoveWorker(string name)
        {

            List<Worker> search = Workers.Where(n => n.Name == name).ToList();
            if (search != null)
            {

                Workers.Remove(search[0]);

            }

            ProductionUpdate();

        }
        public void EditWorker(string name)
        {

            List<Worker> search = Workers.Where(n => n.Name == name).ToList();
            if (search != null)
            {

                Console.Write("1 - name;2 - salary;0 - end: ");

                while (true)
                {

                    int now = int.Parse(Console.ReadLine());
                    if (now == 0)
                    {

                        Console.WriteLine();
                        break;

                    }
                    switch (now)
                    {

                        case 1:

                            Console.Write("New Name: ");

                            search[0].Name = Console.ReadLine();

                            break;

                        case 2:

                            Console.Write("New Salary: ");

                            search[0].Salary = double.Parse(Console.ReadLine());

                            break;

                        default:
                            Console.WriteLine("Wrong Input");

                            break;


                    }

                }
                for (int i = 0; i < Workers.Count; i++)
                {

                    if (Workers[i].Name == name)
                    {


                        Workers[i] = search[0];
                        break;

                    }

                }

            }

        }
        public void DisplayBy(string izbor)
        {

            if (izbor.ToLower() == "all")
            {

                DisplayBy("CEO");
                DisplayBy("Manager");
                DisplayBy("Employee");


            }
            else
            {

                List<Worker> now = Workers.Where(n => n.Position == "CEO").ToList();
                int counter = 1;
                switch (izbor)
                {


                    case "CEO":
                        now = Workers.Where(n => n.Position == "CEO").ToList();

                        foreach (Worker worker in now)
                        {

                            Console.WriteLine($"|{counter}" + worker);
                            counter++;

                        }
                        counter--;
                        Console.WriteLine();
                        Console.WriteLine("Total CEOs: " + counter);
                        Console.WriteLine();

                        break;

                    case "Manager":

                        now = Workers.Where(n => n.Position == "Manager").ToList();

                        foreach (Worker worker in now)
                        {

                            Console.WriteLine($"|{counter}" + worker);
                            counter++;

                        }
                        counter--;
                        Console.WriteLine();
                        Console.WriteLine("Total Managers: " + counter);
                        Console.WriteLine();

                        break;

                    case "Employee":

                        now = Workers.Where(n => n.Position == "Employee").ToList();

                        foreach (Worker worker in now)
                        {

                            Console.WriteLine($"|{counter}" + worker);
                            counter++;

                        }
                        counter--;
                        Console.WriteLine();
                        Console.WriteLine("Total Employeis: " + counter);
                        Console.WriteLine();

                        break;
                }

            }

        }
        public void HigestSalary()
        {

            Console.WriteLine();
            Console.WriteLine($"Higest Salary in {DName}: {Workers.OrderBy(x => x.Salary).FirstOrDefault()}");
            Console.WriteLine();

        }
        public void LowestSalary()
        {

            Console.WriteLine();
            Console.WriteLine($"Lowest Salary in {DName}: {Workers.OrderByDescending(x => x.Salary).FirstOrDefault()}");
            Console.WriteLine();

        }

        public override string ToString()
        {
            return $"[{DName}] - Workers Count: {Workers.Count}| Production per mouth: {ProductionPerMonth}|";
        }
    }

    public class WorkSpace
    {

        public string Name;

        public int Capital;

        public List<Department> Departments = new List<Department>();

        public WorkSpace(string name, int capital)
        {

            Name = name;
            Capital = capital;

        }
        public void AddDepartment()
        {

            Console.WriteLine("!--Add-Department--!");
            Console.Write("Name:");
            string name = Console.ReadLine();

            Departments.Add(new Department(name));

        }
        public void RemoveDepartment(string name)
        {

            bool check = false;
            for (int i = 0; i < Departments.Count; i++)
            {


                if (Departments[i].DName == name)
                {

                    Departments.Remove(Departments[i]);
                    Console.WriteLine($"Department {Departments[i].DName} - sucsessfuly removed");
                    check = true;
                    break;
                }


            }
            if (check == false)
            {

                Console.WriteLine("Department not found");

            }

        }

        public void DisplayDepartments()
        {

            if (Departments != null)
            {

                foreach (Department de in Departments)
                {

                    Console.WriteLine(de);

                }

            }
            else
            {

                Console.WriteLine("No departments for now");

            }

        }

        public void DeptMenu()
        {

            Console.WriteLine("------DEPT-MENU------>");
            Console.WriteLine("[A]Add Worker");
            Console.WriteLine("[R]Remove Worker");
            Console.WriteLine("[E]Edit Worker");
            Console.WriteLine("[D]Display(ALL,CEO,Manager,Employee)");
            Console.WriteLine("[B]Back)");

        }


        public void DeptViewr()
        {
            ConsoleKeyInfo key;
            ConsoleKeyInfo lastClick = new ConsoleKeyInfo();

            while (true)
            {
                Console.Clear();
                DisplayDepartments();
                Console.WriteLine();
                DeptMenu();
                

                Console.Write("---Izberi filial: ");
                string name = Console.ReadLine();
                List<Department> deps = Departments.Where(n => n.DName == name).ToList();
                if (name == "end")
                {

                    Console.Clear();
                    break;

                }
                if (deps.Count > 0)
                {


                    while (true)
                    {

                        Console.WriteLine("Enter Pick: ");
                        Console.WriteLine();
                        key = Console.ReadKey(intercept: true);
                        lastClick = key;
                        if (key.Key == ConsoleKey.B)
                        {

                            Console.Clear();
                            return;

                        }
                        switch (key.Key)
                        {

                            case ConsoleKey.A:
                                deps[0].AddWorker(deps[0]);
                                break;

                            case ConsoleKey.R:
                                Console.Write("Enter name: ");
                                string myName = Console.ReadLine();
                                deps[0].RemoveWorker(myName);
                                break;

                            case ConsoleKey.E:
                                Console.Write("Enter name: ");
                                string myName1 = Console.ReadLine();
                                deps[0].EditWorker(myName1);
                                break;

                            case ConsoleKey.D:
                                Console.WriteLine("[ALL,CEO,Manager,Employee] Pick: ");
                                string pick = Console.ReadLine();
                                Console.Clear();
                                deps[0].DisplayBy(pick);

                                DeptMenu();
                                
                                break;

                        }

                    }

                }
                else
                {


                    Console.WriteLine("Department not found");
                    Console.WriteLine();

                }


            }

        }

        public void MainMenu()
        {


            while (true)
            {
                DisplayDepartments();
                Console.WriteLine();
                Console.WriteLine("-----MENU------");
                Console.WriteLine("[A]Add Department");
                Console.WriteLine("[R]Remove Department");
                Console.WriteLine("[V]Department Viewr");
                Console.WriteLine("Enter Pick: ");

                ConsoleKeyInfo key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.B)
                {

                    break;

                }
                else if (key.Key == ConsoleKey.A || key.Key == ConsoleKey.R || key.Key == ConsoleKey.V)
                {
                    Console.WriteLine();
                    switch (key.Key)
                    {

                        case ConsoleKey.A:
                            Console.Clear();
                            AddDepartment();
                            Console.Clear();

                            break;

                        case ConsoleKey.R:
                            Console.Clear();
                            Console.Write("Enter name(Remove):");
                            string name = Console.ReadLine();
                            RemoveDepartment(name);
                            Console.Clear();
                            DeptViewr();
                            break;

                        case ConsoleKey.V:
                            DeptViewr();
                            break;


                    }
                }
                else
                {
                    Console.Clear ();
                    Console.WriteLine("!!Wrong input!!");
                    Console.WriteLine();
                }
            }

        }



    }

    public class Program
    {

        static void Main(string[] args)
        {


            WorkSpace wrwr = new WorkSpace("fasf", 100000);

            Department deps = new Department("IT");
            Department deps1 = new Department("BG");

            Worker ceo = new Worker("Ivan Petrov", "CEO", 5000, deps);
            Worker manager = new Worker("Maria Ivanova", "Manager", 3000, deps);
            Worker employee = new Worker("Georgi Dimitrov", "Employee", 2000, deps);

            Worker ceo2 = new Worker("Stefan Nikolov", "CEO", 5500, deps1);
            Worker manager2 = new Worker("Elena Petrova", "Manager", 3200, deps1);
            Worker employee2 = new Worker("Nikola Ivanov", "Employee", 2100, deps1);

            deps.Workers.Add(ceo);
            deps.Workers.Add(manager);
            deps.Workers.Add(employee);

            deps1.Workers.Add(manager2);
            deps1.Workers.Add(ceo2);
            deps1.Workers.Add(employee2);

            deps.ProductionUpdate();
            deps1.ProductionUpdate();

            wrwr.Departments.Add(deps);
            wrwr.Departments.Add(deps1);


            wrwr.MainMenu();



        }

    }
}
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

public class Program
{
    IList<Employee> employeeList;
    IList<Salary> salaryList;

    public Program()
    {
        employeeList = new List<Employee>() {
            new Employee(){ EmployeeID = 1, EmployeeFirstName = "Rajiv", EmployeeLastName = "Desai", Age = 49},
            new Employee(){ EmployeeID = 2, EmployeeFirstName = "Karan", EmployeeLastName = "Patel", Age = 32},
            new Employee(){ EmployeeID = 3, EmployeeFirstName = "Sujit", EmployeeLastName = "Dixit", Age = 28},
            new Employee(){ EmployeeID = 4, EmployeeFirstName = "Mahendra", EmployeeLastName = "Suri", Age = 26},
            new Employee(){ EmployeeID = 5, EmployeeFirstName = "Divya", EmployeeLastName = "Das", Age = 20},
            new Employee(){ EmployeeID = 6, EmployeeFirstName = "Ridhi", EmployeeLastName = "Shah", Age = 60},
            new Employee(){ EmployeeID = 7, EmployeeFirstName = "Dimple", EmployeeLastName = "Bhatt", Age = 53}
        };

        salaryList = new List<Salary>() {
            new Salary(){ EmployeeID = 1, Amount = 1000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 1, Amount = 500, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 1, Amount = 100, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 2, Amount = 3000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 2, Amount = 1000, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 3, Amount = 1500, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 4, Amount = 2100, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 5, Amount = 2800, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 5, Amount = 600, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 5, Amount = 500, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 6, Amount = 3000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 6, Amount = 400, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 7, Amount = 4700, Type = SalaryType.Monthly}
        };
    }

    public static void Main()
    {
        Program program = new Program();

        program.Task1();

        program.Task2();

        program.Task3();

        Console.ReadKey();
    }

    public void Task1()
    {
        //Implementation
        //Here I have joined the employeeList and salaryList on employeeID
        var printTotalSalary = from emp in employeeList
                               join sal in salaryList on emp.EmployeeID equals sal.EmployeeID
                               //Then I grouped the resulting data
                               group sal.Amount by new { emp.EmployeeID, emp.EmployeeFirstName, emp.EmployeeLastName } into empsal
                               //And also sorted the sum into ascending order
                               orderby empsal.Sum() ascending
                               select new { EmployeeID = empsal.Key.EmployeeID ,EmployeeName = empsal.Key.EmployeeFirstName +" "+empsal.Key.EmployeeLastName, TotalSalary = empsal.Sum() };
        Console.WriteLine("-----------------------------------------------------------------------------");
        Console.WriteLine("\t\t\t\tTotal Salary");
        Console.WriteLine("-----------------------------------------------------------------------------");
        foreach (var total in printTotalSalary)
        {
            Console.WriteLine("Employee Id : {0}\tEmployee Name : {1}\tTotal Salary : {2} rs",total.EmployeeID ,total.EmployeeName, total.TotalSalary);
        }        
    }
    public void Task2()
    {
        //Implementation
        //To findout the secondOldestEmployee I first tried to sort the employeeList in descending order by age and takeout the 2nd employee from there
        var secondOldestEmployee = employeeList.OrderByDescending(emp => emp.Age).Take(2).LastOrDefault();
        //Then I find his montly salary using the salaryList of that secondOldestEmployee by his employeeID
        var hisMonthlySalary = from sal in salaryList
                               where sal.EmployeeID == secondOldestEmployee.EmployeeID && sal.Type == SalaryType.Monthly
                               select sal.Amount;
        var totalMonthlySalary = hisMonthlySalary.Sum();
        Console.WriteLine("\n\n-----------------------------------------------------------------------------");
        Console.WriteLine("\t\t\t   Second Oldest employee");
        Console.WriteLine("-----------------------------------------------------------------------------");
        Console.WriteLine("Employee Id : {0}\tEmployee Name : {1}\tTotal Salary : {2} rs", secondOldestEmployee.EmployeeID, secondOldestEmployee.EmployeeFirstName+" "+secondOldestEmployee.EmployeeLastName, totalMonthlySalary);
    }
    public void Task3()
    {
        //Implementation
        //For this task I used the same concept of Task 1 but instead of .Sum() method I used .Average() method to get the desired output
        var meanSalary = from emp in employeeList
                         where emp.Age>30
                         join sal in salaryList on emp.EmployeeID equals sal.EmployeeID
                         group sal.Amount by new { emp.EmployeeID, emp.EmployeeFirstName, emp.EmployeeLastName } into empsal
                         orderby empsal.Average()
                         select new { EmployeeID = empsal.Key.EmployeeID, EmployeeName = empsal.Key.EmployeeFirstName + " " + empsal.Key.EmployeeLastName, MeanSalary = empsal.Average() };
        Console.WriteLine("\n\n-----------------------------------------------------------------------------");
        Console.WriteLine("\t\t\t\tMean Salary");
        Console.WriteLine("-----------------------------------------------------------------------------");
        foreach (var mean in meanSalary)
        {
            //Also to get the salary in 2 decimal point I used Math.Round() function
            Console.WriteLine("Employee Id : {0}\tEmployee Name : {1}\tMean Salary : {2} rs",mean.EmployeeID, mean.EmployeeName, Math.Round(mean.MeanSalary, 2));
        }

        //For better visual output in console I have used the same snippet for all the result


    }

    public enum SalaryType
    {
        Monthly,
        Performance,
        Bonus
    }

    public class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public int Age { get; set; }
    }

    public class Salary
    {
        public int EmployeeID { get; set; }
        public int Amount { get; set; }
        public SalaryType Type { get; set; }
    }
}
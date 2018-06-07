using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace SirmaSolutionsTask
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] fileInfo = File.ReadAllLines("EmpInfo.txt");
            Dictionary<string, List<string>> employeesInfo = new Dictionary<string, List<string>>();
            for (int i = 0; i <= fileInfo.Count()-1; i++)
            {
                var currentEmployee = new List<string>();
                var splitInfo = fileInfo[i].Split(',');
                var employeeID = splitInfo[0];
                employeesInfo.Add(employeeID, new List<string>());
                int counter = 0;
                for (int x = 1; x <= 3; x++)
                {
                    currentEmployee.Add(splitInfo[x]);
                    employeesInfo[employeeID].Add(currentEmployee[counter]);
                    counter++;
                }

            }
            TimeSpan maxDaysWorkingTogether = TimeSpan.Zero;
            Dictionary<string, List<string>>.KeyCollection keys = employeesInfo.Keys;
            List<string> employeeIDS = new List<string>();
            foreach(string key in keys)
            {
                employeeIDS.Add(key);
            }
            for (int i = 0; i <= employeesInfo.Count()-1; i++)
            {
                var currentEmployeeID = employeeIDS[i];
                var currentEmployeeEndDate = DateTime.MinValue;
                if (employeesInfo[currentEmployeeID][2] == " NULL ")
                {
                    currentEmployeeEndDate = DateTime.Today;
                }
                else
                {
                    currentEmployeeEndDate = DateTime.Parse(employeesInfo[currentEmployeeID][2]);
                    
                }
                var currentEmployeeStartDate = DateTime.Parse(employeesInfo[currentEmployeeID][1]);

                TimeSpan currentEmployeeWorkedDays = currentEmployeeEndDate
                    .Subtract(currentEmployeeStartDate);
            
                for (int x = i; x <= employeesInfo.Count()-1; x++)
                {
                    var currentCheckedEmployeeID = employeeIDS[x];
                    if (currentEmployeeID == currentCheckedEmployeeID)
                    {
                        var currentEmployeeProjectID = int.Parse(employeesInfo[currentEmployeeID][0]);
                        var checkedEmployeeProjectID = int.Parse(employeesInfo[currentCheckedEmployeeID][0]);

                        if (currentEmployeeProjectID == checkedEmployeeProjectID)
                        {
                            var currentCheckedEmployeeStartDate = DateTime.Parse(employeesInfo[currentCheckedEmployeeID][1]);
                            var currentCheckedEmployeeEndDate = DateTime.MinValue;
                            if (employeesInfo[currentCheckedEmployeeID][2] == " NULL ")
                            {
                                currentCheckedEmployeeEndDate = DateTime.Today;
                            }
                            else
                            {
                                currentCheckedEmployeeEndDate = DateTime.Parse(employeesInfo[currentCheckedEmployeeID][2]);

                            }
                            TimeSpan currentCheckedEmployeeWorkedDays = currentCheckedEmployeeEndDate
                                .Subtract(currentCheckedEmployeeStartDate);

                            var result1ID = string.Empty;
                            var result2ID = string.Empty;
                            DateTime StartDate = DateTime.MinValue;
                            DateTime EndDate = DateTime.MinValue;
                            if (currentEmployeeStartDate >= currentCheckedEmployeeStartDate)
                            {
                                StartDate = currentEmployeeStartDate;
                                if (currentEmployeeEndDate >= currentCheckedEmployeeEndDate)
                                {
                                    EndDate = currentCheckedEmployeeEndDate;
                                }
                                else
                                {
                                    EndDate = currentEmployeeEndDate;
                                }
                            }
                            else
                            {
                                StartDate = currentCheckedEmployeeStartDate;
                                if (currentEmployeeEndDate >= currentCheckedEmployeeEndDate)
                                {
                                    EndDate = currentCheckedEmployeeEndDate;
                                }
                                else
                                {
                                    EndDate = currentEmployeeEndDate;
                                }
                            }
                          
                            TimeSpan timeWorkingTogether = StartDate
                                .Subtract(EndDate);
                            if (maxDaysWorkingTogether < timeWorkingTogether)
                            {
                                result1ID = currentEmployeeID;
                                result2ID = currentCheckedEmployeeID;
                                maxDaysWorkingTogether = timeWorkingTogether;
                            }
                        }

                    }
                }
            }

            Console.WriteLine(String.Format("Max days worked together are: {0} days, {1} hours, {2} minutes, {3} seconds." , maxDaysWorkingTogether.Days, 
                maxDaysWorkingTogether.Hours , maxDaysWorkingTogether.Minutes , maxDaysWorkingTogether.Seconds));
        }
    }
}

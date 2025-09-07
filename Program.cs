namespace EFDay1.Models;
using Microsoft.EntityFrameworkCore;
internal class Program
{
    static void Main(string[] args)
    {
        EFContext context = new EFContext();

        #region IQuerable & IEnumeable Practice

        // الداتا متخزنتش في ميموري 
        //var Name = context.Employees;  
        //foreach (var name in Name)
        //{
        //    Console.WriteLine(name.FirstName + " " + name.Salary);
        //}

        // الداتا اتخزنت في ميموري
        //var Projects = context.Projects.ToList();
        //foreach (var project in Projects)
        //{
        //    Console.WriteLine(project.Dnum + "-" + project.Pname+ "-" + project.Locationcity);
        //}

        ////Where ==> IQuerable-- IN Sql server Buffer
        //var deptName1 = context.Departments.Where(a => a.Ssn == 2);
        //foreach (var department in deptName1)
        //{
        //    Console.WriteLine(department.Locations + "--" + department.Dname);
        //}
        //Console.WriteLine("==============");

        ////Where ==> IEnumeable-- IN Memory --ابطأ واحده فيهم
        //var deptName2 = context.Departments.ToList().Where(a => a.Ssn == 2);
        //foreach (var department in deptName2)
        //{
        //    Console.WriteLine(department.Locations + "--" + department.Dname);
        //}
        //Console.WriteLine("==============");

        //// اسرع واحده فيهم وبتحتفظ بالناتج 
        //var deptName3 = context.Departments.Where(a => a.Ssn == 2).ToList();
        //foreach (var department in deptName3)
        //{
        //    Console.WriteLine(department.Locations + "--" + department.Dname);
        //}


        //var FiratName = context.Employees.Select(a => a.FirstName);
        //foreach (var employee in FiratName)
        //{
        //    Console.WriteLine(employee);
        //}

        #endregion

        #region Include Practice
        //var employee = context.Departments.Include(a => a.Projects).ToList();
        //foreach (var dept in employee)
        //{
        //    Console.WriteLine(dept.Dname + " " +dept.Projects);
        //}

        //var emp = context.Employees.Include(p=>p.EmpProjects).ToList();
        //foreach (var s in emp)
        //{
        //      Console.WriteLine(s.FirstName + " " + s.EmpProjects);
        //}

        #endregion

        #region Tracking
        // find & Single 
        //var depts = context.Departments.Find(4);
        //depts.Dname = "IS";
        //Console.WriteLine(depts.Dname);
        //context.SaveChanges();
        //depts.Dname = "CS";
        //context.SaveChanges();
        //Console.WriteLine(context.Entry(depts).State);

        //var depts = context.Departments.Single(s => s.Ssn == 2);
        //depts.Dname = "Bio";
        //Console.WriteLine((depts.Dname));
        //Console.WriteLine(context.Entry(depts).State);  // Modified

        #endregion

        #region Crud

        // list 
        //var depts = context.Departments.ToList();
        //foreach (var dept in depts)
        //{
        //    Console.WriteLine(dept.Dname + " "+ dept.Dnum);
        //}

        // Select 
        //var depts = context.Departments.Find(3);
        //Console.WriteLine(depts.Dname);


        //add 
        //Department department = new Department() { Dname="IS" , Locations="Menofia"};
        //context.Departments.Add(department);
        //context.SaveChanges();

        //Update 
        //var d = context.Departments.Find(3);
        //d.Dname = "Soft";
        //d.Locations = "Egy";
        //context.SaveChanges();


        //Remove 
        //var d = context.Departments.Find(2);
        //context.Departments.Remove(d);

        #endregion
    }

}

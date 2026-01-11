using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        //Da utišamo C# compiler warning, a da istovremeno NE dozvolimo NULL u bazi.
        //null! = null-forgiving operator(iznimka)
        public string LastName { get; set; } = null!;
        public EmployeePosition Position {  get; set; }
    }

    public enum EmployeePosition
    {
        Receptionist, // Recepcija
        Housekeeper,  // Sobarica
        Manager,      // Menađer hotela
        Security,     // Zaštitari
        Chef,         // Kuvar
        Waiter        // Konobar

    }
}

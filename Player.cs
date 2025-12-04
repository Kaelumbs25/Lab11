using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab11
{
    public enum Position { Goalkeeper, Defender, Midfielder, Forward}

    public class Player : IComparable
    {
        //Properties
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Position PreferredPosition { get; set; }

        public DateTime DateOfBirth { get; set; }

        private int age;

        public int Age
        {
            get { 
                age = DateTime.Now.Year - DateOfBirth.Year;
                if(DateOfBirth.DayOfYear >= DateTime.Now.DayOfYear)
                {
                    age--;
                }
                return age; 
            }

        }

        //Constructors

        //Methods
        public override string ToString()
        {
            return $"{FirstName} {LastName}, ({Age}), {PreferredPosition.ToString().ToUpper()}";
        }

        public int CompareTo(object obj)
        {
            Player that = obj as Player;

            if (this.PreferredPosition > that.PreferredPosition )
                return 1;
            else if (this.PreferredPosition < that.PreferredPosition) 
                return -1;
            else
                return this.FirstName.CompareTo(that.FirstName);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExercise
{
    class Date
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public Date(string date)
        {
            string[] strArr = date.Split('-');
            this.Year = Convert.ToInt32(strArr[0]);
            this.Month = Convert.ToInt32(strArr[1]);
            this.Day = Convert.ToInt32(strArr[2]);
        }

        public Date(int year,int month,int day)
        {
            this.Year = year;
            this.Month = month;
            this.Day = day;
        }

        override public string ToString()
        {
            string day,month;
            string year = this.Year.ToString();
            if (this.Day < 10)
                day = "0" + this.Day.ToString();
            else
                day = this.Day.ToString();
            if (this.Month < 10)
                month = "0" + this.Month.ToString();
            else
                month = this.Month.ToString();

            return year + "-" + month + "-" + day;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace demo_jicheng
{

    static class Program
    {

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            Type t = typeof(Car);
            Type tb = t.BaseType;
            Type tTop = tb.BaseType;

            Car car = new Car();
            // is a
            Console.WriteLine(car is Vehicle);


            Console.WriteLine(tb.FullName);
        }
    }

    class Vehicle : Object
    {

    }

    class Car: Vehicle
    {

    }
}

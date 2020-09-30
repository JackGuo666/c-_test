using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MyLivb;

namespace demo_jicheng
{

    static class Program
    {

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static string Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            /*
            Type t = typeof(Car);
            Type tb = t.BaseType;
            Type tTop = tb.BaseType;

            Car car = new Car();
            car.ShowOwner();
            // is a
            Console.WriteLine(car is Vehicle);*/

            Vehicle vehi = new Vehicle();
            int speed = vehi.Speed;
            int test = vehi.test;
            //Console.WriteLine(vehi.Owner);

            Car car = new Car();
            car.Accelerate();
            car.Accelerate();
        }
    }

    //class Vehicle : Object
    //{
    //    public Vehicle()
    //    {
    //        this.Owner = "N/A";
    //    }

    //    public string Owner { get; set; }
    //}

    //class Car: Vehicle
    //{
    //    public Car()
    //    {
    //        this.Owner = "Car Owner";
    //    }

    //    public void ShowOwner()
    //    {
    //        Console.WriteLine(base.Owner);
    //    }
    //}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLivb
{
    public class Vehicle
    {
        public string Owner { get; set; }
        private int _rpm;
        public void Accelerate()
        {
            _rpm += 1000;
        }

        public int test { get; set; }

        public int Speed 
        {
            get { return _rpm / 100; }
        }

        public Vehicle()
        {
            Owner = "test";
        }
    }
    
    public class Car : Vehicle
    {
 
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestControlTests
{
    public class Person
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String City { get; set; }

        public static BindingSource getExampleBinding1()
        {
            var bs = new BindingSource();
            bs.Add(new Person()
                {
                    FirstName = "F1",
                    LastName = "L1",
                    City = "C1"
                });
            bs.Add(new Person()
            {
                FirstName = "F2",
                LastName = "L2",
                City = "C2"
            });
            bs.Add(new Person()
            {
                FirstName = "F3",
                LastName = "L3",
                City = "C3"
            });
            bs.Add(new Person()
            {
                FirstName = "F4",
                LastName = "L4",
                City = "C4"
            });

            return bs;
        }
        
    }
}

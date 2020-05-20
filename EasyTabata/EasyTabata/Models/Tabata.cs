using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTabata.Models
{
    public class Tabata
    {
        public String Title { get; set; }

        public String Duration { 
            get {
                var rdm = new Random();
                return "00:" + rdm.Next(10, 59) + ":" + rdm.Next(0, 59);
            } 
        }
    }
}

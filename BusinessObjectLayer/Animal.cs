using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessObjectLayer
{
    public class Animal
    {
        public int Id { get; set; }
        public int AnimalTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime BirthDate { get; set; }

        public Animal() { }

    }
}
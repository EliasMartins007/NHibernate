using System;

namespace NHibernateOracleApp.Entities
{
    public class Product
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal Price { get; set; }
        // Add a default constructor for NHibernate
        public Product() { }
    }
}

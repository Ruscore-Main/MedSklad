﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MedStorage
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DataBaseModelContainer : DbContext
    {
        public DataBaseModelContainer()
            : base("name=DataBaseModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Product> ProductSet { get; set; }
        public virtual DbSet<Storage> StorageSet { get; set; }
        public virtual DbSet<Employee> EmployeeSet { get; set; }
        public virtual DbSet<Customer> CustomerSet { get; set; }
        public virtual DbSet<Nacladnaya> NacladnayaSet { get; set; }
        public virtual DbSet<NacladnayaItem> NacladnayaItemSet { get; set; }
    }
}
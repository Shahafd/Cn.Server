using CN.Common.Configs;

using CN.Common.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.DAL.Databases
{
    public class CnContext : DbContext
    {
        public CnContext() : base($"name={MainConfigs.ConnectionString}")
        {
            Database.SetInitializer(new CnContextSeedInitializer());
            Database.Initialize(true);
        }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<UnActiveClient> UnActiveClients { get; set; }
        public virtual DbSet<ClientType> ClientTypes { get; set; }
        public virtual DbSet<Call> Calls { get; set; }
        public virtual DbSet<Line> Lines { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<PackageDetails> PackageDetails { get; set; }
        public virtual DbSet<SelectedNumbers> SelectedNumbers { get; set; }
        public virtual DbSet<SMS> SMS { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<User> Users { get; set; }

    }
}

namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Persistence.models;

    internal sealed class Configuration : DbMigrationsConfiguration<Persistence.datacontext.DBContextBase>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Persistence.datacontext.DBContextBase context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Set<Meeting>().AddOrUpdate(new Persistence.models.Meeting[] {
                new Persistence.models.Meeting()
                        {
                            Id = 0,
                            VenueName = "FLEMINGTON",
                            RGB = 'R',
                            NumberOfRace = 9,
                            Date = new DateTime(2003,03,15),
                            TabcorpReqCode = "MR",
                            TabcorpDispCode = "MEL R",
                            TabLtdReqCode = "MR",
                            TabLtdReqDispCode = "MR",
                            UniTABReqCode = "MR",
                            UniTABReqDispCode = "MR",
                            RaceStarts = new List<RaceStartItem>()
                            {
                                new RaceStartItem() { DateTime = new DateTime(2003,03,15,12,25,00)},
                                new RaceStartItem() { DateTime = new DateTime(2003,03,15,13,05,00)},
                                new RaceStartItem() { DateTime = new DateTime(2003,03,15,13,45,00)}
                            },
                            Coverages = new List<CoverageItem>()
                            {
                                new CoverageItem() { String = "VNQ"},
                                new CoverageItem() { String = "VNU"},
                                new CoverageItem() { String = "VNU"}
                            }
                        },
                    new Persistence.models.Meeting()
                        {
                            Id = 1,
                            VenueName = "GOLD COAST",
                            RGB = 'R',
                            NumberOfRace = 8,
                            Date = new DateTime(2003,03,15),
                            TabcorpReqCode = "3R",
                            TabcorpDispCode = "P2 R",
                            TabLtdReqCode = "CR",
                            TabLtdReqDispCode = "CR",
                            UniTABReqCode = "QR",
                            UniTABReqDispCode = "QR",
                            RaceStarts = new List<RaceStartItem>()
                            {
                                new RaceStartItem() { DateTime = new DateTime(2003,03,15,13,25,00)},
                                new RaceStartItem() { DateTime = new DateTime(2003,03,15,14,05,00)},
                                new RaceStartItem() { DateTime = new DateTime(2003,03,15,14,45,00)}
                            },
                            Coverages = new List<CoverageItem>()
                            {
                                new CoverageItem() { String = "VNQ"},
                                new CoverageItem() { String = "VNU"},
                                new CoverageItem() { String = "VNU"}
                            }
                        }
            });
        }
    }
}

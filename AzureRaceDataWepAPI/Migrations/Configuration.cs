namespace AzureRaceDataWebAPI.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using AzureRaceDataWebAPI.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<AzureRaceDataWebAPI.Context.RaceDataDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AzureRaceDataWebAPI.Context.RaceDataDBContext context)
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

            context.Set<Meeting>().AddOrUpdate(
                new Meeting[] 
                {
                    new Meeting()
                            {
                                Id = 0,
                                VenueName = "FLEMINGTON",
                                RGB = 'R',
                                NumberOfRaces = 9,
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
                        new Meeting()
                            {
                                Id = 1,
                                VenueName = "GOLD COAST",
                                RGB = 'R',
                                NumberOfRaces = 8,
                                Date = new DateTime(2003,03,15),
                                TabcorpReqCode = "3R",
                                TabcorpDispCode = "P2 R",
                                TabLtdReqCode = "CR",
                                TabLtdReqDispCode = "CR",
                                UniTABReqCode = "QR",
                                UniTABReqDispCode = "QR",
                                RaceStarts = new List<RaceStartItem>()
                                {
                                    new RaceStartItem() { DateTime = new DateTime(2003,03,15,15,25,00)},
                                    new RaceStartItem() { DateTime = new DateTime(2003,03,15,16,05,00)},
                                    new RaceStartItem() { DateTime = new DateTime(2003,03,15,16,45,00)}
                                },
                                Coverages = new List<CoverageItem>()
                                {
                                    new CoverageItem() { String = "VNQ"},
                                    new CoverageItem() { String = "VNU"},
                                    new CoverageItem() { String = "VNU"}
                                }
                            },
                        new Meeting()
                            {
                                Id = 2,
                                VenueName = "Sandown",
                                RGB = 'R',
                                NumberOfRaces = 12,
                                Date = new DateTime(2003,03,15),
                                TabcorpReqCode = "3R",
                                TabcorpDispCode = "P2 R",
                                TabLtdReqCode = "CR",
                                TabLtdReqDispCode = "CR",
                                UniTABReqCode = "QR",
                                UniTABReqDispCode = "QR",
                                RaceStarts = new List<RaceStartItem>()
                                {
                                    new RaceStartItem() { DateTime = new DateTime(2003,03,15,10,25,00)},
                                    new RaceStartItem() { DateTime = new DateTime(2003,03,15,11,05,00)},
                                    new RaceStartItem() { DateTime = new DateTime(2003,03,15,12,45,00)},
                                    new RaceStartItem() { DateTime = new DateTime(2003,03,15,13,05,00)},
                                    new RaceStartItem() { DateTime = new DateTime(2003,03,15,14,45,00)},
                                    new RaceStartItem() { DateTime = new DateTime(2003,03,15,15,05,00)},
                                    new RaceStartItem() { DateTime = new DateTime(2003,03,15,16,45,00)}
                                },
                                Coverages = new List<CoverageItem>()
                                {
                                    new CoverageItem() { String = "VNQ"},
                                    new CoverageItem() { String = "VNU"},
                                    new CoverageItem() { String = "VNU"}
                                }
                            },
                        new Meeting()
                            {
                                Id = 3,
                                VenueName = "Eagle Farm",
                                RGB = 'R',
                                NumberOfRaces = 10,
                                Date = new DateTime(2003,03,15),
                                TabcorpReqCode = "3R",
                                TabcorpDispCode = "S2 R",
                                TabLtdReqCode = "SR",
                                TabLtdReqDispCode = "SR",
                                UniTABReqCode = "SR",
                                UniTABReqDispCode = "SR",
                                RaceStarts = new List<RaceStartItem>()
                                {
                                    new RaceStartItem() { DateTime = new DateTime(2003,03,15,10,25,00)},
                                    new RaceStartItem() { DateTime = new DateTime(2003,03,15,11,05,00)},
                                    new RaceStartItem() { DateTime = new DateTime(2003,03,15,12,45,00)}
                                },
                                Coverages = new List<CoverageItem>()
                                {
                                    new CoverageItem() { String = "VPQ"},
                                    new CoverageItem() { String = "VPU"},
                                    new CoverageItem() { String = "VPU"}
                                }
                            },
                        new Meeting()
                            {
                                Id = 4,
                                VenueName = "Ascot",
                                RGB = 'R',
                                NumberOfRaces = 11,
                                Date = new DateTime(2003,03,15),
                                TabcorpReqCode = "3R",
                                TabcorpDispCode = "M2 R",
                                TabLtdReqCode = "MR",
                                TabLtdReqDispCode = "MR",
                                UniTABReqCode = "MR",
                                UniTABReqDispCode = "MR",
                                RaceStarts = new List<RaceStartItem>()
                                {
                                    new RaceStartItem() { DateTime = new DateTime(2003,03,15,10,25,00)},
                                    new RaceStartItem() { DateTime = new DateTime(2003,03,15,11,05,00)},
                                    new RaceStartItem() { DateTime = new DateTime(2003,03,15,12,45,00)},
                                    new RaceStartItem() { DateTime = new DateTime(2003,03,15,13,25,00)},
                                    new RaceStartItem() { DateTime = new DateTime(2003,03,15,14,05,00)},
                                    new RaceStartItem() { DateTime = new DateTime(2003,03,15,15,45,00)}
                                },
                                Coverages = new List<CoverageItem>()
                                {
                                    new CoverageItem() { String = "VSQ"},
                                    new CoverageItem() { String = "VSU"},
                                    new CoverageItem() { String = "VSU"}
                                }
                            }
                    }
                );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence;
using Persistence.repositories;
using Persistence.models;

namespace AzureRaceDataWebAPI.Tests.models
{
    public class RaceDataInMemDBRepository : IRepository<Meeting>
    {
        private bool disposed = false;
        private List<Persistence.models.Meeting> meetings { get; set; }

        public RaceDataInMemDBRepository()
        {
            meetings = new List<Persistence.models.Meeting>()
            {
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
                            RaceStarts = new List<DateTimeRecord>()
                            {
                                new DateTimeRecord() { DateTime = new DateTime(2003,03,15,12,25,00)},
                                new DateTimeRecord() { DateTime = new DateTime(2003,03,15,13,05,00)},
                                new DateTimeRecord() { DateTime = new DateTime(2003,03,15,13,45,00)}
                            },
                            Coverages = new List<StringRecord>()
                            {
                                new StringRecord() { String = "VNQ"},
                                new StringRecord() { String = "VNU"},
                                new StringRecord() { String = "VNU"}
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
                            RaceStarts = new List<DateTimeRecord>()
                            {
                                new DateTimeRecord() { DateTime = new DateTime(2003,03,15,15,25,00)},
                                new DateTimeRecord() { DateTime = new DateTime(2003,03,15,16,05,00)},
                                new DateTimeRecord() { DateTime = new DateTime(2003,03,15,16,45,00)}
                            },
                            Coverages = new List<StringRecord>()
                            {
                                new StringRecord() { String = "VNQ"},
                                new StringRecord() { String = "VNU"},
                                new StringRecord() { String = "VNU"}
                            }
                        }
                };
        }

        public IQueryable<Persistence.models.Meeting> All()
        {
            return meetings.AsQueryable<Persistence.models.Meeting>();
        }

        public IQueryable<Persistence.models.Meeting> Find(int id)
        {
            return meetings.Where(m => m.Id == id).AsQueryable();
        }

        public async Task<Persistence.models.Meeting> FindAsync(int id)
        {
            return meetings.Find(c => c.Id == id);
        }

        public async Task CreateAsync(Persistence.models.Meeting meetingToAdd)
        {
            if (meetings.Any<Persistence.models.Meeting>(m => m.Id == meetingToAdd.Id)!=null)
            {
                meetings.Add(meetingToAdd);
            }
        }

        public async Task UpdateAsync(Persistence.models.Meeting meetingToSave)
        {
            meetings.Remove(meetingToSave);
            meetings.Add(meetingToSave);
        }

        public async Task DeleteAsync(int id)
        {
            Persistence.models.Meeting m = meetings.Find(c => c.Id == id);
            meetings.Remove(m);
        }

        public bool Exists(int id)
        {
            return meetings.Count(e => e.Id == id) > 0;
        }

        public Persistence.models.Meeting newMeeting()
        {
            return new Persistence.models.Meeting()
            {
                Id = 2,
                VenueName = "Doomben",
                RGB = 'R',
                NumberOfRace = 8,
                Date = new DateTime(2003, 03, 15),
                TabcorpReqCode = "3R",
                TabcorpDispCode = "S2 R",
                TabLtdReqCode = "SR",
                TabLtdReqDispCode = "SR",
                UniTABReqCode = "SR",
                UniTABReqDispCode = "SR",
                RaceStarts = new List<DateTimeRecord>()
                            {
                                new DateTimeRecord() { DateTime = new DateTime(2003,03,15,15,25,00)},
                                new DateTimeRecord() { DateTime = new DateTime(2003,03,15,16,05,00)},
                                new DateTimeRecord() { DateTime = new DateTime(2003,03,15,16,45,00)}
                            },
                Coverages = new List<StringRecord>()
                            {
                                new StringRecord() { String = "VNQ"},
                                new StringRecord() { String = "VNU"},
                                new StringRecord() { String = "VNU"}
                            }
            };
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                //empty

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}

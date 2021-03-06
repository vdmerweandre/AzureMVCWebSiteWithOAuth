﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureRaceDataWebAPI.Models;

namespace AzureRaceDataWebAPI.Tests
{
    public class MockMeetings
    {
        private List<Meeting> meetings;

        public MockMeetings()
        {
            CreateMeetings();
        }

        public List<Meeting> Meetings
        {
            get
            {
                return meetings;
            }
        }

        private void CreateMeetings()
        {
            meetings = new List<Meeting>()
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
                        }
                };
        }
    }
}

﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using FootballPredictor.Models.Clubs;
using Ninject;
using FootballPredictor.Models.Connections;
using FootballPredictor.App_Start;
using FootballPredictor.Models.Predictions;

namespace FootballPredictor.Models.Fixtures
{
    public class Fixture : IFixture
    {
        public int Id { get; protected set; }
        public IClub HomeClub { get; protected set; }
        public IClub AwayClub { get; protected set; }
        public DateTime Date { get; protected set; }
        public FixtureScore Score { get; protected set; }
        /// <summary>
        /// Ended and has a full-time result when it is then marked as completed. Allows for in-play score updates
        /// </summary>
        public bool Completed { get; protected set; }
        /// <summary>
        /// Open for predictions if it has not started yet
        /// </summary>
        public bool OpenForPredictions
        {
            get
            {
                if (Date > new Utilities.Utility().UKDateTime)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public Fixture(int id)
        {
            Id = id;
        }
        public Fixture(int id, bool completed, IClub homeClub, IClub awayClub, DateTime date, FixtureScore score)
        {
            Id = id;
            Completed = completed;
            HomeClub = homeClub;
            AwayClub = awayClub;
            Date = date;
            Score = score;
        }
        public Fixture(int id, bool completed)
        {
            Id = id;
            Completed = completed;
        }
        public Fixture(int id, bool completed, DateTime date)
        {
            Id = id;
            Completed = completed;
            Date = date;
        }

    }
}
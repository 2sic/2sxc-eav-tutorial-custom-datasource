﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ToSic.Eav.Data;
using ToSic.Eav.DataSources;
using ToSic.Eav.DataSources.Queries;

namespace ToSic.Tutorial.DataSource.Basic
{
    // additional info so the visual query can provide the correct buttons and infos
    [VisualQuery(
        NiceName = "Tutorial DateTime List",
        Icon = "date_range",
        GlobalName = "10ebb0af-4b4e-44cb-81e3-68c3b0bb388d"   // random & unique Guid
    )]
    public class DateTimeDataSourceBasicList: ExternalData
    {
        public const string DateFieldName = "Date";
        public const string IdField = "Id";
        public const int ItemsToGenerate = 27;

        /// <summary>
        /// Constructor to tell the system what out-streams we have
        /// </summary>
        public DateTimeDataSourceBasicList()
        {
            Provide(GetList); // default out, if accessed, will deliver GetList
        }

        /// <summary>
        /// Get-List method, which will load/build the items once requested 
        /// Note that the setup is lazy-loading,
        /// ...so this code will not execute unless it's really used
        /// </summary>
        /// <returns></returns>
        private ImmutableArray<IEntity> GetList()
        {
            var randomNumbers = new List<IEntity>();

            for (var i = 0; i < ItemsToGenerate; i++)
            {
                var values = new Dictionary<string, object>
                {
                    {IdField, i},
                    {DateFieldName, RandomDay()}
                };
                var ent = DataBuilder.Entity(values, id: i, titleField: DateFieldName);
                randomNumbers.Add(ent);
            }

            return randomNumbers.ToImmutableArray();
        }

        // helper to randomly generate dates
        private readonly Random _randomizer = new Random();
        private readonly DateTime _start = new DateTime(1995, 1, 1);

        private DateTime RandomDay()
        {
            var range = (DateTime.Today - _start).Days;
            return _start.AddDays(_randomizer.Next(range));
        }
        
    }
}
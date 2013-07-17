using System;
using System.Collections.Generic;
using System.Configuration;
using Norm;
using Norm.Collections;

namespace Mood.Modules
{
    public class Mood
    {
        public static void Save(string status)
        {
            using (var db = Mongo.Create(Server))
            {
                var moodCollection = db.GetCollection<MoodDto>();
                var shortDate = CreateDateString();
                var result = GetTodays(status, moodCollection, shortDate);

                Upsert(status, result, shortDate, moodCollection);
            }
        }

        public static IEnumerable<MoodDto> GetByDate(string date)
        {
            using (var db = Mongo.Create(Server))
            {
                var moodCollection = db.GetCollection<MoodDto>();
                return moodCollection.Find(new { Date = date});
            }
        }

        public static IEnumerable<MoodDto> GetTodays()
        {
            using (var db = Mongo.Create(Server))
            {
                var moodCollection = db.GetCollection<MoodDto>();
                return moodCollection.Find(new { Date = CreateDateString() });
            }
        }

        private static void Upsert(string status, MoodDto result, string shortDate, IMongoCollection<MoodDto> mood)
        {
            if (result == null)
            {
                var newMood = CreateNew(status, shortDate);
                mood.Insert(newMood);
            }
            else
            {
                UpdateCount(result, mood);
            }
        }

        private static string Server
        {
            get { return ConfigurationManager.AppSettings.GetValues("MONGOHQ_URL")[0]; }
        }

        private static void UpdateCount(MoodDto result, IMongoCollection<MoodDto> mood)
        {
            var newMood = new MoodDto();
            newMood._Id = result._Id;
            newMood.Count = result.Count + 1;
            newMood.Status = result.Status;
            newMood.LongDate = DateTime.Now;
            newMood.Date = result.Date;
            mood.UpdateOne(new {result._Id}, newMood);
        }

        private static MoodDto CreateNew(string status, string shortDate)
        {
            return new MoodDto { Date = shortDate, LongDate = DateTime.Now, Status = status, Count = 1 };
        }

        private static MoodDto GetTodays(string status, IMongoCollection<MoodDto> moodCollection, string shortDate)
        {
            return moodCollection.FindOne(new {Date = shortDate, Status = status});
        }

        private static string CreateDateString()
        {
            return DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
        }
    }
}
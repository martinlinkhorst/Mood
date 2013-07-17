using System;
using Norm;

namespace MoodApp
{
    public class MoodDto
    {
        public ObjectId _Id { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
        public DateTime LongDate { get; set; }
        public int Count { get; set; }
    }
}
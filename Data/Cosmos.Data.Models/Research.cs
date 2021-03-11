namespace Cosmos.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Cosmos.Data.Common.Models;

    public class Research : BaseDeletableModel<string>
    {
        public Research()
        {
            this.Id = Guid.NewGuid().ToString();
            this.IsStarted = false;
        }

        public string Name { get; set; }

        public DateTime TimeStarted { get; set; }

        public TimeSpan ResearchTime { get; set; }

        public TimeSpan TimeLeft { get => this.CalculateDifference(this.TimeStarted, this.ResearchTime); }

        public int ExperienceGiven { get; set; }

        public bool IsStarted { get; set; }

        public int Cost { get; set; }

        public string PlayerId { get; set; }

        public Player Player { get; set; }

        private TimeSpan CalculateDifference(DateTime start, TimeSpan duration)
        {
            start.AddMinutes(duration.TotalMinutes);
            var timeLeft = start - DateTime.UtcNow;

            return timeLeft;
        }
    }
}

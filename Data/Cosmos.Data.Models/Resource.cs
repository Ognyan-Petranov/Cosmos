namespace Cosmos.Data.Models
{
    using System;

    using Cosmos.Data.Common.Models;

    public class Resource : BaseDeletableModel<string>
    {
        public Resource()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }
    }
}

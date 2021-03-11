namespace Cosmos.Services.Data
{
    using System.Collections.Generic;

    public interface IRacesService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllRacesAsKeyValuePairs();
    }
}

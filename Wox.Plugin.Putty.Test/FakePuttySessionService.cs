namespace Wox.Plugin.Putty.Test
{
    using System.Collections.Generic;

    public class FakePuttySessionService : IPuttySessionService
    {
        public IEnumerable<PuttySession> FakeResult { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public FakePuttySessionService()
        {
            FakeResult = new List<PuttySession>();
        }

        /// <summary>
        /// Returns a List of all Putty Sessions
        /// </summary>
        /// <returns>A List of all Putty Sessions</returns>
        public IEnumerable<PuttySession> GetAll()
        {
            return FakeResult;
        }
    }
}

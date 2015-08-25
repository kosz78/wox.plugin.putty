namespace Wox.Plugin.Putty
{
    public class PuttySession
    {
        /// <summary>
        /// The Putty Session connection identifier
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// The Protocol that is used in the Putty Session
        /// </summary>
        public string Protocol { get; set; }

        /// <summary>
        /// The optional Username that is used in the Putty Session
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The Hostname that is used in the Putty Session
        /// </summary>
        public string Hostname { get; set; }


        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(Username))
            {
                return string.Format("{0}://{1}", Protocol, Hostname);
            }

            return string.Format("{0}://{1}@{2}", Protocol, Username, Hostname);
        }
    }
}

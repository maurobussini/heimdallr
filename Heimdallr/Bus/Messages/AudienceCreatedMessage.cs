namespace ZenProgramming.Heimdallr.Bus.Messages
{
    /// <summary>
    /// Message for audience created
    /// </summary>
    public class AudienceCreatedMessage
    {
        /// <summary>
        /// Id of created audience
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of audience
        /// </summary>
        public string Name { get; set; }
    }
}

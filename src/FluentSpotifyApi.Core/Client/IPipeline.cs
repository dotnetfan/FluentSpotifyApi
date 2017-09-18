namespace FluentSpotifyApi.Core.Client
{
    /// <summary>
    /// The client execution pipeline.
    /// </summary>
    public interface IPipeline
    {
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        IPipeline Add(IPipelineItem item);
    }
}

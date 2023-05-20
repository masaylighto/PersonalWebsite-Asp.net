
namespace TheWayToGerman.Core.Configuration;

public class RateLimiterConfig
{
    public string PolicyName { get; set; }
    // Must be set to a value greater than <see cref="TimeSpan.Zero" /> by the time these options are passed to the constructor of <see cref="FixedWindowRateLimiter"/>.
    /// </summary>
    public int TimeWindowInMinute { get; set; } 

    /// <summary>
    /// Specified whether the <see cref="FixedWindowRateLimiter"/> is automatically refresh counters or if someone else
    /// will be calling <see cref="FixedWindowRateLimiter.TryReplenish"/> to refresh counters.
    /// </summary>
    /// <value>
    /// <see langword="true" /> by default.
    /// </value>
    public bool AutoReplenishment { get; set; } = true;

    /// <summary>
    /// Maximum number of permit counters that can be allowed in a window.
    /// Must be set to a value > 0 by the time these options are passed to the constructor of <see cref="FixedWindowRateLimiter"/>.
    /// </summary>
    public int PermitLimit { get; set; }

    /// <summary>
    /// Determines the behaviour of <see cref="RateLimiterConfig.AcquireAsync"/> when not enough resources can be leased.
    /// </summary>
    /// <value>
    /// <see cref="QueueProcessingOrder.OldestFirst"/> by default.
    /// </value>
    public string QueueProcessingOrder { get; set; }

    /// <summary>
    /// Maximum cumulative permit count of queued acquisition requests.
    /// Must be set to a value >= 0 by the time these options are passed to the constructor of <see cref="FixedWindowRateLimiter"/>.
    /// </summary>
    public int QueueLimit { get; set; }
}

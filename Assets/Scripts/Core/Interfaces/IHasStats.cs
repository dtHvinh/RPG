public interface IHasStats<TStats> where TStats : IStats
{
    TStats Stats { get; }
}

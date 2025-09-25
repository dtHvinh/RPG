public interface IHasHealth<THealth> where THealth : EntityHealth
{
    THealth Health { get; }
}

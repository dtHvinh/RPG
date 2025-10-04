public interface IHasHealth<out THealth> where THealth : IHealth
{
    THealth Health { get; }
}

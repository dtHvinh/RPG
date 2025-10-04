public interface IHasCombat<out TCombat> where TCombat : ICombat
{
    TCombat Combat { get; }
}

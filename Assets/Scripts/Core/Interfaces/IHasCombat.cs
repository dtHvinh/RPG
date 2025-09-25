public interface IHasCombat<TCombat> where TCombat : EntityCombat
{
    TCombat Combat { get; }
}

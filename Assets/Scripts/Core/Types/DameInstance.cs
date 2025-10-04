[System.Serializable]
public class DameInstance
{
    public DameResult damage;
    public ArmorReductionResult armorReduction;

    public IEntity dameDealer;

    public DameInstance(DameResult damage, ArmorReductionResult armorReduction, IEntity dameDealer)
    {
        this.damage = damage;
        this.armorReduction = armorReduction;
        this.dameDealer = dameDealer;
    }
}

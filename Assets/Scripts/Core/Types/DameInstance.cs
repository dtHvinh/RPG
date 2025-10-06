[System.Serializable]
public class DameInstance
{
    public DameResult damage;
    public ArmorPenetrationResult armorPen;

    public IEntity dameDealer;

    public DameInstance(DameResult damage, ArmorPenetrationResult armorPen, IEntity dameDealer)
    {
        this.damage = damage;
        this.armorPen = armorPen;
        this.dameDealer = dameDealer;
    }
}

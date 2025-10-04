public interface IHasCollision<TCollision> where TCollision : ICollision
{
    TCollision Collision { get; }
}

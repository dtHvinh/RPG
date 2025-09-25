public interface IHasCollision<TCollision> where TCollision : EntityCollision
{
    TCollision Collision { get; }
}

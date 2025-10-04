public interface IHasMovement<TMovement> where TMovement : IMovement
{
    TMovement Movement { get; }
}


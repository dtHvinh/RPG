public interface IHasMovement<IMovement> where IMovement : EntityMovement
{
    IMovement Movement { get; }
}


namespace Project.AI
{
    public interface IMovableEntity
    {
        IMovingPattern MovingPattern { get; }
        void ChangeMovingPattern(IMovingPattern movingPattern);
        void MoveTick();
    }
}
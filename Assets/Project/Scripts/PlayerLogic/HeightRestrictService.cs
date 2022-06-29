namespace Project.Scripts.Player
{
    public class HeightRestrictService
    {
        public float MinHeight { get; private set; }
        public float MaxHeight { get; private set; }

        public void SetMaxHeight(float value)
        {
            MaxHeight = value;
        }
        public void SetMinHeight(float value)
        {
            MinHeight = value;
        }
    }
}
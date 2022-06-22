namespace Project.Scripts.Player
{
    public interface IPlayerHeightLevelChangeListener
    {
        void OnHeightLevelSwitch(SwitchHeightLevelContext switchHeightLevelContext);
    }
    
    public struct SwitchHeightLevelContext
    {   
        public int Level;
    }
}
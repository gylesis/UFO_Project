namespace Project.PlayerLogic
{
    public class PlayerHeightLevelDispatcher
    {
        private readonly IPlayerHeightLevelChangeListener[] _playerHeightLevelChangeListeners;

        public PlayerHeightLevelDispatcher(IPlayerHeightLevelChangeListener[] playerHeightLevelChangeListeners)
        {
            _playerHeightLevelChangeListeners = playerHeightLevelChangeListeners;
        }

        public void OnSwitchHeightLevel(SwitchHeightLevelContext switchHeightLevelContext)
        {
            foreach (IPlayerHeightLevelChangeListener playerHeightLevelChangeListener in
                     _playerHeightLevelChangeListeners)
                playerHeightLevelChangeListener.OnHeightLevelSwitch(switchHeightLevelContext);
        }
    }
}
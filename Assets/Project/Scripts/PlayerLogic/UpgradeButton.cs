using Warlords.Utils;

namespace Project.PlayerLogic
{
    public class UpgradeButton : ReactiveButton<UpgradeType>
    {
        private UpgradeType _upgradeType;
        protected override UpgradeType Value => _upgradeType;

        public void Init(UpgradeType upgradeType)
        {
            _upgradeType = upgradeType;
        }
        
    }
}
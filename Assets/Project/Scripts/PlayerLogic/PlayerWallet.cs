using UniRx;

namespace Project.PlayerLogic
{
    public class PlayerWallet
    {
        public Subject<PlayerWalletData> WalletUpdated { get; } = new Subject<PlayerWalletData>();

        private int _tenge;

        private PlayerWalletData _walletData;

        public PlayerWallet()
        {
            _walletData = new PlayerWalletData();
            _walletData.Tenge = 0;
        }

        public void AddTenge(int amount)
        {
            _tenge += amount;
            _walletData.Tenge = _tenge;
            WalletUpdated.OnNext(_walletData);
        }
    }
}
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.PlayerLogic
{
    public class PlayerWalletView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _tengeView;

        [Inject]
        private void Init(PlayerWallet playerWallet)
        {
            playerWallet.WalletUpdated.TakeUntilDestroy(this).Subscribe((OnWalletUpdate));
        }

        private void OnWalletUpdate(PlayerWalletData walletData)
        {
            _tengeView.text = walletData.Tenge.ToString();
        }
    }
}
using System;
using System.Collections.Generic;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Warlords.Utils;
using Zenject;
using Object = UnityEngine.Object;

namespace Project.PlayerLogic.Upgrades
{
    public class UpgradesViewController : IInitializable, ITickable, IDisposable
    {
        private Transform _parent;
        private UpgradeView _upgradeViewPrefab;
        private CanvasGroup _canvasGroup;

        private bool _isOpened;

        public Subject<UpgradeType> UpgradeEvent { get; } = new Subject<UpgradeType>();

        private CompositeDisposable _compositeDisposable = new CompositeDisposable();

        private Dictionary<UpgradeType, UpgradeView> _upgradeViews = new Dictionary<UpgradeType, UpgradeView>();

        private UpgradesViewController(UpgradeUIViewData upgradeUIViewData, UpgradeView upgradeViewPrefab)
        {
            _upgradeViewPrefab = upgradeViewPrefab;
            _parent = upgradeUIViewData.Parent;
            _canvasGroup = upgradeUIViewData.CanvasGroup;
        }

        public void Initialize()
        {
            return;
        }

        public void SpawnView(UpgradeUIViewContext context)
        {
            UpgradeType upgradeType = context.UpgradeType;

            if (_upgradeViews.ContainsKey(upgradeType))
            {
                Debug.LogError($"This type of upgrade exists already");
                return;
            }

            UpgradeView upgradeView = Object.Instantiate(_upgradeViewPrefab, _parent);

            _upgradeViews.Add(upgradeType, upgradeView);

            upgradeView.UpgradeButton.Clicked.Subscribe((OnUpgradeButtonClicked)).AddTo(_compositeDisposable);

            upgradeView.Init(context);
        }

        private void OnUpgradeButtonClicked(EventContext<UpgradeType> context)
        {
            UpgradeType upgradeType = context.Value;

            UpgradeEvent.OnNext(upgradeType);
        }

        public void UpdateView(UpdateViewUpdateContext context)
        {
            UpgradeType upgradeType = context.UpgradeType;
            UpgradeView upgradeView = _upgradeViews[upgradeType];
            upgradeView.UpdateView(context);
        }

        public void ShowMenu()
        {
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.DOFade(1, 1);
        }

        public void HideMenu()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.DOFade(0, 1);
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (_isOpened == false)
                {
                    ShowMenu();
                }
                else
                {
                    HideMenu();
                }

                _isOpened = !_isOpened;
            }
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }

    public struct UpdateViewUpdateContext
    {
        public UpgradeType UpgradeType;
        public int NewPrice;
        public ushort NewLevel;
    }
}
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Project.PlayerLogic
{
    public class UpgradeView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _costText;
        [SerializeField] private TMP_Text _upgradeLevelText;
        [SerializeField] private TMP_Text _upgradeTypeText;

        [SerializeField] private UpgradeButton _upgradeButton;

        public UpgradeButton UpgradeButton => _upgradeButton;

        public void Init(UpgradeUIViewContext context)
        {
            _upgradeTypeText.text = context.UpgradeType.ToString();
            _nameText.text = context.Name;
            _upgradeLevelText.text = context.Level.ToString();
            _costText.text = $"{context.Cost}$";
            _description.text = context.Description;

            _upgradeButton.Init(context.UpgradeType);
        }

        public void UpdateView(UpdateViewUpdateContext context)
        {
            _costText.text = $"{context.NewPrice}$";

            UpdateLevelView(context.NewLevel);
        }

        private void UpdateLevelView(ushort level)
        {   
            if (level == 99)
            {
                _upgradeLevelText.text = "MAX";
            }
            else
            {
                _upgradeLevelText.text = $"{level}";
            }
        }
    }

    public struct UpgradeUIViewContext
    {
        public string Name;
        public string Description;
        public UpgradeType UpgradeType;
        public int Cost;
        public byte Level;
    }

    public enum UpgradeType
    {
        Health,
        ScavengeSpeed,
        FlySpeed,
        Shield
    }


    public abstract class Upgrade : ScriptableObject
    {
        [SerializeField] private string _title;
        [SerializeField] private string _description;

        [SerializeField] private List<UpgradeInfo> _upgradeInfos = new List<UpgradeInfo>();
    }

    [Serializable]
    public class UpgradeInfo
    {
        [SerializeField] private string _name;

        public int Cost;
    }
}
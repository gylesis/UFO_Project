using System;
using System.Linq;
using UnityEngine;

namespace Project.Scripts.AI
{
    [CreateAssetMenu(menuName = "CreaturesInfo", fileName = "CreaturesInfo", order = 0)]
    public class CreaturesInfo : ScriptableObject
    {
        [SerializeField] private CreatureInfo[] _creatures;
       
        [SerializeField] private CreatureTagSO _cloudTag;        
        [SerializeField] private CreatureTagSO _junkTag;
        [SerializeField] private CreatureTagSO _boltTag;

        public CreatureInfo[] Creatures => _creatures;

        public CreatureInfo GetCloudInfo()
        {
            return _creatures.FirstOrDefault(x => x.Tag == _cloudTag);
        }
        
        public CreatureInfo GetJunkInfo()
        {
            return _creatures.FirstOrDefault(x => x.Tag == _junkTag);
        }

        public CreatureInfo GetBoltInfo()
        {
            return _creatures.FirstOrDefault(x => x.Tag == _boltTag);
        }
        
        private void OnValidate()
        {
            foreach (CreatureInfo creatureInfo in _creatures)
                creatureInfo._name = creatureInfo.Prefab.name;
        }
    }


    [Serializable]
    public class CreatureInfo
    {
        [HideInInspector] public string _name;
        public GameObject Prefab;
        public CreatureTagSO Tag;
    }
}
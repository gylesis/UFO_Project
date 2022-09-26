using System;
using UniRx;
using UnityEngine;

namespace Project
{
    [CreateAssetMenu(menuName = "StaticData/Config", fileName = "Config", order = 0)]
    public class Config : ScriptableObject
    {
        [SerializeField] private FloatReactiveProperty _cameraYSpeedMovement;
        [SerializeField] private FloatReactiveProperty _cameraXSpeedMovement;
        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private BuildingResourcesData _buildingResourcesData;

        public BuildingResourcesData BuildingResourcesData => _buildingResourcesData;
        public PlayerStats PlayerStats => _playerStats;
        public FloatReactiveProperty CameraYSpeedMovement => _cameraYSpeedMovement;
        public FloatReactiveProperty CameraXSpeedMovement => _cameraXSpeedMovement;

        public HeightLevelTransitionInfo[] _heightLevelTransitionInfo;

        private void OnValidate()
        {
            for (var index = 0; index < _heightLevelTransitionInfo.Length; index++)
            {
                HeightLevelTransitionInfo cameraHeightLevelInfo = _heightLevelTransitionInfo[index];
                cameraHeightLevelInfo._name = $"Level{index + 1}";
            }
        }
    }

    [Serializable]
    public class BuildingResourcesData
    {
        public int StartResources;
    }

    [Serializable]
    public class HeightLevelTransitionInfo
    {
        [HideInInspector] public string _name;
        public CameraHeightLevelInfo CameraHeightLevelInfo;
        public PlayerHeightLevelInfo PlayerHeightLevelInfo;
    }

    [Serializable]
    public class PlayerHeightLevelInfo
    {
        public float Size;
        public AnimationCurve SizeTransition;
    }

    [Serializable]
    public class CameraHeightLevelInfo
    {
        public AnimationCurve PosTransition;
        public float OrthographicSize;
        public float YHeight;
    }
}
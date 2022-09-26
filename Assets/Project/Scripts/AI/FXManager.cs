using UnityEngine;

namespace Project.AI
{
    public class FXManager
    {
        private StaticData _staticData;

        public FXManager(StaticData staticData)
        {
            _staticData = staticData;
        }

        public void MissileExplosion(Vector3 pos)
        {
            GameObject missileExplosion =
                Object.Instantiate(_staticData.MissileExplosionParticles, pos, Quaternion.identity);

            Object.Destroy(missileExplosion, 5);
        }
    }
}
using UnityEngine;

namespace ShooterProject.Scripts.Spawner
{
    public class SpawnObjectParams : MonoBehaviour
    {
        [Header("Spawn Object Params")]
        
        #region Fields

        public int spawnWeight;

        public int maxImpacts;
	
        #endregion

        #region Properties

        public int SpawnWeight 
        { 
            get 
            {
                return spawnWeight;
            }
            private set 
            {
                spawnWeight = value;
            }
        }

        #endregion
    }
}
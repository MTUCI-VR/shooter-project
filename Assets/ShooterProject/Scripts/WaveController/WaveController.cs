using UnityEngine;

namespace ShooterProject.Scripts.WaveControllers
{
    public class WaveController : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private int timeBetweenWaves;

        #endregion

        #region Properties

        public int TimeBetweenWaves => timeBetweenWaves;

        #endregion
    }
}

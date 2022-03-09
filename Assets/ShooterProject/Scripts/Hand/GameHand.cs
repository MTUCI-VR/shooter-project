using UnityEngine;

namespace ShooterProject.Scripts.Hand
{
    public class GameHand : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private HandSide side;

        #endregion

        #region Properties

        public HandSide Side => side;

        #endregion

        #region LifeCycle

        private void OnValidate()
        {
            side = name.Contains("Left") ? HandSide.Left : HandSide.Right;
        }

        #endregion
    }
}

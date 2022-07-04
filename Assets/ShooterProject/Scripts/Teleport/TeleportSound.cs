using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace ShooterProject.Scripts.Teleport
{
    [RequireComponent(typeof(TeleportationArea))]
    public class TeleportSound : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private AudioSource audioSource;

        private TeleportationArea _teleportationArea;

        #endregion

        #region Life Cycle

        private void Awake()
        {
            _teleportationArea = GetComponent<TeleportationArea>();
        }

        private void OnEnable()
        {
            _teleportationArea.teleporting.AddListener(OnTeleporting);
        }
        private void OnDisable()
        {
            _teleportationArea.teleporting.RemoveListener(OnTeleporting);
        }

        #endregion

        #region Private Methods

        private void OnTeleporting(TeleportingEventArgs teleportingEventArgs)
        {
            audioSource.Play();
        }

        #endregion
    }
}

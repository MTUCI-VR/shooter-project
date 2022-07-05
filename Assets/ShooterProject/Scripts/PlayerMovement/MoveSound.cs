using UnityEngine;
using UnityEngine.InputSystem;

public class MoveSound : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private InputActionProperty leftHandMoveAction;

    [SerializeField]
    private AudioSource audioSource;

    #endregion

    #region Life Cycle

    private void OnEnable()
    {
        leftHandMoveAction.action.started += OnActivateActionStarted;
        leftHandMoveAction.action.canceled += OnActivateActionCanceled;
    }

    private void OnDisable()
    {
        leftHandMoveAction.action.started -= OnActivateActionStarted;
        leftHandMoveAction.action.canceled -= OnActivateActionCanceled;
    }

    #endregion

    #region Private Methods

    private void OnActivateActionStarted(InputAction.CallbackContext callbackContext)
    {
        audioSource.Play();
    }

    private void OnActivateActionCanceled(InputAction.CallbackContext callbackContext)
    {
        audioSource.Stop();
    }

    #endregion
}

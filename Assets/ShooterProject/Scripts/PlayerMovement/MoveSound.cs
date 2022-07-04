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
        leftHandMoveAction.action.started += OnActivateActionPerformed;
        leftHandMoveAction.action.canceled += OnActivateActionCanceled;
    }

    private void OnDisable()
    {
        leftHandMoveAction.action.started -= OnActivateActionPerformed;
        leftHandMoveAction.action.canceled -= OnActivateActionCanceled;
    }

    #endregion

    #region Private Methods

    private void OnActivateActionPerformed(InputAction.CallbackContext callbackContext)
    {
        audioSource.Play();
    }

    private void OnActivateActionCanceled(InputAction.CallbackContext callbackContext)
    {
        audioSource.Stop();
    }

    #endregion
}

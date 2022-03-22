namespace UnityEngine.XR.Interaction.Toolkit
{
    public class UpdateCharacterController : CharacterControllerDriver
    {
        #region LifeCycle
        private void FixedUpdate()
        {
            UpdateCharacterController();
        }
        #endregion
    }
}
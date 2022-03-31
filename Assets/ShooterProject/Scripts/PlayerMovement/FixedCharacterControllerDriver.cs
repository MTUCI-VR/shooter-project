namespace UnityEngine.XR.Interaction.Toolkit
{
	public class FixedCharacterControllerDriver : CharacterControllerDriver
	{
		#region LifeCycle

		private void FixedUpdate()
		{
			UpdateCharacterController();
		}

		#endregion
	}
}

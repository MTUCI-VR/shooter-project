using UnityEngine;
using ShooterProject.Scripts.PlayerScripts;

namespace ShooterProject.Scripts.Effects
{
	public class HealEffect : Effect
	{
		#region Constant Fields

		private const string START_TRIGGER = "Start";

		#endregion
		
		#region Life Cycle

		private void OnEnable()
		{
			Player.Instance.PlayerHealth.OnHealed += OnHealed;
		}

		private void OnDisable()
		{
			if (!(Player.Instance is null))
				Player.Instance.PlayerHealth.OnHealed -= OnHealed;
		}

		#endregion

		#region Private Methods

		private void OnHealed()
		{
			_animator.SetTrigger(START_TRIGGER);
		}

		#endregion
	}
}

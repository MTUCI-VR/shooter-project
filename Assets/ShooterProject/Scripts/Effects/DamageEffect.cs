using UnityEngine;
using ShooterProject.Scripts.PlayerScripts;

namespace ShooterProject.Scripts.Effects
{
	public class DamageEffect : Effect
	{
		#region Constant Fields

		private const string START_TRIGGER = "Start";

		#endregion

		#region Life Cycle

		private void OnEnable()
		{
			Player.Instance.PlayerHealth.OnDamaged += OnDamaged;
		}

		private void OnDisable()
		{
			if (!(Player.Instance is null))
				Player.Instance.PlayerHealth.OnDamaged -= OnDamaged;
		}

		#endregion

		#region Private Methods

		private void OnDamaged()
		{
			_animator.SetTrigger(START_TRIGGER);
		}

		#endregion
	}
}

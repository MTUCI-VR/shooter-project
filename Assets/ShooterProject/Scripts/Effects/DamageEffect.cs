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
			Player.Instance.PlayerHealth.onDamaged += OnDamaged;
		}

		private void OnDisable()
		{
			Player.Instance.PlayerHealth.onDamaged -= OnDamaged;
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

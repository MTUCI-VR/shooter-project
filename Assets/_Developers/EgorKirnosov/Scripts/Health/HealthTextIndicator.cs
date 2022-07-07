using UnityEngine;
using TMPro;

namespace _Developers.EgorKirnosov.Scripts.Health
{
	[RequireComponent(typeof(ShooterProject.Scripts.Actors.Health.Health))]
	public class HealthTextIndicator : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private TextMeshProUGUI textLabel;

		private ShooterProject.Scripts.Actors.Health.Health targetHealth;

		#endregion

		#region LifeCycle

		private void Awake()
		{
			targetHealth = GetComponent<ShooterProject.Scripts.Actors.Health.Health>();
		}

		private void OnEnable()
		{
			targetHealth.OnChanged += OnHit;
			targetHealth.OnDied += OnHpZeroed;
		}

		private void OnDisable()
		{
			targetHealth.OnChanged -= OnHit;
			targetHealth.OnDied -= OnHpZeroed;
		}

		#endregion

		#region Private Methods

		public void OnHit(ShooterProject.Scripts.Actors.Health.Health sender)
		{
			textLabel.text = targetHealth.CurrentHealth.ToString();
		}
		public void OnHpZeroed(ShooterProject.Scripts.Actors.Health.Health sender)
		{
			textLabel.text = "Dead";
		}

		#endregion
	}
}

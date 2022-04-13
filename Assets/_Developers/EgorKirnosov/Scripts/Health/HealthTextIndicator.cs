using UnityEngine;
using TMPro;

namespace _Developers.EgorKirnosov.Scripts.Health
{
	[RequireComponent(typeof(ShooterProject.Scripts.Actors.Health))]
	public class HealthTextIndicator : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private TextMeshProUGUI textLabel;

		private ShooterProject.Scripts.Actors.Health targetHealth;

		#endregion

		#region LifeCycle

		private void Awake()
		{
			targetHealth = GetComponent<ShooterProject.Scripts.Actors.Health>();
		}

		private void OnEnable()
		{
			targetHealth.OnHpChanged += OnHit;
			targetHealth.OnHpZeroed += OnHpZeroed;
		}

		private void OnDisable()
		{
			targetHealth.OnHpChanged -= OnHit;
			targetHealth.OnHpZeroed -= OnHpZeroed;
		}

		#endregion

		#region Private Methods

		public void OnHit()
		{
			textLabel.text = targetHealth.CurrentHealth.ToString();
		}
		public void OnHpZeroed()
		{
			textLabel.text = "Dead";
		}

		#endregion
	}
}

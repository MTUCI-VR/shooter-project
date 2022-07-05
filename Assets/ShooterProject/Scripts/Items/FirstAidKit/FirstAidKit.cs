using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using ShooterProject.Scripts.Actors.Health;
using ShooterProject.Scripts.PlayerScripts;

namespace ShooterProject.Scripts.Items.FirstAidKit
{
	[RequireComponent(typeof(XRGrabInteractable))]
	public class FirstAidKit : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private int healthPointsForHealing;

		private Health _playerHealth;

		private XRGrabInteractable _grabInteractable;

		#endregion

		#region Life Cycle

		private void Awake()
		{
			_playerHealth = Player.instance.PlayerHealth;

			_grabInteractable = GetComponent<XRGrabInteractable>();
		}

		private void OnEnable()
		{
			_grabInteractable.activated.AddListener(OnActivated);
		}

		private void OnDisable()
		{
			_grabInteractable.activated.RemoveListener(OnActivated);
		}

		#endregion

		#region Private Methods

		private void OnActivated(ActivateEventArgs activateEventArgs)
		{
			if (_playerHealth.CurrentHealth < _playerHealth.MaxHealth)
			{
				_playerHealth.Heal(healthPointsForHealing);
				Destroy(gameObject);
			}
		}

		#endregion
	}
}

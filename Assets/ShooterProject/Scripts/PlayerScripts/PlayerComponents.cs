using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using ShooterProject.Scripts.Teleport;
using ShooterProject.Scripts.Actors.Health;

namespace ShooterProject.Scripts.PlayerScripts
{
	[RequireComponent(typeof(Health))]
	[RequireComponent(typeof(CharacterController))]
	[RequireComponent(typeof(LocomotionSystem))]
	[RequireComponent(typeof(TeleportationProvider))]
	[RequireComponent(typeof(TeleportationToggler))]
	[RequireComponent(typeof(FixedCharacterControllerDriver))]
	[RequireComponent(typeof(ActionBasedContinuousMoveProvider))]
	[RequireComponent(typeof(MoveSound))]
	public class PlayerComponents : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private List<GameObject> playerObjects;

		[SerializeField]
		private GameObject fingerCollider;

		private List<MonoBehaviour> _components = new List<MonoBehaviour>();

		#endregion

		#region Life Cycle

		private void Awake()
		{
			_components.Add(GetComponent<Health>());
		}

		#endregion

		#region Public Methods

		///<summary>
		/// Выключает лишние объекты и компоненты игрока для сцен с меню
		///</summary>
		public void DisableComponents()
		{
			transform.position = Vector3.zero;

			_components.ForEach(component => component.enabled = false);
			playerObjects.ForEach(playerObject => playerObject.SetActive(false));

			fingerCollider.SetActive(true);
		}

		///<summary>
		/// Включает объекты и компоненты игрока для сцен с игрой
		///</summary>
		public void EnableComponents()
		{
			transform.position = Vector3.zero;

			_components.ForEach(component => component.enabled = true);
			playerObjects.ForEach(playerObject => playerObject.SetActive(true));
			
			fingerCollider.SetActive(false);
		}

		#endregion
	}
}

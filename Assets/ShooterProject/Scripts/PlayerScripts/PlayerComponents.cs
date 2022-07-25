using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using ShooterProject.Scripts.Teleport;
using ShooterProject.Scripts.Actors.Health;

namespace ShooterProject.Scripts.PlayerScripts
{
	public class PlayerComponents : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private List<GameObject> playerObjectsForGame;

		[SerializeField]
		private GameObject leftFingerCollider;
		
		[SerializeField]
		private GameObject rightFingerCollider;

		#endregion

		#region Public Methods
	

		///<summary>
		/// Выключает лишние объекты и компоненты игрока для сцен с меню
		///</summary>
		public void DisableComponents()
		{
			transform.position = Vector3.zero;

			Player.Instance.PlayerHealth.enabled = false;

			playerObjectsForGame.ForEach(playerObject => playerObject.SetActive(false));

			leftFingerCollider.SetActive(true);
			rightFingerCollider.SetActive(true);
		}

		///<summary>
		/// Включает объекты и компоненты игрока для сцен с игрой
		///</summary>
		public void EnableComponents()
		{
			transform.position = Vector3.zero;

			Player.Instance.PlayerHealth.enabled = true;

			playerObjectsForGame.ForEach(playerObject => playerObject.SetActive(true));
			
			leftFingerCollider.SetActive(false);
			rightFingerCollider.SetActive(false);
		}

		#endregion
	}
}

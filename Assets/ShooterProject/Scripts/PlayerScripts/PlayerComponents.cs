using System.Collections.Generic;
using UnityEngine;

namespace ShooterProject.Scripts.PlayerScripts
{
	public class PlayerComponents : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private List<GameObject> playerObjectsForGame;

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
		}

		///<summary>
		/// Включает объекты и компоненты игрока для сцен с игрой
		///</summary>
		public void EnableComponents()
		{
			transform.position = Vector3.zero;

			Player.Instance.PlayerHealth.enabled = true;

			playerObjectsForGame.ForEach(playerObject => playerObject.SetActive(true));
		}

		#endregion
	}
}

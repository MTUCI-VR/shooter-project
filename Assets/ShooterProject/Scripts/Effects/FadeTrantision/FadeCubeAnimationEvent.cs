using System;
using UnityEngine;

namespace ShooterProject.Scripts.Effects
{
	public class FadeCubeAnimationEvent : MonoBehaviour
	{
		#region Events

		public event Action OnFadeCubeStartAnimationFinished;

		#endregion

		#region Private Methods

		private void OnAnimationEvent()
		{
			OnFadeCubeStartAnimationFinished?.Invoke();
		}

		#endregion
	}
}

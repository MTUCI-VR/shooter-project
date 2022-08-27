using System;
using UnityEngine;

namespace ShooterProject.Scripts.Effects
{
	public class FadeBackgroundAnimationEvent : MonoBehaviour
	{
		#region Events

		public event Action OnFadeBackgroundEndAnimationFinished;
		public event Action OnFadeBackgroundStartAnimationFinished;

		#endregion

		#region Private Methods

		private void OnEndAnimationFinishedEvent()
		{
			OnFadeBackgroundEndAnimationFinished?.Invoke();
		}

		private void OnStartAnimationFinishedEvent()
		{
			OnFadeBackgroundStartAnimationFinished?.Invoke();
		}

		#endregion
	}
}

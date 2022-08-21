using UnityEngine;
using System.Collections;
using ShooterProject.Scripts.General;

namespace ShooterProject.Scripts.GameManager
{
	public class FadeTransition : Singleton<FadeTransition>
	{
		#region Constant Fields

		private const string START_TRIGGER = "Start";
		private const string END_TRIGGER = "End";

		#endregion

		#region Fields

		[SerializeField]
		private Animator fadeCubeAnimator;

		[SerializeField]
		private Animator fadeBackgroundAnimator;

		[SerializeField]
		private AnimationClip fadeCubeStartAnimationClip;

		[SerializeField]
		private AnimationClip fadeBackgroundStartAnimationClip;

		#endregion

		#region Properties

		private float FadeBackgroundDuration => fadeBackgroundStartAnimationClip.length;
		private float FadeCubeDuration => fadeCubeStartAnimationClip.length;

		public float Duration => FadeBackgroundDuration + FadeCubeDuration;

		#endregion

		#region Private Methods

		private IEnumerator FadeTransitionStartCoroutine()
		{
			fadeCubeAnimator.SetTrigger(START_TRIGGER);

			yield return new WaitForSeconds(FadeCubeDuration);

			fadeBackgroundAnimator.SetTrigger(START_TRIGGER);
		}

		private IEnumerator FadeTransitionEndCoroutine()
		{
			fadeBackgroundAnimator.SetTrigger(END_TRIGGER);

			yield return new WaitForSeconds(FadeBackgroundDuration);

			fadeCubeAnimator.SetTrigger(END_TRIGGER);
		}

		#endregion

		#region Public Methods

		public void FadeTransitionStart()
		{
			StartCoroutine(FadeTransitionStartCoroutine());
		}

		public void FadeTransitionEnd()
		{
			StartCoroutine(FadeTransitionEndCoroutine());
		}

		#endregion
	}
}

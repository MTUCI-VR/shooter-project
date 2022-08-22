using UnityEngine;
using System.Collections;
using ShooterProject.Scripts.General;

namespace ShooterProject.Scripts.Effects
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
		private FadeCubeAnimationEvent fadeCubeAnimationEvent;

		[SerializeField]
		private FadeBackgroundAnimationEvent fadeBackgroundAnimationEvent;

		#endregion

		#region Properties

		public bool IsFadeBackroundDarkened;

		#endregion

		#region Life Cycle

		private void OnEnable()
		{
			fadeCubeAnimationEvent.OnFadeCubeStartAnimationFinished += FadeBackgroundAnimationStart;

			fadeBackgroundAnimationEvent.OnFadeBackgroundEndAnimationFinished += FadeCubeAnimationEnd;
			fadeBackgroundAnimationEvent.OnFadeBackgroundStartAnimationFinished += OnFadeBackgroundStartAnimationFinished;
		}

		private void OnDisable()
		{
			fadeCubeAnimationEvent.OnFadeCubeStartAnimationFinished -= FadeBackgroundAnimationStart;

			fadeBackgroundAnimationEvent.OnFadeBackgroundEndAnimationFinished -= FadeCubeAnimationEnd;
			fadeBackgroundAnimationEvent.OnFadeBackgroundStartAnimationFinished -= OnFadeBackgroundStartAnimationFinished;
		}

		#endregion

		#region Private Methods

		private void FadeCubeAnimationStart()
		{
			fadeCubeAnimator.SetTrigger(START_TRIGGER);
		}

		private void FadeCubeAnimationEnd()
		{
			fadeCubeAnimator.SetTrigger(END_TRIGGER);
		}

		private void FadeBackgroundAnimationStart()
		{
			fadeBackgroundAnimator.SetTrigger(START_TRIGGER);
		}

		private void FadeBackgroundAnimationEnd()
		{
			IsFadeBackroundDarkened = false;

			fadeBackgroundAnimator.SetTrigger(END_TRIGGER);
		}

		private void OnFadeBackgroundStartAnimationFinished()
		{
			IsFadeBackroundDarkened = true;
		}

		#endregion

		#region Public Methods

		public void FadeTransitionStart()
		{
			FadeCubeAnimationStart();
		}

		public void FadeTransitionEnd()
		{
			FadeBackgroundAnimationEnd();
		}

		#endregion
	}
}

using UnityEngine;

namespace ShooterProject.Scripts.GameManager
{
	public class FadeTransition : MonoBehaviour
	{
		#region Constant Fields

		private const string START_TRIGGER = "Start";
		private const string END_TRIGGER = "End";

		#endregion

		#region Fields

		[SerializeField]
		private Animator animator;

		#endregion

		#region Properties

		public float FadeTransitionDuration => animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;

		#endregion

		#region Public Methods

		public void FadeTransitionStart()
		{
			animator.SetTrigger(START_TRIGGER);
		}

		public void FadeTransitionEnd()
		{
			animator.SetTrigger(END_TRIGGER);
		}

		#endregion
	}
}

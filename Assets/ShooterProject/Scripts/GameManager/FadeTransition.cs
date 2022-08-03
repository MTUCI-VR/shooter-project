using UnityEngine;

namespace ShooterProject.Scripts.GameManager
{
	public class FadeTransition : MonoBehaviour
	{
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
			animator.SetTrigger("Start");
		}
		public void FadeTransitionEnd()
		{
			animator.SetTrigger("End");
		}

		#endregion
	}
}

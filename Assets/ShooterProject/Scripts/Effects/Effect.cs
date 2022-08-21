using UnityEngine;

namespace ShooterProject.Scripts.Effects
{
	[RequireComponent(typeof(Animator))]
	public class Effect : MonoBehaviour
	{
		#region Fields

		protected Animator _animator;

		#endregion

		#region Life Cycle

		protected virtual void Awake()
		{
			_animator = GetComponent<Animator>();
		}

		#endregion
	}
}

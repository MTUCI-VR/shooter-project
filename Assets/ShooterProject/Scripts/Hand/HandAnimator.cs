using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShooterProject.Scripts.Hand
{
	[RequireComponent(typeof(Animator))]
	public class HandAnimator : MonoBehaviour
	{
		#region Static Fields

		private static readonly int Grip = Animator.StringToHash("Grip");
		private static readonly int Trigger = Animator.StringToHash("Trigger");

		#endregion

		#region Fields

		[SerializeField]
		private InputActionProperty triggerAction;

		[SerializeField]
		private InputActionProperty gripAction;

		private Animator _animatorge;

		#endregion

		#region LifeCycle

		private void Awake()
		{
			_animatorge = GetComponent<Animator>();
		}

		private void Update()
		{
			UpdateAnimatorTriggers();
		}

		#endregion

		#region Private Methods

		private void UpdateAnimatorTriggers()
		{
			_animatorge.SetFloat(Grip, gripAction.action.ReadValue<float>());
			_animatorge.SetFloat(Trigger, triggerAction.action.ReadValue<float>());
		}

		#endregion
	}
}

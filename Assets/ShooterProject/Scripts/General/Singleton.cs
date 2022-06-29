using UnityEngine;

namespace ShooterProject.Scripts.General
{
	public abstract class Singleton<T> : MonoBehaviour where T : Component
	{		
		#region Fields

		private static T _instance;

		#endregion

		#region Properties
		public static T Instance
		{
			get
			{
				if ( _instance == null )
				{
					_instance = FindObjectOfType<T> ();
				}
				return _instance;
			}
		}

		#endregion

		#region Methods

		protected virtual void Awake ()
		{
			if ( _instance == null )
			{
				_instance = this as T;
			}
		}

		#endregion
		
	}
}
using System.Collections;

namespace ShooterProject.Scripts.Spawner
{
	public class TestSpawner : GeneralSpawner
	{
		#region Fields
		private IEnumerator _spawnCoroutine;

		#endregion

		#region  LifeCycle

		private void Start()
		{
			_spawnCoroutine = Spawn();
			StartCoroutine(_spawnCoroutine);
		}

		#endregion
	}
}

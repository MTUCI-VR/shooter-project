namespace ShooterProject.Scripts.Spawner
{
	public struct EnemySpawnerActivationData
	{
		public EnemySpawner EnemySpawner;
		public bool CanSpawn;

		public EnemySpawnerActivationData(EnemySpawner enemySpawner, bool canSpawn)
		{
			EnemySpawner = enemySpawner;
			CanSpawn = canSpawn;
		}
	}
}

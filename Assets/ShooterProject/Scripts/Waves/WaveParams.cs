using ShooterProject.Scripts.Spawner;

namespace ShooterProject.Scripts.Waves
{
	[System.Serializable]
	public class WaveParams
	{
		public int EnemiesCount;
		public SpawnObjectParams[] AvailableEnemies;
		public float NextWavePreparationSeconds;
	}
}

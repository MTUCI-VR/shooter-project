using ShooterProject.Scripts.Spawner;

namespace ShooterProject.Scripts.Waves
{
	public class WaveParams
	{
		public SpawnObjectParams[] AvailableEnemies;
		public int EnemiesCount;
		public float NextWavePreparationSeconds;
	}
}

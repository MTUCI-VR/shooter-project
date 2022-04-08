using UnityEngine;
using System.Collections;
using ShooterProject.Scripts.General;

namespace ShooterProject.Scripts.Spawner
{
    public class GeneralSpawner : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        protected GameObject[] objectsForSpawn;

        [SerializeField]
        private int timeToNextSpawn;

        protected GameObjectsPool[] _impactPools;

        #endregion

        #region Private Methods

        private int GetImpactPoolSpawnObject(GameObject[] objectsForSpawn)
        {
            System.Collections.Generic.List<int> spawnWeights = new System.Collections.Generic.List<int>();

            int sum = 0;

            foreach (var objectForSpawn in objectsForSpawn)
            {
                int objectSpawnWeight = objectForSpawn.GetComponent<SpawnObjectParams>().SpawnWeight;

                spawnWeights.Add(objectSpawnWeight);
                sum += objectSpawnWeight;
            }

            int[] spawnWeightsForCount = spawnWeights.ToArray();
            
            System.Array.Sort(spawnWeightsForCount);

            int spawnWeight = 0;

            int random = Random.Range(0, sum);

            for (int i = 0; i < spawnWeightsForCount.Length; i++)
            {
                if (random < spawnWeightsForCount[i])
                {
                    spawnWeight = spawnWeightsForCount[i];
                    break;
                }
                else
                    random -= spawnWeightsForCount[i];
            }

            return spawnWeights.FindIndex(x => x == spawnWeight);
        }

        #endregion

        #region Protected Methods
        protected IEnumerator Spawn()
        {
            while(_impactPools != null)
            {
                yield return new WaitForSeconds(timeToNextSpawn);

                int spawnObjectIndex = GetImpactPoolSpawnObject(objectsForSpawn);

                GameObjectsPool impact = _impactPools[spawnObjectIndex];
                GameObject objectForSpawn = objectsForSpawn[spawnObjectIndex];

                if (impact.TryGetFreeElement(out objectForSpawn,true))
                {
                    objectForSpawn.transform.position = transform.position;
                    objectForSpawn.transform.rotation = transform.rotation;
                }
            }
        }

        #endregion
    }

}

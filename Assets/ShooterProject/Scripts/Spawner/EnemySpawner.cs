using UnityEngine;
using System.Collections;
using ShooterProject.Scripts.General;

namespace ShooterProject.Scripts.Spawner
{
    public class EnemySpawner : GeneralSpawner
    {
        #region Fields
        private IEnumerator _spawnCoroutine;

        #endregion

        #region  LifeCycle
        private void Awake()
        {
            _impactPools = new GameObjectsPool[objectsForSpawn.Length];

            for (int i = 0; i < objectsForSpawn.Length; i++)
            {
                SpawnObjectParams objectParams = objectsForSpawn[i].GetComponent<SpawnObjectParams>();

                _impactPools[i] = new GameObjectsPool(objectParams.maxImpacts,
                    false,
                    false,
                    objectsForSpawn[i],
                    null);
            }

            _spawnCoroutine = Spawn();
        }
        private void Start()
        {
            StartCoroutine(_spawnCoroutine);
        }

        #endregion

        #region Private Methods
        private void OnTriggerEnter(Collider collider) {
            
            if (collider.tag == "Player")
            {
                StopCoroutine(_spawnCoroutine);
            }
        }
        private void OnTriggerExit(Collider collider) {

            if (collider.tag == "Player")
            {
                StartCoroutine(_spawnCoroutine);
            }
        }

        #endregion
    }
}
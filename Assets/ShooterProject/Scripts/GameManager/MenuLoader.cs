using UnityEngine;

namespace ShooterProject.Scripts.GameManager
{
    public class MenuLoader : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private string menuSceneName;

        #endregion

        #region LifeCycle

        private void Start()
        {
			StartCoroutine(ShooterProject.Scripts.GameManager.SceneLoader.LoadScene(menuSceneName,null,GameManager.SceneType.Menu));
        }

        #endregion
    }
}

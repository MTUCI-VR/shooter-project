// This class is Auto-Generated. Don't delete it!
using UnityEditor;
using UnityEditor.SceneManagement;

public static class SceneMenuList
{
	[MenuItem("Scenes/PersistentScene")]
	private static void OpenPersistentScene()
	{
		EditorSceneManager.OpenScene(
			"Assets/ShooterProject/Scenes/PersistentScene/PersistentScene.unity",
			OpenSceneMode.Single
		);
		EditorSceneManager.OpenScene(
			"Assets/ShooterProject/Scenes/PersistentScene/PersistentScene.unity",
			OpenSceneMode.Additive
		);
	}

	[MenuItem("Scenes/TestScene")]
	private static void OpenTestScene()
	{
		EditorSceneManager.OpenScene(
			"Assets/ShooterProject/Scenes/PersistentScene/PersistentScene.unity",
			OpenSceneMode.Single
		);
		EditorSceneManager.OpenScene(
			"Assets/ShooterProject/Scenes/TestScene/TestScene.unity",
			OpenSceneMode.Additive
		);
	}
}

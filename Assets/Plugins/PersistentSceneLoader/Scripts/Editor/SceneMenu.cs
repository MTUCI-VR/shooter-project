using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Shared.Scripts.Editor
{
	public static class SceneMenu
	{
		#region Fields

		private static readonly string ScriptPath =
			$"{Application.dataPath}/Plugins/PersistentSceneLoader/Scripts/Editor/SceneMenuList.cs";

		private const string PersistentScenePath =
			"Assets/ShooterProject/Scenes/PersistentScene/PersistentScene.unity";

		#endregion

		#region Public Methods

		[MenuItem("Scenes/Update List", false, 0)]
		public static void UpdateList()
		{
			IEnumerable<string> scenePaths = GetScenePaths();

			GenerateScriptFile(ScriptPath, scenePaths);

			AssetDatabase.ImportAsset(ScriptPath);
		}

		#endregion

		#region Private Methods

		private static IEnumerable<string> GetScenePaths()
		{
			var scenesGUIDs = AssetDatabase.FindAssets("t:Scene");
			var scenesPaths = scenesGUIDs
				.Select(AssetDatabase.GUIDToAssetPath)
				.Where(path => path.StartsWith("Assets/ShooterProject/"));
			return scenesPaths;
		}

		private static void GenerateScriptFile(string fileName, IEnumerable<string> scenePaths)
		{
			var scriptContent = new StringBuilder();
			scriptContent.AppendLine("// This class is Auto-Generated. Don't delete it!");
			scriptContent.AppendLine("using UnityEditor;");
			scriptContent.AppendLine("using UnityEditor.SceneManagement;");
			scriptContent.AppendLine("");
			scriptContent.AppendLine("public static class SceneMenuList {");
			scriptContent.AppendLine("");

			foreach (var scenePath in scenePaths)
			{
				var sceneName = Path.GetFileNameWithoutExtension(scenePath);

				scriptContent.AppendLine($"    [MenuItem(\"Scenes/{sceneName}\")]");
				scriptContent.AppendLine($"    private static void Open{sceneName}() {{");
				scriptContent.AppendLine(
					$"        EditorSceneManager.OpenScene(\"{PersistentScenePath}\", OpenSceneMode.Single);"
				);
				scriptContent.AppendLine(
					$"        EditorSceneManager.OpenScene(\"{scenePath}\", OpenSceneMode.Additive);"
				);
				scriptContent.AppendLine("    }");
				scriptContent.AppendLine("");
			}

			scriptContent.AppendLine("");
			scriptContent.AppendLine("}");

			File.Delete(fileName);
			File.WriteAllText(fileName, scriptContent.ToString(), Encoding.UTF8);
		}

		#endregion
	}
}

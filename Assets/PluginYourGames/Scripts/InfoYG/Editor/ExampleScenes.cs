using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace YG.EditorScr
{
    public static class ExampleScenes
    {
        public static string[] sceneNames = new string[0];

        private const string FILE_NAME_SCENES_LIST = "DemoSceneNames";

        private static string FILE_PATH_SCENES_LIST =>
            $"{InfoYG.PATCH_PC_YG2}/Example/Resources/{FILE_NAME_SCENES_LIST}.txt";

        public static void LoadSceneList()
        {
            var sceneListFile = Resources.Load<TextAsset>(FILE_NAME_SCENES_LIST);
            if (sceneListFile != null)
            {
                var fileContent = sceneListFile.text;
                sceneNames = fileContent.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                sceneNames = new string[0];
            }
        }

        public static void AddScenesToBuildSettings()
        {
            RemoveScenesFromBuildSettings();

            if (!Directory.Exists($"{InfoYG.PATCH_PC_YG2}/Example"))
            {
#if RU_YG2
                EditorUtility.DisplayDialog($"Сообщение", "Демо материалы были удалены!", "Ok");
#else
                EditorUtility.DisplayDialog("Message", "The demo materials have been deleted!", "Ok");
#endif
                return;
            }

            var scenePathsToAdd = GetScenesModules();
            sceneNames = scenePathsToAdd.ToArray();

            var existingScenes = EditorBuildSettings.scenes;

            foreach (var existScene in existingScenes)
                for (var i = scenePathsToAdd.Count - 1; i >= 0; i--)
                    if (scenePathsToAdd[i] == existScene.path)
                        scenePathsToAdd.RemoveAt(i);

            var newScenes = new EditorBuildSettingsScene[scenePathsToAdd.Count + existingScenes.Length];

            for (var i = 0; i < scenePathsToAdd.Count; i++)
                newScenes[i] = new EditorBuildSettingsScene(scenePathsToAdd[i], true);

            for (var i = 0; i < existingScenes.Length; i++)
                newScenes[scenePathsToAdd.Count + i] = existingScenes[i];

            EditorBuildSettings.scenes = newScenes;

            using (var writer = new StreamWriter(FILE_PATH_SCENES_LIST, false))
            {
                for (var i = 0; i < sceneNames.Length; i++)
                {
                    var lastSlashIndex = sceneNames[i].LastIndexOf('/');
                    sceneNames[i] = sceneNames[i].Substring(lastSlashIndex + 1).Replace(".unity", string.Empty);
                    writer.WriteLine(sceneNames[i]);
                }
            }

            AssetDatabase.Refresh();
        }

        public static void RemoveScenesFromBuildSettings()
        {
            var scenePathsToRemove = GetScenesModules();

            var existingScenes = EditorBuildSettings.scenes;
            var remainingScenes = new List<EditorBuildSettingsScene>();

            foreach (var scene in existingScenes)
            {
                var contains = false;
                for (var i = 0; i < scenePathsToRemove.Count; i++)
                    if (scene.path == scenePathsToRemove[i])
                    {
                        contains = true;
                        break;
                    }

                if (!contains)
                    remainingScenes.Add(scene);
            }

            EditorBuildSettings.scenes = remainingScenes.ToArray();

            sceneNames = new string[0];
            using (var fs = new FileStream(FILE_PATH_SCENES_LIST, FileMode.Truncate))
            {
            }

            AssetDatabase.Refresh();
        }

        public static List<string> GetScenesModules()
        {
            var scenePathsToAdd = new List<string>();

            var patchExample = $"{InfoYG.PATCH_PC_YG2}/Example";

            if (!Directory.Exists(patchExample)) return scenePathsToAdd;

            {
                var directory = $"{patchExample}/Resources";
                var file = $"{directory}/DemoSceneNames.txt";

                if (!File.Exists(file))
                {
                    Directory.CreateDirectory(directory);
                    File.WriteAllText(file, string.Empty);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }

            var directories = new List<string>
            {
                $"{InfoYG.PATCH_PC_YG2}"
            };
            directories.AddRange(Directory.GetDirectories(InfoYG.PATCH_PC_MODULES).ToList());

            foreach (var directory in directories)
            {
                var dir = directory + "/Example/Scenes";
                if (!Directory.Exists(dir))
                    continue;

                var files = Directory.GetFiles(dir);
                var sceneFiles = files.Where(file =>
                    Path.GetExtension(file).Equals(".unity", StringComparison.OrdinalIgnoreCase));
                var sceneList = sceneFiles.ToList();

                for (var i = 0; i < sceneList.Count; i++)
                {
                    sceneList[i] = sceneList[i].Remove(0, Application.dataPath.Length - 6);
                    sceneList[i] = sceneList[i].Replace("\\", "/");
                }

                scenePathsToAdd.AddRange(sceneList);
            }

            return scenePathsToAdd;
        }
    }
}
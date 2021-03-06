using UnityEditor;
using static System.IO.Directory;
using static System.IO.Path;
using static UnityEngine.Application;
using static UnityEditor.AssetDatabase;

namespace Game
{
    namespace Editor
    {
        public static class ToolsMenu
        {
            [MenuItem("Tools/Setup/Create Default Folders (2D)")]
            public static void CreateDefaultFolders2D()
            {
                Dir("_Project", "Scripts", "Animations", "Art", "Fonts", "Prefabs", "Scenes", "Sound & Music", "Tests", "Tiles");
                Refresh();
            }

            // TODO: Crear carpetas para un proyecto en 3D

            public static void Dir(string root, params string[] dir)
            {
                var fullpath = Combine(dataPath, root);
                foreach (var newDirectory in dir)
                    CreateDirectory(Combine(fullpath, newDirectory));
            }
        }
    }
}

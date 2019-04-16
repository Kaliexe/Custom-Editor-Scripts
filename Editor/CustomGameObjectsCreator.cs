using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

namespace CustomEditorScripts {


	public class CustomGameObjectsCreator {


		[MenuItem("GameObject/UI/Horizontal Two Button List", false, 1)]
		static void CreateCustomGameObject(MenuCommand menuCommand) {

			GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Custom GameObjects/Horizontal Two Button List/Horizontal Two Button List.prefab", typeof(GameObject));

			// Create a custom game object
			GameObject go = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
			// Ensure it gets reparented if this was a context click (otherwise does nothing)
			GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
			// Register the creation in the undo system
			Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
			Selection.activeObject = go;
		}
	}
}

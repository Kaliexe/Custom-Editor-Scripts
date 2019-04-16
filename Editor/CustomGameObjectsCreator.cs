using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
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

			//find canvas
			Canvas canvas = (Canvas)Object.FindObjectOfType(typeof(Canvas));

			//if no canvas, make a new one and parent to gameobject
			if (canvas == null) {

				// default Canvas
				GameObject canvasGO = new GameObject();
				canvasGO.name = "Canvas";
				canvasGO.AddComponent<Canvas>();

				Canvas canvasComponent = canvasGO.GetComponent<Canvas>();
				canvasComponent.renderMode = RenderMode.ScreenSpaceOverlay;
				canvasGO.AddComponent<CanvasScaler>();
				canvasGO.AddComponent<GraphicRaycaster>();

				go.transform.SetParent(canvasGO.transform, false);

			} else {

				go.transform.SetParent(canvas.transform, false);
			}

			Selection.activeObject = go;
		}
	}
}

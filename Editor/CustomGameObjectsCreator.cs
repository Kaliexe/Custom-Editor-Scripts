using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEditor;

namespace CustomEditorScripts {


	public class CustomGameObjectsCreator {



		[MenuItem("GameObject/UI/Horizontal Two Button List", false, 10)]
		static void CreateCustomGameObject(MenuCommand menuCommand) {

			//Debug.Log("Run command");

			GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Custom GameObjects/Horizontal Two Button List/Horizontal Two Button List.prefab", typeof(GameObject));

			// Create a custom game object
			GameObject go = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
			// Ensure it gets reparented if this was a context click (otherwise does nothing)
			GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
			// Register the creation in the undo system
			Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);

			Selection.activeGameObject = menuCommand.context as GameObject;

			#region Code
			//if there is a selected object
			if (Selection.activeGameObject != null) {

				Debug.Log(Selection.activeGameObject.name);
				//Debug.Log("not null go");
				//var parentHasCanvasCheck = false;

				var activeGO = Selection.activeGameObject;

				var activeGOparent = activeGO.transform.parent;

				//check through each parent to see if they have a canvas.
				var hasCanvas = false;

				while (activeGOparent != null) {

					var parentCanvas = activeGOparent.GetComponent<Canvas>();

					//there is a canvas!
					if (parentCanvas != null) {
						hasCanvas = true;
						break;
					}
					activeGOparent = activeGOparent.parent;
				}

				//canvas in hiearchy, so attach
				if (hasCanvas) {

					go.transform.SetParent(activeGO.transform, false);

					//no canvas exist in hierarchy, so do previous check
				} else {

					//current selection has no parent
					//so just check activeGO

					var canvasCheck = activeGO.GetComponent<Canvas>();

					//so, activeGO is root, but not canvas
					if (canvasCheck == null) {

						GameObject canvasGO = CreateAndGetNewCanvas();
						canvasGO.transform.SetParent(activeGO.transform, false);
						go.transform.SetParent(canvasGO.transform, false);
					} else {

						//else, active go is root and canvas
						go.transform.SetParent(activeGO.transform, false);
					}
				}

			} else {

				//find canvas
				Canvas canvas = (Canvas)Object.FindObjectOfType(typeof(Canvas));

				//if no canvas, make a new one and parent to gameobject
				if (canvas == null) {

					// default Canvas
					GameObject canvasGO = CreateAndGetNewCanvas();
					go.transform.SetParent(canvasGO.transform, false);

				} else {

					go.transform.SetParent(canvas.transform, false);
				}

			}

			//selectionIndex++;

			//Selection.activeGameObject = null;
			#endregion
		}

		public static GameObject CreateAndGetNewCanvas() {
			// default Canvas
			GameObject canvasGO = new GameObject();
			canvasGO.name = "Canvas";
			canvasGO.AddComponent<Canvas>();

			Canvas canvasComponent = canvasGO.GetComponent<Canvas>();
			canvasComponent.renderMode = RenderMode.ScreenSpaceOverlay;
			canvasGO.AddComponent<CanvasScaler>();
			canvasGO.AddComponent<GraphicRaycaster>();

			Undo.RegisterCreatedObjectUndo(canvasGO, "Create " + canvasGO.name);
			//Selection.activeGameObject = canvasGO;
			return canvasGO;
		}

		//public static GameObject CreateAndGetHorizontalTwoButtonList(MenuCommand menuCommand) {
		//	GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Custom GameObjects/Horizontal Two Button List/Horizontal Two Button List.prefab", typeof(GameObject));

		//	// Create a custom game object
		//	GameObject go = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
		//	// Ensure it gets reparented if this was a context click (otherwise does nothing)
		//	GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
		//	// Register the creation in the undo system
		//	Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
		//	//Selection.activeGameObject = go;
		//	return go;
		//}
	}
}

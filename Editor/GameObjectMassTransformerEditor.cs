using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using UnityEditor.SceneManagement;

using UnityEngine.UI;

//https://docs.unity3d.com/ScriptReference/EditorWindow.OnGUI.html
namespace BulletSystemEditor {

	public class GameObjectMassTransformerEditor : EditorWindow {
		
		bool massTransformEnable;
		
		bool buttonClick1 = false;

		GameObject targetGameObject;
		Sprite textureImage;
		Image targetImage;

		
		// Add menu named "My Window" to the Window menu
		[MenuItem("Window/Game Object Mass Transformer Editor Window")]
		static void Init() {
			// Get existing open window or if none, make a new one:
			GameObjectMassTransformerEditor window = (GameObjectMassTransformerEditor)EditorWindow.GetWindow(typeof(GameObjectMassTransformerEditor));
			window.Show();
		}

		void OnGUI() {

			targetGameObject = Selection.activeGameObject;
			EditorGUILayout.ObjectField("Target Object", targetGameObject, typeof(GameObject), true);
			//EditorGUILayout.ObjectField = targetObject;

		

			textureImage = (Sprite)EditorGUI.ObjectField(new Rect(0, 30, 200, 200),
			"Add a Texture:",
			textureImage,
			typeof(Sprite), false);

			for (int i = 0; i < 12; i++) {
				EditorGUILayout.Space();
			}

			//with this safety button, I make sure not to hit nothing serious;
			massTransformEnable = EditorGUILayout.BeginToggleGroup("Mass Transform Safety Toggle", massTransformEnable);

			EditorGUILayout.Space();

			EditorGUILayout.Space();
			buttonClick1 = GUILayout.Button("Mass Transform Button Images");

			
			//change all button images to new image
			if (buttonClick1 && targetGameObject != null) {

				Debug.Log("Mass Transform button images");
				MassTransformButtonImages(targetGameObject);

				EditorUtility.SetDirty(targetGameObject);
				EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
				EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
			}


			EditorGUILayout.Space();


		}

		void MassTransformButtonImages(GameObject gameObject) {

			for (int i = 0; i < gameObject.transform.childCount; i++) {

				MassTransformButtonImages(gameObject.transform.GetChild(i).gameObject);
			}
			
			//is button
			if (gameObject.GetComponent<Button>() != null) {
				gameObject.GetComponent<Image>().sprite = textureImage;
			}
		}
	}
}
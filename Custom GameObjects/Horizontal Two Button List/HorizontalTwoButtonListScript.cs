using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//https://www.youtube.com/watch?v=gZUSz4Mjpeo
namespace CustomEditorScripts {

	[System.Serializable]
	public class HorizontalTwoButtonListScript : MonoBehaviour {

		private Text label;

		private int currentIndex = 0;

		private int index {

			get {

				return currentIndex;

			}

			set {

				currentIndex = value;

				label.text = labels[currentIndex];
			}

		}

		private string value {

			get {
				return labels[currentIndex];
			}
		}

		public int defaultIndex = 0;

		//convert to reorderable list later
		public List<string> labels = new List<string>();

		// Start is called before the first frame update
		void Start() {

			label = transform.Find("Label").GetComponent<Text>();
		}

		public void OnLeftClick() {

			if (index > 0) {

				index--;
			}
		}

		public void OnRightClick() {

			if (index < labels.Count -1) {

				index++;
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//https://www.youtube.com/watch?v=gZUSz4Mjpeo
namespace BulletSystemEditor {

	[System.Serializable]
	public class HorizontalTwoButtonListScript : MonoBehaviour {


		public int defaultIndex = 0;


		private Text label;
		private int currentIndex = 0;
		
		//convert to reorderable list later
		public List<string> itemList = new List<string>();

		// Start is called before the first frame update
		void Start() {

			label = transform.Find("Label").GetComponent<Text>();
			currentIndex = defaultIndex;
		}

		public void OnLeftClick() {

			if (currentIndex > 0) {

				currentIndex--;
				//currentIndex = index;
				label.text = itemList[currentIndex];
				//Debug.Log(label.text);
			}
		}

		public void OnRightClick() {

			if (currentIndex < itemList.Count -1) {

				currentIndex++;
				//currentIndex = index;
				label.text = itemList[currentIndex];
				//Debug.Log(label.text);
			}
		}
	}
}

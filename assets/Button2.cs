using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 
//using UnityEngine.UI;

public class Button2 : MonoBehaviour {

	// Use this for initialization
	void Start () {

		//Button button2 = this.GetComponent <Button1> ();
		//button2.onClick.AddListener (() => {

			//Debug.Log ("Clicked.");

		//	Application.LoadLevel ("3D4D");

		//});



	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClick() {

		//SceneManager.LoadScene ("3D4D");

		SceneManager.LoadScene ("3D4D");

		//Application.LoadLevel ("3D4D");

		//Debug.Log("Button Click: "+counter);
		//counter++;
	}


}

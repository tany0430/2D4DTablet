using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 
//using UnityEngine.UI;

public class Button1 : MonoBehaviour {

	// Use this for initialization
	void Start () {

		SceneManager.LoadScene ("2D3D");

		//Application.LoadLevel ("2D3D");

		//Button button1 = this.GetComponent <Button1> ();
		//button1.onClick.AddListener (() => {
			
			//Debug.Log ("Clicked.");

		//	Application.LoadLevel ("2D3D");

		//});

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClick() {

		SceneManager.LoadScene ("2D3D");

		//Application.LoadLevel ("2D3D");

		//Debug.Log("Button Click: "+counter);
		//counter++;
	}


}

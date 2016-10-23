using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class move2d1 : MonoBehaviour {

	Slider sliderX;

	private GameObject plate1;
	private GameObject plate2;
	private GameObject plate3;
	private GameObject plate4;
	private GameObject plate5;
	private GameObject plate6;

	private GameObject camera1;

	private float rotateZ = 0;
	private float posX = 0;

	//加速度センサーの傾き
	private Vector2 acc_vec;

	//タップ時
	float rotateAngle;


	// Use this for initialization
	void Start () {

		plate1 = GameObject.Find ("Cube1");
		plate2 = GameObject.Find ("Cube2");
		plate3 = GameObject.Find ("Cube3");

		plate4 = GameObject.Find ("Cube4");
		plate5 = GameObject.Find ("Cube5");

		plate6 = GameObject.Find ("Cube6");

		camera1 = GameObject.Find ("Main Camera");

		sliderX = GameObject.Find ("XSlider").GetComponent<Slider>() ; 


	}

	void Update () {

		if (Input.GetKey (KeyCode.F)) {
			
			camera1.transform.position = new Vector3 (camera1.transform.position.x, (Mathf.PingPong (Time.time * 1f, 0.5f)) + 3 , camera1.transform.position.z);

		} 
		else 
		
		{

			camera1.transform.position = new Vector3 (camera1.transform.position.x, camera1.transform.position.y, camera1.transform.position.z);

		}



		if (Input.GetMouseButton (0)) {
			//スライダー用

			posX = - sliderX.value;

			rotateZ = 90f * (- sliderX.value) / 4f ;



			expandDms (posX, rotateZ);

	
		} 

		else {



			//加速度を取得


			Screen.orientation = ScreenOrientation.LandscapeLeft;

			acc_vec.x += Input.acceleration.x / 8;
			//acc_vec.y += Input.acceleration.y / 8;


			posX = acc_vec.x;
			rotateZ = 90f * (posX) / 4f;


			expandDms (posX, rotateZ);


			if(Input.touchCount == 1) {
				camera1.transform.position = new Vector3 (camera1.transform.position.x, (Mathf.PingPong (Time.time * 1f, 0.5f)) + 3 , camera1.transform.position.z);
			}
				

		}

	}


	void expandDms(float posX, float rotateZ)
	{


		if (posX < 0 && posX > -4) {

			float hinge = 0.5f * Mathf.Sin (-posX / 2.5f);

			float hinge180 = 1f * Mathf.Sin (2f * (-posX) / 2.5f);


			plate1.transform.rotation = Quaternion.Euler (0f, 0f, rotateZ);

			plate1.transform.position = new Vector3 (posX - 1 - posX / 8f, hinge, 0);

			plate2.transform.rotation = Quaternion.Euler (0f, 0f, 0f);

			plate2.transform.position = new Vector3 (posX, 0, 0);

			plate3.transform.rotation = Quaternion.Euler (rotateZ, 0f, 0f);

			plate3.transform.position = new Vector3 (posX, hinge, 1f + posX / 8f);

			plate4.transform.rotation = Quaternion.Euler (0f, 0f, -rotateZ);

			plate4.transform.position = new Vector3 (posX + 1 + posX / 8f, hinge, 0);

			plate5.transform.rotation = Quaternion.Euler (-rotateZ, 0f, 0f);

			plate5.transform.position = new Vector3 (posX, hinge, -1f - posX / 8f);


			plate6.transform.rotation = Quaternion.Euler (rotateZ * 2, 0f, 0f);


			if (posX > -2) {

				plate6.transform.position = new Vector3 (posX, hinge180, 2f + posX / 2);

			} else {
				plate6.transform.position = new Vector3 (posX, 1, 2f + posX / 2);

			}
				
			camera1.transform.position = new Vector3 (-2 + posX / 8, 2.5f, -5 - posX / 2);

			camera1.transform.rotation = Quaternion.Euler (15 - rotateZ / 4, 5, 0);

		}

	}
		

}



using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;


/// <summary>
/// 立方体コントローラークラス
/// </summary>
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]





public class Cube8 : MonoBehaviour {


	Slider sliderX;

	private GameObject Cube8MH; 

	private GameObject camera1;

	private float posX = 0f;

	private float Alpha = 1f;
	float posXX ;



	//加速度センサーの傾き
	private Vector2 acc_vec;


	//タップ時
	float rotateAngle;



	/// <summary>
	/// 面のアルファ値
	/// </summary>
	//private const float ALPHA = 0.3f;
	//float ALPHA = 1;



	/// <summary>
	/// W軸関連の回転角度
	/// </summary>
	[SerializeField, Tooltip("W軸関連の回転角度")]
	public Vector3 wAngles;

	// 生成されたメッシュオブジェクト
	private Mesh mesh_;

	// メッシュ描画設定
	private MeshRenderer meshRenderer_;

	// 回転角度プロパティID
	private int cubeRotationId_;



	[SerializeField, Tooltip("X軸回転用スライダー")]
	private LabeledSliderController Slider;


	void Awake()
	{
		meshRenderer_ = GetComponent<MeshRenderer>();
		cubeRotationId_ = Shader.PropertyToID("_CubeRotation");

		Vector3[] vertices = new Vector3[]

		{

			vec(-0.5f,  0.5f , -1.5f +2),
			vec( 0.5f,  0.5f , -1.5f +2),
			vec( 0.5f, -0.5f , -1.5f +2),
			vec(-0.5f, -0.5f , -1.5f +2),

			vec(-0.5f ,  0.5f,  -0.5f +2),
			vec( 0.5f ,  0.5f,  -0.5f +2),
			vec( 0.5f , -0.5f,  -0.5f +2),
			vec(-0.5f , -0.5f,  -0.5f +2),

			vec(-0.5f,  0.5f, -1.5f +2),
			vec( 0.5f,  0.5f, -1.5f +2),
			vec( 0.5f, -0.5f, -1.5f +2),
			vec(-0.5f, -0.5f, -1.5f +2),

			vec(-0.5f,  0.5f,  -0.5f +2),
			vec( 0.5f,  0.5f,  -0.5f +2),
			vec( 0.5f, -0.5f,  -0.5f +2),
			vec(-0.5f, -0.5f,  -0.5f +2),


		};

		mesh_ = new Mesh();

		// 頂点の設定(4次元座標の3次元分だけ設定)
		mesh_.vertices = vertices.Select(v => new Vector3(v.x, v.y, v.z)).ToArray();



			// 面(三角形分割)の設定
			// 時計回り方向が法線方向
			mesh_.triangles = new int[]
			{
				// -wの立方体

				// 手前の面
				0, 1, 2,
				0, 2, 3,

				// 上面
				0, 4, 5,
				0, 5, 1,

				// 下面
				3, 6, 7,
				3, 2, 6,

				// 左面
				0, 7, 4,
				0, 3, 7,

				// 右面
				1, 5, 6,
				1, 6, 2,

				// 奥の面
				4, 7, 6,
				4, 6, 5,

				// +wの立方体

				// 手前の面
				8, 9, 10,
				8, 10, 11,

				// 上面
				8, 12, 13,
				8, 13, 9,

				// 下面
				11, 14, 15,
				11, 10, 14,

				// 左面
				8, 15, 12,
				8, 11, 15,

				// 右面
				9, 13, 14,
				9, 14, 10,

				// 奥の面
				12, 15, 14,
				12, 14, 13,

				// 2つの立方体をつなぐ-y側の面

				// 手前の面
				11, 10, 2,
				11, 2, 3,

				// 右の面
				10, 14, 6,
				10, 6, 2,

				// 左の面
				11, 3, 7,
				11, 7, 15,

				// 奥の面
				14, 15, 7,
				14, 7, 6,

				// 2つの立方体をつなぐ+y側の面

				// 手前の面
				0, 1, 9,
				1, 9, 8,

				// 右の面
				1, 5, 13,
				1, 13, 9,

				// 左の面
				0, 12, 4,
				0, 8, 12,

				// 奥の面
				5, 4, 12,
				5, 12, 13,

				// 2つの立方体をつなぐ縦の面

				// 左前の面
				0, 8, 11,
				0, 11, 3,

				// 右前の面
				1, 2, 10,
				1, 10, 9,

				// 左後の面
				4, 7, 15,
				4, 15, 12,

				// 右後の面
				5, 13, 14,
				5, 14, 6,
			};

			mesh_.RecalculateNormals();

		mesh_.RecalculateBounds();
	}



	// Use this for initialization
	void Start () {


		camera1 = GameObject.Find ("Main Camera");

		sliderX = GameObject.Find ("Slider").GetComponent<Slider>() ; 


		gameObject.AddComponent<MeshFilter>().mesh = mesh_;

		// ダミーオブジェクトを非表示にする
		transform.FindChild("Dummy").gameObject.SetActive(false);



		Cube8MH = GameObject.Find ("Cube8Mesh");
	
	}
	
	// Update is called once per frame
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

			camera1.transform.position = new Vector3 (camera1.transform.position.x, (Mathf.PingPong (Time.time * 3f, 0.5f)) + 3 , camera1.transform.position.z);


		} else {

			camera1.transform.position = new Vector3 (camera1.transform.position.x, camera1.transform.position.y, camera1.transform.position.z);

			//加速度を取得

			Screen.orientation = ScreenOrientation.LandscapeLeft;


			if (acc_vec.x >= -2 && acc_vec.x <= 4) {

				acc_vec.x += Input.acceleration.x / 8;

			}

			if (acc_vec.x < -2) {

				acc_vec.x -= Input.acceleration.x / 8;

			}

			if (acc_vec.x > 4) {

				acc_vec.x -= Input.acceleration.x / 8;

			}

			//acc_vec.y += Input.acceleration.y / 8;

			posX = acc_vec.x / 2;


			if (acc_vec != null) {

				transform.position = acc_vec;

			}



			if (Input.touchCount == 1) {
				camera1.transform.position = new Vector3 (camera1.transform.position.x, (Mathf.PingPong (Time.time * 1f, 0.5f)) + 3, camera1.transform.position.z);
			}



		}

		if (acc_vec.x >= 0 && acc_vec.x <= 2) {

			posX = acc_vec.x/2;

		} 
		else if (acc_vec.x > 2) {
			posX = 1;

		}
		else if(acc_vec.x < 0)
		{
			posX = 0;

		}


		Alpha = 1 - posX * 0.9f ;

		Cb3D (posX);

		Cube8MH.gameObject.transform.eulerAngles = new Vector3(posX, posX, posX) * 90.0f;

		meshRenderer_.material.SetVector(cubeRotationId_, wAngles);


	}



	/// <summary>
	/// Vector3を生成する。
	/// </summary>
	/// <param name="x">X座標</param>
	/// <param name="y">Y座標</param>
	/// <param name="z">Z座標</param>
	/// <returns>Vector3</returns>
	private static Vector3 vec(float x, float y, float z)
	{

		return new Vector3(x , y, z);
	}

	public Vector3 vecXY(float x, float y, float z)
	{

		return new Vector3(x + posX  , y + posX, z);

	}
	public Vector3 vecXmY(float x, float y, float z)
	{

		return new Vector3(x - posX  , y + posX, z);

	}
	public Vector3 vecXYm(float x, float y, float z)
	{

		return new Vector3(x + posX  , y - posX, z);

	}
	public Vector3 vecXmYm(float x, float y, float z)
	{

		return new Vector3(x - posX  , y - posX, z);

	}

	/// <summary>
	/// Vector2を生成する。
	/// </summary>
	/// <param name="x">X座標</param>
	/// <param name="y">Y座標</param>
	/// <returns>Vector2</returns>
	private static Vector2 vec(float x, float y)
	{
		return new Vector2(x, y);
	}

	/// <summary>
	/// Colorを生成する。各成分の値は[0.0f, 1.0f]
	/// </summary>
	/// <param name="r">赤成分</param>
	/// <param name="g">緑成分</param>
	/// <param name="b">青成分</param>
	/// <returns></returns>
	private static Color rgb(float r, float g, float b)
	{
		return rgba(r, g, b, 1.0f);
	}

	/// <summary>
	/// Colorを生成する。各成分の値は[0.0f, 1.0f]
	/// </summary>
	/// <param name="r">赤成分</param>
	/// <param name="g">緑成分</param>
	/// <param name="b">青成分</param>
	/// <param name="a">アルファ成分</param>
	/// <returns></returns>
	private static Color rgba(float r, float g, float b, float a)
	{
		return new Color(r, g, b, a);
	}


	//public void CbColor(float ALPHA)
	public void CbColor(float ALPHA)
	{


		// 頂点色の設定
		mesh_.colors = new Color[]
		{
			rgba(0.5f, 0.5f, 0.5f, 1),
			rgba(0.5f, 0.5f, 0.5f, 1),
			rgba(0.5f, 0.5f, 0.5f, 1),
			rgba(0.5f, 0.5f, 0.5f, 1),

			rgba(0.5f, 0.5f, 0.5f, 1),
			rgba(0.5f, 0.5f, 0.5f, 1),
			rgba(0.5f, 0.5f, 0.5f, 1),
			rgba(0.5f, 0.5f, 0.5f, 1),

			rgba(0.5f, 0.5f, 0.5f, 1),
			rgba(0.5f, 0.5f, 0.5f, 1),
			rgba(0.5f, 0.5f, 0.5f, 1),
			rgba(0.5f, 0.5f, 0.5f, 1),

			rgba(0.5f, 0.5f, 0.5f, 1),
			rgba(0.5f, 0.5f, 0.5f, 1),
			rgba(0.5f, 0.5f, 0.5f, 1),
			rgba(0.5f, 0.5f, 0.5f, 1),



		};

	}


	public void Cb3D(float posi)
	{

		float posiMv;

		if(posi <=2)
		{
			posiMv = posi*2;

		}
		else
		{
			posiMv = 2; 
		}

		mesh_.vertices = new Vector3[]

		{
			
			vec(-0.5f ,  0.5f , -0.5f +2 - posiMv),
			vec( 0.5f ,  0.5f , -0.5f +2 - posiMv),
			vec( 0.5f , -0.5f  , -0.5f +2 - posiMv),
			vec(-0.5f , -0.5f  , -0.5f +2 - posiMv),


			// 奥の面-w
			vec(-0.5f ,  0.5f ,  0.5f +2 - posiMv),
			vec( 0.5f ,  0.5f ,  0.5f +2- posiMv),
			vec( 0.5f , -0.5f ,  0.5f +2- posiMv),
			vec(-0.5f , -0.5f ,  0.5f +2- posiMv),

			// 手前の面+w
			vec(-0.5f,  0.5f, -0.5f +2- posiMv),
			vec( 0.5f,  0.5f, -0.5f +2- posiMv),
			vec( 0.5f, -0.5f, -0.5f +2- posiMv),
			vec(-0.5f, -0.5f, -0.5f +2- posiMv),

			// 奥の面+w
			vec(-0.5f,  0.5f,  0.5f +2- posiMv),
			vec( 0.5f,  0.5f,  0.5f +2- posiMv),
			vec( 0.5f, -0.5f,  0.5f +2- posiMv),
			vec(-0.5f, -0.5f,  0.5f +2- posiMv),

		};


	}






}

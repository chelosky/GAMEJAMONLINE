using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scOperatorController : MonoBehaviour
{
    private Camera magnifyCamera;
    private float MGOX, MG0Y; // Magnify Glass Origin X and Y position
    private float MGWidth = Screen.width / 5f, MGHeight = Screen.width / 5f; // Magnify glass width and height
    private Vector3 mousePos;
	private bool cameraActive;

	public static scOperatorController instance;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy(this);
		}
	}

	void Start()
    {
		cameraActive = false;
		createMagnifyGlass();
    }

	public void UpdateStateCamera(bool state)
    {
		cameraActive = state;
		magnifyCamera.enabled = cameraActive;

	}

    // Update is called once per frame
    void Update()
    {
		// Following lines set the camera's pixelRect and camera position at mouse position
		magnifyCamera.pixelRect = new Rect(Input.mousePosition.x - MGWidth / 2.0f, Input.mousePosition.y - MGHeight / 2.0f, MGWidth, MGHeight);
		mousePos = getWorldPosition(Input.mousePosition);
		magnifyCamera.transform.position = mousePos;
		mousePos.z = 0;
		//magnifyBorders.transform.position = mousePos;
	}

	// Following method creates MagnifyGlass
	private void createMagnifyGlass()
	{
		//GameObject camera = new GameObject("OperatorCamera");
		MGOX = Screen.width / 2f - MGWidth / 2f;
		MG0Y = Screen.height / 2f - MGHeight / 2f;
		magnifyCamera = GetComponent<Camera>();
		magnifyCamera.pixelRect = new Rect(MGOX, MG0Y, MGWidth, MGHeight);
		magnifyCamera.transform.position = new Vector3(0, 0, 0);
		if (Camera.main.orthographic)
		{
			magnifyCamera.orthographic = true;
			magnifyCamera.orthographicSize = Camera.main.orthographicSize / 10.0f;//+ 1.0f;
																				  //createBordersForMagniyGlass ();
		}
		else
		{
			magnifyCamera.orthographic = false;
			magnifyCamera.fieldOfView = Camera.main.fieldOfView / 10.0f;//3.0f;
		}
	}

	// Following method calculates world's point from screen point as per camera's projection type
	public Vector3 getWorldPosition(Vector3 screenPos)
	{
		Vector3 worldPos;
		if (Camera.main.orthographic)
		{
			worldPos = Camera.main.ScreenToWorldPoint(screenPos);
			worldPos.z = Camera.main.transform.position.z;
		}
		else
		{
			worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Camera.main.transform.position.z));
			worldPos.x *= -1;
			worldPos.y *= -1;
		}
		return worldPos;
	}
}

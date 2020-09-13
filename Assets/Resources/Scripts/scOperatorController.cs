using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scOperatorController : MonoBehaviour
{
    private Camera magnifyCamera;
    private float MGOX, MG0Y; 
    private float MGWidth = Screen.width / 5f, MGHeight = Screen.width / 5f;
    private Vector3 mousePos;
	private bool cameraActive;
	private bool canShoot;

	public static scOperatorController instance;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			//DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy(this);
		}
	}

	void Start()
    {
		canShoot = true;
		cameraActive = false;
		createMagnifyGlass();
    }

	public void UpdateStateCamera(bool state)
    {
		cameraActive = state;
        if (cameraActive)
        {
			canShoot = true;
        }
		magnifyCamera.enabled = cameraActive;

	}

    void Update()
    {
		magnifyCamera.pixelRect = new Rect(Input.mousePosition.x - MGWidth / 2.0f, Input.mousePosition.y - MGHeight / 2.0f, MGWidth, MGHeight);
		mousePos = getWorldPosition(Input.mousePosition);
		magnifyCamera.transform.position = mousePos;
		mousePos.z = 0;
		if (Input.GetMouseButtonDown(0) && canShoot && scGameManager.instance.stateGame == 1)
		{
			canShoot = false;
			scGameManager.instance.PlayASound("fire");
			Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

			RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2D, Vector2.zero);
			for (int i=0; i < hits.Length; i++)
            {
                if (hits[i].transform.gameObject.tag == "Person")
                {
					scGameManager.instance.PlayASound("death");
					hits[i].transform.GetComponent<scRandomGenerateCharacter>().DeathPerson();
                    if (hits[i].transform.gameObject.GetComponent<scRandomGenerateCharacter>().GetPerson().esObjetivo)
                    {
						scGameManager.instance.stateGame = 2;
                    }
                    else
                    {
						scGameManager.instance.stateGame = 3;
					}
					break;
				}
			}
			StartCoroutine("Reload");
		}
	}

	IEnumerator Reload()
    {
		yield return new WaitForSeconds(1f);
		canShoot = true;
	}

	// Following method creates MagnifyGlass
	private void createMagnifyGlass()
	{
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

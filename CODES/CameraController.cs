using UnityEngine;

public class CameraController : MonoBehaviour {

	public float panSpeed = 30f;
	public float panBorderThickness = 10f;
	public float cameraSpeed = 0.02f;
	public float scrollSpeed = 5f;
	public float minY = 30f;
	public float maxY = 80f;
	private float minX = 12.5f;
	private float maxX = 97.5f;
	private float minZ = -62.5f;
	private float maxZ = -7.5f;


	// Update is called once per frame
	void Update () {

		if (GameManager.GameIsOver)
		{
			this.enabled = false;
			return;
		}

		float scroll = Input.GetAxis("Mouse ScrollWheel");

		Vector3 pos = transform.position;

		pos.y -= scroll * 1000 * scrollSpeed * 0.005f;
		pos.y = Mathf.Clamp(pos.y, minY, maxY);
		
		//updateXs(pos.y);
		//updateZs(pos.y);
		//pos.x = Mathf.Clamp(pos.x, minX, maxX);
		//pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

		transform.position = pos;

		if (Input.GetKey("w"))
		{
			pos = transform.position;
			pos.z += 1 * panSpeed * cameraSpeed;
			pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
			transform.position = pos;
			//transform.Translate(Vector3.forward * panSpeed * cameraSpeed, Space.World);
		}
		if (Input.GetKey("s"))
		{
			pos = transform.position;
			pos.z -= 1 * panSpeed * cameraSpeed;
			pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
			transform.position = pos;
			//transform.Translate(Vector3.back * panSpeed * cameraSpeed, Space.World);
			
		}
		if (Input.GetKey("d"))
		{
			pos = transform.position;
			pos.x += 1 * panSpeed * cameraSpeed;
			pos.x = Mathf.Clamp(pos.x, minX, maxX);
			transform.position = pos;
			//transform.Translate(Vector3.right * panSpeed * cameraSpeed, Space.World);
		}
		if (Input.GetKey("a"))
		{
			pos = transform.position;
			pos.x -= 1 * panSpeed * cameraSpeed;
			pos.x = Mathf.Clamp(pos.x, minX, maxX);
			transform.position = pos;
			//transform.Translate(Vector3.left * panSpeed * cameraSpeed, Space.World);
		}

		//Input.mousePosition.x/y <= panBorderThickness
	}

	void updateXs(float y)
	{
		minX = y - 30;
		maxX = minX + (10*((85 - y)/5));
	}

	void updateZs(float y)
	{
		minZ = -22.5f + (2.5f*((80-y)/5));
		maxZ = minZ- (5*((105-y)/5));
	}
}


/*
xm = y-30
XM = y-30 + (10*((85 - y)/5))
zm = -22.5 + (2.5*((80-y)/5))
zm = -22.5 + ((10/3)*((80-y)/5))
ZM = zm + (5*((105-y)/5))



y = 80 xm = 50 XM = 60 zm = -22.5 ZM = -47.5
y = 75 xm = 45 XM = 65 zm = -20 ZM = -50
y = 70 xm = 40 XM = 70 zm = -17.5 ZM = -52.5
y = 65 xm = 35 XM = 75 zm = -15


y = 30 xm = 0 XM = 110 zm = 2.5 ZM = -72.5

*/

/*
xm = y 
XM = y + (10*((55 - y)/5))

((55 - y)/5)



y = 50 xm = 50 XM = 60 
y = 45 xm = 45 XM = 65
y = 40 xm = 40 XM = 70
y = 35
y = 30 xm = 30 XM = 80




*/
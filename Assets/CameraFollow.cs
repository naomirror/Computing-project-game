using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;

	public float smoothSpeed = 0.125f;

	public Vector3 cameraOffset;

	void FixedUpdate ()
	{
		//Vector3 desiredPosition = target.position + cameraOffset;
		//Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		//transform.position = smoothedPosition;
		//transform.eulerAngles = target.eulerAngles;
		//transform.rotation = target.transform.rotation;
		//transform.RotateAround (target.transform.position, Vector3.up, target.rotation.y);
		transform.LookAt (target);
	}
}

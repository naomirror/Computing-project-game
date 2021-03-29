using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
	public GameObject backLeftWheel, backRightWheel, frontLeftWheel, frontRightWheel;
    WheelCollider blWheel, brWheel,flWheel,frWheel;
	public Rigidbody car;
	// Start is called before the first frame update
	public float torque, brakeTorque, maxSpeed, highSpeedSteeringAngle, lowSpeedSteeringAngle;

	public Vector3 resetPosition;
	public Quaternion resetRotation;
    void Start()
    {
		car.centerOfMass = new Vector3 (0.0f, -0.75f, .35f);
		blWheel = backLeftWheel.GetComponent<WheelCollider> ();
		brWheel = backRightWheel.GetComponent<WheelCollider> ();
		flWheel = frontLeftWheel.GetComponent<WheelCollider>();
		frWheel = frontRightWheel.GetComponent<WheelCollider> ();
    }

    // Update is called once per frame
    void Update()
    {
		float carVelocity = this.gameObject.GetComponent<Rigidbody> ().velocity.magnitude;
		if (Input.GetAxis ("Vertical") > 0 && carVelocity < maxSpeed) {
			blWheel.motorTorque = torque;
			brWheel.motorTorque = torque;
		} else if (Input.GetAxis ("Vertical") < 0 && carVelocity > 0) {
			blWheel.brakeTorque = brakeTorque;
			brWheel.brakeTorque = brakeTorque;
			flWheel.brakeTorque = brakeTorque;
			frWheel.brakeTorque = brakeTorque;
		} 
		else
		{
			blWheel.brakeTorque = 0;
			brWheel.brakeTorque = 0;
			flWheel.brakeTorque = 0;
			frWheel.brakeTorque = 0;
			blWheel.motorTorque = 0;
			brWheel.motorTorque = 0;
		}
		if (Input.GetAxis ("Horizontal") != 0) {
			flWheel.steerAngle = lowSpeedSteeringAngle*Input.GetAxis ("Horizontal");
			frWheel.steerAngle = lowSpeedSteeringAngle*Input.GetAxis ("Horizontal");
		} 

		if (this.transform.position.y < -1f) {
			resetCar ();
		}
	}

	void resetCar(){
		this.GetComponent<Rigidbody>().isKinematic = true;
		this.transform.position = resetPosition;
		this.transform.rotation = resetRotation;
		this.GetComponent<Rigidbody>().isKinematic = false;
	}

	void OnCollisionEnter(Collision collision){
		Debug.Log ("hit " + collision.transform.name);
		collision.transform.gameObject.tag = "InContact";

		//Doesn't account for left hand turns flipping the parent 180degrees so car lands upside down. 
		resetPosition = collision.transform.parent.position;
		resetPosition.y = resetPosition.y + 1f;
		resetRotation = collision.transform.parent.rotation* Quaternion.Euler(0f,-90f,0f);

	}

	void OnCollisionExit(Collision collision){
		Debug.Log ("No longer in contact with "+ collision.transform.gameObject.name);

		collision.transform.gameObject.tag = "FinishedContact";
	}
}

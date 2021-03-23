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
		} 
			/*NOT WORKING FOR REVERSING
			 * else if (Input.GetAxis ("Vertical") < 0 && carVelocity <= 0){
			blWheel.motorTorque = -torque;
			brWheel.motorTorque = -torque;
			}
			*/
		else
		{
			blWheel.brakeTorque = 0;
			brWheel.brakeTorque = 0;
			blWheel.motorTorque = 0;
			brWheel.motorTorque = 0;
		}
		if (Input.GetAxis ("Horizontal") != 0) {
			flWheel.steerAngle = lowSpeedSteeringAngle*Input.GetAxis ("Horizontal");
			frWheel.steerAngle = lowSpeedSteeringAngle*Input.GetAxis ("Horizontal");
		} 
	}
	void OnCollisionEnter(Collision collision){
		Debug.Log ("hit");
		if (collision.gameObject.name == "road") {
			Debug.Log ("hit the road");
		}
	}
}

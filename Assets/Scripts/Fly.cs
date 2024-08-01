using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Fly : MonoBehaviour
{

	public enum OpMode
	{
		Off,
		EditorOnly,
		AlwaysOn
	}
	public OpMode opMode = OpMode.EditorOnly;

    public GameObject head;
	public GameObject rightHand;

    public float flySpeed = 0.5f;
	public float flyDoubleSpeed = 2f;
	private bool isFlying = false;
	private bool isFlyingDouble = false;


	void Update()
    {
		if (IsActivated())
		{
			CheckIfFlying();
			FlyIfFliying(); 
		}
		
    }

	private void CheckIfFlying()
	{
		Debug.Log("Pressing + " + OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch));
		
		 if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
		{
			Debug.Log("Is flying");
			isFlying = true;
		}
		if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) && isFlying)
		{
			Debug.Log("Is flying double");
			isFlyingDouble = true;

		}

	   if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) && isFlyingDouble)
		{
			isFlyingDouble = false;
		}

		else if(OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.RTouch))
		{
			isFlying = false;
			isFlyingDouble = false;
		}
	}

	private void FlyIfFliying(){
		if (isFlyingDouble)
		{
			Vector3 flyDirection = rightHand.transform.forward;
			transform.position += flyDirection * flyDoubleSpeed * Time.deltaTime;
		}
		else if (isFlying){
			Vector3 flyDirection = rightHand.transform.forward;
			transform.position += flyDirection * flySpeed * Time.deltaTime;
		}
	}

	bool IsActivated()
	{
		if (opMode == OpMode.Off)
		{
			return false;
		}
		else if (opMode == OpMode.EditorOnly && !Application.isEditor)
		{
			return false;
		}
		
		return true;
	}
}

using UnityEngine;
using System.Collections;

public class CorePlayer : MonoBehaviour
{

    private HFTInput input;

    private Material material;
    private float maxGrav = 1;

    private Vector3 Joystick
    {
        get { return new Vector3(input.GetAxis("Horizontal"), -input.GetAxis("Vertical")); }
    }

	// Use this for initialization
	void Start ()
	{
	    input = GetComponent<HFTInput>();
	    material = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    transform.position += Joystick * Time.deltaTime;
        material.color = new Color((input.gyro.userAcceleration.magnitude) / maxGrav, 0, 0, 1);

	    if (input.gyro.userAcceleration.magnitude > maxGrav)
	    {
	        maxGrav = input.gyro.userAcceleration.magnitude;
	    }
	}
}

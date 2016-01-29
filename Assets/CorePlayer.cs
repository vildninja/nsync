using UnityEngine;
using System.Collections;

public class CorePlayer : MonoBehaviour
{

    private HFTInput input;

    private Vector3 Joystick
    {
        get { return new Vector3(input.GetAxis("Horizontal"), input.GetAxis("Vertical")); }
    }

	// Use this for initialization
	void Start ()
	{
	    input = GetComponent<HFTInput>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    transform.position += Joystick * Time.deltaTime;
	}
}

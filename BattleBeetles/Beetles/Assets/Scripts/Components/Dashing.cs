using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour {


    float axisX = 0;
    float axisY = 0;

    [Header("Movement Parameters")]
    public float dashPower;
    public float dashTime;
    public float dashCooldown;
    public bool justDashed = false;
    public bool isDashing = false;
    public bool canDash = true;
    public ParticleSystem parts;

    
    void Start () {
        parts.Stop();
    }
	
	// Update is called once per frame
	void Update () {

        // update the axis values for use with analog controls
        UpdateAxes();
        //Check the direction of the moving player and if they press the right bumper to dash
        checkInput();
    }

    void UpdateAxes()
    {
        if (GetComponent<BasePlayer>().isPlayer1)
        {
            axisX = Input.GetAxis("HorizontalLeft");
            axisY = Input.GetAxis("VerticalLeft");
        }
        else
        {
            axisX = Input.GetAxis("HorizontalLeft2");
            axisY = Input.GetAxis("VerticalLeft2");
        }
    }

    void checkInput()
    {
        if (Input.GetButtonDown(GetComponent<InputStorage>().getFromInputStorage("Dash", GetComponent<BasePlayer>().isPlayer1)) && !justDashed && canDash && GetComponent<BasePlayer>().canMove)
        {
            // Quaternion.Euler found here:
            //http://answers.unity3d.com/questions/46770/rotate-a-vector3-direction.html

            isDashing = true;
            parts.Play();

            Vector3 tmp = new Vector3(axisX, 0, axisY).normalized;
            float angle = Vector3.Angle(Vector3.forward, transform.forward);
            //Debug.Log(angle);
            if (Vector3.Angle(Vector3.right, transform.forward) >= Vector3.Angle(Vector3.left, transform.forward))
            {
                tmp = Quaternion.Euler(0, angle * -1, 0) * tmp;
                Debug.Log("Tmp made");
            }
            else
            {
                tmp = Quaternion.Euler(0, angle, 0) * tmp;
            }

            GetComponent<Rigidbody>().velocity = new Vector3(tmp.x * dashPower, GetComponent<Rigidbody>().velocity.y, tmp.z * dashPower);

            justDashed = true;
            Invoke("StopDash", dashTime);
            Invoke("resetDash", dashCooldown);
        }
    }

    void StopDash()
    {
        isDashing = false;
        parts.Stop();
    }

    void resetDash()
    {
        justDashed = false;
    }

    //float SignedAngleBetween(Vector3 a, Vector3 b, Vector3 n)
    //{
    //    // angle in [0,180]
    //    float angle = Vector3.Angle(a, b);
    //    float sign = Mathf.Sign(Vector3.Dot(n, Vector3.Cross(a, b)));

    //    // angle in [-179,180]
    //    float signed_angle = angle * sign;

    //    // angle in [0,360] (not used but included here for completeness)
    //    float angle360 = (signed_angle + 180) % 360;

    //    return signed_angle;
    //}
}

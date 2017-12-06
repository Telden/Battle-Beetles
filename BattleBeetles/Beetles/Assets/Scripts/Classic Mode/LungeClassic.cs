using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LungeClassic : MonoBehaviour {

    float axisX = 0;
    float axisY = 0;

    [Header("Hitbox Used")]
    public GameObject attackHitbox;

    [Header("Damage Dealt")]
    public int damageDealt;

    [Header("Movement Parameters")]
    public float dashPower;
    public float dashTime;
    public float dashCooldown;
    public bool justDashed = false;
    public bool isDashing = false;
    public bool canDash = true;
    public ParticleSystem parts;

    //[Header("Lag")]
    //public float endLag;

	[Header("AudioClips")]
    public AudioClip audioCharge;

    private bool justHit = false;


    void Start()
    {
        // attackHitbox.enabled = false;
        attackHitbox.GetComponent<Collider>().enabled = false;
        attackHitbox.GetComponent<Renderer>().enabled = false;
        parts.Stop();
    }

    // Update is called once per frame
    void Update()
    {

        // update the axis values for use with analog controls
        UpdateAxes();
        //Check the direction of the moving player and if they press the right bumper to dash
        checkInput();
    }

    void UpdateAxes()
    {
        axisX = Input.GetAxis("HorizontalLeft");
        axisY = Input.GetAxis("VerticalLeft");
    }

    void checkInput()
    {
        if (Input.GetAxis("MouseScrollWheel") > 0 && !justDashed && canDash && GetComponent<BaseClassic>().canMove)
        {
			GameObject.Find("AudioManager").GetComponent<AudioManager>().PlaySound(audioCharge, 0.1f);

			// if the hover component exists, stop hovering while attacking
			if (GetComponent<Hover>() != null && GetComponent<Hover>().isHovering)
			{
				GetComponent<Hover>().isHovering = false;
				GetComponent<Hover>().parts.Stop();
			}

            // Quaternion.Euler found here:
            gameObject.GetComponent<LockOn>().lockOnEnabled = false;
            //http://answers.unity3d.com/questions/46770/rotate-a-vector3-direction.html

            isDashing = true;
            attackHitbox.GetComponent<Collider>().enabled = true;
            attackHitbox.GetComponent<Renderer>().enabled = true;
            parts.Play();
            //Dash Forwards
            GetComponent<Rigidbody>().velocity = (transform.forward * dashPower);
            justDashed = true;
            Invoke("StopDash", dashTime);
            Invoke("resetDash", dashCooldown);
        }
    }

    void StopDash()
    {
        isDashing = false;
        attackHitbox.GetComponent<Collider>().enabled = false;
        attackHitbox.GetComponent<Renderer>().enabled = false;
        parts.Stop();
        gameObject.GetComponent<LockOn>().lockOnEnabled = true;

        //GetComponent<BasePlayer>().applyEndLag(endLag);
    }

    void resetDash()
    {
        justDashed = false;
        canDash = true;
    }

    void OnTriggerEnter(Collider hitbox)
    {
        if (hitbox.name == "DashAttack" && !justHit)
        {
        	justHit = true;
			Invoke("reenableHit", 0.1f);

            gameObject.GetComponent<BasePlayer>().applyKnocback();
            gameObject.GetComponent<BasePlayer>().playerHealth -= damageDealt;
            if (gameObject.GetComponent<BasePlayer>().isPlayer1)
            {
                GameObject.Find("Player2").GetComponent<BasePlayer>().applyKnocback();
            }
            else
            {
                GameObject.Find("Player1").GetComponent<BasePlayer>().applyKnocback();
            }
        }
    }

    void reenableHit()
    {
    	justHit = false;
    }
}

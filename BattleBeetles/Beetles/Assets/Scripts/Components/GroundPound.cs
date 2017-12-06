using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPound : MonoBehaviour
{

    [Header("Ground Pound Parameters")]
    public float groundPoundPower;

    [Header("Hitbox Used")]
    public Collider groundPoundHitbox;

    [Header("End Lag")]
    public float endLag;

    [Header("Damage Dealt")]
    public int damageDealt;

    public bool isAttacking;
    public bool canGroundPound;
    public ParticleSystem parts;
    public float particleTimer = 3;

    private bool canGetHit = true;
    bool isknockedback = false;
    
    void Start()
    {
		groundPoundHitbox.GetComponent<Collider>().enabled = false;
        groundPoundHitbox.GetComponent<Renderer>().enabled = false;

        isAttacking = false;
        canGroundPound = true;
        parts.Stop();
    }

    void Update()
    {
        //Check if the ground-pound button is pressed
        checkInput();
        //Movement
        groundPoundAttack();
        //Check Ground
        checkGround();

        if (canGetHit == false)
        {
        	Invoke("enableHitting", 0.1f);
        }
    }

    void checkInput()
    {
        if (Input.GetButtonDown(GetComponent<InputStorage>().getFromInputStorage("AttackSecondary", GetComponent<BasePlayer>().isPlayer1)) && !isAttacking && !GetComponent<BasePlayer>().grounded && canGroundPound)
        {
            isAttacking = true;
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

			// if the hover component exists, stop hovering while ground pounding
            if (GetComponent<Hover>() != null && GetComponent<Hover>().isHovering)
            {
				GetComponent<Hover>().isHovering = false;
				GetComponent<Hover>().parts.Stop();
            }
        }
    }

    void groundPoundAttack()
    {
        if (isAttacking)
        {
        	// apply force
            GetComponent<Rigidbody>().AddForce(new Vector3(0, groundPoundPower * -1, 0));

            // enable the hitbox
			groundPoundHitbox.GetComponent<Collider>().enabled = true;
            groundPoundHitbox.GetComponent<Renderer>().enabled = true;
        }
    }

    void checkGround()
    {
        if (GetComponent<BasePlayer>().grounded && isAttacking)
        {
        	// disable the hitbox
			groundPoundHitbox.GetComponent<Collider>().enabled = false;
            groundPoundHitbox.GetComponent<Renderer>().enabled = false;

			GetComponent<BasePlayer>().applyEndLag(endLag);

            isAttacking = false;
            parts.Play();
            Invoke("stopParticles", particleTimer);
        }
    }

    void stopParticles()
    {
        parts.Stop();
    }

	void OnTriggerEnter(Collider hitbox)
    {
        if (hitbox.name == "GroundPound" && canGetHit)
        {
            //Debug.Log(" dash attack");
            gameObject.GetComponent<BasePlayer>().applyKnocback();
            gameObject.GetComponent<BasePlayer>().playerHealth -= damageDealt;
            canGetHit = false;

            if (gameObject.GetComponent<BasePlayer>().isPlayer1)
            {
            	Debug.Log("p2 just hit " + gameObject.name);
                GameObject.Find("Player2").GetComponent<GroundPound>().isAttacking = false;
                GameObject.Find("Player2").GetComponent<GroundPound>().groundPoundHitbox.GetComponent<Collider>().enabled = false;
                GameObject.Find("Player2").GetComponent<GroundPound>().groundPoundHitbox.GetComponent<Renderer>().enabled = false;
                GameObject.Find("Player2").GetComponent<BasePlayer>().applyKnocback();
				//GameObject.Find("Player2").GetComponent<GroundPound>().groundPoundHitbox.GetComponent<Collider>().enabled = false;
				//GameObject.Find("Player2").GetComponent<GroundPound>().groundPoundHitbox.GetComponent<Renderer>().enabled = false;
			}
            else
            {
				Debug.Log("p1 just hit " + gameObject.name);
                GameObject.Find("Player1").GetComponent<GroundPound>().isAttacking = false;
                GameObject.Find("Player1").GetComponent<GroundPound>().groundPoundHitbox.GetComponent<Collider>().enabled = false;
                GameObject.Find("Player1").GetComponent<GroundPound>().groundPoundHitbox.GetComponent<Renderer>().enabled = false;
                GameObject.Find("Player1").GetComponent<BasePlayer>().applyKnocback();
				//GameObject.Find("Player1").GetComponent<GroundPound>().groundPoundHitbox.GetComponent<Collider>().enabled = false;
				//GameObject.Find("Player1").GetComponent<GroundPound>().groundPoundHitbox.GetComponent<Renderer>().enabled = false;
			}

        }
    }

    void enableHitting()
    {
    	canGetHit = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonicBoom : MonoBehaviour {

    [Header("Hitbox Used")]
    public GameObject attackHitbox;

    [Header("Damage Dealt")]
    public int damageDealt;

    [Header("Attack Bools")]
    bool isAttacking = false;

    [Header("Attack Bools")]
    public float hitboxRadius;
    public float radiusGrowth;
    public float maximumRadius;


    // Use this for initialization
    void Start () {
        //Set hitbox off until user input specifies its activation
        attackHitbox.GetComponent<Collider>().enabled = false;
        attackHitbox.GetComponent<Renderer>().enabled = false;
        hitboxRadius = attackHitbox.GetComponent<SphereCollider>().radius;
    }
	
	// Update is called once per frame
	void Update () {
        checkInput();
        updateAttack();

	}

    void checkInput()
    {
        if (Input.GetButtonDown(GetComponent<InputStorage>().getFromInputStorage("AttackPrimary", GetComponent<BasePlayer>().isPlayer1)) && GetComponent<BasePlayer>().canMove)
        {
           // Debug.Log("SonicBoom Input");
            GetComponent<BasePlayer>().canMove = false;
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            isAttacking = true;
        }
    }

    //If the player is attacking, increase the size of the hitbox radius until it reaches maximum size
    void updateAttack()
    {
        if(isAttacking)
        {
            if(attackHitbox.GetComponent<SphereCollider>().radius > maximumRadius)
            {
                //Reset the bools
                Debug.Log("Resetting Bools");
                attackHitbox.GetComponent<Collider>().enabled = false;
                attackHitbox.GetComponent<Renderer>().enabled = false;
                attackHitbox.GetComponent<SphereCollider>().radius = hitboxRadius;
                GetComponent<BasePlayer>().canMove = true;
                isAttacking = false;
            }
            else
            {
                //Increase the hitbox size
                Debug.Log("Increasing Size");
                attackHitbox.GetComponent<Collider>().enabled = true;
                attackHitbox.GetComponent<Renderer>().enabled = true;
                attackHitbox.GetComponent<SphereCollider>().radius += radiusGrowth;
            }
            
        }
        
    }

    
}

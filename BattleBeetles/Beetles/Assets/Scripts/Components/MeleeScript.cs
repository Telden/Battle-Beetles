using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeScript : MonoBehaviour {

    [Header("Hitbox Used")]
    public Collider attackHitbox;

    [Header("Damage Dealt")]
    public int damageDealt;

    
	// Use this for initialization
	void Start () {
        attackHitbox.GetComponent<Collider>().enabled = false;
        attackHitbox.GetComponent<Renderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        userInput();
	}

    void userInput()
    {
        if (Input.GetButtonDown(GetComponent<InputStorage>().getFromInputStorage("AttackSecondary", GetComponent<BasePlayer>().isPlayer1)) && gameObject.GetComponent<BasePlayer>().grounded)
        {
            attackHitbox.GetComponent<Collider>().enabled = true;
            launchAttack(attackHitbox);
            attackHitbox.GetComponent<Collider>().enabled = false;

        }
    }

    void launchAttack(Collider hitbox)
    {
        //Creates an array of hitboxes within the area of the attack hitbox itself (I think it also includes the attack hitbox itself)
        Collider[] cols = Physics.OverlapBox(hitbox.bounds.center, hitbox.bounds.extents, hitbox.transform.rotation);
        //Iterates through the array of colliders within the 
        foreach (Collider c in cols)
        {
            //Debug.Log(c.name);
            //If the hitbox hit wither player, apply knockback to each player
            if(c.name == "Player1" || c.name == "Player2")
            {
                GameObject.Find(c.name).GetComponent<BasePlayer>().applyKnocback();
                GameObject.Find(c.name).GetComponent<BasePlayer>().playerHealth -= damageDealt;
                gameObject.GetComponent<BasePlayer>().applyKnocback();
                Debug.Log(c.name + " hit");
            }


        }

       
       
      
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour {

    [Header("Damage Numbers")]
    public int lungeDamage;
    public int groundPoundDamage;

    void OnTriggerEnter(Collider hitbox)
    {
    	Debug.Log("onTriggerEnter");
        if (hitbox.name == "Lunge" && !gameObject.GetComponent<Lunge>().justHit)
        {
            gameObject.GetComponent<Lunge>().justHit = true;
            Invoke("reenableHitLunge", 0.1f);

            gameObject.GetComponent<BasePlayer>().applyKnocback();
            gameObject.GetComponent<BasePlayer>().playerHealth -= lungeDamage;
            if (gameObject.GetComponent<BasePlayer>().isPlayer1)
            {
                GameObject.Find("Player2").GetComponent<BasePlayer>().applyKnocback();
            }
            else
            {
                GameObject.Find("Player1").GetComponent<BasePlayer>().applyKnocback();
            }
        }
        //If groundpound
        //If SonicBoom
    }

	void reenableHitLunge()
    {
    	GetComponent<Lunge>().justHit = false;
    }
}

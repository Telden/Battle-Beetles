using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputStorage : MonoBehaviour {

    private string Jump = "A";
    private string LockOn = "Left Bumper";
    private string Dash = "Right Bumper";
    private string AttackPrimary = "B";
    private string AttackSecondary = "X";
    private string AttackTertiary = "Y";

    public string getFromInputStorage(string inputName, bool player1)
    {
        if (player1)
        {
            //Debug.Log("player 1 input detected");
            if (inputName == "Jump")
            {
                //Debug.Log(Jump);
                return Jump;
            }

            else if (inputName == "LockOn")
            {
                //Debug.Log(LockOn);
                return LockOn;
            }

            else if (inputName == "Dash")
            {
                //Debug.Log(Dash);
                return Dash;
            }

            else if (inputName == "AttackPrimary")
            {
                //Debug.Log(AttackPrimary);
                return AttackPrimary;
            }

            else if (inputName == "AttackSecondary")
            {
                //Debug.Log(AttackSecondary);
                return AttackSecondary;
            }

            else if (inputName == "AttackTertiary")
            {
                //Debug.Log(AttackTertiary);
                return AttackTertiary;
            }

            Debug.Log("Player 1: Input Name doesn't exist");
            return "";
        }

        else
        {
            //Debug.Log("player 2 input detected");
            if (inputName == "Jump")
            {
                //Debug.Log(Jump + "2");
                return Jump + "2";
            }

            else if (inputName == "LockOn")
            {
                //Debug.Log(LockOn + "2");
                return LockOn + "2";
            }

            else if (inputName == "Dash")
            {
                //Debug.Log(Dash + "2");
                return Dash + "2";
            }

            else if (inputName == "AttackPrimary")
            {
                //Debug.Log(AttackPrimary + "2");
                return AttackPrimary + "2";
            }

            else if (inputName == "AttackSecondary")
            {
                //Debug.Log(AttackSecondary + "2");
                return AttackSecondary + "2";
            }

            else if (inputName == "AttackTertiary")
            {
                //Debug.Log(AttackTertiary + "2");
                return AttackTertiary + "2";
            }

            Debug.Log("Player 2: Input Name doesn't exist");
            return "";
        }
    }
	
	//void Update () {
	//	if (Input.GetButtonDown(Jump))
 //       {
 //           Debug.Log(Jump);
 //       }

 //       if (Input.GetButtonDown(Dash))
 //       {
 //           Debug.Log(Dash);
 //       }

 //       if (Input.GetButtonDown(LockOn))
 //       {
 //           Debug.Log(LockOn);
 //       }

 //       if (Input.GetButtonDown(AttackPrimary))
 //       {
 //           Debug.Log(AttackPrimary);
 //       }

 //       if (Input.GetButtonDown(AttackSecondary))
 //       {
 //           Debug.Log(AttackSecondary);
 //       }

 //       if (Input.GetButtonDown(AttackTertiary))
 //       {
 //           Debug.Log(AttackTertiary);
 //       }
 //   }
}

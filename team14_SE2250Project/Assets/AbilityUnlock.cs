using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AbilityUnlock : MonoBehaviour
{
    public bool unlockDoubleJump;
    public bool unlockDashing;
    public bool unlockBall;
    public bool unlockBomb;
    public GameObject pickupEffect;
    public string unlockedMessage;
    public TMP_Text unlockText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            AbilityTracker player = collision.GetComponentInParent<AbilityTracker>();
            if (unlockDoubleJump)
            {
                player.DoubleJump = true;
                PlayerPrefs.SetInt("DoubleJumpUnlocked", 1);
            }
            if (unlockDashing)
            {
                player.Dash = true;
                PlayerPrefs.SetInt("DashUnlocked", 1);

            }
            if (unlockBall)
            {
                player.Ball = true;
                PlayerPrefs.SetInt("BallUnlocked", 1);

            }
            if (unlockBomb)
            {
                player.DropBomb = true;
                PlayerPrefs.SetInt("BombUnlocked", 1);

            }

            Instantiate(pickupEffect, transform.position, transform.rotation);
            unlockText.transform.parent.SetParent(null);
            unlockText.transform.parent.position = transform.position;
            unlockText.text = unlockedMessage;
            unlockText.gameObject.SetActive(true);
            Destroy(unlockText.transform.parent.gameObject, 5f);

            Destroy(gameObject);
        }


    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleCollider : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D other)
    {
        string tg = other.gameObject.tag;
        if (tg.Equals("Player"))
        {
            // Obtain a reference to the Player's PlayerController
            PlayerController playerController =
              other.gameObject.GetComponent<PlayerController>();

            // Register damage with player
            playerController.Enhance();

            // Make this object disappear
            GameObject.Destroy(gameObject);
        }
    }

}

using UnityEngine;

/*
 * Provide the obstacles with a way of damaging the player.
 */
public class ObstacleCollider : MonoBehaviour
{
    /*
     * A trigger callback to detect when the player's collider has
     * entered the obstacle's. Then simply obtain the PlayerController
     * reference can apply damage. Then remove the obstacle for feedback.
     */
    private void OnCollisionEnter2D(Collision2D other)
  {
    string tg = other.gameObject.tag;
        if (tg.Equals("Player"))
        {
            // Obtain a reference to the Player's PlayerController
            PlayerController playerController =
              other.gameObject.GetComponent<PlayerController>();

            // Register damage with player
            playerController.Damage();

            // Make this object disappear
            GameObject.Destroy(gameObject);
        } else if (!tg.Equals("Ground"))
        {
            Destroy(gameObject);
        }
  }

}

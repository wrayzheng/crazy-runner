using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Behaviour to handle keyboard input and also store the player's
 * current health.
 */
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private int health;
    private bool canJump;
    private Animator animator;

    /*
     * Apply initial health and also store the Rigidbody2D reference for
     * future because GetComponent<T> is relatively expensive.
     */
    private void Start()
    {
        health = 6;
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    /*
     * Remove one health unit from player and if health becomes 0, change
     * scene to the end game scene.
     */
    public void Damage()
    {
        health -= 1;

        if (health < 1)
        {
            Dead();
        }
    }

    /*
     * Increase blood
     */
     public void Enhance()
    {
        health += 1;
        if (health > 6) health = 6;
    }

    /*
     * Accessor for health variable, used by he HUD to display health.
     */
    public int GetHealth()
    {
        return health;
    }

    public void Dead() {
        health = 0;
        PlayerPrefs.DeleteKey("score");
        PlayerPrefs.SetInt("score", FindObjectOfType<GroundTriggerController>().getScore());
        SceneManager.LoadScene("EndGame");
    }

    /*
     * Poll keyboard for when the up arrow is pressed. If the player can jump
     * (is on the ground) then add force to the cached Rigidbody2D component.
     * Finally unset the canJump flag so the player has to wait to land before
     * another jump can be triggered.
     */
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            rigidbody2d.AddForce(new Vector2(200, 0));
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            rigidbody2d.AddForce(new Vector2(-300, 0));
        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (canJump == true)
            {
                animator.Play("jump");
                rigidbody2d.AddForce(new Vector2(0, 800));
                canJump = false;
            }
        }
    }

    /*
     * If the player has collided with the ground, set the canJump flag so that
     * the player can trigger another jump.
     */
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Finish")) Dead();
        canJump = true;
        animator.Play("run");
    }
}

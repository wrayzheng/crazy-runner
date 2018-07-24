using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Behaviour to handle keyboard input and also store the player's
 * current health.
 */
public class PlayerController2 : MonoBehaviour
{
    public GameObject birdTrigger;
    private int health;
    private int height;
    private float deltaY = 3.3f;

    /*
     * Apply initial health and also store the Rigidbody2D reference for
     * future because GetComponent<T> is relatively expensive.
     */
    private void Start()
    {
        height = 0;
        health = 6;
        transform.position = new Vector3(0, 0, 0);
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

    public void Dead()
    {
        health = 0;
        PlayerPrefs.DeleteKey("score");
        PlayerPrefs.SetInt("score", FindObjectOfType<BirdGenerator>().getScore());
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
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (height < 1)
            {
                transform.Translate(new Vector3(0, deltaY, 0));
                height++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (height > -1)
            {
                transform.Translate(new Vector3(0, -deltaY, 0));
                height--;
            }
        }
    }

    /*
     * If the player has collided with the ground, set the canJump flag so that
     * the player can trigger another jump.
     */
    private void OnCollisionEnter2D(Collision2D other)
    {
        FindObjectOfType<BirdGenerator>().resetSpeed();
        if (!other.gameObject.tag.Equals("Finish"))
        {
            transform.Translate(new Vector3(-1.5f, 0, 0));
            birdTrigger.transform.Translate(new Vector3(-1.5f, 0, 0));
            Destroy(other.gameObject);
        } else
        {
            Dead();
        }
    }
}

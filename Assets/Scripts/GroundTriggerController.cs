using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundTriggerController : MonoBehaviour {

    public GameObject ground;
    public GameObject obstacle;
    public GameObject apple;
    public GameObject canvas_text;
    private int score = 0;
    private PlayerController pc;
    private float groundWidth;

	// Use this for initialization
	void Start () {
        pc = FindObjectOfType<PlayerController>();
        groundWidth = ground.transform.localScale.x * ground.GetComponent<BoxCollider2D>().size.x;
    }
	
	// Update is called once per frame
	void Update () {
        canvas_text.GetComponent<Text>().text = score.ToString();
    }

    public int getScore()
    {
        return this.score;
    }

    // Generate ground and other objects when triggered
    private void OnTriggerExit2D(Collider2D other)
    {
        // Position of generated ground
        float x = other.gameObject.transform.position.x;
        if (other.tag.Equals("Ground") && pc.GetHealth() > 0)
        {
            if (Random.value < 0.4) x += 6; // Leave a gap between new ground and old one. In this case, obstacle won't be generated.
            else GenerateObstacle();

            // Generate apple to increase blood
            if (Random.value < 0.1)
            {
                Instantiate(apple, new Vector3(x + 8, 0.86f, 0), new Quaternion());
            }

            Instantiate(ground, new Vector3(x + groundWidth, -3.76f, 0), new Quaternion());
        }

        score += 10;
    }

    public void GenerateObstacle() {
        if (Random.value < 0.8)
        {
            GameObject obj = Instantiate(obstacle, new Vector3(12, 0.6f, 0), new Quaternion());
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(-900, 0));
            obj.transform.Rotate(new Vector3(0, 0, 30));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdGenerator : MonoBehaviour {

    public GameObject raven;
    public GameObject scoreBoard;
    public GameObject speedBoard;
    private int score = 0;
    private float deltaY = 3.3f;
    private float lastTime = -2;
    private float minSpeed = 9;
    private Scroller sc;

	// Use this for initialization
	void Start () {
        sc = raven.GetComponent<Scroller>();
        resetSpeed();
    }
	
	// Update is called once per frame
	void Update () {
        scoreBoard.GetComponent<Text>().text = score.ToString();
        speedBoard.GetComponent<Text>().text = sc.speed.ToString();
	}

    public int getScore()
    {
        return this.score;
    }

    public void resetSpeed()
    {
        sc.speed = minSpeed;
        minSpeed += 1.5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player")) return;
        score += 10;
        if (sc.speed < 25) sc.speed += 0.3f;
        if (Time.time - lastTime < 0.3) return;
        lastTime = Time.time;
        int height1 = Random.Range(-1, 2);
        int height2 = Random.Range(-1, 2);
        Instantiate(raven, new Vector3(15, height1 * deltaY, 0), new Quaternion());
        if (height1 != height2) Instantiate(raven, new Vector3(15, height2 * deltaY, 0), new Quaternion());
    }
}

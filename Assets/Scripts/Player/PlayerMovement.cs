using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Vector3 direction;
    private float[,] noiseMap;
    GameManager gameManager;
    WorldSetup worldSetup;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        worldSetup = FindObjectOfType<WorldSetup>();
        noiseMap = worldSetup.noiseMap;
    }

    
    // Update is called once per frame
    void Update()
    {
        float dirX = Input.GetAxis("Horizontal");
        float dirY = Input.GetAxis("Vertical");

        if (dirX != 0 || dirY != 0)
        {
         //   AttemptMove<Wall>(dirX, dirY);
        }

        direction = new Vector3(dirX, 0.0f, 0.0f) + (dirY * transform.forward);

        transform.position += new Vector3(dirX, dirY, 0) * speed * Time.deltaTime;
        //make the camera follow the player
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RedEnemy")
        {
            Debug.Log("Hit");
        }
    }
}

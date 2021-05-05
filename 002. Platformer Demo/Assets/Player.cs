using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rbd;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move;
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = 0;
        rbd.velocity = move * speed;
    }
}

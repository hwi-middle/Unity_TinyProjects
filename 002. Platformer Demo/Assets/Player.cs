using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rbd;
    public float speed;
    public float maxSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonUp("Horizontal"))
        {
            rbd.velocity = new Vector2(rbd.velocity.normalized.x * 0.5f, rbd.velocity.y);
        }
    }

    void FixedUpdate()
    {
        Debug.Log(rbd.velocity);
        float axis = Input.GetAxisRaw("Horizontal") * speed;
        rbd.AddForce(Vector2.right * axis, ForceMode2D.Impulse);

        if (rbd.velocity.x > maxSpeed)
        {
            rbd.velocity = new Vector2(maxSpeed, rbd.velocity.y);
        }
        else if (rbd.velocity.x < -1 * maxSpeed)
        {
            rbd.velocity = new Vector2(-1 * maxSpeed, rbd.velocity.y);
        }
    }
}

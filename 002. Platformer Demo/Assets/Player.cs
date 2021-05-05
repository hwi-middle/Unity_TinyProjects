using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rbd;
    public float speed;
    public float maxSpeed;
    public float jumpPower;
    public int maxJumpCount = 1;
    private int curJumpCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Horizontal"))
        {
            rbd.velocity = new Vector2(rbd.velocity.normalized.x * 0.5f, rbd.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && curJumpCount < maxJumpCount)
        {
            rbd.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            curJumpCount++;
        }
    }

    void FixedUpdate()
    {
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
        Debug.DrawRay(rbd.position, Vector3.down, new Color(1, 0, 0));

        RaycastHit2D rayHit = Physics2D.Raycast(rbd.position, Vector3.down, 1f, LayerMask.GetMask("Ground"));

        //·¹ÀÌ¿¡ ¹º°¡ ´ê¾Ò°í ³«ÇÏÁßÀÏ °æ¿ì
        if (rayHit.collider != null && rbd.velocity.y < 0)
        {
            if (rayHit.distance < 0.5)
            {
                curJumpCount = 0;
            }
        }
    }
}

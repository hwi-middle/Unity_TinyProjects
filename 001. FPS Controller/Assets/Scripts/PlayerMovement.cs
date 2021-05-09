using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //�� ����
    private const float GRAVITY_CONSTANT = -9.81f;
    public float gravityScale = 1.0f;
    private float gravity;
    public float speed = 12f;
    public float alternativeSpeedFactor = 1f; // ���� Shift Ű�� ������ ���� �ӵ�(�Ϲ� �ӵ����� �����ų� ������ ����)
    public float jumpHeight = 3f;

    //�ٴڿ� ��Ҵ��� üũ�ϴ� groundCheck ������Ʈ
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    //�̵� ����
    public CharacterController controller;
    Vector3 velocity;
    private bool isGrounded;
    private bool isMoving = false;

    //�߼Ҹ� ���
    public AudioClip[] audioClips;
    private int prevClipIndex = -1;
    private AudioSource audioSource;
    public float frequency;
    private float time = 0f;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gravity = GRAVITY_CONSTANT * gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        //���� ���� üũ
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //�߷��� ��ø����Ǵ� ���� ����
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        //�̵� ó��
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        move *= speed;

        bool isLeftShiftKeyDown = false;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isLeftShiftKeyDown = true;
            move *= alternativeSpeedFactor;
        }

        if (move.x != 0 || move.z != 0) isMoving = true;
        else isMoving = false;

        controller.Move(move * Time.deltaTime);

        //���� ó��
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //���ڱ� �Ҹ� ���
        time += Time.deltaTime;
        bool isFootstepSoundRequired;
        if (isLeftShiftKeyDown)
        {
            isFootstepSoundRequired = time  * alternativeSpeedFactor > frequency ;
        }
        else
        {
            isFootstepSoundRequired = time > frequency;
        }

        if (isMoving && isGrounded && isFootstepSoundRequired)
        {
            time = 0f;

            int clipIndex = 0;
            do
            {
                clipIndex = Random.Range(0, audioClips.Length);
            } while (prevClipIndex == clipIndex);
            prevClipIndex = clipIndex;
            audioSource.clip = audioClips[clipIndex];
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}

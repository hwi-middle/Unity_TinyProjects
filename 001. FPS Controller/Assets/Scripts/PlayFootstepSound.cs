using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFootstepSound : MonoBehaviour
{
    private PlayerMovement player;
    public AudioClip[] audioClips;
    private int prevClipIndex = 0;
    private AudioSource audioSource;
    public float frequency;
    private float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (player.GetIsMoving() && player.GetIsGrounded() && time > frequency)
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

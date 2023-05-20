using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField]Animator animator;
    [SerializeField] AudioSource jumpAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateJumpPad();
        }
    }

    void ActivateJumpPad()
    {
        animator.SetTrigger("push");
        GameManager.instance.ball.JumpPad();
        if (jumpAudio != null)
            jumpAudio.Play();
    }

}

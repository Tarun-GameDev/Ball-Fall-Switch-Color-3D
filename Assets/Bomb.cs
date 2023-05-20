using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] bool bombActivated = false;
    [SerializeField] bool exploded = false;
    [SerializeField] float exlpodeCheckRadius = 4f;
    [SerializeField] LayerMask explodeLayer;
    public string[] colorNames;
    [SerializeField] GameObject fracturedBlock;
    [SerializeField] Material[] fracturedBlockMat;
    [SerializeField] GameObject ExplosionEffect;
    [SerializeField] ParticleSystem sparksParticleEffect;
    [SerializeField] AudioSource bombTimerAudio;
    [SerializeField] AudioSource fireCracks;

    Ball ball;

    private void Start()
    {
        ball = GameManager.instance.ball;
    }   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (bombActivated)
                Explode();
            else
                StartCoroutine(timerStart());
        }
    }

    IEnumerator timerStart()
    {
        sparksParticleEffect.Play();
        if (bombTimerAudio != null)
            bombTimerAudio.Play();
        if (fireCracks != null)
            fireCracks.Play();
        bombActivated = true;
        yield return new WaitForSeconds(2f);
        if(!exploded)
            Explode();
    }

    void Explode()
    {
        exploded = true;
        colorNames = ball.colorNames;
        fracturedBlockMat = ball.fracturedBlockMat;
        CinemachineShake.instance.CameraShake(10f, .2f);

        Collider[] col = Physics.OverlapSphere(transform.position, exlpodeCheckRadius, explodeLayer);
        foreach (Collider nearbyObj in col)
        {
            if (nearbyObj.CompareTag("Player"))
                ball.Dead();

            for (int i = 0; i < 4; i++)
            {
                if (nearbyObj.CompareTag(colorNames[i]))
                {
                    var obj = Instantiate(fracturedBlock, nearbyObj.transform.position, Quaternion.identity);
                    Destroy(nearbyObj.gameObject);
                    obj.GetComponent<ChangeMat>().changeMaterial(fracturedBlockMat[i]);
                }
            }
        }
        Deactivate();
    }

    void Deactivate()
    {
        if (ExplosionEffect != null)
            Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position, exlpodeCheckRadius);         
    }
}

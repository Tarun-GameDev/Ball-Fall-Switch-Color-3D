using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] bool invisiblePowUp = true;
    [SerializeField] bool sizeUpPowerUp = false;
    [SerializeField] bool shieldpowerUp = false;

    [SerializeField] GameObject particleEffect;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (invisiblePowUp)
                ActivateInvisiblePowerUp();
            else if (sizeUpPowerUp)
                SizeUpPowerUp();
            else if (shieldpowerUp)
                ShieldPowerUp();

        }
    }

    void ActivateInvisiblePowerUp()
    {
        GameManager.instance.ball.InvisiblePowerUp();
        Deactivate();
    }

    void SizeUpPowerUp()
    {
        GameManager.instance.ball.SizeUPPowerUP();
        Deactivate();
    }

    void ShieldPowerUp()
    {
        GameManager.instance.ball.ShieldPowerUp();
        Deactivate();
    }

    void Deactivate()
    {
        AudioManager _audio = AudioManager.instance;
        if (_audio != null)
            _audio.Play("PowerUp");
        if (particleEffect != null)
            Instantiate(particleEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

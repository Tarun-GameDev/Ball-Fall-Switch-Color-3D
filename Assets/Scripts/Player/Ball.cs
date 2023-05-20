using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 10f;
    public bool right = true;
    Rigidbody rb;
    SphereCollider col;

    public string[] colorNames;
    public int selectedColorId = 4;
    public bool colliderCheck = false;
    [SerializeField] Material BallMatl;
    [SerializeField] Vector2[] mateialOffset = { new Vector2(0.887f, 0.115f),
        new Vector2(0.884f, 0.370f),
        new Vector2(0.7f, 0.35f),
        new Vector2(0.695f, 0.43f),
        new Vector2(0.511f,0.490f)};
    public bool dead = false;
    [SerializeField] UIManager uiManager;
    [SerializeField] GameObject ballFractured;
    [SerializeField] GameObject ballModel;
    
    public bool levelCompleted = false;

    [Header("PowerUPS")]
    [SerializeField] float invisPowTimer = 3f;
    bool sizeUpPowUPActive = false;
    [SerializeField] float sizeUpPowTimer = 3f;
    [SerializeField] GameObject fracturedBlock;
    public Material[] fracturedBlockMat;
    [SerializeField] float jumpPadTimer = 3f;
    [SerializeField] float jumpPadForce = 10f;
    [SerializeField] bool shieldActive = false;
    [SerializeField] GameObject shieldModel;
    [SerializeField] Animator shieldAnimator;
    [SerializeField] ParticleSystem shieldOffPartiEff;
    AudioManager audioManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
        audioManager = AudioManager.instance;
        if (BallMatl != null)
            BallMatl.mainTextureOffset = mateialOffset[selectedColorId];
    }

    private void Update()
    {
        if (dead || levelCompleted)
            return;


    }

    void FixedUpdate()
    {
        if (dead)
            return;

        if (right)
        {
            rb.AddForce(Vector3.right * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);            
        }
        else
        {            
            rb.AddForce(Vector3.left * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }


        var pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, -7.1f, 7.1f);
        transform.position = pos;
            
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (dead)
            return;

        if (collision.collider.CompareTag("Border"))
        {
            right = !right;
        }

        if(sizeUpPowUPActive)
        {
            for (int i = 0; i < 4; i++)
            {
                if (collision.collider.CompareTag(colorNames[i]))
                {
                    CinemachineShake.instance.CameraShake(10f, .2f);
                    var obj = Instantiate(fracturedBlock, collision.collider.transform.position, Quaternion.identity);
                    Destroy(collision.collider.gameObject);
                    obj.GetComponent<ChangeMat>().changeMaterial(fracturedBlockMat[i]);
                }
            }
            return;
        }
        Collider _col = collision.collider;
        GameObject _colObj = collision.gameObject;

        if(selectedColorId != 4)
        {
            if (_col.CompareTag(colorNames[selectedColorId]))
            {
                //_col.isTrigger = true;
                _colObj.GetComponent<BoxCollider>().enabled = false;
                StartCoroutine(disableTrigget(_col,_colObj));
            }
        }

        if (collision.collider.CompareTag("Spikes"))
        {
            if (shieldActive)
            {
                right = !right;
                StartCoroutine(jumpPadUP(.5f,15f));
                StartCoroutine(deactivateShield());
            }
            else
                Dead();
        }
    }
    
    IEnumerator disableTrigget(Collider _col,GameObject _colObj)
    {
        yield return new WaitForSeconds(2f);
        if(!dead)
        {
            _colObj.GetComponent<BoxCollider>().enabled = true;
            //_col.isTrigger = false;
        }
          
    }

    public void SetColorID(int _colorId)
    {
        if (BallMatl != null)
            BallMatl.mainTextureOffset = mateialOffset[_colorId];
        selectedColorId = _colorId;
        colliderCheck = true;
    }

    public void disalecollideCheck()
    {
        colliderCheck = false;
        selectedColorId = 4;
        if (BallMatl != null)
            BallMatl.mainTextureOffset = mateialOffset[selectedColorId];
    }

    public void Dead()
    {
        dead = true;

        CinemachineShake.instance.CameraShake(10f, .5f);
        if (ballModel != null)
            ballModel.SetActive(false);
        if (ballFractured != null)
            Instantiate(ballFractured, transform.position, Quaternion.identity);
        if (uiManager != null)
            uiManager.DeadMenu();
        if (audioManager != null)
            audioManager.Play("BallBreak");
    }


    #region powerUps
    public void InvisiblePowerUp()
    {
        StartCoroutine(StartInvisiblePow());
    }

    IEnumerator StartInvisiblePow()
    {
        CinemachineShake.instance.CameraShake(10f, .2f);
        gameObject.layer = 15;
        BallMatl.SetColor("_Color", new Color(1, 1, 1, .5f));
        yield return new WaitForSeconds(invisPowTimer);
        if(!dead)
            DisablePowerUp();
    }

    public void DisablePowerUp()
    {
        gameObject.layer = 3;
        BallMatl.SetColor("_Color", new Color(1, 1, 1, 1f));
    }

    public void SizeUPPowerUP()
    {
        StartCoroutine(SizeUPPow());
    }

    IEnumerator SizeUPPow()
    {
        CinemachineShake.instance.CameraShake(10f, .2f);
        transform.localScale = transform.localScale * 2f;
        rb.mass = rb.mass * 2f;
        sizeUpPowUPActive = true;
        yield return new WaitForSeconds(sizeUpPowTimer);
        if (!dead)
        {
            transform.localScale = transform.localScale / 2f;
            rb.mass = rb.mass/2f;
            sizeUpPowUPActive = false;
        }
    }

    public void JumpPad()
    {
        StartCoroutine(jumpPadUP(jumpPadTimer,jumpPadForce));
    }

    IEnumerator jumpPadUP(float _jumpPadTimer,float _jumpForce)
    {
        gameObject.layer = 15;
        BallMatl.SetColor("_Color", new Color(1, 1, 1, .5f));
        rb.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
        CinemachineShake.instance.CameraShake(10f, .2f);
        yield return new WaitForSeconds(_jumpPadTimer);
        if(!dead)
            DisablePowerUp();
    }

    public void ShieldPowerUp()
    {
        StartCoroutine(Shield());
    }

    IEnumerator Shield()
    {
        CinemachineShake.instance.CameraShake(10f, .2f);
        shieldActive = true;
        shieldModel.SetActive(true);
        col.radius = 0.75f;
        shieldAnimator.SetTrigger("ShieldActivate");
        yield return new WaitForSeconds(15f);
        if(shieldActive && !dead)
            StartCoroutine(deactivateShield());
    }

    IEnumerator deactivateShield()
    {
        CinemachineShake.instance.CameraShake(10f, .2f);
        shieldModel.SetActive(false);
        col.radius = 0.5f;
        yield return new WaitForSeconds(.1f);
        shieldActive = false;
        if (shieldOffPartiEff != null)
            shieldOffPartiEff.Play();
        if (audioManager != null)
            audioManager.Play("PowerUp");
    }

    

    /*
    void disableCollision()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, collisionCheckRadius, collideWithObstuclesMask);
        foreach (Collider nearbyObject in collider)
        {
            Collider _colorCol = nearbyObject.GetComponent<Collider>();
            if (_colorCol != null)
                _colorCol.isTrigger = true;
        }
    }*/
    #endregion

}

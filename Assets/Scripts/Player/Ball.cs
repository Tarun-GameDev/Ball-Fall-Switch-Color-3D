using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 10f;
    public bool right = true;
    Rigidbody rb;
    Collider col;

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
    bool invisiblePowerUP = false;
    public bool levelCompleted = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        if (BallMatl != null)
            BallMatl.mainTextureOffset = mateialOffset[selectedColorId];
    }

    private void Update()
    {
        if (dead || levelCompleted)
            return;

        //if(invisiblePowerUP)
            //disableCollision();
    }


    void FixedUpdate()
    {
        if (dead)
            return;

        if (right)
        {
            rb.AddForce(Vector3.right * speed, ForceMode.Force);
        }
        else
            rb.AddForce(Vector3.left * speed, ForceMode.Force);   
            
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (dead)
            return;

        if (collision.collider.CompareTag("Border"))
        {
            right = !right;
        }

        if(selectedColorId != 4)
        {
            if (collision.collider.CompareTag(colorNames[selectedColorId]))
            {
                collision.collider.isTrigger = true;
            }
            else if (collision.collider.isTrigger)
                collision.collider.isTrigger = false;

        }

        if (collision.collider.CompareTag("Spikes"))
        {
            Dead();
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
    }


    #region powerUps
    public void InvisiblePowerUp()
    {
        StartCoroutine(StartInvisiblePow());
    }

    IEnumerator StartInvisiblePow()
    {
        //invisiblePowerUP = true;
        gameObject.layer = 15;
        BallMatl.SetColor("_Color", new Color(1, 1, 1, .5f));
        yield return new WaitForSeconds(2f);
        //invisiblePowerUP = false;
        DisablePowerUp();
    }

    public void DisablePowerUp()
    {
        gameObject.layer = 3;
        BallMatl.SetColor("_Color", new Color(1, 1, 1, 1f));
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

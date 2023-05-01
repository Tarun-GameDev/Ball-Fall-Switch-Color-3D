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
    public int selectedColorId;
    public bool colliderCheck = false;
    public float collisionCheckRadius = 1f;
    public LayerMask collideWithObstuclesMask;
    [SerializeField] float enemyCheckRadius = .5f;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] Material BallMatl;
    [SerializeField] Vector2[] mateialOffset;
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

        //for spikes collision check
        EnemyCollisionCheck();

        if (colliderCheck)
            CollisionCheck();

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

        /*
        if(colliderCheck)
        {
            if (collision.collider.CompareTag(colorNames[selectedColorId]))
            {
                collision.collider.isTrigger = true;
            }
            colliderCheck = false;
        }*/
    }

    void CollisionCheck()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, collisionCheckRadius, collideWithObstuclesMask);
        foreach (Collider nearbyObject in collider)
        {
            Collider _colorCol = nearbyObject.GetComponent<Collider>();

            if (_colorCol.CompareTag(colorNames[selectedColorId]))
            {
                _colorCol.isTrigger = true;
            }
            else if (_colorCol.isTrigger)
                _colorCol.isTrigger = false;
        }
    }

    void EnemyCollisionCheck()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, enemyCheckRadius, enemyMask);
        foreach (Collider nearbyObject in collider)
        {

            var obj = nearbyObject.GetComponent<Collider>();
            if (obj != null)
                if(obj.enabled)
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

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(transform.position, collisionCheckRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, enemyCheckRadius);
    }

}

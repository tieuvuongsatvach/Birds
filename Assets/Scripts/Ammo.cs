using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public float Force;

    Rigidbody2D m_rb;
    LineRenderer m_lr;

    public Vector2 mousePos;
    public Vector2 mousePosOld;

    GameControl m_gc;

    public float springRange;
    Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_lr = GetComponent<LineRenderer>();
        m_gc = FindObjectOfType<GameControl>();
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_gc.getOperation())
        {
            if (Input.GetButtonDown("Fire1"))
            {
                mousePosOld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetButton("Fire1"))
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 vectorForce = mousePosOld - mousePos;
                float distance = vectorForce.magnitude;
                if (distance < springRange)
                {
                    Force = Mathf.Sqrt(vectorForce.x * vectorForce.x + vectorForce.y * vectorForce.y);
                    Vector2 velocity = (mousePosOld - mousePos) * Force;
                    Vector2[] trajectory = Plot(m_rb, (Vector2)transform.position, velocity, 500);
                    m_lr.positionCount = trajectory.Length;
                    Vector3[] positions = new Vector3[trajectory.Length];
                    for (int i = 0; i < positions.Length; i++)
                    {
                        positions[i] = trajectory[i];
                    }
                    m_lr.SetPositions(positions);
                }
                if (distance >= springRange)
                {
                    Force = 1.4f;
                    vectorForce = (mousePosOld - mousePos).normalized * springRange;
                    Vector2 velocity = vectorForce * Force;
                    Vector2[] trajectory = Plot(m_rb, (Vector2)transform.position, velocity, 500);
                    m_lr.positionCount = trajectory.Length;
                    Vector3[] positions = new Vector3[trajectory.Length];
                    for (int i = 0; i < positions.Length; i++)
                    {
                        positions[i] = trajectory[i];
                    }
                    m_lr.SetPositions(positions);
                }

            }

            if (Input.GetButtonUp("Fire1"))
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 vectorForce = mousePosOld - mousePos;
                float distance = vectorForce.magnitude;
                if (distance < springRange)
                {
                    Force = Mathf.Sqrt(vectorForce.x * vectorForce.x + vectorForce.y * vectorForce.y);
                }
                if (distance >= springRange)
                {
                    Force = 1.4f;
                    vectorForce = (mousePosOld - mousePos).normalized * springRange;
                }

                m_rb.gravityScale = 1;
                m_rb.AddForce(vectorForce * 350 * Force);
                m_gc.isOperation();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("pig")||col.gameObject.CompareTag("grass"))
        {
            m_gc.setTimeAmmo();
            m_gc.setAmmoDestroy();
            Destroy(gameObject, 2);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("limit"))
        {
            m_gc.setTimeAmmo();
            m_gc.setAmmoDestroy();
            Destroy(gameObject, 1);
        }
    }

    private void OnMouseDrag()
    {
        Vector3 birdPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        birdPos.z = 0;
        float distance = (initialPos - birdPos).magnitude;
        if (distance < springRange)
        {
            transform.position = new Vector3(birdPos.x, birdPos.y);
        }
        else
        {
            transform.position = initialPos + springRange * (birdPos - initialPos).normalized;
        }
    }

    public Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps)
    {
        Vector2[] results = new Vector2[steps];

        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * timestep * timestep;
        //Vector2 gravityAccel = Physics2D.gravity * rigidbody.gravityScale * timestep * timestep;
        float drag = 1f - timestep * rigidbody.drag;
        Vector2 moveStep = velocity * timestep*7;
        //Vector2 moveStep = velocity * timestep;


        for (int i = 0; i < steps; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            results[i] = pos;
        }

        return results;
    }
    public static Vector3[] toVector3 (Vector2[] v2)
    {
        return System.Array.ConvertAll<Vector2, Vector3>(v2, getV3fromV2);
    }

    public static Vector3 getV3fromV2(Vector2 v2)
    {
        return new Vector3(v2.x, v2.y, 0);
    }
}
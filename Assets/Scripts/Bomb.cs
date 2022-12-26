using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public GameObject FireMid;
    public GameObject FireHor;
    public GameObject FireVert;
    public GameObject FireLeft;
    public GameObject FireRight;
    public GameObject FireTop;
    public GameObject FireBottom;

    public float Delay;

    public LayerMask StoneLayer;
    public LayerMask BlowableLayer;

    private int fireLength;
    private float Counter;

    public List<Vector2> CellsToBlowLeft;
    public List<Vector2> CellsToBlowRight;
    public List<Vector2> CellsToBlowUp;
    public List<Vector2> CellsToBlowDown;

    void Start()
    {
        Counter = Delay;
        CellsToBlowLeft = new List<Vector2>();
        CellsToBlowRight = new List<Vector2>();
        CellsToBlowUp = new List<Vector2>();
        CellsToBlowDown = new List<Vector2>();
    }
    void Update()
    {
        if (Counter > 0)
            Counter -= Time.deltaTime;
        else
        {
            Blow();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Buble")
        {
            Blow();
            Destroy(gameObject);
        }



    }

    private void Blow()
    {
        CalculateFireDirections();
        // Stat explode anim
        Instantiate(FireMid, transform.position, transform.rotation);

        //Left
        if (CellsToBlowLeft.Count > 0)
            for (int i = 0; i < CellsToBlowLeft.Count; i++)
            {

                if (i == CellsToBlowLeft.Count - 1)
                    Instantiate(FireLeft, CellsToBlowLeft[i], transform.rotation);
                else
                    Instantiate(FireHor, CellsToBlowLeft[i], transform.rotation);
            }
        //Right
        if (CellsToBlowRight.Count > 0)
            for (int i = 0; i < CellsToBlowRight.Count; i++)
            {

                if (i == CellsToBlowRight.Count - 1)
                    Instantiate(FireRight, CellsToBlowRight[i], transform.rotation);
                else
                    Instantiate(FireHor, CellsToBlowRight[i], transform.rotation);
            }
        //Up
        if (CellsToBlowUp.Count > 0)
            for (int i = 0; i < CellsToBlowUp.Count; i++)
            {

                if (i == CellsToBlowUp.Count - 1)
                    Instantiate(FireTop, CellsToBlowUp[i], transform.rotation);
                else
                    Instantiate(FireVert, CellsToBlowUp[i], transform.rotation);
            }
        //Down
        if (CellsToBlowDown.Count > 0)
            for (int i = 0; i < CellsToBlowDown.Count; i++)
            {

                if (i == CellsToBlowDown.Count - 1)
                    Instantiate(FireBottom, CellsToBlowDown[i], transform.rotation);
                else
                    Instantiate(FireVert, CellsToBlowDown[i], transform.rotation);
            }

        Destroy(gameObject);
    }

    private void CalculateFireDirections()
    {
        fireLength = FindObjectOfType<Duck>().GetFireLengh();
        // Left
        for (int i = 1; i <= fireLength; i++)
        {
            if (Physics2D.OverlapCircle(new Vector2(transform.position.x - i, transform.position.y), 0.1f, StoneLayer))
            {
                break;
            }
            if (Physics2D.OverlapCircle(new Vector2(transform.position.x - i, transform.position.y), 0.1f, BlowableLayer))
            {
                CellsToBlowLeft.Add(new Vector2(transform.position.x - i, transform.position.y));
                break;
            }
            CellsToBlowLeft.Add(new Vector2(transform.position.x - i, transform.position.y));
        }
        //Right
        for (int i = 1; i <= fireLength; i++)
        {
            if (Physics2D.OverlapCircle(new Vector2(transform.position.x + i, transform.position.y), 0.1f, StoneLayer))
            {
                break;
            }
            if (Physics2D.OverlapCircle(new Vector2(transform.position.x + i, transform.position.y), 0.1f, BlowableLayer))
            {
                CellsToBlowRight.Add(new Vector2(transform.position.x + i, transform.position.y));
                break;
            }
            CellsToBlowRight.Add(new Vector2(transform.position.x + i, transform.position.y));
        }
        //Up
        for (int i = 1; i <= fireLength; i++)
        {
            if (Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - i), 0.1f, StoneLayer))
            {
                break;
            }
            if (Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - i), 0.1f, BlowableLayer))
            {
                CellsToBlowUp.Add(new Vector2(transform.position.x, transform.position.y - i));
                break;
            }
            CellsToBlowUp.Add(new Vector2(transform.position.x, transform.position.y - i));
        }
        //Down
        for (int i = 1; i <= fireLength; i++)
        {
            if (Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + i), 0.1f, StoneLayer))
            {
                break;
            }
            if (Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + i), 0.1f, BlowableLayer))
            {
                CellsToBlowDown.Add(new Vector2(transform.position.x, transform.position.y + i));
                break;
            }
            CellsToBlowDown.Add(new Vector2(transform.position.x, transform.position.y + i));
        }
    }

    private void OnDrawGizmos()
    {
        if (CellsToBlowLeft != null)
        {
            foreach (var item in CellsToBlowLeft)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(item, 0.2f);
            }
            foreach (var item in CellsToBlowRight)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(item, 0.2f);
            }
            foreach (var item in CellsToBlowUp)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(item, 0.2f);
            }
            foreach (var item in CellsToBlowDown)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(item, 0.2f);
            }
        }
    }
}

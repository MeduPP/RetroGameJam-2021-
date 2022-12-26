using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour
{

    private Rigidbody2D rb2D;

    public float MovingSpeed = 1f;

    private bool ButtonLeft;
    private bool ButtonRight;
    private bool ButtonUp;
    private bool ButtonDown;
    private bool ButtonBomb;
    private bool ButtonDetonate;

    private bool CanMove;
    private bool IsMoving;
    private bool insideBomb;
    private bool insideFire;


    private int Direction;//numpad 8,4,2,6

    public Transform Sencer;

    public float SencerSize = 0.7f;
    public float SencerRange = 0.4f;

    public float MoveSpeed = 2f;

    public LayerMask StoneLayer;
    public LayerMask BombLayer;
    public LayerMask BrickLayer;
    public LayerMask FireLayer;


    private int BombsAllowed = 2;
    private int FireLengh = 1;

    public GameObject bomb;

    void Update()
    {
        GetInput();
        GetDirection();
        HandSencor();
        HandleBomb();
        Move();

        Animate();
    }

    private void HandleBomb()
    {
        if (ButtonBomb && GameObject.FindGameObjectsWithTag("Bomb").Length < BombsAllowed && !insideBomb)
        {
            Instantiate(bomb, new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y)), transform.rotation);
        }
    }

    private void Move()
    {
        if (!CanMove)
        {
            return;
        }

        IsMoving = true;

        switch (Direction)
        {
            case 8:
                transform.position = new Vector2(Mathf.Round(transform.position.x), transform.position.y + MoveSpeed * Time.deltaTime);
                break;
            case 6:
                transform.position = new Vector2(transform.position.x + MoveSpeed * Time.deltaTime, Mathf.Round(transform.position.y));
                break;
            case 4:
                transform.position = new Vector2(transform.position.x - MoveSpeed * Time.deltaTime, Mathf.Round(transform.position.y));
                break;
            case 2:
                transform.position = new Vector2(Mathf.Round(transform.position.x), transform.position.y - MoveSpeed * Time.deltaTime);
                break;
            case 5:
                IsMoving = false;
                break;
        }
    }

    private void HandSencor()
    {
        Sencer.transform.localPosition = new Vector2(0, 0);

        insideBomb = Physics2D.OverlapBox(Sencer.position, new Vector2(SencerSize, SencerSize), 0, BombLayer);

        insideFire = Physics2D.OverlapBox(Sencer.position, new Vector2(SencerSize, SencerSize), 0, FireLayer);

        switch (Direction)
        {
            case 8:
                Sencer.transform.localPosition = new Vector2(0, SencerRange);
                break;
            case 6:
                Sencer.transform.localPosition = new Vector2(SencerRange, 0);
                break;
            case 4:
                Sencer.transform.localPosition = new Vector2(-SencerRange, 0);
                break;
            case 2:
                Sencer.transform.localPosition = new Vector2(0, -SencerRange);
                break;
        }

        CanMove = !Physics2D.OverlapBox(Sencer.position, new Vector2(SencerSize, SencerSize), 0, StoneLayer);

        if (CanMove)
            CanMove = !Physics2D.OverlapBox(Sencer.position, new Vector2(SencerSize, SencerSize), 0, BrickLayer);

        if (CanMove && !insideBomb)
            CanMove = !Physics2D.OverlapBox(Sencer.position, new Vector2(SencerSize, SencerSize), 0, BombLayer);
    }

    private void GetInput()
    {
        ButtonLeft = Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow);
        ButtonRight = !Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow);
        ButtonUp = !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow);
        ButtonDown = !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow);

        ButtonBomb = Input.GetKeyDown(KeyCode.Z);
        ButtonDetonate = Input.GetKeyDown(KeyCode.X);
    }


    private void GetDirection()
    {
        Direction = 5;

        if (ButtonLeft)
            Direction = 4;
        if (ButtonRight)
            Direction = 6;
        if (ButtonUp)
            Direction = 8;
        if (ButtonDown)
            Direction = 2;
    }

    public void AddBomb()
    {
        BombsAllowed++;
    }

    public void AddFireLengh()
    {
        FireLengh++;
    }

    public int GetFireLengh()
    {
        return FireLengh;
    }

    private void Animate()
    {
        var amimator = GetComponent<Animator>();

        amimator.SetInteger("Direction", Direction);
        amimator.SetBool("IsMoving", IsMoving);
    }
}

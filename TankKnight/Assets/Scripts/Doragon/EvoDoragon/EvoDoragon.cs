using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvoDoragon : PlayerController
{
    // Start is called before the first frame update
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        con = GetComponent<CharacterController>();
        startPos = transform.position;
        animator = GetComponent<Animator>();
        mainCamera = GameObject.FindGameObjectWithTag("Camera").GetComponent<Camera>();
       
    }

    // Update is called once per frame
    protected override void Update()
    {

        base.Move();
        base.InputContorol("isWalk");
        base.Attack("isAttack");
        
        if (DoragonDate.Instance.currentExp >= DoragonDate.Instance.nextLvExp[1]) { canEvo = true; }
        Debug.Log(canEvo);
        if (canEvo)
        {
            Evo(1,1, 0);
            canEvo = false;

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody rb;
    [SerializeField]protected Camera mainCamera; // �v���C���[�ɒǏ]����J����
    protected  CharacterController con;

    //�ړ�
    Vector3 moveDirection = Vector3.zero;
    protected Vector3 startPos;
    //�J����
    [SerializeField] float rotationSpeed; // �J�����̉�]���x
    [SerializeField] float cameraHeight ; // �J�����̍���
    [SerializeField] float cameraDistance = 5.0f; // �J�����ƃv���C���[�̋���
    [SerializeField] float fieldOfView = 20; // �J�����̃t�B�[���h�I�u�r���[

    //�ʔ���
    [SerializeField] Transform bulletTrans;
    [SerializeField] GameObject bulletObj;
    //�U���C���^�[�o���֌W
    bool isShot=false;
    float shotIntervalCount;
    //�i���֌W
    [SerializeField] GameObject[] evoDoragons;
    protected bool canEvo=true;
    protected bool doOnes = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        con = GetComponent<CharacterController>();
        startPos = transform.position;
        animator = GetComponent<Animator>();
    }

    protected virtual void  Update()
    {

        Move();
        InputContorol("isWalk");
        Attack("isAttack");
        Debug.Log(canEvo);
        
        if (canEvo)
        {
            Evo(0,1, 0);
        }

    }


    #region �i��
  protected  void Evo(int exp,int evoNum,int destoroyNum)
    {
        if (DoragonDate.Instance.currentExp >= DoragonDate.Instance.nextLvExp[exp])
        {
            Instantiate(evoDoragons[evoNum],transform .position, Quaternion.identity);
            Destroy(evoDoragons[destoroyNum]);
            DoragonDate.Instance.CurrentLv++;
            canEvo = false;

        }
    }
    #endregion


    #region �U��
    protected void Attack(string anim)
    {
        shotIntervalCount += Time.deltaTime;

        if (!isShot)
        { 
                if (Input.GetKeyDown("joystick button 5"))
                {
                    ShootBullet();
                    animator.SetTrigger(anim);
                    isShot = true;
                    shotIntervalCount = 0;
                }
                
        }

        if (shotIntervalCount >= BulletState.Instance.bulletInterval)
        {
            isShot = false;
        }
    }
    #endregion


    #region �e���Ƃ΂�
    protected void ShootBullet()
    {
        // �v���C���[�̌������擾
        Vector3 playerDirection = transform.forward;

        // �e�𐶐�
        GameObject bullet = Instantiate(bulletObj, bulletTrans.position, Quaternion.identity);

        // �e�̌������v���C���[�̌����ɍ��킹��
        bullet.transform.forward = playerDirection;

        // �e�̑��x�������ݒ�
        bullet.GetComponent<Rigidbody>().velocity = playerDirection*BulletState.Instance.bulletSpeed;
    }
    #endregion


    #region Xbox����
    protected void InputContorol(string anim)
    {
        // �g�p����v���C���[���͂��擾
        var playerInput = InputSystem.GetDevice<Gamepad>();

        if (playerInput != null)
        {
            // ���A�i���O�X�e�B�b�N�̓��͂��擾
            Vector2 stickInput = playerInput.leftStick.ReadValue();

            // �X�e�B�b�N�̓��͂��g�p���ăv���C���[���ړ�������
            Vector3 moveDirection = new Vector3(stickInput.x, 0f, stickInput.y).normalized;

            float moveAngle = stickInput.x * rotationSpeed * Time.deltaTime;
            Vector3 temp = transform.eulerAngles;
            temp.y += moveAngle;
            transform.eulerAngles = temp;

            moveDirection = transform.forward * DoragonDate.Instance.moveSpeed;
            moveDirection.y = 0;

            con.Move(moveDirection * Time.deltaTime * stickInput.y);

            if (stickInput.x != 0 || stickInput.y != 0)
            {
                animator.SetBool(anim, true);
            }
            else
            {
                animator.SetBool(anim, false);
            }
            UpdateCameraPosition();
        }
    }
    #endregion


    #region �L�[����
    protected void Move()
    {
        MoveToAngle("Horizontal", "Vertical", "isWalk");
    }
    #endregion


    #region Move����
    protected void MoveToAngle(string Horizontal,string Vertical,string anim)
    {
        float inputHorizontal = Input.GetAxis(Horizontal);
        float inputVertical = Input.GetAxis(Vertical);

        float moveAngle = inputHorizontal * rotationSpeed * Time.deltaTime;
        Vector3 temp = transform.eulerAngles;
        temp.y += moveAngle;
        transform.eulerAngles = temp;
        Vector3 forward = transform.forward * DoragonDate.Instance.moveSpeed;
        forward.y = 0;
        Vector3 moveVec = forward * Time.deltaTime * inputVertical;
        //con.Move(forward * Time.deltaTime * inputVertical);
        transform.position = new Vector3(transform.position.x + moveVec.x, transform.position.y + moveVec.y, transform.position.z + moveVec.z);
        UpdateCameraPosition();

        if (inputHorizontal != 0||inputVertical!=0)
        {
           animator.SetBool(anim, true);
        }
        else
        {
            animator.SetBool(anim, false);
        }
    }
    #endregion


    #region �g�b�v�_�E��
    void TopDownMove()
    {
        // �v���C���[�̈ړ�����
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            rb.rotation = Quaternion.Slerp(rb.rotation, toRotation, Time.deltaTime * rotationSpeed);
        }

        rb.velocity = moveDirection * DoragonDate.Instance.moveSpeed;
    }
    #endregion


    #region �J����
    protected void UpdateCameraPosition()
    {
        // �J�������v���C���[�̌��ɔz�u
        Vector3 offset = -transform.forward * cameraDistance + Vector3.up * cameraHeight;
        mainCamera.transform.position = transform.position + offset;

        // �J�����̒����_���v���C���[�ɍ��킹��
        mainCamera.transform.LookAt(transform.position);
        // �J�����̃t�B�[���h�I�u�r���[��ύX
        mainCamera.fieldOfView = fieldOfView;
    }
    #endregion


    #region �g��Ȃ��ړ�
    //Vector3 cameraForward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1,0 ,1)).normalized;

    //Vector3 moveZ = cameraForward * Input.GetAxis("Vertical") * speed;  //�@�O��i�J������j�@ 
    //Vector3 moveX = mainCamera.transform.right * Input.GetAxis("Horizontal") * speed; // ���E�i�J������j
    //moveDirection = moveZ + moveX;
    // �v���C���[�̌�������͂̌����ɕύX�@
    //transform.LookAt(transform.position + moveZ + moveX);

    // Move �͎w�肵���x�N�g�������ړ������閽��
    //con.Move(moveDirection * Time.deltaTime);
    #endregion()
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody rb;
    [SerializeField]protected Camera mainCamera; // プレイヤーに追従するカメラ
    protected  CharacterController con;

    //移動
    Vector3 moveDirection = Vector3.zero;
    protected Vector3 startPos;
    //カメラ
    [SerializeField] float rotationSpeed; // カメラの回転速度
    [SerializeField] float cameraHeight ; // カメラの高さ
    [SerializeField] float cameraDistance = 5.0f; // カメラとプレイヤーの距離
    [SerializeField] float fieldOfView = 20; // カメラのフィールドオブビュー

    //玉発射
    [SerializeField] Transform bulletTrans;
    [SerializeField] GameObject bulletObj;
    //攻撃インターバル関係
    bool isShot=false;
    float shotIntervalCount;
    //進化関係
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


    #region 進化
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


    #region 攻撃
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


    #region 弾をとばす
    protected void ShootBullet()
    {
        // プレイヤーの向きを取得
        Vector3 playerDirection = transform.forward;

        // 弾を生成
        GameObject bullet = Instantiate(bulletObj, bulletTrans.position, Quaternion.identity);

        // 弾の向きをプレイヤーの向きに合わせる
        bullet.transform.forward = playerDirection;

        // 弾の速度や方向を設定
        bullet.GetComponent<Rigidbody>().velocity = playerDirection*BulletState.Instance.bulletSpeed;
    }
    #endregion


    #region Xbox入力
    protected void InputContorol(string anim)
    {
        // 使用するプレイヤー入力を取得
        var playerInput = InputSystem.GetDevice<Gamepad>();

        if (playerInput != null)
        {
            // 左アナログスティックの入力を取得
            Vector2 stickInput = playerInput.leftStick.ReadValue();

            // スティックの入力を使用してプレイヤーを移動させる
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


    #region キー操作
    protected void Move()
    {
        MoveToAngle("Horizontal", "Vertical", "isWalk");
    }
    #endregion


    #region Move実装
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


    #region トップダウン
    void TopDownMove()
    {
        // プレイヤーの移動処理
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


    #region カメラ
    protected void UpdateCameraPosition()
    {
        // カメラをプレイヤーの後ろに配置
        Vector3 offset = -transform.forward * cameraDistance + Vector3.up * cameraHeight;
        mainCamera.transform.position = transform.position + offset;

        // カメラの注視点をプレイヤーに合わせる
        mainCamera.transform.LookAt(transform.position);
        // カメラのフィールドオブビューを変更
        mainCamera.fieldOfView = fieldOfView;
    }
    #endregion


    #region 使わない移動
    //Vector3 cameraForward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1,0 ,1)).normalized;

    //Vector3 moveZ = cameraForward * Input.GetAxis("Vertical") * speed;  //　前後（カメラ基準）　 
    //Vector3 moveX = mainCamera.transform.right * Input.GetAxis("Horizontal") * speed; // 左右（カメラ基準）
    //moveDirection = moveZ + moveX;
    // プレイヤーの向きを入力の向きに変更　
    //transform.LookAt(transform.position + moveZ + moveX);

    // Move は指定したベクトルだけ移動させる命令
    //con.Move(moveDirection * Time.deltaTime);
    #endregion()
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletContorol : MonoBehaviour
{
    Rigidbody rb;
    LineRenderer lineRenderer;

    //弾の種類
    [SerializeField] GameObject diffusionCol,longBulletCol;

    bool isSlip=false;
    float interval=10;
    float count;

    [SerializeField] float length;

    [SerializeField] float rotateSpeed = 5f;
    Transform target;

    [SerializeField] float zigzagAmount = 1f;
    [SerializeField] float zigzagSpeed = 2f;

     float horizontalMovement = 0f;
     Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        // 敵のTransformを取得する例として、ここでは"Enemy"タグを持つ敵を探します。
        target = GameObject.FindGameObjectWithTag("Knight").transform;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, BulletState.Instance.buletLifeTime);
        SlipDamegeBullet();
        LongBullet();
        Homing();
        CurveBullet();
    }
    void CurveBullet()
    {
        if (KindDate.Instance.canCurveBullet)
        {
            // 左右に緩やかにジグザグと曲がる動き
            horizontalMovement = Mathf.Sin(Time.time * zigzagSpeed) * zigzagAmount;

            // 弾の移動
            transform.Translate(new Vector3(horizontalMovement, 0, 1) * BulletState.Instance.bulletSpeed * Time.deltaTime);

            //もし必要なら、弾がある程度進んだらリセットするなどの処理を追加することも考えられます
            if (Vector3.Distance(startPosition, transform.position) > 100) // 10fは弾の最大の移動距離
            {
                // 弾のリセットなどの処理を追加
                ResetBullet();
            }
        }
    }
    void ResetBullet()
    {
        // 弾をリセットするための処理を記述（例: プールに戻す、新しい位置に配置するなど）
        transform.position = startPosition;
    }
    void SlipDamegeBullet()
    {
        if (isSlip)
        {
            count += Time.deltaTime;
            //当たった敵のHPをーする処理を追加
            if (count >= interval)
            {
                isSlip = false;
                count = 0;
            }
        }
    }

    void LongBullet()
    {
        if (KindDate.Instance.canLongBullet)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + transform.forward* length); // lengthは軌跡の長さ

            // 当たり判定処理
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, length)) // lengthは軌跡の長さと同じ
            {
                // ここで当たり判定が発生した時の処理を記述
                Debug.Log("Hit: " + hit.collider.name);
            }
        }
       
    }
    void Homing()
    {
        if (KindDate.Instance.canHomingBullet)
        {
            if (target == null)
            {
                // 敵が存在しない場合は何もしない
                return;
            }

            // 向きを調整して敵に向かう
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);

            // 前進
            transform.Translate(Vector3.forward * BulletState.Instance.bulletSpeed * Time.deltaTime);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Knight"))
        {
            if (!KindDate.Instance.canPiercingBullet)
            {
                Destroy(this.gameObject);
            }

            if (KindDate.Instance.canSlowBullet)
            {
                //当たった物体のスピードを零に
                //collision.gameObject.
            }

            if (KindDate.Instance.canDiffusionBullet)
            {
                Instantiate(diffusionCol, this.transform.position, Quaternion.identity);
            }

            if (KindDate.Instance.canSlipDamegeBullet)
            {
                if (!isSlip)
                {
                    isSlip = true;
                }
            }

            
        }
    }

}

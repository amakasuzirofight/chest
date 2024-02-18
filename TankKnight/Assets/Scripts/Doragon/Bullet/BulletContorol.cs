using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletContorol : MonoBehaviour
{
    Rigidbody rb;
    LineRenderer lineRenderer;

    //�e�̎��
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
        // �G��Transform���擾�����Ƃ��āA�����ł�"Enemy"�^�O�����G��T���܂��B
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
            // ���E�Ɋɂ₩�ɃW�O�U�O�ƋȂ��铮��
            horizontalMovement = Mathf.Sin(Time.time * zigzagSpeed) * zigzagAmount;

            // �e�̈ړ�
            transform.Translate(new Vector3(horizontalMovement, 0, 1) * BulletState.Instance.bulletSpeed * Time.deltaTime);

            //�����K�v�Ȃ�A�e��������x�i�񂾂烊�Z�b�g����Ȃǂ̏�����ǉ����邱�Ƃ��l�����܂�
            if (Vector3.Distance(startPosition, transform.position) > 100) // 10f�͒e�̍ő�̈ړ�����
            {
                // �e�̃��Z�b�g�Ȃǂ̏�����ǉ�
                ResetBullet();
            }
        }
    }
    void ResetBullet()
    {
        // �e�����Z�b�g���邽�߂̏������L�q�i��: �v�[���ɖ߂��A�V�����ʒu�ɔz�u����Ȃǁj
        transform.position = startPosition;
    }
    void SlipDamegeBullet()
    {
        if (isSlip)
        {
            count += Time.deltaTime;
            //���������G��HP���[���鏈����ǉ�
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
            lineRenderer.SetPosition(1, transform.position + transform.forward* length); // length�͋O�Ղ̒���

            // �����蔻�菈��
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, length)) // length�͋O�Ղ̒����Ɠ���
            {
                // �����œ����蔻�肪�����������̏������L�q
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
                // �G�����݂��Ȃ��ꍇ�͉������Ȃ�
                return;
            }

            // �����𒲐����ēG�Ɍ�����
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);

            // �O�i
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
                //�����������̂̃X�s�[�h����
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

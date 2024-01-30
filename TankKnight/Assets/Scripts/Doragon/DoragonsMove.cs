using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
     Rigidbody rb;
    [SerializeField] float speed = 3.0f;
    [SerializeField] float cameraHeight = 3.0f; // カメラの高さ
    [SerializeField] float cameraDistance = 5.0f; // カメラとプレイヤーの距離
    [SerializeField] float rotationSpeed = 5.0f; // カメラの回転速度
    [SerializeField] Camera mainCamera; // プレイヤーに追従するカメラ
    [SerializeField] float fieldOfView = 20; // カメラのフィールドオブビュー
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        UpdateCameraPosition();
    }

    void Move()
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

        rb.velocity = moveDirection * speed;
    }

    void UpdateCameraPosition()
    {
        // カメラをプレイヤーの後ろに配置
        Vector3 offset = -transform.forward * cameraDistance + Vector3.up * cameraHeight;
        mainCamera.transform.position = transform.position + offset;

        // カメラの注視点をプレイヤーに合わせる
        mainCamera.transform.LookAt(transform.position + Vector3.up * cameraHeight);
        // カメラのフィールドオブビューを変更
        mainCamera.fieldOfView = fieldOfView;
    }
}



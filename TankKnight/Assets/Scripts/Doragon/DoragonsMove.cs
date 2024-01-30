using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
     Rigidbody rb;
    [SerializeField] float speed = 3.0f;
    [SerializeField] float cameraHeight = 3.0f; // �J�����̍���
    [SerializeField] float cameraDistance = 5.0f; // �J�����ƃv���C���[�̋���
    [SerializeField] float rotationSpeed = 5.0f; // �J�����̉�]���x
    [SerializeField] Camera mainCamera; // �v���C���[�ɒǏ]����J����
    [SerializeField] float fieldOfView = 20; // �J�����̃t�B�[���h�I�u�r���[
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
        // �v���C���[�̈ړ�����
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
        // �J�������v���C���[�̌��ɔz�u
        Vector3 offset = -transform.forward * cameraDistance + Vector3.up * cameraHeight;
        mainCamera.transform.position = transform.position + offset;

        // �J�����̒����_���v���C���[�ɍ��킹��
        mainCamera.transform.LookAt(transform.position + Vector3.up * cameraHeight);
        // �J�����̃t�B�[���h�I�u�r���[��ύX
        mainCamera.fieldOfView = fieldOfView;
    }
}



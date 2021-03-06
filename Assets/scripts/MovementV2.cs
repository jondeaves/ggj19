﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementV2 : MonoBehaviour
{
    private const float INPUT_DEADZONE = 0.19f;

    [SerializeField]
    [Tooltip("Base movement speed")]
    public float m_Speed = 0.1f;

    [SerializeField]
    [Tooltip("Base turn speed")]
    public float m_TurnSpeed = 5f;

    [SerializeField]
    [Tooltip("Which gamepad player will be bound to")]
    public int PlayerNumber = 1;

    private bool m_IsMovingPreviousFrame;
    private Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        m_IsMovingPreviousFrame = false;
        m_Animator = GetComponent<Animator>();
    }
 
    // Update is called once per frame
    void Update()
    {
        bool isMoving = Input.GetAxis("Horizontal " + PlayerNumber) > INPUT_DEADZONE || Input.GetAxis("Horizontal " + PlayerNumber) < -INPUT_DEADZONE ||
            Input.GetAxis("Vertical " + PlayerNumber) > INPUT_DEADZONE || Input.GetAxis("Vertical " + PlayerNumber) < -INPUT_DEADZONE;

        if (isMoving != m_IsMovingPreviousFrame) {
            m_Animator.SetBool("isWalking", isMoving);
            m_IsMovingPreviousFrame = isMoving;
        }

        if (!isMoving)
        {   
            return;
        }

        // m_Animator.SetBool("isWalking", true);
        Vector3 forwardVector = new Vector3(
            Input.GetAxis("Horizontal " + PlayerNumber),
            0,
            Input.GetAxis("Vertical " + PlayerNumber)
        );

        transform.position += (forwardVector * m_Speed);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(forwardVector), Time.time * m_TurnSpeed);
    }
}

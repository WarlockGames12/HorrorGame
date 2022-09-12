using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadbopScript : MonoBehaviour
{
    [Header("Bools: ")]
    [SerializeField] private bool enable = true;

    [Header("Ranges with Floats: ")]
    [SerializeField, Range(0, 0.1f)] private float amplitude = 0.015f;
    [SerializeField, Range(0, 30f)] private float frequency = 10.0f;

    [Header("Camera bops: ")]
    [SerializeField] private Transform Cameras = null;
    [SerializeField] private Transform CamerasHolder = null;
    [SerializeField] private LayerMask FloorMask;
    [SerializeField] private Transform FeetTransform;

    private float toggleSpeed = 3.0f;
    private Vector3 startPos;
    private CharacterController characterController;
    private Rigidbody rb;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        startPos = Cameras.localPosition;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!enable) return;

        CheckMotion();
        ResetPosition();
        Cameras.LookAt(FocusTarget());
    }

    private void CheckMotion()
    {
        var velocity = characterController.velocity;
        var speed = new Vector3(velocity.x, 0, velocity.z).magnitude;

        if (speed < toggleSpeed) return;
        if (!Physics.CheckSphere(FeetTransform.position, 0.1f, FloorMask)) return;

        PlayMotion(FootStepMotion());
    }

    private void ResetPosition()
    {
        if (Cameras.localPosition == startPos) return;
        Cameras.localPosition = Vector3.Lerp(Cameras.localPosition, startPos, 1 * Time.deltaTime);
    }

    private Vector3 FootStepMotion()
    {
        var pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * frequency) * amplitude;
        pos.x += Mathf.Cos(Time.time * frequency / 2) * amplitude * 2;
        return pos;
    }

    private Vector3 FocusTarget()
    {
        var position = transform.position;
        var pos = new Vector3(position.x, position.y + CamerasHolder.localPosition.y, position.z);
        pos += CamerasHolder.forward * 15.0f;
        return pos;
    }

    private void PlayMotion(Vector3 motion)
    {
        Cameras.localPosition += motion;
    }
}

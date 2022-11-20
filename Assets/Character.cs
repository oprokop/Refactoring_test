using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float gravity = -9.81f;
    public float speed = 10.0f;
    public Transform transformCamera;
    public Transform pivot;
    public GameObject bullet;

    private float ySpeed;
    private bool isJump;
    private bool shot = false;

    void Start() { }

    void Update()
    {
        var controller = GetComponent<CharacterController>();
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        float rotation = Input.GetAxis("Mouse X");

        

        Vector3 inputDirection = new Vector3(horizontal, 0.0f, vertical);
        Vector3 movement = new Vector3(horizontal * speed, gravity, vertical * speed);
        
        var inputAngle = horizontal < 0.0f ? -Vector3.Angle(Vector3.forward, inputDirection) : Vector3.Angle(Vector3.forward, inputDirection);
        var animator = GetComponent<Animator>();
        animator.SetFloat("direction", inputAngle / 180.0f);
        animator.SetFloat("idle", inputDirection.magnitude);
        Debug.Log(inputDirection + " " + inputAngle);
        movement = Quaternion.AngleAxis(transformCamera.rotation.eulerAngles.y, Vector3.up) * movement;
        

        controller.Move(movement * Time.deltaTime);
        controller.transform.Rotate(Vector3.up, rotation);
        if(Input.GetMouseButtonDown(0) && shot == false)
        {
            StartCoroutine(StartShotAnimation(animator));
        }
    }

    IEnumerator StartShotAnimation(Animator animator)
    {
        shot = true;
        animator.SetTrigger("shot");
        yield return new WaitForSeconds(0.15f);
        SpawnProjectile();
        yield return new WaitForSeconds(0.5f);
        shot = false;
    }

    void SpawnProjectile()
    {
        var bulletInstance = Instantiate(bullet, pivot);
        bulletInstance.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        bulletInstance.transform.localRotation = new Quaternion(0, 0, 0, 0);
        bulletInstance.transform.SetParent(null);
        bulletInstance.GetComponent<Rigidbody>().AddForce(bulletInstance.transform.forward * 50.0f);

        Time.timeScale = 0.25f;
    }

    private void FixedUpdate() { }

    private void LateUpdate() { }
}

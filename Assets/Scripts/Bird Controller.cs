using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class BirdController : MonoBehaviour
{
    private AudioSource myAudio;
    [SerializeField] private ColorData colorData;
    private int colorIndex = 0;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float rotationSpeed = 500f;

    private Rigidbody myRBD;
    private bool isJumping;

    public static event Action OnBirdGameOver;
    public static event Action<int> OnScoreIncreased;

    private void Awake()
    {
        myAudio = GetComponent<AudioSource>();
        myRBD = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    private void Update()
    {
        RotateBird();
    }

    private void FixedUpdate()
    {
        if (isJumping)
        {
            myRBD.velocity = new Vector3(myRBD.velocity.x, 0, 0); 
            myRBD.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isJumping = true;
        }
    }

    private void RotateBird()
    {
        float angle = Mathf.Clamp(myRBD.velocity.y * -rotationSpeed * 0.01f, -90f, 45f);
        transform.rotation = Quaternion.Euler(0, 180, angle);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            myRBD.velocity = new Vector3(myRBD.velocity.x, 0, 0);

            Vector3 forceDirection;

            if (collision.transform.position.y > transform.position.y)
            {
                forceDirection = new Vector3(0f, 4f, 0f);
                
            }
            else
            {
                forceDirection = new Vector3(0f, -4f, 0f);  
            }

            myRBD.AddForce(forceDirection, ForceMode.Impulse);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Limite"))
        {
            OnBirdGameOver?.Invoke(); 
        }
        if (other.gameObject.CompareTag("Point"))
        {
            myAudio.Play();
            OnScoreIncreased?.Invoke(1);
            CheckForColorChange();
        }
    }
    private void CheckForColorChange()
    {
        if (GameManager.Instance.GetScore() % 5 == 0) 
        {
            ChangeColor();
        }
    }
    private void ChangeColor()
    {
        colorIndex = (colorIndex + 1) % colorData.colors.Length;

        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material.SetColor("_BaseColor", colorData.colors[colorIndex]);
    }
    private void CheckGameOver()
    {
        if (transform.position.x >= -5)
        {
            OnBirdGameOver?.Invoke();
        }
    }
}

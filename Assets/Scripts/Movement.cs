using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]float thrust = 1000f;
    [SerializeField] float rotateSpeed = 1f;
    [SerializeField] AudioClip thrust_sfx;
    [SerializeField] ParticleSystem main_thrust;
    [SerializeField] ParticleSystem left_thrust;
    [SerializeField] ParticleSystem right_thrust;

    Rigidbody rb;
    AudioSource thrust_sound;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        thrust_sound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();

        void ProcessThrust()
        {
            StartThrusting();
        }
        void ProcessRotation()
        {
            StartRotating();
        }
    }

    void StartRotating()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            RotationThrust(rotateSpeed);
            left_thrust.Play();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            RotationThrust(-rotateSpeed);
            right_thrust.Play();
        }
        else
        {
            left_thrust.Stop();
            right_thrust.Stop();
        }
    }

    void StartThrusting()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!thrust_sound.isPlaying)
            {
                thrust_sound.PlayOneShot(thrust_sfx);
                main_thrust.Play();
            }

            rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
        }
        else
        {
            thrust_sound.Stop();
            main_thrust.Stop();
        }
    }

    void RotationThrust(float rotateThisframe)
    {
        rb.freezeRotation = true; //Freeze rotation so we can manually rotate
        transform.Rotate(0, 0, -1 * rotateThisframe * Time.deltaTime);
        rb.freezeRotation = false; //UnFreeze for physics system to takeover
    }

}

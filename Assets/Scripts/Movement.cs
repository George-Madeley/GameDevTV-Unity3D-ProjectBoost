using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrust = 1;
    [SerializeField] float rotationSpeed = 1;
    [SerializeField] AudioClip engineThrustAudio;

    [SerializeField] ParticleSystem mainBoosterParticle;
    [SerializeField] ParticleSystem leftBoosterParticle;
    [SerializeField] ParticleSystem rightBoosterParticle;

    Rigidbody RigidbodyComponent;
    AudioSource AudioSourceComponent;

    // Start is called before the first frame update
    void Start()
    {
        RigidbodyComponent = GetComponent<Rigidbody>();
        AudioSourceComponent = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust() {
        if (Input.GetKey(KeyCode.W))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StopThrusting()
    {
        AudioSourceComponent.Stop();
        mainBoosterParticle.Stop();
    }

    private void StartThrusting()
    {
        RigidbodyComponent.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
        if (!mainBoosterParticle.isPlaying)
        {
            mainBoosterParticle.Play();
        }
        if (!AudioSourceComponent.isPlaying)
        {
            AudioSourceComponent.PlayOneShot(engineThrustAudio);
        }
    }

    private void ProcessRotation() {
        if (Input.GetKey(KeyCode.A))
        {
            StartLeftRotation();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            StartRightRotation();
        }
        else
        {
            StopRotation();
        }
    }

    private void StopRotation()
    {
        leftBoosterParticle.Stop();
        rightBoosterParticle.Stop();
    }

    private void StartRightRotation()
    {
        ApplyRotation(-rotationSpeed);
        if (!leftBoosterParticle.isPlaying)
        {
            leftBoosterParticle.Play();
        }
        rightBoosterParticle.Stop();
    }

    private void StartLeftRotation()
    {
        ApplyRotation(rotationSpeed);
        if (!rightBoosterParticle.isPlaying)
        {
            rightBoosterParticle.Play();
        }
        leftBoosterParticle.Stop();
    }

    private void ApplyRotation(float rotationThisFrame) {
        RigidbodyComponent.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        RigidbodyComponent.freezeRotation = false;
    }
}

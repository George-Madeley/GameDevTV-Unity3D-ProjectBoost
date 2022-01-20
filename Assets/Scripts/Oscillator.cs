using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    Vector3 startingPosition;


    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) {
            Debug.LogError("Period must be larger than 0");
        } else {
            ProcessOscillation();
        }
    }

    private void ProcessOscillation()
    {
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);
        float movementFactor = (rawSinWave + 1f) / 2f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}

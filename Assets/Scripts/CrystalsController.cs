using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalsController : MonoBehaviour
{
    private bool _reversed = false;  // True for up, false for down
    public bool Reversed
    {
        get
        {
            return _reversed;
        }

        set
        {
            _reversed = value;
            _currentDirectionTime = 0;
            _trackingTime = false;
        }
    }

    public bool startingDirection = false; // True for up, false for down
    [SerializeField]
    private float _rotationSpeed = 5;
    [SerializeField]
    private float _floatSpeed = 2;
    [SerializeField]
    private float _changeDirectionStrength = 1;
    [SerializeField]
    private float _directionChangeRate;
    [SerializeField]
    private Vector3 _vectorForSpin = Vector3.up;

    private float _currentDirectionTime = 0;

    private Rigidbody _rb;

    private bool _trackingTime; // Used to determine if we are tracking to change direction yet.

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _trackingTime = false;

        if (_directionChangeRate <= 0)
        {
            _directionChangeRate = 0;
        }

        else
        {
            Reversed = startingDirection;
            StartCoroutine(DirectionChange());
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(_vectorForSpin * Time.deltaTime * _rotationSpeed);

        if (_directionChangeRate <= 0) { return; }

        if (_currentDirectionTime >= _directionChangeRate)
        {
            Reversed = !Reversed;
            StartCoroutine(DirectionChange());
        }

        else if (_trackingTime)
        {
            _currentDirectionTime += Time.deltaTime;
        }
    }

    IEnumerator DirectionChange()
    {
        if (Reversed)
        {
            while (_rb.velocity.magnitude <= _floatSpeed)
            {
                _rb.AddForce(0, _changeDirectionStrength, 0);
                yield return new WaitForEndOfFrame();
            }

            _rb.velocity = new Vector3(0, _floatSpeed);
        }

        else
        {     
            while (_rb.velocity.magnitude <= _floatSpeed)
            {
                _rb.AddForce(0, -_changeDirectionStrength, 0);
                yield return new WaitForEndOfFrame();
            }

            _rb.velocity = new Vector3(0, -_floatSpeed);
        }

        _trackingTime = true;
    }
}

using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MotionState { Run, Idle }
public class PlayerManager : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] FixedJoystick joystick;
    [SerializeField] Transform bag;
    public List<GemManager> BagList = new List<GemManager>();
    
    private Animator _animator;
    private MotionState _defaultState;
    Rigidbody _rb;
    private void Awake()
    {
        SetupPlayer();
    }

    private void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal;
        _rb.AddForce(direction * speed);
        transform.LookAt(direction*speed);
        if (direction.magnitude>0)
        {
            setState(MotionState.Run);
        }
        else
        {
            setState(MotionState.Idle);
        }
    }
    private void SetupPlayer()
    {
        _animator = GetComponent<Animator>();
        _defaultState = MotionState.Idle;
        _rb = GetComponent<Rigidbody>();
    }
    private void setState(MotionState newState)
    {
        _defaultState = newState;
        switch (_defaultState)
        {
            case MotionState.Run:
                _animator.SetBool("IsMove", true);
                break;
            case MotionState.Idle:
                _animator.SetBool("IsMove", false);
                break;
        }
    }
    public void BagSet(GemManager gem)
    {
        gem.StopGrow();
        gem.transform.localPosition = Vector3.zero;
        gem.transform.SetParent(bag);
        BagList.Add(gem);
        gem.transform.localPosition = new Vector3(0,BagList.Count-1,0);
    }
}

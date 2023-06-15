using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerManager : MonoSingleton<PlayerManager>
{
    #region Variables
    PlayerState defaultState;
    public PlayerIdleState idleState;
    public PlayerRunState runState;
    [SerializeField] float speed;
    [SerializeField] FixedJoystick joystick; 
    [SerializeField] Transform bag;
    public List<GemManager> BagList = new List<GemManager>();
    private Vector3 _direction = new Vector3(0,0,0);
    Rigidbody _rb;
    #endregion

    private void Awake()
    {
        SetupPlayer();
    }
    private void Start()
    {
        defaultState = idleState;
        defaultState.EnterState(this);
    }

    private void FixedUpdate()
    {
        _direction = Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal;
        _rb.AddForce(_direction * speed);
        transform.LookAt(_direction * speed);
        if (_direction.magnitude > 0)
        {
            
            NextState(runState);
        }
        else
        {
            
            NextState(idleState);
        }
    }
    #region Functions
    public void NextState(PlayerState state)
    {
        defaultState = state;
        state.EnterState(this);
    }
    private void SetupPlayer()
    {
        _rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Player objesi bir hucreye girdiginde alacagi gem uzerinde yapilan fonksiyonlar.
    /// Buyume asamasi duruyor
    /// Cantanin child objesi haline geliyor ve yerel pozisyon sifirlaniyor.
    /// Canta listesinin buyuklugunce pozisyonun yuksekligi hesaplamasina gore son gelen gem cantaya yerlestiriliyor
    /// </summary>
    /// <param name="gem"></param>
    public void BagSet(GemManager gem)
    {
        gem.StopGrow();
        gem.transform.localPosition = Vector3.zero;
        gem.transform.SetParent(bag);
        BagList.Add(gem);
        gem.transform.localPosition = new Vector3(0, BagList.Count - 1, 0);
    }
    #endregion

}

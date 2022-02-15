using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapons : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent onShoot;
    public UnityEvent onStartAttack;
    public UnityEvent onEndAttack;
    public UnityEvent onAlternateAttackStart;
    public UnityEvent onAlternateAttackEnd;
    public UnityEvent onReload;

    // Start is called before the first frame update
   public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
}

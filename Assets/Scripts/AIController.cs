using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIController : MonoBehaviour
{
    // Awake is called when the object is created
    public abstract void Awake();

    // Start is called before the first frame update
    public abstract void Start();

    // Update is called once per frame
    public abstract void Update();
}

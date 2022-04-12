using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dropable : MonoBehaviour
{
    [SerializeField] private UnityEvent onDrop = null;

    public void Drop()
    {
        gameObject.SetActive(true);
        transform.parent = null;

        onDrop?.Invoke();
    }
}

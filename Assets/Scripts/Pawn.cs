using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector3 moveVector)
    {
        anim.SetFloat("Right", moveVector.x);
        anim.SetFloat("Forward", moveVector.z);
    }
}

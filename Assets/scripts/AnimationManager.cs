using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private  Animator animator;
    public string animationName;
    public bool isPickingUp;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isPickingUp)
            {
                animator.StopPlayback();
                isPickingUp = false;
            }
            else
            {
                animator.StartPlayback();
                isPickingUp = true;
            }
        }
    }
}

using EightDirectionalSpriteSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PointClickController : Actor
{

    public CharacterController controller;

    public float speed = 6f;
    public Transform cam;
    public float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;

    //public LayerMask whatCanBeClicked;

    //private NavMeshAgent myAgent;

    private void Start()
    {
        cam = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        //myAgent = GetComponent<NavMeshAgent>();
        //myAgent.updateRotation = false;
    }

    private void Update()
    {
        ProcessMovement();
        Shader.SetGlobalVector("_PositionMoving", transform.position);
        //if (myAgent.velocity.sqrMagnitude > Mathf.Epsilon)
        //{
        //    transform.rotation = Quaternion.LookRotation(myAgent.velocity.normalized);
        //}

        //if (Input.GetMouseButtonDown (0))
        //{
        //    Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hitInfo;

        //    if (Physics.Raycast (myRay, out hitInfo, 100, whatCanBeClicked))
        //    {
        //        myAgent.SetDestination(hitInfo.point);
        //    }
        //}

        //if (myAgent.remainingDistance <= myAgent.stoppingDistance)
        //{
        //    if (currentState != State.IDLE)
        //        SetCurrentState(State.IDLE);
        //}
        //else
        //{
        //    if (currentState != State.WALKING)
        //        SetCurrentState(State.WALKING);
        //}
    }
    void ProcessMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);

        if (direction.magnitude >= 0.1f)
        {
            if (currentState != State.WALKING)
                SetCurrentState(State.WALKING);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        else
        {
            if (currentState != State.IDLE)
                SetCurrentState(State.IDLE);
        }
    }

}

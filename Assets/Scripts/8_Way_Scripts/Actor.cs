using UnityEngine;
using UnityEditor;
using System.Collections;

namespace EightDirectionalSpriteSystem
{
    public class Actor : MonoBehaviour
    {
        public enum State { NONE, IDLE, WALKING, SHOOT, PAIN, DIE};

        public ActorBillboard actorBillboard;

        public ActorAnimation idleAnim;
        public ActorAnimation runAnim;

        private Transform myTransform;
        private ActorAnimation currentAnimation = null;
        public State currentState = State.NONE;

        void Awake()
        {
            myTransform = GetComponent<Transform>();
        }

        void Start()
        {
            SetCurrentState(State.IDLE);
        }

        private void OnEnable()
        {
            SetCurrentState(State.IDLE);
        }

        private void OnValidate()
        {
            if (actorBillboard != null && actorBillboard.CurrentAnimation == null)
                SetCurrentState(currentState);
        }

        void Update()
        {
            if (actorBillboard != null)
            {
                actorBillboard.SetActorForwardVector(myTransform.forward);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                State nextState = currentState;
                switch (currentState)
                {
                    case State.NONE:
                        nextState = State.IDLE;
                        break;

                    case State.IDLE:
                        nextState = State.WALKING;
                        break;

                    default:
                        nextState = State.IDLE;
                        break;
                }

                SetCurrentState(nextState);
            }
           
        }

        public void SetCurrentState(State newState)
        {
            currentState = newState;
            switch (currentState)
            {

                case State.WALKING:
                    currentAnimation = runAnim;
                    break;

                default:
                    currentAnimation = idleAnim;
                    break;
            }

            if (actorBillboard != null)
            {
                actorBillboard.PlayAnimation(currentAnimation);
            }
        }
    }
}

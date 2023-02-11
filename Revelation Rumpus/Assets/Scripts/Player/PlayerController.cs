using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.limphus.revelation_rumpus
{
    [RequireComponent(typeof(CharacterController2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float walkSpeed = 3.0f;
        [SerializeField] private float runSpeed = 6.0f, crouchSpeed = 1.0f, jumpSpeed = 5.0f, gravity = 20.0f, antiBumpAmount = 40.0f;

        [Space]
        [SerializeField] private float speedSmoothRate;

        [Header("Stance Settings")]
        [SerializeField] private float standingHeight = 2.0f;
        [SerializeField] private float crouchingHeight = 1.0f;

        [Space]
        [SerializeField] private Vector3 standingCenter = new Vector3(0, 0, 0);
        [SerializeField] private Vector3 crouchingCenter = new Vector3(0, -0.5f, 0), standingCameraPosition = new Vector3(0, 0.5f, 0), crouchingCameraPosition = new Vector3(0, 0, 0);

        private CharacterController2D cc;

        private float currentSpeed;

        private bool canMove = true;

        public void ToggleCanMove(bool b)
        {
            canMove = b;
        }

        private Vector2 moveDirection = Vector2.zero;

        // Start is called before the first frame update
        void Start()
        {
            //Grabs the CharacterController2D from the object
            cc = gameObject.GetComponent<CharacterController2D>();

            //Lock Cursor - replace with a player manager later on?
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Update is called once per frame
        void Update() => Inputs();

        void Inputs()
        {


            CalculateMovement();
        }


        void CalculateMovement()
        {


            Move();
        }

        void Move()
        {
            cc.Move();
        }

        private float previousSpeed, speedI = 0f;

        void ChangeSpeed(float speed)
        {
            //if our previous speed is not our current inputed speed, reset speedI to 0
            if (previousSpeed != speed)
            {
                previousSpeed = speed;
                speedI = 0f;
            }

            //calculate our current speed by lerping between the new speed and current speed
            currentSpeed = Mathf.Lerp(currentSpeed, speed, (speedI + Time.deltaTime) * speedSmoothRate);
        }
    }
}
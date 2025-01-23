using UnityEngine;

namespace Platformer397
{
    [RequireComponent(typeof(Rigidbody))] // helps avoid issues with null references
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] private InputReader input;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Vector3 movement;

        [SerializeField] private float moveSpeed = 200f;
        [SerializeField] private float roationSpeed = 200f;

        [SerializeField] private Transform mainCam;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            mainCam = Camera.main.transform;
        }

        private void Start()
        {
            //Debug.Log("[Start]");
            input.EnablePlayerActions();
        }

        private void OnEnable()
        {
            input.Move += GetMovement;
        }

        private void OnDisable()
        {
            input.Move -= GetMovement;
        }

        private void FixedUpdate()
        {
            UpdateMovement();
        }

        private void UpdateMovement()
        {
            // auto identifaction of variable type
            // it also works for primitive types variables
            var adjustedDirection = Quaternion.AngleAxis(mainCam.eulerAngles.y, Vector3.up) * movement;
            if (adjustedDirection.magnitude > 0f)
            {
                // Handle the roation and movement
                HandleRotation(adjustedDirection);
                HandleMovement(adjustedDirection);
            }
            else
            {
                // not change the roation or movement, but need to apply rigidcody Y movement for gravity
                rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            }
        }
        private void HandleMovement(Vector3 adjustedDirection)
        {
            var velocity = adjustedDirection * moveSpeed * Time.fixedDeltaTime;
            rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
        }
        private void HandleRotation(Vector3 adjustedDirection)
        {
            var targetRotation = Quaternion.LookRotation(adjustedDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, roationSpeed * Time.deltaTime);
        }


        private void GetMovement(Vector2 move)
        {
            //Debug.Log("Input Working " + move); // check if input working or not
            movement.x = move.x;
            movement.z = move.y;

        }

    }
}

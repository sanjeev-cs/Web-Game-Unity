using Unity.Cinemachine;
using UnityEngine;

namespace Platformer397
{
    public class CameraManager : MonoBehaviour
    {
        // References to the CinemachineVirtualCameran and the transform of player
        [SerializeField] private CinemachineCamera freeLookCam;
        [SerializeField] private Transform player;

        // In Awake, lock the mouse into the game view in unity and make the cursor invisible
        private void Awake()
        {
            // Cursor.lockState = CursorLockMode.Locked;
            // Cursor.visible = false;
            if (player != null) return;
            player = GameObject.FindWithTag("Player").transform;
        }

        // On Enable, associate the transform of our player into the target of your cinemachine camera
        private void OnEnable()
        {
            freeLookCam.Target.TrackingTarget = player;
        }
    }
}
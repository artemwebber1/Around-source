using Assets.Scripts.PlayerScripts;
using UnityEngine;

namespace Assets.Scripts.CameraScripts
{
    public sealed class Focus : MonoBehaviour
    {
        [Header("Camera position controllers")]
        [SerializeField] private float yPosition;
        [SerializeField] private float zPosition;

        private Player player;
        public Player Player => player;

        private void Awake()
        {
            player = FindObjectOfType<Player>();
        }

        private void Update()
        {
            Lerp();
            transform.forward = player.Center.Position - transform.position;
        }

        private void Lerp()
        {
            Vector3 lerpTarget = player.transform.position - transform.forward * zPosition;
            transform.position = Vector3.Lerp(transform.position, lerpTarget + Vector3.up * yPosition, 6 * Time.deltaTime);
        }
    }
}

using UnityEngine;

namespace Test.CharactersActions
{
    public class MovementAction
    {
        public void UpdateMove(Transform transform, Vector3 direction, float speed)
        {
            transform.position += direction * speed * Time.deltaTime;
        }

        public void UpdateJump(Transform transform, float jumpForce)
        {
            // Assuming the player has a Rigidbody component for physics-based movement
            Rigidbody rb = transform.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Apply the jump force or gravity
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Acceleration);
            }
            else
            {
                Debug.LogWarning("Rigidbody component is missing. Jump functionality requires a Rigidbody.");
            }
        }
    }
}

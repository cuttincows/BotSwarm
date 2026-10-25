using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerInputDriver : MonoBehaviour
{
    private CharacterController characterController;
    private Vector2 lastMoveInput = Vector2.zero;
    private Vector3 correctedInput;
    public float playerSpeed = 20f;

    [Header("Aim")]
    public LayerMask aimLayers;
    public float aimOffset = 0.5f;
    public Transform spawnPos;

    [Header("Projectiles")]
    public GameObject playerProjectilePrefab;
    public float projForce = 20f;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        correctedInput = lastMoveInput;
        correctedInput.z = correctedInput.y;
        correctedInput.y = 0;
        characterController.Move(correctedInput * Time.deltaTime * playerSpeed);
    }

    public void OnMove(InputValue value)
    {
        lastMoveInput = value.Get<Vector2>();
    }

    public void OnAttack(InputValue value)
    {
        Vector3 aimTarget;
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit, Mathf.Infinity, aimLayers))
        {
            return;
        }
        aimTarget = hit.point;
        aimTarget.y += aimOffset;

        Vector3 aimDir = aimTarget - transform.position;
        aimDir.y = 0;
        Quaternion aimRot = Quaternion.Euler(aimDir);

        var proj = Instantiate(playerProjectilePrefab, spawnPos.transform.position, aimRot);
        proj.GetComponent<Rigidbody>().AddForce(aimDir * projForce + characterController.velocity * 10);
    }
}

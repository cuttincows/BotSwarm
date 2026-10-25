using UnityEngine;
using UnityEngine.VFX;

[RequireComponent (typeof(Rigidbody))]
public class SpellDriver : MonoBehaviour
{
    Vector3 startScale;
    private Rigidbody rBody;
    public float speedStretchScalar = 4f;
    float maxSpeed = 20f;
    Vector3 _adjustedScale;
    public VisualEffect Impact;

    public float spellPower = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startScale = transform.localScale;
        _adjustedScale = startScale;
        rBody = GetComponent<Rigidbody>();

        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        float t = rBody.linearVelocity.magnitude / maxSpeed;
        float speedAdjustedZ = Mathf.Lerp(startScale.z, startScale.z * speedStretchScalar, t);
        _adjustedScale.z = speedAdjustedZ;
        Debug.Log(speedAdjustedZ);
        transform.localScale = _adjustedScale;

        transform.forward = rBody.linearVelocity.normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamagable damagable;
        if ((damagable = collision.gameObject.GetComponent<IDamagable>()) != null)
        {
            damagable.Damage(spellPower);
            Impact.gameObject.SetActive(true);
            Impact.transform.SetParent(null);
            Destroy(Impact.gameObject, 1f);
            Impact.Play();
        }
        Destroy(gameObject);
    }
}

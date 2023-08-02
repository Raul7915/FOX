using UnityEngine;

public class FallowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [Range(1, 10)]
    public float smoothFactor;

    private void FixedUpdate()
    {
        Fallow();
    }

    void Fallow()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition,smoothFactor *Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}
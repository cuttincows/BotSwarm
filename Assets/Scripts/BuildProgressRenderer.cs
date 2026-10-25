using UnityEngine;

public class BuildProgressRenderer : MonoBehaviour
{
    public Vector3 buildProgressEmptyScale;
    public Vector3 buildProgressEmptyPos;

    private Vector3 buildProgressFullScale;
    private Vector3 buildProgressFullPos;
    public void Start()
    {
        buildProgressFullScale = transform.localScale;
        buildProgressFullPos = transform.localPosition;
    }

    public void UpdateProgress(float t)
    {
        transform.localScale = Vector3.Lerp(buildProgressEmptyScale, buildProgressFullScale, 1 - t);
        transform.localPosition = Vector3.Lerp(buildProgressEmptyPos, buildProgressFullPos, 1 - t);
    }
}

using UnityEngine;

[RequireComponent(typeof(Camera))]
public class LetterBoxCamera : MonoBehaviour
{
    // Aspect ratio desain game
    private const float TARGET_ASPECT = 2300f / 1080f;

    private void Start()
    {
        Camera cam = GetComponent<Camera>();

        float currentAspect = (float)Screen.width / Screen.height;
        float scaleHeight = currentAspect / TARGET_ASPECT;

        Rect rect = cam.rect;

        if (scaleHeight < 1.0f)
        {
            // Layar lebih sempit daripada desain
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
        }
        else
        {
            // Layar lebih lebar daripada desain
            float scaleWidth = 1.0f / scaleHeight;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
        }

        cam.rect = rect;
    }
}
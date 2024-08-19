using UnityEngine;

public class Hex : MonoBehaviour
{
    private bool isRotating = false;
    private int rotationState = 0;

    private const float rotationSpeed = 200;

    private void Update()
    {
        if (isRotating)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

            float delta = Mathf.Abs(transform.localRotation.eulerAngles.z - rotationState * 60);

            if (delta < rotationSpeed / 100)
            {
                isRotating = false;
                transform.localRotation = Quaternion.Euler(0, 0, rotationState * 60);
            }
        }
    }

    private void OnMouseUpAsButton()
    {
        Rotate60Degrees();
    }

    private void Rotate60Degrees()
    {
        rotationState = (rotationState + 1) % 6;
        isRotating = true;
    }

}

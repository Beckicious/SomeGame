using UnityEngine;

public class Hex : MonoBehaviour
{
    private bool isRotating = false;
    private HexDirection rotatedTop = HexDirection.N;

    private const float rotationSpeed = 200;

    [SerializeField]
    private Hex[] Neighbors = new Hex[6];
    [SerializeField]
    private bool[] Connections = new bool[6];

    private void Update()
    {
        if (isRotating)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

            float delta = Mathf.Abs(transform.localRotation.eulerAngles.z - (int)rotatedTop * 60);

            if (delta < rotationSpeed / 100)
            {
                isRotating = false;
                transform.localRotation = Quaternion.Euler(0, 0, (int)rotatedTop * 60);
            }
        }
    }

    private void OnMouseDown()
    {
        Rotate60Degrees();

        gameObject.GetComponentInParent<HexGrid>().CountBridges();
    }

    private void Rotate60Degrees()
    {
        rotatedTop = rotatedTop.Next();
        isRotating = true;
    }

    public void AddNeighbor(Hex neighbor, HexDirection dir)
    {
        Neighbors[(int)dir] = neighbor;
        neighbor.Neighbors[(int)dir.Opposite()] = this;
    }

    public void ActivateConnection(HexDirection dir)
    {
        Connections[(int)dir] = true;
        transform.GetChild((int)dir + 1).gameObject.SetActive(true);
    }

    public bool HasConnection(HexDirection dir)
    {
        int rotatedIndex = (int)dir.Add(rotatedTop);
        return Connections[rotatedIndex];
    }

    public bool HasBridge(HexDirection dir)
    {
        if (Neighbors[(int)dir] is null)
        {
            return false;
        }
        return HasConnection(dir) && Neighbors[(int)dir].HasConnection(dir.Opposite());
    }
}

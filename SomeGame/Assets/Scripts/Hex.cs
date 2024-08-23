using System;
using UnityEngine;

public class Hex : MonoBehaviour
{
    private bool isRotating = false;
    private HexDirection rotatedTop = HexDirection.N;

    private const float rotationSpeed = 200;

    private readonly Color fullyBridged = Color.Lerp(Color.green, Color.blue, 0.2f);

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

    private void RotateDirToTop(HexDirection dir)
    {
        while (rotatedTop != dir)
        {
            Rotate60Degrees();
        }
    }

    private void Rotate60Degrees()
    {
        rotatedTop = rotatedTop.Next();
        isRotating = true;

        CheckBridged();
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

    public void CheckBridged()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, fullyBridged, 1.0f / 6 * Bridgeness());
    }

    public int Bridgeness()
    {
        int i = 0;

        foreach (HexDirection dir in Enum.GetValues(typeof(HexDirection)))
        {
            if (Neighbors[(int)dir] is null)
            {
                i++;
            }
            else if (!HasConnection(dir))
            {
                i++;
            }
            else if (Neighbors[(int)dir].HasConnection(dir.Opposite()))
            {
                i++;
            }
        }

        return i;
    }

    public string ConnectionsAsString()
    {
        int connectionFlags = 0;
        for (int i = 0; i < Connections.Length; i++)
        {
            if (Connections[i])
            {
                connectionFlags |= 1 << i;
            }
        }
        return connectionFlags.ToString("D2") + ":" + ((int)rotatedTop).ToString("D1");
    }

    public void FromString(string str)
    {
        //int connectionFlags = int.Parse(str.Split(':')[0]);

        //for (int i = 0; i < Connections.Length; i++)
        //{
        //    if ((connectionFlags >> i & 1) == 1)
        //    {
        //        ActivateConnection((HexDirection)i);
        //    }
        //}

        int rotation = int.Parse(str.Split(':')[1]);
        RotateDirToTop((HexDirection)rotation);
    }
}

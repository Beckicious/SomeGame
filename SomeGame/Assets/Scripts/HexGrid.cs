using TMPro;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    public GameObject HexPrefab;
    public TextMeshProUGUI ConnectionCounter;

    private const int numXCells = 32;
    private const int numYCells = 16;

    public Hex[,] hexGrid;

    private void OnEnable()
    {
        Random.State currentState = Random.state;
        Random.InitState(1337 * 1337);
        SetupGrid();
        Random.state = currentState;

        CountBridges();
    }

    private void ClearGrid()
    {
        if (hexGrid is null)
        {
            return;
        }

        for (int i = 0; i < hexGrid.Length; i++)
        {
            Destroy(hexGrid[i % numYCells, i / numYCells].gameObject);
        }
    }
    public void SetupGrid()
    {
        ClearGrid();

        hexGrid = new Hex[numYCells, numXCells];

        for (int x = 0; x < numXCells; x++)
        {
            for (int y = 0; y < numYCells; y++)
            {
                Vector3 position;
                position.x = x * HexMetrics.HorizontalDistance - (numXCells - 1) / 2 * HexMetrics.HorizontalDistance;
                position.y = (y + x * 0.5f - x / 2) * HexMetrics.VerticalDistance - ((numYCells - 1) / 2 * HexMetrics.VerticalDistance);
                position.z = 0f;

                var hexGO = Instantiate(HexPrefab, position, Quaternion.Euler(0, 0, 0), transform);
                Hex hex = hexGO.GetComponent<Hex>(); // https://www.youtube.com/watch?v=_klGQkeE7sk
                hexGrid[y, x] = hex;

                foreach (HexDirection dir in System.Enum.GetValues(typeof(HexDirection)))
                {
                    if (Random.value > 0.6)
                    {
                        hex.ActivateConnection(dir);
                    }
                }

                // Add neighbors
                if (y > 0)
                {
                    hex.AddNeighbor(hexGrid[y - 1, x], HexDirection.S);
                }
                if (x > 0)
                {
                    if (x % 2 == 0)
                    {
                        if (y > 0)
                        {
                            hex.AddNeighbor(hexGrid[y - 1, x - 1], HexDirection.SW);
                        }
                        hex.AddNeighbor(hexGrid[y, x - 1], HexDirection.NW);
                    }
                    else
                    {
                        hex.AddNeighbor(hexGrid[y, x - 1], HexDirection.SW);
                        if (y < numYCells - 1)
                        {
                            hex.AddNeighbor(hexGrid[y + 1, x - 1], HexDirection.NW);
                        }
                    }
                }
            }
        }
    }

    public int CountBridges()
    {
        int numBridges = 0;

        for (int x = 0; x < numXCells; x++)
        {
            for (int y = 0; y < numYCells; y++)
            {
                Hex hex = hexGrid[y, x];

                if (hex.HasBridge(HexDirection.N))
                {
                    numBridges++;
                }
                if (hex.HasBridge(HexDirection.NE))
                {
                    numBridges++;
                }
                if (hex.HasBridge(HexDirection.SE))
                {
                    numBridges++;
                }
            }
        }

        ConnectionCounter.text = $"Connections: {numBridges}";
        return numBridges;
    }
}

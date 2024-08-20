using UnityEngine;

public class HexGrid : MonoBehaviour
{
    public GameObject HexPrefab;

    private const int numXCells = 32;
    private const int numYCells = 16;

    private void OnEnable()
    {
        for (int y = 0; y < numYCells; y++)
        {
            for (int x = 0; x < numXCells; x++)
            {
                Vector3 position;
                position.x = x * (HexMetrics.outerRadius * 1.5f) - ((numXCells - 1) * HexMetrics.outerRadius * 1.5f / 2);
                position.y = (y + x * 0.5f - x / 2) * (HexMetrics.innerRadius * 2f) - ((numYCells - 1) * HexMetrics.innerRadius);
                position.z = 0f;

                var hex = Instantiate(HexPrefab, position, Quaternion.Euler(0, 0, 0), transform);
                for (int i = 1; i <= 6; i++)
                {
                    hex.transform.GetChild(i).gameObject.SetActive(Random.value > 0.6);
                }
            }
        }
    }
}

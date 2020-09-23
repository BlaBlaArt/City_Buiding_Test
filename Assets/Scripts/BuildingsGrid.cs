using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsGrid : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(10, 10);

    private Building[,] grid;

    private Building flyingBuilding;
    private Camera mainCamera;

    private void Awake()
    {
        grid = new Building[GridSize.x, GridSize.y];

        mainCamera = Camera.main;
    }

    public void StartPlacingBuilding(Building BuildingPref)
    {
        if(flyingBuilding != null)
        {
            Destroy(flyingBuilding.gameObject);
        }

        flyingBuilding = Instantiate(BuildingPref);
    }

    private void Update()
    {
        if(flyingBuilding != null)
        {
            var _groundPlace = new Plane(Vector3.up, Vector3.zero);

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if(_groundPlace.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.z);

                bool available = true;

                if (x < 0 || x > GridSize.x - flyingBuilding.Size.x) available = false;
                if (y < 0 || y > GridSize.y - flyingBuilding.Size.y) available = false;

                if(available && isPlaceTaken(x, y))
                {
                    available = false;
                }

                flyingBuilding.transform.position = new Vector3(x, 0, y);
                flyingBuilding.SetTransparrent(available);

                if (available && Input.GetMouseButtonDown(0))
                {
                    PlaceFlyingBuiding(x,y);
                }
            }
        }
    }

    private bool isPlaceTaken(int Place_x, int Place_y)
    {
        for (int x = 0; x < flyingBuilding.Size.x; x++)
        {
            for (int y = 0; y < flyingBuilding.Size.y; y++)
            {
                if(grid[Place_x + x, Place_y + y] != null)
                {
                    return true;
                }
            }
        }

        return false;

    }

    private void PlaceFlyingBuiding(int Place_x, int Place_y)
    {
        for (int x = 0; x < flyingBuilding.Size.x; x++)
        {
            for (int y = 0; y < flyingBuilding.Size.y; y++)
            {
                grid[Place_x + x, Place_y + y] = flyingBuilding;
            }
        }

        flyingBuilding.SetNormal();
        flyingBuilding = null;
    }

}

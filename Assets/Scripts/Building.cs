using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Renderer MainRenederer;
    public Vector2Int Size = Vector2Int.one;

    public void SetTransparrent( bool available)
    {
        if(available)
           MainRenederer.material.color = Color.green;
        else
           MainRenederer.material.color = Color.red;
    }

    public void SetNormal()
    {
        MainRenederer.material.color = Color.white;

    }

    private void OnDrawGizmos()
    {
        for(int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                if((x + y) % 2 == 0) Gizmos.color = Color.yellow;
                else Gizmos.color = Color.blue;

                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y) , new Vector3(1,.1f,1));
            }
        }
    }
}

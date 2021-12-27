using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class Line : MonoBehaviour
{
    public Knop start;
    public Knop finish;
    List<Vector2> colliderPoints = new List<Vector2>();
    PolygonCollider2D polygon;
    [HideInInspector] public bool isDone = false;

    private void Start()
    {
        polygon = GetComponent<PolygonCollider2D>(); 
    }
    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            if (finish)
            {
                finish.TF = start.TF;
                if (GetComponent<LineRenderer>())
                {
                    Vector3[] pos = new Vector3[2] 
                    {
                        start.transform.position - new Vector3(0, 0, 1),
                        finish.transform.position - new Vector3(0, 0, 1)
                    };

                    GetComponent<LineRenderer>().SetPositions(pos);
                    colliderPoints = CalculateColliderPoints();
                    polygon.SetPath(0, colliderPoints);
                }
            }
        }

    if(isDone && (!start || !finish))
        {
            Destroy(gameObject);
        }
    }

    List<Vector2> CalculateColliderPoints()
    {
        Vector3[] positions = GetPositions();

        float width = GetComponent<LineRenderer>().startWidth;

        float m = (positions[1].y - positions[0].y) / (positions[1].x - positions[0].x);
        float deltaX = (width / 2f) * (m / Mathf.Pow(m * m + 1, 0.5f));
        float deltaY = (width / 2f) * (1 / Mathf.Pow(1 + m * m, 0.5f));

        Vector3[] offsets = new Vector3[2];
        offsets[0] = new Vector3(-deltaX, deltaY);
        offsets[1] = new Vector3(deltaX, -deltaY);

        List<Vector2> colliderpositions = new List<Vector2>
        {
            positions[0] + offsets[0],
            positions[1] + offsets[0],
            positions[1] + offsets[1],
            positions[0] + offsets[1]
        };
        return colliderpositions;
    }

    Vector3[] GetPositions()
    {
        Vector3[] positions = new Vector3[gameObject.GetComponent<LineRenderer>().positionCount];
        gameObject.GetComponent<LineRenderer>().GetPositions(positions);
        return positions;
    }

    private void OnDestroy()
    {
        if (finish)
        {
            finish.isOccu = false;
        }
    }
}


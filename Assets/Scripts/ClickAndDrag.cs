using UnityEngine;
public class ClickAndDrag : MonoBehaviour
{
    GameObject selectedObject;
    GameObject selectedKnop;
    Line line;
    Vector3 offset;
    public Material lineMaterial;
    public float lineWidth;
    float screenSize = 20;

    void Update()
    {
        resizeScreen();
        panScreen();

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {

            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);

            if (targetObject)
            {
                if (targetObject.tag == "dragable")
                {
                    selectedObject = targetObject.transform.gameObject;
                    offset = selectedObject.transform.position - mousePosition;
                }
                else if (targetObject.tag == "Output")
                {
                    Debug.Log("out");
                    selectedKnop = targetObject.transform.gameObject;
                    line = new GameObject().AddComponent<Line>();
                    line.start = selectedKnop.GetComponent<Knop>();
                }
            }
        }

        moveObject(mousePosition);

        if (Input.GetMouseButtonUp(1))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject)
            {
                if (targetObject.GetComponent<Line>())
                {
                    Destroy(targetObject.gameObject);
                }
                else if (targetObject.GetComponent<Gate>())
                {
                    Destroy(targetObject.gameObject);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (selectedObject)
            {
                selectedObject = null;
            }
            else if (selectedKnop && line)
            {
                Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
                if (targetObject && targetObject.tag == "Input")
                {
                    selectedKnop = targetObject.transform.gameObject;
                    if (!selectedKnop.GetComponent<Knop>().isOccu)
                    {
                        line.finish = selectedKnop.GetComponent<Knop>();
                        LineRenderer lr = line.gameObject.AddComponent<LineRenderer>();
                        lr.material = lineMaterial;
                        lr.SetWidth(lineWidth, lineWidth);
                        selectedKnop.GetComponent<Knop>().isOccu = true;
                        line.isDone = true;
                    }
                    else
                    {
                        Destroy(line.gameObject);
                    }
                }
                else
                {
                    Destroy(line.gameObject);
                }
                line = null;
            }

        }
    }
    void resizeScreen()
    {
        screenSize -= Input.GetAxis("Mouse ScrollWheel") * 5;
        screenSize = Mathf.Clamp(screenSize, 5, 50);
        GetComponent<Camera>().orthographicSize = screenSize;
    }
    void panScreen()
    {
        if (Input.GetMouseButton(2))
        {
            float mousex = Input.GetAxis("Mouse X") * 0.8f;
            float mouseY = Input.GetAxis("Mouse Y") * 0.8f;

            transform.position = transform.position - new Vector3(mousex, mouseY);
        }
    }
    void moveObject(Vector3 mousePosition)
    {
        if (selectedObject)
        {
            selectedObject.transform.position = mousePosition + offset;
        }
    }
}
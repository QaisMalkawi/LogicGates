using UnityEngine;

public class ItemsSpawner : MonoBehaviour
{
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject gates;
    [SerializeField] GameObject inputs;
    [SerializeField] Gate[] items;
    int GateIndex = -1;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);

        if (Input.GetMouseButtonDown(0) && !targetObject)
        {
            if (GateIndex > -1)
            {
                Gate newGate = Instantiate(items[GateIndex], Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10), transform.rotation);
                GateIndex = -1;
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventory.active)
            {
                if (gates.active)
                {
                    switchInventory(true, false, true);
                }
                else
                {
                    switchInventory(false, true, false);
                }
            }
            else
            {
                switchInventory(true, true, false);
            }
        }
    }

    public void switchInventory(bool _inventory, bool _gates, bool _inputs)
    {
        inventory.SetActive(_inventory);
        gates.SetActive(_gates);
        inputs.SetActive(_inputs);
    }
    public void UpdateIndex(int index)
    {
        GateIndex = index;
    }

    public void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 256, 512), "Press tap to open and switch between inventory pages!\n\nSelect item and press anywhere on screen to place the selected element!\n\nUse mouse scroll wheel to zoom in and out, and the mouse wheel button to pan thru screen!\n\nHold and drag left mouse button on top of any node to move it, or right click to delete it!\n\nHold and drag left mouse button from output to any input to create connection, or right click on any connection to delete it!");

        GUI.Label(new Rect(Screen.currentResolution.width - 80, Screen.currentResolution.height - 20, 80, 20), "v. 0.2.1.0beta");
    }
}

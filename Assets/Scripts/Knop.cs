using UnityEngine;

[System.Serializable]
public class Knop : MonoBehaviour
{
    [HideInInspector]public bool TF = false;
    [HideInInspector]public bool isOccu;
    public bool isInput;
    SpriteRenderer rend;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        rend.color = TF ? Color.green : Color.red;
    }

    private void OnMouseUp()
    {
        if (!isOccu)
        {
            if (isInput)
            {
                TF = !TF;
            }
        }
    }
}

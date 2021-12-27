using UnityEngine;

public class Gate : MonoBehaviour
{
    public enum GateType
    {
        AND,
        OR,
        XOR,
        NOT,
        NAND,
        NOR,
        XNOR,
        Button
    }
    public GateType gateType = new GateType();

    public Knop[] input;

    public Knop output;


    void Update()
    {
        if (gateType == GateType.AND)
        {
            output.TF = AndGate(input[0].TF, input[1].TF);
        }
        else if (gateType == GateType.OR)
        {
            output.TF = OrGate(input[0].TF, input[1].TF);
        }
        else if (gateType == GateType.NOT)
        {
            output.TF = NotGate(input[0].TF);
        }
        else if (gateType == GateType.XOR)
        {
            output.TF = XorGate(input[0].TF, input[1].TF);
        }
        else if (gateType == GateType.NAND)
        {
            output.TF = NandGate(input[0].TF, input[1].TF);
        }
        else if (gateType == GateType.NOR)
        {
            output.TF = NorGate(input[0].TF, input[1].TF);
        }
        else if (gateType == GateType.XNOR)
        {
            output.TF = XnorGate(input[0].TF, input[1].TF);
        }
        else if (gateType == GateType.Button)
        {
          output.TF = input[0].TF;
        }
    }

    bool AndGate(bool i1, bool i2)
    {
        return(i1 & i2);
    }
    bool OrGate(bool i1, bool i2)
    {
        return (i1 | i2);
    }
    bool NotGate(bool i1)
    {
        return !i1;
    }
    bool XorGate(bool i1, bool i2)
    {
        return AndGate((OrGate(i1, i2)) ,(NotGate(i1 == i2)));
    }
    bool NandGate(bool i1, bool i2)
    {
        return NotGate(AndGate(i1, i2));
    }
    bool NorGate(bool i1, bool i2)
    {
        return NotGate(OrGate(i1, i2));
    }
    bool XnorGate(bool i1, bool i2)
    {
        return NotGate(XorGate(i1, i2));
    }
}

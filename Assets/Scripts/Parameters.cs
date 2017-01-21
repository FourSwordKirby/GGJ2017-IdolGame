using UnityEngine;
using System.Collections;

public class Parameters : MonoBehaviour
{

    public enum ControllerDirection
    {
        N,
        NE,
        E,
        SE,
        S,
        SW,
        W,
        NW,
        Neutral
    };

    public static ControllerDirection vectorToDirection(Vector2 inputVector)
    {
        if (inputVector == Vector2.zero)
            return Parameters.ControllerDirection.Neutral;

        float angle = Mathf.Atan2(inputVector.y, inputVector.x) * Mathf.Rad2Deg;

        if (angle >= -22.5 && angle < 22.5)
        {
            return Parameters.ControllerDirection.E;
        }
        else if (angle >= 22.5 && angle < 67.5)
        {
            return Parameters.ControllerDirection.NE;
        }
        else if (angle >= 67.5 && angle < 112.5)
        {
            return Parameters.ControllerDirection.N;
        }
        else if (angle >= 112.5 && angle < 157.5)
        {
            return Parameters.ControllerDirection.NW;
        }
        else if (angle >= 157.5 || angle < -157.5)
        {
            return Parameters.ControllerDirection.W;
        }
        else if (angle >= -157.5 && angle < -112.5)
        {
            return Parameters.ControllerDirection.SW;
        }
        else if (angle >= -112.5 && angle < -67.5)
        {
            return Parameters.ControllerDirection.S;
        }
        else if (angle >= -67.5 && angle < -22.5)
        {
            return Parameters.ControllerDirection.SE;
        }

        return Parameters.ControllerDirection.Neutral;
    }
    

}


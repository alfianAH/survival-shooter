using UnityEngine;

public interface IFactory
{
    GameObject FactoryMethod(int tag, Vector3 position);
}
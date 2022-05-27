using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMemory : MonoBehaviour
{
    private List<Char> _inputMemory;
    // Start is called before the first frame update
    void Start()
    {
        _inputMemory = new List<Char>(3);
    }

    public void AddInputToMemory(Char _input)
    {
        _inputMemory.Insert(0, _input);
        Debug.Log(_inputMemory.Count);
    }

    public Char CheckInputMemoryLastInput()
    {
        Char _lastInput = _inputMemory[0];
        return _lastInput;
    }

    public int CheckInputMemoryCount()
    {
        int _count = _inputMemory.Count;
        return _count;
    }


    public void ClearInputMemory()
    {
        _inputMemory.Clear();
    }
}

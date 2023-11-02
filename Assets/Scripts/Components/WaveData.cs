using System.Collections.Generic;
using System.Data;
using Unity.Collections;
using Unity.Entities;

public struct WaveData : IComponentData
{
    public DynamicBuffer<IntBufferElement> WaveAmounts;
    public int Wave1Amount;
    public int Wave2Amount;
    public int Wave3Amount;
}
using System;

[Serializable] public class InventoryItem
{
    public int ID { get; private set; }
    public string Name { get; private set; }
    public int Value { get; private set; }

    public InventoryItem(int id, string name, int value)
    {
        ID = id;
        Name = name;
        Value = value;
    }
}

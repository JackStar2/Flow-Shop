using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

[Serializable]
public class list_Manager : ISerializable
{
    public List<Manager> Managers { get; set; } = new List<Manager>();
    public list_Manager() { }
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("Managers", Managers);
    }

    public list_Manager(SerializationInfo info, StreamingContext context)
    {
        Managers = (List<Manager>)info.GetValue("Managers", typeof(List<Manager>));
    }

    public void Add(Manager item)
    {
        Managers.Add(item);
    }
}
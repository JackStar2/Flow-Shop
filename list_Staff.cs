using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

[Serializable]
public class list_Staff : ISerializable
{
    public List<Staff> Staffs { get; set; } = new List<Staff>();
    public list_Staff() { }
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("Staffs", Staffs);
    }

    public list_Staff(SerializationInfo info, StreamingContext context)
    {
        Staffs = (List<Staff>)info.GetValue("Staffs", typeof(List<Staff>));
    }

    public void Add(Staff item)
    {
        Staffs.Add(item);
    }
}
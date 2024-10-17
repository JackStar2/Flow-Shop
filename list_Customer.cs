using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


[Serializable]
public class list_Customer : ISerializable
{
    public List<Customer> Customers { get; set; } = new List<Customer>();

    public list_Customer() { }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("Customers", Customers);
    }

    public list_Customer(SerializationInfo info, StreamingContext context)
    {
        Customers = (List<Customer>)info.GetValue("Customers", typeof(List<Customer>));
    }

    public void Add(Customer item)
    {
        Customers.Add(item);
    }
}
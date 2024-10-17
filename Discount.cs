using System;

public class Discount
{
    public string Discount_ID { get; set; }
    public string Discount_Type { get; set; }
    public string Discount_value { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string ApplicableProduct { get; set; }

    public Discount(string discount_ID, string discount_Type, string discount_value, DateTime startDate, DateTime endDate, string applicableProduct)
    {
        Discount_ID = discount_ID;
        Discount_Type = discount_Type;
        Discount_value = discount_value;
        StartDate = startDate;
        EndDate = endDate;
        ApplicableProduct = applicableProduct;

    }
}

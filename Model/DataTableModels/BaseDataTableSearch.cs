using System;

namespace Model.DataTableModels;

public abstract class BaseDataTableSearch : IDataTableSearch
{
    public long? Id { get; set; }
    public int? SerialNo { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}
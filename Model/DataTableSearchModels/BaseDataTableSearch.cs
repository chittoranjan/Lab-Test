using System;
using System.ComponentModel;

namespace Model.DataTableModels;

public abstract class BaseDataTableSearch : IDataTableSearch
{
    public long? Id { get; set; }

    [DisplayName("#SL")]
    public int? SerialNo { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}
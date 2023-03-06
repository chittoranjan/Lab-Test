using System;

namespace Model.DataTableSearchModels
{
    public interface IDataTableSearch
    {
        public long? Id { get; set; }
        public int? SerialNo { get; set; }
        public string Action { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}

using System.Collections.Generic;

namespace Model.DataTableModels;

public interface IDataTablePagination<TSearchVm, TSearchResult> where TSearchVm : class where TSearchResult : class, new()
{
    long Id { get; set; }
    int SerialNo { get; set; }
    string Filter { get; set; }

    int TotalItemsCount { get; set; }
    int CurrentPageNo { get; set; }
    int ItemsPerPage { get; set; }
    int[] ItemsPerPageOptions { get; set; }
    int StartPoint { get; }
    int EndPoint { get; }

    ICollection<TSearchResult> DataList { get; set; }
    TSearchVm SearchModel { get; set; }

}
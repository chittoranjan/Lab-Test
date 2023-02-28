using System;
using System.Collections.Generic;

namespace Model.DataTableModels;

public class DataTablePagination<TSearchVm, TSearchResult> : IDataTablePagination<TSearchVm, TSearchResult> where TSearchVm : class where TSearchResult : class, new()
{
    public DataTablePagination() { }

    public DataTablePagination(int currentPageNo, int itemsPerPage, int[] itemsPerPageOptions, string filter)
    {
        CurrentPageNo = currentPageNo;
        ItemsPerPage = itemsPerPage;
        ItemsPerPageOptions = itemsPerPageOptions;
        Filter = filter;
    }


    public long Id { get; set; }
    public int SerialNo { get; set; }
    public string Filter { get; set; }

    public int TotalItemsCount { get; set; }
    public int CurrentPageNo { get; set; }
    public int DisplayDataCount => DataList?.Count ?? 0;
    public int TotalPageCount
    {
        get
        {
            decimal data = 0;
            if (TotalItemsCount > 0 && ItemsPerPage > 0)
            {
                var dividedValue = ((decimal)TotalItemsCount / ItemsPerPage);
                data = Math.Ceiling(dividedValue);
            }
            return (int)data;
        }
        set { }

    }
    public int ItemsPerPage { get; set; } = 25;
    public int[] ItemsPerPageOptions { get; set; }
    public int[] ItemsPerPageReportOptions { get; set; }

    public int StartPoint
    {
        get
        {
            var data = CurrentPageNo * (ItemsPerPage = ItemsPerPage > 0 ? ItemsPerPage : 1);
            return data;
        }
        set { }
    }
    public int EndPoint => StartPoint + (ItemsPerPage = ItemsPerPage > 0 ? ItemsPerPage : 1);

    public ICollection<TSearchResult> DataList { get; set; } = new List<TSearchResult>();
    public TSearchVm SearchModel { get; set; }
    
}
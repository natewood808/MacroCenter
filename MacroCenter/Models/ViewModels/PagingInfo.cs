using System;

namespace MacroCenter.Models.ViewModels
{
    // ViewModel classes are used specifically to pass data from a
    // controller to a view. This class is used to support the
    // ProductsListViewModel.cs class. This class holds details about
    // the number of pages available, the current page, and the total number
    // of products in the repository.
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }
}

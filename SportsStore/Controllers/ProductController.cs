using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System.Linq;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        public int PageSize = 4; // Specifies we want 4 products per page, will replace with better mechanism later
        private IProductRepository repository;
        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        // View(repository.Products         This is what was done before
        // .OrderBy(p => p.ProductID)       changing how the controller
        // .Skip((page - 1) * PageSize)     sent data to the view
        // .Take(PageSize));
        public ViewResult List(string category, int page = 1)
            => View(new ProductsListViewModel
            {
                Products = repository.Products
                           .Where(p => category == null || p.Category == category)
                           .OrderBy(p => p.ProductID)
                           .Skip((page - 1) * PageSize)
                           .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Count()
                },
                CurrentCategory = category
            });
    }
}

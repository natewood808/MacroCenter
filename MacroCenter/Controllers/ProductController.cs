using Microsoft.AspNetCore.Mvc;
using MacroCenter.Models;
using MacroCenter.Models.ViewModels;
using System.Linq;

namespace MacroCenter.Controllers
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
        public ViewResult List(string category, string price, int page = 1)
        {
            ProductsListViewModel viewModel = new ProductsListViewModel();

            if (price != null && price.Equals("Descending"))
            {
                viewModel.Products = repository.Products
                                     .Where(p => category == null || p.Category == category)
                                     .OrderByDescending(p => p.Price)
                                     .Skip((page - 1) * PageSize)
                                     .Take(PageSize);
            } 
            else if (price != null && price.Equals("Ascending"))
            {
                viewModel.Products = repository.Products
                                     .Where(p => category == null || p.Category == category)
                                     .OrderBy(p => p.Price)
                                     .Skip((page - 1) * PageSize)
                                     .Take(PageSize);
            } 
            else
            {
                viewModel.Products = repository.Products
                                     .Where(p => category == null || p.Category == category)
                                     .OrderBy(p => p.ProductID)
                                     .Skip((page - 1) * PageSize)
                                     .Take(PageSize);
            }

            viewModel.PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = category == null ? repository.Products.Count() : repository.Products.Where(x => x.Category == category).Count()
            };

            viewModel.CurrentCategory = category;
            viewModel.CurrentPriceSort = price;
            
            return View(viewModel);
        }
    }
}

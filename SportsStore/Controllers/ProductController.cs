using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
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

        public ViewResult List(int page = 1) => View(repository.Products
                                                .OrderBy(p => p.ProductID)
                                                .Skip((page - 1) * PageSize)
                                                .Take(PageSize));
    }
}

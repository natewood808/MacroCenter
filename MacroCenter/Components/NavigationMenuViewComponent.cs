using Microsoft.AspNetCore.Mvc;
using MacroCenter.Models;
using System.Linq;

namespace MacroCenter.Components
{
    /// <summary>
    /// This is a view component, a class that provides a small amount of reusable
    /// application logic with the ability to display Razor partial views.
    /// 
    /// They handle providing application logic to support partial views, and
    /// inject small fragments of HTML or JSON into a parent view. 
    /// 
    /// Components are used to embed content in views that are not related to the primary purpose
    /// of the application. Such as site navigation tools or letting the user login
    /// without visiting a separate page.
    /// 
    /// Components can provide data to a partial view, rather than the partial view
    /// getting the data it needs from the parent view or the action that renders it.
    /// 
    /// A view component can be thought of as a specific action, but does not receive
    /// HTTP requests.
    /// </summary>
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IProductRepository repository;

        /// <summary>
        /// Whenever MVC needs an instance of NavigationMenuViewComponent it will go to the
        /// configuration in the Startup class to find a class that implements IProductRepository
        /// to use to instantiate this view component. This is the same dependency injection feature
        /// we used in ProductController.cs. This is done so this view component doesn't need
        /// to know which repository implementation to use.
        /// </summary>
        /// <param name="repo"></param>
        public NavigationMenuViewComponent(IProductRepository repo)
        {
            repository = repo;
        }
        /// <summary>
        /// This method is called whenever this view component is used in a Razor view.
        /// </summary>
        /// <returns></returns>
        public IViewComponentResult Invoke()
        {
            // This will ultimately be used to provide feedback to the user about
            // what category is currently selected. On a real project we would pass the
            // selected category to the view by using a new view model class. But this will do.
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            // The LINQ expression returns an IEnumberable<string> object, which is model
            // data passed into the Default.cshtml partial view.
            return View(repository.Products
                        .Select(x => x.Category)
                        .Distinct()
                        .OrderBy(x => x));
        }
    }
}

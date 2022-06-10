using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeduShop.Common;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class HomeController : Controller
    {
        IProductCategoryService _productCategoryService;
        IProductService _productService;
        ICommonService _commonService;

        public HomeController(IProductCategoryService productCategoryService,
            IProductService productService,
            ICommonService commonService)
        {
            _productCategoryService = productCategoryService;
            _commonService = commonService;
            _productService = productService;
        }

        [OutputCache(Duration = 60, Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult Index()
        {
            //var slideModel = _commonService.GetSlides();
            //var slideView = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slideModel);
            var homeViewModel = new HomeViewModel();
            //homeViewModel.Slides = slideView;

            homeViewModel.HomeProduct = _productService.GetHomeProduct();
            var lastestProductModel = _productService.GetLastest();
            var videos = _productService.GetVideos();
            var topSaleProductModel = _productService.GetHotProduct(3);
            homeViewModel.ListCategory = _productCategoryService.GetListCategory();
            homeViewModel.ProductViewMax = _productService.GetProductViewMax();
            var topSaleProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(topSaleProductModel);
            homeViewModel.LastestProducts = lastestProductModel;
            homeViewModel.TopSaleProducts = topSaleProductViewModel;
            homeViewModel.Videos = videos;

            try
            {
                homeViewModel.Title = _commonService.GetSystemConfig(CommonConstants.HomeTitle).ValueString;
                homeViewModel.MetaKeyword = _commonService.GetSystemConfig(CommonConstants.HomeMetaKeyword).ValueString;
                homeViewModel.MetaDescription = _commonService.GetSystemConfig(CommonConstants.HomeMetaDescription).ValueString;
            }
            catch
            {
               
            }

            return View(homeViewModel);
        }


        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult FooterXipo()
        {
            var footerModel = _commonService.GetFooter();
            var footerViewModel = Mapper.Map<Footer, FooterViewModel>(footerModel);
            return PartialView(footerViewModel);
        }

        [ChildActionOnly]
        public ActionResult HeaderXipo()
        {
            return PartialView();
        }
        [OutputCache(Duration = 3600)]
        public ActionResult Category(string categoryAlias)
        {
            bool isUrlValid = _productService.CheckUrlIsValid(categoryAlias);
            if (!isUrlValid)
            {
                return HttpNotFound();
            }
            var homeViewModel = new HomeViewModel();
            var lastestProductModel = _productService.GetLastest();
            var videos = _productService.GetVideos();
            homeViewModel.ListCategory = _productCategoryService.GetListCategory();
            homeViewModel.ProductViewMax = _productService.GetProductViewMax();
            homeViewModel.LastestProducts = lastestProductModel;
            homeViewModel.Videos = videos;
            homeViewModel.HomeProduct = _productService.GetNewsByCategory(categoryAlias);
            return View("Index", homeViewModel);
        }
    }
}
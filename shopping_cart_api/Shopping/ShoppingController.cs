using Microsoft.AspNetCore.Mvc;
using NLog;

namespace ShoppingCart
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        static Logger _logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        [HttpGet(Name = "GetProductListController")]
        public IActionResult Get(string countryName)
        {

            try
            {
                var products = ShoppingPresenter.GetProductsList(countryName);
                return Ok(products);
            }
            catch (ShoppingException e)
            {
                _logger.Warn($"GetProductsList threw ShoppingException - {e.Message}");
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.Warn($"GetProductsList threw Exception - {e.StackTrace}");
                return BadRequest(e.Message);
            }
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        static Logger _logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        [HttpGet(Name = "GetCountryListController")]
        public IActionResult Get()
        {
            try
            {
                var countries = StaticData.Countries.Select(x => x.CountryName);
                return Ok(countries);
            }
            catch (Exception e)
            {
                _logger.Warn($"CountryController threw Exception - {e.StackTrace}");
                return BadRequest(e.Message);
            }
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class CartUpdateController : ControllerBase
    {
        static Logger _logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        [HttpPost(Name = "PostCartUpdateController")]
        public IActionResult Post(CartUpdateRequest request)
        {
            try
            {
                var cart = ShoppingPresenter.GetUpdatedCart(request);
                return Ok(cart);
            }
            catch (ShoppingException e)
            {
                _logger.Warn($"GetUpdatedCart threw ShoppingException - {e.Message}");
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.Warn($"GetUpdatedCart threw Exception - {e.StackTrace}");
                return BadRequest(e.Message);
            }
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class PostageController : ControllerBase
    {
        static Logger _logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        [HttpGet(Name = "GetPostageController")]
        public IActionResult Get(decimal subTotal, string countryName)
        {
            try
            {
                var shippingCost = ShoppingPresenter.GetShippingCost(subTotal, countryName);
                return Ok(shippingCost);
            }
            catch (ShoppingException e)
            {
                _logger.Warn($"GetShippingCost threw ShoppingException - {e.Message}");
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.Warn($"GetShippingCost threw Exception - {e.StackTrace}");
                return BadRequest(e.Message);
            }
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class PlaceOrderController : ControllerBase
    {
        static Logger _logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        [HttpPost(Name = "PostOrderController")]
        public IActionResult Post(Order order)
        {
            try
            {
                var orderResult = ShoppingPresenter.ProcessOrder(order);
                return Ok(orderResult);
            }
            catch (Exception e)
            {
                _logger.Warn($"ProcessOrder threw ShoppingException - {e.StackTrace}");
                return BadRequest(e.Message);
            }
        }
    }
}
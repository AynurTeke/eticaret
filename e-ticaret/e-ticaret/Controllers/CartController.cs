using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace e_ticaret.Controllers
{
    public class CartController : Controller
    {
        public struct CartProduct
        {
            public Models.Product Product { get; set; }
            public short Count { get; set; }
            public float Total { get; set; }
        }
        public string Add(short id)
        {
            DbContextOptions<Models.eTicaretContext> options = new DbContextOptions<Models.eTicaretContext>();
            Models.eTicaretContext eTicaretContext = new Models.eTicaretContext(options);
            CustomersController customersController = new CustomersController(eTicaretContext);

            CookieOptions cookieOptions = new CookieOptions();
            string? cart = Request.Cookies["cart"];
            string[] cartItems;
            string[] itemDetails;
            short itemCount;
            string newCart = "";
            string cartItem;
            short totalCount = 0;
            bool itemExist = false;
            short productId;

            if (cart == null)
            {
                newCart = id.ToString() + ":1";
                totalCount = 1;
            }
            else
            {
                cartItems = cart.Split(',');// 9:4,37:1,56:2,18:9
                for (short i = 0; i < cartItems.Length; i++)
                {
                    cartItem = cartItems[i]; // 9:4
                    itemDetails = cartItem.Split(':');//9[0] 4[1]
                    itemCount = Convert.ToInt16(itemDetails[1]);//4
                    if (itemDetails[0] == id.ToString())
                    {
                        itemCount++;
                        itemExist = true;
                    }
                    totalCount += itemCount; // totalc = totalc + itemc
                    newCart = newCart + itemDetails[0] + ":" + itemCount.ToString();
                    if (i < cartItems.Length - 1)
                    {
                        newCart = newCart + ",";
                    }

                }
                if (itemExist == false)
                {
                    newCart = newCart + "," + id.ToString() + ":1";
                    totalCount++;
                }
            }
            cookieOptions.Path = "/";
            cookieOptions.Expires = DateTime.MaxValue;
            Response.Cookies.Append("cart", newCart,cookieOptions);
            if (this.HttpContext.Session.GetString("customer") != null)
            {
                customersController.TransferCart(Convert.ToInt64(this.HttpContext.Session.GetString("customer")),eTicaretContext, this.HttpContext, "");
            }
            return totalCount.ToString();
        }
        public IActionResult Index()
        {
            DbContextOptions<Models.eTicaretContext> options = new DbContextOptions<Models.eTicaretContext>();
            Models.eTicaretContext eTicaretContext = new Models.eTicaretContext(options);
            Areas.Seller.Controllers.ProductsController productsController = new Areas.Seller.Controllers.ProductsController(eTicaretContext);
            Models.Product product;

            CookieOptions cookieOptions = new CookieOptions();
            short productId;
            short i = 0;
            string? cart = Request.Cookies["cart"];
            string[] cartItems;
            string[] itemDetails;
            short itemCount;
            string cartItem;
            List<CartProduct> cartProducts= new List<CartProduct>();
            float cartTotal = 0;
            if (cart == null)
            {
                ViewBag.message = "Sepetinizde ürün bulunmamaktadır.";
                return View();
            }
            else
            {
                cartItems = cart.Split(',');// 9:4,37:1,56:2,18:9
                for  (i = 0; i < cartItems.Length; i++)
                {
                    cartItem = cartItems[i]; // 9:4
                    itemDetails = cartItem.Split(':');//9[0] 4[1]
                    CartProduct cartProduct = new CartProduct();
                    productId = Convert.ToInt16(itemDetails[0]);
                    product = productsController.Product(productId);
                    cartProduct.Product = product;
                    cartProduct.Count= Convert.ToInt16(itemDetails[1]);
                    cartProduct.Total = cartProduct.Count * product.ProductPrice;
                    cartTotal += cartProduct.Total;
                    cartProducts.Add(cartProduct);
                }
                ViewData["product"] = cartProducts;
                ViewData["cartTotal"] = cartTotal;

            }
            cookieOptions.Path = "/";
            return View();
        }
        public string CalculateTotal(long id, byte count)
        {
            DbContextOptions<Models.eTicaretContext> options = new DbContextOptions<Models.eTicaretContext>();
            Models.eTicaretContext eTicaretContext = new Models.eTicaretContext(options);
            Areas.Seller.Controllers.ProductsController productsController = new Areas.Seller.Controllers.ProductsController(eTicaretContext);
            Models.Product product= productsController.Product(id);

            float subTotal = 0;
            subTotal = product.ProductPrice * count;
            ChangeCookie(id, count);
            return subTotal.ToString();
        }

        private void ChangeCookie(long id, byte count)
        {
            DbContextOptions<Models.eTicaretContext> options = new DbContextOptions<Models.eTicaretContext>();
            Models.eTicaretContext eTicaretContext = new Models.eTicaretContext(options);
            CustomersController customersController = new CustomersController(eTicaretContext);


            string? cart = Request.Cookies["cart"];
            string[] cartItems;
            string[] itemDetails;
            short itemCount=0;
            string newCart = "";
            string cartItem;
            short totalCount = 0;
            CookieOptions cookieOptions = new CookieOptions();

            cartItems = cart.Split(',');
            for (short i = 0; i < cartItems.Length; i++)
            {
                cartItem = cartItems[i];
                itemDetails = cartItem.Split(':');
                itemCount = Convert.ToInt16(itemDetails[1]);
                if (itemDetails[0] == id.ToString())
                {
                    itemCount = count;
                }
                if (itemCount == 0)
                {
                    continue;
                }
                totalCount += itemCount;
                newCart = newCart + itemDetails[0] + ":" + itemCount.ToString();

                if (i < cartItems.Length - 1)
                {
                    newCart = newCart + ",";
                }
            }
            if (newCart != "")
            {
                if (newCart.Substring(newCart.Length - 1) == ",")
                {
                    newCart = newCart.Substring(0, newCart.Length - 1);
                }
            }
            else
            {
                Response.Cookies.Delete("cart");
                return;
            }

            cookieOptions.Path = "/";
            cookieOptions.Expires = DateTime.MaxValue;
            Response.Cookies.Append("cart", newCart, cookieOptions);
            if (this.HttpContext.Session.GetString("customer") != null)
            {
                customersController.TransferCart(Convert.ToInt64(this.HttpContext.Session.GetString("customer")),eTicaretContext, this.HttpContext, "");
            }
        }

        public void EmptyBasket()
        {
            DbContextOptions<Models.eTicaretContext> options = new DbContextOptions<Models.eTicaretContext>();
            Models.eTicaretContext eTicaretContext = new Models.eTicaretContext(options);
            CustomersController customersController = new CustomersController(eTicaretContext);

            Response.Cookies.Delete("cart");
            if (this.HttpContext.Session.GetString("customer") != null)
            {
                customersController.TransferCart(Convert.ToInt64(this.HttpContext.Session.GetString("customer")), eTicaretContext, this.HttpContext, "");
            }
            Response.Redirect("Index");
        }
    }
}

using ContosoTravel.Web.Application.Interfaces;
using ContosoTravel.Web.Application.Models;
using System.Collections.Generic;
using System.Linq;

namespace ContosoTravel.Web.Host.MVC.FullFramework
{
    public class ASPNetFullCartCookieProvider : ICartCookieProvider
    {
        public string GetCartCookie()
        {
            var currentContext = System.Web.HttpContext.Current;
            string cartId = currentContext.Request.Cookies.Get("CartId")?.Value ?? string.Empty;
            if (string.IsNullOrEmpty(cartId))
            {
                CartModel currentCart = new CartModel();
                cartId = currentCart.Id.ToString("n");
                currentContext.Response.Cookies.Add(new System.Web.HttpCookie("CartId", cartId));
            }
            return cartId;
        }

        public void SetCartCookie(string cookieId)
        {
            var currentContext = System.Web.HttpContext.Current;
            currentContext.Response.Cookies.Add(new System.Web.HttpCookie("CartId", cookieId));
        }
    }
}

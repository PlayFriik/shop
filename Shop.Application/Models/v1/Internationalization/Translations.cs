namespace Shop.Application.Models.v1.Internationalization;

public class Translations
{
    public Areas Areas { get; set; } = new();
    public Common Common { get; set; } = new();
    public Views Views { get; set; } = new();
}

public class Areas
{
    public Identity Identity { get; set; } = new();
}
    
public class Identity
{
    public Pages Pages { get; set; } = new();
}
    
public class Pages
{
    public Account Account { get; set; } = new();
}
    
public class Account
{
    public AccountLogin Login { get; set; } = new();
    public AccountRegister Register { get; set; } = new();
}
    
public class AccountLogin
{
    public string Loading { get; set; } = Base.Domain.Resources.WebApp.Areas.Identity.Pages.Account.Login.Loading;
    public string LogIn { get; set; } = Base.Domain.Resources.WebApp.Areas.Identity.Pages.Account.Login.LogIn;
    public string Email { get; set; } = Base.Domain.Resources.WebApp.Areas.Identity.Pages.Account.Login.Email;
    public string Password { get; set; } = Base.Domain.Resources.WebApp.Areas.Identity.Pages.Account.Login.Password;
    public string RegisterNewUser { get; set; } = Base.Domain.Resources.WebApp.Areas.Identity.Pages.Account.Login.RegisterNewUser;
    public string Success { get; set; } = Base.Domain.Resources.WebApp.Areas.Identity.Pages.Account.Login.Success;
    public string UseLocalAccount { get; set; } = Base.Domain.Resources.WebApp.Areas.Identity.Pages.Account.Login.UseLocalAccount;
}
    
public class AccountRegister
{
    public string ButtonRegister { get; set; } = Base.Domain.Resources.WebApp.Areas.Identity.Pages.Account.Register.ButtonRegister;
    public string ConfirmPassword { get; set; } = Base.Domain.Resources.WebApp.Areas.Identity.Pages.Account.Register.ConfirmPassword;
    public string CreateNewAccount { get; set; } = Base.Domain.Resources.WebApp.Areas.Identity.Pages.Account.Register.CreateNewAccount;
    public string Email { get; set; } = Base.Domain.Resources.WebApp.Areas.Identity.Pages.Account.Register.Email;
    public string FirstName { get; set; } = Base.Domain.Resources.WebApp.Areas.Identity.Pages.Account.Register.FirstName;
    public string LastName { get; set; } = Base.Domain.Resources.WebApp.Areas.Identity.Pages.Account.Register.LastName;
    public string Loading { get; set; } = Base.Domain.Resources.WebApp.Areas.Identity.Pages.Account.Register.Loading;
    public string PageTitle { get; set; } = Base.Domain.Resources.WebApp.Areas.Identity.Pages.Account.Register.PageTitle;
    public string Password { get; set; } = Base.Domain.Resources.WebApp.Areas.Identity.Pages.Account.Register.Password;
    public string PhoneNumber { get; set; } = Base.Domain.Resources.WebApp.Areas.Identity.Pages.Account.Register.PhoneNumber;
    public string Success { get; set; } = Base.Domain.Resources.WebApp.Areas.Identity.Pages.Account.Register.Success;
}
    
public class Common
{
    public string ErrorMessage_AttemptedValueIsInvalid { get; set; } = Base.Domain.Resources.Common.ErrorMessage_AttemptedValueIsInvalid;
    public string ErrorMessage_Email { get; set; } = Base.Domain.Resources.Common.ErrorMessage_Email;
    public string ErrorMessage_MaxLength { get; set; } = Base.Domain.Resources.Common.ErrorMessage_MaxLength;
    public string ErrorMessage_MinLength { get; set; } = Base.Domain.Resources.Common.ErrorMessage_MinLength;
    public string ErrorMessage_MissingBindRequiredValue { get; set; } = Base.Domain.Resources.Common.ErrorMessage_MissingBindRequiredValue;
    public string ErrorMessage_MissingKeyOrValue { get; set; } = Base.Domain.Resources.Common.ErrorMessage_MissingKeyOrValue;
    public string ErrorMessage_MissingRequestBodyRequiredValue { get; set; } = Base.Domain.Resources.Common.ErrorMessage_MissingRequestBodyRequiredValue;
    public string ErrorMessage_NonPropertyAttemptedValueIsInvalid { get; set; } = Base.Domain.Resources.Common.ErrorMessage_NonPropertyAttemptedValueIsInvalid;
    public string ErrorMessage_NonPropertyUnknownValueIsInvalid { get; set; } = Base.Domain.Resources.Common.ErrorMessage_NonPropertyUnknownValueIsInvalid;
    public string ErrorMessage_NonPropertyValueMustBeANumber { get; set; } = Base.Domain.Resources.Common.ErrorMessage_NonPropertyValueMustBeANumber;
    public string ErrorMessage_NotValidPhone { get; set; } = Base.Domain.Resources.Common.ErrorMessage_NotValidPhone;
    public string ErrorMessage_Range { get; set; } = Base.Domain.Resources.Common.ErrorMessage_Range;
    public string ErrorMessage_Required { get; set; } = Base.Domain.Resources.Common.ErrorMessage_Required;
    public string ErrorMessage_StringLengthMax { get; set; } = Base.Domain.Resources.Common.ErrorMessage_StringLengthMax;
    public string ErrorMessage_StringLengthMinMax { get; set; } = Base.Domain.Resources.Common.ErrorMessage_StringLengthMinMax;
    public string ErrorMessage_UnknownValueIsInvalid { get; set; } = Base.Domain.Resources.Common.ErrorMessage_UnknownValueIsInvalid;
    public string ErrorMessage_ValueIsInvalid { get; set; } = Base.Domain.Resources.Common.ErrorMessage_ValueIsInvalid;
    public string ErrorMessage_ValueMustBeANumber { get; set; } = Base.Domain.Resources.Common.ErrorMessage_ValueMustBeANumber;
    public string ErrorMessage_ValueMustNotBeNull { get; set; } = Base.Domain.Resources.Common.ErrorMessage_ValueMustNotBeNull;
}

public class Views
{
    public Cart Cart { get; set; } = new();
    public Categories Categories { get; set; } = new();
    public Checkout Checkout { get; set; } = new();
    public Home Home { get; set; } = new();
    public Locations Locations { get; set; } = new();
    public OrderProducts OrderProducts { get; set; } = new();
    public Orders Orders { get; set; } = new();
    public Pictures Pictures { get; set; } = new();
    public Products Products { get; set; } = new();
    public Providers Providers { get; set; } = new();
    public Shared Shared { get; set; } = new();
    public Statuses Statuses { get; set; } = new();
    public Transactions Transactions { get; set; } = new();
}

public class Cart
{
    public CartIndex Index { get; set; } = new();
    public CartShipping Shipping { get; set; } = new();
}

public class CartIndex
{
    public string Cart { get; set; } = Shop.Domain.Resources.WebApp.Views.Cart.Index.Cart;
    public string ChooseShipping { get; set; } = Shop.Domain.Resources.WebApp.Views.Cart.Index.ChooseShipping;
    public string ContinueShopping { get; set; } = Shop.Domain.Resources.WebApp.Views.Cart.Index.ContinueShopping;
    public string Price { get; set; } = Shop.Domain.Resources.WebApp.Views.Cart.Index.Price;
    public string Product { get; set; } = Shop.Domain.Resources.WebApp.Views.Cart.Index.Product;
    public string Quantity { get; set; } = Shop.Domain.Resources.WebApp.Views.Cart.Index.Quantity;
    public string Subtotal { get; set; } = Shop.Domain.Resources.WebApp.Views.Cart.Index.Subtotal;
    public string Total { get; set; } = Shop.Domain.Resources.WebApp.Views.Cart.Index.Total;
    public string YourCartIsEmpty { get; set; } = Shop.Domain.Resources.WebApp.Views.Cart.Index.YourCartIsEmpty;
}
    
public class CartShipping
{
    public string Choose { get; set; } = Shop.Domain.Resources.WebApp.Views.Cart.Shipping.Choose;
    public string GoToCheckout { get; set; } = Shop.Domain.Resources.WebApp.Views.Cart.Shipping.GoToCheckout;
    public string Location { get; set; } = Shop.Domain.Resources.WebApp.Views.Cart.Shipping.Location;
    public string Provider { get; set; } = Shop.Domain.Resources.WebApp.Views.Cart.Shipping.Provider;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Cart.Shipping.Title;
}

public class Categories
{
    public CategoriesCreate Create { get; set; } = new();
    public CategoriesDelete Delete { get; set; } = new();
    public CategoriesDetails Details { get; set; } = new();
    public CategoriesEdit Edit { get; set; } = new();
    public CategoriesIndex Index { get; set; } = new();
}
    
public class CategoriesCreate
{
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.Categories.Create.BackToList;
    public string Category { get; set; } = Shop.Domain.Resources.WebApp.Views.Categories.Create.Category;
    public string Name { get; set; } = Shop.Domain.Resources.WebApp.Views.Categories.Create.Name;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Categories.Create.Title;
}
    
public class CategoriesDelete
{
    public string AreYouSure { get; set; } = Shop.Domain.Resources.WebApp.Views.Categories.Delete.AreYouSure;
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.Categories.Delete.BackToList;
    public string Category { get; set; } = Shop.Domain.Resources.WebApp.Views.Categories.Delete.Category;
    public string Name { get; set; } = Shop.Domain.Resources.WebApp.Views.Categories.Delete.Name;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Categories.Delete.Title;
}
    
public class CategoriesDetails
{
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.Categories.Details.BackToList;
    public string Category { get; set; } = Shop.Domain.Resources.WebApp.Views.Categories.Details.Category;
    public string Edit { get; set; } = Shop.Domain.Resources.WebApp.Views.Categories.Details.Edit;
    public string Name { get; set; } = Shop.Domain.Resources.WebApp.Views.Categories.Details.Name;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Categories.Details.Title;
}
    
public class CategoriesEdit
{
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.Categories.Edit.BackToList;
    public string Category { get; set; } = Shop.Domain.Resources.WebApp.Views.Categories.Edit.Category;
    public string Name { get; set; } = Shop.Domain.Resources.WebApp.Views.Categories.Edit.Name;
    public string Save { get; set; } = Shop.Domain.Resources.WebApp.Views.Categories.Edit.Save;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Categories.Edit.Title;
}
    
public class CategoriesIndex
{
    public string Create { get; set; } = Shop.Domain.Resources.WebApp.Views.Categories.Index.Create;
    public string Delete { get; set; } = Shop.Domain.Resources.WebApp.Views.Categories.Index.Delete;
    public string Details { get; set; } = Shop.Domain.Resources.WebApp.Views.Categories.Index.Details;
    public string Edit { get; set; } = Shop.Domain.Resources.WebApp.Views.Categories.Index.Edit;
    public string Name { get; set; } = Shop.Domain.Resources.WebApp.Views.Categories.Index.Name;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Categories.Index.Title;
}
    
public class Checkout
{
    public CheckoutFailure Failure { get; set; } = new();
    public CheckoutIndex Index { get; set; } = new();
    public CheckoutSuccess Success { get; set; } = new();
}
    
public class CheckoutFailure
{
    public string GoToHomepage { get; set; } = Shop.Domain.Resources.WebApp.Views.Checkout.Failure.GoToHomepage;
    public string IfYouThinkThisIsAnError { get; set; } = Shop.Domain.Resources.WebApp.Views.Checkout.Failure.IfYouThinkThisIsAnError;
    public string Payment { get; set; } = Shop.Domain.Resources.WebApp.Views.Checkout.Failure.Payment;
    public string PaymentFailed { get; set; } = Shop.Domain.Resources.WebApp.Views.Checkout.Failure.PaymentFailed;
    public string UnfortunatelyYourPaymentFailed { get; set; } = Shop.Domain.Resources.WebApp.Views.Checkout.Failure.UnfortunatelyYourPaymentFailed;
}
    
public class CheckoutIndex
{
    public string PayWith { get; set; } = Shop.Domain.Resources.WebApp.Views.Checkout.Index.PayWith;
    public string Price { get; set; } = Shop.Domain.Resources.WebApp.Views.Checkout.Index.Price;
    public string Product { get; set; } = Shop.Domain.Resources.WebApp.Views.Checkout.Index.Product;
    public string Quantity { get; set; } = Shop.Domain.Resources.WebApp.Views.Checkout.Index.Quantity;
    public string Shipping { get; set; } = Shop.Domain.Resources.WebApp.Views.Checkout.Index.Shipping;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Checkout.Index.Title;
    public string Total { get; set; } = Shop.Domain.Resources.WebApp.Views.Checkout.Index.Total;
    public string YouWillBeRedirectedToPayPal { get; set; } = Shop.Domain.Resources.WebApp.Views.Checkout.Index.YouWillBeRedirectedToPayPal;
}

public class CheckoutSuccess
{
    public string Payment { get; set; } = Shop.Domain.Resources.WebApp.Views.Checkout.Success.Payment;
    public string PaymentWasSuccessful { get; set; } = Shop.Domain.Resources.WebApp.Views.Checkout.Success.PaymentWasSuccessful;
    public string ViewOrder { get; set; } = Shop.Domain.Resources.WebApp.Views.Checkout.Success.ViewOrder;
    public string YourOrderHasBeenPlaced { get; set; } = Shop.Domain.Resources.WebApp.Views.Checkout.Success.YourOrderHasBeenPlaced;
}
    
public class Home
{
    public HomeIndex Index { get; set; } = new();
}
    
public class HomeIndex
{
    public string AddToCart { get; set; } = Shop.Domain.Resources.WebApp.Views.Home.Index.AddToCart;
    public string BestSellers { get; set; } = Shop.Domain.Resources.WebApp.Views.Home.Index.BestSellers;
    public string NowAvailable { get; set; } = Shop.Domain.Resources.WebApp.Views.Home.Index.NowAvailable;
    public string NowAvailablePrice { get; set; } = Shop.Domain.Resources.WebApp.Views.Home.Index.NowAvailablePrice;
    public string OutOfStock { get; set; } = Shop.Domain.Resources.WebApp.Views.Home.Index.OutOfStock;
    public string TakeALook { get; set; } = Shop.Domain.Resources.WebApp.Views.Home.Index.TakeALook;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Home.Index.Title;
}
    
public class Locations
{
    public LocationsCreate Create { get; set; } = new();
    public LocationsDelete Delete { get; set; } = new();
    public LocationsDetails Details { get; set; } = new();
    public LocationsEdit Edit { get; set; } = new();
    public LocationsIndex Index { get; set; } = new();
}
    
public class LocationsCreate
{
    public string Address { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Create.Address;
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Create.BackToList;
    public string Location { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Create.Location;
    public string Name { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Create.Name;
    public string Provider { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Create.Provider;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Create.Title;
}
    
public class LocationsDelete
{
    public string Address { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Delete.Address;
    public string AreYouSure { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Delete.AreYouSure;
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Delete.BackToList;
    public string Location { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Delete.Location;
    public string Name { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Delete.Name;
    public string Provider { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Delete.Provider;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Delete.Title;
}
    
public class LocationsDetails
{
    public string Address { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Details.Address;
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Details.BackToList;
    public string Edit { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Details.Edit;
    public string Location { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Details.Location;
    public string Name { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Details.Name;
    public string Provider { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Details.Provider;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Details.Title;
}
    
public class LocationsEdit
{
    public string Address { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Edit.Address;
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Edit.BackToList;
    public string Location { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Edit.Location;
    public string Name { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Edit.Name;
    public string Provider { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Edit.Provider;
    public string Save { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Edit.Save;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Edit.Title;
}
    
public class LocationsIndex
{
    public string Address { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Index.Address;
    public string Create { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Index.Create;
    public string Delete { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Index.Delete;
    public string Details { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Index.Details;
    public string Edit { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Index.Edit;
    public string Name { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Index.Name;
    public string Provider { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Index.Provider;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Locations.Index.Title;
}
    
public class OrderProducts
{
    public OrderProductsCreate Create { get; set; } = new();
    public OrderProductsDelete Delete { get; set; } = new();
    public OrderProductsDetails Details { get; set; } = new();
    public OrderProductsEdit Edit { get; set; } = new();
    public OrderProductsIndex Index { get; set; } = new();
}
    
public class OrderProductsCreate
{
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Create.BackToList;
    public string Order { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Create.Order;
    public string OrderProduct { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Create.OrderProduct;
    public string Product { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Create.Product;
    public string Quantity { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Create.Quantity;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Create.Title;
}
    
public class OrderProductsDelete
{
    public string AreYouSure { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Delete.AreYouSure;
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Delete.BackToList;
    public string Order { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Delete.Order;
    public string OrderProduct { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Delete.OrderProduct;
    public string Product { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Delete.Product;
    public string Quantity { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Delete.Quantity;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Delete.Title;
}
    
public class OrderProductsDetails
{
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Details.BackToList;
    public string Edit { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Details.Edit;
    public string Order { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Details.Order;
    public string OrderProduct { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Details.OrderProduct;
    public string Product { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Details.Product;
    public string Quantity { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Details.Quantity;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Details.Title;
}
    
public class OrderProductsEdit
{
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Edit.BackToList;
    public string Order { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Edit.Order;
    public string OrderProduct { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Edit.OrderProduct;
    public string Product { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Edit.Product;
    public string Quantity { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Edit.Quantity;
    public string Save { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Edit.Save;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Edit.Title;
}
    
public class OrderProductsIndex
{
    public string Create { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Index.Create;
    public string Delete { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Index.Delete;
    public string Details { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Index.Details;
    public string Edit { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Index.Edit;
    public string Order { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Index.Order;
    public string Product { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Index.Product;
    public string Quantity { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Index.Quantity;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.OrderProducts.Index.Title;
}
    
public class Orders
{
    public OrdersCreate Create { get; set; } = new();
    public OrdersDelete Delete { get; set; } = new();
    public OrdersDetails Details { get; set; } = new();
    public OrdersEdit Edit { get; set; } = new();
    public OrdersIndex Index { get; set; } = new();
}
    
public class OrdersCreate
{
    public string AppUser { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Create.AppUser;
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Create.BackToList;
    public string DateTime { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Create.DateTime;
    public string Location { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Create.Location;
    public string Order { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Create.Order;
    public string Status { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Create.Status;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Create.Title;
    public string Total { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Create.Total;
    public string Tracking { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Create.Tracking;
}
    
public class OrdersDelete
{
    public string AppUser { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Delete.AppUser;
    public string AreYouSure { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Delete.AreYouSure;
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Delete.BackToList;
    public string DateTime { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Delete.DateTime;
    public string Location { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Delete.Location;
    public string Order { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Delete.Order;
    public string Status { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Delete.Status;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Delete.Title;
    public string Total { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Delete.Total;
    public string Tracking { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Delete.Tracking;
}
    
public class OrdersDetails
{
    public string Amount { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Details.Amount;
    public string Cart { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Details.Cart;
    public string Name { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Details.Name;
    public string Order { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Details.Order;
    public string Phone { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Details.Phone;
    public string Price { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Details.Price;
    public string Product { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Details.Product;
    public string Quantity { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Details.Quantity;
    public string Recipient { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Details.Recipient;
    public string Shipping { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Details.Shipping;
    public string Total { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Details.Total;
    public string Tracking { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Details.Tracking;
    public string Transactions { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Details.Transactions;
}
    
public class OrdersEdit
{
    public string AppUser { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Edit.AppUser;
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Edit.BackToList;
    public string DateTime { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Edit.DateTime;
    public string Location { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Edit.Location;
    public string Order { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Edit.Order;
    public string Save { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Edit.Save;
    public string Status { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Edit.Status;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Edit.Title;
    public string Total { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Edit.Total;
    public string Tracking { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Edit.Tracking;
}
    
public class OrdersIndex
{
    public string Date { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Index.Date;
    public string Order { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Index.Order;
    public string Status { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Index.Status;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Index.Title;
    public string Total { get; set; } = Shop.Domain.Resources.WebApp.Views.Orders.Index.Total;
}
    
public class Pictures
{
    public PicturesCreate Create { get; set; } = new();
    public PicturesDelete Delete { get; set; } = new();
    public PicturesIndex Index { get; set; } = new();
}
    
public class PicturesCreate
{
    public string Cancel { get; set; } = Shop.Domain.Resources.WebApp.Views.Pictures.Create.Cancel;
    public string Path { get; set; } = Shop.Domain.Resources.WebApp.Views.Pictures.Create.Path;
    public string Picture { get; set; } = Shop.Domain.Resources.WebApp.Views.Pictures.Create.Picture;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Pictures.Create.Title;
}
    
public class PicturesDelete
{
    public string AreYouSure { get; set; } = Shop.Domain.Resources.WebApp.Views.Pictures.Delete.AreYouSure;
    public string Cancel { get; set; } = Shop.Domain.Resources.WebApp.Views.Pictures.Delete.Cancel;
    public string Path { get; set; } = Shop.Domain.Resources.WebApp.Views.Pictures.Delete.Path;
    public string Picture { get; set; } = Shop.Domain.Resources.WebApp.Views.Pictures.Delete.Picture;
    public string Product { get; set; } = Shop.Domain.Resources.WebApp.Views.Pictures.Delete.Product;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Pictures.Delete.Title;
}

public class PicturesIndex
{
    public string Path { get; set; } = Shop.Domain.Resources.WebApp.Views.Pictures.Index.Path;
    public string Product { get; set; } = Shop.Domain.Resources.WebApp.Views.Pictures.Index.Product;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Pictures.Index.Title;
}
    
public class Products
{
    public ProductsCreate Create { get; set; } = new();
    public ProductsDelete Delete { get; set; } = new();
    public ProductsDetails Details { get; set; } = new();
    public ProductsEdit Edit { get; set; } = new();
    public ProductsIndex Index { get; set; } = new();
}
    
public class ProductsCreate
{
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Create.BackToList;
    public string Category { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Create.Category;
    public string Description { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Create.Description;
    public string Name { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Create.Name;
    public string Price { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Create.Price;
    public string Product { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Create.Product;
    public string Quantity { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Create.Quantity;
    public string Sold { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Create.Sold;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Create.Title;
}
    
public class ProductsDelete
{
    public string AreYouSure { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Delete.AreYouSure;
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Delete.BackToList;
    public string Category { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Delete.Category;
    public string Description { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Delete.Description;
    public string Name { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Delete.Name;
    public string Price { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Delete.Price;
    public string Product { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Delete.Product;
    public string Quantity { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Delete.Quantity;
    public string Sold { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Delete.Sold;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Delete.Title;
}
    
public class ProductsDetails
{
    public string AddToCart { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Details.AddToCart;
    public string InStock { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Details.InStock;
    public string OutOfStock { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Details.OutOfStock;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Details.Title;
}
    
public class ProductsEdit
{
    public string Add { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Edit.Add;
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Edit.BackToList;
    public string Category { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Edit.Category;
    public string Delete { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Edit.Delete;
    public string Description { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Edit.Description;
    public string Name { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Edit.Name;
    public string None { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Edit.None;
    public string Pictures { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Edit.Pictures;
    public string Price { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Edit.Price;
    public string Product { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Edit.Product;
    public string Quantity { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Edit.Quantity;
    public string Save { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Edit.Save;
    public string Sold { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Edit.Sold;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Edit.Title;
}
    
public class ProductsIndex
{
    public string AddToCart { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Index.AddToCart;
    public string CategoriesNotFound { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Index.CategoriesNotFound;
    public string Category { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Index.Category;
    public string Clear { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Index.Clear;
    public string OutOfStock { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Index.OutOfStock;
    public string Products { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Index.Products;
    public string ProductsNotFound { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Index.ProductsNotFound;
    public string SortBy { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Index.SortBy;
    public string SortByAlphabeticallyAZ { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Index.SortByAlphabeticallyAZ;
    public string SortByAlphabeticallyZA { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Index.SortByAlphabeticallyZA;
    public string SortByBestSellers { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Index.SortByBestSellers;
    public string SortByPriceHL { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Index.SortByPriceHL;
    public string SortByPriceLH { get; set; } = Shop.Domain.Resources.WebApp.Views.Products.Index.SortByPriceLH;
}
    
public class Providers
{
    public ProvidersCreate Create { get; set; } = new();
    public ProvidersDelete Delete { get; set; } = new();
    public ProvidersDetails Details { get; set; } = new();
    public ProvidersEdit Edit { get; set; } = new();
    public ProvidersIndex Index { get; set; } = new();
}
    
public class ProvidersCreate
{
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Create.BackToList;
    public string Name { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Create.Name;
    public string Price { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Create.Price;
    public string Provider { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Create.Provider;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Create.Title;
}
    
public class ProvidersDelete
{
    public string AreYouSure { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Delete.AreYouSure;
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Delete.BackToList;
    public string Name { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Delete.Name;
    public string Price { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Delete.Price;
    public string Provider { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Delete.Provider;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Delete.Title;
}
    
public class ProvidersDetails
{
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Details.BackToList;
    public string Edit { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Details.Edit;
    public string Name { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Details.Name;
    public string Price { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Details.Price;
    public string Provider { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Details.Provider;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Details.Title;
}
    
public class ProvidersEdit
{
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Edit.BackToList;
    public string Name { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Edit.Name;
    public string Price { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Edit.Price;
    public string Provider { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Edit.Provider;
    public string Save { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Edit.Save;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Edit.Title;
}
    
public class ProvidersIndex
{
    public string Create { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Index.Create;
    public string Delete { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Index.Delete;
    public string Details { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Index.Details;
    public string Edit { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Index.Edit;
    public string Name { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Index.Name;
    public string Price { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Index.Price;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Providers.Index.Title;
}
    
public class Shared
{
    public Shared_CartPartial _CartPartial { get; set; } = new();
    public Shared_LanguagePartial _LanguagePartial { get; set; } = new();
    public Shared_Layout _Layout { get; set; } = new();
    public Shared_LoginPartial _LoginPartial { get; set; } = new();
}
    
public class Shared_CartPartial
{
    public string Cart { get; set; } = Shop.Domain.Resources.WebApp.Views.Shared._CartPartial.Cart;
}
    
public class Shared_LanguagePartial
{
    public string Language { get; set; } = Base.Domain.Resources.WebApp.Views.Shared._LanguagePartial.Language;
}
    
public class Shared_Layout
{
    public string Brand { get; set; } = Shop.Domain.Resources.WebApp.Views.Shared._Layout.Brand;
    public string Products { get; set; } = Shop.Domain.Resources.WebApp.Views.Shared._Layout.Products;
    public string Seller { get; set; } = Shop.Domain.Resources.WebApp.Views.Shared._Layout.Seller;
}
    
public class Shared_LoginPartial
{
    public string Login { get; set; } = Base.Domain.Resources.WebApp.Views.Shared._LoginPartial.Login;
    public string Logout { get; set; } = Base.Domain.Resources.WebApp.Views.Shared._LoginPartial.Logout;
    public string Manage { get; set; } = Base.Domain.Resources.WebApp.Views.Shared._LoginPartial.Manage;
    public string Register { get; set; } = Base.Domain.Resources.WebApp.Views.Shared._LoginPartial.Register;
    public string Categories { get; set; } = Shop.Domain.Resources.WebApp.Views.Shared._LoginPartial.Categories;
    public string Locations { get; set; } = Shop.Domain.Resources.WebApp.Views.Shared._LoginPartial.Locations;
    public string OrderProducts { get; set; } = Shop.Domain.Resources.WebApp.Views.Shared._LoginPartial.OrderProducts;
    public string Orders { get; set; } = Shop.Domain.Resources.WebApp.Views.Shared._LoginPartial.Orders;
    public string Pictures { get; set; } = Shop.Domain.Resources.WebApp.Views.Shared._LoginPartial.Pictures;
    public string Providers { get; set; } = Shop.Domain.Resources.WebApp.Views.Shared._LoginPartial.Providers;
    public string Roles { get; set; } = Shop.Domain.Resources.WebApp.Views.Shared._LoginPartial.Roles;
    public string Statuses { get; set; } = Shop.Domain.Resources.WebApp.Views.Shared._LoginPartial.Statuses;
    public string Transactions { get; set; } = Shop.Domain.Resources.WebApp.Views.Shared._LoginPartial.Transactions;
    public string UserRoles { get; set; } = Shop.Domain.Resources.WebApp.Views.Shared._LoginPartial.UserRoles;
    public string Users { get; set; } = Shop.Domain.Resources.WebApp.Views.Shared._LoginPartial.Users;
}
    
public class Statuses
{
    public StatusesCreate Create { get; set; } = new();
    public StatusesDelete Delete { get; set; } = new();
    public StatusesDetails Details { get; set; } = new();
    public StatusesEdit Edit { get; set; } = new();
    public StatusesIndex Index { get; set; } = new();
}
    
public class StatusesCreate
{
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.Statuses.Create.BackToList;
    public string Name { get; set; } = Shop.Domain.Resources.WebApp.Views.Statuses.Create.Name;
    public string Status { get; set; } = Shop.Domain.Resources.WebApp.Views.Statuses.Create.Status;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Statuses.Create.Title;
}
    
public class StatusesDelete
{
    public string AreYouSure { get; set; } = Shop.Domain.Resources.WebApp.Views.Statuses.Delete.AreYouSure;
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.Statuses.Delete.BackToList;
    public string Name { get; set; } = Shop.Domain.Resources.WebApp.Views.Statuses.Delete.Name;
    public string Status { get; set; } = Shop.Domain.Resources.WebApp.Views.Statuses.Delete.Status;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Statuses.Delete.Title;
}
    
public class StatusesDetails
{
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.Statuses.Details.BackToList;
    public string Edit { get; set; } = Shop.Domain.Resources.WebApp.Views.Statuses.Details.Edit;
    public string Name { get; set; } = Shop.Domain.Resources.WebApp.Views.Statuses.Details.Name;
    public string Status { get; set; } = Shop.Domain.Resources.WebApp.Views.Statuses.Details.Status;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Statuses.Details.Title;
}
    
public class StatusesEdit
{
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.Statuses.Edit.BackToList;
    public string Name { get; set; } = Shop.Domain.Resources.WebApp.Views.Statuses.Edit.Name;
    public string Save { get; set; } = Shop.Domain.Resources.WebApp.Views.Statuses.Edit.Save;
    public string Status { get; set; } = Shop.Domain.Resources.WebApp.Views.Statuses.Edit.Status;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Statuses.Edit.Title;
}
    
public class StatusesIndex
{
    public string Create { get; set; } = Shop.Domain.Resources.WebApp.Views.Statuses.Index.Create;
    public string Delete { get; set; } = Shop.Domain.Resources.WebApp.Views.Statuses.Index.Delete;
    public string Details { get; set; } = Shop.Domain.Resources.WebApp.Views.Statuses.Index.Details;
    public string Edit { get; set; } = Shop.Domain.Resources.WebApp.Views.Statuses.Index.Edit;
    public string Name { get; set; } = Shop.Domain.Resources.WebApp.Views.Statuses.Index.Name;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Statuses.Index.Title;
}
    
public class Transactions
{
    public TransactionsCreate Create { get; set; } = new();
    public TransactionsDelete Delete { get; set; } = new();
    public TransactionsDetails Details { get; set; } = new();
    public TransactionsEdit Edit { get; set; } = new();
    public TransactionsIndex Index { get; set; } = new();
}
    
public class TransactionsCreate
{
    public string Amount { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Create.Amount;
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Create.BackToList;
    public string Order { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Create.Order;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Create.Title;
    public string Transaction { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Create.Transaction;
}
    
public class TransactionsDelete
{
    public string Amount { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Delete.Amount;
    public string AreYouSure { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Delete.AreYouSure;
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Delete.BackToList;
    public string Order { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Delete.Order;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Delete.Title;
    public string Transaction { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Delete.Transaction;
}
    
public class TransactionsDetails
{
    public string Amount { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Details.Amount;
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Details.BackToList;
    public string Edit { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Details.Edit;
    public string Order { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Details.Order;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Details.Title;
    public string Transaction { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Details.Transaction;
}
    
public class TransactionsEdit
{
    public string Amount { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Edit.Amount;
    public string BackToList { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Edit.BackToList;
    public string Order { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Edit.Order;
    public string Save { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Edit.Save;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Edit.Title;
    public string Transaction { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Edit.Transaction;
}
    
public class TransactionsIndex
{
    public string Amount { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Index.Amount;
    public string Create { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Index.Create;
    public string Delete { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Index.Delete;
    public string Details { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Index.Details;
    public string Edit { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Index.Edit;
    public string Order { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Index.Order;
    public string Title { get; set; } = Shop.Domain.Resources.WebApp.Views.Transactions.Index.Title;
}
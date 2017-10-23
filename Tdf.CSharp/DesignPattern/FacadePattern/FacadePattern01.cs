using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.DesignPattern.FacadePattern
{
    /*
     * Facade模式对外提供了统一的接口，而隐藏了内部细节。
     * 在网上购物的场景中，当点击提交订单按钮，
     * 与此订单相关的库存、订单确认、折扣、确认支付、完成支付、物流配送等都要做相应的动作。
     * 本篇尝试使用Facade模式，把这些类似工作者单元的动作隐藏到一类中，只要点击提交订单，余下的事情一步到位：
     */


    #region 关于库存
    public interface IInventory
    {
        void Update(int productId);
    }

    public class InventoryManager : IInventory
    {
        public void Update(int productId)
        {
            Console.WriteLine($"产品编号为{productId}的库存已更新");
        }
    }

    #endregion

    #region 关于确认订单
    public interface IOrderVerify
    {
        bool VerifyShippingAddress(int pinCode);
    }

    public class OrderVerifyManager : IOrderVerify
    {
        public bool VerifyShippingAddress(int pinCode)
        {
            Console.WriteLine($"产品可被运输至{pinCode}");
            return true;
        }
    }
    #endregion

    #region 关于打折
    public interface ICosting
    {
        float ApplyDiscounts(float originalPrice, float discountPercent);
    }

    public class CostManager : ICosting
    {
        public float ApplyDiscounts(float originalPrice, float discountPercent)
        {
            Console.WriteLine($"产品的原价为：{originalPrice}，采取的折扣为{discountPercent}%");
            return originalPrice - ((discountPercent / 100) * originalPrice);
        }
    }
    #endregion

    #region 关于确认支付和支付
    public interface IPaymentGateway
    {
        bool VerifyCardDetails(string cardNo);
        bool ProcessPayment(string cardNo, float cost);
    }

    public class PaymentGatewayManager : IPaymentGateway
    {
        public bool VerifyCardDetails(string cardNo)
        {
            Console.WriteLine($"卡号为{cardNo}的卡可以被使用");
            return true;
        }

        public bool ProcessPayment(string cardNo, float cost)
        {
            Console.WriteLine($"卡号为{cardNo}的卡支付{cardNo}元");
            return true;
        }
    }

    #endregion

    #region 关于物流
    public interface ILogistics
    {
        void ShipProduct(string productName, string shippingAddress);
    }

    public class LogisticsManager : ILogistics
    {
        public void ShipProduct(string productName, string shippingAddress)
        {
            Console.WriteLine($"产品{productName}准备发送至{shippingAddress}");
        }
    }
    #endregion

    #region 关于OrderDetails

    public class OrderDetails
    {
        public int ProductNo { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public float Price { get; set; }
        public float DiscountPercent { get; set; }
        public string Address1 { get; set; }
        public string Addres2 { get; set; }
        public int PinCode { get; set; }
        public string CardNo { get; set; }

        public OrderDetails(string productName, string prodDescription, float price,
            float discount, string address1, string address2,
            int pinCode, string cardNo)
        {
            this.ProductNo = new Random(1).Next(1, 100);
            this.ProductName = productName;
            this.ProductDescription = prodDescription;
            this.Price = price;
            this.DiscountPercent = discount;
            this.Address1 = address1;
            this.Addres2 = address2;
            this.PinCode = pinCode;
            this.CardNo = cardNo;
        }
    }
    #endregion

    #region 体现Facade模式的类

    public class OnlineShoppingFacade
    {
        public readonly IInventory Inventory = new InventoryManager();
        public readonly IOrderVerify OrderVerify = new OrderVerifyManager();
        public readonly ICosting CostManager = new CostManager();
        public readonly IPaymentGateway PaymentGateway = new PaymentGatewayManager();
        public readonly ILogistics Logistics = new LogisticsManager();

        public void SubmitOrder(OrderDetails ordeerDetails)
        {
            Inventory.Update(ordeerDetails.ProductNo);
            OrderVerify.VerifyShippingAddress(ordeerDetails.PinCode);
            ordeerDetails.Price = CostManager.ApplyDiscounts(ordeerDetails.Price, ordeerDetails.DiscountPercent);
            PaymentGateway.VerifyCardDetails(ordeerDetails.CardNo);
            PaymentGateway.ProcessPayment(ordeerDetails.CardNo, ordeerDetails.Price);
            Logistics.ShipProduct(ordeerDetails.ProductName,
                $"{ordeerDetails.Address1},{ordeerDetails.Addres2} - {ordeerDetails.PinCode}");
        }
    }
    #endregion

    #region 客户端调用
    public class FacadePattern01
    {
        public static void TestMethod()
        {
            var orderDetails = new OrderDetails("产品A",
                "清凉一夏",
                800,
                20,
                "山东省",
                "青岛市",
                1122,
                "888666999");

            var onlineShopping = new OnlineShoppingFacade();
            onlineShopping.SubmitOrder(orderDetails);

            Console.ReadKey();
        }
    }
    #endregion
}

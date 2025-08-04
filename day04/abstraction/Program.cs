
public interface IpaymentGateway
{
   
    //by default methods are abstract 
    void ProcessPayment();
    
}

class ZomatoPartner : DeliveryPlatform, IpaymentGateway
{

    public override void DeliveryOrder()
    {

        Console.WriteLine("Delivered in 20 mins ");
    }

    public  void ProcessPayment()
    {
        Console.WriteLine("Payment Gateway ( Paytm ) transaction Started ... ");
    }


}

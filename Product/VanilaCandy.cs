namespace PreMarket.Product
{
    public class VanilaCandy : CandyProductBase
    {
        public int VanilaCount { get; set; }

        public VanilaCandy(int vanilaCount, string name, decimal price, int count = 1)
            :base(name, price, count)
        {
            VanilaCount = vanilaCount;
        }
    }
}
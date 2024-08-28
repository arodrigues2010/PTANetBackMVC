namespace EsettMvcIntegration.Models
{
    public class FeeDataModel
    {
        public int Id { get; set; }
        public FeeAllDataModel feeAllDataModel{ get; set; }
    }
   public class FeeAllDataModel
    {
        public DateTime? timestamp { get; set; }
        public DateTime? timestampUTC { get; set; }
        public string? country { get; set; }
        public decimal? imbalanceFee { get; set; }
        public decimal? hourlyImbalanceFee { get; set; }
        public decimal? peakLoadFee { get; set; }
        public decimal? volumeFee { get; set; }
        public decimal? weeklyFee { get; set; }
    }
}
using Zero.Ddd.Domain.Entities;

namespace Zero.TestApp.Domain.Data
{
    /// <summary>
    /// 餐厅
    /// </summary>
    public class Restaurant : Entity<Guid>
    {

        public string Name { get; set; }

        public string RestaurantId { get; set; }

        public string Cuisine { get; set; }

        public Address Address { get; set; } = new Address();

        public string Borough { get; set; }

        public List<GradeEntry> Grades { get; set; } = new List<GradeEntry>();
    }

    /// <summary>
    /// 地址
    /// </summary>
    public class Address
    {
        public string Building { get; set; }

        public double[] Coordinates { get; set; }

        public string Street { get; set; }

        public string ZipCode { get; set; }
    }

    ///// <summary>
    ///// 评分
    ///// </summary>
    public class GradeEntry
    {
        public DateTime Date { get; set; } = DateTime.Now;

        public string Grade { get; set; }

        public float Score { get; set; }
    }
}

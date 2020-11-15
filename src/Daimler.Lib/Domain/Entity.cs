using System;

namespace Daimler.Lib.Domain
{
    public interface   IEntity
    {
        int Id { get; set; }
        string CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        string UpdatedBy { get; set; }
        DateTime UpdatedDate { get; set; }
        DateTime? ValidFor { get; set; }  
    }
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }

        // proje basit oldugu icin, ayri bir audit layer yapmaya gerek gormedim.
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime? ValidFor { get; set; } // null sa sinirsir gecerli, ileriki bir tarih varsa o tarihe kadar gecerli,
    }
}
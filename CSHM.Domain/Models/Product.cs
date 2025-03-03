using CSHM.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Domain
{
    public class Product : IEntity
    {
        public Product() { }

        public int ID { get; set; }

        public int PublisherID { get; set; }

        public int ProductTypeID { get; set; }

        public int PublishTypeID { get; set; }

        public int LanguageID { get; set; }

        public int SizeTypeID { get; set; }

        public int CoverTypeID { get; set; }

        public string ProductCode { get; set; }

        public string Title { get; set; }

        public long Price { get; set; }

        public long MaxPrice { get; set; }

        public long MinPrice { get; set; }

        public long BeforePrice { get; set; }
        
        public decimal StudyTime { get; set; } // زمان مطالعه بر حسب ساعت

        public string ISBN { get; set; }

        public string Barcode { get; set; }

        public int PublisheYear { get; set; } // سال انتشار

        public int PublishSeason { get; set; } // فصل انتشار

        public int PublishTurn { get; set; } //نوبت انتشار

        public string PublishDate { get; set; } // تاریخ انتشار

        public string MetaDescription { get; set; }

        public int PageCount { get; set; }

        public int Weight { get; set; } // برحسب گرم

        public int VisitedCount { get; set; }

        public decimal Rate { get; set; }

        public string Summary { get; set; }

        public bool IsNew { get; set; }

        public bool IsRecommended { get; set; }

        public bool IsSelected { get; set; }

        public bool IsSoon { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public int CreatorID { get; set; }

        public DateTime CreationDateTime { get; set; }

        public int? ModifierID { get; set; }

        public DateTime? ModificationDateTime { get; set; }

        public virtual Publisher Publisher { get; set; }

        public virtual ProductType ProductType { get; set; }

        public virtual PublishType PublishType { get; set; }

        public virtual Language Language { get; set; }

        public virtual SizeType SizeType { get; set; }

        public virtual CoverType CoverType { get; set; }

        //public virtual ICollection<ProductCategoryType> ProductCategoryTypes { get; set; }

        //public virtual ICollection<ProductLable> ProductLables { get; set; }

        //public virtual ICollection<ProductComment> Productomments { get; set; }

        //public virtual ICollection<ProductOccupation> ProductOccupations { get; set; }
    }
}

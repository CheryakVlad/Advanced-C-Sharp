namespace HttpHandler.Northwind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CreditCard
    {
        public int CreditCardID { get; set; }

        public string Number { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public string CardHolderName { get; set; }

        public int? Holder_EmployeeID { get; set; }

        public virtual Employee Employee { get; set; }
    }
}

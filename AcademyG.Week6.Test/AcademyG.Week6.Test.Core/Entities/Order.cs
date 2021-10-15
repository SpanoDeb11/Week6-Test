using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AcademyG.Week6.Test.Core.Entities
{
    [DataContract]
    public class Order
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public DateTime OrderDate { get; set; }

        [DataMember]
        public string OrderCode { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public int ClientId { get; set; }

        public Client Client { get; set; }
    }
}

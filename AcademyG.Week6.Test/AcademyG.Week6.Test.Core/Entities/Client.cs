using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AcademyG.Week6.Test.Core.Entities
{
    [DataContract]
    public class Client
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string ClientCode { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        public List<Order> Orders { get; set; }
    }
}

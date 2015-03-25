using System.Runtime.Serialization;

namespace Confluent.TestHarness
{
    [DataContract]
    public class Person
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Age { get; set; }
    }
}

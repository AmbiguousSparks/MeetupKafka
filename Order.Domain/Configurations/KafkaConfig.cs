using System.Collections.Generic;

namespace Order.Domain.Configurations
{
    public class KafkaConfig
    {
        public string BootstrapServer { get; set; }
        public List<string> Topics { get; set; }
    }
}

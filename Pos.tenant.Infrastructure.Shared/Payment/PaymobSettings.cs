using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Infrastructure.Shared.Payment
{
    public class PaymobSettings
    {
        public string BaseUrl { get; set; } = null!;

        public string SecretKey { get; set; } = null!;

        public string PublicKey { get; set; } = null!;

        public string HmacSecret { get; set; } = null!;

        public int IntegrationId { get; set; }
    }
}

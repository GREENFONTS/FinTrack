using System.ComponentModel.DataAnnotations;

namespace Bank_Apis.Model
{
    public class ServiceKeys
    {
        [Key]
        public string UserId { get; set; } = null;

        public string MonoPrivateKey { get; set; } = null;

        public string MonoSecretKey { get; set; } = null;

        public string FlutterKey { get; set; } = null;

    }
}

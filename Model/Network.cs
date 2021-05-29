using System;

namespace GenoBankIo
{
    public struct Network
    {
        private Network(
            String namespacePrefix,
            String certificateUrlBase,
            String apiUrlBase,
            String genoBankIoAddress) {
            this.namespacePrefix = namespacePrefix;
            this.certificateUrlBase = new Uri(certificateUrlBase);
            this.apiUrlBase = new Uri(apiUrlBase);
            this.genoBankIoAddress = genoBankIoAddress;
        }

        public static Network Test() {
            return new Network(
                "io.genobank.test", 
                "https://genobank.io/test/certificates/verify-certificate-v1#",
                "https://api-test.genobank.io/",
                "0x795faFFc58648e435E3bD3196C4F75F8EFc4b306"
            );
        }

        public static Network Production() {
            return new Network(
                "io.genobank",
                "https://genobank.io/certificates/verify-certificate-v1#",
                "https://api.genobank.io/",
                "0x633F5500A87C3DbB9c15f4D41eD5A33DacaF4184"
            );
        }
 
        public string namespacePrefix { get; }
        public Uri certificateUrlBase { get; }
        public Uri apiUrlBase { get; }
        public String genoBankIoAddress { get; }
    }
}
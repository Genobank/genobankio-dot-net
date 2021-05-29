using Nethereum.HdWallet;
using Nethereum.Signer;
using Nethereum.Web3.Accounts;

namespace GenoBankIo
{
    public class PermitteeSigner {

        // Load an Ethereum wallet: https://nethereum.readthedocs.io/en/latest/nethereum-managing-hdwallets/
        public PermitteeSigner(string twelveWordPassphrase, uint permitteeId) {
            string mnemonicPassword = "";
            Wallet wallet = new Wallet(twelveWordPassphrase, mnemonicPassword);
            this.credentials = wallet.GetAccount(0);
            this.permitteeId = permitteeId;
        }

        public byte[] sign(PermitteeRepresentations representations) {
            string message = representations.getFullSerialization();
            EthECKey privateKey = new EthECKey(credentials.PrivateKey);
            // Match the signature output format as Ethers.js v5.0.31
            var signer = new EthereumMessageSigner();
            string output = signer.EncodeUTF8AndSign(message, privateKey);
            return HexUtil.ToBytes(output);
        }

        public Account credentials { get; }
        public uint permitteeId { get; }
    }
}
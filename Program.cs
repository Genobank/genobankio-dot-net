using System;
using System.Threading.Tasks;
using Nethereum.Web3;
using Nethereum.Util;

using System;
using System.Numerics;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.Util;
using System.Threading.Tasks;
using NBitcoin;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.HdWallet;



using System;
using System.Text;
using System.Collections.Generic;
using Nethereum.Util;
using Nethereum.Signer;
using Nethereum.Hex.HexConvertors.Extensions;



// JSON REST Client // https://github.com/dotnet/samples/blob/main/csharp/getting-started/console-webapiclient/Program.cs
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace NethereumSample
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            var repositories = await ProcessRepositories();

            foreach (var repo in repositories)
            {
                Console.WriteLine(repo.Name);
                Console.WriteLine(repo.Description);
                Console.WriteLine(repo.GitHubHomeUrl);
                Console.WriteLine(repo.Homepage);
                Console.WriteLine(repo.Watchers);
                Console.WriteLine(repo.LastPush);
                Console.WriteLine();
            }
        }

        private static async Task<List<Repository>> ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);
            return repositories;
        }




/*        
        static async Task Main(string[] args)
        {

            
            // Try web3 online
            GetAccountBalance().Wait(); 



            // Try SHA256 ///////////////////////////////////////////////////////////////
            var hash = Sha3Keccack.Current.CalculateHash("Hello World");
            Console.WriteLine($"Hash: {hash}");



            // Try wallet ////////////////////////////////////////////////////////////////

        // This samples shows how to create an HD Wallet using BIP32 standard in Ethereum.
        // For simpler context, this allows you to recover your accounts and private keys created with a seed set of words
        // For example Metamask uses 12 words
        // 
        //Nethereum uses internally NBitcoin to derive the private and public keys, for more information on BIP32 check
        //https://programmingblockchain.gitbook.io/programmingblockchain/key_generation/bip_32

        //Initiating a HD Wallet requires a list of words and an optional password to add further entropy (randomness)

        var words = "ripple scissors kick mammal hire column oak again sun offer wealth tomorrow wagon turn fatal";
        //Note: do not confuse the password with your Metamask password, Metamask password is used to secure the storage
        var password = "password";
        var wallet = new Wallet(words, password);

        // An HD Wallet is deterministic, it will derive the same number of addresses 
        // given the same seed (wordlist + optional password).

        // All the created accounts can be loaded in a Web3 instance and used as any other account, 
        // we can for instance check the balance of one of them:

        var account = new Wallet(words, password).GetAccount(0);
        Console.WriteLine("The account address is: " + account.Address);

        var web3 = new Web3(account, "http://testchain.nethereum.com:8545");
        //we connect to the Nethereum testchain which has already the account preconfigured with some Ether balance.
        var balance = await web3.Eth.GetBalance.SendRequestAsync(account.Address);
        Console.WriteLine("The account balance is: " + balance.Value);

        //Or transfer some Ether, as the account already has the private key required to sign the transactions.

        var toAddress = "0x13f022d72158410433cbd66f5dd8bf6d2d129924";
        var transactionReceipt = await web3.Eth.GetEtherTransferService()
            .TransferEtherAndWaitForReceiptAsync(toAddress, 2.11m, 2);
        Console.WriteLine($"Transaction {transactionReceipt.TransactionHash} for amount of 2.11 Ether completed");



        // Try sign message ////////////////////////////////////////////////////////
                    
        //This sample demonstrates how to sign a message with a private key 
        //and validate the signer later on by recovering the address of the signature.

        //Given an address with a private key
        var address = "0x94618601FE6cb8912b274E5a00453949A57f8C1e";
        var privateKey = "0x7580e7fb49df1c861f0050fae31c2224c6aba908e116b8da44ee8cd927b990b0";
        Console.WriteLine($"Address {address} with private key: {privateKey}");

        //And a message to sign
        var messageToSign = "wee test message 18/09/2017 02:55PM";

        //We can create an Ethereum signer. 
        //Note: the EthereumSigner is a specialised signer for messages that prefixes the messages with "x19Ethereum Signed Message:"
        var signer = new EthereumMessageSigner();

        //To sign a text message we UTF8 encoded it (byte array) first and then sign it
        var signature = signer.EncodeUTF8AndSign(messageToSign, new EthECKey(privateKey));
        Console.WriteLine($"Signature: {signature}");

        //To recover the address of the signer of a text message 
        //we first UTF8 encoded it (byte array) and then using the Elliptic Curve recovery we get the address.
        var addressRecovered = signer.EncodeUTF8AndEcRecover(messageToSign, signature);

        //We can validate the signature if the address matches
        Console.WriteLine($"Address recovered: {addressRecovered}");










            Console.ReadLine();
            
        }

        static async Task GetAccountBalance()
        {
var web3 = new Web3("https://mainnet.infura.io/v3/7238211010344719ad14a89db874158c");
            var balance = await web3.Eth.GetBalance.SendRequestAsync("0xde0b295669a9fd93d5f28d9ec85e40f4cb697bae");
            Console.WriteLine($"Balance in Wei: {balance.Value}");

            var etherAmount = Web3.Convert.FromWei(balance.Value);
            Console.WriteLine($"Balance in Ether: {etherAmount}");
        }
*/
    }



}
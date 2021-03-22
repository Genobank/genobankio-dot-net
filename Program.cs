using System;

using System.Threading.Tasks;
using Nethereum.Util;

namespace genobankio_dot_net
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                showHelp();
                return;
            }

            // This is a useless operation to ensure the dependency is working
            var hash = Sha3Keccack.Current.CalculateHash("Hello World");

            // This output is an example in the correct format
            string certificateUrl = await generateCertificate();
            Console.WriteLine(certificateUrl);
        }

        private static async Task<string> generateCertificate()
        {
            await Task.Delay(4000);
            return "https://genobank.io/test/certificates/verify-certificate-v1#Tadej%20Vengust%7CMX234234234%7C1%7CN%7C%7C1613144520000%7C12%7C0xb0960d5ea99687e3bdcadaf77d731d898bab5cdfd2e89aea3982481e79b3b84d3d50c19f579dc38102903e7d8f2445cb22ba8f1f2533bd15bcc971e66720e39b1c%7C1613144554319%7C0x21eb267f57705e4ededb09f04414c18d776cfbcc56cb7043bc7feb5c9895fdea08d8ec3eca423270b7d21962aaa3576bd9bae7b65e6ed36cf8dd828f37aade0b1c%7C0xd7684baf865df40061d72c8db1e8edd4a6bd6c3c15da144ac744b056849a4ed9";
        }

        static void showHelp()
        {
            Console.WriteLine("Blockchain Lab Results Certification");
            Console.WriteLine("GenoBank.io Dot Net, version 1.0");
            Console.WriteLine("(c) GenoBank.io 🧬");
            Console.WriteLine();
            Console.WriteLine("SYNOPSIS");
            Console.WriteLine("    certificates");
            Console.WriteLine("    certificates --test TWELVE_WORD_PHRASE PERMITTEE_ID PATIENT_NAME PATIENT_PASSPORT PROCEDURE_CODE RESULT_CODE SERIAL TIMESTAMP");
            Console.WriteLine("    certificates --production TWELVE_WORD_PHRASE PERMITTEE_ID PATIENT_NAME PATIENT_PASSPORT PROCEDURE_CODE RESULT_CODE SERIAL TIMESTAMP");
            Console.WriteLine();
            Console.WriteLine("DESCRIPTION");
            Console.WriteLine("    This notarizes a laboratory result using the GenoBank.io platform.");
            Console.WriteLine("    Running on the production network is billable per your laboratory agreement.");
            Console.WriteLine();
            Console.WriteLine("    TWELVE_WORD_PHRASE a space-separated string of your twelve word phrase");
            Console.WriteLine("    PERMITTEE_ID       your GenoBank.io permittee identifier");
            Console.WriteLine("    PATIENT_NAME       must match [A-Za-z0-9 .-]+");
            Console.WriteLine("    PATIENT_PASSPORT   must match [A-Z0-9 -]+");
            Console.WriteLine("    PROCEDURE_CODE     must be a procedure key in the Laboratory Procedure Taxonomy");
            Console.WriteLine("    RESULT_CODE        must be a result key in the Laboratory Procedure Taxonomy");
            Console.WriteLine("    SERIAL             must match [A-Z0-9 -]*");
            Console.WriteLine("    TIMESTAMP          procedure/sample collection time as number of milliseconds since UNIX epoch");
            Console.WriteLine();
            Console.WriteLine("OUTPUT");
            Console.WriteLine("    A complete URL for the certificate is printed to standard output.");
            Console.WriteLine("    Please note: you should keep a copy of this output because you paid for it");
            Console.WriteLine("    and nobody else has a copy or can recreate it for you.");
            Console.WriteLine();
            Console.WriteLine("REFERENCES");
            Console.WriteLine("    Laboratory Procedure Taxonomy (test):");
            Console.WriteLine("    https://genobank.io/certificates/laboratoryProcedureTaxonomy.json");
            Console.WriteLine();
            Console.WriteLine("    Laboratory Procedure Taxonomy (production):");
            Console.WriteLine("    https://genobank.io/test/certificates/laboratoryProcedureTaxonomy.json");
        }
    }
}

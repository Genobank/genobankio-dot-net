using System;
using Nethereum.Util;

namespace GenoBankIo
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 9) {
                Console.Error.WriteLine(args.Length);
                Console.Error.WriteLine(args);
                ShowHelp();
                return;
            }

            Console.Error.WriteLine("Blockchain Lab Results Certification");
            Console.Error.WriteLine("Java Certification Example, version 1.0");
            Console.Error.WriteLine("(c) GenoBank.io 🧬");
            Console.Error.WriteLine();

            Network network;
            switch (args[0]) {
                case "--test":
                    Console.Error.WriteLine("Network:     TEST NETWORK");
                    network = Network.Test();
                    break;
                case "--production":
                    Console.Error.WriteLine("Network:     PRODUCTION NETWORK (BILLABLE)");
                    network = Network.Production();
                    break;
                default:
                    throw new Exception("You must specify --test or --production network");
            }

            PermitteeSigner signer = new PermitteeSigner(args[1], uint.Parse(args[2]));
            var pretty = (new AddressUtil()).ConvertToChecksumAddress(signer.credentials.Address);
            Console.Error.WriteLine("Address:     " + pretty);
            
            PermitteeRepresentations representations = new PermitteeRepresentations(
                network,
                args[3], // Patient name
                args[4], // Patient passport
                new LaboratoryProcedure(args[5]), // Laboratory procedure
                new LaboratoryProcedure(args[5]).resultWithCode(args[6]),
                args[7], // Serial
                DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(args[8])).UtcDateTime, // Time
                signer.permitteeId // Permittee ID
            );

            Console.Error.WriteLine("Patient:     " + representations.patientName);
            Console.Error.WriteLine("Passport:    " + representations.patientPassport);
            Console.Error.WriteLine("Procedure:   " + representations.procedure.code);
            Console.Error.WriteLine("Result:      " + representations.result.code);
            Console.Error.WriteLine("Serial:      " + representations.serial);
            Console.Error.WriteLine("Time:        " + new DateTimeOffset(representations.time).ToUnixTimeMilliseconds().ToString());

            byte[] signature = signer.sign(representations);
            Console.Error.WriteLine("Signature:   0x" + BitConverter.ToString(signature).Replace("-",""));
            Console.Error.WriteLine();
            
            Console.Error.WriteLine("Notarizing on blockchain...");
            Platform platform = new Platform(network, signer);
            NotarizedCertificate certificate = platform.notarize(representations, signature);
            Console.Error.WriteLine();

            Console.Error.WriteLine("Certificate URL");
            Console.WriteLine(certificate.toURL());
        }

        static void ShowHelp()
        {
            Console.WriteLine("Blockchain Lab Results Certification");
            Console.WriteLine("Java Certification Example, version 1.0");
            Console.WriteLine("(c) GenoBank.io 🧬");
            Console.WriteLine();
            Console.WriteLine("SYNOPSIS");
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
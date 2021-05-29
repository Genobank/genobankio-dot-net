using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Nethereum.Signer;

namespace GenoBankIo
{
    public struct PermitteeRepresentations
    {
        public PermitteeRepresentations(
            Network network,
            string patientName,
            string patientPassport,
            LaboratoryProcedure procedure,
            LaboratoryProcedureResult result,
            string serial,
            DateTime time,
            uint permitteeId
        ) {
            // Network
            this.network = network;
            
            // Patient name
            if (!Regex.IsMatch(patientName, @"^[A-Za-z0-9 -.ñÑ]+$")) {
                throw new ArgumentException("Patient name does not use required format");
            }
            this.patientName = patientName;
            
            // Patient passport
            if (!Regex.IsMatch(patientPassport, @"^[A-Z0-9 .-]+$")) {
                throw new ArgumentException("Patient passport does not use required format");
            }
            this.patientPassport = patientPassport;
            
            // Laboratory procedure
            this.procedure = procedure;
            
            // Laboratory result
            this.result = result;
            
            // Serial number
            if (!Regex.IsMatch(serial, @"^[A-Z0-9 -]*$")) {
                throw new ArgumentException("Serial does not use required format");
            }
            this.serial = serial;
            
            // Time
            if (time < new DateTime(2021, 1, 1)) {
                throw new ArgumentException("Time is too early, it is before 2021-01-01");
            }
            this.time = time;

            // Permittee ID
            this.permitteeId = permitteeId;
        }

        public String getFullSerialization() {
            string isoInstantWithMilliseconds = "yyyy-MM-dd'T'HH:mm:ss.fffK";

            return string.Join("|", new string[]{
                network.namespacePrefix + namespaceSuffix,
                patientName,
                patientPassport,
                procedure.internationalName,
                result.internationalName,
                serial,
                time.ToString(isoInstantWithMilliseconds, CultureInfo.InvariantCulture),
                permitteeId.ToString()
            });
        }

        public String getTightSerialization() {
            return string.Join("|", new string[]{
                patientName,
                patientPassport,
                procedure.code,
                result.code,
                serial,
                new DateTimeOffset(time).ToUnixTimeMilliseconds().ToString(),
                permitteeId.ToString()
            });
        }

        public byte[] getClaim() {
            return new EthereumMessageSigner().HashPrefixedMessage(Encoding.UTF8.GetBytes(getFullSerialization()));
        }

        private const string versionCode = "V1";
        private const string namespaceSuffix = ".certificates.v1.permittee-certification";
        public Network network { get; }
        public string patientName { get; }
        public string patientPassport { get; }
        public LaboratoryProcedure procedure { get; }
        public LaboratoryProcedureResult result { get; }
        public string serial { get; }
        public DateTime time { get; }
        public uint permitteeId { get; }
    }
}
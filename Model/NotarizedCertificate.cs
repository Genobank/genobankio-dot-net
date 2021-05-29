using System;
using System.Globalization;

namespace GenoBankIo
{
    public struct NotarizedCertificate
    {
        public NotarizedCertificate(
            PermitteeRepresentations permitteeRepresentations,
            byte[] permiteeSignature,
            DateTime notarizedTime,
            byte[] notarizedSignature,
            byte[] txHash
        ) {
            this.permitteeRepresentations = permitteeRepresentations;
            this.permitteeSignature = permiteeSignature;
            this.notarizedTime = notarizedTime;
            this.notarizedSignature = notarizedSignature;
            this.txHash = txHash;
            this.network = permitteeRepresentations.network;
        }

        public String getFullSerialization() {
            string isoInstantWithMilliseconds = "yyyy-MM-dd'T'HH:mm:ss.fffK";

            return string.Join("|", new String[]{
                permitteeRepresentations.getFullSerialization(),
                "0x" + BitConverter.ToString(permitteeSignature).Replace("-",""),
                notarizedTime.ToString(isoInstantWithMilliseconds, CultureInfo.InvariantCulture),
                "0x" + BitConverter.ToString(notarizedSignature).Replace("-","")
            });
        }

        public String getTightSerialization() {
            return string.Join("|", new String[]{
                permitteeRepresentations.getTightSerialization(),
                "0x" + BitConverter.ToString(permitteeSignature).Replace("-",""),
                new DateTimeOffset(notarizedTime).ToUnixTimeMilliseconds().ToString(),
                "0x" + BitConverter.ToString(notarizedSignature).Replace("-",""),
                "0x" + BitConverter.ToString(txHash).Replace("-",""),
            });
        }
        public String toURL() {
            return network.certificateUrlBase + getTightSerialization();
            // Is there a nicer way to do that ^ using the Uri class?
        }

        public Network network { get; }
        public PermitteeRepresentations permitteeRepresentations { get; }
        public byte[] permitteeSignature { get; }
        public DateTime notarizedTime { get; }
        public byte[] notarizedSignature { get; }
        public byte[] txHash { get; }
    }
}
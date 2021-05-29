using System;

namespace GenoBankIo
{
    public struct LaboratoryProcedure
    {
        public LaboratoryProcedure(string code) {
            switch (code) {
                case "1":
                    this.code = code;
                    this.internationalName = "COVID-19-PCR";
                    break;
                case "2":
                    this.code = code;
                    this.internationalName = "COVID-19-ANTIGEN";
                    break;
                case "3":
                    this.code = code;
                    this.internationalName = "COVID-19-LAMP";
                    break;
                default:
                    throw new ArgumentException("Only laboratory procedure 1=COVID-19-PCR, 2=COVID-19-ANTIGEN and 3=COVID-19-LAMP are supported in this version");
            }
        }

        public LaboratoryProcedureResult resultWithCode(string code) {
            switch (code) {
            case "N":
                return new LaboratoryProcedureResult(code, "NEGATIVE");
            case "P":
                return new LaboratoryProcedureResult(code, "POSITIVE");
            default:
                throw new ArgumentException("Only laboratory result N=NEGATIVE and P=POSITIVE is supported in this version");
            }
        }
        public string code { get; }
        public string internationalName { get; }
    }
}
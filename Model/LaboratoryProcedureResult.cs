namespace GenoBankIo
{
    public struct LaboratoryProcedureResult
    {
        public LaboratoryProcedureResult(
            string code,
            string internationalName
        ) {
            this.code = code;
            this.internationalName = internationalName;
        }

        public string code { get; }
        public string internationalName { get; }
    }
}
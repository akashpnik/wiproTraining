namespace SolidPrinciples
{
    public class srp
    {
        // Before (Violation of SRP)
       /* public class Report
        {
            public string Content { get; set; }

            public void GenerateReport()
            {
                // Generate report logic
            }

            public void SaveToFile(string path)
            {
                // Save report to file logic
            }
        }
       */
        // After (SRP Applied)
        public class Report
        {
            public string Content { get; set; }

            public void GenerateReport()
            {
                // Generate report logic
            }
        }

        public class ReportSaver
        {
            public void SaveToFile(Report report, string path)
            {
                // Save report to file logic
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Delegates.Reports;
using NUnitLite;

class Program
{
	static void Main(string[] args)
	{
	    List<Measurement> data = new List<Measurement>
	    {
	        new Measurement
	        {
	            Humidity=1,
	            Temperature=-10,
	        },
	        new  Measurement
	        {
	            Humidity=2,
	            Temperature=2,
	        },
	        new Measurement
	        {
	            Humidity=3,
	            Temperature=14
	        },
	        new Measurement
	        {
	            Humidity=2,
	            Temperature=30
	        }
	    };
        string meanAndStdMarkdownReport = ReportMakerHelper.MeanAndStdMarkdownReport(data);
	    var expectedMeanAndStdMarkdownReport = "## Mean and Std\n\n * **Temperature**: 9±17.0880074906351\n\n * **Humidity**: 2±0.816496580927726\n\n";

	    var medianMarkdownReport = ReportMakerHelper.MedianMarkdownReport(data);

        string meanAndStdHtmlReport = ReportMakerHelper.MeanAndStdHtmlReport(data);
        string expectedmedianHtmlReport = "<h1>Median</h1><ul><li><b>Temperature</b>: 8<li><b>Humidity</b>: 2</ul>";
	    string medianHtmlReport = ReportMakerHelper.MedianHtmlReport(data);

        
        Console.WriteLine($"MeanAndStdHtmlReport: {meanAndStdHtmlReport}");
        Console.WriteLine($"MedianHtmlReport: {medianHtmlReport}");
        
        Console.WriteLine($"MedianMarkdownReport: {medianMarkdownReport}");
        Console.WriteLine($"MeanAndStdMarkdownReport: {meanAndStdMarkdownReport}");
        //new AutoRun().Execute(args);
    }
}

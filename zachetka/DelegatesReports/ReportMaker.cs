using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.Reports
{
    public struct BeginEnd
    {
        public BeginEnd(string begin, string end)
        {
            Begin = begin;
            End = end;
        }

        public string Begin { get; set; }
        public string End { get; set; }
    }

    public class Report
    {
        public Report(string caption, 
            Func<IEnumerable<double>, object> makerStatisticFunc,
            BeginEnd beginEnd,
            Func<string, string, string> makeItemFunc)
        {
            this.makerStatisticFunc = makerStatisticFunc;
            this.makeItemFunc = makeItemFunc;
            Caption = caption;
            this.beginEnd = beginEnd;
        }

        protected string Caption { get; set; }
        public Func<string, string, string> BeginEndFunc { get; }
        private Func<IEnumerable<double>, object> makerStatisticFunc;
        BeginEnd beginEnd;
        private Func<string, string, string> makeItemFunc;

        public string MakeReport(IEnumerable<Measurement> measurements)
        {
            var data = measurements.ToList();
            var result = new StringBuilder();
            result.Append(Caption);
            result.Append(beginEnd.Begin);
            result.Append(makeItemFunc("Temperature", makerStatisticFunc(data.Select(z => z.Temperature)).ToString()));
            result.Append(makeItemFunc("Humidity", makerStatisticFunc(data.Select(z => z.Humidity)).ToString()));
            result.Append(beginEnd.End);

            return result.ToString();
        }
    }

    public static class ReportMakerHelper
	{
		public static string MeanAndStdHtmlReport(IEnumerable<Measurement> data)
		{
			return new Report("<h1>Mean and Std</h1>", (doubles) =>
			{
			    var listDoubles = doubles.ToList();
                var mean = listDoubles.Average();
                var std = Math.Sqrt(listDoubles.Select(z => Math.Pow(z - mean, 2)).Sum() / (listDoubles.Count - 1));

                return new MeanAndStd
                {
                    Mean = mean,
                    Std = std
                };
            }, new BeginEnd("<ul>", "</ul>"), (valueType, entry) =>{ return $"<li><b>{valueType}</b>: {entry}";})
			    .MakeReport(data);
		}

		public static string MedianMarkdownReport(IEnumerable<Measurement> data)
		{
		    return new Report("## Median\n\n", (doubles) =>
		    {
                var list = doubles.OrderBy(z => z).ToList();
                if (list.Count % 2 == 0)
                    return (list[list.Count / 2] + list[list.Count / 2 - 1]) / 2;
                return list[list.Count / 2];
		    }, new BeginEnd("", ""), (valueType, entry) =>
		    {
		        return $" * **{valueType}**: {entry}\n\n";
            }).MakeReport(data);
        }

		public static string MeanAndStdMarkdownReport(IEnumerable<Measurement> measurements)
		{
		    return new Report("## Mean and Std\n\n", (doubles) =>
		    {
		        var listDoubles = doubles.ToList();
		        var mean = listDoubles.Average();
		        var std = Math.Sqrt(listDoubles.Select(z => Math.Pow(z - mean, 2)).Sum() / (listDoubles.Count - 1));
		        return new MeanAndStd
		        {
		            Mean = mean,
		            Std = std
		        };
            }, new BeginEnd("", ""), (valueType, entry) =>
		    {
		        return $" * **{valueType}**: {entry}\n\n";
		    }).MakeReport(measurements);
        }

		public static string MedianHtmlReport(IEnumerable<Measurement> measurements)
		{
		    return new Report("<h1>Median</h1>", (doubles) =>
		        {
		            var list = doubles.OrderBy(z => z).ToList();
		            if (list.Count % 2 == 0)
		                return (list[list.Count / 2] + list[list.Count / 2 - 1]) / 2;
		            return list[list.Count / 2];
                }, new BeginEnd("<ul>", "</ul>"), (valueType, entry) => { return $"<li><b>{valueType}</b>: {entry}"; })
		        .MakeReport(measurements);
        }
	}
}

using Microsoft.Extensions.FileSystemGlobbing.Internal;
using RepositoryPatternPractice.Models.Business_Objet;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RepositoryPatternPractice.API_Repository
{
    public class LogReport
    {
        
        static string FinalPage = @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}.\d{4}\s\[[A-Z]*\]\s-\s\[Thread:\d{1,3}\s*?\]\s?-\s?[==\s==\s^[A-z]*\s[A-z]*.[A-z]*\s[A-z]*.\s?[A-z]*:[A-z]*\|";

        //MatchCollection matches = Regex.Matches(logFile, pattern);


        public IDictionary<String,List<String>> ReadLog()
        {

            string lidPatternPage = @"pageName:[A-z]*";

            string pattern = @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}.\d{4}\s\[[A-Z]*\]\s-\s\[Thread:\d{1,3}\s*?\]\s?-\s?[==\s==\s^[A-z]*\s[A-z]*.[A-z]*\s[A-z]*.\s?[A-z]*:[A-z]*\|lid:\d{1,}[A-z]+\]";

            string lidPattern = @"lid:\d{1,}";

            string time = @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}.\d{4}\s";


            IDictionary<String, List<String>> MappedPage = new Dictionary<String, List<String>>();

            IDictionary<String, List<Match>> Mapped = new Dictionary<String, List<Match>>();


            var filePaths = Directory
            .EnumerateFiles(@"C:\Users\SECL\Downloads\logs")
            .Where(f => f.EndsWith("All.log.0"));

            foreach (var file in filePaths)
            {
                MatchCollection matches = Regex
                .Matches(File.ReadAllText(file.ToString()), pattern);

                FindUserTrace((Dictionary<string, List<String>>)MappedPage,
                                  (Dictionary<String, List<Match>>)Mapped,
                                  lidPatternPage,
                                  FinalPage,
                                  lidPattern,
                                  time,
                                  matches);
               
            }
            //Console.WriteLine(filePaths);
            
            return MappedPage;
        }



        public static void FindUserTrace(
        Dictionary<String, List<String>> MappedPage,
        Dictionary<String, List<Match>> Mapped,
        string lidPatternPage,
        string FinalPage,
        string lidPattern,
        string time,
        MatchCollection matches)

        {
            foreach (Match match in matches)
            {
                Match id = Regex.Match(match.Value, lidPattern);


                if (!Mapped.ContainsKey(id.Value))
                {
                    List<Match> pagelist = new List<Match>();

                    List<String> page = new List<String>();

                    Mapped.Add(id.Value.ToString(), pagelist);

                    MappedPage.Add(id.Value.ToString(), page);
                }

                if (Regex.IsMatch(match.Value, id.Value))
                {
                    Match pageDetail = Regex.Match(match.Value, FinalPage);

                    Match page = Regex.Match(match.Value, lidPatternPage);

                    Match dateTime = Regex.Match(match.Value, time);

                    Mapped[id.Value.ToString()].Add(pageDetail);

                    MappedPage[id.Value.ToString()].Add("Date & Time: " + dateTime.Value+" => "+ page.Value);
                }
            }
        }



    }
}

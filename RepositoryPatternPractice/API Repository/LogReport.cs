using Microsoft.Extensions.FileSystemGlobbing.Internal;
using RepositoryPatternPractice.Models.Business_Objet;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RepositoryPatternPractice.API_Repository
{
    public class LogReport
    {


        static string lidPatternPage = @"pageName:[A-z]*";

        static string pattern = @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}.\d{4}\s\[[A-Z]*\]\s-\s\[Thread:\d{1,3}\s*?\]\s?-\s?[==\s==\s^[A-z]*\s[A-z]*.[A-z]*\s[A-z]*.\s?[A-z]*:[A-z]*\|lid:\d{1,}[A-z]+\]";

        static string lidPattern = @"lid:\d{1,}";

        static string FinalPage = @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}.\d{4}\s\[[A-Z]*\]\s-\s\[Thread:\d{1,3}\s*?\]\s?-\s?[==\s==\s^[A-z]*\s[A-z]*.[A-z]*\s[A-z]*.\s?[A-z]*:[A-z]*\|";

        //MatchCollection matches = Regex.Matches(logFile, pattern);

        static IDictionary<String, List<Match>> Mapped = new Dictionary<String, List<Match>>();

        static IDictionary<String, List<Match>> MappedPage = new Dictionary<String, List<Match>>();


        public IDictionary<String,List<Match>> ReadLog()
        {
            var filePaths = Directory
            .EnumerateFiles(@"C:\Users\SECL\Downloads\logs")
            .Where(f => f.EndsWith("All.log.0"));

            foreach (var file in filePaths)
            {
                MatchCollection matches = Regex
                .Matches(File.ReadAllText(file.ToString()), pattern);

                FindUserTrace((Dictionary<string, List<Match>>)MappedPage,
                                  (Dictionary<String, List<Match>>)Mapped,
                                  lidPatternPage,
                                  FinalPage,
                                  lidPattern,
                                  matches);
               
            }
            //Console.WriteLine(filePaths);
            
            return MappedPage;
        }



        public static void FindUserTrace(
           Dictionary<String, List<Match>> MappedPage,
           Dictionary<String, List<Match>> Mapped,
           string lidPatternPage,
           string FinalPage,
           string lidPattern,
           MatchCollection matches)

        {
            foreach (Match match in matches)
            {
                Match id = Regex.Match(match.Value, lidPattern);


                if (!Mapped.ContainsKey(id.Value))
                {
                    List<Match> pagelist = new List<Match>();

                    List<Match> page = new List<Match>();

                    Mapped.Add(id.Value, pagelist);

                    MappedPage.Add(id.Value, page);
                }

                if (Regex.IsMatch(match.Value, id.Value))
                {
                    Match pageDetail = Regex.Match(match.Value, FinalPage);

                    Match page = Regex.Match(match.Value, lidPatternPage);


                    Mapped[id.Value].Add(pageDetail);

                    MappedPage[id.Value].Add(page);
                }
            }
        }



    }
}

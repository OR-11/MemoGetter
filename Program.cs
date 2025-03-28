using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json.Nodes;
using System.Diagnostics;
using System.Web;

namespace MemoGetter
{
    internal class Program
    {
        static bool isDark;
        static bool OpenAnyway;
        static void Main(string[] args)
        {
            Console.WriteLine("取得中...");

            string[] result = MemoGetter().Result;

            //./TempをTempフォルダとする
            var htmlGenerator = new CreateHTML();
            htmlGenerator.IsDark = isDark;
            foreach (string r in result)
            {
                htmlGenerator.memo.Add(r);
            }

            string html = htmlGenerator.Generate();

            //保存
            Directory.CreateDirectory("./Temp");
            using (var writer = new StreamWriter("./Temp/Memos.html", false))
            {
                writer.WriteLine(html);
            }

            //開く
            if (htmlGenerator.memo.Count > 0 || OpenAnyway)
            {
                var startInfo = new ProcessStartInfo()
                {
                    WorkingDirectory = "./Temp/",
                    FileName = "Memos.html",
                    UseShellExecute = true,
                };
                Process.Start(startInfo);
            }
        }

        static async Task<string[]> MemoGetter()
        {
            //URL読み込み
            string fileJsonText;
            using (var reader = new StreamReader("./Settings.json"))
            {
                fileJsonText = reader.ReadToEnd();
            }

            //JSON構造体化
            var jsonalized = JsonNode.Parse(fileJsonText);

            //ダークモードかどうか
            isDark = (bool)jsonalized["DarkMode"];

            //要素が0でも開くかどうか
            OpenAnyway = (bool)jsonalized["OpenAnyway"];

            //メモ取得
            JsonNode jsonalizedAnswer;
            using (var client = new HttpClient())
            {
                var result = client.GetAsync(jsonalized["URL"].ToString()).Result.Content.ReadAsStringAsync().Result;
                Console.WriteLine(result);
                jsonalizedAnswer = JsonNode.Parse(result);
            }

            //配列化
            string[] r = jsonalizedAnswer["data"].AsArray().Select(x => HttpUtility.UrlDecode(x.ToString())).ToArray();
            return r;
        }
    }
}

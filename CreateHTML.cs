using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MemoGetter
{
    internal class CreateHTML
    {
        public CreateHTML() { }
        bool isDark;
        public bool IsDark
        {
            set { isDark = value; }
        }

        public List<string> memo = new List<string>();

        public string Generate()
        {
            //htmlカキカキ
            string htmlr = "";
            htmlr += $"<body{(isDark ? " style=\"background-color: rgb(8, 8, 34)\"" : "")}>\n";
            {
                htmlr += "<div style=\"padding-bottom: 15px;\">\n";
                {
                    //タイトル
                    htmlr += $"<h1{(isDark ? " style=\"color: #ffffff;\"" : "")}>送信された内容</h1>\n";

                    //すべて保存ボタン
                    htmlr += "<button id=\"downloadAllButton\"><big>全て保存</big></button>\n";
                    htmlr += GenerateSaveAllScript() + "\n";
                }
                htmlr += "</div>\n";

                //hrごと少し真ん中に寄せるdiv
                htmlr += "<div style=\"padding-left: 30px; padding-right: 30px;\">\n";
                {
                    for (int i = 0; i < memo.Count; i++)//一つの要素
                    {
                        //hrの内側へ少し真ん中に寄せるdiv
                        htmlr += "<div style=\"padding-left: 30px; padding-right: 30px;\">\n";
                        {
                            htmlr += $"<h2{(isDark ? " style=\"color: #ffffff;\"" : "")}>{i + 1} <button id=\"downloadNo{i}Button\"><big>保存</big></button></h2>\n";
                            htmlr += $"<p id=\"downloadNo{i}\"{(isDark ? " style=\"color: #ffffff;\"" : "")}>{memo[i]}</p>\n";
                            htmlr += GenerateSaveScript(i) + "\n";
                        }
                        htmlr += "</div>\n";

                        if (i + 1 < memo.Count)
                            htmlr += "<hr />\n";
                    }
                }
                htmlr += "</div>\n";

            }
            htmlr += "</body>\n";

            return htmlr;
        }

        string GenerateSaveScript(int index)//一つだけ保存script
        {
            string sc = "<script>\n";
            {
                sc +=
                $"document.getElementById(\"downloadNo{index}Button\").addEventListener(\"click\", function() {{\n" +
                 "// ダウンロードするデータ\n" +
                $"const content = String(document.getElementById('downloadNo{index}').textContent);\n" +
                 "const blob = new Blob([content], { type: \"text/plain\" });\n" +

                 "// ダウンロード用のリンクを作成\n" +
                 "const a = document.createElement(\"a\");\n" +
                 "a.href = URL.createObjectURL(blob);\n" +
                $"a.download = \"送信されたメモ{DateTime.Now.ToString("yyyyMMddHHmmss")}_{index + 1}.txt\";  // ダウンロード時のファイル名\n" +
                 "document.body.appendChild(a);\n" +

                 "// 自動クリックしてダウンロード開始\n" +
                 "a.click();\n" +

                 "// 不要になった要素を削除\n" +
                 "document.body.removeChild(a);\n" +
                 "URL.revokeObjectURL(a.href);\n" +
                "});\n";
            }
            sc += "</script>\n";

            return sc;
        }

        string GenerateSaveAllScript()//すべて保存script
        {
            string sc = "<script>\n";
            {
                sc +=
                $"document.getElementById(\"downloadAllButton\").addEventListener(\"click\", function() {{\n" +
                 "  // ダウンロードするデータ\n" +
                $"  for (let i = 0; i < {memo.Count}; i++){{\n" +
                 "    const content = String(document.getElementById(`downloadNo${i}`).textContent);\n" +
                 "    const blob = new Blob([content], { type: \"text/plain\" });\n" +

                 "    // ダウンロード用のリンクを作成\n" +
                 "    const a = document.createElement(\"a\");\n" +
                 "    a.href = URL.createObjectURL(blob);\n" +
                $"    a.download = `送信されたメモ{DateTime.Now.ToString("yyyyMMddHHmmss")}_${{i + 1}}.txt`;  // ダウンロード時のファイル名\n" +
                 "    document.body.appendChild(a);\n" +

                 "    // 自動クリックしてダウンロード開始\n" +
                 "    a.click();\n" +

                 "    // 不要になった要素を削除\n" +
                 "    document.body.removeChild(a);\n" +
                 "    URL.revokeObjectURL(a.href);\n" +
                 "  }\n" +
                 "});\n";
            }
            sc += "</script>\n";

            return sc;
        }
    }
}

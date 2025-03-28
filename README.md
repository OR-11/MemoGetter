# MemoGetter
## なんだこれ
スマートフォンなどから、PCの電源がついていなくても、PCへメモを送信することができるプログラムです。  
ネーミングセンスには目をつぶってください

## 使い方
### 準備
- GAS側  
Googleフォームを作成してください。作成したら入力欄を1つだけ(「段落」や「記述式(短文)」など)にし、「回答」タブからスプライトシートを新規作成します。  
新規作成したスプレッドシートの「拡張機能」タブ→「Apps Script」をクリックし、コードエディタに「GAS側に張り付けるコード.txt」の内容をペーストします。ウェブアプリとしてデプロイし、デプロイIDを控えておいてください。  
- PC側  
とりあえずビルドします。
Settings.jsonをMemoGetter.exeと同じ階層に配置し、Settings.jsonの中のURLの値の[デプロイID]と書かれているところを、デプロイしたGASのデプロイIDに置き換えます。  
Settings.jsonのDarkModeで、実行後に表示されるページをダークモードにするか変更できます。  
Settings.jsonのOpenAnywayで、実行時、取得内容数が0の時ページを表示するかどうかを設定できます。  
### 使用
任意の端末から作成したフォームにアクセスし、内容を書きこみ、送信します。  
タスクスケジューラなり手動なりでMemoGetter.exeを起動してください。しばらく(数秒)するとブラウザが開き、前回の起動より後に送信された内容が表示されます。保存ボタンでtxtファイルとしてダウンロードできます。  
※一度起動するとそれまでに送信された内容はスプレッドシートから削除されます。  
※開かれるページは、MemoGetter.exeと同じ階層から./Temp/Memos.htmlでアクセスできます。もう一度MemoGetter.exeを起動すると上書きされます。

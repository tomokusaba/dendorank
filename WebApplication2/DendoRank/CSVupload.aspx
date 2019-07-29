<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CSVupload.aspx.cs" Inherits="WebApplication2.DendoRank.CSVupload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Museの殿堂データアップロード画面</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            データはエンコードUTF-8(BOMつき)のみです。<br />
            アップロード可能な拡張子は.csvまたは.txtのみです。<br />
            <asp:Label ID="Label1" runat="server"></asp:Label>
        </div>
        <asp:FileUpload ID="FileUpload1" runat="server" OnLoad="Page_Load" />
        <p>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="アップロード" />
        </p>
        <h1>データ形式について</h1>
        このアップロード画面から登録するとデータは追加されます。<br />
        新規に追加されたデータのみアップロードしてください。<br />
<pre>集計年度,ジャンル,評価,曲名,作者,演奏時間(時),演奏時間(分),演奏時間(秒),メンバー数,ファイルサイズ</pre>
<ul class="list1" style="padding-left:16px;margin-left:16px"><li>集計年度
　当年の2回目の更新～翌年の1回目の更新を当年度とする。</li></ul>
<ul class="list1" style="padding-left:16px;margin-left:16px"><li>ジャンル
　<!--autolink--><a href="https://musewiki.dip.jp:443/MuseWiki/index.php?Muse%A4%CE%C5%C2%C6%B2" title="Museの殿堂 (1546d)">Museの殿堂</a><!--/autolink-->でプルダウンメニューで選択できる項目「ポピュラー」、「クラシック」など</li></ul>
<ul class="list1" style="padding-left:16px;margin-left:16px"><li>評価
　★→2<br />
　☆→1<br />
　無印→0<br />
(半角数字)</li></ul>
<ul class="list1" style="padding-left:16px;margin-left:16px"><li>曲名
　曲名をそのまま入力</li></ul>
<ul class="list1" style="padding-left:16px;margin-left:16px"><li>作者
　作成者をそのまま入力</li></ul>
<ul class="list1" style="padding-left:16px;margin-left:16px"><li>演奏時間(時)
　演奏時間の時間の部分を入力(半角数字)</li></ul>
<ul class="list1" style="padding-left:16px;margin-left:16px"><li>演奏時間(分)
　演奏時間の分の部分を入力(半角数字)</li></ul>
<ul class="list1" style="padding-left:16px;margin-left:16px"><li>演奏時間(秒)
　演奏時間の秒の部分を入力(半角数字)</li></ul>
<ul class="list1" style="padding-left:16px;margin-left:16px"><li>メンバー数
　メンバー数を入力(半角数字)</li></ul>
<ul class="list1" style="padding-left:16px;margin-left:16px"><li>ファイルサイズ
　ファイルサイズを入力(半角数字)</li></ul>

        
        データ例
        <pre>
        "2017","ポピュラー","2","ロ・ロ・ロ・ロシアンルーレット","MIZ","0","3","44","16","9931"
        </pre>
    </form>
</body>
</html>

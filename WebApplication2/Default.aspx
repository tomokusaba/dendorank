<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication2._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script>
</script>
    <div class="card">
        <div class="card-header">
            <ul id="myTabs" class="nav nav-tabs card-header-tabs" role="tablist">
                <li role="presentation" class="nav-item active">
                    <a href="#chart" aria-controls="chart" role="tab" data-toggle="tab" class="nav-link active">殿堂チャート</a></li>
                <li role="presentation" class="nav-item">
                    <a href="#nendo" aria-controls="nendo" role="tab" data-toggle="tab" class="nav-link">殿堂チャート年度ごと</a></li>
                <li role="presentation" class="nav-item">
                    <a href="#toroku" aria-controls="toroku" role="tab" data-toggle="tab" class="nav-link">データベース編集</a></li>
            </ul>
        </div>
        <div class="card-body">
            <div class="tab-content">
                <div class="tab-pane active" id="chart">
                    <ul>
                        <li><a href="../dendoweb2/">殿堂データ閲覧</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=12">殿堂チャート・殿堂ポイント</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=1">殿堂チャート・殿堂登録曲数</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=2">殿堂チャート・★登録曲数</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=3">殿堂チャート・☆登録曲数</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=4">殿堂チャート・無印登録曲数</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=5">殿堂チャート・楽曲別ファイルサイズ最大</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=6">殿堂チャート・楽曲別ファイルサイズ最小</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=7">殿堂チャート・楽曲別短時間演奏</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=8">殿堂チャート・楽曲別長時間演奏</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=9">殿堂チャート・作者別ファイルサイズ最大</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=10">殿堂チャート・作者別演奏時間最大</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=11">殿堂チャート・作者別時間あたりのファイルサイズ</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=13">殿堂チャート・曲名ランキング</a></li>
                    </ul>
                </div>
                <div class="tab-pane" id="nendo">
                    <ul>
                        <li><a href="./DendoRank/dendoRank.aspx?id=1999">殿堂チャート・殿堂ポイント1999年度版</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=2000">殿堂チャート・殿堂ポイント2000年度版</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=2001">殿堂チャート・殿堂ポイント2001年度版</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=2002">殿堂チャート・殿堂ポイント2002年度版</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=2003">殿堂チャート・殿堂ポイント2003年度版</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=2004">殿堂チャート・殿堂ポイント2004年度版</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=2005">殿堂チャート・殿堂ポイント2005年度版</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=2006">殿堂チャート・殿堂ポイント2006年度版</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=2007">殿堂チャート・殿堂ポイント2007年度版</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=2008">殿堂チャート・殿堂ポイント2008年度版</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=2009">殿堂チャート・殿堂ポイント2009年度版</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=2010">殿堂チャート・殿堂ポイント2010年度版</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=2011">殿堂チャート・殿堂ポイント2011年度版</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=2012">殿堂チャート・殿堂ポイント2012年度版</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=2013">殿堂チャート・殿堂ポイント2013年度版</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=2014">殿堂チャート・殿堂ポイント2014年度版</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=2015">殿堂チャート・殿堂ポイント2015年度版</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=2016">殿堂チャート・殿堂ポイント2016年度版</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=2017">殿堂チャート・殿堂ポイント2017年度版</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=2018">殿堂チャート・殿堂ポイント2018年度版</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=2019">殿堂チャート・殿堂ポイント2019年度版</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=2020">殿堂チャート・殿堂ポイント2020年度版</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=2021">殿堂チャート・殿堂ポイント2021年度版</a></li>
                        <li><a href="./DendoRank/dendoRank.aspx?id=2022">殿堂チャート・殿堂ポイント2022年度版</a></li>
                    </ul>
                </div>
                <div class="tab-pane" id="toroku">
                    <ul>
                        <li><a href="./DendoRank/CSVupload.aspx">CSV登録</a></li>
                        <li><a href="./DendoRank/CSVuploadPas.aspx">CSV登録(Pascalさん仕様)</a></li>
                        <li><a href="./DendoRank/dendoUpdate.aspx?Pagesize=50">編集</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
</asp:Content>


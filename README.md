# deserialize-rrs_log-cs

c#言語でRRSログをデシリアライズするコード

## つかいかた？

### 1. クローン
`git clone https://github.com/nono2224/deserialize-rrs_log-cs.git`

### 2. 依存関係の復元
`dotnet restore`

### 3. ビルド
`dotnet build`

### 4. 入力ファイルを記述
`Program.cs`ファイル内の`logDirectoryPath`部分に入力するログディレクトリのパスを記述

### 5. 実行
`dotnet run`

### 6. おわり
`output`ディレクトリが出力され，ここにデシリアライズされたログデータが格納されます
# deserialize-rrs_log-cs

c#言語でRRSログをデシリアライズするコード

## つかいかた？

### 1. クローン
`git clone https://github.com/nono2224/deserialize-rrs_log-cs.git`

### 2. 作業ディレクトリとブランチの移動
`cd deserialize-rrs_log-cs`

`git checkout allFile`

### 3. 依存関係の復元
`dotnet restore`

### 4. ビルド
`dotnet build`

### 5. 入力ファイルを記述
`Program.cs`ファイル内の`logDirectoryPath`部分に入力するログディレクトリのパスを記述

### 6. 実行
`dotnet run`

### 7. おわり
`output`ディレクトリが出力され，ここにデシリアライズされたログデータが格納されます
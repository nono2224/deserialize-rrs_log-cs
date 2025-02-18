# deserialize-rrs_log-cs

RoboCupRescue Simulation においてシミュレーション終了時に出力されるログを C#言語でデシリアライズするコードです

## くわしく

RoboCupRescue Simulation においてシミュレーションが終了すると，シミュレーションにおけるログがファイルとして出力されます．
そのログは Google Protocol Buffers によってシリアライズされて出力されます．
つまり，出力されるログはバイナリ形式となり，そのままではログに格納されている情報を読み取ることができません．
そのためログを読む場合や利用する際には，読み取ることができる形式に変換するデシリアライズを行う必要があります．

本プログラムでは，Google Protocol Buffers を利用して，指定されたログファイルをデシリアライズするコードです．
本プログラムファイルには，Google Protocol Buffers で使用されるログのデータ構造を示した`.proto`ファイル 2 つ，データ構造を C#言語で使用可能にしたファイルを 2 つ，これらを用いてデシリアライズする`Progmra.cs`が含まれます．

## セットアップ

### 1. クローン

GitHub より本リポジトリをクローンします

```sh
git clone https://github.com/nono2224/deserialize-rrs_log-cs.git
```

### 2. リポジトリとブランチの移動

本リポジトリへ移動とブランチの移動を行います

```sh
cd deserialize-rrs_log-cs
```

```sh
git checkout allFile
```

### 3. 依存関係の復元

依存関係の復元を行います

```sh
dotnet restore
```

## つかいかた

### 1. 入力ファイルを記述

`Program.cs`ファイル内の`logDirectoryPath`部分に入力するログディレクトリのパスを入力します

### 2. ビルド

ビルドを行います

```sh
dotnet build
```

### 3. 実行

本システムの実行を行います

```sh
dotnet run
```

実行すると，入力ディレクトリ内のファイルをデシリアライズしたものが，`output`ディレクトリに出力されます

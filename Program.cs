using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.IO;
using RCRSLogProto;
using RCRSProto;

public class Program
{
    public static void Main(string[] args)
    {
        //ログファイルがあるディレクトリのパスを指定
        string logDirectoryPath = "./filePath"; // ここをログファイルがあるディレクトリに変更

        //出力先のディレクトリのパスを指定。同じ階層に"output"ディレクトリを作成する。
        string outputDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "output");

        // 出力ディレクトリが存在しない場合は作成
        Directory.CreateDirectory(outputDirectoryPath);

        // JsonFormatter の設定 (一度だけ作成)
        JsonFormatter formatter = new JsonFormatter(
            new JsonFormatter.Settings(true)
                .WithIndentation("  ")
                .WithTypeRegistry(TypeRegistry.FromMessages(
                    LogProto.Descriptor,
                    StartLogProto.Descriptor,
                    InitialConditionsLogProto.Descriptor,
                    CommandLogProto.Descriptor,
                    PerceptionLogProto.Descriptor,
                    ConfigLogProto.Descriptor,
                    UpdatesLogProto.Descriptor,
                    EndLogProto.Descriptor,
                    MessageProto.Descriptor,
                    MessageListProto.Descriptor,
                    MessageComponentProto.Descriptor,
                    StrListProto.Descriptor,
                    IntListProto.Descriptor,
                    FloatListProto.Descriptor,
                    IntMatrixProto.Descriptor,
                    ValueProto.Descriptor,
                    PropertyProto.Descriptor,
                    Point2DProto.Descriptor,
                    EntityProto.Descriptor,
                    EntityListProto.Descriptor,
                    ConfigProto.Descriptor,
                    EdgeListProto.Descriptor,
                    EdgeProto.Descriptor,
                    ChangeSetProto.Descriptor,
                    ChangeSetProto.Types.EntityChangeProto.Descriptor
                ))
                .WithFormatEnumsAsIntegers(false)
                .WithPreserveProtoFieldNames(false)
        );

        // 指定されたディレクトリ内のすべてのファイルを処理
        ProcessDirectory(logDirectoryPath, outputDirectoryPath, formatter);

    }
    //ディレクトリを再帰的に処理する関数
    static void ProcessDirectory(string targetDirectory, string outputDirectory, JsonFormatter formatter)
    {
      try{
        // 指定されたディレクトリ内のすべてのファイルを処理
        string[] fileEntries = Directory.GetFiles(targetDirectory);
        foreach (string filePath in fileEntries)
        {
            ProcessFile(filePath, outputDirectory, formatter);
        }

        // 指定されたディレクトリ内のすべてのサブディレクトリを再帰的に処理
        string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
        foreach (string subdirectory in subdirectoryEntries)
        {
            //サブディレクトリ名を取得
            string subDirName = Path.GetFileName(subdirectory);
            //出力先にサブディレクトリを作成
            string newOutputDir = Path.Combine(outputDirectory,subDirName);
            Directory.CreateDirectory(newOutputDir);

            ProcessDirectory(subdirectory, newOutputDir, formatter);
        }
      }
      catch (Exception e)
        {
            Console.WriteLine($"Error processing directory {targetDirectory}: {e.Message}");
            // 必要であれば、ここで例外を再スローするか、他のエラー処理を行う
        }
    }

    //ファイルを処理する関数
    static void ProcessFile(string filePath, string outputDirectory, JsonFormatter formatter)
    {
        try
        {
            // ファイルからログデータを読み込む
            byte[] receivedData = File.ReadAllBytes(filePath);

            // LogProto のデシリアライズ
            LogProto log = LogProto.Parser.ParseFrom(receivedData);

            // JSON 文字列に変換
            string jsonString = formatter.Format(log);

            // 出力ファイルパスを作成 (入力ファイル名 + .json)
            string outputFileName = Path.GetFileNameWithoutExtension(filePath) + ".json";
            string outputFilePath = Path.Combine(outputDirectory, outputFileName);

            // JSON 文字列をファイルに保存
            File.WriteAllText(outputFilePath, jsonString);
            Console.WriteLine($"{filePath} was converted and saved as {outputFilePath}");
        }

        catch (Exception e)
        {
            Console.WriteLine($"Error processing file {filePath}: {e.Message}");
            //ログファイルとして不適切なファイルが読み込まれた場合、処理を続行
        }
    }
}
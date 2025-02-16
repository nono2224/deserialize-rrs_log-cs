using Google.Protobuf;
using Google.Protobuf.WellKnownTypes; // Timestamp, Duration などを使う場合
using Google.Protobuf.Reflection;
using System;
using System.IO;
using RCRSLogProto;
using RCRSProto;

public class Program
{
    public static void Main(string[] args)
    {
        // ファイルからログデータを読み込む
        byte[] receivedData;
        try
        {
            receivedData = File.ReadAllBytes("INITIAL_CONDITIONS"); // Your log file
        }
        catch (IOException e)
        {
            Console.WriteLine($"Error reading file: {e.Message}");
            return;
        }

        // LogProto のデシリアライズ
        LogProto log = LogProto.Parser.ParseFrom(receivedData);

        // 1. JsonFormatter を使う (整形されたJSON)
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
                    // RCRSProto.proto 内のすべての MessageDescriptor を追加
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
                    ChangeSetProto.Types.EntityChangeProto.Descriptor // Nested type
                ))
                .WithFormatEnumsAsIntegers(false)
                .WithPreserveProtoFieldNames(false)
        );


        string jsonString = formatter.Format(log); // LogProto オブジェクトを JSON 文字列に変換
        Console.WriteLine(jsonString);

        // (オプション) JSON 文字列をファイルに保存
        try
        {
            File.WriteAllText("output.json", jsonString);
        }
        catch (IOException e)
        {
            Console.WriteLine($"Error writing to file: {e.Message}");
        }
    }
}
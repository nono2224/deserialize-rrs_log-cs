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


// ----------------------------------------------------------------



// using Google.Protobuf;
// using System;
// using System.IO;
// using RCRSLogProto; // 指定した名前空間
// using RCRSProto;

// public class Program
// {
//     public static void Main(string[] args)
//     {
//         // ファイルからログデータを読み込む
//         byte[] receivedData = File.ReadAllBytes("INITIAL_CONDITIONS");

//         // 1. LogProto のデシリアライズ (最上位のメッセージ)
//         LogProto log = LogProto.Parser.ParseFrom(receivedData);

//         Console.WriteLine(log);

//         // 2. LogProto の oneof フィールド "log" から InitialConditionsLogProto を取得
//         InitialConditionsLogProto initialConditions = log.InitialCondition;

//         if (initialConditions != null)
//         {
//             Console.WriteLine("Initial Conditions:");
//             // 3. InitialConditionsLogProto の entities フィールド (RepeatedField<EntityProto>) を処理
//             foreach (EntityProto entity in initialConditions.Entities)
//             {
//                 Console.WriteLine($"  Entity ID: {entity.EntityID}");
//                 Console.WriteLine($"  URN: {entity.Urn}");
//                 Console.WriteLine("  Properties:");

//                 // 4. 各 EntityProto の properties フィールド (RepeatedField<PropertyProto>) を処理
//                 foreach (PropertyProto property in entity.Properties)
//                 {
//                     Console.Write($"    {property.Urn}: ");

//                     // 5. PropertyProto の oneof フィールド "value" に基づいて値を取得
//                     switch (property.ValueCase)
//                     {
//                         case PropertyProto.ValueOneofCase.IntValue:
//                             Console.WriteLine(property.IntValue);
//                             break;
//                         case PropertyProto.ValueOneofCase.BoolValue:
//                             Console.WriteLine(property.BoolValue);
//                             break;
//                         case PropertyProto.ValueOneofCase.DoubleValue:
//                             Console.WriteLine(property.DoubleValue);
//                             break;
//                         case PropertyProto.ValueOneofCase.ByteList:
//                             Console.WriteLine(BitConverter.ToString(property.ByteList.ToByteArray())); // バイト配列を文字列に変換
//                             break;
//                         case PropertyProto.ValueOneofCase.IntList:
//                             Console.WriteLine(string.Join(", ", property.IntList.Values));
//                             break;
//                         case PropertyProto.ValueOneofCase.IntMatrix:
//                             // IntMatrix の表示 (各 IntList を行として表示)
//                             Console.WriteLine("{");
//                             foreach (IntListProto intList in property.IntMatrix.Values)
//                             {
//                                 Console.WriteLine($"      [{string.Join(", ", intList.Values)}]");
//                             }
//                             Console.WriteLine("    }");
//                             break;
//                         case PropertyProto.ValueOneofCase.EdgeList:
//                             // EdgeList の表示
//                             Console.WriteLine("{");
//                             foreach (EdgeProto edge in property.EdgeList.Edges)
//                             {
//                                 Console.WriteLine($"      Start: ({edge.StartX}, {edge.StartY}), End: ({edge.EndX}, {edge.EndY}), Neighbour: {edge.Neighbour}");
//                             }
//                             Console.WriteLine("    }");
//                             break;
//                         case PropertyProto.ValueOneofCase.Point2D:
//                             Console.WriteLine($"({property.Point2D.X}, {property.Point2D.Y})");
//                             break;
//                         case PropertyProto.ValueOneofCase.None:
//                             Console.WriteLine("Undefined"); // 定義されていない場合
//                             break;

//                     }
//                 }
//                 Console.WriteLine();
//             }
//         }
//         else
//         {
//             Console.WriteLine("The log file does not contain InitialConditionsLogProto.");
//         }
//     }
// }
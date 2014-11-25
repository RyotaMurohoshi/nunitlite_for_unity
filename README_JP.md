# NUnitLiteForUnity - Run tests on any platform -

# 概要

　NUnitLiteForUnityは、iOSなどのAOTコンパイル環境下でもテスト可能なUnity用テストフレームワークです。

　UnityはPC、複数のモバイルプラットフォーム、いくつかのコンシューマゲーム機のプラットフォームなど様々なプラットフォームに向けてゲームを開発できるゲームエンジンです。プラットフォーム依存の機能を除き、共通のコードでゲームを作ることができます。　

　しかしiOSなどAOTコンパイルを行うプラットフォームでは、固有の問題を持っています。例えばあるメソッド(LINQのいくつかのメソッドなど)を実行した際、AOT関連のエラーが発生してしまいます。このエラーはUnityEditorやAndroidでは発生しません。

　例えばiOSでは、次のコードを実行すると、

```csharp
using UnityEngine;
using System.Linq;

public class Sample : MonoBehaviour
{
	void Start ()
	{
		int first = new []{0, 1, 2, 3}.FirstOrDefault ();
		Debug.Log (first);
	}
}
```

　エラーが発生します。Xcodeで次のようなエラーログが表示されます。

```
ExecutionEngineException: Attempting to JIT compile method 'System.Linq.Enumerable/PredicateOf`1<int>:.cctor ()' while running with --aot-only.


Rethrow as TypeInitializationException: An exception was thrown by the type initializer for PredicateOf`1
  at Sample.Start () [0x00000] in <filename unknown>:0 
```

　UnityEditorではエラーにならないが、iOSなど特定のプラットフォームで発生するエラーが存在するため、それぞれのプラットフォーム上でテストをすることが必要です。

　NUniteLiteForUnityは、iOSなどのAOT環境でも動作するようにカスタマイズしたNUnitLiteと、そのUnity用ラッパーを提供します。NUniteLiteForUnityを用いて、それぞれのプラットフォーム上でテストすることが可能です。

# 使い方

## テスト定義

　NUnitLite (言い換えるとNUnit)の作り方で、テストクラスを作ってください。

　つまり、

* テストクラスにTestFixture属性を付けてください
* テストメソッドにTest属性を付けてください
* テストメソッド内でAssertクラスを用いてください

　下記に簡単なサンプルを示します。

```csharp
using NUnit.Framework;

namespace NUnitLiteForUnity.Sample
{
	[TestFixture]
	public class SampleTest
	{
		[Test]
		public void TestSample ()
		{
			Assert.That (1 + 1, Is.EqualTo (2));
		}
	}
}
```

　テストクラスはEditorディレクトリ下でない、Assetsディレクトリ下に置いてください。NUnitLiteForUnityは、Assembly-CSharpに含まれる全てのテストクラスを実行します。Editor下に入れてしまうと、Assembly-CSharpにテストが含まれません。

　テストの書き方について更に詳しい情報は、[こちら](http://nunit.org/index.php?p=quickStart&r=3.0)を参考にしてください。 

## テストシーン定義

　それぞれのプラットフォームでテストするためにテストシーンを定義してください。

1. 新たに空のシーンを定義してください
2. 新たにGameObjectを作ってください
3. 2.で作ったGameObjectにTestRunnerBehaviourコンポーネントを付与してください
4. (任意)2.で作ったGameObjectをTestRunnerなどに変えてください

　Assets/NUnitLiteForUnity/Scenes/test_scene.unityも参考にしてください。


## テスト実行

　BuildSettingsで、テストシーンを最初に実行するシーンに設定してください。そして各プラットフォーム用にビルドし、ゲームを実行すれば、各プラットフォーム上でテストを実行することが可能です。

# プロジェクト構成内容
## Assets/Editor/UnityPackageExporter.cs

　NUnitLiteForUnityをエクスポートするためのスクリプトです。

## Assets/NUnitLiteForUnity/NUnitLite/nunitlite.dll

　NUnitLiteForUnity用にカスタマイズされたNUnitLiteのdllです。

## Assets/NUnitLiteForUnity/Sample/SampleTest.cs

　テストのサンプルです。SampleTestクラスはTestFixure属性が付与されたクラスで、Test属性がついたメソッドを持っています。

## Assets/NUnitLiteForUnity/Scenes/test_scene.unity
　テストシーンです。このシーンを実行するとテストが実行されます。TestRunnerBehaviourコンポーネントをもつGameObjectがシーン中に存在します。

## Assets/NUnitLiteForUnity/Scripts/NUnitLiteForUnityTestRunner.cs

　NUnitLiteForUnityTestRunnerクラスのRunTestsメソッドは、Assembly-CSharpアセンブリ内のテストを実行し、結果をDebug.Logメソッドで出力します。

## Assets/NUnitLiteForUnity/Scripts/TestRunnerBehaviour.cs

　MonoBehaviourを継承したクラスです。Startメソッドで、NUnitLiteForUnityTestRunnerクラスのRunTestsメソッドを呼び出します。

## Assets/NUnitLiteForUnity/LICENSE.txt

　NUnitLiteForUnityのライセンスです。

## Assets/NUnitLiteForUnity/CHANGES.md

　NUnitLiteForUnityの更新記録です。

# NUnitLiteとは？なぜNUnitLiteなのか？
　NUnitLiteは、NUnitのアイディアをベースにしその機能のサブセットを持つ、.NET向けの軽量テストフレームワークです。NUitLiteはオープンソースプロジェクトで、MITライセンスの下で利用できます。

　Unity Technologiesが作った[Unity Test Tools](https://www.assetstore.unity3d.com/jp/#!/content/13802)はNUnitを使っています。iOSのデバイス上でNUnitを用いてテストを実行した場合、AOT関連のエラーが発生してしまいます。ですが、NUnitLiteではそのようなエラーは発生しません。

　iOSを含め様々なプラットフォーム上でテストをするために、NUnitLiteForUnityでは、NUnitでなくNUnitLiteを用いました。

　NUnitLiteについて更に詳しくは、[こちら](http://nunitlite.org/)を読んでください。


# NUnitLiteのカスタマイズ内容
　iOSの実機でNUniteLiteを用いたテストをするために、オリジナルのNUnitLiteにいくつか変更を加えました。元のプロジェクトはgithubの[nunit/nunitlite](https://github.com/nunit/nunitlite)の[このコミット](3808d7fe49f8c24771ae804701adce0c21583d37)です。NUnitLiteForUnityプロジェクトに含まれているDLLはXamarin 5.5.4を用いて、.NET 2.0向けにビルドされたものです。


## TcpWriterの削除

　NUnitLite.Runner.TcpWriterクラスは、 System.Net.Sockets名前空間のクラスを使っています。

　System.Net.Sockets名前空間のクラスをiOSで使うためには、Unity iOS Proのライセンスが必要です。もしUnity iOS Proライセンスが無いならば、ビルド時に次のようなエラーが表示されます。

```
Error building Player: SystemException: System.Net.Sockets are supported only on Unity iOS Pro. Referenced from assembly 'nunitlite'.
```

　Unity iOS Proのライセンス無しでもテストできるように、NUnitLite.Runner.TcpWriterクラスを削除しました。

## WorkItemsのCompletedイベントをカスタマイズ

　NUnit.Framework.Internal.WorkItemsクラスはCompletedイベントを持っています。iOSなどAOT環境下では、外部DLLに含まれるクラス内のeventが発火されると、次のようなエラーが発生してしまいます。

```
ExecutionEngineException: Attempting to JIT compile method '(wrapper managed-to-native) System.Threading.Interlocked:CompareExchange (System.EventHandler&,System.EventHandler,System.EventHandler)' while running with --aot-only.

  at NUnit.Framework.Internal.WorkItems.WorkItem.add_Completed (System.EventHandler value) [0x00000] in <filename unknown>:0 
  at NUnit.Framework.Internal.WorkItems.CompositeWorkItem.RunChildren () [0x00000] in <filename unknown>:0 
  at NUnit.Framework.Internal.WorkItems.CompositeWorkItem.PerformWork () [0x00000] in <filename unknown>:0 
  at NUnit.Framework.Internal.WorkItems.WorkItem.RunTest () [0x00000] in <filename unknown>:0 
  at NUnit.Framework.Internal.WorkItems.WorkItem.Execute (NUnit.Framework.Internal.TestExecutionContext context) [0x00000] in <filename unknown>:0 
  at NUnit.Framework.Internal.NUnitLiteTestAssemblyRunner.Run (ITestListener listener, ITestFilter filter) [0x00000] in <filename unknown>:0 
  at NUnitLiteForUnity.NUnitLiteForUnityTestRunner.RunTests () [0x00000] in <filename unknown>:0 
  at NUnitLiteForUnity.TestRunnerBehaviour.Start () [0x00000] in <filename unknown>:0 
```

　このエラーを避けるために、次のような変更を加えました。

```csharp
// original code
// public event EventHandler Completed;

public event EventHandler Completed { add { completed += value; } remove { completed -= value; } }
EventHandler completed;
```

```csharp
// original code
// if (Completed != null)
//		 Completed(this, EventArgs.Empty);

if (completed != null)
	completed(this, EventArgs.Empty);
```

# 作者

室星亮太

* 投稿先:http://qiita.com/RyotaMurohoshi
* Twitter:https://twitter.com/RyotaMurohoshi

# ライセンス
MIT License です。